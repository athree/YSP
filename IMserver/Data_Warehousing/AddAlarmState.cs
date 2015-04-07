using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMserver.DBservice;
using IMserver.Models;
using IMserver.CommonFuncs;

namespace IMserver.Data_Warehousing
{
    public class AddAlarmState
    {
        /// <summary>
        /// 后台解析到数据字典转换后存入数据库
        /// ！！！异常
        /// </summary>
        /// <param name="middata"></param>
        /// <param name="devid"></param>
        /// <returns></returns>
        public static bool Warehousing(Dictionary<ushort, object> middata, byte devid)
        {
            AlarmState als = new AlarmState();
            als.aa = new Models.SimlDefine.AlarmAll();
            als.aa.GasAlarm = new Models.SimlDefine.GasAlarm[7];
            //可以在构造函数中实现，通过继承数据库集合类，并改写构造函数，由于不确定带有方法的类能否作为集合类映射到数据库
            als.aa.thga = new Models.SimlDefine.TotHyd_GasAlarm();
            als.aa.ta = new Models.SimlDefine.T_GasAlarm();
            als.aa.pa = new Models.SimlDefine.PPM_GasAlarm();
            als.aa.awa = new Models.SimlDefine.AW_GasAlarm();

            //为可能存在的部分操作设置new标志
            //bool[] GasAlarm_Flag = new bool[]{false,false,false,false,false,false,false,false,false,false,false};

            als.DevID = devid.ToString();
            als.Time_Stamp = DateTime.Now;
            foreach(KeyValuePair<ushort , object> kvp in middata)
            {
                switch (kvp.Key)
                {
                    case 286:
                        {
                            als.aa.AutoAlarm = (char)kvp.Value;
                            break;
                        }
                    case 287:
                        {
                            als.aa.AutoDiagnose = (char)kvp.Value;
                            break;
                        }
                    case 288:
                        {
                            als.aa.Interval = (ushort)kvp.Value;
                            break;
                        }
                    /*========================================================*/
                    #region H2报警及分析
                    case 291:	//H2气体含量注意值，一级报警
                        {
                            als.aa.GasAlarm[0].Level1 = (float)kvp.Value;
                            break;
                        }
                    case 292:	//H2气体含量注意值，二级报警
                        {
                            als.aa.GasAlarm[0].Level2 = (float)kvp.Value;
                            break;
                        }
                    case 313:	//H2绝对产气速率注意值ul/天，一级报警
                        {
                            als.aa.GasAlarm[0].AbsLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 314:	//H2绝对产气速率注意值ul/天，二级报警
                        {
                            als.aa.GasAlarm[0].AbsLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 315:	//H2相对产气速率注意值%/月，一级报警
                        {
                            als.aa.GasAlarm[0].RelLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 316:	//H2相对产气速率注意值%/月，二级报警
                        {
                            als.aa.GasAlarm[0].RelLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 317:	//H2报警门限值触发条件ul/L
                        {
                            als.aa.GasAlarm[0].AlarmContent = (float)kvp.Value;
                            break;
                        }
                    case 318:	//H2绝对产气速率触发ul/天
                        {
                            als.aa.GasAlarm[0].AbsRateAlarmNumber = (float)kvp.Value;
                            break;
                        }
                    case 319:	//H2相对产气速率触发%/月
                        {
                            als.aa.GasAlarm[0].RelRateAlarmNumer = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    /*========================================================*/
                    #region CO报警及分析
                    case 293:	//CO气体含量注意值，一级报警
                        {
                            als.aa.GasAlarm[1].Level1 = (float)kvp.Value;
                            break;
                        }
                    case 294:	//CO气体含量注意值，二级报警
                        {
                            als.aa.GasAlarm[1].Level2 = (float)kvp.Value;
                            break;
                        }
                    case 320:	//CO绝对产气速率注意值ul/天，一级报警
                        {
                            als.aa.GasAlarm[1].AbsLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 321:	//CO绝对产气速率注意值ul/天，二级报警
                        {
                            als.aa.GasAlarm[1].AbsLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 322:	//CO相对产气速率注意值%/月，一级报警
                        {
                            als.aa.GasAlarm[1].RelLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 323:	//CO相对产气速率注意值%/月，二级报警
                        {
                            als.aa.GasAlarm[1].RelLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 324:	//CO报警门限值触发条件ul/L
                        {
                            als.aa.GasAlarm[1].AlarmContent = (float)kvp.Value;
                            break;
                        }
                    case 325:	//CO绝对产气速率触发ul/天
                        {
                            als.aa.GasAlarm[1].AbsRateAlarmNumber = (float)kvp.Value;
                            break;
                        }
                    case 326:	//CO相对产气速率触发%/月
                        {
                            als.aa.GasAlarm[1].RelRateAlarmNumer = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    /*========================================================*/
                    #region CH4报警及分析
                    case 295:	//CH4气体含量注意值，一级报警
                        {
                            als.aa.GasAlarm[2].Level1 = (float)kvp.Value;
                            break;
                        }
                    case 296:	//CH4气体含量注意值，二级报警
                        {
                            als.aa.GasAlarm[2].Level2 = (float)kvp.Value;
                            break;
                        }
                    case 327:	//CH4绝对产气速率注意值ul/天，一级报警
                        {
                            als.aa.GasAlarm[2].AbsLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 328:	//CH4绝对产气速率注意值ul/天，二级报警
                        {
                            als.aa.GasAlarm[2].AbsLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 329:	//CH4相对产气速率注意值%/月，一级报警
                        {
                            als.aa.GasAlarm[2].RelLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 330:	//CH4相对产气速率注意值%/月，二级报警
                        {
                            als.aa.GasAlarm[2].RelLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 331:	//CH4报警门限值触发条件ul/L
                        {
                            als.aa.GasAlarm[2].AlarmContent = (float)kvp.Value;
                            break;
                        }
                    case 332:	//CH4绝对产气速率触发ul/天
                        {
                            als.aa.GasAlarm[2].AbsRateAlarmNumber = (float)kvp.Value;
                            break;
                        }
                    case 333:	//CH4相对产气速率触发%/月
                        {
                            als.aa.GasAlarm[2].RelRateAlarmNumer = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    /*========================================================*/
                    #region C2H2报警及分析
                    case 301:	//C2H2气体含量注意值，一级报警
                        {
                            als.aa.GasAlarm[3].Level1 = (float)kvp.Value;
                            break;
                        }
                    case 302:	//C2H2气体含量注意值，二级报警
                        {
                            als.aa.GasAlarm[3].Level2 = (float)kvp.Value;
                            break;
                        }
                    case 334:	//C2H2绝对产气速率注意值ul/天，一级报警
                        {
                            als.aa.GasAlarm[3].AbsLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 335:	//C2H2绝对产气速率注意值ul/天，二级报警
                        {
                            als.aa.GasAlarm[3].AbsLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 336:	//C2H2相对产气速率注意值%/月，一级报警
                        {
                            als.aa.GasAlarm[3].RelLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 337:	//C2H2相对产气速率注意值%/月，二级报警
                        {
                            als.aa.GasAlarm[3].RelLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 338:	//C2H2报警门限值触发条件ul/L
                        {
                            als.aa.GasAlarm[3].AlarmContent = (float)kvp.Value;
                            break;
                        }
                    case 339:	//C2H2绝对产气速率触发ul/天
                        {
                            als.aa.GasAlarm[3].AbsRateAlarmNumber = (float)kvp.Value;
                            break;
                        }
                    case 340:	//C2H2相对产气速率触发%/月
                        {
                            als.aa.GasAlarm[3].RelRateAlarmNumer = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    /*========================================================*/
                    #region C2H4报警及分析
                    case 297:	//C2H4气体含量注意值，一级报警
                        {
                            als.aa.GasAlarm[4].Level1 = (float)kvp.Value;
                            break;
                        }
                    case 298:	//C2H4气体含量注意值，二级报警
                        {
                            als.aa.GasAlarm[4].Level2 = (float)kvp.Value;
                            break;
                        }
                    case 341:	//C2H4绝对产气速率注意值ul/天，一级报警
                        {
                            als.aa.GasAlarm[4].AbsLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 342:	//C2H4绝对产气速率注意值ul/天，二级报警
                        {
                            als.aa.GasAlarm[4].AbsLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 343:	//C2H4相对产气速率注意值%/月，一级报警
                        {
                            als.aa.GasAlarm[4].RelLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 344:	//C2H4相对产气速率注意值%/月，二级报警
                        {
                            als.aa.GasAlarm[4].RelLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 345:	//C2H4报警门限值触发条件ul/L
                        {
                            als.aa.GasAlarm[4].AlarmContent = (float)kvp.Value;
                            break;
                        }
                    case 346:	//C2H4绝对产气速率触发ul/天
                        {
                            als.aa.GasAlarm[4].AbsRateAlarmNumber = (float)kvp.Value;
                            break;
                        }
                    case 347:	//C2H4相对产气速率触发%/月
                        {
                            als.aa.GasAlarm[4].RelRateAlarmNumer = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    /*========================================================*/
                    #region C2H6报警及分析
                    case 299:	//C2H6气体含量注意值，一级报警
                        {
                            als.aa.GasAlarm[5].Level1 = (float)kvp.Value;
                            break;
                        }
                    case 300:	//C2H6气体含量注意值，二级报警
                        {
                            als.aa.GasAlarm[5].Level2 = (float)kvp.Value;
                            break;
                        }
                    case 348:	//C2H6绝对产气速率注意值ul/天，一级报警
                        {
                            als.aa.GasAlarm[5].AbsLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 349:	//C2H6绝对产气速率注意值ul/天，二级报警
                        {
                            als.aa.GasAlarm[5].AbsLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 350:	//C2H6相对产气速率注意值%/月，一级报警
                        {
                            als.aa.GasAlarm[5].RelLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 351:	//C2H6相对产气速率注意值%/月，二级报警
                        {
                            als.aa.GasAlarm[5].RelLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 352:	//C2H6报警门限值触发条件ul/L
                        {
                            als.aa.GasAlarm[5].AlarmContent = (float)kvp.Value;
                            break;
                        }
                    case 353:	//C2H6绝对产气速率触发ul/天
                        {
                            als.aa.GasAlarm[5].AbsRateAlarmNumber = (float)kvp.Value;
                            break;
                        }
                    case 354:	//C2H6相对产气速率触发%/月
                        {
                            als.aa.GasAlarm[5].RelRateAlarmNumer = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    /*========================================================*/
                    #region CO2报警及分析
                    case 303:	//CO2气体含量注意值，一级报警
                        {
                            als.aa.GasAlarm[6].Level1 = (float)kvp.Value;
                            break;
                        }
                    case 304:	//CO2气体含量注意值，二级报警
                        {
                            als.aa.GasAlarm[6].Level2 = (float)kvp.Value;
                            break;
                        }
                    case 355:	//CO2绝对产气速率注意值ul/天，一级报警
                        {
                            als.aa.GasAlarm[6].AbsLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 356:	//CO2绝对产气速率注意值ul/天，二级报警
                        {
                            als.aa.GasAlarm[6].AbsLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 357:	//CO2相对产气速率注意值%/月，一级报警
                        {
                            als.aa.GasAlarm[6].RelLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 358:	//CO2相对产气速率注意值%/月，二级报警
                        {
                            als.aa.GasAlarm[6].RelLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 359:	//CO2报警门限值触发条件ul/L
                        {
                            als.aa.GasAlarm[6].AlarmContent = (float)kvp.Value;
                            break;
                        }
                    case 360:	//CO2绝对产气速率触发ul/天
                        {
                            als.aa.GasAlarm[6].AbsRateAlarmNumber = (float)kvp.Value;
                            break;
                        }
                    case 361:	//CO2相对产气速率触发%/月
                        {
                            als.aa.GasAlarm[6].RelRateAlarmNumer = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    /*========================================================*/
                    #region 总烃
                    case 305:	//总烃，一级报警
                        {
                            als.aa.thga.Level1 = (float)kvp.Value;
                            break;
                        }
                    case 306:	//总烃，二级报警
                        {
                            als.aa.thga.Level2 = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    /*========================================================*/
                    #region 微水PPM
                    case 307:	//微水（PPM），一级报警
                        {
                            als.aa.pa.Level1 = (float)kvp.Value;
                            break;
                        }
                    case 308:	//微水（PPM），二级报警
                        {
                            als.aa.pa.Level2 = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    /*========================================================*/
                    #region 微水AW
                    case 309:	//微水（AW），一级报警
                        {
                            als.aa.awa.Level1 = (float)kvp.Value;
                            break;
                        }
                    case 310:	//微水（AW），二级报警
                        {
                            als.aa.awa.Level2 = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    /*========================================================*/
                    #region 微水T
                    case 311:	//微水（T），一级报警
                        {
                            als.aa.ta.Level1 = (float)kvp.Value;
                            break;
                        }
                    case 312:	//微水（T），二级报警
                        {
                            als.aa.ta.Level2 = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    /*========================================================*/
                    #region 其他报警及检测
                    case 135:
                        {
                            als.NextSampleTime = TimerTick.TimeSpanToDate((long)kvp.Value);  //下次采样时间
                            break;
                        }
                    case 145:
                        {
                            als.GasPressure = (float)kvp.Value;     //载气压力检测实际值
                            break;
                        }
                    case 11:
                        {
                            als.VacuPres = (float)kvp.Value;       //（脱气机）真空度压力检测值
                            break;
                        }
                    case 13:
                        {
                            als.OilPres = (float)kvp.Value;     //油压检测值
                            break;
                        }
                    case 14:
                        {
                            als.YouBeiLevel = (char)kvp.Value;    //油杯液位状态
                            break;
                        }
                    case 15:
                        {
                            als.QiBeiLevel = (char)kvp.Value;    //气杯液位状态
                            break;
                        }
                    case 16:
                        {
                            als.QiGangForw = (char)kvp.Value;    //气缸进到位
                            break;
                        }
                    case 17:
                        {
                            als.QiGangBackw = (char)kvp.Value;     //气缸退到位
                            break;
                        }
                    case 18:
                        {
                            als.YouGangForw = (char)kvp.Value;   //油缸进到位
                            break;
                        }
                    case 19:
                        {
                            als.YouGangBackw = (char)kvp.Value;   //油缸退到位
                            break;
                        }
                    case 7:
                        {
                            als.TuoQiTimes = (char)kvp.Value;   //脱气次数查询/设置
                            break;
                        }
                    case 8:
                        {
                            als.ChangeTimes = (char)kvp.Value;   //置换次数查询/设置
                            break;
                        }
                    case 62:
                        {
                            als.SensorRoomT = (float)kvp.Value;    //传感器室温度实际采样值
                            break;
                        }
                    case 63:
                        {
                            als.LengJingT = (float)kvp.Value;   //冷井温度实际采样值
                            break;
                        }
                    case 64:
                        {
                            als.SePuZhuT = (float)kvp.Value;   //色谱柱温度实际采样值
                            break;
                        }
                    case 136:
                        {
                            als.SampleInterval = (ushort)kvp.Value; //采样间隔
                            break;
                        }
                    case 154:
                        {
                            als.PLCTime = TimerTick.TimeSpanToDate((long)kvp.Value); //系统时间，下位机
                            break;
                        }
                    #endregion
                    default:
                            {
                                break;
                            }
                }
            }
            try
            {
                return Warehousing(als);
            }
            catch(Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }

        /// <summary>
        /// 重载，用于web端已准备类的数据操作
        /// ！！！异常
        /// </summary>
        /// <param name="als"></param>
        /// <returns></returns>
        public static bool Warehousing(AlarmState als)
        {
            try
            {
                MongoHelper<AlarmState> mhas = new MongoHelper<AlarmState>();
                return mhas.Insert(als);
            }
            catch (Exception ep)
            {
                throw new Exception(ep.Message);
            }
        }
    }
}