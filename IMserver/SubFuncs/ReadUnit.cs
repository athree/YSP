using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMserver.CommonFuncs;
using IMserver.Models.SimlDefine;

namespace IMserver.SubFuncs
{
    public class ReadUnit
    {
        #region 注释的getdata
        //public static List<ushort> handleunit = new List<ushort>(); 
        /// <summary>
        /// 从web段获取操作单元编码数组转换为字节数组
        /// </summary>
        /// <returns>用于组入字节数组的字节流</returns>
        //public static byte[] GetData(ushort[] handleunit)
        //{
        //    byte[] handleunitbytes = new byte[2 * handleunit.Length];
        //    for (int i = 0; i < handleunit.Length; i++)
        //    {
        //        handleunitbytes[2 * i] = (byte)handleunit[i];
        //        handleunitbytes[2 * i + 1] = (byte)(handleunit[i] >> 8);
        //    }
        //    return handleunitbytes;
        //}
        #endregion

        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ReadData(byte destid, ushort[] unit)
        {   
            byte [] donedata;
            byte[] data = GetData(unit);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.ReadUnit;
            _frame.msgSubType = (byte)MSGEncoding.ReadUint.ReadData;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }
           
        /// <summary>
        /// 读配置信息
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Readcfg(byte destid, ushort[] unit)
        {
            byte[] donedata;
            byte[] data = GetData(unit);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.ReadUnit;
            _frame.msgSubType = (byte)MSGEncoding.ReadUint.Readcfg;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }

        /// <summary>
        /// 查询设备状态
        /// </summary>
        /// <param name="destid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetDevStatus(byte destid, ushort[] unit)
        {
            byte[] donedata;
            byte[] data = GetData(unit);
            PrepareData.Msg_Bus _frame = new PrepareData.Msg_Bus();
            _frame.flag = PrepareData.BUS_FRAME_FLAG;
            _frame.srcID = MSGEncoding.srcID;
            _frame.destID = destid;
            _frame.msgDir = (byte)MSGEncoding.MsgDir.Request;
            _frame.msgVer = MSGEncoding.msgVer;
            _frame.msgType = (byte)MSGEncoding.MsgType.ReadUnit;
            _frame.msgSubType = (byte)MSGEncoding.ReadUint.GetDevStatus;
            _frame.dataLen = (ushort)data.Length;
            donedata = PrepareData.MergeMsg(ref _frame, data);
            return donedata;
        }

        /// <summary>
        /// 返回数据域,用于组帧
        /// </summary>
        /// <returns></returns>
        public static byte[] GetData(ushort []handleunit)
        {
            byte[] tempdata;
            List<byte> _tempdata = new List<byte>();
            for (int i = 0; i < handleunit.Length; i++)
            {
                _tempdata.Add((byte)handleunit[i]);
                _tempdata.Add((byte)(handleunit[i] >> 8));
            }
            tempdata = _tempdata.ToArray<byte>();
            return tempdata;
        }

        /// <summary>
        /// 重写，内部数据处理单独封装为方法，一些重复处理在switch外完成
        /// </summary>
        /// <param name="unit"> 操作单元数组</param>
        /// <param name="comtemp">发送信息缓存</param>
        /// <param name="b_recvframe">接受到的数据字节数组</param>
        /// <param name="errornum">请求操作单元的错误数</param>
        /// <param name="error">错误列表</param>
        /// <returns></returns>
        public static Dictionary<ushort, object> MsgHandle(ushort[] unit, PrepareData.Compare comtemp, byte[] b_recvframe,
                                                           ref ushort errornum, Dictionary<ushort, ushort> error)
        {
            Dictionary<ushort, object> retinfo = new Dictionary<ushort, object>();

            switch (HandleData.TypeofFrame(b_recvframe).frametype)
            {
                //接收的是长帧
                case 1:
                    {
                        //接收数据的拆分
                        byte[] recvframeout = new byte[b_recvframe.Length - PrepareData.BUS_FRAME_MINLEN - 2];
                        Array.Copy(b_recvframe, PrepareData.BUS_FRAME_MINLEN, recvframeout, 0, 
                                   b_recvframe.Length - PrepareData.BUS_FRAME_MINLEN - 2);
                        //传入的字节数组只包括执行状态字和返回数据（除CRC外）
                        TransInfo(recvframeout, unit, retinfo, ref errornum, error);

                        #region  从右边去第一不方便，反相的对应取
                        //for (int i = 0; i < basebyte; i++)
                        //{
                        //    byte low = sendframeout[sendframeout.Length - 2 * (i + 2)];
                        //    byte high = sendframeout[sendframeout.Length - 2 * (i + 2) + 1];
                        //    ushort temp = (ushort)(((ushort)(high << 8)) | low);
                        //    //读最后一位执行状态字，看是否有错误，有错误则加到错误列表，并错误数加1
                        //    //没有错误则读取倒数第一个值，与操作单元一起加入数据字典，取值时长度由数据字典截取
                        //}
                        #endregion
                    }
                    break;
                //接收帧为短帧
                case 0:
                    {
                        byte[] onlydata = ((PrepareData.Msg_Bus)ByteStruct.BytesToStruct(b_recvframe,
                                                                                         typeof(PrepareData.Msg_Bus))).data;
                        //只有执行状态字和返回数据
                        TransInfo(onlydata, unit, retinfo, ref errornum, error);
                    }
                    break;
                //返回的代码错误
                default:
                    {
                        //MessageBox.Show("返回的代码错误");
                    }
                    break;
            }
            return retinfo;
        }

        /// <summary>
        /// 翻译接受的数据字节数组
        /// </summary>
        /// <param name="predata">传入字节数组（执行状态字和操作单元返回数据的组合）</param>
        /// <param name="unit"></param>
        /// <param name="remainder"></param>
        /// <param name="retinfo"></param>
        /// <param name="errornum"></param>
        /// <param name="error"></param>
        public static void TransInfo(byte[] predata, ushort[] unit, Dictionary<ushort, object> retinfo,
                                     ref ushort errornum, Dictionary<ushort, ushort> error)
        {
            int m = 0;                   //返回数据字节数组的下标
            int index = 0;               //操作单元数组的下标
            //操作单元的个数，这里是从发送帧中间接得来
            int basenum = unit.Length;
            int remainder = basenum % 8;
            //发来字节中的执行状态字所占字节数
            int basebyte = (int)Math.Ceiling(basenum / 8.0);

            //存放执行状态字
            byte[] b_basebyte = new byte[basebyte];
            Array.Copy(predata, 0, b_basebyte, 0, basebyte);

            //返回值的数据信息（不包括执行状态字）
            byte[] onlydata = new byte[predata.Length - basebyte];
            Array.Copy(predata, basebyte, onlydata, 0, predata.Length - basebyte);

            //只要传入需要解析的数据、操作单元数组、余数
            for (int i = basebyte - 1; i >= 0; i--)
            {
                //处理如果存在少于8个执行状态字的首个字节
                if (0 == i)
                {
                    for (int j = 0; j <= remainder - 1; j++)
                    {
                        //对应操作单元的执行状态字是否成功
                        int result = b_basebyte[i] & (byte)(int)Math.Pow(2, j);
                        //哪一个执行状态字
                        //int index = remainder - j - 1;
                        //请求的操作单元返回错误
                        if (0 == result)
                        {
                            //获取错误代码
                            byte[] temp = new byte[2];
                            Array.Copy(onlydata, m%8, temp, 0, 2);
                            //推移移动的位数
                            m += 2;
                            ushort errorcode = (ushort)ByteStruct.BytesToStruct(temp, typeof(ushort));
                            //errorcode处理
                            errornum++;
                            error.Add(unit[index], errorcode);
                            index++;
                            //获取操作单元号（非结构体通信下读两个字节），获取错误代码存入
                            //获取操作单元和值存入
                        }
                        else
                        {
                            //对应操作单元的值类型所占空间
                            ushort count = MyDictionary.unitlendict[unit[index]];
                            byte[] temp = new byte[count];
                            Array.Copy(onlydata, m, temp, 0, count);
                            m += count;
                            object value = GetValue(temp, unit[index]);
                            retinfo.Add(unit[index], value);
                            index++;
                        }
                    }
                    //只处理一遍
                    //remainder = 0;
                }
                else
                {
                    for (int j = 0; j <= 8-1; j++)
                    {
                        int result = b_basebyte[i] & (byte)(int)Math.Pow(2, j);
                        //int index = i * 8 + 7 -j;
                        //请求的操作单元返回错误
                        if (0 == result)
                        {
                            //获取错误代码
                            byte[] temp = new byte[2];
                            Array.Copy(onlydata, m, temp, 0, 2);
                            //推移移动的位数
                            m += 2;
                            ushort errorcode = (ushort)ByteStruct.BytesToStruct(temp, typeof(ushort));
                            //errorcode处理
                            errornum++;
                            error.Add(unit[index], errorcode);
                            index++;
                            //获取操作单元号（非结构体通信下读两个字节），获取错误代码存入
                            //获取操作单元和值存入
                        }
                        else
                        {
                            ushort count = MyDictionary.unitlendict[unit[index]];
                            byte[] temp = new byte[count];
                            Array.Copy(onlydata, m, temp, 0, count);
                            m += count;
                            object value = GetValue(temp, unit[index]);
                            retinfo.Add(unit[index], value);
                            index++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 未考虑数组的情况，用于解析帧
        /// </summary>
        /// <param name="src">根据操作单元查字典把字节数组翻译为相应的数据类型</param>
        /// <param name="unit">操作单元</param>
        /// <returns></returns>
        public static object GetValue(byte[] src, ushort unit)
        {
            object temp;
            switch (MyDictionary.unittypedict[unit])
            {
                //uchar
                case 0:
                    {
                        temp = src[0];
                    }
                    break;
                //ushort
                case 1:
                    {
                        temp = ByteStruct.BytesToStruct(src, typeof(ushort));
                    }
                    break;
                //float
                case 2:
                    {
                        temp = ByteStruct.BytesToStruct(src, typeof(float));
                    }
                    break;
                //datetime
                case 3:
                    {
                        long _temp = (long)ByteStruct.BytesToStruct(src , typeof(long));
                        temp = TimerTick.TimeSpanToDate(_temp);
                    }
                    break;
                //char[]
                case 4:
                    {
                        char[] _temp = new char[src.Length];
                        for (int i = 0; i < src.Length; i++)
                        {
                            _temp[i] = (char)src[i];
                        }
                        temp = _temp;
                        break;
                    }
                //ushort[2]==EraseRange
                case 5:
                    {
                        //temp = (Define.EraseRange)ByteStruct.BytesToStruct(src , 
                        //                                                   typeof(Define.EraseRange));
                        temp = (EraseRange)ByteStruct.BytesToStruct(src , 
                                                                           typeof(EraseRange));
                        break;
                    }
                //ushort[4]==GasFixPara_A
                case 6:
                    {
                        //temp = (Define.GasFixPara_A)ByteStruct.BytesToStruct(src,
                        //                                                     typeof(Define.GasFixPara_A));
                        temp = (Para_A)ByteStruct.BytesToStruct(src,
                                                                             typeof(Para_A));
                        break;
                    }
                //ushort[4]==GasFixPara_B
                case 7:
                    {
                        temp = (Para_B)ByteStruct.BytesToStruct(src,
                                                                             typeof(Para_B));
                        break;
                    }
                //float[2]==EnvironmentSetting--油品系数
                case 8:
                    {
                        //temp = (Define.EnvironmentSetting)ByteStruct.BytesToStruct(src,
                        //                                                     typeof(Define.EnvironmentSetting));
                        temp = (OilFactor)ByteStruct.BytesToStruct(src,
                                                                             typeof(OilFactor));
                        break;
                    }
                //float[3]==H2OAnlyParam_AW
                case 9:
                    {
                        //temp = (Define.H2OAnlyParam_AW)ByteStruct.BytesToStruct(src,
                        //                                                     typeof(Define.H2OAnlyParam_AW));
                        temp = (H2oFixPara_AW)ByteStruct.BytesToStruct(src,
                                                                             typeof(H2oFixPara_AW));
                        break;
                    }
                //float[3]==H2OAnlyParam_T
                case 10:
                    {
                        //temp = (Define.H2OAnlyParam_T)ByteStruct.BytesToStruct(src,
                        //                                                     typeof(Define.H2OAnlyParam_T));
                        temp = (H2oFixPara_T)ByteStruct.BytesToStruct(src,
                                                                             typeof(H2oFixPara_T));
                        break;
                    }
                //float[5]==GasFixParameters
                case 11:
                    {
                        //temp = (Define.GasFixParameters)ByteStruct.BytesToStruct(src,
                        //                                                     typeof(Define.GasFixParameters));
                        temp = (GasFixK)ByteStruct.BytesToStruct(src,
                                                                             typeof(GasFixK));
                        
                        break;
                    }
                default:
                    //MessageBox.Show("readunit-getdata:这里只做系统类型的处理，自定义类型暂未考虑");
                    temp = null;
                    break;
            }
            return temp;
        }
    }
}
