using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMserver.CommonFuncs;

namespace IMserver.SubFuncs
{
    public class GetErrorInfo
    {
        /// <summary>
        /// 向设备查询Error Code的详细解释
        /// 通过该功能，主机可向设备查询每个Error Code的详细英文解释。
        /// ！！！注意：一次请求只能查询一个error code的解释。
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ExplainError(byte destid , ushort error)
        {
            byte [] donedata;
            byte[] data = GetData(error);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.GetErrorInfo;
            _frame.msgSubType = (byte)MSGEncoding.Other.GetErrorInfo;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }

        /// <summary>
        /// 返回  请求错误代码的数据域
        /// </summary>
        /// <returns></returns>
        public static byte[] GetData(ushort errorcode)
        {
            byte[] tempdata;
            List<byte> _tempdata = new List<byte>();
            WriteUnit.AddUshortToList(_tempdata ,errorcode);
            tempdata = _tempdata.ToArray<byte>();
            return tempdata;
        }

        /// <summary>
        /// 获取错误信息的帧处理
        /// </summary>
        /// <param name="b_recvframe">接收到的字节数组（完整的）</param>
        /// <returns></returns>
        public static string MsgHandle(byte[] b_recvframe)
        {
            string explain;
            switch (HandleData.TypeofFrame(b_recvframe).frametype)
            {
                //接收的是长帧
                case 1:
                    {
                        //接收数据的拆分
                        byte[] recvframeout = new byte[b_recvframe.Length - PrepareData.BUS_FRAME_MINLEN - 2];
                        Array.Copy(b_recvframe, PrepareData.BUS_FRAME_MINLEN, recvframeout, 0,
                                   b_recvframe.Length - PrepareData.BUS_FRAME_MINLEN - 2);
                        explain = Encoding.ASCII.GetString(recvframeout);
                    }
                    break;
                //接收帧为短帧
                case 0:
                    {
                        byte[] onlydata = ((PrepareData.Msg_Bus)ByteStruct.BytesToStruct(b_recvframe,
                                                                                         typeof(PrepareData.Msg_Bus))).data;
                        explain = Encoding.ASCII.GetString(onlydata);
                    }
                    break;
                //返回的代码错误
                default:
                    {
                        //如果返回的为null的话，那么请求错误代码失败（内容上的失败，毕竟已完成前序校验）
                        explain = null;
                    }
                    break;
            }
            return explain;
        }
    }
}