using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMserver.SubFuncs
{
    //用于通讯测试,不同通讯链路上能稳定通讯的数据包大小，特别在使用GPRS通讯时。
    public class LoopBack
    {
        /// <summary>
        /// LoopBack测试一般只会用在文件传输上，指定的文件的内容通过CRC校验可以确保是否一样
        /// 无须按字节对比，或者equal  还是字节对比快
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="testlen"></param>
        /// <returns></returns>
        public static byte[] LoopTest(byte destid, ushort testlen)
        {
            byte[] donedata;
            byte[] data = GetData(testlen);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.ReadBuffer;
            _frame.msgSubType = (byte)MSGEncoding.ReadBuffer.ReadCO2Cache;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }

        /// <summary>
        /// 获取数据域信息
        /// </summary>
        /// <param name="testlen"></param>
        /// <returns></returns>
        public static byte[] GetData(ushort testlen)
        {
            byte[] tempdata;
            List<byte> _tempdata = new List<byte>();
            //循环为指定长度的数据域添加对256（一个字节表示）的余数
            for (int i = 0; i < testlen; i++)
            {
                _tempdata.Add((byte)(i%256));
            }
            tempdata = _tempdata.ToArray<byte>();
            return tempdata;
        }

        public static bool MsgHandle(byte[] b_recvframe , byte[] rawdata)
        {
            if (null == b_recvframe || null == rawdata)
            {
                return false;
            }
            if (b_recvframe.Length != rawdata.Length)
            {
                return false;
            }
            for (int i = 0; i < rawdata.Length; i++)
            {
                if (b_recvframe[i] != rawdata.Length)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
