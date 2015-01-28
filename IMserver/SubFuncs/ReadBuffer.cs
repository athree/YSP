using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMserver.CommonFuncs;
using System.Runtime.InteropServices;

namespace IMserver.SubFuncs
{
    //缓存对应的是谱图显示，web端请求显示谱图时开始轮训读，请求关闭时停止轮训
    //开始和关闭的过程通过标志来决定
    public class ReadBuffer
    {
        //h20采样点的结构体
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct H2OPoint
        {
            public float h2o_AW;
            public float h2o_T;
            public float h2o_PPM;
        }

        /// <summary>
        /// 读操作单元组包用参数
        /// </summary>
        public struct Parameter
        {
            public ushort startpoint;
            public ushort stoppoint;
        }

        /// <summary>
        /// 读六种气体的采样数据缓存
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ReadCO2Cache(byte destid , ReadBuffer.Parameter para)
        {
            byte[] donedata;
            byte[] data = GetData(para);
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
        /// 读CO2气体的采样缓存
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ReadGasCache(byte destid , ReadBuffer.Parameter para)
        {
            byte[] donedata;
            byte[] data = GetData(para);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.ReadBuffer;
            _frame.msgSubType = (byte)MSGEncoding.ReadBuffer.ReadGasCache;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }

        /// <summary>
        /// 读H2O采样缓存
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ReadH2OCache(byte destid , ReadBuffer.Parameter para)
        {
            byte[] donedata;
            byte[] data = GetData(para);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.ReadBuffer;
            _frame.msgSubType = (byte)MSGEncoding.ReadBuffer.ReadH2OCache;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }

        //读取数据域
        public static byte[] GetData(ReadBuffer.Parameter para)
        {
            byte [] tempdata;
            List<byte> _tempdata = new List<byte>();
            _tempdata.Add((byte)para.startpoint);
            _tempdata.Add((byte)(para.startpoint >> 8));
            _tempdata.Add((byte)para.stoppoint);
            _tempdata.Add((byte)(para.stoppoint >> 8));
            tempdata = _tempdata.ToArray<byte>();
            return tempdata;
        }

        /// <summary>
        /// 接受数据的处理，数据库中存储的为字节数组
        /// h2o的为float数组
        /// co2的为ushort数组
        /// 六组分的为short数组
        /// </summary>
        /// <param name="b_recvframe"></param>
        /// <param name="msgsubtype">发送数据帧的子类型</param>
        /// <returns></returns>
        public static object MsgHandle(byte[] b_recvframe, byte msgsubtype)
        {
            byte[] sampledata;
            int datalen;
            int point;
            List<H2OPoint> h2odata = new List<H2OPoint>();
            List<ushort> co2data = new List<ushort>();
            List<short> sixgasdata = new List<short>();
            switch(HandleData.TypeofFrame(b_recvframe).frametype)
            {
                //长帧
                case 1:
                    {
                        //去除了CRC校验2字节                   
                        datalen = b_recvframe.Length - PrepareData.BUS_FRAME_MINLEN - 2;
                        sampledata = new byte[datalen];
                        Array.Copy(b_recvframe, PrepareData.BUS_FRAME_MINLEN, sampledata, 0, datalen);
                        switch (msgsubtype)
                        {
                            //H2O
                            case (byte)MSGEncoding.ReadBuffer.ReadH2OCache:
                                {
                                    //AW、T、ppm一组
                                    point = datalen / 12;
                                    for (int i = 0; i < point; i++)
                                    {
                                        //更新h20采样点为结构体（实际h2o采样点只有一个）
                                        H2OPoint temp = new H2OPoint();
                                        byte[] _h2odata = new byte[4];
                                        Array.Copy(sampledata, 4 * i, _h2odata, 0, 4);
                                        temp.h2o_AW = BitConverter.ToSingle(_h2odata, 0);
                                        Array.Copy(sampledata, 4 * i + 4, _h2odata, 0, 4);
                                        temp.h2o_T = BitConverter.ToSingle(_h2odata, 0);
                                        Array.Copy(sampledata, 4 * i + 8, _h2odata, 0, 4);
                                        temp.h2o_PPM = BitConverter.ToSingle(_h2odata, 0);
                                        ///原始顺序存储采样点数据
                                        //for (int j = 0; j < 3; j++)
                                        //{
                                        //    byte[]_h2odata = new byte[4];
                                        //    //四个四个字节的取数据
                                        //    Array.Copy(sampledata , 4*(3*i+j) , _h2odata , 0 , 4);
                                        //    h2odata.Add(BitConverter.ToSingle(_h2odata , 0));
                                        //}
                                    }
                                }
                                break;
                            //CO2
                            case (byte)MSGEncoding.ReadBuffer.ReadCO2Cache:
                                {
                                    point = datalen / 2;
                                    for (int i = 0; i < point; i++)
                                    {
                                        byte[] _co2data = new byte[2];
                                        Array.Copy(sampledata , 2*i , _co2data , 0, 2);
                                        co2data.Add(BitConverter.ToUInt16(_co2data , 0));
                                    }
                                }
                                break;
                            //6组分
                            case (byte)MSGEncoding.ReadBuffer.ReadGasCache:
                                {
                                    point = datalen / 2;
                                    for (int i = 0; i < point; i++)
                                    {
                                        byte[] _sixgasdata = new byte[2];
                                        Array.Copy(sampledata , 2*i , _sixgasdata , 0 , 2);
                                        sixgasdata.Add(BitConverter.ToInt16(_sixgasdata , 0));
                                    }
                                }
                                break;
                            default:
                                {
                                    //MessageBox.Show("读缓存过程错误！");
                                }
                                break;
                        }
                    }
                    break;
                //短帧
                case 0:
                    {
                        sampledata = ((PrepareData.Msg_Bus)(ByteStruct.BytesToStruct(b_recvframe,
                                    typeof(PrepareData.Msg_Bus)))).data;
                        datalen = sampledata.Length;
                        switch (msgsubtype)
                        {
                            //H20必定为长帧，至少为三个浮点数位12字节
                            //case 0x00:
                            //    {
                            //    }
                            //    break;
                            //CO2
                            case (byte)MSGEncoding.ReadBuffer.ReadCO2Cache:
                                {
                                    point = datalen / 2;
                                    for (int i = 0; i < point; i++)
                                    {
                                        byte[] _co2data = new byte[2];
                                        Array.Copy(sampledata , 2*i , _co2data , 0 , 2);
                                        co2data.Add(BitConverter.ToUInt16(_co2data, 0));
                                    }
                                }
                                break;
                            //6组分
                            case (byte)MSGEncoding.ReadBuffer.ReadGasCache:
                                {
                                    point = datalen / 2;
                                    for (int i = 0; i < point; i++)
                                    {
                                        byte[] _sixgasdata = new byte[2];
                                        Array.Copy(sampledata, 2 * i, _sixgasdata, 0, 2);
                                        sixgasdata.Add(BitConverter.ToInt16(_sixgasdata, 0));
                                    }
                                }
                                break;
                            default:
                                {
                                    //MessageBox.Show("读缓存过程存在错误！");
                                }
                                break;
                        }
                    }
                    break;
            }
            if((byte)MSGEncoding.ReadBuffer.ReadGasCache == msgsubtype)
            {
                return sixgasdata.ToArray<short>();
            }
            else
                if ((byte)MSGEncoding.ReadBuffer.ReadCO2Cache == msgsubtype)
                {
                    return co2data.ToArray<ushort>();
                }
                else
                    {
                        return h2odata.ToArray<H2OPoint>();
                    }
        }
    }
}
