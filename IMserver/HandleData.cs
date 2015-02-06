using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using IMserver.CommonFuncs;
using IMserver.SubFuncs;
using IMserver.Data_Warehousing;
using SysLog;

namespace IMserver
{
    /// <summary>
    /// 这个类中的所有的方法都需在TypeofFrame()调用之后调用
    /// 当接收到数据时用来进行数据的处理
    /// 包括数据和合理性检验，即检验帧头
    /// 数据长度矫正，数据内容校验（CRC）
    /// </summary>
    public class HandleData
    {
        //用于测试读操作单元的结果
        public struct temporary
        {
            public bool readone;
            public object result;
        }
        public static temporary hello = new temporary();

        //编号和检查返回代码的封装
        public struct CheckRet
        {
            public byte packetnum;
            public int retcode;
        }

        //编号和typeofframe返回的封装
        public struct TypeRet
        {
            public byte packetnum;
            public int frametype;
        }

        //aftersend返回类
        public class AF_Ret
        {
            public byte packetnum;
            //主要用于一帧多包发送是判断是否为处理结束包的aftersend
            public bool multipacketover;
            //用于返回aftersend中的一些硬错，不是数据域表示内容的错误
            //主要用于回避byte作为包编号无法表示负数，即所有0-255都用作表示报编号
            //（存在错误包默认数值，影响后续处理）
            public int errorcode;
            ///分为单包解析成功和多包完成，单包解析看errorcode，如果为0表示单包解析成功，
            ///如果不为0，单包解析错误，完美情况下根据编号重发（根据缓存组包）
            //数据请求正常的情况下，返回的接收帧的处理结果
            public object result;
        }

        //字节数组转换之后的结构体，拿到外面，防止多次调用字节转结构体方法
        private static PrepareData.Msg_Bus framedata;

        /// <summary>
        /// 判断接收到的数据是长帧还是短帧,在发送之后接收到数据调用
        /// 可以使用共享变量中的发送缓冲区
        /// 注意：最初定义，添加参数构成重载忽略
        /// </summary>
        /// <param name="data">接收到的第一帧数据（针对长帧说，对于短帧则直接第一帧就是最后一帧）</param>
        /// <param name="len">接收到的第一帧数据的长度</param>
        /// <returns>
        /// 如果接收到的数据是长帧，返回1，同时获取整帧长度，循环接收，
        /// 直到在指定的可接受时间内接收到指定的长度，并调用长帧的处理方法
        /// 如果接收到的数据是短帧，返回0，并调用短帧处理方法
        /// </returns>
        public static int TypeofFrame(byte []data , object no)
        {
            //framedata = (PrepareData.Msg_Bus)ByteStruct.BytesToStruct(data, typeof(PrepareData.Msg_Bus));
            //接收帧为长帧，发送帧也为长帧
            if (data.Length > PrepareData.BUS_FRAME_MINLEN &&
                IMServerUDP.shared.sendbuffer.Length > PrepareData.BUS_FRAME_MINLEN)
                return 2;
            else
                //接收帧为短帧，发送帧为长帧
                if (data.Length == PrepareData.BUS_FRAME_MINLEN &&
                    IMServerUDP.shared.sendbuffer.Length > PrepareData.BUS_FRAME_MINLEN)
                    return 1;
                else
                    //接收帧为长帧，发送帧为短帧
                    if (data.Length > PrepareData.BUS_FRAME_MINLEN &&
                        IMServerUDP.shared.sendbuffer.Length == PrepareData.BUS_FRAME_MINLEN)
                        return -1;
                    else
                        //错误
                        return 0;
        }

        /// <summary>
        /// 重载：只判断接收帧，不讨论发送帧，发送帧由发送简介结构体
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TypeRet TypeofFrame(byte[] data)
        {
            TypeRet tr = new TypeRet();
            //截取编号
            byte[] packetnum = new byte[1];
            Array.Copy(data , 8 , packetnum , 0 , 1);
            tr.packetnum = packetnum[0];
            //接收帧为长帧
            if (data.Length > PrepareData.BUS_FRAME_MINLEN)
            {
                tr.frametype = 1;
            }
            else
                //接收帧为短帧
                if (data.Length == PrepareData.BUS_FRAME_MINLEN)
                {
                    tr.frametype = 0;
                }
                else
                {
                    //判长度错误，长度只能是大于或者等于最小数据帧长
                    tr.frametype = -1;
                }
            return tr;
        }

        /// <summary>
        /// 判断接受到的数据帧是否合法，返回各种情况的代码
        /// </summary>
        /// <param name="recvframe">接收数据数据结构体</param>
        /// <returns></returns>
        public static int FrameInCheck(byte[] b_recvframe, PrepareData.Msg_Bus justsend)
        {
            string frame_flag = Encoding.ASCII.GetString(b_recvframe, 0, 8); ;
            //MessageBox.Show(frame_flag);
            //无效的数据包(通过帧头检测来确认)
            if (PrepareData.FRAME_HEAD != frame_flag)
            {
                return (int)MSGEncoding.LastError.InvalidPackage;
            }
            else
            {
                PrepareData.Msg_Bus s_recvframe = (PrepareData.Msg_Bus)ByteStruct.BytesToStruct(b_recvframe , 
                                                                                                typeof(PrepareData.Msg_Bus));
                ushort crctemp = CRC16.CalculateCrc16(b_recvframe, PrepareData.CRCStart, PrepareData.CRCStop);

                //CRC校验失败
                if (s_recvframe.crc16 != crctemp)
                {
                    return (int)MSGEncoding.LastError.InvalidCRC;
                }
                //源设备号错误
                else
                    if (s_recvframe.srcID != justsend.srcID)
                    {
                        return (int)MSGEncoding.LastError.SrcIDError;
                    }
                    //目标设备号错误
                    else
                        if (s_recvframe.destID != justsend.destID)
                        {
                            return (int)MSGEncoding.LastError.DestIDError;
                        }
                        //错误的消息版本号
                        else
                            if (s_recvframe.msgVer != justsend.msgVer)
                            {
                                return (int)MSGEncoding.LastError.InvalidMsgVer;
                            }
                            //消息方向错误
                            else
                                if (s_recvframe.msgDir != justsend.msgDir)
                                {
                                    return (int)MSGEncoding.LastError.MsgDirError;
                                }
                                //消息类型错误
                                else
                                    if (s_recvframe.msgType != justsend.msgType)
                                    {
                                        return (int)MSGEncoding.LastError.MsgTypeError;
                                    }
                                    //子消息类型错误
                                    else
                                        if (s_recvframe.msgSubType != justsend.msgSubType)
                                        {
                                            return (int)MSGEncoding.LastError.MsgSubTypeError;
                                        }
                                        //消息检测通过正常
                                        else
                                            return 33;
            }
        }

        /// <summary>
        /// 当接收到的数据位短帧时，FrameInCheck
        /// 对接收到的数据帧进行检测，返回各种情况的代码
        /// 可以添加对接收到的数据是否为所请求的校验，与server中的结构体权衡
        /// 将以上错误归结到无效的数据包错误类型
        /// </summary>
        /// <param name="recvframe">接收到的数据字节数组</param>
        /// <returns></returns>
        public static int FrameInCheck(byte[] b_recvframe, byte[] justsend)
        {
            PrepareData.Msg_Bus send = (PrepareData.Msg_Bus)ByteStruct.BytesToStruct(justsend , typeof(PrepareData.Msg_Bus));

            string frame_flag= Encoding.ASCII.GetString(b_recvframe , 0 , 8);
            //MessageBox.Show(frame_flag);
            //无效的数据包(通过帧头检测来确认)
            if (!PrepareData.FRAME_HEAD.Equals(frame_flag))
            {
                return (int)MSGEncoding.LastError.InvalidPackage;
            }
            else
            {
                //承接接收到数据（字节数组）的结构体
                PrepareData.Msg_Bus s_recvframe = (PrepareData.Msg_Bus)ByteStruct.BytesToStruct(b_recvframe,
                                                                                                typeof(PrepareData.Msg_Bus));
                ushort crctemp = CRC16.CalculateCrc16(b_recvframe, 0, b_recvframe.Length-2);

                //CRC校验失败
                if (s_recvframe.crc16 != crctemp)
                {
                    return (int)MSGEncoding.LastError.InvalidCRC;
                }
                //源设备号错误
                else
                    if(s_recvframe.srcID != send.srcID)
                    {
                        return (int)MSGEncoding.LastError.SrcIDError;
                    }
                //目标设备号错误
                else
                    if(s_recvframe.destID != send.srcID)
                    {
                        return (int)MSGEncoding.LastError.DestIDError;
                    }
                //错误的消息版本号
                else
                    if(s_recvframe.msgVer != send.msgVer)
                    {
                        return (int)MSGEncoding.LastError.InvalidMsgVer;
                    }
                //消息方向错误
                else
                    if(s_recvframe.msgDir != (byte)MSGEncoding.MsgDir.Response)
                    {
                        return (int)MSGEncoding.LastError.MsgDirError;
                    }
                //消息类型错误
                else
                    if(s_recvframe.msgType != send.msgType)
                    {
                        return (int)MSGEncoding.LastError.MsgTypeError;
                    }
                //子消息类型错误
                else
                    if (s_recvframe.msgSubType != send.msgSubType)
                    {
                        return (int)MSGEncoding.LastError.MsgSubTypeError;
                    }
                //消息检测通过正常
                else
                        return 33;
            }
        }

        /// <summary>
        /// 用于接收端在没有发送数据对照的情况下校验数据帧
        /// 注意：不考虑帧内其他错误，直接CRC校验代替全部
        /// 而如果中断正常反应：需要根据帧类型及子类型组不同的数据库操作语句来查询相应请求
        /// </summary>
        /// <param name="recvframe">接收到的数据字节数组或者截取长帧中的短帧部分</param>
        /// <returns></returns>
        public static int FrameInCheck(byte[] b_recvframe)
        {
            string frame_flag = Encoding.ASCII.GetString(b_recvframe, 0, 8);
            //无效的数据包(通过帧头检测来确认)
            if (!PrepareData.FRAME_HEAD.Equals(frame_flag))
            {
                return (int)MSGEncoding.LastError.InvalidPackage;
            }
            else
            {
                //承接接收到数据（字节数组）的结构体
                PrepareData.Msg_Bus s_recvframe = (PrepareData.Msg_Bus)ByteStruct.BytesToStruct(b_recvframe,
                                                                                                typeof(PrepareData.Msg_Bus));
                ushort crctemp = CRC16.CalculateCrc16(b_recvframe, 0, 24);

                //CRC校验失败
                if (s_recvframe.crc16 != crctemp)
                {
                    return (int)MSGEncoding.LastError.InvalidCRC;
                }
                //消息检测通过正常
                else
                {
                    return 33;
                }
            }
        }

        /// <summary>
        /// 当接收到的数据位短帧时，FrameInCheck
        /// 对接收到的数据帧进行检测，返回各种情况的代码
        /// 可以添加对接收到的数据是否为所请求的校验，与server中的结构体权衡
        /// 将以上错误归结到无效的数据包错误类型
        /// </summary>
        /// <param name="recvframe">接收到的数据字节数组</param>
        /// <returns></returns>
        public static int FrameInCheck(byte[] b_recvframe, PrepareData.Compare comtemp)
        {
            string frame_flag = Encoding.ASCII.GetString(b_recvframe, 0, 8);
            //MessageBox.Show(frame_flag);
            //无效的数据包(通过帧头检测来确认)
            if (!PrepareData.FRAME_HEAD.Equals(frame_flag))
            {
                return (int)MSGEncoding.LastError.InvalidPackage;
            }   
            else
            {
                //承接接收到数据（字节数组）的结构体
                PrepareData.Msg_Bus s_recvframe = (PrepareData.Msg_Bus)ByteStruct.BytesToStruct(b_recvframe,
                                                                                                typeof(PrepareData.Msg_Bus));
                ushort crctemp = CRC16.CalculateCrc16(b_recvframe, 0, b_recvframe.Length - 2);
                //action：这里从头开始轮训只要有个错误就会返回，之后的不再考虑，这里改变if-else结构获取错误列表意义不大
                //CRC校验失败
                if (s_recvframe.crc16 != crctemp)
                {
                    return (int)MSGEncoding.LastError.InvalidCRC;
                }

                //源设备号错误
                else
                    if (s_recvframe.srcID != comtemp.destID)
                    {
                        return (int)MSGEncoding.LastError.SrcIDError;
                    }
                    //目标设备号错误
                    else
                        if (s_recvframe.destID != comtemp.srcID)
                        {
                            return (int)MSGEncoding.LastError.DestIDError;
                        }
                        //错误的消息版本号
                        else
                            if (s_recvframe.msgVer != comtemp.msgVer)
                            {
                                return (int)MSGEncoding.LastError.InvalidMsgVer;
                            }
                            //消息方向错误
                            else
                                if (s_recvframe.msgDir != (byte)MSGEncoding.MsgDir.Response)
                                {
                                    return (int)MSGEncoding.LastError.MsgDirError;
                                }
                                //消息类型错误
                                else
                                    if (s_recvframe.msgType != comtemp.msgType)
                                    {
                                        return (int)MSGEncoding.LastError.MsgTypeError;
                                    }
                                    //子消息类型错误
                                    else
                                        if (s_recvframe.msgSubType != comtemp.msgSubType)
                                        {
                                            return (int)MSGEncoding.LastError.MsgSubTypeError;
                                        }
                                        //消息检测通过正常
                                        else
                                            return 33;
            }
        }

        /// <summary>
        /// 当接收到的数为长数据帧，调用FrameOutCheck
        /// 发送的和接受的都有课能是长帧
        /// </summary>
        /// <param name="b_recvframe">接收到的长数据帧</param>
        /// <param name="justsend">刚刚发送数据帧</param>
        /// <returns>如果是帧外CRC校验错误，返回44</returns>
        public static int FrameOutCheck(byte[] b_recvframe, PrepareData.Compare comtemp)
        {
            byte[] framein = new byte[PrepareData.BUS_FRAME_MINLEN];
            byte[] frameout = new byte[b_recvframe.Length - PrepareData.BUS_FRAME_MINLEN];
            Array.Copy(b_recvframe, 0, framein, 0, framein.Length);
            Array.Copy(b_recvframe, PrepareData.BUS_FRAME_MINLEN, frameout, 0, frameout.Length);
            int incheckcode = FrameInCheck(framein , comtemp);

            //帧内数据错误，如果有帧外数据也不再检测，直接返回帧内的错误代码
            if (incheckcode != 33)
            {
                return incheckcode;
            }
            else
            {
                //帧外数据的CRC校验
                ushort crccalc = CRC16.CalculateCrc16(frameout, 0, frameout.Length - 2);
                byte []crcbytes = new byte[2];
                Array.Copy(frameout , frameout.Length-2 , crcbytes , 0 , 2);
                ushort crcrecv = (ushort)ByteStruct.BytesToStruct(crcbytes , typeof(ushort));

                //判断发送数据是否有错误
                if (crccalc != crcrecv)
                {
                    //MessageBox.Show("帧外数据CRC校验错误！");
                    return 44;
                }
                else
                    return 33;
            }
        }

        /// <summary>
        /// framecheckin的更改并未动本质，只是返回值更改为目前需要的结构体，将原返回值和附加封装一下
        /// </summary>
        /// <param name="b_recvframe">修改后从队列中取得</param>
        /// <param name="comtemp">修改后从查表得来</param>
        /// <returns></returns>
        public static CheckRet FrameInCheck_New(byte[] b_recvframe, PrepareData.Compare comtemp)
        {
            //返回结构体
            CheckRet cir = new CheckRet();
            string frame_flag = Encoding.ASCII.GetString(b_recvframe, 0, 8);
            //MessageBox.Show(frame_flag);
            //无效的数据包(通过帧头检测来确认)
            if (!PrepareData.FRAME_HEAD.Equals(frame_flag))
            {
                cir.retcode = (int)MSGEncoding.LastError.InvalidPackage;
            }
            else
            {
                //承接接收到数据（字节数组）的结构体
                PrepareData.Msg_Bus s_recvframe = (PrepareData.Msg_Bus)ByteStruct.BytesToStruct(b_recvframe,
                                                                                                typeof(PrepareData.Msg_Bus));
                cir.packetnum = s_recvframe.msgID;

                ushort crctemp = CRC16.CalculateCrc16(b_recvframe, 0, b_recvframe.Length - 2);
                //action：这里从头开始轮训只要有个错误就会返回，之后的不再考虑，这里改变if-else结构获取错误列表意义不大
                //CRC校验失败
                if (s_recvframe.crc16 != crctemp)
                {
                    cir.retcode = (int)MSGEncoding.LastError.InvalidCRC;
                }

                //源设备号错误
                else
                    if (s_recvframe.srcID != comtemp.destID)
                    {
                        cir.retcode = (int)MSGEncoding.LastError.SrcIDError;
                    }
                    //目标设备号错误
                    else
                        if (s_recvframe.destID != comtemp.srcID)
                        {
                            cir.retcode = (int)MSGEncoding.LastError.DestIDError;
                        }
                        //错误的消息版本号
                        else
                            if (s_recvframe.msgVer != comtemp.msgVer)
                            {
                                cir.retcode = (int)MSGEncoding.LastError.InvalidMsgVer;
                            }
                            //消息方向错误
                            else
                                if (s_recvframe.msgDir != (byte)MSGEncoding.MsgDir.Response)
                                {
                                    cir.retcode = (int)MSGEncoding.LastError.MsgDirError;
                                }
                                //消息类型错误
                                else
                                    if (s_recvframe.msgType != comtemp.msgType)
                                    {
                                        cir.retcode = (int)MSGEncoding.LastError.MsgTypeError;
                                    }
                                    //子消息类型错误
                                    else
                                        if (s_recvframe.msgSubType != comtemp.msgSubType)
                                        {
                                            cir.retcode = (int)MSGEncoding.LastError.MsgSubTypeError;
                                        }
                                        //消息检测通过正常
                                        else
                                            cir.retcode = 33;
            }
            return cir;
        }

        /// <summary>
        /// 区别主要看FrameInCheck
        /// </summary>
        /// <param name="b_recvframe"></param>
        /// <param name="comtemp"></param>
        /// <returns></returns>
        public static CheckRet FrameOutCheck_New(byte[] b_recvframe, PrepareData.Compare comtemp)
        {
            byte[] framein = new byte[PrepareData.BUS_FRAME_MINLEN];
            byte[] frameout = new byte[b_recvframe.Length - PrepareData.BUS_FRAME_MINLEN];
            Array.Copy(b_recvframe, 0, framein, 0, framein.Length);
            Array.Copy(b_recvframe, PrepareData.BUS_FRAME_MINLEN, frameout, 0, frameout.Length);
            CheckRet incheckret = FrameInCheck_New(framein, comtemp);

            //帧内数据错误，如果有帧外数据也不再检测，直接返回帧内的错误代码
            if (33 == incheckret.retcode)
            {
                //帧外数据的CRC校验
                ushort crccalc = CRC16.CalculateCrc16(frameout, 0, frameout.Length - 2);
                byte[] crcbytes = new byte[2];
                Array.Copy(frameout, frameout.Length - 2, crcbytes, 0, 2);
                ushort crcrecv = (ushort)ByteStruct.BytesToStruct(crcbytes, typeof(ushort));

                //判断发送数据是否有错误
                if (crccalc != crcrecv)
                {
                    //MessageBox.Show("帧外数据CRC校验错误！");
                    incheckret.retcode = 44;
                }
                else
                    incheckret.retcode = 33;
             }
            return incheckret;
        }

        ///不需要在这里检测接收到的响应是否与发送的请求相对应
        ///如果不对应，会在返回的数据中显示错误代码，全帧的合理性由CRC决定
        public static int FrameOutCheck(byte[] b_recvframe)
        {
            //ushort crcbycalc = CRC16.CalculateCrc16(b_recvframe, 0, b_recvframe.Length - 2);
            //byte[] data = new byte[2];
            //Array.Copy(b_recvframe, b_recvframe.Length - 2, data, 0, 2);
            //ushort crcinframe = (ushort)ByteStruct.BytesToStruct(data , typeof(ushort));
            //if (crcinframe == crcbycalc)
            //{
            //    return 33;
            //}
            //else
            //{
            //    return 44;
            //}
            byte[] framein = new byte[PrepareData.BUS_FRAME_MINLEN];
            byte[] frameout = new byte[b_recvframe.Length - PrepareData.BUS_FRAME_MINLEN];
            Array.Copy(b_recvframe, 0, framein, 0, PrepareData.BUS_FRAME_MINLEN);
            Array.Copy(b_recvframe, PrepareData.BUS_FRAME_MINLEN, frameout, 0, frameout.Length);
            int incheckcode = FrameInCheck(framein);

            //帧内数据错误，如果有帧外数据也不再检测，直接返回帧内的错误代码
            if (incheckcode != 33)
            {
                return incheckcode;
            }
            else
            {
                //帧外数据的CRC校验
                ushort crccalc = CRC16.CalculateCrc16(frameout, 0, frameout.Length - 2);
                byte[] crcbytes = new byte[2];
                Array.Copy(frameout, frameout.Length - 2, crcbytes, 0, 2);
                ushort crcrecv = (ushort)ByteStruct.BytesToStruct(crcbytes, typeof(ushort));

                //判断发送数据是否有错误
                if (crccalc != crcrecv)
                {
                    //MessageBox.Show("帧外数据CRC校验错误！");
                    return 44;
                }
                else
                    return 33;
            }
        }

        /// <summary>
        /// 用于消息处理线程，定时扫描接受队列，如果存在待处理项目，就出队处理
        /// </summary>
        public static void AnalysisQueue()
        {
            while (true)
            {
                if (Define.recv_queue.Count != 0)
                {
                    Define.RecvQueueItem drq = Define.recv_queue.Dequeue();
                    //调用aftersend方法处理队列
                    //处理完成之后清除计时队列中的对应项
                    AF_Ret temp = AfterSend(drq.recvdata);

                    //读操作单元专项开始
                    hello.result = temp.result;
                    hello.readone = true;
                    //读操作单元专项结束

                    byte packetnum = temp.packetnum;//aftersend处理后返回的编号
                    if (true == temp.multipacketover)
                    {
                        Define.existime.Remove(packetnum);
                    }
                }
                else
                {
                    Thread.Sleep(50);
                }
            }
        }

        /// <summary>
        /// 从接收队列中取出一条记录来处理
        /// 发送之后的一切处理过程
        /// aftersend方法中的error和errornum等需要外部交互的变量在整合是调出
        /// </summary>
        /// <param name="comstruct">发送数据的核对留样
        ///                         根据解析出的数据帧编码确定comstruct</param>
        /// <param name="sender">各类数据帧组织所需要的数据结构（ushort[]、dictionary<string , string>）
        ///                      根据解析出的数据帧编号确定sender</param>
        /// <param name="recvbuffer">只有有效数据的数据信息，空余字节已剔除</param>        
        /// <param name="donewrite">专门为文件的读写做已读字节的标注，即下次开始读写的位置</param>
        /// <returns>新增功能：主要用于返回数据包的编号</returns>
        public static AF_Ret AfterSend(byte[] datatemp)
        {
            //返回帧编码及对于一帧多包是否为处理最后一包的aftersend
            AF_Ret afret = new AF_Ret();
            //这两个配合在读写操作单元的时候确定有多少出错，具体错误代码是什么
            ushort errornum = 0;
            Dictionary<ushort, ushort> error = new Dictionary<ushort, ushort>();

            int retcode = 0;
            HandleData.TypeRet hdtr = HandleData.TypeofFrame(datatemp);
            afret.packetnum = hdtr.packetnum;
            //取得摘要信息
            PrepareData.Compare comstruct = (PrepareData.Compare)Define.index_com[hdtr.packetnum];
            //接收到的包是允许时间内发送出去请求的响应（即及时队列中存在响应的原始请求）
            if (Define.existime.Keys.Contains(hdtr.packetnum))
            {
                //正常处理
                //这里应封装原始的所有的正常处理
                //取得缓存信息
                object sender = Define.index_obj[hdtr.packetnum];

                if (0 == hdtr.frametype)
                {
                    retcode = HandleData.FrameInCheck(datatemp, comstruct);
                }
                else
                    if (1 == hdtr.frametype)
                    {
                        retcode = HandleData.FrameOutCheck(datatemp, comstruct);
                    }
                    else
                    {
                        retcode = 0;
                        //MessageBox.Show("长帧和短帧类型判断错误！");
                    }

                //在这之前的framecheck已经对接收帧和compare做了对比，不存在是以解析出的帧描述消息或者
                //摘要信息为标准，都是统一的
                if (33 == retcode)
                {
                    switch (comstruct.msgType)
                    {
                        //通信测试
                        case (byte)MSGEncoding.MsgType.LoopBack:
                            {
                                //对于loopback来说的sender为发送下去的通信测试帧的CRC校验码=>改为发送的字节数组原始数据
                                //通过比对CRC校验码（返回）=>不如直接字节数组比较方便
                                byte[] rawdata = (byte[])sender;
                                bool result = LoopBack.MsgHandle(datatemp, rawdata);
                                afret.multipacketover = true;
                            }
                            break;
                        //读操作单元
                        case (byte)MSGEncoding.MsgType.ReadUnit:
                            {
                                //公用error和errornum
                                Dictionary<ushort, object> result = ReadUnit.MsgHandle((ushort[])sender,
                                                                                        comstruct,
                                                                                        datatemp,
                                                                                        ref errornum,
                                                                                        error);
                                if (0 == errornum)
                                {
                                    try
                                    {
                                        //读取数据（开始界面）
                                        if ((byte)MSGEncoding.ReadUint.ReadData == comstruct.msgSubType)
                                        {
                                            AddRunningState.Warehousing(result, comstruct.destID);
                                        }
                                        else   //读取配置信息
                                            if ((byte)MSGEncoding.ReadUint.Readcfg == comstruct.msgSubType)
                                            {
                                                AddConfig.Warehousing(result, comstruct.destID);
                                            }
                                            else  //这里应该是状态控制
                                                if ((byte)MSGEncoding.ReadUint.GetDevStatus == comstruct.msgSubType)
                                                {
                                                    //这个还未完成，无法到达
                                                    AddSIML.Warehousing(result, comstruct.destID);
                                                }
                                                else
                                                {
                                                    AddAlarmState.Warehousing(result, comstruct.destID);
                                                }
                                    }
                                    catch (Exception ep)
                                    {
                                        LogException(ep);
                                    }
                                }
                                else
                                {
                                    //读取操作单元过程出错！
                                }
                                afret.result = result;
                                afret.multipacketover = true;
                            }
                            break;
                        //写操作单元
                        case (byte)MSGEncoding.MsgType.WriteUnit:
                            {
                                //提取发送的数据域原始
                                Dictionary<ushort, object> rawobj = (Dictionary<ushort, object>)sender;
                                ushort[] units = new ushort[rawobj.Count];
                                int i = 0;
                                //得到写下的操作单元数组
                                foreach (KeyValuePair<ushort, object> kvp in rawobj)
                                {
                                    units[i++] = kvp.Key;
                                }

                                int result = WriteUnit.MsgHandle(error, ref errornum, datatemp, units);

                                if (1 == result)
                                {
                                    //MessageBox.Show("handledata-aftersend:写操作单元成功！");
                                    try
                                    {
                                        //初始界面
                                        if ((byte)MSGEncoding.WriteUint.WriteData == comstruct.msgSubType)
                                        {
                                            AddRunningState.Warehousing(rawobj, comstruct.destID);
                                        }
                                        else   //写下配置信息
                                            if ((byte)MSGEncoding.WriteUint.Setcfg == comstruct.msgSubType)
                                            {
                                                AddConfig.Warehousing(rawobj, comstruct.destID);
                                            }
                                            else  //这里应该是状态控制
                                                if ((byte)MSGEncoding.WriteUint.ControlDev == comstruct.msgSubType)
                                                {
                                                    //这个还未完成，无法到达
                                                    AddSIML.Warehousing(rawobj, comstruct.destID);
                                                }
                                                else  //这里应该是写报警
                                                {
                                                    AddAlarmState.Warehousing(rawobj, comstruct.destID);
                                                }
                                    }
                                    catch (Exception ep)
                                    {
                                        LogException(ep);
                                    }
                                }
                                else
                                {
                                    //MessageBox.Show("handledata-aftersend:写操作单元存在错误！");
                                }
                                afret.multipacketover = true;
                            }
                            break;
                        //读文件【donewrite只做写文件使用】
                        case (byte)MSGEncoding.MsgType.ReadFile:
                            {
                                //交互过程
                                ushort errorcode = 0;
                                ReadFile.Parameter rfp = (ReadFile.Parameter)sender;
                                //这些在第一次组包的时候都已初始化
                                //string path = "s:\\" + rfp.filename + ".txt";
                                //FileStream filerecv = new FileStream(path, FileMode.OpenOrCreate,
                                //                                     FileAccess.Write);
                                //文件接收发在缓存中考虑建立临时文件
                                int thisdone = ReadFile.MsgHandle(hdtr, datatemp, ref errorcode);
                                //如果写入0字节，那么有两种情况：A，文件读取完毕（error为0）B，文件读取出错（error不为0）
                                if (0 == thisdone)
                                {
                                    //文件读取完成
                                    if (0 == errorcode)
                                    {
                                        //MessageBox.Show("IMServerUDP:文件读取完成！");
                                    }
                                    //文件读取出错
                                    else
                                    {
                                        //MessageBox.Show("IMServerUDP:读取文件时出错！");
                                    }
                                    afret.multipacketover = true;
                                }
                                //如果读取到大于0的字节，说明正在读取文件，如果读取到小于指定读取字节个数，应该是文件读完，
                                //但是为了统一写文件，这个交互之后再次发送请求，下面解析到请求读取超过文件长度的信息，
                                //会返回文件读取结束（读到0字节并且error=0）
                                else
                                {
                                    ReadFile.FileItem rffi = (ReadFile.FileItem)ReadFile.filepool[hdtr.packetnum];
                                    rfp.start = rffi.Index;
                                    rfp.readbytes = Define.TransFile_MAX;
                                    PrepareData.AddRequire(comstruct, rfp, hdtr.packetnum);
                                    afret.multipacketover = false;
                                }
                            }
                            break;
                        //写文件
                        case (byte)MSGEncoding.MsgType.WriteFile:
                            {
                                //写文件的交互过程，需要传结构体，结构体中数据需要返回并有这里参考循环
                                //本函数中传入的donewrite为after之前packet中读取的字节数
                                //文件读取到哪里由返回的响应帧的数据为标准移动
                                ushort errorcode = 0;
                                WriteFile.Parameter wfp = (WriteFile.Parameter)sender;
                                //返回帧中上送的下位机写入的字节数
                                ushort thisdone = WriteFile.MsgHandle(datatemp, ref errorcode);
                                //本次发送了0字节数据【文件读取完成或者文件读取出错】
                                if (0 == thisdone)
                                {
                                    if (0 == errorcode)
                                    {
                                        wfp.filehandle.Flush();
                                        wfp.filehandle.Close();
                                        //MessageBox.Show("IMServerUDP:文件发送完成！");
                                    }
                                    else
                                    {
                                        //MessageBox.Show("IMServerUDP:文件传输出错！");
                                    }
                                    afret.multipacketover = true;
                                }
                                //文件读取正常，这里是否讨论请求和响应不一致的情况，为错误的一种情况
                                else
                                {
                                    //WriteFile.FileItem wffi = (WriteFile.FileItem)WriteFile.filepool[hdtr.packetnum];
                                    //wffi.FileIndex += thisdone;
                                    ////写回以更新缓存
                                    //WriteFile.filepool.Add(hdtr.packetnum , wffi);
                                    //wfp.start = wffi.FileIndex;
                                    wfp.start += thisdone;
                                    wfp.writebytes = Define.TransFile_MAX;
                                    PrepareData.AddRequire(comstruct, wfp, hdtr.packetnum);
                                    afret.multipacketover = false;
                                }
                            }
                            break;
                        //获取文件信息
                        //独属于获取文件信息的错误代码
                        case (byte)MSGEncoding.MsgType.GetFileInfo:
                            {
                                ushort sorterror = 0;
                                Dictionary<long, ushort> fileinfo = GetFileInfo.MsgHandle(ref sorterror, datatemp);
                                if (0 == sorterror)
                                {
                                    //MessageBox.Show("handledata-aftersend:获取文件信息处理完成！");
                                    for (int i = 0; i < fileinfo.Count; i++)
                                    {
                                        //MessageBox.Show("第 " + i.ToString()+ "个文件名为"+fileinfo.ElementAt(i).Key.ToString()+"大小为"+fileinfo.ElementAt(i).Value.ToString());
                                    }
                                }
                                else
                                {
                                    //MessageBox.Show("handledata-aftersend:获取文件信息错误！");
                                }
                                afret.multipacketover = true;
                            }
                            break;
                        //读缓冲区
                        case (byte)MSGEncoding.MsgType.ReadBuffer:
                            {
                                //根据请求的数据帧的子类型确定返回值
                                object result = ReadBuffer.MsgHandle(datatemp, comstruct.msgSubType);

                                afret.multipacketover = false;
                            }
                            break;
                        //获取错误信息
                        //查询出现错误情况，后期结合具体情况讨论
                        case (byte)MSGEncoding.MsgType.GetErrorInfo:
                            {
                                string errorexplain = GetErrorInfo.MsgHandle(datatemp);
                                if (null == errorexplain)
                                {
                                    //MessageBox.Show("获取错误信息失败，内容上的失败，因为已经经过校验");
                                }
                                else
                                {
                                    //MessageBox.Show("获取错误信息成功！");
                                    //MessageBox.Show(errorexplain);
                                }
                                afret.multipacketover = true;
                            }
                            break;
                        default:
                            {
                                retcode = -2;
                                //MessageBox.Show( "Handledata—aftersend:接收帧的类型错误！"+
                                                 //"（经校验，应该不会出现之类的错误！）");
                            }
                            break;
                    }

                }
                else
                {
                    //MessageBox.Show("aftersend中：接收的数据帧存在错误！校验中存在错误");
                }
            }
            else
            {
                retcode = -1;
                //接收到的包是超时后才响应的包
                //MessageBox.Show("此响应包的请求包已经超时，剔除计时队列！");
            }
            afret.errorcode = retcode;
            return afret;
        }

        /// <summary>
        /// 异常信息写入日志
        /// </summary>
        /// <param name="ex"></param>
        private static void LogException(Exception ex)
        {
            using (ILog log = new FileLog())
            {
                log.Write("Exception from Class: HandleData");
                log.Write("Exception:" + ex.Message);
                log.Write(ex.StackTrace);
                log.Write("时间：" + DateTime.Now.ToString());
                log.Write("----------------------------------------------------------");
            }
        }
    }
}
              #region  未将数据翻译拆分的msghandle方法
/// <summary>
/// 重载：帧检测在处理函数之外，避免返回类型不统一
/// 数据帧的处理，是发送数据帧之后接收到相应的情况下
/// 首先只考虑读操作单元的接收数据处理
/// </summary>
/// <param name="unit"></param>
/// <param name="comtemp"></param>
/// <param name="b_recvframe"></param>
/// <param name="errornum">（在外应该初始化为0）ref 做错误数目参数，为0表示么有错误，为非零正整数表示错误的个数</param>
/// <param name="error"> ref 做具体错误藐视 ， 操作单元对应错误代码</param>
/// <returns></returns>
//        public static Dictionary<ushort, object> MsgHandle(ushort[] unit, PrepareData.Compare comtemp, byte[] b_recvframe,
//                                                           ref ushort errornum, ref Dictionary<ushort, ushort> error)
//        {
//            Dictionary<ushort, object> retinfo = new Dictionary<ushort, object>();
//            switch (TypeofFrame(b_recvframe))
//            {
//                //接收的是长帧
//                case 1:
//                    {
//                        //接收数据的拆分
//                        byte[] recvframeout = new byte[b_recvframe.Length - PrepareData.BUS_FRAME_MINLEN];
//                        Array.Copy(b_recvframe, 0, recvframeout, 0, b_recvframe.Length - PrepareData.BUS_FRAME_MINLEN);

//                        //操作单元的个数，这里是从发送帧中间接得来
//                        int basenum = unit.Length;

//                        //发来字节中的执行状态字所占字节数
//                        int basebyte = (int)Math.Ceiling(basenum / 8.0);

//                        //存放执行状态字
//                        byte[] b_basebyte = new byte[basebyte];
//                        Array.Copy(recvframeout, 0, b_basebyte, 0, basebyte);

//                        //返回的值数据信息（不包括执行状态字）
//                        byte[] onlydata = new byte[recvframeout.Length - basebyte - 2];
//                        Array.Copy(recvframeout, basebyte, onlydata, 0, recvframeout.Length - basebyte - 2);

//                        //放弃从后向前转存，直接从头开始读取
//                        int quotient = basenum / 8;  //执行状态字占用的整字节
//                        int remainder = basenum % 8; //执行状态字不足一个字节部分
//                        int m = 0;                   //返回数据字节数组的下标
//                        int index = 0;               //操作单元数组的下标
//                        for (int i = 0; i < basebyte; i++)
//                        {
//                            //处理如果存在少于8个执行状态字的首个字节
//                            if (remainder != 0)
//                            {
//                                for (int j = remainder - 1; j >= 0; j--)
//                                {
//                                    //对应操作单元的执行状态字是否成功
//                                    int result = b_basebyte[i] & (byte)(int)Math.Pow(2, j);
//                                    //哪一个执行状态字
//                                    //int index = remainder - j - 1;
//                                    //请求的操作单元返回错误
//                                    if (0 == result)
//                                    {
//                                        //获取错误代码
//                                        byte[] temp = new byte[2];
//                                        Array.Copy(onlydata, m, temp, 0, 2);
//                                        //推移移动的位数
//                                        m += 2;
//                                        ushort errorcode = (ushort)ByteStruct.BytesToStruct(temp, typeof(ushort));
//                                        //errorcode处理
//                                        errornum++;
//                                        error.Add(unit[index], errorcode);
//                                        index++;
//                                        //获取操作单元号（非结构体通信下读两个字节），获取错误代码存入
//                                        //获取操作单元和值存入
//                                    }
//                                    else
//                                    {
//                                        //对应操作单元的值类型所占空间
//                                        ushort count = MyDictionary.unitlendict[unit[index]];
//                                        byte[] temp = new byte[count];
//                                        Array.Copy(onlydata, m, temp, 0, count);
//                                        m += count;
//                                        object value = GetValue(temp, unit[index]);
//                                        retinfo.Add(unit[index], value);
//                                        index++;
//                                    }
//                                }
//                                //只处理一遍
//                                remainder = 0;
//                            }
//                            else
//                            {
//                                for (int j = 8 - 1; j >= 0; j--)
//                                {
//                                    int result = b_basebyte[i] & (byte)(int)Math.Pow(2, j);
//                                    //int index = i * 8 + 7 -j;
//                                    //请求的操作单元返回错误
//                                    if (0 == result)
//                                    {
//                                        //获取错误代码
//                                        byte[] temp = new byte[2];
//                                        Array.Copy(onlydata, m, temp, 0, 2);
//                                        //推移移动的位数
//                                        m += 2;
//                                        ushort errorcode = (ushort)ByteStruct.BytesToStruct(temp, typeof(ushort));
//                                        //errorcode处理
//                                        errornum++;
//                                        error.Add(unit[index], errorcode);
//                                        index++;
//                                        //获取操作单元号（非结构体通信下读两个字节），获取错误代码存入
//                                        //获取操作单元和值存入
//                                    }
//                                    else
//                                    {
//                                        ushort count = MyDictionary.unitlendict[unit[index]];
//                                        byte[] temp = new byte[count];
//                                        Array.Copy(recvframeout, m, temp, 0, count);
//                                        m += count;
//                                        object value = GetValue(temp, unit[index]);
//                                        retinfo.Add(unit[index], value);
//                                        index++;
//                                    }
//                                }
//                            }
//                        }
//                           #region  从右边去第一不方便，反相的对应取
//                        //for (int i = 0; i < basebyte; i++)
//                        //{
//                        //    byte low = sendframeout[sendframeout.Length - 2 * (i + 2)];
//                        //    byte high = sendframeout[sendframeout.Length - 2 * (i + 2) + 1];
//                        //    ushort temp = (ushort)(((ushort)(high << 8)) | low);
//                        //    //读最后一位执行状态字，看是否有错误，有错误则加到错误列表，并错误数加1
//                        //    //没有错误则读取倒数第一个值，与操作单元一起加入数据字典，取值时长度由数据字典截取
//                        //}
//#endregion
//                    }
//                    break;
//                //接收帧为短帧
//                case 0:
//                    {
//                        byte[] onlydata = ((PrepareData.Msg_Bus)ByteStruct.BytesToStruct(b_recvframe,
//                                                                                         typeof(PrepareData.Msg_Bus))).data;

//                    }
//                    break;
//                //接收帧为长帧，发送帧为短帧
//                case -1:
//                    {
//                        //接收数据的拆分
//                        byte[] recvframein = new byte[PrepareData.BUS_FRAME_MINLEN];
//                        byte[] recvframeout = new byte[originaldata.Length - PrepareData.BUS_FRAME_MINLEN];
//                        Array.Copy(originaldata, 0, recvframein, 0, recvframein.Length);
//                        Array.Copy(originaldata, PrepareData.BUS_FRAME_MINLEN, recvframeout, 0, recvframeout.Length);
//                        //帧内数据的检查
//                        int incheckcode = FrameInCheck(recvframein, IMServerUDP.shared.sendbuffer);
//                        int outcheckcode = FrameOutCheck(recvframeout);
//                        if (incheckcode != 33 || outcheckcode != 33)
//                        {
//                            if (incheckcode != 33)
//                            {
//                                MessageBox.Show("帧内数据存在错误!");
//                            }
//                            else
//                            {
//                                MessageBox.Show("帧外数据存在错误!");
//                            }
//                            return null;
//                        }
//                    }
//                    break;
//                //返回错误的代码
//                case 0:
//                    {
//                        MessageBox.Show("返回错误的代码");
//                    }
//                    break;
//                //返回的代码错误
//                default:
//                    {
//                        MessageBox.Show("返回的代码错误");
//                    }
//                    break;
//            }
//        }
#endregion

              #region   重载的未完成的msghandle方法，帧检测在方法内完成
        ///// <summary>
        ///// 数据帧的处理，是发送数据帧之后接收到相应的情况下
        ///// 首先只考虑读操作单元的接收数据处理
        ///// </summary>
        ///// <param name="originaldata"></param>
        ///// <param name="errornum">ref 做错误数目参数，为0表示么有错误，为非零正整数表示错误的个数</param>
        ///// <param name="error"> ref 做具体错误藐视 ， 操作单元对应错误代码</param>
        ///// <returns></returns>
        //public static Dictionary<ushort , object> MsgHandle(byte [] originaldata , ref ushort errornum ,
        //                                                    ref Dictionary <ushort , ushort> error)
        //{
        //    Dictionary<ushort, object> retinfo = new Dictionary<ushort, object>();
        //    switch (TypeofFrame(originaldata))
        //    {
        //        //接收和发送的全是长帧
        //        case 2:
        //            {
        //                //接收数据的拆分
        //                byte [] recvframein = new byte[PrepareData.BUS_FRAME_MINLEN];
        //                byte [] recvframeout = new byte[originaldata.Length - PrepareData.BUS_FRAME_MINLEN];
        //                byte [] thisunitcode = new byte[recvframeout.Length - 2];
        //                Array.Copy(recvframeout , 0 , thisunitcode , 0 , thisunitcode.Length);
        //                Array.Copy(originaldata, 0, recvframein, 0, recvframein.Length);
        //                Array.Copy(originaldata, PrepareData.BUS_FRAME_MINLEN, recvframeout, 0, recvframeout.Length);

        //                //发送数据的拆分
        //                byte[] sendframein = new byte[PrepareData.BUS_FRAME_MINLEN];
        //                byte[] sendframeout = new byte[originaldata.Length - PrepareData.BUS_FRAME_MINLEN];
        //                Array.Copy(originaldata, 0, sendframein, 0, sendframein.Length);
        //                Array.Copy(originaldata, PrepareData.BUS_FRAME_MINLEN, sendframeout, 0, sendframeout.Length);

        //                //帧内数据的检查
        //                int incheckcode = FrameInCheck(recvframein , sendframein);
        //                int outcheckcode = FrameOutCheck(recvframeout);
        //                if (incheckcode != 33 || outcheckcode != 33)
        //                {
        //                    if (incheckcode != 33)
        //                    {
        //                        MessageBox.Show("帧内数据检测由错误！");
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("帧外数据检测有错误！");
        //                    }
        //                    return null ;
        //                }
        //                //数据帧检测通过，具体的数据解析
        //                //操作单元的个数，这里是从发送帧中间接得来
        //                int basenum = thisunitcode.Length / 2;

        //                //double _basebyte = Math.Ceiling(Math.Log(basenum  , 2));  理解错误：是一位表示一个操作单元
        //                //发来字节中的执行状态字所占字节数
        //                int basebyte = (int)Math.Ceiling(basenum / 8.0);

        //                //存放执行状态字
        //                byte[] b_basebyte = new byte [basebyte];
        //                Array.Copy(recvframeout, 0, b_basebyte, 0, basebyte);

        //                //返回的数据信息（不包括执行状态字）
        //                byte [] retdata = new byte[recvframeout.Length - basebyte - 2];
        //                Array.Copy(recvframeout , basebyte , retdata , 0 , recvframeout.Length - basebyte - 2);

        //                //放弃从后向前转存，直接从头开始读取
        //                int quotient = basenum / 8;
        //                int remainder = basenum % 8;
        //                for (int i = 0; i < quotient; i++)
        //                {
        //                    if (remainder != 0)
        //                    {
        //                        for (int j = remainder-1; j >= 0; j--)
        //                        {
        //                            int result = b_basebyte[i] & (byte)(int)Math.Pow(2 , j);
        //                            if (0 == result)
        //                            {
        //                                errornum++;
        //                                error.Add();
        //                                //获取操作单元号（非结构体通信下读两个字节），获取错误代码存入
        //                                //获取操作单元和值存入
        //                            }
        //                        }
        //                    }
        //                }
        //                //从右边去第一不方便，反相的对应取
        //                //for (int i = 0; i < basebyte; i++)
        //                //{
        //                //    byte low = sendframeout[sendframeout.Length - 2 * (i + 2)];
        //                //    byte high = sendframeout[sendframeout.Length - 2 * (i + 2) + 1];
        //                //    ushort temp = (ushort)(((ushort)(high << 8)) | low);
        //                //    //读最后一位执行状态字，看是否有错误，有错误则加到错误列表，并错误数加1
        //                //    //没有错误则读取倒数第一个值，与操作单元一起加入数据字典，取值时长度由数据字典截取
        //                //}
        //            }
        //            break;
        //        //接收帧为短帧，发送帧为长帧
        //        case 1:
        //            {
        //                //发送数据的拆分
        //                byte[] sendframein = new byte[PrepareData.BUS_FRAME_MINLEN];
        //                byte[] sendframeout = new byte[originaldata.Length - PrepareData.BUS_FRAME_MINLEN];
        //                Array.Copy(originaldata, 0, sendframein, 0, sendframein.Length);
        //                Array.Copy(originaldata, PrepareData.BUS_FRAME_MINLEN, sendframeout, 0, sendframeout.Length);
        //                //镇内数据的检查
        //                int retcode = FrameInCheck(originaldata, sendframein);
        //                if (retcode != 33)
        //                {
        //                    MessageBox.Show("帧内数据存在错误");
        //                    return null;
        //                }
        //            }
        //            break;
        //        //接收帧为长帧，发送帧为短帧
        //        case -1:
        //            {
        //                //接收数据的拆分
        //                byte[] recvframein = new byte[PrepareData.BUS_FRAME_MINLEN];
        //                byte[] recvframeout = new byte[originaldata.Length - PrepareData.BUS_FRAME_MINLEN];
        //                Array.Copy(originaldata, 0, recvframein, 0, recvframein.Length);
        //                Array.Copy(originaldata, PrepareData.BUS_FRAME_MINLEN, recvframeout, 0, recvframeout.Length);
        //                //帧内数据的检查
        //                int incheckcode = FrameInCheck(recvframein, IMServerUDP.shared.sendbuffer);
        //                int outcheckcode = FrameOutCheck(recvframeout);
        //                if (incheckcode != 33 || outcheckcode != 33)
        //                {
        //                    if (incheckcode != 33)
        //                    {
        //                        MessageBox.Show("帧内数据存在错误!");
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("帧外数据存在错误!");
        //                    }
        //                    return null;
        //                }
        //            }
        //            break;
        //        //返回错误的代码
        //        case 0:
        //            {
        //                MessageBox.Show("返回错误的代码");
        //            }
        //            break;
        //        //返回的代码错误
        //        default:
        //            {
        //                MessageBox.Show("返回的代码错误");
        //            }
        //            break;
        //    }
        //}
#endregion