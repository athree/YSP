using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMserver.Models;
using IMserver.DBservice;
using IMserver.Models.SimlDefine;

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
            cf.AnalyPara.H2oFix = new H2oFixPara[2];

            cf.AnalyPara.GasFix = new GasFixPara[7];
            for(int i = 0; i < 7; i++)
            {
                cf.AnalyPara.GasFix[0].MultiK = new GasFixK[12];
                cf.AnalyPara.GasFix[0].J = new double[12];
            }
            cf.SampSet = new SampleSetting();
            cf.SampStart = new SampleStartting();
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
                            cf.AnalyPara.EnviSet = (EnvironmentSetting)kvp.Value;           //油品系数A/B（4bytes）
                            break;
                        }
                    //case : 包含在上面的类中
                    //    {
                    //        cf.AnalyPara.EnviSet.oilFactorB = (float)kvp.Value;           //油品系数B（4bytes
                    //        break;
                    //    }
#endregion
                    #region 微水修正参数 AW,T 
                    case 186:
                        {
                            cf.AnalyPara.
                            break;
                        }
                    case 187:
                        {
                            break;
                        }
                    (H2oFixPara[2] H2oFix =  
                    A = (float)kvp.Value;
                    K = (float
                    B = (float
#endregion
                    #region 气体修正参数  H2,CO,CH4,C2H2,C2H4,C2H6,CO2
                    (GasFixPara[7] GasFix{get;set;}        
                    PeakPoint = (ushort)kvp.Value;
                    PeakLeft = (ushort)kvp.Value;
                    PeakRight = (ushort)kvp.Value;
                    LeftTMin = (ushort)kvp.Value;
                    LeftTMax = (ushort)kvp.Value;
                    RightTMin = (ushort)kvp.Value;
                    RightTMax = (ushort)kvp.Value;
                    PeakWidth = (ushort)kvp.Value;
                    (GasFixK[12] MultiK =  //12个K值设置
                    k = (float)kvp.Value;
                    mi = (float)kvp.Value;
                    ni = (float)kvp.Value;                     //K值，柱修正系数，脱气率修正系数
                    areaMin = (float)kvp.Value;               //该K值对应的最小面积
                    areaMax = (float)kvp.Value;               //该K值对应的最大面积
                    (float[12] J =         //离线/在线计算偏差值J，12个
#endregion
                     EraseStart = (ushort)kvp.Value;//剔除区间开始
                     EraseEnd = (ushort)kvp.Value; //剔除区间结尾

                    //采样控制
                    #region 初始参数
                    ChuiSaoBefore_Tick =  (ushort)kvp.Value;              //采样前吹扫阀工作时间 0-3600s
                    (DingLiangWork_Tick =	  	    //定量阀开启后的持续时间，18s	[0-65535](秒)
                    ChuiSaoDelay_Tick =	(ushort)kvp.Value;  	//定量阀打开后，吹扫阀延迟打开的时间，2s [0-65535]	(秒)
                    ChuiSaoAfter_Tick =	(ushort)kvp.Value;  	    //采样结束吹扫阀工作时间  0-3600s
#endregion
                    #region 微水采样
                    H2oDelayStart_Tick = (ushort)kvp.Value;  //微水传感器延时开始加热时刻
                    H2oSampStart_Tick = (ushort)kvp.Value; //微水传感器采样开始时间
                    H2oSampInterval = (ushort)kvp.Value;		//采样间隔,100	[1-255]	(10ms为单位)
                    H2oAwSampNum = (ushort)kvp.Value;		//微水AW的采样点数	[1-10]
                    H2oTSampNum = (ushort)kvp.Value;			//微水T的采样点数	[1-10]
#endregion
                    #region 气体采样控制,六组分+CO2
                    HuiFuBeforeStart = (ushort)kvp.Value;//六组分传感器恢复阀采样前开始时刻
                    HuiFuBeforeWork_Tick = (ushort)kvp.Value;//六组分传感器恢复阀采样前工作时间
                    HuiFuAfterStart = (ushort)kvp.Value;//六组分传感器恢复阀采样后开始时刻
                    HuiFuAfterWork_Tick = (ushort)kvp.Value; //六组分传感器恢复阀采样后工作时间
                    SixGasHeatStart = (ushort)kvp.Value; //六组分传感器加热开始时刻
                    SixGasHeatWork_Tick = (ushort)kvp.Value; //六组分传感器加热工作时间
                    SixGasAfterSamp_Tick = (ushort)kvp.Value; //采样结束六组分传感器加热工作时间
                    SixGasSampInterval = (ushort)kvp.Value; //六组分采样间隔
                    SixGasSampNum = (ushort)kvp.Value; //六组分采样点数
                    CO2HeatStart = (ushort)kvp.Value;  //CO2传感器加热开始时刻
                    CO2HeatWork_Tick = (ushort)kvp.Value;      //CO2传感器工作时间
                    CO2SampInterval = (ushort)kvp.Value; //CO2采样间隔
                    CO2SampNum = (ushort)kvp.Value; //CO2采样点数
                    CO2GasStart = (ushort)kvp.Value; //CO2气路切换开始时刻(以6组气体开始采样为准)
                    CO2GasWork_Tick = (ushort)kvp.Value; //CO2气路工作时间
                    BiaoDingTimes = (char)kvp.Value;   //标定次数  1-5次
#endregion

                    //采样开始控制
                    SampleTime = (DateTime                 //开始采样时间，年月日时分秒  //////下次采样时间
                    interval = (ushort)kvp.Value;                  //采样间隔,2*24*60, 2[3600-65535]
                    aheadTime = (ushort)kvp.Value;                //工作流程提前时间,0, [0-65535]	(秒)

                    //系统设置
                    SoftwareRelease = (string            // 软件版本
                    SuCO2 = (char)kvp.Value;                        //二氧化碳模块，1:支持，0：不支持
                    SuH2O = (char)kvp.Value;                        //微水模块，1:支持，0：不支持
                    TuoQi_Mode = (char)kvp.Value; 		            //0:真空脱气，1:膜脱气,2:顶空脱气

                    //脱气设置
                    #region 真空脱气
                    Cycle_Tick = (ushort)kvp.Value; //循环时间查询/设置
                    EvacuTimes = (char)kvp.Value;  //抽空次数查询/设置
                    CleanTimes = (char)kvp.Value;  //清洗次数查询/设置
                    TuoQiTimes = (char)kvp.Value; //脱气次数查询/设置
                    ChangeTimes = (char)kvp.Value;  //置换次数查询/设置
                    TuoQiEnd_Tick = (ushort)kvp.Value;//脱气机预计脱气完成时间
                    #endregion
                    #region  膜脱气
                    YouBengKeep_Tick = (ushort)kvp.Value;			//油泵持续时间,4*60*60 [0-65535] (秒)
                    PaiQiClen_Tick = (ushort)kvp.Value;       //排气阀清洗时间
                    QiBengClean_Tick = (ushort)kvp.Value;	//气泵清洗时间
                    PaiQiKeep_Tick = (ushort)kvp.Value;			//排气阀持续时间,60 [0-65535] (秒)
                    QiBengKeepOn_Tick =	(ushort)kvp.Value;	//气泵连续工作时间,2*60 [0-65535] (秒)
                    PainQiKeepOff_Tick = (ushort)kvp.Value;       //排气阀间隔停止时间
                    QiBengKeepOff_Tick = (ushort)kvp.Value;		//气泵间隔停止时间,3*60 [0-65535] (秒)
                    #endregion
                    #region 顶空脱气
                    StirStart = (ushort)kvp.Value; //搅拌开始时刻
                    StirWork_Tick = (ushort)kvp.Value;//搅拌工作时间
                    CleanPumpStart = (ushort)kvp.Value;//清洗泵开始工作时刻
                    CleanPumpWork_Time = (ushort)kvp.Value;//清洗泵工作时间
                    ChangeValveStart = (ushort)kvp.Value;//置换阀开始工作时刻
                    ChangeValveWork_Tick = (ushort)kvp.Value;//置换阀工作时间
                    #endregion

                    //检测辅助设置
                    #region 传感器
                    Start = (ushort)kvp.Value;//启动开始时刻
                    Work_Tick = (ushort)kvp.Value;//连续工作时间
                    TempSet = (float)kvp.Value;//温度设置值
                    TempP = (float)kvp.Value;//温度设置P值
                    TempI = (float)kvp.Value;//温度设置I值
                    TempD = (float)kvp.Value;//温度设置D值
                    TempPID = (float)kvp.Value;//温度控制PID范围
                    #endregion
                    #region 冷井
                    Start = (ushort)kvp.Value;//启动开始时刻
                    Work_Tick = (ushort)kvp.Value;//连续工作时间
                    TempSet = (float)kvp.Value;//温度设置值
                    TempP = (float)kvp.Value;//温度设置P值
                    TempI = (float)kvp.Value;//温度设置I值
                    TempD = (float)kvp.Value;//温度设置D值
                    empPID = (float)kvp.Value;T//温度控制PID范围
                    #endregion
                    #region 色谱柱
                    Start = (ushort)kvp.Value;//启动开始时刻
                    Work_Tick = (ushort)kvp.Value;//连续工作时间
                    TempSet = (float)kvp.Value;//温度设置值
                    TempP = (float)kvp.Value;//温度设置P值
                    TempI = (float)kvp.Value;//温度设置I值
                    TempD = (float)kvp.Value;//温度设置D值
                    TempPID = (float)kvp.Value;//温度控制PID范围
                    #endregion

                    //报警及分析配置
                    AlarmAll--报警界面写过
                }
            }
            return false;
        }

        public static bool Warehousing(Config cf)
        {
            MongoHelper<Config> mhas = new MongoHelper<Config>();
            return mhas.Insert(cf);
            return false;
        }
    }
}