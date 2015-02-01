using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMserver.Models;
using IMserver.DBservice;
using IMserver.Models.SimlDefine;
using System.Text;

namespace IMserver.Data_Warehousing
{
    public class AddConfig
    {
        public bool Warehousing(Dictionary<ushort, object> middata, byte devid)
        {
            Config cf = new Config();
            #region 类及内嵌类的初始化
            cf.OutSideSet = new OutSideSetting();
            cf.AnalyPara = new AnalysisParameter();

            cf.AnalyPara.EnviSet = new EnvironmentSetting();
            cf.AnalyPara.EnviSet.oilfactor = new OilFactor();

            cf.AnalyPara.AW = new H2oFixPara_AW();
            cf.AnalyPara.T = new H2oFixPara_T();

            cf.AnalyPara.GasFix = new GasFixPara[6];
            for(int i = 0; i < 6; i++)
            {
                cf.AnalyPara.GasFix[i].p_a = new Para_A();
                cf.AnalyPara.GasFix[i].p_b = new Para_B();
                cf.AnalyPara.GasFix[i].MultiK = new GasFixK[12];
                cf.AnalyPara.GasFix[i].J = new double[12];
            }
            //CO2区别于其他的六种气体
            cf.AnalyPara.GasFix_CO2 = new GasFixPara_CO2();
            cf.AnalyPara.GasFix_CO2.p_a = new Para_A();
            cf.AnalyPara.GasFix_CO2.p_b = new Para_B();
            cf.AnalyPara.GasFix_CO2.MultiK = new GasFixK();
            cf.AnalyPara.er = new EraseRange();

            cf.SampSet = new SampleSetting();
            cf.SysSet = new SystemSetting();
            cf.TQSet = new TuoQiSetting();

            cf.JCFZSet = new JCFZSetting();
            cf.JCFZSet.SensorRoom = new JianCeFuZhu();
            cf.JCFZSet.LengJing = new JianCeFuZhu();
            cf.JCFZSet.SePuZhu = new JianCeFuZhu();

            cf.Alarm = new AlarmAll();
            cf.Alarm.GasAlarm = new GasAlarm[7];
            cf.Alarm.thga = new TotHyd_GasAlarm();
            cf.Alarm.ta = new T_GasAlarm();
            cf.Alarm.pa = new PPM_GasAlarm();
            cf.Alarm.awa = new AW_GasAlarm();
#endregion
            cf.DevID = devid.ToString();
            cf.Time_Stamp = DateTime.Now;

            foreach (KeyValuePair<ushort, object> kvp in middata)
            {
                switch (kvp.Key)
                {
                    //环境及外围设置
#region
                    case 97:
                        {
                            cf.OutSideSet.FengShanKeep_Tick = (ushort)kvp.Value;	//([电柜]风扇持续工作时间)风扇预吹保持时间(以脱气开始时间为准),5*60 [0-65535] (秒)
                            break;
                        }
                    case 98:
                        {
                            cf.OutSideSet.FengShanWork_Tick =	(ushort)kvp.Value;	//（[电柜]风扇停止时间)风扇吹尾保持时间(用于换气),5*60	[0-65535](秒)？？风扇停止时间？？
                            break;
                        }
                    case 99:
                        {
                            cf.OutSideSet.AirControlStart = (ushort)kvp.Value;        //柜体空调采样前开启时刻
                            break;
                        }
                    case 100:
                        {
                            cf.OutSideSet.AirControlWork_Tick = (ushort)kvp.Value;    //柜体空调连续工作时间
                            break;
                        }
                    case 101:
                        {
                            cf.OutSideSet.BanReDaiStart_Tick = (ushort)kvp.Value;     //伴热带采样前开始时刻
                            break;
                        }
                    case 102:
                        {
                            cf.OutSideSet.BanReDaiWork_Tick = (ushort)kvp.Value;      //伴热带采样前工作时间
                            break;
                        }
                    case 103:
                        {
                            cf.OutSideSet.DrainStart = (ushort)kvp.Value;             //载气发生器排水阀启动时刻
                            break;
                        }
                    case 104:
                        {
                            cf.OutSideSet.DrainWork_Tick = (ushort)kvp.Value;         //载气发生器排水阀工作时间
                            break;
                        }
#endregion
                    //分析计算参数
#region
#region  环境设置,海拔、油压等
                    case 184:
                        {
                            cf.AnalyPara.EnviSet.altitude = (ushort)kvp.Value;            //海拔高度（2bytes）：米
                            break;
                        }
                    case 182:
                        {
                            cf.AnalyPara.EnviSet.voltage = (float)kvp.Value;             //电压等级（2bytes）
                            break;
                        }
                    case 181:
                        {
                            cf.AnalyPara.EnviSet.oilTotal = (float)kvp.Value;          //油总量，油重（2bytes）
                            break;
                        }
                    case 180:
                        {
                            cf.AnalyPara.EnviSet.oilDensity = (float)kvp.Value;          //油密度（2bytes）
                            break;
                        }
                    case 183:
                        {
                            cf.AnalyPara.EnviSet.oilfactor = (OilFactor)kvp.Value;           //油品系数A/B（4bytes）
                            break;
                        }
                    //case : 包含在上面的类中
                    //    {
                    //        cf.AnalyPara.EnviSet.oilFactorB = (float)kvp.Value;           //油品系数B（4bytes
                    //        break;
                    //    }
#endregion
#region 微水修正参数 AW,T 
                    case 186:            //微水修正参数 AW
                        {
                            cf.AnalyPara.AW = (H2oFixPara_AW)kvp.Value;
                            break;
                        }
                    case 187:            //微水修正参数 T
                        {
                            cf.AnalyPara.T= (H2oFixPara_T)kvp.Value;
                            break;
                        }
#endregion
#region 气体修正参数  H2,CO,CH4,C2H2,C2H4,C2H6,CO2
#region  //H2
                    case 189: 
                        {
                            cf.AnalyPara.GasFix[0].p_a = (Para_A)kvp.Value;
                            break;
                        }
                    case 190:
                        {
                            cf.AnalyPara.GasFix[0].p_b = (Para_B)kvp.Value;
                            break;
                        }
                    case 191:
                        {
                            cf.AnalyPara.GasFix[0].MultiK[0] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 192:
                        {
                            cf.AnalyPara.GasFix[0].MultiK[1] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 193:
                        {
                            cf.AnalyPara.GasFix[0].MultiK[2] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 194:
                        {
                            cf.AnalyPara.GasFix[0].MultiK[3] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 195:
                        {
                            cf.AnalyPara.GasFix[0].MultiK[4] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 196:
                        {
                            cf.AnalyPara.GasFix[0].MultiK[5] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 197:
                        {
                            cf.AnalyPara.GasFix[0].MultiK[6] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 198:
                        {
                            cf.AnalyPara.GasFix[0].MultiK[7] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 199:
                        {
                            cf.AnalyPara.GasFix[0].MultiK[8] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 200:
                        {
                            cf.AnalyPara.GasFix[0].MultiK[9] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 201:
                        {
                            cf.AnalyPara.GasFix[0].MultiK[10] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 202:
                        {
                            cf.AnalyPara.GasFix[0].MultiK[11] = (GasFixK)kvp.Value;
                            break;
                        }
                    //cf.AnalyPara.GasFix[0].J  不在传输的范围这里不做注释
#endregion
#region //CO
                    case 204: 
                        {
                            cf.AnalyPara.GasFix[1].p_a = (Para_A)kvp.Value;
                            break;
                        }
                    case 205:
                        {
                            cf.AnalyPara.GasFix[1].p_b = (Para_B)kvp.Value;
                            break;
                        }
                    case 206:
                        {
                            cf.AnalyPara.GasFix[1].MultiK[0] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 207:
                        {
                            cf.AnalyPara.GasFix[1].MultiK[1] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 208:
                        {
                            cf.AnalyPara.GasFix[1].MultiK[2] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 209:
                        {
                            cf.AnalyPara.GasFix[1].MultiK[3] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 210:
                        {
                            cf.AnalyPara.GasFix[1].MultiK[4] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 211:
                        {
                            cf.AnalyPara.GasFix[1].MultiK[5] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 212:
                        {
                            cf.AnalyPara.GasFix[1].MultiK[6] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 213:
                        {
                            cf.AnalyPara.GasFix[1].MultiK[7] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 214:
                        {
                            cf.AnalyPara.GasFix[1].MultiK[8] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 215:
                        {
                            cf.AnalyPara.GasFix[1].MultiK[9] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 216:
                        {
                            cf.AnalyPara.GasFix[1].MultiK[10] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 217:
                        {
                            cf.AnalyPara.GasFix[1].MultiK[11] = (GasFixK)kvp.Value;
                            break;
                        }
                    //cf.AnalyPara.GasFix[0].J  不在传输的范围这里不做注释
#endregion
#region //CH4
                    case 219: 
                        {
                            cf.AnalyPara.GasFix[2].p_a = (Para_A)kvp.Value;
                            break;
                        }
                    case 220:
                        {
                            cf.AnalyPara.GasFix[2].p_b = (Para_B)kvp.Value;
                            break;
                        }
                    case 221:
                        {
                            cf.AnalyPara.GasFix[2].MultiK[0] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 222:
                        {
                            cf.AnalyPara.GasFix[2].MultiK[1] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 223:
                        {
                            cf.AnalyPara.GasFix[2].MultiK[2] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 224:
                        {
                            cf.AnalyPara.GasFix[2].MultiK[3] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 225:
                        {
                            cf.AnalyPara.GasFix[2].MultiK[4] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 226:
                        {
                            cf.AnalyPara.GasFix[2].MultiK[5] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 227:
                        {
                            cf.AnalyPara.GasFix[2].MultiK[6] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 228:
                        {
                            cf.AnalyPara.GasFix[2].MultiK[7] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 229:
                        {
                            cf.AnalyPara.GasFix[2].MultiK[8] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 230:
                        {
                            cf.AnalyPara.GasFix[2].MultiK[9] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 231:
                        {
                            cf.AnalyPara.GasFix[2].MultiK[10] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 232:
                        {
                            cf.AnalyPara.GasFix[2].MultiK[11] = (GasFixK)kvp.Value;
                            break;
                        }
                    //cf.AnalyPara.GasFix[0].J  不在传输的范围这里不做注释
#endregion
#region //C2H2
                    case 234:  
                        {
                            cf.AnalyPara.GasFix[3].p_a = (Para_A)kvp.Value;
                            break;
                        }
                    case 235:
                        {
                            cf.AnalyPara.GasFix[3].p_b = (Para_B)kvp.Value;
                            break;
                        }
                    case 236:
                        {
                            cf.AnalyPara.GasFix[3].MultiK[0] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 237:
                        {
                            cf.AnalyPara.GasFix[3].MultiK[1] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 238:
                        {
                            cf.AnalyPara.GasFix[3].MultiK[2] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 239:
                        {
                            cf.AnalyPara.GasFix[3].MultiK[3] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 240:
                        {
                            cf.AnalyPara.GasFix[3].MultiK[4] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 241:
                        {
                            cf.AnalyPara.GasFix[3].MultiK[5] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 242:
                        {
                            cf.AnalyPara.GasFix[3].MultiK[6] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 243:
                        {
                            cf.AnalyPara.GasFix[3].MultiK[7] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 244:
                        {
                            cf.AnalyPara.GasFix[3].MultiK[8] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 245:
                        {
                            cf.AnalyPara.GasFix[3].MultiK[9] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 246:
                        {
                            cf.AnalyPara.GasFix[3].MultiK[10] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 247:
                        {
                            cf.AnalyPara.GasFix[3].MultiK[11] = (GasFixK)kvp.Value;
                            break;
                        }
                    //cf.AnalyPara.GasFix[0].J  不在传输的范围这里不做注释
#endregion
#region //C2H4
                    case 249:  
                        {
                            cf.AnalyPara.GasFix[4].p_a = (Para_A)kvp.Value;
                            break;
                        }
                    case 250:
                        {
                            cf.AnalyPara.GasFix[4].p_b = (Para_B)kvp.Value;
                            break;
                        }
                    case 251:
                        {
                            cf.AnalyPara.GasFix[4].MultiK[0] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 252:
                        {
                            cf.AnalyPara.GasFix[4].MultiK[1] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 253:
                        {
                            cf.AnalyPara.GasFix[4].MultiK[2] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 254:
                        {
                            cf.AnalyPara.GasFix[4].MultiK[3] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 255:
                        {
                            cf.AnalyPara.GasFix[4].MultiK[4] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 256:
                        {
                            cf.AnalyPara.GasFix[4].MultiK[5] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 257:
                        {
                            cf.AnalyPara.GasFix[4].MultiK[6] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 258:
                        {
                            cf.AnalyPara.GasFix[4].MultiK[7] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 259:
                        {
                            cf.AnalyPara.GasFix[4].MultiK[8] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 260:
                        {
                            cf.AnalyPara.GasFix[4].MultiK[9] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 261:
                        {
                            cf.AnalyPara.GasFix[4].MultiK[10] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 262:
                        {
                            cf.AnalyPara.GasFix[4].MultiK[11] = (GasFixK)kvp.Value;
                            break;
                        }
                    //cf.AnalyPara.GasFix[0].J  不在传输的范围这里不做注释
#endregion
#region //C2H6
                    case 264:  
                        {
                            cf.AnalyPara.GasFix[5].p_a = (Para_A)kvp.Value;
                            break;
                        }
                    case 265:
                        {
                            cf.AnalyPara.GasFix[5].p_b = (Para_B)kvp.Value;
                            break;
                        }
                    case 266:
                        {
                            cf.AnalyPara.GasFix[5].MultiK[0] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 267:
                        {
                            cf.AnalyPara.GasFix[5].MultiK[1] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 268:
                        {
                            cf.AnalyPara.GasFix[5].MultiK[2] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 269:
                        {
                            cf.AnalyPara.GasFix[5].MultiK[3] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 270:
                        {
                            cf.AnalyPara.GasFix[5].MultiK[4] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 271:
                        {
                            cf.AnalyPara.GasFix[5].MultiK[5] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 272:
                        {
                            cf.AnalyPara.GasFix[5].MultiK[6] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 273:
                        {
                            cf.AnalyPara.GasFix[5].MultiK[7] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 274:
                        {
                            cf.AnalyPara.GasFix[5].MultiK[8] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 275:
                        {
                            cf.AnalyPara.GasFix[5].MultiK[9] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 276:
                        {
                            cf.AnalyPara.GasFix[5].MultiK[10] = (GasFixK)kvp.Value;
                            break;
                        }
                    case 277:
                        {
                            cf.AnalyPara.GasFix[5].MultiK[11] = (GasFixK)kvp.Value;
                            break;
                        }
                    //cf.AnalyPara.GasFix[0].J  不在传输的范围这里不做注释
#endregion
#region  //CO2
                    case 281:  
                        {
                            cf.AnalyPara.GasFix_CO2.p_a = (Para_A)kvp.Value;
                            break;
                        }
                    case 282:
                        {
                            cf.AnalyPara.GasFix_CO2.p_b = (Para_B)kvp.Value;
                            break;
                        }     
                    case 283:
                        {
                            cf.AnalyPara.GasFix_CO2.MultiK = (GasFixK)kvp.Value;
                            break;
                        }
                    //cf.AnalyPara.GasFix[0].J  不在传输的范围这里不做注释
#endregion
                    //PeakPoint = (ushort)kvp.Value;          //峰顶点位置
                    //PeakLeft = (ushort)kvp.Value;           //峰顶范围起点
                    //PeakRight = (ushort)kvp.Value;          //峰顶范围结束点
                    //PeakWidth = (ushort)kvp.Value;          //峰顶宽度

                    //LeftTMin = (ushort)kvp.Value;           //左梯度Min
                    //LeftTMax = (ushort)kvp.Value;           //左梯度Max
                    //RightTMin = (ushort)kvp.Value;          //右梯度Min
                    //RightTMax = (ushort)kvp.Value;          //右梯度Max
#endregion
                    case 279:
                        {
                            cf.AnalyPara.er = (EraseRange)kvp.Value;
                            break;
                        }
#endregion
                    //采样控制
#region
#region 初始参数
                    case 107:
                        {
                            cf.SampSet.ChuiSaoBefore_Tick =  (ushort)kvp.Value;     //采样前吹扫阀工作时间 0-3600s
                            break;
                        }
                    case 108:
                        {
                            cf.SampSet.DingLiangWork_Tick =	(ushort)kvp.Value;  	//定量阀开启后的持续时间，18s	[0-65535](秒)
                            break;
                        }
                    case 109:
                        {
                            cf.SampSet.ChuiSaoDelay_Tick =	(ushort)kvp.Value;  	//定量阀打开后，吹扫阀延迟打开的时间，2s [0-65535]	(秒)
                            break;
                        }
                    case 110:
                        {
                            cf.SampSet.ChuiSaoAfter_Tick =	(ushort)kvp.Value;  	//采样结束吹扫阀工作时间  0-3600s
                            break;
                        }
#endregion
#region 微水采样
                    case 115:
                        {
                            cf.SampSet.H2oDelayStart_Tick = (ushort)kvp.Value;  //微水传感器延时开始加热时刻
                             break;
                        }
                    case 116:
                        {
                            cf.SampSet.H2oSampStart_Tick = (ushort)kvp.Value; //微水传感器采样开始时间
                             break;
                        }
                    case 117:
                        {
                            cf.SampSet.H2oSampInterval = (ushort)kvp.Value;		//采样间隔,100	[1-255]	(10ms为单位)
                             break;
                        }
                    case 118:
                        {
                            cf.SampSet.H2oAwSampNum = (ushort)kvp.Value;		//微水AW的采样点数	[1-10]
                             break;
                        }
                    case 119:
                        {
                            cf.SampSet.H2oTSampNum = (ushort)kvp.Value;			//微水T的采样点数	[1-10]
                             break;
                        }
#endregion
#region 气体采样控制,六组分+CO2
                    case 111:
                        {
                             cf.SampSet.HuiFuBeforeStart = (ushort)kvp.Value;//六组分传感器恢复阀采样前开始时刻
                             break;
                        }
                    case 112:
                        {
                             cf.SampSet.HuiFuBeforeWork_Tick = (ushort)kvp.Value;//六组分传感器恢复阀采样前工作时间
                             break;
                        }
                    case 113:
                        {
                            cf.SampSet.HuiFuAfterStart = (ushort)kvp.Value;//六组分传感器恢复阀采样后开始时刻
                            break;
                        }
                    case 114:
                        {
                            cf.SampSet.HuiFuAfterWork_Tick = (ushort)kvp.Value; //六组分传感器恢复阀采样后工作时间
                            break;
                        }
                    case 120:
                        {
                            cf.SampSet.SixGasHeatStart = (ushort)kvp.Value; //六组分传感器加热开始时刻 
                            break;
                        }
                    case 121:
                        {
                            cf.SampSet.SixGasHeatWork_Tick = (ushort)kvp.Value; //六组分传感器加热工作时间
                            break;
                        }
                    case 122:
                        {
                            cf.SampSet.SixGasAfterSamp_Tick = (ushort)kvp.Value; //采样结束六组分传感器加热工作时间
                            break;
                        }
                    case 123:
                        {
                            cf.SampSet.SixGasSampInterval = (ushort)kvp.Value; //六组分采样间隔
                            break;
                        }
                    case 124:
                        {
                            cf.SampSet.SixGasSampNum = (ushort)kvp.Value; //六组分采样点数
                            break;
                        }
                    case 125:
                        {
                            cf.SampSet.CO2HeatStart = (ushort)kvp.Value;  //CO2传感器加热开始时刻 
                            break;
                        }
                    case 126:
                        {
                            cf.SampSet.CO2HeatWork_Tick = (ushort)kvp.Value;      //CO2传感器工作时间
                            break;
                        }
                    case 127:
                        {
                            cf.SampSet.CO2SampInterval = (ushort)kvp.Value; //CO2采样间隔
                            break;
                        }
                    case 128:
                        {
                            cf.SampSet.CO2SampNum = (ushort)kvp.Value; //CO2采样点数
                            break;
                        }
                    case 129:
                        {
                            cf.SampSet.CO2GasStart = (ushort)kvp.Value; //CO2气路切换开始时刻(以6组气体开始采样为准)
                            break;
                        }
                    case 130:
                        {
                            cf.SampSet.CO2GasWork_Tick = (ushort)kvp.Value; //CO2气路工作时间
                            break;
                        }
                    case 131:
                        {
                            cf.SampSet.BiaoDingTimes = (char)kvp.Value;   //标定次数  1-5次
                            break;
                        }
#endregion
#endregion
                    //采样开始控制 --已经删除
#region
                    //case 135:
                    //    {
                    //        cf.SampStart.SampleTime = TimerTick.TimeSpanToDate((long)kvp.Value); //开始采样时间，年月日时分秒\/下次采样时间
                    //        break;
                    //    }
                    //case 136:
                    //    {
                    //        cf.SampStart.interval = (ushort)kvp.Value;                  //采样间隔,2*24*60, 2[3600-65535]
                    //        break;
                    //    }
                    //case ?:
                    //    {
                    //        cf.SampStart.aheadTime = (ushort)kvp.Value;                //工作流程提前时间,0, [0-65535]	(秒)
                    //        break;
                    //    }
#endregion
                    //系统设置
#region
                    case 149:
                        {
                            cf.SysSet.SoftwareRelease = Encoding.ASCII.GetString((byte[])kvp.Value);    // 软件版本
                            break;
                        }
                    case 150:
                        {
                            cf.SysSet.SuCO2 = (char)kvp.Value;                        //二氧化碳模块，1:支持，0：不支持
                            break;
                        }
                    case 151:
                        {
                            cf.SysSet.SuH2O = (char)kvp.Value;                        //微水模块，1:支持，0：不支持
                            break;
                        }
                    case 152:
                        {
                            cf.SysSet.TuoQi_Mode = (char)kvp.Value; 		            //0:真空脱气，1:膜脱气,2:顶空脱气
                            break;
                        }
#endregion
                    //脱气设置
#region
                    #region 真空脱气
                    case 4:
                        {
                            cf.TQSet.Cycle_Tick = (ushort)kvp.Value; //循环时间查询/设置
                            break;
                        }
                    case 5:
                        {
                            cf.TQSet.EvacuTimes = (char)kvp.Value;  //抽空次数查询/设置
                            break;
                        }
                    case 6:
                        {
                            cf.TQSet.CleanTimes = (char)kvp.Value;  //清洗次数查询/设置
                            break;
                        }
                    case 7:
                        {
                            cf.TQSet.TuoQiTimes = (char)kvp.Value; //脱气次数查询/设置
                            break;
                        }
                    case 8:
                        {
                            cf.TQSet.ChangeTimes = (char)kvp.Value;  //置换次数查询/设置
                            break;
                        }
                    case 9:
                        {
                            cf.TQSet.TuoQiEnd_Tick = (ushort)kvp.Value;//脱气机预计脱气完成时间
                            break;
                        }
                    #endregion
                    #region  膜脱气
                    case 36:
                        {
                            cf.TQSet.YouBengKeep_Tick = (ushort)kvp.Value;			//油泵持续时间,4*60*60 [0-65535] (秒)
                            break;
                        }
                    case 37:
                        {
                            cf.TQSet.PaiQiClen_Tick = (ushort)kvp.Value;       //排气阀清洗时间
                            break;
                        }
                    case 38:
                        {
                            cf.TQSet.QiBengClean_Tick = (ushort)kvp.Value;	//气泵清洗时间
                            break;
                        }
                    case 39:
                        {
                            cf.TQSet.PaiQiKeep_Tick = (ushort)kvp.Value;			//排气阀持续时间,60 [0-65535] (秒)
                            break;
                        }
                    case 40:
                        {
                            cf.TQSet.QiBengKeepOn_Tick =	(ushort)kvp.Value;	//气泵连续工作时间,2*60 [0-65535] (秒)
                            break;
                        }
                    case 41:
                        {
                            cf.TQSet.PainQiKeepOff_Tick = (ushort)kvp.Value;       //排气阀间隔停止时间
                            break;
                        }
                    case 42:
                        {
                            cf.TQSet.QiBengKeepOff_Tick = (ushort)kvp.Value;		//气泵间隔停止时间,3*60 [0-65535] (秒)
                            break;
                        }
                    #endregion
                    #region 顶空脱气
                    case 51:
                        {
                            cf.TQSet.StirStart = (ushort)kvp.Value; //搅拌开始时刻
                            break;
                        }
                    case 52:
                        {
                            cf.TQSet.StirWork_Tick = (ushort)kvp.Value;//搅拌工作时间
                            break;
                        }
                    case 53:
                        {
                            cf.TQSet.CleanPumpStart = (ushort)kvp.Value;//清洗泵开始工作时刻
                            break;
                        }
                   case 54:
                        {
                            cf.TQSet.CleanPumpWork_Time = (ushort)kvp.Value;//清洗泵工作时间
                            break;
                        }
                    case 55:
                        {
                            cf.TQSet.ChangeValveStart = (ushort)kvp.Value;//置换阀开始工作时刻
                            break;
                        }
                    case 56:
                        {
                            cf.TQSet.ChangeValveWork_Tick = (ushort)kvp.Value;//置换阀工作时间
                            break;
                        }
                    #endregion
#endregion
                    //检测辅助设置
#region
                    #region 传感器
                    case 78:
                        {
                            cf.JCFZSet.SensorRoom.Start = (ushort)kvp.Value;//启动开始时刻
                            break;
                        }
                    case 79:
                        {
                            cf.JCFZSet.SensorRoom.Work_Tick = (ushort)kvp.Value;//连续工作时间
                            break;
                        }
                    case 80:
                        {
                            cf.JCFZSet.SensorRoom.TempSet = (float)kvp.Value;//温度设置值
                            break;
                        }
                    case 81:
                        {
                            cf.JCFZSet.SensorRoom.TempP = (float)kvp.Value;//温度设置P值
                            break;
                        }
                    case 82:
                        {
                            cf.JCFZSet.SensorRoom.TempI = (float)kvp.Value;//温度设置I值
                            break;
                        }
                    case 83:
                        {
                            cf.JCFZSet.SensorRoom.TempD = (float)kvp.Value;//温度设置D值
                            break;
                        }
                    case 84:
                        {
                            cf.JCFZSet.SensorRoom.TempPID = (float)kvp.Value;//温度控制PID范围
                            break;
                        }
                    #endregion
                    #region 冷井
                    case 66:
                        {
                            cf.JCFZSet.LengJing.Start = (ushort)kvp.Value;//启动开始时刻
                            break;
                        }
                    case 67:
                        {
                            cf.JCFZSet.LengJing.Work_Tick = (ushort)kvp.Value;//连续工作时间
                            break;
                        }
                    case 68:
                        {
                            cf.JCFZSet.LengJing.TempSet = (float)kvp.Value;//温度设置值
                            break;
                        }
                    case 69:
                        {
                            cf.JCFZSet.LengJing.TempP = (float)kvp.Value;//温度设置P值
                            break;
                        }
                    case 70:
                        {
                            cf.JCFZSet.LengJing.TempI = (float)kvp.Value;//温度设置I值
                            break;
                        }
                    case 71:
                        {
                            cf.JCFZSet.LengJing.TempD = (float)kvp.Value;//温度设置D值
                            break;
                        }
                    case 72:
                        {
                            cf.JCFZSet.LengJing.TempPID = (float)kvp.Value;T//温度控制PID范围
                            break;
                        }
                    #endregion
                    #region 色谱柱   --   这里色谱柱比较于前两个缺少开始连个属性，不知是否为手误，所以仍用全类，确实少就空着不用
                    //case :
                    //    {
                    //        cf.JCFZSet.SePuZhu.Start = (ushort)kvp.Value;//启动开始时刻
                    //        break;
                    //    }
                    //case :
                    //    {
                    //        cf.JCFZSet.SePuZhu.Work_Tick = (ushort)kvp.Value;//连续工作时间
                    //        break;
                    //    }
                    case 73:
                        {
                            cf.JCFZSet.SePuZhu.TempSet = (float)kvp.Value;//温度设置值
                            break;
                        }
                    case 74:
                        {
                            cf.JCFZSet.SePuZhu.TempP = (float)kvp.Value;//温度设置P值
                            break;
                        }
                    case 75:
                        {
                            cf.JCFZSet.SePuZhu.TempI = (float)kvp.Value;//温度设置I值
                            break;
                        }
                    case 76:
                        {
                            cf.JCFZSet.SePuZhu.TempD = (float)kvp.Value;//温度设置D值
                            break;
                        }
                    case 77:
                        {
                            cf.JCFZSet.SePuZhu.TempPID = (float)kvp.Value;//温度控制PID范围
                            break;
                        }
                    #endregion
#endregion
                    //报警及分析配置
#region
                    case 286:
                        {
                            cf.Alarm.AutoAlarm = kvp.Value.ToString();
                            break;
                        }
                    case 287:
                        {
                            cf.Alarm.AutoDiagnose = kvp.Value.ToString();
                            break;
                        }
                    case 288:
                        {
                            cf.Alarm.Interval = (ushort)kvp.Value;
                            break;
                        }
                    #region H2报警及分析
                    case 291:	//H2气体含量注意值，一级报警
                        {
                            cf.Alarm.GasAlarm[0].Level1 = (float)kvp.Value;
                            break;
                        }
                    case 292:	//H2气体含量注意值，二级报警
                        {
                            cf.Alarm.GasAlarm[0].Level2 = (float)kvp.Value;
                            break;
                        }
                    case 313:	//H2绝对产气速率注意值ul/天，一级报警
                        {
                            cf.Alarm.GasAlarm[0].AbsLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 314:	//H2绝对产气速率注意值ul/天，二级报警
                        {
                            cf.Alarm.GasAlarm[0].AbsLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 315:	//H2相对产气速率注意值%/月，一级报警
                        {
                            cf.Alarm.GasAlarm[0].RelLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 316:	//H2相对产气速率注意值%/月，二级报警
                        {
                            cf.Alarm.GasAlarm[0].RelLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 317:	//H2报警门限值触发条件ul/L
                        {
                            cf.Alarm.GasAlarm[0].AlarmContent = (float)kvp.Value;
                            break;
                        }
                    case 318:	//H2绝对产气速率触发ul/天
                        {
                            cf.Alarm.GasAlarm[0].AbsRateAlarmNumber = (float)kvp.Value;
                            break;
                        }
                    case 319:	//H2相对产气速率触发%/月
                        {
                            cf.Alarm.GasAlarm[0].RelRateAlarmNumer = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    #region CO报警及分析
                    case 293:	//CO气体含量注意值，一级报警
                        {
                            cf.Alarm.GasAlarm[1].Level1 = (float)kvp.Value;
                            break;
                        }
                    case 294:	//CO气体含量注意值，二级报警
                        {
                            cf.Alarm.GasAlarm[1].Level2 = (float)kvp.Value;
                            break;
                        }
                    case 320:	//CO绝对产气速率注意值ul/天，一级报警
                        {
                            cf.Alarm.GasAlarm[1].AbsLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 321:	//CO绝对产气速率注意值ul/天，二级报警
                        {
                            cf.Alarm.GasAlarm[1].AbsLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 322:	//CO相对产气速率注意值%/月，一级报警
                        {
                            cf.Alarm.GasAlarm[1].RelLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 323:	//CO相对产气速率注意值%/月，二级报警
                        {
                            cf.Alarm.GasAlarm[1].RelLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 324:	//CO报警门限值触发条件ul/L
                        {
                            cf.Alarm.GasAlarm[1].AlarmContent = (float)kvp.Value;
                            break;
                        }
                    case 325:	//CO绝对产气速率触发ul/天
                        {
                            cf.Alarm.GasAlarm[1].AbsRateAlarmNumber = (float)kvp.Value;
                            break;
                        }
                    case 326:	//CO相对产气速率触发%/月
                        {
                            cf.Alarm.GasAlarm[1].RelRateAlarmNumer = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    #region CH4报警及分析
                    case 295:	//CH4气体含量注意值，一级报警
                        {
                            cf.Alarm.GasAlarm[2].Level1 = (float)kvp.Value;
                            break;
                        }
                    case 296:	//CH4气体含量注意值，二级报警
                        {
                            cf.Alarm.GasAlarm[2].Level2 = (float)kvp.Value;
                            break;
                        }
                    case 327:	//CH4绝对产气速率注意值ul/天，一级报警
                        {
                            cf.Alarm.GasAlarm[2].AbsLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 328:	//CH4绝对产气速率注意值ul/天，二级报警
                        {
                            cf.Alarm.GasAlarm[2].AbsLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 329:	//CH4相对产气速率注意值%/月，一级报警
                        {
                            cf.Alarm.GasAlarm[2].RelLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 330:	//CH4相对产气速率注意值%/月，二级报警
                        {
                            cf.Alarm.GasAlarm[2].RelLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 331:	//CH4报警门限值触发条件ul/L
                        {
                            cf.Alarm.GasAlarm[2].AlarmContent = (float)kvp.Value;
                            break;
                        }
                    case 332:	//CH4绝对产气速率触发ul/天
                        {
                            cf.Alarm.GasAlarm[2].AbsRateAlarmNumber = (float)kvp.Value;
                            break;
                        }
                    case 333:	//CH4相对产气速率触发%/月
                        {
                            cf.Alarm.GasAlarm[2].RelRateAlarmNumer = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    #region C2H2报警及分析
                    case 301:	//C2H2气体含量注意值，一级报警
                        {
                            cf.Alarm.GasAlarm[3].Level1 = (float)kvp.Value;
                            break;
                        }
                    case 302:	//C2H2气体含量注意值，二级报警
                        {
                            cf.Alarm.GasAlarm[3].Level2 = (float)kvp.Value;
                            break;
                        }
                    case 334:	//C2H2绝对产气速率注意值ul/天，一级报警
                        {
                            cf.Alarm.GasAlarm[3].AbsLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 335:	//C2H2绝对产气速率注意值ul/天，二级报警
                        {
                            cf.Alarm.GasAlarm[3].AbsLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 336:	//C2H2相对产气速率注意值%/月，一级报警
                        {
                            cf.Alarm.GasAlarm[3].RelLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 337:	//C2H2相对产气速率注意值%/月，二级报警
                        {
                            cf.Alarm.GasAlarm[3].RelLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 338:	//C2H2报警门限值触发条件ul/L
                        {
                            cf.Alarm.GasAlarm[3].AlarmContent = (float)kvp.Value;
                            break;
                        }
                    case 339:	//C2H2绝对产气速率触发ul/天
                        {
                            cf.Alarm.GasAlarm[3].AbsRateAlarmNumber = (float)kvp.Value;
                            break;
                        }
                    case 340:	//C2H2相对产气速率触发%/月
                        {
                            cf.Alarm.GasAlarm[3].RelRateAlarmNumer = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    #region C2H4报警及分析
                    case 297:	//C2H4气体含量注意值，一级报警
                        {
                            cf.Alarm.GasAlarm[4].Level1 = (float)kvp.Value;
                            break;
                        }
                    case 298:	//C2H4气体含量注意值，二级报警
                        {
                            cf.Alarm.GasAlarm[4].Level2 = (float)kvp.Value;
                            break;
                        }
                    case 341:	//C2H4绝对产气速率注意值ul/天，一级报警
                        {
                            cf.Alarm.GasAlarm[4].AbsLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 342:	//C2H4绝对产气速率注意值ul/天，二级报警
                        {
                            cf.Alarm.GasAlarm[4].AbsLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 343:	//C2H4相对产气速率注意值%/月，一级报警
                        {
                            cf.Alarm.GasAlarm[4].RelLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 344:	//C2H4相对产气速率注意值%/月，二级报警
                        {
                            cf.Alarm.GasAlarm[4].RelLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 345:	//C2H4报警门限值触发条件ul/L
                        {
                            cf.Alarm.GasAlarm[4].AlarmContent = (float)kvp.Value;
                            break;
                        }
                    case 346:	//C2H4绝对产气速率触发ul/天
                        {
                            cf.Alarm.GasAlarm[4].AbsRateAlarmNumber = (float)kvp.Value;
                            break;
                        }
                    case 347:	//C2H4相对产气速率触发%/月
                        {
                            cf.Alarm.GasAlarm[4].RelRateAlarmNumer = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    #region C2H6报警及分析
                    case 299:	//C2H6气体含量注意值，一级报警
                        {
                            cf.Alarm.GasAlarm[5].Level1 = (float)kvp.Value;
                            break;
                        }
                    case 300:	//C2H6气体含量注意值，二级报警
                        {
                            cf.Alarm.GasAlarm[5].Level2 = (float)kvp.Value;
                            break;
                        }
                    case 348:	//C2H6绝对产气速率注意值ul/天，一级报警
                        {
                            cf.Alarm.GasAlarm[5].AbsLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 349:	//C2H6绝对产气速率注意值ul/天，二级报警
                        {
                            cf.Alarm.GasAlarm[5].AbsLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 350:	//C2H6相对产气速率注意值%/月，一级报警
                        {
                            cf.Alarm.GasAlarm[5].RelLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 351:	//C2H6相对产气速率注意值%/月，二级报警
                        {
                            cf.Alarm.GasAlarm[5].RelLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 352:	//C2H6报警门限值触发条件ul/L
                        {
                            cf.Alarm.GasAlarm[5].AlarmContent = (float)kvp.Value;
                            break;
                        }
                    case 353:	//C2H6绝对产气速率触发ul/天
                        {
                            cf.Alarm.GasAlarm[5].AbsRateAlarmNumber = (float)kvp.Value;
                            break;
                        }
                    case 354:	//C2H6相对产气速率触发%/月
                        {
                            cf.Alarm.GasAlarm[5].RelRateAlarmNumer = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    #region CO2报警及分析
                    case 303:	//CO2气体含量注意值，一级报警
                        {
                            cf.Alarm.GasAlarm[6].Level1 = (float)kvp.Value;
                            break;
                        }
                    case 304:	//CO2气体含量注意值，二级报警
                        {
                            cf.Alarm.GasAlarm[6].Level2 = (float)kvp.Value;
                            break;
                        }
                    case 355:	//CO2绝对产气速率注意值ul/天，一级报警
                        {
                            cf.Alarm.GasAlarm[6].AbsLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 356:	//CO2绝对产气速率注意值ul/天，二级报警
                        {
                            cf.Alarm.GasAlarm[6].AbsLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 357:	//CO2相对产气速率注意值%/月，一级报警
                        {
                            cf.Alarm.GasAlarm[6].RelLevel1 = (float)kvp.Value;
                            break;
                        }
                    case 358:	//CO2相对产气速率注意值%/月，二级报警
                        {
                            cf.Alarm.GasAlarm[6].RelLevel2 = (float)kvp.Value;
                            break;
                        }
                    case 359:	//CO2报警门限值触发条件ul/L
                        {
                            cf.Alarm.GasAlarm[6].AlarmContent = (float)kvp.Value;
                            break;
                        }
                    case 360:	//CO2绝对产气速率触发ul/天
                        {
                            cf.Alarm.GasAlarm[6].AbsRateAlarmNumber = (float)kvp.Value;
                            break;
                        }
                    case 361:	//CO2相对产气速率触发%/月
                        {
                            cf.Alarm.GasAlarm[6].RelRateAlarmNumer = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    #region 总烃
                    case 305:	//总烃，一级报警
                        {
                            cf.Alarm.thga.Level1 = (float)kvp.Value;
                            break;
                        }
                    case 306:	//总烃，二级报警
                        {
                            cf.Alarm.thga.Level2 = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    #region 微水PPM
                    case 307:	//微水（PPM），一级报警
                        {
                            cf.Alarm.pa.Level1 = (float)kvp.Value;
                            break;
                        }
                    case 308:	//微水（PPM），二级报警
                        {
                            cf.Alarm.pa.Level2 = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    #region 微水AW
                    case 309:	//微水（AW），一级报警
                        {
                            cf.Alarm.awa.Level1 = (float)kvp.Value;
                            break;
                        }
                    case 310:	//微水（AW），二级报警
                        {
                            cf.Alarm.awa.Level2 = (float)kvp.Value;
                            break;
                        }
                    #endregion
                    #region 微水T
                    case 311:	//微水（T），一级报警
                        {
                            cf.Alarm.ta.Level1 = (float)kvp.Value;
                            break;
                        }
                    case 312:	//微水（T），二级报警
                        {
                            cf.Alarm.ta.Level2 = (float)kvp.Value;
                            break;
                        }
                    #endregion
#endregion
                    default:
                        {
                            break;
                        }
                }
            }
            return Warehousing(cf);
        }

        public static bool Warehousing(Config cf)
        {
            MongoHelper<Config> mhas = new MongoHelper<Config>();
            return mhas.Insert(cf);
        }
    }
}