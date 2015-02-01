using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMserver.Models.SimlDefine
{
  


    //气体微水含量
    [Serializable]
    public class ContentData
    {
        public int ID;

        //读取数据的时间，诊断时用
        public DateTime ReadDate { get; set; }

        [Display(Name = "H2")]//氢气浓度
        public float H2 { get; set; }

        [Display(Name = "CO")]//一氧化碳浓度
        public float CO { get; set; }

        [Display(Name = "CH4")]//甲烷浓度
        public float CH4 { get; set; }

        [Display(Name = "C2H2")]//乙炔浓度
        public float C2H2 { get; set; }

        [Display(Name = "C2H4")]//乙烯浓度
        public float C2H4 { get; set; }

        [Display(Name = "C2H6")]//乙烷浓度
        public float C2H6 { get; set; }

        [Display(Name = "CO2")]//二氧化碳浓度
        public float CO2 { get; set; }

        [Display(Name = "总烃")]
        public float TotHyd { get; set; }

        [Display(Name = "总可燃气体")]
        public float TotGas;              //总可燃气体

        [Display(Name = "微水")] //微水浓度
        public float Mst { get; set; }

        [Display(Name = "微水温度")]
        public float T { get; set; }

        [Display(Name = "微水活性")]
        public float AW { get; set; }


    }

    //public class RawData
    //{
    //    public DateTime SampleTime { get; set; }

    //    [MaxLength(7)]
    //    public float[] Density{get;set;}

    //    public float RawPPM { get; set; }
       
    //    public ushort RawAW { get; set; }
       
    //    public ushort RawT { get; set; }


    //    [MaxLength(5000)]
    //    public ushort[] RawSixGax{get;set;}

    //    [MaxLength(2000)]
    //    public ushort[] RawCO2{get;set;}
        

    //}

    #region 配置信息定义
    //环境及外围控制
    public class OutSideSetting
    {
        public ushort FengShanKeep_Tick { get; set; }		//风扇预吹保持时间(以脱气开始时间为准),5*60 [0-65535] (秒)
        public ushort FengShanWork_Tick { get; set; }		//风扇吹尾保持时间(用于换气),5*60	[0-65535](秒)？？风扇停止时间？？
        public ushort AirControlStart { get; set; }         //柜体空调采样前开启时刻
        public ushort AirControlWork_Tick { get; set; }     //柜体空调连续工作时间
        public ushort BanReDaiStart_Tick { get; set; }      //伴热带采样前开始时刻
        public ushort BanReDaiWork_Tick { get; set; }       //伴热带采样前工作时间
        public ushort DrainStart { get; set; }              //载气发生器排水阀启动时刻
        public ushort DrainWork_Tick { get; set; }          //载气发生器排水阀工作时间
    }


    //分析计算参数
    public class AnalysisParameter
    {
        public EnvironmentSetting EnviSet { get; set; }
        [MaxLength(2)]
        public H2oFixPara[] H2oFix { get; set; }  //微水修正参数 AW,T
        //public H2oFixPara T { get; set; }
        [MaxLength(7)]
        public GasFixPara[] GasFix{get;set;}  //气体修正参数  H2,CO,CH4,C2H2,C2H4,C2H6,CO2
        public ushort EraseStart { get; set; }  //剔除区间开始
        public ushort EraseEnd { get; set; }   //剔除区间结尾
    }


    //采样控制
    public class SampleSetting
    {


        public ushort ChuiSaoBefore_Tick { get; set; }                 //采样前吹扫阀工作时间 0-3600s
        public ushort DingLiangWork_Tick { get; set; }	  	    //定量阀开启后的持续时间，18s	[0-65535](秒)
        public ushort ChuiSaoDelay_Tick { get; set; }	  	//定量阀打开后，吹扫阀延迟打开的时间，2s [0-65535]	(秒)
        public ushort ChuiSaoAfter_Tick { get; set; }	  	    //采样结束吹扫阀工作时间  0-3600s


        //微水采样控制

       
        public ushort H2oDelayStart_Tick { get; set; }   //微水传感器延时开始加热时刻
        public ushort H2oSampStart_Tick { get; set; }  //微水传感器采样开始时间
        public ushort H2oSampInterval { get; set; }		//采样间隔,100	[1-255]	(10ms为单位)
        public ushort H2oAwSampNum { get; set; }		//微水AW的采样点数	[1-10]
        public ushort H2oTSampNum { get; set; }			//微水T的采样点数	[1-10]
      

        //气体采样控制,六组分+CO2

        public ushort HuiFuBeforeStart { get; set; } //六组分传感器恢复阀采样前开始时刻
        public ushort HuiFuBeforeWork_Tick { get; set; } //六组分传感器恢复阀采样前工作时间
        public ushort HuiFuAfterStart { get; set; } //六组分传感器恢复阀采样后开始时刻
        public ushort HuiFuAfterWork_Tick { get; set; } //六组分传感器恢复阀采样后工作时间

        public ushort SixGasHeatStart { get; set; }  //六组分传感器加热开始时刻
        public ushort SixGasHeatWork_Tick { get; set; }  //六组分传感器加热工作时间
        public ushort SixGasAfterSamp_Tick { get; set; }  //采样结束六组分传感器加热工作时间
        public ushort SixGasSampInterval { get; set; }  //六组分采样间隔
        public ushort SixGasSampNum { get; set; }  //六组分采样点数

        public ushort CO2HeatStart { get; set; }  //CO2传感器加热开始时刻
        public ushort CO2HeatWork_Tick { get; set; }      //CO2传感器工作时间
        public ushort CO2SampInterval { get; set; }  //CO2采样间隔
        public ushort CO2SampNum { get; set; }  //CO2采样点数
        public ushort CO2GasStart { get; set; }  //CO2气路切换开始时刻(以6组气体开始采样为准)
        public ushort CO2GasWork_Tick { get; set; }  //CO2气路工作时间

        public char BiaoDingTimes { get; set; }  //标定次数  1-5次
    }

    //采样开始控制?????????????????????????????????????????????
    public class SampleStartting
    {
        public DateTime SampleTime;                //开始采样时间，年月日时分秒  //////下次采样时间
        public ushort interval;                  //采样间隔,2*24*60, 2[3600-65535]
        public ushort aheadTime;                 //工作流程提前时间,0, [0-65535]	(秒)
    }

    //系统设置
    public class SystemSetting
    {
        public string SoftwareRelease { get; set; }// 软件版本

        //系统支持模块
        public char SuCO2 { get; set; }                        //二氧化碳模块，1:支持，0：不支持
        public char SuH2O { get; set; }                        //微水模块，1:支持，0：不支持
        public char TuoQi_Mode { get; set; }		            //0:真空脱气，1:膜脱气,2:顶空脱气


    }


    //脱气控制
    public class TuoQiSetting
    {

        #region ZKTQ 真空脱气

        public ushort Cycle_Tick { get; set; }//循环时间查询/设置
        public char EvacuTimes { get; set; }//抽空次数查询/设置
        public char CleanTimes { get; set; }//清洗次数查询/设置
        public char TuoQiTimes { get; set; }//脱气次数查询/设置
        public char ChangeTimes { get; set; }//置换次数查询/设置
        public ushort TuoQiEnd_Tick { get; set; }//脱气机预计脱气完成时间


        #endregion


        #region MoTQuery 膜脱气
        public ushort YouBengKeep_Tick { get; set; }			//油泵持续时间,4*60*60 [0-65535] (秒)
        public ushort PaiQiClen_Tick { get; set; }//排气阀清洗时间
        public ushort QiBengClean_Tick { get; set; }	//气泵清洗时间
        public ushort PaiQiKeep_Tick { get; set; }			//排气阀持续时间,60 [0-65535] (秒)

        public ushort QiBengKeepOn_Tick { get; set; }		//气泵连续工作时间,2*60 [0-65535] (秒)

        public ushort PainQiKeepOff_Tick { get; set; }//排气阀间隔停止时间

        public ushort QiBengKeepOff_Tick { get; set; }		//气泵间隔停止时间,3*60 [0-65535] (秒)

        #endregion



        #region DKTQ 顶空脱气


        public ushort StirStart { get; set; }//搅拌开始时刻
        public ushort StirWork_Tick { get; set; }//搅拌工作时间
        public ushort CleanPumpStart { get; set; }//清洗泵开始工作时刻
        public ushort CleanPumpWork_Time { get; set; }//清洗泵工作时间
        public ushort ChangeValveStart { get; set; }//置换阀开始工作时刻
        public ushort ChangeValveWork_Tick { get; set; }//置换阀工作时间

        #endregion

    }


    //检测辅助设置
    public class JCFZSetting
    {
        public JianCeFuZhu SensorRoom { get; set; }  //传感器室
        public JianCeFuZhu LengJing { get; set; }   //冷井
        public JianCeFuZhu SePuZhu { get; set; }   //色谱柱
    }


    //报警及分析
    public class AlarmAll
    {
        [Display(Name = "自动阀值告警")]
        public string AutoAlarm { get; set; }

        [Display(Name = " 自动诊断")]
        public string AutoDiagnose { get; set; }

        [Display(Name = "自动告警诊断功能启用最小日期"), Range(0, 100)]
        public int Interval { get; set; }

        //[Display(Name = "报警功能设置")]
        //public DiagnoseSet DiagSet { get; set; }

        [MaxLength(7)]
        public GasAlarm[] GasAlarm { get; set; }  //各个报警设置，顺序h2,co,ch4,c2h2,c2h4,c2h6,co2
        public TotHyd_GasAlarm thga { get; set; } //TotHyd
        public PPM_GasAlarm pa { get; set; } //Mst、PPM
        public AW_GasAlarm awa { get; set; } //AW--活性
        public T_GasAlarm ta { get; set; } //T--温度
       

    }

    #endregion


    #region 状态控制信息定义
    //真空状态/控制
    public class ZhenKongStateCtrl
    {
        public float VacuPres { get; set; }//（脱气机）真空度压力检测值
        public float QiBengPres { get; set; }//气泵气压检测值
        public float OilPres { get; set; }//油压检测值
        public char YouBeiLevel { get; set; }//油杯液位状态
        public char QiBeiLevel { get; set; }//气杯液位状态
        public char QiGangForw { get; set; }//气缸进到位
        public char QiGangBackw { get; set; }//气缸退到位
        public char YouGangForw { get; set; }//油缸进到位
        public char YouGangBackw { get; set; }//油缸退到位

        public char OilPump { get; set; }//油泵
        public float OilPumpRoV { get; set; }//油泵转速控制输出
        public char OilValve { get; set; } //进出油阀
        public char YV10 { get; set; }//YV10阀
        public char YV11 { get; set; }//YV11阀
        public char YV12 { get; set; }//YV12阀
        public char YV14 { get; set; }//YV14阀
        public char YV15 { get; set; }//YV15阀
        public char YV4 { get; set; }//气缸YV4阀
        public char YV5 { get; set; }//气缸YV5阀
        public char YV6 { get; set; }//气缸YV6阀
        public char YV7 { get; set; }//气缸YV7阀
        public char QiBeng { get; set; }//气泵

    }

    //顶空状态控制
    public class DingKongStateCtrl
    {
        public char StirSwitch { get; set; }//（手动）搅拌开关（立即开始）
        public char LevelA { get; set; }//液位A
        public char LevelB { get; set; }//液位B
        public float StirTemprature { get; set; }//搅拌缸检测温度
    }

    //检测辅助状态控制
    public class JCFZStateCtrl
    {
        public char LengJingSwitch { get; set; } //（手动）冷井开关
        public char SensorRoomCooler { get; set; }  //（手动）传感器室制冷

        public float SensorRoomT;    //传感器室温度实际采样值
        public float LengJingT;    //冷井温度实际采样值
        public float SePuZhuT;    //色谱柱温度实际采样值
    }

    //环境外围状态控制
    public class OutSideStateCtrl
    {
        [Display(Name = "油温")]
        public double OilTemprature { get; set; }

        [Display(Name = "柜内温度")]
        public double Temprature_In { get; set; }

        [Display(Name = "柜外温度")]
        public double Temprature_Out { get; set; }
        public float BanReDaiT { get; set; }

        [Display(Name = "通讯正常")]
        public char OnLineStatus { get; set; }

        public char QiBengSwitch { get; set; }  //载气发生器气泵开关
        public char AirControlSwitch { get; set; } //（手动）柜体空调开关
        public char BanReDaiStart { get; set; }//伴热带立即加热
        public char DrainSwitch { get; set; }//载气发生器排水阀
    }

    //采样状态控制
    public class SampleStateCtrl
    {
        public char StandardTime { get; set; }  //标定次数
        public char StandardStart { get; set; }  //标定（立即启动）
        public char SampleStart { get; set; }    //采样（立即启动）
        public ushort SampleInterval { get; set; }  //采样间隔
        public DateTime NextSampleTime { get; set; }   //查询下次采样时间

        public char SixGaxStart { get; set; }  //六组分传感器手动立即启动
        public char H2oHeatStart { get; set; } //微水传感器加热手动立即启动
        public char H2oStart { get; set; }    //微水传感器手动立即启动
        public char DingLiangStart { get; set; } //定量阀手动立即启动
        public char ChuiSaoStart { get; set; }  //吹扫阀手动立即启动
        public char CO2ChangeStart { get; set; } //CO2切换阀手动立即启动
        public char ChuiQiStart { get; set; } //吹气阀手动立即启动

        [Display(Name = "载气压力检测实际值")]
        public float GasPressure { get; set; }  //载气压力检测实际值
    }

    //系统状态控制

    public class SystemStateCtrl
    {
        public DateTime PLCTime { get; set; }  //系统时间，下位机
        public char DevState { get; set; } //设备状态（手动/自动方式）
        public char WorkFlow { get; set; }  //工作流程
        //public SysSetting Set1 { get; set; }  //指向上位机1，串口1，网卡1
        //public SysSetting Set2 { get; set; }  //指向上位机2，串口2，网卡2
    }

    #endregion


    #region 诊断告警相关定义

    // 诊断分析
    public class AnlyInformation
    {

        public ThreeValue threevar;	                    //三比值编码

        public DevidTriAnlyInfo devidInfo;		//大卫三角形分析结果

        public Ratio ratio;					    //立体图示法所需参数


        public string Desc;	                    //分析结果

        public string chDesc;

        public string enDesc;
    }
       
    //大卫三角形分析结果

    public struct DevidTriAnlyInfo
    {

        public decimal percent_C2H2;	            // %C2H2

        public decimal percent_C2H4;	            // %C2H4

        public decimal percent_CH4;	                // %CH4

        public string Desc;		                    // 分析结果
    };

    //立体图示法所需比值参数

    public struct Ratio
    {

        public decimal C2H2_C2H4;	                // C2H2 / C2H4

        public decimal C2H4_C2H6;	                // C2H4 / C2H6

        public decimal CH4_H2;		                // CH4 / H2
    };

    //三比值

    public struct ThreeValue
    {

        public float Var1;

        public float Var2;

        public float Var3;

        public string Desc;		                    // 分析结果
    }


    //告警信息

    public class AlarmMsgAll
    {
        //private int _id;
        //private string _deviceID;
        private string _gasName;
        private int _type;
        private decimal _alarmValue;        //设定的告警值
        private decimal _realValue;         //实际测量值
        private string _message;
        private int _curGasID;                 //当前气体ID
        private int _compareGasID;          //比较的气体ＩＤ
        private DateTime _alarmDate;        //告警日期
        private DateTime _curDate;          //当前气体采样日期
        private DateTime _prevDate;         //告警气体采样日期
        private int _level;                 //告警级别


        //public int ID
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}


        //public string DeviceID
        //{
        //    get { return _deviceID; }
        //    set { _deviceID = value; }
        //}


        public string GasName
        {
            get { return _gasName; }
            set { _gasName = value; }
        }


        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }


        public decimal AlarmValue
        {
            get { return _alarmValue; }
            set { _alarmValue = value; }
        }


        public decimal RealValue
        {
            get { return _realValue; }
            set { _realValue = value; }
        }


        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }


        public int CurGasID
        {
            get { return _curGasID; }
            set { _curGasID = value; }
        }


        public int CompareGasID
        {
            get { return _compareGasID; }
            set { _compareGasID = value; }
        }


        public DateTime AlarmDate
        {
            get { return _alarmDate; }
            set { _alarmDate = value; }
        }


        public DateTime CurDate
        {
            get { return _curDate; }
            set { _curDate = value; }
        }


        public DateTime PrevData
        {
            get { return _prevDate; }
            set { _prevDate = value; }
        }


        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
    }


    #endregion



   



    #region 各部分相关定义


    //气体修正参数
    public class GasFixPara
    {
        [Display(Name = "峰顶点位置"), Range(0, 5000)]
        public ushort PeakPoint { get; set; }

        [Display(Name = "峰顶范围起点"), Range(0, 5000)]
        public ushort PeakLeft { get; set; }

        [Display(Name = "峰顶范围结束点"), Range(0, 5000)]
        public ushort PeakRight { get; set; }

        [Display(Name = "左梯度Min"), Range(0, 1000)]
        public ushort LeftTMin { get; set; }

        [Display(Name = "左梯度Max"), Range(0, 1000)]
        public ushort LeftTMax { get; set; }

        [Display(Name = "右梯度Min"), Range(0, 1000)]
        public ushort RightTMin { get; set; }
        [Display(Name = "右梯度Max"), Range(0, 1000)]
        public ushort RightTMax { get; set; }

        [Display(Name = "峰顶宽度"), Range(0, 1000)]
        public ushort PeakWidth { get; set; }

        [Display(Name = "多级K值")]

        [MaxLength(12)]
        public GasFixK[] MultiK { get; set; }  //12个K值设置

        [Range(1, 10.00), Display(Name = "离线/在线计算偏差基值")]   //离线/在线计算偏差值J，12个

        [MaxLength(12)]
        public double[] J { get; set; }    

      
    }
    //气体修正K值
    public struct GasFixK
    {
        public float k, mi, ni;                     //K值，柱修正系数，脱气率修正系数
        public float areaMin;               //该K值对应的最小面积
        public float areaMax;               //该K值对应的最大面积
    }

    //微水修正参数
    public class H2oFixPara
    {
        public float A, K, B;
    }


    //检测辅助控制,,传感器室，冷井，色谱柱的
    public class JianCeFuZhu
    {
        public ushort Start;//启动开始时刻
        public ushort Work_Tick;//连续工作时间
        public float TempSet;//温度设置值
        public float TempP;//温度设置P值
        public float TempI;//温度设置I值
        public float TempD;//温度设置D值
        public float TempPID;//温度控制PID范围
    }


    //气体报警及分析
    public class GasAlarm
    {
        [Display(Name = "一级报警")]
        public float Level1 { get; set; }

        [Display(Name = "二级报警")]
        public float Level2 { get; set; }

        [Display(Name = "绝对气体产生速率注意值一级")]
        public float AbsLevel1 { get; set; }

        [Display(Name = "绝对气体产生速率注意值二级")]
        public float AbsLevel2 { get; set; }
        [Display(Name = "相对气体产生速率注意值一级")]
        public float RelLevel1 { get; set; }

        [Display(Name = "相对气体产生速率注意值二级")]
        public float RelLevel2 { get; set; }

        [Display(Name = "报警门限值触发条件")]
        public float AlarmContent { get; set; }

        [Display(Name = "绝对产气速率触发")]
        public float AbsRateAlarmNumber { get; set; }

        [Display(Name = "相对产气速率触发")]
        public float RelRateAlarmNumer { get; set; }
    }

    public class PPM_GasAlarm
    {
        [Display(Name = "一级报警")]
        public float Level1 { get; set; }

        [Display(Name = "二级报警")]
        public float Level2 { get; set; }
    }

    //Mst  同上
    //public class Mst_GasAlarm
    //{
    //    [Display(Name = "一级报警")]
    //    public float Level1 { get; set; }

    //    [Display(Name = "二级报警")]
    //    public float Level2 { get; set; }
    //}

    public class T_GasAlarm
    {
        [Display(Name = "一级报警")]
        public float Level1 { get; set; }

        [Display(Name = "二级报警")]
        public float Level2 { get; set; }
    }

    public class AW_GasAlarm
    {
        [Display(Name = "一级报警")]
        public float Level1 { get; set; }

        [Display(Name = "二级报警")]
        public float Level2 { get; set; }
    }

    public class TotHyd_GasAlarm
    {
        [Display(Name = "一级报警")]
        public float Level1 { get; set; }

        [Display(Name = "二级报警")]
        public float Level2 { get; set; }
    }

    //报警级别
    public class AlarmLevel
    {
        public float Level1 { get; set; }
        public float Level2 { get; set; }
    }


    


    //环境设置,海拔、油压等

    public class EnvironmentSetting
    {
        [Display(Name = "海拔高度"), Range(0,50000)]
        public ushort altitude { get; set; }             //海拔高度（2bytes）：米

        [Display(Name = "电压等级"), Range(0, 5000)]
        public float voltage { get; set; }              //电压等级（2bytes）

        [Display(Name = "油重"), Range(0, 200.0)]
        public float oilTotal { get; set; }           //油总量，油重（2bytes）

        [Display(Name = "油密度"), Range(0, 1.0000)]
        public float oilDensity { get; set; }           //油密度（2bytes）

        [Display(Name = "油品系数A"), Range(0, 5000)]
        public float oilFactorA { get; set; }            //油品系数A（4bytes）

        [Display(Name = "油品系数B"), Range(0, 5000)]
        public float oilFactorB { get; set; }            //油品系数B（4bytes）
    }
    public class OilFactor
    {
 
    }

    #endregion



    public class RawData
    {
        public ushort[] RawSixGax;
        public ushort[] RawCO2;
    }






}



