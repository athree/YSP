using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using IMserver.SubFuncs;
using IMserver.CommonFuncs;
using System.Net;
using System.Net.Sockets;

namespace IMserver
{
    //本类的所有大于2个字节的数据类型均按照大端方式存储,即高字节存低地址，低字节存高地址
    public class PrepareData
    {
        //由于packetzation方法没有修改为非静态
        private static byte packetindex = 0;                    //数据包的编号

        public static int CRCStart = 0;                         //CRC校验开始
        public static int CRCStop = BUS_FRAME_MINLEN-2;         //CRC校验结束(检测的长度)
        public const string _FRAME_HEAD = "XYBUS";              //帧头
        public const int BUS_FRAME_IN_DATALEN = 8;              //帧内数据域长度
        public static byte[] BUS_FRAME_FLAG = new byte[]        //数据帧头字节数组
                                             { 0x58, 0x59, 0x42, 0x55, 0x53, 0x00, 0x00, 0x00 };  
        public static string FRAME_HEAD = Encoding.ASCII.GetString(BUS_FRAME_FLAG);        //八字节的数据帧头
        public static int BUS_FRAME_MINLEN = Marshal.SizeOf(typeof(Msg_Bus));              //最小帧长=26

        //Msg_Bus结构体的长度在数据域小于8时不是固定的，即少于8的不需要补充
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct Msg_Bus
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = BUS_FRAME_IN_DATALEN)]
            public byte[] flag;                     //消息头
            public byte msgID;                      //消息标记，用以对照响应数据
            public byte srcID;                      //源设备号
            public byte destID;                     //目标设备号
            public byte msgVer;                     //消息版本号，0x01
            public byte msgDir;                     //消息方向
            public byte msgType;                    //消息类型
            public byte msgSubType;                 //消息子类型
            public ushort dataLen;                  //数据长度   默认为数据域长度
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = BUS_FRAME_IN_DATALEN)]
            public byte[] data;                     //数据域
            public ushort crc16;                    //CRC16校验码
        }

        //原packet返回字节数组（含编码）与编码
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct PacketRet
        {
            public bool packetdone;
            public ushort errorcode;
            public byte packetnum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public byte[] packetdata;
        }

        /// <summary>
        /// 发出消息和接受消息的留样对比结构体
        /// 传参或者留样
        /// 这个结构体在传给具体子函数时没有整体传入，只是传入最直接有效果的destid，没有根本影响
        /// 由于麻烦，前期暂用
        /// </summary>
        public struct Compare
        {
            public byte srcID;                      //源设备号
            public byte destID;                     //目标设备号
            public byte msgVer;                     //消息版本号，0x01
            public byte msgDir;                     //消息方向
            public byte msgType;                    //消息类型
            public byte msgSubType;                 //消息子类型
        }
        

        /// <summary>
        /// 如果是多包数据，那么分包后放到list中
        /// 1、当需要组包时，传入参数，分析、使用switch区分，组装不同的帧
        /// 2、类型和子类型的方法分别用于区分和组装数据域外数据
        /// 3、针对不同的功能组织不同的数据域，最后合并
        /// 4、ref donewrite参数为写文件准备，返回本次组包本地读取的字节数
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="msgtype"></param>
        /// <param name="msgsubtype"></param>
        /// <returns>错误代码另外给出</returns>
        public static PacketRet Packetization(Compare example, object data)
        {
            byte[] frame = null;
            PacketRet tempret = new PacketRet();
            //packetization的错误代码定义另外给出
            tempret.errorcode = 0;
            switch (example.msgType)
            {
                //读操作单元
                case (byte)MSGEncoding.MsgType.ReadUnit:
                    {
                        switch (example.msgSubType)
                        {
                            //读数据
                            case (byte)MSGEncoding.ReadUint.ReadData:
                                {
                                    frame = ReadUnit.ReadData(example.destID, (ushort[])data);
                                    break;
                                }
                            //读取配置信息
                            case (byte)MSGEncoding.ReadUint.Readcfg:
                                {
                                    frame = ReadUnit.Readcfg(example.destID, (ushort[])data);
                                    break;
                                }
                            //查询设备状态
                            case (byte)MSGEncoding.ReadUint.GetDevStatus:
                                {
                                    frame = ReadUnit.GetDevStatus(example.destID, (ushort[])data);
                                    break;
                                }
                            default:
                                {
                                    //MessageBox.Show("preparedata-packetzation:错误子代码！");
                                    tempret.errorcode = 1;
                                }
                                break;
                        }
                        break;
                    }
                //写操作单元
                case (byte)MSGEncoding.MsgType.WriteUnit:
                    {
                        //为适应前台，测试其他成功后添加转换
                        Dictionary<ushort, object> data_temp = WriteUnit.TransDict(((Dictionary<string , object>)data));
                        switch (example.msgSubType)
                        {
                            //控制设备（应该是状态/控制量）
                            case (byte)MSGEncoding.WriteUint.ControlDev:
                                {
                                    frame = WriteUnit.ControlDev(example.destID, data_temp);
                                    break;
                                }
                            //设置配置信息（配置量）
                            case (byte)MSGEncoding.WriteUint.Setcfg:
                                {
                                    frame = WriteUnit.Setcfg(example.destID, data_temp);
                                    break;
                                }
                            //写数据（数据量）
                            case (byte)MSGEncoding.WriteUint.WriteData:
                                {
                                    frame = WriteUnit.WriteData(example.destID, data_temp);
                                    break;
                                }
                            default:
                                {
                                    //MessageBox.Show("preparedata-packetzation:错误子代码！");
                                    tempret.errorcode = 1;
                                }
                                break;
                        }
                        break;
                    }
                //读文件
                case (byte)MSGEncoding.MsgType.ReadFile:
                    {
                        switch (example.msgSubType)
                        {
                            //读取指定配置文件，从x位置开始的n个字节数据
                            case (byte)MSGEncoding.ReadFile.ReadcfgFile:
                                {
                                    frame = ReadFile.ReadcfgFile(example.destID, (ReadFile.Parameter)data);
                                    break;
                                }
                            //读取指定数据文件，从x位置开始的n个字节数据
                            case (byte)MSGEncoding.ReadFile.ReadDataFile:
                                {
                                    frame = ReadFile.ReadDataFile(example.destID, (ReadFile.Parameter)data);
                                    break;
                                }
                            default:
                                {
                                    //MessageBox.Show("preparedata-packetzation:错误子代码！");
                                    tempret.errorcode = 1;
                                }
                                break;
                        }
                        break;
                    }
                //写文件到设备
                //注意：传入的para中的filehandle已经初始化
                case (byte)MSGEncoding.MsgType.WriteFile:
                    {
                        switch (example.msgSubType)
                        {
                            //上传配置文件到设备，并作为最新配置
                            case (byte)MSGEncoding.WriteFile.UpcfgToDev:
                                {
                                    frame = WriteFile.UpcfgToDev(example.destID, (WriteFile.Parameter)data);
                                    break;
                                }
                            //上传数据文件到设备
                            case (byte)MSGEncoding.WriteFile.UpDataToDev:
                                {
                                    frame = WriteFile.UpDataToDev(example.destID, (WriteFile.Parameter)data);
                                    break;
                                }
                            //上传升级文件
                            case (byte)MSGEncoding.WriteFile.UpGradeFile:
                                {
                                    frame = WriteFile.UpGradeFile(example.destID, (WriteFile.Parameter)data);
                                    break;
                                }
                            default:
                                {
                                    //MessageBox.Show("preparedata-packetzation:错误子代码！");
                                    tempret.errorcode = 1;
                                }
                                break;
                        }
                        break;
                    }
                //通信测试
                case (byte)MSGEncoding.MsgType.LoopBack:
                    {
                        //LoopBack通讯测试用
                        frame = LoopBack.LoopTest(example.destID , (ushort)data);
                        break;
                    }
                //其他（向设备查询Error Code的详细解释）
                case (byte)MSGEncoding.MsgType.Other:
                    {
                        //向设备查询Error Code的详细解释
                        frame = GetErrorInfo.ExplainError(example.destID, (ushort)data);
                        break;
                    }
                //文件信息查询
                case (byte)MSGEncoding.MsgType.GetFileInfo:
                    {
                        switch (example.msgSubType)
                        {
                            //取得配置文件名称(=最新更新时间)及文件大小
                            case (byte)MSGEncoding.GetFileInfo.GetcfgFile:
                                {
                                    frame = GetFileInfo.GetcfgFile(example.destID, (GetFileInfo.Parameter)data);
                                    break;
                                }
                            //按日期范围查询数据文件名及文件大小
                            case (byte)MSGEncoding.GetFileInfo.GetFileByRange:
                                {
                                    frame = GetFileInfo.GetFileByRange(example.destID, (GetFileInfo.Parameter)data);
                                    break;
                                }
                            //查询离指定日期最近的x个数据文件名及文件大小
                            case (byte)MSGEncoding.GetFileInfo.GetXFileByLately:
                                {
                                    frame = GetFileInfo.GetXFileByLately(example.destID, (GetFileInfo.Parameter)data);
                                    break;
                                }
                            default:
                                {
                                    //MessageBox.Show("preparedata-packetzation:错误子代码！");
                                    tempret.errorcode = 1;
                                }
                                break;
                        }
                        break;
                    }
                //读缓冲区
                case (byte)MSGEncoding.MsgType.ReadBuffer:
                    {
                        switch (example.msgSubType)
                        {
                            //读CO2气体的采样缓存
                            case (byte)MSGEncoding.ReadBuffer.ReadCO2Cache:
                                {
                                    frame = ReadBuffer.ReadCO2Cache(example.destID, (ReadBuffer.Parameter)data);
                                    break;
                                }
                            //读六种气体的采样缓存
                            case (byte)MSGEncoding.ReadBuffer.ReadGasCache:
                                {
                                    frame = ReadBuffer.ReadGasCache(example.destID, (ReadBuffer.Parameter)data);
                                    break;
                                }
                            //读H2O采样数据缓存
                            case (byte)MSGEncoding.ReadBuffer.ReadH2OCache:
                                {
                                    frame = ReadBuffer.ReadH2OCache(example.destID, (ReadBuffer.Parameter)data);
                                    break;
                                }
                            default:
                                {
                                    //MessageBox.Show("preparedata-packetzation:错误子代码！");
                                    tempret.errorcode = 1;
                                }
                                break;
                        }
                        break;
                    }
                default:
                    {
                        //MessageBox.Show("preparedata-packetzation:数据帧外层类型存在错误！");
                        tempret.errorcode = 2;
                    }
                    break;
                        
            }
            try
            {
                //为统一发送和接受帧的对照，添加的标志，这里稳定后挪进所有帧组装方法中
                byte[] changeframe = new byte[frame.Length + 1];
                Array.Copy(frame, 0, changeframe, 0, 8);
                changeframe[8] = packetindex;
                Array.Copy(frame, 8, changeframe, 9, frame.Length - 8);
                tempret.packetnum = packetindex;
                tempret.packetdata = frame;
                //通过自动溢出归零
                packetindex++;
            }
            catch (Exception ep)
            {
                //组好的原始帧中插入帧编码存在错误！
                //MessageBox.Show("preparedata-packetization:"+ep.ToString());
                tempret.errorcode = 3;
            }
            return tempret;
        }

        /// <summary>
        /// 重载方法，为了编号的自定义
        /// </summary>
        /// <param name="example"></param>
        /// <param name="data"></param>
        /// <param name="msgid">如果msgid为-1，那么自动填充包中id，如果为正数，那么手动填充</param>
        /// <returns></returns>
        public static PacketRet Packetization(Compare example, object data, int msgid )
        {
            byte[] frame = null;
            PacketRet tempret = new PacketRet();
            //packetization的错误代码定义另外给出
            tempret.errorcode = 0;
            switch (example.msgType)
            {
                //读操作单元
                case (byte)MSGEncoding.MsgType.ReadUnit:
                    {
                        switch (example.msgSubType)
                        {
                            //读数据
                            case (byte)MSGEncoding.ReadUint.ReadData:
                                {
                                    frame = ReadUnit.ReadData(example.destID, (ushort[])data);
                                    break;
                                }
                            //读取配置信息
                            case (byte)MSGEncoding.ReadUint.Readcfg:
                                {
                                    frame = ReadUnit.Readcfg(example.destID, (ushort[])data);
                                    break;
                                }
                            //查询设备状态
                            case (byte)MSGEncoding.ReadUint.GetDevStatus:
                                {
                                    frame = ReadUnit.GetDevStatus(example.destID, (ushort[])data);
                                    break;
                                }
                            default:
                                {
                                    //MessageBox.Show("preparedata-packetzation:错误子代码！");
                                    tempret.errorcode = 1;
                                }
                                break;
                        }
                        break;
                    }
                //写操作单元
                case (byte)MSGEncoding.MsgType.WriteUnit:
                    {
                        //为适应前台，测试其他成功后添加转换
                        Dictionary<ushort, object> data_temp = WriteUnit.TransDict(((Dictionary<string, object>)data));
                        switch (example.msgSubType)
                        {
                            //控制设备（应该是状态/控制量）
                            case (byte)MSGEncoding.WriteUint.ControlDev:
                                {
                                    frame = WriteUnit.ControlDev(example.destID, data_temp);
                                    break;
                                }
                            //设置配置信息（配置量）
                            case (byte)MSGEncoding.WriteUint.Setcfg:
                                {
                                    frame = WriteUnit.Setcfg(example.destID, data_temp);
                                    break;
                                }
                            //写数据（数据量）
                            case (byte)MSGEncoding.WriteUint.WriteData:
                                {
                                    frame = WriteUnit.WriteData(example.destID, data_temp);
                                    break;
                                }
                            default:
                                {
                                    //MessageBox.Show("preparedata-packetzation:错误子代码！");
                                    tempret.errorcode = 1;
                                }
                                break;
                        }
                        break;
                    }
                //读文件
                case (byte)MSGEncoding.MsgType.ReadFile:
                    {
                        switch (example.msgSubType)
                        {
                            //读取指定配置文件，从x位置开始的n个字节数据
                            case (byte)MSGEncoding.ReadFile.ReadcfgFile:
                                {
                                    frame = ReadFile.ReadcfgFile(example.destID, (ReadFile.Parameter)data);
                                    break;
                                }
                            //读取指定数据文件，从x位置开始的n个字节数据
                            case (byte)MSGEncoding.ReadFile.ReadDataFile:
                                {
                                    frame = ReadFile.ReadDataFile(example.destID, (ReadFile.Parameter)data);
                                    break;
                                }
                            default:
                                {
                                    //MessageBox.Show("preparedata-packetzation:错误子代码！");
                                    tempret.errorcode = 1;
                                }
                                break;
                        }
                        break;
                    }
                //写文件到设备
                //注意：传入的para中的filehandle已经初始化
                case (byte)MSGEncoding.MsgType.WriteFile:
                    {
                        switch (example.msgSubType)
                        {
                            //上传配置文件到设备，并作为最新配置
                            case (byte)MSGEncoding.WriteFile.UpcfgToDev:
                                {
                                    frame = WriteFile.UpcfgToDev(example.destID, (WriteFile.Parameter)data);
                                    break;
                                }
                            //上传数据文件到设备
                            case (byte)MSGEncoding.WriteFile.UpDataToDev:
                                {
                                    frame = WriteFile.UpDataToDev(example.destID, (WriteFile.Parameter)data);
                                    break;
                                }
                            //上传升级文件
                            case (byte)MSGEncoding.WriteFile.UpGradeFile:
                                {
                                    frame = WriteFile.UpGradeFile(example.destID, (WriteFile.Parameter)data);
                                    break;
                                }
                            default:
                                {
                                    //MessageBox.Show("preparedata-packetzation:错误子代码！");
                                    tempret.errorcode = 1;
                                }
                                break;
                        }
                        break;
                    }
                //通信测试
                case (byte)MSGEncoding.MsgType.LoopBack:
                    {
                        //LoopBack通讯测试用
                        frame = LoopBack.LoopTest(example.destID, (ushort)data);
                        break;
                    }
                //其他（向设备查询Error Code的详细解释）
                //虽然将error code拉到了other中，但是另一个成员只做主动上送不做组包下发
                case (byte)MSGEncoding.MsgType.Other:
                    {
                        //向设备查询Error Code的详细解释
                        frame = GetErrorInfo.ExplainError(example.destID, (ushort)data);
                        break;
                    }
                //文件信息查询
                case (byte)MSGEncoding.MsgType.GetFileInfo:
                    {
                        switch (example.msgSubType)
                        {
                            //取得配置文件名称(=最新更新时间)及文件大小
                            case (byte)MSGEncoding.GetFileInfo.GetcfgFile:
                                {
                                    frame = GetFileInfo.GetcfgFile(example.destID, (GetFileInfo.Parameter)data);
                                    break;
                                }
                            //按日期范围查询数据文件名及文件大小
                            case (byte)MSGEncoding.GetFileInfo.GetFileByRange:
                                {
                                    frame = GetFileInfo.GetFileByRange(example.destID, (GetFileInfo.Parameter)data);
                                    break;
                                }
                            //查询离指定日期最近的x个数据文件名及文件大小
                            case (byte)MSGEncoding.GetFileInfo.GetXFileByLately:
                                {
                                    frame = GetFileInfo.GetXFileByLately(example.destID, (GetFileInfo.Parameter)data);
                                    break;
                                }
                            default:
                                {
                                    //MessageBox.Show("preparedata-packetzation:错误子代码！");
                                    tempret.errorcode = 1;
                                }
                                break;
                        }
                        break;
                    }
                //读缓冲区
                case (byte)MSGEncoding.MsgType.ReadBuffer:
                    {
                        switch (example.msgSubType)
                        {
                            //读CO2气体的采样缓存
                            case (byte)MSGEncoding.ReadBuffer.ReadCO2Cache:
                                {
                                    frame = ReadBuffer.ReadCO2Cache(example.destID, (ReadBuffer.Parameter)data);
                                    break;
                                }
                            //读六种气体的采样缓存
                            case (byte)MSGEncoding.ReadBuffer.ReadGasCache:
                                {
                                    frame = ReadBuffer.ReadGasCache(example.destID, (ReadBuffer.Parameter)data);
                                    break;
                                }
                            //读H2O采样数据缓存
                            case (byte)MSGEncoding.ReadBuffer.ReadH2OCache:
                                {
                                    frame = ReadBuffer.ReadH2OCache(example.destID, (ReadBuffer.Parameter)data);
                                    break;
                                }
                            default:
                                {
                                    //MessageBox.Show("preparedata-packetzation:错误子代码！");
                                    tempret.errorcode = 1;
                                }
                                break;
                        }
                        break;
                    }
                default:
                    {
                        //MessageBox.Show("preparedata-packetzation:数据帧外层类型存在错误！");
                        tempret.errorcode = 2;
                    }
                    break;

            }
            try
            {
                byte[] changeframe = new byte[frame.Length + 1];
                Array.Copy(frame, 0, changeframe, 0, 8);
                Array.Copy(frame, 8, changeframe, 9, frame.Length - 8);
                tempret.packetdata = frame;

                if (-1 == msgid)
                {
                    //为统一发送和接受帧的对照，添加的标志，这里稳定后挪进所有帧组装方法中
                    changeframe[8] = packetindex;
                    tempret.packetnum = packetindex;
                    //通过自动溢出归零
                    packetindex++;
                }
                else
                {
                    //如果为自定义msgid
                    changeframe[8] = (byte)msgid;
                    tempret.packetnum = (byte)msgid;
                }
            }
            catch (Exception ep)
            {
                //组好的原始帧中插入帧编码存在错误！
                //MessageBox.Show("preparedata-packetization:" + ep.ToString());
                tempret.errorcode = 3;
            }
            return tempret;
        }

        /// <summary>
        /// 组织完整的数据帧
        /// 注意：这里组织默认的都是小端方式
        /// </summary>
        /// <param name="data">数据部分</param>
        /// <returns>完整的数据帧</returns>
        public static byte[] MergeMsg(ref PrepareData.Msg_Bus _frame , byte []data)
        {
            byte[] sender;
            ushort CRC ;
            _frame.data = new byte[PrepareData.BUS_FRAME_IN_DATALEN];
            //数据域小于8，在帧内组织
            if (data.Length <= 8)
            {
                //帧内拷贝
                Array.Copy(data, _frame.data, data.Length);
                _frame.crc16 = 0x0000;
                sender = ByteStruct.StructToBytes(_frame);
                CRC = CRC16.CalculateCrc16(sender, 0, sender.Length - 2);
                sender[sender.Length - 1] = (byte)(CRC >> 8);
                sender[sender.Length - 2] = (byte)CRC;
            }
            //数据帧过长需要帧外组织
            else
            {
                _frame.crc16 = 0x0000;
                //帧外的CRC校验
                CRC = CRC16.CalculateCrc16(data);
                byte []temp = ByteStruct.StructToBytes(_frame);
                //帧内数据的CRC校验码
                ushort crctemp = CRC16.CalculateCrc16(temp , 0 , temp.Length - 2);
                temp[temp.Length - 1] = (byte)(crctemp >> 8);
                temp[temp.Length - 2] = (byte)crctemp;
                //整体数据组织
                sender = new byte[PrepareData.BUS_FRAME_MINLEN + data.Length + 2];
                //帧内数据
                temp.CopyTo(sender , 0);
                //帧外数据
                Array.Copy(data , 0 , sender , PrepareData.BUS_FRAME_MINLEN , data.Length);
                //帧外CRC
                sender[sender.Length - 1] = (byte)(CRC >> 8);
                sender[sender.Length - 2] = (byte)CRC;
            }
            return sender;
        }

        /// <summary>
        /// 将发送的数据帧添加到发送缓冲队列
        /// </summary>
        /// <param name="example"></param>
        /// <param name="data"></param>
        /// <param name="donewrite">仅针对于文件的写，且是分多次写下的情况</param>
        public static byte AddRequire(Compare example, object data)
        {
            //根据destID获取目标主机的IP和port，这里如果发送多个数据需多次跟数据库通信
            //麻烦，所以可以一次性将ip和port读到数据字典
            //指定终端设备号，通过查询心跳维护的DEVID-IP-PORT字典获取endpoint
            IPEndPoint endpointtemp = Define.id_ip_port[0x01];
            PacketRet pr = Packetization(example, data);
            //加入发送队列
            Define.SendQueueItem sqi = new Define.SendQueueItem();
            sqi.destpoint = endpointtemp;
            sqi.senddata = pr.packetdata;
            Define.send_queue.Enqueue(sqi);
            //如果是一帧中的第一包
            //计时队列和摘要队列是一次添加，内容不变化
            if (!Define.index_obj.ContainsKey(pr.packetnum))
            {
                //加入计时队列 TimerTick.count初始为30
                Define.existime.Add(pr.packetnum, TimerTick.count);
                //加入发送缓存队列
                Define.index_obj.Add(pr.packetnum, data);
                //加入到摘要队列
                Define.index_com.Add(pr.packetnum, example);
            }
            //针对一帧多包，一帧中的后续包
            else
            {
                //更新发送缓冲队列
                Define.index_obj[pr.packetnum] = data;
            }
            return pr.packetnum;
        }

        /// <summary>
        /// 方法重载
        /// </summary>
        /// <param name="example"></param>
        /// <param name="data"></param>
        /// <param name="msgid">用以实现自定义模式的包编号</param>
        /// <returns></returns>
        public static byte AddRequire(Compare example, object data, int msgid)
        {
            //根据destID获取目标主机的IP和port，这里如果发送多个数据需多次跟数据库通信
            //麻烦，所以可以一次性将ip和port读到数据字典
            IPEndPoint endpointtemp = Define.id_ip_port[example.destID];
            PacketRet pr = Packetization(example, data, msgid);
            //加入发送队列
            Define.SendQueueItem sqi = new Define.SendQueueItem();
            sqi.destpoint = endpointtemp;
            sqi.senddata = pr.packetdata;
            Define.send_queue.Enqueue(sqi);
            //如果是一帧中的第一包
            //计时队列和摘要队列是一次添加，内容不变化
            //这里存在安全隐患：关联于队列中暂留项目的冲突可能性
            if (!Define.index_obj.ContainsKey(pr.packetnum))
            {
                //加入计时队列 TimerTick.count初始为30
                Define.existime.Add(pr.packetnum, TimerTick.count);
                //加入发送缓存队列
                Define.index_obj.Add(pr.packetnum, data);
                //加入到摘要队列
                Define.index_com.Add(pr.packetnum, example);
            }
            //针对一帧多包，一帧中的后续包
            else
            {
                //更新发送缓冲队列
                Define.index_obj[pr.packetnum] = data;
            }
            return pr.packetnum;
        }

    }
}

#region  吴工的loopback（）测试方法
/// <summary>
/// 1.通信测试，正常返回true
/// 2.分包处理，可看成将上面数据的分治，提供接口
/// </summary>
/// <param name="DeviceID">目的设备ID</param>
/// <returns>？？？</returns>
//public static bool _Loopback(byte DeviceID)
//{
//    Msg_Bus msg = new Msg_Bus();
//    msg.flag =BUS_FRAME_FLAG;
//    msg.srcID = 0x00;
//    msg.destID = DeviceID;
//    msg.msgVer = 0x01;
//    msg.msgDir = 0x00;
//    msg.msgType = (byte)MSGEncoding.MsgType.LoopBack;
//    //子类型为任意测试字
//    //msg.msgSubType = (byte)MSGEncoding.Read.GetTime;
//    msg.msgSubType = 0x00;
//    //msg.dataLen = CommonFuncs.ConvertBS(MsgDefine.BUS_FRAME_IN_DATALEN);
//    msg.dataLen = BUS_FRAME_IN_DATALEN;
//    msg.data = new Byte[BUS_FRAME_IN_DATALEN];
//    msg.crc16 = 0x0000;

//    //生成随机数
//    Random r = new Random();
//    for (int i = 0; i < BUS_FRAME_IN_DATALEN; i++)
//    {
//        msg.data[i] = new byte();
//        msg.data[i] = (byte)r.Next();
//    }

//    byte[] sender = ByteStruct.StructToBytes(msg);

//    //计算CRC
//    ushort CRC = CRC16.CalculateCrc16(sender, 0, sender.Length - 2);
//    sender[sender.Length - 1] = (byte)(CRC >> 8);
//    sender[sender.Length - 2] = (byte)CRC;
//    return false;
//try
//{
//    DataPackage dp = new DataPackage(sender);
//    CommEntity.CommEntity comm = new CommEntity.CommEntity();
//    comm.LoopBack(dp);

//    return true;
//}
//catch (System.Exception ex)
//{
//    LogException(ex);
//    return false;
//}
//}
#endregion

#region     组装心跳帧
            //byte []heartbeat =new byte[BUS_FRAME_MINLEN];
            //string hello = "hello";
            ////Msg_Bus heartstruct;
            ////heartstruct.flag = BUS_FRAME_FLAG;
            ////heartstruct.srcID = 0x00;
            ////heartstruct.destID = 0x11;
            ////heartstruct.MsgVer = 0x01;
            ////heartstruct.msgDir = 0x01;
            ////heartstruct.msgType = 0x33;
            ////heartstruct.dataLen = (ushort)hello.Length;
            ////heartstruct.data = Encoding.ASCII.GetBytes(hello);
            //Array.Copy(BUS_FRAME_FLAG , 0 , heartbeat , 0 , BUS_FRAME_FLAG.Length);
            //BUS_FRAME_FLAG.CopyTo(heartbeat , 0);
            //heartbeat[8] = 0x00;
            //heartbeat[9] = 0x11;
            //heartbeat[10] = 0x01;
            //heartbeat[11] = 0x01;
            //heartbeat[12] = 0x33;
            //heartbeat[13] = (byte)(hello.Length & 0xff);           //默认大型数据的低字节存储到数组的低位
            //heartbeat[14] = (byte)((hello.Length >>8) & 0xff);     //默认大型数据的高字节存储到数组的低位
            //Encoding.ASCII.GetBytes(hello).CopyTo(heartbeat , 15);
            //ushort crc = CRC16.CalculateCrc16(heartbeat , CRCStart , CRCStop);
            //heartbeat[24] = (byte)(crc & 0xff);
            //heartbeat[25] =(byte)((crc >> 8) & 0xff);
            //frame.Add(heartbeat);
            #endregion