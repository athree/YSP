using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMserver.CommonFuncs;

namespace IMserver.SubFuncs
{
    public class GetFileInfo
    {
        //获取文件信息数据摘要
        public struct Parameter
        {
            //public byte msgsubtype;
            //按日期范围查询数据文件名及文件大小开始时间
            public DateTime starttime;
            //按日期范围查询数据文件名及文件大小结束时间
            public DateTime endtime;
            //距离timepoint时间点的文件
            public DateTime timepoint;
            //距离timepoint时间点的文件数目
            public ushort filenum;
        }

        /// <summary>
        /// 按日期范围查询数据文件名及文件大小
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetcfgFile(byte destid, GetFileInfo.Parameter para)
        {
            byte [] donedata;
            byte subtype = (byte)MSGEncoding.GetFileInfo.GetcfgFile;
            byte[] data = GetFileInfo.GetData(para , subtype);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.GetFileInfo;
            _frame.msgSubType = (byte)MSGEncoding.GetFileInfo.GetcfgFile;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }

        /// <summary>
        /// 查询离指定日期最近的x个数据文件名及文件大小
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetFileByRange(byte destid, GetFileInfo.Parameter para)
        {
            byte[] donedata;
            byte subtype = (byte)MSGEncoding.GetFileInfo.GetFileByRange;
            byte[] data = GetFileInfo.GetData(para , subtype);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.GetFileInfo;
            _frame.msgSubType = (byte)MSGEncoding.GetFileInfo.GetFileByRange;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }

        /// <summary>
        /// 取得配置文件名称(=最新更新时间)及文件大小
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetXFileByLately(byte destid, GetFileInfo.Parameter para)
        {
            byte[] donedata;
            byte subtype = (byte)MSGEncoding.GetFileInfo.GetXFileByLately;
            byte[] data = GetFileInfo.GetData(para , subtype);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.GetFileInfo;
            _frame.msgSubType = (byte)MSGEncoding.GetFileInfo.GetXFileByLately;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }

        /// <summary>
        /// 组包中获取数据域字节数组
        /// </summary>
        /// <param name="para">发送摘要</param>
        /// <param name="subtype">包子类型，用于区分getdata调用哪些</param>
        /// <returns></returns>
        public static byte[] GetData(GetFileInfo.Parameter para , byte subtype)
        {
            byte[] tempdata;

            List<byte> _tempdata = new List<byte>();
            switch (subtype)
            {
                //按日期范围查询数据文件名及文件大小
                case (byte)MSGEncoding.GetFileInfo.GetFileByRange:
                    {
                        long start = TimerTick.TimeSpanToSecond(para.starttime);
                        long stop = TimerTick.TimeSpanToSecond(para.endtime);
                        WriteUnit.AddTimeToList(_tempdata , start);
                        WriteUnit.AddTimeToList(_tempdata , stop);
                        break;
                    }
                //查询离指定日期最近的x个数据文件名及文件大小
                case (byte)MSGEncoding.GetFileInfo.GetXFileByLately:
                    {
                        long time = TimerTick.TimeSpanToSecond(para.timepoint);
                        WriteUnit.AddTimeToList(_tempdata , time);
                        WriteUnit.AddUshortToList(_tempdata , para.filenum);
                        break;
                    }
                //取得配置文件名称(=更新时间)及文件大小
                case (byte)MSGEncoding.GetFileInfo.GetcfgFile:
                    {
                        //发送内容为空，通过包类型及子类型截取
                        break;
                    }
                default:
                    {
                        //MessageBox.Show("文件信息查询子类消息编号超出范围！");
                        break;
                    }
            }
            tempdata = _tempdata.ToArray<byte>();
            return tempdata;
        }

        /// <summary>
        /// 接收到返回包的处理方法
        /// </summary>
        /// <param name="error"></param>
        /// <param name="b_recvframe"></param>
        /// <returns></returns>
        public static Dictionary<long, ushort> MsgHandle(ref ushort error, byte[] b_recvframe)
        {
            Dictionary<long, ushort> retinfo = new Dictionary<long, ushort>();
            //如果是短帧，一定没有查找到文件，相反如果是长帧一定查询到文件
            switch (HandleData.TypeofFrame(b_recvframe).frametype)
            {
                    //长帧（查询到文件）
                case 1:
                    {
                        //接收数据的拆分
                        byte[] recvframeout = new byte[b_recvframe.Length - PrepareData.BUS_FRAME_MINLEN - 2];
                        Array.Copy(b_recvframe, PrepareData.BUS_FRAME_MINLEN, recvframeout, 0,
                                   b_recvframe.Length - PrepareData.BUS_FRAME_MINLEN - 2);
                        //传入的字节数组只包括执行状态字和返回数据（除CRC外）
                        TransInfo(recvframeout , retinfo);
                    }
                    break;
                    //短帧（未查询到文件）
                    //不存在符合要求的文件、查询出错、配置文件应该会有，设备出厂默认配置、查询错误返回错误代码
                case 0:
                    {
                        byte[] errortemp = new byte[2];
                        PrepareData.Msg_Bus temp = (PrepareData.Msg_Bus)ByteStruct.BytesToStruct(
                                                                           b_recvframe ,
                                                                           typeof(PrepareData.Msg_Bus));
                        //从原始数据中截取两个首字节，错误代码只有两个字节
                        Array.Copy(temp.data , 0 , errortemp , 0 , 2);
                        error = BitConverter.ToUInt16(errortemp , 0);
                        error = (ushort)ByteStruct.BytesToStruct(errortemp , typeof(ushort));
                        retinfo = null;
                    } 
                    break;
                    //错误
                default:
                    {
                        retinfo = null;
                        //MessageBox.Show("获取文件信息的长短帧错误！");
                    }
                    break;
            }
            return retinfo;
        }

        /// <summary>
        /// 忽略错误代码的数据翻译
        /// </summary>
        /// <param name="recvdata"></param>
        /// <param name="retdata"></param>
        public static void TransInfo(byte[] recvdata, Dictionary<long, ushort> retdata)
        {
            byte[] fileinfo = new byte[recvdata.Length - 2];
            Array.Copy(recvdata, 2, fileinfo, 0, fileinfo.Length);
            int filenum = fileinfo.Length / 10;
            for (int i = 0, index = 2; i < filenum; i++)
            {
                byte[] _filename = new byte[8];
                byte[] _filelen = new byte[2];
                Array.Copy(recvdata , index , _filename , 0 , 8);
                index += 8;
                Array.Copy(recvdata , index , _filelen , 0 , 2);
                index += 2;
                long filename = BitConverter.ToInt64(_filename , 0);
                ushort filelen = BitConverter.ToUInt16(_filelen , 0);
                retdata.Add(filename , filelen);
            }
        }
    }
}
