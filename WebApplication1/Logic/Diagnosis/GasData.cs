using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

using System.IO;
using WebApplication1.Models;
using WebApplication1.Models.SimlDefine;
namespace WebApplication1.Diagnosis
{
    /// <summary>
    /// 气体数据的格式：
    ///     采样日期[7byte] + 七组气体浓度值[7*4 byte](顺序：h2,co,ch4,c2h2,c2h4,c2h6,co2)+ 
    ///     80Bytes空白（采样计数[2Bytes]+脱气完成信号[1Bytes]+blank[17bytes]）+
    ///     原始数据[六组分+CO2，长度从配置参数中读出]+ Config结构体[配置参数 + 计算参数]
    /// </summary>
    public class GasData
    {
        private SIML _data;
        private const int timeLen = 7;                //采样日期7byte
        private const int denisityLen = 4 * 7;        //浓度值，浮点型表示，7组气体
        private const int blankLen = 80;              //80个字节的空白，
        private const int counterLen = 2;             //采样计数长度
        private int sixDataLen = 0;          //六组分原始数据长度
        private int co2Datalen = 0;


        //public GasData(byte Deviceid, SIML data)
        //{
        //    Debug.Assert(data != null);
        //    if (data == null)
        //    {
        //        return;
        //    }

        //    _data = data;
        //    //SIML.Config cfg;
        //    //byte[] cfgByte = new byte[SIML.CONFIG_LEN];   //data中的配置信息
        //    //Array.Copy(_data, _data.Length - cfgByte.Length, cfgByte, 0, SIML.CONFIG_LEN);

        //    //object obj = CommonFuncs.BytesToStuct(cfgByte, typeof(SIML.Config));
        //    //if (obj != null)
        //    //{
        //    //    cfg = (SIML.Config)obj;
                
        //    //}
        //    //else
        //    //{
        //    //    throw new Exception("Invalid Gas_Data!");
        //    //}


        //    SampleSetting set = data.SampSet;               //采样设置

        //    //计算六组分和二氧化碳数据的长度
        //    //sixDataLen = CommonFuncs.ConvertBS(set.GasSampPointNum) * 2;

        //    //if (cfg.module.CO2 == 0x01)
        //    //{
        //    //    co2Datalen = CommonFuncs.ConvertBS(set.Co2SampPointNum) * 2;
        //    //}
        //    //else
        //    //{
        //    //    co2Datalen = 0;
        //    //}
        //}

        /// <summary>
        /// 采样日期
        /// </summary>
        public DateTime? SampleTime
        {
            get
            {
                if (_data == null)
                {   return null;    }

            //    byte[] timeBytes = new byte[timeLen];
            //    Array.Copy(_data, 0, timeBytes, 0, timeLen);

            //    object obj = CommonFuncs.BytesToStuct(timeBytes, typeof(SIML.DateDef));
            //    if (obj == null)
            //    { return null; }
            //    SIML.DateDef date = (SIML.DateDef)obj;

            //    try
            //    {
            //        return new DateTime(CommonFuncs.ConvertBS(date.year), date.month, date.day, 
            //            date.hour, date.min, date.second);
            //    }
            //    catch (System.Exception ex)
            //    { return null; }
            //}
            DateTime d=_data.SampStart.SampleTime;           
            return d;
            }
        }
        
        /// <summary>
        /// 七种气体浓度
        /// </summary>
        public float[] Denisity
        {
            get 
            {
                if(_data == null)
                {
                    return null;
                }

            //    byte[] bytes = new byte[denisityLen];
            //    Array.Copy(_data, timeLen, bytes, 0, denisityLen);
            //    object[] objs = CommonFuncs.BytesToStructArray(bytes, typeof(float), 7);
            //    if (objs == null)
            //    {
            //        return null;
            //    }

                float[] rets = new float[7];
            //    for (int i = 0; i < objs.Length; i++ )
            //    {
            //        rets[i] = CommonFuncs.ConvertBS((float)objs[i]);
            //    }
                rets[1] = _data.Content.H2ppm;
                rets[2] = _data.Content.COppm;
                rets[3] = _data.Content.CH4ppm;
                rets[4] = _data.Content.C2H2ppm;
                rets[5] = _data.Content.C2H4ppm;
                rets[6] = _data.Content.C2H6ppm;
                rets[7] = _data.Content.CO2ppm;               

                return rets;
            }
            


        }

        /// <summary>
        /// 采样计数??????????????????????????????
        /// </summary>
        //public ushort WorkCounter
        //{
        //    get
        //    {
        //        return (ushort)(_data[timeLen + denisityLen] << 8 | _data[timeLen + denisityLen + 1]);
        //    }
        //}

        public SGasOtherData? OtherData
        {
            get
            {
                if (_data == null)
                {
                    return null;
                }

                byte[] otherdata = new byte[SIML.GASOTHERDATA_LEN];
                int index = timeLen + denisityLen;
                Array.Copy(_data, index, otherdata, 0, otherdata.Length);
                object obj = CommonFuncs.BytesToStuct(otherdata, typeof(SIML.SGasOtherData));
                if (obj == null)
                {
                    return null;
                }
                return (SIML.SGasOtherData)obj;
            }
        }
        /// <summary>
        /// 六组分采原始数据
        /// </summary>
        public byte[] SixRawData
        {
            get
            {
                if (_data == null)
                {
                    return null;
                }

                int index = timeLen + denisityLen + blankLen;
                byte[] sixRawData = new byte[sixDataLen];
                Array.Copy(_data, index, sixRawData, 0, sixDataLen);
                return sixRawData;
            }
        }

        /// <summary>
        /// CO2采样原始数据
        /// </summary>
        public byte[] CO2RawData
        {
            get
            {
                if (_data == null || co2Datalen == 0)
                {
                    return null;
                }

                //if (Config == null)
                //{
                //    return null;
                //}

                if (Config.Value.module.CO2 == 0x00)   //不支持CO2
                {
                    return null;
                }

                //byte[] co2RawData = new byte[co2Datalen];
                //int index = timeLen + denisityLen + blankLen + sixDataLen;
                //if (index + co2Datalen > _data.Length)
                //{
                //    return null;
                //}

                
                //Array.Copy(_data, index, co2RawData, 0, co2Datalen);
                //return co2RawData;
                return _data.Content.CO2ppm;
            }
        }

        /// <summary>
        /// 与此条数据相关的配置信息，包括系统支持模块、采样时间、工作参数配置、分析参数配置
        /// </summary>
        public Config? Config
        {
            get
            {
                if (_data == null)
                {
                    return null;
                }

                byte[] config = new byte[SIML.CONFIG_LEN];
                int index = timeLen + denisityLen + blankLen + sixDataLen + co2Datalen;

                Array.Copy(_data, index, config, 0, config.Length);
                object obj = CommonFuncs.BytesToStuct(config, typeof(SIML.Config));
                if (obj == null)
                {
                    return null;
                }
                return (SIML.Config)obj;
            }
        }

        /// <summary>
        /// 气体的工作配置参数
        /// </summary>
        public SampleSetting? SampleSet
        {
            get
            {
                if (_data == null || this.Config == null)
                {
                    return null;
                }
                return Config.Value.sample;
            }
        }

        /// <summary>
        /// 气体的分析参数
        /// </summary>
        public AnalysisParameter? Analysis
        {
            get
            {
                if (_data == null || this.Config == null)
                {
                    return null;
                }
                return Config.Value.analysis;
            }
        }

        /// <summary>
        /// 脱气是否完成
        /// </summary>
        public bool? TuiqiOver
        {
            get
            {
                if (_data == null)
                {
                    return null;
                }

                if(_data[timeLen + denisityLen + counterLen] == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

    }

    /// <summary>
    /// 格式：
    ///     日期[7bytes] + AW[float, 4bytes] + T[float, 4bytes] + PPM[float, 4bytes] + 
    ///     空白[20bytes] + AW采样值[2bytes] + T采样值[2Bytes] +  
    ///     Config结构体[配置参数 + 计算参数] 
    /// </summary>
    public class H2OData
    {
        private byte[] _data;
        private const int timeLen = 7;    //采样日期6byte
        private const int awLen = 4;
        private const int tLen = 4;
        private const int ppmLen = 4;
        private const int blankLen = 20;

        public H2OData(byte deviceID, byte[] data)
        {
            _data = data;
        }

        /// <summary>
        /// 采样日期
        /// </summary>
        public DateTime? SampleTime
        {
            get
            {
                if (_data == null)
                {
                    return null;
                }

                try
                {
                    byte[] timeBytes = new byte[timeLen];
                    Array.Copy(_data, 0, timeBytes, 0, timeLen);
                    object obj = CommonFuncs.BytesToStuct(timeBytes, typeof(SIML.DateDef));
                    if (obj == null)
                    {
                        return null;
                    }
                    SIML.DateDef date = (SIML.DateDef)obj;

                    return new DateTime(CommonFuncs.ConvertBS(date.year), date.month, date.day,
                        date.hour, date.min, date.second);
                }
                catch (System.Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 水活性
        /// </summary>
        public float? AW
        {
            get
            {
                if (_data == null)
                {
                    return null;
                }

                byte[] aw = new byte[awLen];
                Array.Copy(_data, timeLen, aw, 0, awLen);
                object obj = CommonFuncs.BytesToStuct(aw, typeof(float));
                if (obj == null)
                {
                    return null;
                }
                else
                {
                    return CommonFuncs.ConvertBS((float)obj);       //数据库中都按照大端方式存储
                }
            }
        }

        /// <summary>
        /// 温度
        /// </summary>
        public float? T
        {
            get
            {
                if (_data == null)
                {
                    return null;
                }

                byte[] t = new byte[tLen];
                Array.Copy(_data, timeLen + awLen, t, 0, t.Length);
                object obj = CommonFuncs.BytesToStuct(t, typeof(float));
                if (obj == null)
                {
                    return null;
                }
                else
                {
                    return CommonFuncs.ConvertBS((float)obj);       //数据库中都按照大端方式存储
                }
            }
        }

        public float? PPM
        {
            get
            {
                if (_data == null)
                {
                    return null;
                }

                byte[] ppm = new byte[ppmLen];
                Array.Copy(_data, timeLen + awLen + tLen, ppm, 0, ppmLen);
                object obj = CommonFuncs.BytesToStuct(ppm, typeof(float));
                if (obj == null)
                {
                    return null;
                }
                else
                {
                    return CommonFuncs.ConvertBS((float)obj);       //数据库中都按照大端方式存储
                }
            }
        }

        /// <summary>
        /// 根据aw采样值计算出的AW
        /// aw = (float)(2.0625 * aw / 4096 - 0.25);
        /// </summary>
        public ushort? RawAW
        {
            get
            {
                if (_data == null)
                {
                    return null;
                }

                byte[] bytes = new byte[2];
                int index = timeLen + awLen + tLen + ppmLen + blankLen;
                Array.Copy(_data, index, bytes, 0, 2);

                ushort aw = CommonFuncs.ConvertBS(BitConverter.ToUInt16(bytes, 0));
                //return (float)(2.0625 * aw / 4096 - 0.25);
                return aw;
            }
        }

        /// <summary>
        /// 根据t采样值计算出的T
        /// t = (float)(4125 / 65536 * t - 56.25);
        /// </summary>
        public ushort? RawT
        {
            get
            {
                if (_data == null)
                {
                    return null;
                }

                byte[] bytes = new byte[2];
                int index = timeLen + awLen + tLen + ppmLen + blankLen + 2;
                Array.Copy(_data, index, bytes, 0, 2);

                ushort t = CommonFuncs.ConvertBS(BitConverter.ToUInt16(bytes, 0));
                //return (float)(4125 / 65536 * t - 56.25);
                return t;
            }
        }

        /// <summary>
        /// 与此条数据相关的配置信息，包括系统支持模块、采样时间、工作参数配置、分析参数配置
        /// </summary>
        public Config? Config
        {
            get
            {
                if (_data == null)
                {
                    return null;
                }

                byte[] config = new byte[SIML.CONFIG_LEN];
                int index = timeLen + awLen + tLen + ppmLen + blankLen + 4;

                Array.Copy(_data, index, config, 0, config.Length);
                object obj = CommonFuncs.BytesToStuct(config, typeof(SIML.Config));
                if (obj == null)
                {
                    return null;
                }
                return (SIML.Config)obj;
            }
        }


        /// <summary>
        /// 气体的工作配置参数
        /// </summary>
        public SampleSetting? SampleSet
        {
            get
            {
                if (_data == null || this.Config == null)
                {
                    return null;
                }
                return Config.Value.sample;
            }
        }

        /// <summary>
        /// 气体的分析参数
        /// </summary>
        public AnalysisParameter? Analysis
        {
            get
            {
                if (_data == null || this.Config == null)
                {
                    return null;
                }
                return Config.Value.analysis;
            }
        }

        public byte[] RawData
        {
            get
            {
                if (_data == null)
                {
                    return null;
                }

                byte[] bytes = new byte[4];
                int index = timeLen + awLen + tLen + ppmLen + blankLen;
                Array.Copy(_data, index, bytes, 0, bytes.Length);
                return bytes;
            }
        }
    }
}
