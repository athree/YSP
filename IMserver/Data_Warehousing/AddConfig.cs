using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMserver.Models;
using IMserver.DBservice;
using IMserver.Models.SimlDefine;

namespace IMserver.Data_Warehousing
{
    (class AddConfig
    {
        (bool Warehousing(Dictionary<ushort, object> middata, byte devid)
        {
            Config cf = new Config();
            cf.DevID = devid.ToString();
            cf.Time_Stamp = DateTime.Now;

            foreach (KeyValuePair<ushort, object> kvp in middata)
            {
                switch (kvp.Key)
                {
                    //环境及外围设置
                    FengShanKeep_Tick =	(ushort 	//风扇预吹保持时间(以脱气开始时间为准),5*60 [0-65535] (秒)
                    FengShanWork_Tick =	(ushort 	//风扇吹尾保持时间(用于换气),5*60	[0-65535](秒)？？风扇停止时间？？
                    AirControlStart = (ushort         //柜体空调采样前开启时刻
                    AirControlWork_Tick = (ushort     //柜体空调连续工作时间
                    BanReDaiStart_Tick = (ushort      //伴热带采样前开始时刻
                    BanReDaiWork_Tick = (ushort       //伴热带采样前工作时间
                    DrainStart = (ushort              //载气发生器排水阀启动时刻
                    DrainWork_Tick = (ushort          //载气发生器排水阀工作时间

                    //分析计算参数
                    #region  环境设置,海拔、油压等
                    altitude = (ushort             //海拔高度（2bytes）：米
                    voltage = (float              //电压等级（2bytes）
                    oilTotal = (float           //油总量，油重（2bytes）
                    oilDensity = (float           //油密度（2bytes）
                    oilFactorA = (float            //油品系数A（4bytes）
                    oilFactorB = (float            //油品系数B（4bytes
#endregion
                    #region 微水修正参数 AW,T 
                    (H2oFixPara[2] H2oFix =  
                    A = (float 
                    K = (float
                    B = (float
#endregion
                    #region 气体修正参数  H2,CO,CH4,C2H2,C2H4,C2H6,CO2
                    (GasFixPara[7] GasFix{get;set;}        
                    PeakPoint = (ushort 
                    PeakLeft = (ushort 
                    PeakRight = (ushort 
                    LeftTMin = (ushort 
                    LeftTMax = (ushort 
                    RightTMin = (ushort 
                    RightTMax = (ushort 
                    PeakWidth = (ushort 
                    (GasFixK[12] MultiK =  //12个K值设置
                    k = (float 
                    mi = (float 
                    ni = (float                      //K值，柱修正系数，脱气率修正系数
                    areaMin = (float                //该K值对应的最小面积
                    areaMax = (float                //该K值对应的最大面积
                    (float[12] J =         //离线/在线计算偏差值J，12个
#endregion
                    (ushort EraseStart =  //剔除区间开始
                    (ushort EraseEnd =   //剔除区间结尾

                    //采样控制
                    #region 初始参数
                    (ushort ChuiSaoBefore_Tick =                 //采样前吹扫阀工作时间 0-3600s
                    (DingLiangWork_Tick =	  	    //定量阀开启后的持续时间，18s	[0-65535](秒)
                    (ushort ChuiSaoDelay_Tick =	  	//定量阀打开后，吹扫阀延迟打开的时间，2s [0-65535]	(秒)
                    (ushort ChuiSaoAfter_Tick =	  	    //采样结束吹扫阀工作时间  0-3600s
#endregion
                    #region 微水采样
                    (ushort H2oDelayStart_Tick =   //微水传感器延时开始加热时刻
                    (ushort H2oSampStart_Tick =  //微水传感器采样开始时间
                    (ushort H2oSampInterval =		//采样间隔,100	[1-255]	(10ms为单位)
                    (ushort H2oAwSampNum =		//微水AW的采样点数	[1-10]
                    (ushort H2oTSampNum =			//微水T的采样点数	[1-10]
#endregion
                    #region 气体采样控制,六组分+CO2
                    (ushort HuiFuBeforeStart = //六组分传感器恢复阀采样前开始时刻
                    (ushort HuiFuBeforeWork_Tick = //六组分传感器恢复阀采样前工作时间
                    (ushort HuiFuAfterStart = //六组分传感器恢复阀采样后开始时刻
                    (ushort HuiFuAfterWork_Tick = //六组分传感器恢复阀采样后工作时间
                    (ushort SixGasHeatStart =  //六组分传感器加热开始时刻
                    (ushort SixGasHeatWork_Tick =  //六组分传感器加热工作时间
                    (ushort SixGasAfterSamp_Tick =  //采样结束六组分传感器加热工作时间
                    (ushort SixGasSampInterval =  //六组分采样间隔
                    (ushort SixGasSampNum =  //六组分采样点数
                    (ushort CO2HeatStart =  //CO2传感器加热开始时刻
                    (ushort CO2HeatWork_Tick =      //CO2传感器工作时间
                    (ushort CO2SampInterval =  //CO2采样间隔
                    (ushort CO2SampNum =  //CO2采样点数
                    (ushort CO2GasStart =  //CO2气路切换开始时刻(以6组气体开始采样为准)
                    (ushort CO2GasWork_Tick =  //CO2气路工作时间
                    (char BiaoDingTimes =  //标定次数  1-5次
#endregion

                    //采样开始控制
                    (DateTime SampleTime;                //开始采样时间，年月日时分秒  //////下次采样时间
                    (ushort interval;                  //采样间隔,2*24*60, 2[3600-65535]
                    (ushort aheadTime;                 //工作流程提前时间,0, [0-65535]	(秒)

                    //系统设置
                    (string SoftwareRelease =// 软件版本
                    (char SuCO2 =                        //二氧化碳模块，1:支持，0：不支持
                    (char SuH2O =                        //微水模块，1:支持，0：不支持
                    (char TuoQi_Mode =		            //0:真空脱气，1:膜脱气,2:顶空脱气

                    //脱气设置
                    #region 真空脱气
                    (ushort Cycle_Tick =//循环时间查询/设置
                    (char EvacuTimes =//抽空次数查询/设置
                    (char CleanTimes =//清洗次数查询/设置
                    (char TuoQiTimes =//脱气次数查询/设置
                    (char ChangeTimes =//置换次数查询/设置
                    (ushort TuoQiEnd_Tick =//脱气机预计脱气完成时间
                    #endregion
                    #region  膜脱气
                    (ushort YouBengKeep_Tick =			//油泵持续时间,4*60*60 [0-65535] (秒)
                    (ushort PaiQiClen_Tick =        //排气阀清洗时间
                    (ushort QiBengClean_Tick =	//气泵清洗时间
                    (ushort PaiQiKeep_Tick =			//排气阀持续时间,60 [0-65535] (秒)
                    (ushort QiBengKeepOn_Tick =		//气泵连续工作时间,2*60 [0-65535] (秒)
                    (ushort PainQiKeepOff_Tick =       //排气阀间隔停止时间
                    (ushort QiBengKeepOff_Tick =		//气泵间隔停止时间,3*60 [0-65535] (秒)
                    #endregion
                    #region 顶空脱气
                    (ushort StirStart =//搅拌开始时刻
                    (ushort StirWork_Tick =//搅拌工作时间
                    (ushort CleanPumpStart =//清洗泵开始工作时刻
                    (ushort CleanPumpWork_Time =//清洗泵工作时间
                    (ushort ChangeValveStart =//置换阀开始工作时刻
                    (ushort ChangeValveWork_Tick =//置换阀工作时间
                    #endregion

                    //检测辅助设置
                    #region 传感器
                    (ushort Start;//启动开始时刻
                    (ushort Work_Tick;//连续工作时间
                    (float TempSet;//温度设置值
                    (float TempP;//温度设置P值
                    (float TempI;//温度设置I值
                    (float TempD;//温度设置D值
                    (float TempPID;//温度控制PID范围
                    #endregion
                    #region 冷井
                    (ushort Start;//启动开始时刻
                    (ushort Work_Tick;//连续工作时间
                    (float TempSet;//温度设置值
                    (float TempP;//温度设置P值
                    (float TempI;//温度设置I值
                    (float TempD;//温度设置D值
                    (float TempPID;//温度控制PID范围
                    #endregion
                    #region 色谱柱
                    (ushort Start;//启动开始时刻
                    (ushort Work_Tick;//连续工作时间
                    (float TempSet;//温度设置值
                    (float TempP;//温度设置P值
                    (float TempI;//温度设置I值
                    (float TempD;//温度设置D值
                    (float TempPID;//温度控制PID范围
                    #endregion

                    //报警及分析配置
                    AlarmAll--报警界面写过
                }
            }
            return false;
        }

        (static bool Warehousing(Config cf)
        {
            MongoHelper<Config> mhas = new MongoHelper<Config>();
            return mhas.Insert(cf);
            return false;
        }
    }
}