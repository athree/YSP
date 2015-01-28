using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMserver.CommonFuncs;
using System.IO;
using System.Collections;

namespace IMserver.SubFuncs
{
    public class ReadFile
    {
        public struct Parameter
        {
            public long filename;
            public ushort start;
            public ushort readbytes;
        }

        //文件缓存hash表
        //public struct FileItem
        //{
        //    public long filename;
        //    public byte[] filedata;
        //}

        public class FileItem
        {
            ushort index;
            public long filename;
            public byte[] filedata;
            public ushort Index { get { return index; } set { index = value; } }
        }

        //每次图文件最大长度，这个考虑随网络状况动态变化（LoopBack）
        //public static ushort MAX = 256;

        //读到文件的缓存列表
        public static Hashtable filepool = new Hashtable();
        /// <summary>
        /// 读取指定数据文件，从x位置开始的n个字节数据
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ReadcfgFile(byte destid , ReadFile.Parameter para)
        {
            byte [] donedata;
            byte[] data = GetData(para);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.ReadFile;
            _frame.msgSubType = (byte)MSGEncoding.ReadFile.ReadcfgFile;
            _frame.dataLen = (ushort)data.Length;
            //合并数据帧，组装外域和数据域
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }

        /// <summary>
        /// 读取指定配置文件，从x位置开始的n个字节数据
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ReadDataFile(byte destid, ReadFile.Parameter para)
        {
            byte[] donedata;
            byte[] data = GetData(para);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.ReadFile;
            _frame.msgSubType = (byte)MSGEncoding.ReadFile.ReadDataFile;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }

        /// <summary>
        /// 正好占用八个字节的数据域，所以不采用list转array
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public static byte[] GetData(ReadFile.Parameter para)
        {
            byte[] tempdata = new byte[12];
            byte[] temp = ByteStruct.StructToBytes(para.filename);
            Array.Copy(temp , tempdata , 8);
            //tempdata[0] = (byte)para.filename;
            //tempdata[1] = (byte)(para.filename >> 8);
            //tempdata[2] = (byte)(para.filename >> 16);
            //tempdata[3] = (byte)(para.filename >> 24);
            //tempdata[4] = (byte)(para.filename >> 32);
            //tempdata[5] = (byte)(para.filename >> 24);
            //tempdata[6] = (byte)(para.filename >> 24);
            //tempdata[7] = (byte)(para.filename >> 24);
            tempdata[8] = (byte)para.start;
            tempdata[9] = (byte)(para.start >> 8);
            tempdata[10] = (byte)para.readbytes;
            tempdata[11] = (byte)(para.readbytes >> 8);
            return tempdata;
        }

        /// <summary>
        /// 函数内完成交互，直到接收完成
        /// 返回数据为空，且error为0x00，说明文件读取完成
        /// 返回错误代码，且数据域为空为产生错误，例如：找不到文件
        /// 返回数据域不为空，且error为0x00，说明正常读取文件
        /// </summary>
        /// <param name="b_recvframe">接收到的字节数组,未拆包的</param>
        /// <param name="error">错误代码</param>
        /// <returns>已接收多少字节</returns>
        public static int MsgHandle(HandleData.TypeRet hdtr,
                                    byte[] b_recvframe, ref ushort error)
        {
            //执行状态字和文件数据
            byte[] recvdata;
            //字节型执行状态字
            byte[] _statecode = new byte[2];
            //不包括执行状态字和CRC校验的文件数据
            byte[] filedata ;
            //转换为ushort类型的执行状态字
            ushort statecode;
            //文件数据的长度（不包括执行状态字和CRC校验）
            int datalen = 0;
            int donerecvinmen = 0;
            byte packetnum = hdtr.packetnum;
            ReadFile.Parameter sender = (ReadFile.Parameter)Define.index_obj[packetnum];

            switch (HandleData.TypeofFrame(b_recvframe).frametype)
            {
                //长帧
                //statecode == 0表示无错误，且为长帧，表示肯定datalen肯定大于0
                //如果datalen == 0则肯定为短帧情况，且表示文件接收完成
                case 1:
                    {   //去除了CRC校验2字节（包括执行状态字和文件数据）                   
                        recvdata = new byte[b_recvframe.Length - PrepareData.BUS_FRAME_MINLEN - 2];
                        Array.Copy(b_recvframe , PrepareData.BUS_FRAME_MINLEN , recvdata, 0, 
                                   recvdata.Length);
                        Array.Copy(recvdata , 0 , _statecode , 0 , 2);
                        statecode = BitConverter.ToUInt16(_statecode , 0);
                        //去除了返回状态字2字节    
                        datalen = recvdata.Length - 2;
                        //不能出现状态执行为0且数据长度为0的情况
                        //因为这是长帧，这种情况只能出现在短帧的情况下
                        //if (0 == statecode && datalen > 0)
                        if(0 == statecode)
                        {
                            //不包括执行状态字和CRC校验码的文件数据
                            filedata = new byte[datalen];
                            Array.Copy(recvdata, 2 , filedata, 0, datalen);
                            //该帧属于以接收文件的部分(若讨论文件接收完毕，到短帧中处理)
                            if (filepool.ContainsKey(packetnum))
                            {
                                FileItem fitemp = (FileItem)filepool[packetnum];
                                byte[] lastfiledata = fitemp.filedata;
                                byte[] datatemp = new byte[datalen + lastfiledata.Length];
                                Array.Copy(lastfiledata , 0 , datatemp , 0 , lastfiledata.Length);
                                Array.Copy(recvdata , 0 , datatemp , lastfiledata.Length , datalen);
                                fitemp.Index += (ushort)datalen;
                                fitemp.filedata = datatemp;
                                filepool[packetnum] = fitemp;
                            }
                            //该帧是要传送文件的第一帧，要重新建立缓冲
                            else 
                            {
                                FileItem fitemp = new FileItem();
                                fitemp.filename = sender.filename;
                                fitemp.filedata = recvdata;
                                fitemp.Index += (ushort)datalen;
                                filepool.Add(packetnum , fitemp);
                            }
                        }
                        donerecvinmen = datalen;
                        error = statecode;
                    }
                    break;
                //短帧(分类处理，错误和不错误   文件传输结束)
                case 0:
                    {
                        recvdata = ((PrepareData.Msg_Bus)ByteStruct.BytesToStruct(b_recvframe ,
                                                                                  typeof(PrepareData.Msg_Bus))).data;
                        int count = ((PrepareData.Msg_Bus)ByteStruct.BytesToStruct(b_recvframe,
                                                                               typeof(PrepareData.Msg_Bus))).dataLen;
                        datalen = count - 2;
                        Array.Copy(recvdata , 0 , _statecode , 0 , 2);
                        statecode = BitConverter.ToUInt16(_statecode , 0);
                        //传回的文件不存在错误
                        if (0 == statecode)
                        {
                            if (datalen > 0)
                            {
                                filedata = new byte[datalen];
                                Array.Copy(recvdata, 2, filedata, 0, datalen);
                                //byte packetnum = hdtr.packetnum;
                                //ReadFile.Parameter sender = (ReadFile.Parameter)Define.index_obj[packetnum];
                                //该帧属于以接收文件的部分(若讨论文件接收完毕，到短帧中处理)
                                if (filepool.ContainsKey(packetnum))
                                {
                                    FileItem fitemp = (FileItem)filepool[packetnum];
                                    byte[] lastfiledata = fitemp.filedata;
                                    byte[] datatemp = new byte[datalen + lastfiledata.Length];
                                    Array.Copy(lastfiledata, 0, datatemp, 0, lastfiledata.Length);
                                    Array.Copy(recvdata, 0, datatemp, lastfiledata.Length, datalen);
                                    fitemp.filedata = datatemp;
                                    fitemp.Index += (ushort)datalen;
                                    filepool[packetnum] = fitemp;
                                }
                                //该帧是要传送文件的第一帧，要重新建立缓冲
                                else
                                {
                                    FileItem fitemp = new FileItem();
                                    fitemp.filename = sender.filename;
                                    fitemp.filedata = recvdata;
                                    fitemp.Index += (ushort)datalen;
                                    filepool.Add(packetnum, fitemp);
                                }
                            }
                            //errorcode和datalen都为0，文件传输结束帧
                            else
                            {
                                //不论存在还是不存在都使用create模式，如果文件存在那么就直接覆盖
                                string filename = sender.filename.ToString();
                                string path = "d:\\ "+filename+".txt";
                                //如果指定位置已经存在该文件
                                if (File.Exists(path))
                                {
                                    //MessageBox.Show("接收到的文件已存在，正在准备覆盖源文件！");
                                }
                                FileItem fitemp = (FileItem)filepool[hdtr.packetnum];
                                byte[] lastfiledata = fitemp.filedata;
                                FileStream tempfile = new FileStream(path, FileMode.Create, FileAccess.Write);
                                tempfile.Write(lastfiledata, 0, lastfiledata.Length);
                                tempfile.Flush();
                                tempfile.Close();
                            }
                        }
                        donerecvinmen = datalen;
                        //传回的文件存在错误
                        error = statecode;
                    }
                    break;
                    //错误情况
                default:
                    {
                        //MessageBox.Show("读文件（msghandle）：长短帧判断处理失败！返回值均为操作！");
                    }
                    break;
            }
            return donerecvinmen;
        }
    }
}
