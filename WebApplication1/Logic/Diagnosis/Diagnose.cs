using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

using System.IO;
using IMserver.DBservice;

using IMserver.Models.SimlDefine;
using DCAG.Diagnosis;
using System.Linq.Expressions;
using System.Data;
using IMserver.Models;

namespace WebApplication1.Diagnosis
{
    public class Diagnose
    {
       
        private const double COMPENSATION_FACTOR = 1.0009;
        private const float MIN = 0.00000000000001F;

        private static MongoHelper<SIML> _siml = new MongoHelper<SIML>();
        private static YSPservice _ysp = new YSPservice();

        //得到滤波后谱图 及 峰识别点
        public static AreaStruct Gas_Process_Step1(string deviceID, RawData raw,AnalysisParameter anlycfg)
        {
            if (raw == null || anlycfg == null)
            {
                return null;
            }

            //GasData gasData = new GasData(deviceID, allgasdata);


            //气体的配置和分析参数
            //ushort eraseStart = allgasdata.AnalyPara.EraseStart;
            //ushort eraseEnd = allgasdata.AnalyPara.EraseEnd;

            GasFixPara[] fpList = anlycfg.GasFix;
            if (fpList == null) { return null; }
            ushort[] mid = new ushort[6];
            ushort[] left = new ushort[6];
            ushort[] right = new ushort[6];
            ushort[] lx = new ushort[6];
            ushort[] ly = new ushort[6];
            ushort[] rx = new ushort[6];
            ushort[] ry = new ushort[6];
            ushort[] fw = new ushort[6];
            for (int i = 0; i < 6; i++)         //六组份出峰区间
            {
                //mid[i] = fpList[i].PeakPoint;
                //left[i] = fpList[i].PeakLeft;
                //right[i] = fpList[i].PeakRight;
                //lx[i] = fpList[i].LeftTMax;
                //rx[i] = fpList[i].RightTMax;
                //ly[i] = fpList[i].LeftTMin;
                //ry[i] = fpList[i].RightTMin;
                //fw[i] = fpList[i].PeakWidth;
                //上面注释为修改最小集合类之前，之后为修改最小集合类之后的组织
                mid[i] = fpList[i].p_a.PeakPoint;
                left[i] = fpList[i].p_a.PeakLeft;
                right[i] = fpList[i].p_a.PeakRight;
                lx[i] = fpList[i].p_b.LeftTMax;
                rx[i] = fpList[i].p_b.RightTMax;
                ly[i] = fpList[i].p_b.LeftTMin;
                ry[i] = fpList[i].p_b.RightTMin;
                fw[i] = fpList[i].p_a.PeakWidth;
            }
            //ushort co2Left = fpList[6].PeakLeft;  //CO2出峰区间
            //ushort co2Right = fpList[6].PeakRight;
            ushort co2Left = fpList[6].p_a.PeakLeft;  //CO2出峰区间
            ushort co2Right = fpList[6].p_a.PeakRight;
            //六组分采原始数据
          
            ushort[] sampleData=raw.RawSixGax;
           
            //CO2
           
            ushort[] co2SampleData = raw.RawCO2;
              

            //将剔除区间的数据拟合为直线
            //int start = anlycfg.EraseStart;
            //int end = anlycfg.EraseEnd;
            int start = anlycfg.er.start;
            int end = anlycfg.er.end;
            if (end - start > 0 && start < sampleData.Length && end < sampleData.Length)
            {
                int n = end - start;
                float deltY = (sampleData[end] - sampleData[start]) / n;

                for (int i = start; i < end; i++)
                {
                    sampleData[i] = (ushort)(sampleData[start] + deltY * (i - start));
                }
            }


            //带入算法求气体面积和出峰范围
            //ComputeArea ca = new ComputeArea(sampleData, sampleData.Length, mid, left, right);
            ComputeArea ca = new ComputeArea(sampleData, sampleData.Length, mid, left, right, ly, lx, ry, rx, fw);
            AreaStruct areas = ca.Compute();
            if (co2SampleData != null)
            {
                areas.Co2Areas = ComputeArea.Compute_Co2(co2SampleData, co2Left, co2Right);
            }

            return areas;
        }

        public static float[] Gas_Process_Step2(string deviceID, AreaStruct areas, GasFixPara[] fpList)
        {
            if (areas == null || fpList == null)
            { return null; }

            float[] ret = new float[8];

            //根据面积计算浓度。采样浓度＝(K+Ni)*Ai+M
            for (int i = 0; i < 6; i++)
            {
                int sel = SIML.GetGasFixParameters(fpList[i].MultiK, areas.Areas[i]);

                ret[i] = fpList[i].MultiK[sel].k * (areas.Areas[i] - fpList[i].MultiK[sel].ni) +
                       fpList[i].MultiK[sel].mi;

            }

            int co2idx = 6;
            int co2sel = SIML.GetGasFixParameters(fpList[co2idx].MultiK, areas.Co2Areas);
            
            ret[co2idx] = (fpList[co2idx].MultiK[co2sel].k) * (areas.Co2Areas - fpList[co2idx].MultiK[co2sel].ni) +
                 fpList[co2idx].MultiK[co2sel].mi;


            int cxhxIdx = 7;
            float cxhx = ret[2] + ret[3] + ret[4] + ret[5];     //CH4+C2H2+C2H4+C2H6
            ret[cxhxIdx] = cxhx;

            return ret;
        }

        //失败原因：0-成功  1-对比数据浓度为0  2-数据间无采样间隔
        public static float[] Gas_Anly_Step1_RelSpeed(out int[] failReason, string deviceID, float[] data, DateTime timeData, float[] contrast_data, DateTime timeContrastData)
        {
            failReason = null;
            if (data == null || contrast_data == null || timeData == null || timeContrastData == null)
            { return null; }
            if (timeData <= timeContrastData)
            { 
                return null;      //9月20日改
            }

            TimeSpan ts = timeData - timeContrastData;
            decimal t = (decimal)ts.TotalHours / 24.0m / 30.417m;	//粗略计算 //时间间隔 需要折算为 月

            int[] reason = new int[8];
            float[] ret = new float[8];
            for (int i = 0; i < 8; i++)
            {
                ret[i] = 0;

                if (Math.Abs(t) < (decimal)MIN)
                { reason[i] = 2; }
                else if (contrast_data[i] == 0)
                { reason[i] = 1; }
                else
                {
                    reason[i] = 0;
                    ret[i] = (data[i] - contrast_data[i]) / contrast_data[i] * 1.0f / (float)(t) * 100.0f;
                }
            }

            failReason = reason;
            return ret;
        }

        /// <summary>
        /// 计算绝对产期速率。
        /// 绝对产期速率公式：
        ///     r = (C2 - C1) / t * (G / p)
        ///     r : 绝对产期速率，mL/d
        ///     C2: 第二次取样测得的有中气体密度，uL/L
        ///     C1: 第一次取样测得的有中气体密度，uL/L
        ///     t : 两次取样时间间隔(日)
        ///     G : 设备油总量, t
        ///     p : 油密度，t / m3
        /// </summary>
        ///         //失败原因：0-成功  1-油密度为0  2-数据间无采样间隔
        public static float[] Gas_Anly_Step1_AbsSpeed(out int[] failReason, string deviceID, float[] data, DateTime timeData, float[] contrast_data, DateTime timeContrastData, float oilTotal, float oilP)
        {
            failReason = null;
            if (data == null || contrast_data == null || timeData == null || timeContrastData == null)
            { return null; }
            if (timeData <= timeContrastData)
            { 
                return null;      //9月20日改
            }

            TimeSpan span = timeData - timeContrastData;        //时间间隔 需要折算为 天
            float t = (float)span.TotalHours / 24.0F;
            if (Math.Abs(t) < MIN)
            {
                return null;
            }

            int[] reason = new int[8];
            float[] ret = new float[8];
            for (int i = 0; i < 8; i++)
            {
                ret[i] = 0;

                if (Math.Abs(t) < MIN)
                { reason[i] = 2; }
                else if (oilP <= (float)MIN)
                { reason[i] = 1; }
                else
                {
                    reason[i] = 0;
                    ret[i] = (data[i] - contrast_data[i]) / (float)(t) * oilTotal / oilP;
                }
            }

            failReason = reason;
            return ret;
        }

        //阈值告警判断
        public static byte[] Gas_Anly_Step2_ThresholdAlarm(int devId, AlarmAll hold, float[] data, int alarmType, ref float[] lv)
        {
            byte[] ret = new byte[8];
            for (int i = 0; i < 8; i++)
            { ret[i] = 0; };    //无告警
            
            if (hold == null || data == null || lv==null)
            {
                return ret;
            }
            float[] lv1 = new float[8];
            float[] lv2 = new float[8];
            if (alarmType == 0)  //浓度阈值告警
            {
                for (int i = 0; i < lv1.Length; i++)
                {
                    lv1[i] = hold.GasAlarm[i].Level1;
                    lv2[i] = hold.GasAlarm[i].Level2;
                }
                   
            }
            else if(alarmType == 1) //绝对产气速率告警
            {
                for (int i = 0; i < lv1.Length; i++)
                {
                    lv1[i] = hold.GasAlarm[i].AbsLevel1;
                    lv2[i] = hold.GasAlarm[i].AbsLevel1;
                }
            }
            else if (alarmType == 2) //相对产气速率告警
            {
                for (int i = 0; i < lv1.Length; i++)
                {
                    lv1[i] = hold.GasAlarm[i].RelLevel1;
                    lv2[i] = hold.GasAlarm[i].RelLevel1;
                }
            }
            else
            {
                return ret;
            }

            //
            for (int i = 0; i < 8; i++)
            {
                ret[i] = 0;         //无告警
                lv[i] = 0.0f;

                if (Math.Abs(lv1[i]) > MIN && Math.Abs(lv2[i]) < MIN)      //lv1>0 && lv2==0
                {
                    if (data[i] >= lv1[i])
                    {
                        ret[i] = 1;
                        lv[i] = (float)lv1[i];
                    }
                }
                if (Math.Abs(lv1[i]) < MIN && Math.Abs(lv2[i]) > MIN)      //lv1==0 && lv2>0
                {
                    if (data[i] >= lv1[i])
                    {
                        ret[i] = 2;
                        lv[i] = (float)lv2[i];
                    }
                }
                if (Math.Abs(lv1[i])>MIN && Math.Abs(lv2[i])>MIN && Math.Abs(lv1[i])<Math.Abs(lv2[i]))      //lv1>0 && lv2>0 && lv1<lv2
                {
                    if (data[i] >= lv2[i] && data[i] < lv2[i])
                    {
                        ret[i] = 1;
                        lv[i] = (float)lv1[i];
                    }
                    else if(data[i] >= lv2[i])
                    {
                        ret[i] = 2;
                        lv[i] = (float)lv2[i];
                    }
                }                
            }

            return ret;
        }

        /// 判断是否有故障（即超过故障阈值）
        ///    故障诊断触发用浓度注意值（H2,C2H2,总烃）
        ///    故障诊断触发用绝对产气速率注意值（H2,C2H2,总烃,CO,CO2），单位ml/天
        ///    故障诊断触发用相对产气速率注意值（总烃），单位%/月
        public static bool Gas_Anly_Step2_FaultChk(int devId,AlarmAll hold, float[] data, float[] absdata, float[] reldata)
        {
            bool ret = false;

            if (hold == null || data == null || absdata==null || reldata==null)
            {
                return ret;
            }

            float ppmh2 = data[0];
            float ppmc2h2 = data[3];
            float ppmcxhx = data[7];

            float absh2 = absdata[0];
            float absc2h2 = absdata[3];
            float abscxhx = absdata[7];
            float absco = absdata[1];
            float absco2 = absdata[6];

            float relcxhx = reldata[7];

            ret = false;
            for (int i = 0; i < data.Length; i++)
            {
                if (ret == false && (data[i] >= hold.GasAlarm[i].AlarmContent))
                {
                    ret = true;
                    break;
                }
               
            }
            for (int i = 0; i < data.Length; i++)
            {
                if (ret == false && (absdata[i] >= hold.GasAlarm[i].AbsRateAlarmNumber))
                {
                    ret = true;
                    break;
                }
            }
            for (int i = 0; i < data.Length; i++)
            {

                if (ret == false && (reldata[i] >= hold.GasAlarm[i].RelRateAlarmNumer))
                {
                    ret = true;
                    break;
                }
            }
               

            return ret;
        }

        //具体故障原因诊断
        public static AnlyInformation Gas_Anly_Step3_FaultDiagnosis(float[] data)
        {
            if (data == null)
            { return null; }

            AnlyInformation ai = new AnlyInformation();

            decimal ppmh2 = Convert.ToDecimal(data[0]);
            decimal ppmch4 = Convert.ToDecimal(data[2]);
            decimal ppmc2h2 = Convert.ToDecimal(data[3]);
            decimal ppmc2h4 = Convert.ToDecimal(data[4]);
            decimal ppmc2h6 = Convert.ToDecimal(data[5]);

            ThreeRatio.Diagnose(ppmh2, ppmch4, ppmc2h2, ppmc2h4, ppmc2h6, ref ai);

            return ai;
        }
        private static void save_bin(byte[] bytes)
        {

            FileStream fs = new FileStream("c:\\Users\\Administrator\\Desktop\\123\\1\\2.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            try
            {
                for (int i = 0; i < bytes.Length; i++)
                    sw.WriteLine("{0},{1}", i, bytes[i]);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Close();
                fs.Close();
            }

        }

        public static float Pec8Domn(float f)
        {
            float ft;
            Random rd = new Random();
            int x = rd.Next(1);
            if (x == 0)
            {
                ft = f + f * 0.02f;
            }
            else
            {
                ft = f - f * 0.02f;
            }
            return ft;
        }

        /// <summary>
        /// 处理气体数据
        /// </summary>
        /// <param name="data"></param>
        public static bool GasData_Process(RunningState alldata, int anlyMode)
        {
            //sampTime = null;

            if (alldata == null)
            {
                return false;
            }
            RunningState _data = alldata;
            #region
            //FileStream fs = new FileStream("c:\\Users\\Administrator\\Desktop\\123\\1\\1.txt", FileMode.Create);
            //StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            //try
            //{
            //    for (int i = 0; i < gasData.Analysis.Value.fixList.Length; i++)
            //    {


            //        {

            //            sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}", gasData.Analysis.Value.fixList[i].highPos, gasData.Analysis.Value.fixList[i].highPosLeft, gasData.Analysis.Value.fixList[i].highPosRight, gasData.Analysis.Value.fixList[i].MaxHighWidth, gasData.Analysis.Value.fixList[i].MaxLXGrade, gasData.Analysis.Value.fixList[i].MaxRXGrade, gasData.Analysis.Value.fixList[i].MinLYGrade, gasData.Analysis.Value.fixList[i].MinRYGrade);
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            //    sw.Close();
            //    fs.Close();
            //}

            //save_bin(alldata);
            #endregion
            
            Config cfg = _ysp.GetCFG(_data.DevID);
            //if (_data.WorkFlow == null || cfg.SampStart.SampleTime == null)
            //{ return false; }

            if (_data.ReadDate == null)
            { 
                return false;
            }

            //设备号
            string deviceID = _data.DevID;

            //非采样流程产生的数据则不处理           
            //if (_data.WorkFlow != '1')
            //{
            //    Console.WriteLine("--- _data_Process : Receive Gas data From Other WorkFlow, Not Process! " + DateTime.Now.ToString());
            //    return false;
            //}

            //采样日期
            //DateTime sampleTime = _data.ReadDate;

            //分析参数配置
            AnalysisParameter anlyParams = null;
            //--从原始数据拿
            if (anlyMode != 1)
            {
                //Define.Config? cfg = null;
                //cfg = _data.Config;

                anlyParams = cfg.AnalyPara;

                if (anlyParams == null)
                {
                    Console.WriteLine("--- _data_Process : Receive Invalid Gas Data, Config is NULL! " + DateTime.Now.ToString());
                    return false;
                }


            }
            #region   --从数据库拿////
            //else
            //{
            //    WebApplication1.Models.AnalysisParam dbAnlyCfg = null;
            //    dbAnlyCfg = new WebApplication1.BLL.AnalysisParam().GetModel(deviceID);
            //    if (dbAnlyCfg == null)
            //    {
            //        Console.WriteLine("--- GasData_Process : Get Anly_Params from Database Fail!" + DateTime.Now.ToString());
            //        return false;
            //    }

            //    // DB AnalysisParam    TO     DEFINE AnalysisParameter
            //    Define.AnalysisParameter sap = new Define.AnalysisParameter();
            //    sap.fixList = new Define.FixedParam[7];

            //    object obj = WebApplication1.COMMON.CommonFuncs.BytesToStuct(dbAnlyCfg.EnvironmentSetting, typeof(WebApplication1.COMMON.Define.EnvironmentSetting));
            //    if (obj == null)
            //    {
            //        Console.WriteLine("--- GasData_Process : Get EnvironmentSetting from Database Fail!" + DateTime.Now.ToString());
            //        return false;
            //    }
            //    sap.enviroment = (WebApplication1.COMMON.Define.EnvironmentSetting)obj;

            //    if (dbAnlyCfg.Other.Length >= Define.ER_LEN)
            //    {
            //        //obj = DCAG.COMMON.CommonFuncs.BytesToStuct(dbAnlyCfg.Other, typeof(DCAG.COMMON.Define.EraseRange));
            //        //if (obj == null) { return null; }
            //        //sap.range = (DCAG.COMMON.Define.EraseRange)obj;
            //        byte[] temprange = new byte[Define.ER_LEN];
            //        Array.Copy(dbAnlyCfg.Other, 0, temprange, 0, Define.ER_LEN);
            //        obj = CommonFuncs.BytesToStuct(temprange, typeof(Define.EraseRange));
            //        if (obj == null)
            //        {
            //            Console.WriteLine("--- GasData_Process : Get Other.EraseRange from Database Fail!" + DateTime.Now.ToString());
            //            return false;
            //        }
            //        sap.range = (WebApplication1.COMMON.Define.EraseRange)obj;
            //    }
            //    else
            //    {
            //        Console.WriteLine("--- GasData_Process : Invaid Anly_Params.Other in Database!" + DateTime.Now.ToString());
            //        return false;
            //    }

            //    if (dbAnlyCfg.Other.Length >= (Define.ER_LEN + Define.HAP_LEN))
            //    {
            //        byte[] temphap = new byte[Define.HAP_LEN];
            //        Array.Copy(dbAnlyCfg.Other, Define.ER_LEN, temphap, 0, Define.HAP_LEN);
            //        obj = CommonFuncs.BytesToStuct(temphap, typeof(Define.H2OAnlyParam));
            //        if (obj == null)
            //        {
            //            Console.WriteLine("--- GasData_Process : Get Other.H2OAnlyParam from Database Fail!" + DateTime.Now.ToString());
            //            return false;
            //        }
            //        sap.h2o = (WebApplication1.COMMON.Define.H2OAnlyParam)obj;
            //    }

            //    object[] objs = WebApplication1.COMMON.CommonFuncs.BytesToStructArray(dbAnlyCfg.FixedParam, typeof(Define.FixedParam), 7);
            //    if (objs == null)
            //    {
            //        Console.WriteLine("--- GasData_Process : Get FixedParam from Database Fail!" + DateTime.Now.ToString());
            //        return false;
            //    }
            //    for (int i = 0; i < sap.fixList.Length; i++)
            //    {
            //        sap.fixList[i] = (Define.FixedParam)objs[i];
            //    }

            //    anlyParams = sap;
            //}
            #endregion


            ////计算 -- 面积
            //AreaStruct areas = Gas_Process_Step1(deviceID, raw, anlyParams);
            //Console.WriteLine("--- GasData_Process : Calc Gas Area Finish! " + DateTime.Now.ToString());
            ////计算 -- 浓度
            //float[] DenisityList = Gas_Process_Step2(deviceID, areas, anlyParams.GasFix);
            //Console.WriteLine("--- GasData_Process : Calc Gas Denisity Finish! " + DateTime.Now.ToString());

            ////将数据存入数据库
            //RunningState gas = alldata.Content;
            ////gas.DeviceID = deviceID;
            ////gas.ReadDate = sampleTime;
            ////gas.H2 = Convert.ToDecimal(DenisityList[0]);
            ////gas.CO = Convert.ToDecimal(DenisityList[1]);
            ////gas.CH4 = Convert.ToDecimal(DenisityList[2]);
            ////gas.C2H2 = Convert.ToDecimal(DenisityList[3]);
            ////gas.C2H4 = Convert.ToDecimal(DenisityList[4]);
            ////gas.C2H6 = Convert.ToDecimal(DenisityList[5]);
            ////gas.CO2 = Convert.ToDecimal(DenisityList[6]);
            ////gas.Total = Convert.ToDecimal(DenisityList[7]); //总烃
            ////gas.RawData = alldata;                             //原始数据
            ////gas.ProcessedData = areas.ToBytes();
            ////gas.Result = "0";           
            //gas.H2 = DenisityList[0];
            //gas.CO = DenisityList[1];
            //gas.CH4 = DenisityList[2];
            //gas.C2H2 = DenisityList[3];
            //gas.C2H4 = DenisityList[4];
            //gas.C2H6 = DenisityList[5];
            //gas.CO2 = DenisityList[6];
            //gas.TotHyd = DenisityList[7];
            //_data.Content = gas;
            
            //_siml.Update(_data);

           #region 北京测试修改
        //    bool bModify = false;
        //    DateTime dt;
        //    int total;
        //    int donetotal;
        //    int mode;
        //    WebApplication1.BLL.BJCheck bjCheck = new WebApplication1.BLL.BJCheck();
        //    RunningState oldGas = new RunningState();
        //    try
        //    {
        //        if (bjCheck.Exists(gas.DeviceID))
        //        {
        //            if (bjCheck.GetBJCheckData(gas.DeviceID, out dt, out total, out donetotal, out mode))
        //            {
        //                if (mode == 0)      //正常工作
        //                {
        //                    bModify = false;
        //                }
        //                else if (mode == 1) //按次数拷贝
        //                {
        //                    if (total > 0 && donetotal >= 0 && donetotal < total)   //
        //                    {
        //                        WebApplication1.BLL.RunningState RunningState = new BLL.RunningState();
        //                        oldGas = RunningState.GetGasdataByDate((byte)gas.DeviceID, dt);
        //                        bModify = true;
        //                        donetotal = donetotal + 1;
        //                        bjCheck.UpdateBJCheckData(gas.DeviceID, dt, total, donetotal, mode);
        //                    }
        //                }
        //                else if (mode == 2) //一直拷贝
        //                {
        //                    WebApplication1.BLL.RunningState RunningState = new BLL.RunningState();
        //                    oldGas = RunningState.GetGasdataByDate((byte)gas.DeviceID, dt);
        //                    bModify = true;
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        bModify = false;
        //    }

        //    try
        //    {
        //        if (bModify == false)
        //        {
        //            int gasID = new WebApplication1.BLL.RunningState().Add(gas);
        //            gas.ID = gasID;
        //            //Save_H2_ABS(gas);
        //            Console.WriteLine("--- GasData_Process : Write to DB ok! " + DateTime.Now.ToString());
        //        }
        //        else  //有要求修改-北京测试
        //        {
        //            gas.H2 =Convert.ToDecimal( Pec8Domn(float.Parse(oldGas.H2.ToString())));
        //            gas.CO = Convert.ToDecimal(Pec8Domn(float.Parse(oldGas.CO.ToString())));
        //            gas.CH4 = Convert.ToDecimal(Pec8Domn(float.Parse(oldGas.CH4.ToString())));
        //            gas.C2H2 = Convert.ToDecimal(Pec8Domn(float.Parse(oldGas.C2H2.ToString())));
        //            gas.C2H4 = Convert.ToDecimal(Pec8Domn(float.Parse(oldGas.C2H4.ToString())));
        //            gas.C2H6 = Convert.ToDecimal(Pec8Domn(float.Parse(oldGas.C2H6.ToString())));
        //            gas.CO2 = Convert.ToDecimal(Pec8Domn(float.Parse(oldGas.CO2.ToString())));
        //            gas.Total = Convert.ToDecimal(Pec8Domn(float.Parse(oldGas.Total.ToString())));
        //            int gasID = new WebApplication1.BLL.RunningState().Add(gas);
        //            gas.ID = gasID;
        //            //Save_H2_ABS(gas);
        //            Console.WriteLine("--- GasData_Process : ---------------Write to DB ok!-------------------- " + DateTime.Now.ToString());     
        //        }            
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ILog log = new FileLog();
        //        log.Write(ex.Message + "\r\n" + ex.StackTrace);
        //        log.Dispose();
        //        Console.WriteLine("--- GasData_Process : Write to DB fail! " + DateTime.Now.ToString());
        //        Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
        //        return false;
        //    }
           #endregion

            //从数据库 取得告警配置
            try
            {                
                AlarmAll alarmall = cfg.Alarm;
                if (alarmall != null)
                {
                    ////自动告警
                    //if (alarmall.AutoAlarm=="")
                    //{
                    //    List<AlarmMsgAll> msgList = Do_Auto_GasAlarm(deviceID,sampleTime,gas,alarmall,anlyParams.EnviSet);
                    //    //保存
                    //    if (msgList != null && msgList.Count > 0)
                    //    {
                    //        _siml.Update(_data);
                    //    }
                    //}

                    ////自动诊断
                    //if (alarmall.AutoDiagnose=="")
                    //{
                    //    AnlyInformation r = Do_Auto_GasDiagnose(_data,cfg);
                    //    //保存
                    //    if (r != null)
                    //    {
                    //        //保存诊断结果
                    //        _data.AnalyInfo = r;
                    //        _siml.Update(_data);
                    //    }
                    //}
                }
            }
            catch (System.Exception ex)
            {
                //文件记录。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
                //ILog log = new FileLog();
                //log.Write(ex.Message + "\r\n" + ex.StackTrace);
                //log.Dispose();
            }


            //sampTime = sampleTime;      //
            return true;
        }

        

        /// <summary>
        /// 当氢气产生的量高于前5天平均值的15%，提出报警，并将自动工作时间改为4小时一次。
        /// </summary>
        /// <param name="gas"></param>  
        //public static void Save_H2_ABS(RunningState data)

        //{
            
        //    RunningState gas = data;           
        //    try
        //    {
                
                
        //        //数据库操作。。。。。。。。。。。。。。。。。。。。。 

        //        Expression<Func<SIML, IEnumerable<bool>>> ex = p =>(new bool[2]{(p.DevID == _data.DevID),(p.Content.H2 != null)}).AsEnumerable();

        //        IQueryable<SIML>  iq= _siml.FindNBy(ex,6);
        //        bool flag;
        //        float total=0;
        //        float ave = 0;
        //        if (iq.Count() == 6)
        //        {
        //            for (int i = 0; i < 6; i++)
        //            {
        //                total += float.Parse(iq.ElementAt(i).Content.H2.ToString());
        //            }
        //            ave = total / (6 - 1);
        //            if ((float.Parse(iq.ElementAt(0).Content.H2.ToString()) - ave) > (0.15 * ave))
        //            {
        //                flag=true;
        //            }
        //        }
        //        flag=false;

        //        bool H2ABS = flag;
        //        if (H2ABS)
        //        {
        //            AlarmMsgAll msg = new AlarmMsgAll();
        //            msg.AlarmDate = _data.Time_Stamp;
        //            msg.AlarmValue = (decimal)gas.H2;
        //            msg.CompareGasID = gas.ID;
        //            msg.CurDate = gas.ReadDate;
        //            msg.CurGasID = gas.ID;
        //            //msg.DeviceID = data.DevID;
        //            msg.GasName = "H2";
        //            msg.Message = "H2产气速率告警，工作间隔改为4小时";
        //            msg.PrevData = DateTime.Now;
        //            msg.RealValue = (decimal)gas.H2;
        //            //数据库操作。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
        //            Config cfgParam =_ysp.GetCFG(data.DevID);
        //            if (cfgParam == null)
        //            {
        //                //.Config...........................................................................从下位机取配置....................
        //            }                  
                    
        //            if (cfgParam != null)
        //            {
        //                //.............................................................要更改设备的。。。。。。。。。。。。。。。...........
        //                cfgParam.SampStart.interval = 240;                        

        //                //更改数据库中的配置。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
        //                MongoHelper<Config> _cfg=new MongoHelper<Config>();
        //                _cfg.Update(cfgParam);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    { }
        //}
        /// <summary>
        /// 自动告警
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        //public static List<AlarmMsgAll> Do_Auto_GasAlarm(string deviceid, DateTime sampletime, RunningState gas, AlarmAll alarmall, EnvironmentSetting enviset)
        //{
        //    RunningState dg = gas;
        //    if (dg == null)
        //    { 
        //        return null; 
        //    }


        //    //从原始数据中取得 环境配置信息
        //    EnvironmentSetting es = enviset;
        //    if (es == null)
        //    {

        //        //从数据库读取 自动告警配置、阈值设置          
        //        AlarmAll aa = alarmall;
        //        if (aa == null)
        //        { return null; }
        //        DateTime st = sampletime;
        //        if (st == null)
        //        { return null; }


        //        //取得 对比数据
        //        int interval = aa.Interval;
        //        TimeSpan ts = new TimeSpan(interval, 0, 0, 0);
        //        DateTime time = st - ts;
        //        //数据库操作。。。。。。。。。。。。。。。。。。。。。。。。。。取时间为time的气体数据。。。。。。。

        //        Expression<Func<SIML, bool>> ex = p => (p.DevID == deviceid && p.Raw.SampleTime == time);
                
        //        SIML prevData = _siml.FindOneBy(ex);
        //        if (prevData == null)
        //        {
        //            Console.WriteLine("auto alarm: do alarm with before {0} data, not exist", time);
        //            return null;
        //        }
        //        Console.WriteLine("auto alarm: do alarm with {0} data", prevData.Raw.SampleTime);

        //        //
        //        List<AlarmMsgAll> almList = GasAlarm(dg, prevData.Content, prevData.Content, es, aa);
        //        if (almList != null)
        //        {
        //            Console.WriteLine("auto alarm: {0} items alarm message", almList.Count);
        //            //for (int i=0; i < almList.Count; i++)
        //            //{
        //            //    Console.WriteLine(almList[i].Message);
        //            //}
        //        }
        //        else
        //        {
        //            Console.WriteLine("auto alarm: no alarm message");
        //        }
        //        return almList;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// 自动诊断
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        //public static AnlyInformation Do_Auto_GasDiagnose(RunningState data,Config cfg)
        //{
        //    RunningState gasData = data;
        //    //从原始数据中取得 环境配置信息
        //    EnvironmentSetting es = cfg.AnalyPara.EnviSet;
        //    if(es==null)    {   return null;    }

        //    //从数据库读取 自动告警配置、阈值设置
        //    AlarmAll aa = cfg.Alarm;
        //    if (aa == null)
        //    { return null; }
        //    //Attention at = new WebApplication1.BLL.Attention().GetModel(dg.DeviceID);
        //    //if (at == null)
        //    //{ return null; }
        //    //AutoAlarm auto = new WebApplication1.BLL.AutoAlarm().GetModel(dg.DeviceID);
        //    //if (auto == null)
        //    //{ return null; }


        //    //取得 对比数据
        //    int interval = aa.Interval;
        //    DateTime time = data.Raw.SampleTime - new TimeSpan(interval, 0, 0, 0);
        //    //去数据库中，时间为time的气体数据。。。。。。。。。。。。。。。。。。。。。。。。。。。
        //    Expression<Func<SIML,bool>> ex=p=>(p.DevID==data.DevID &&p.Raw.SampleTime==time);
        //        MongoHelper<SIML> _siml=new MongoHelper<SIML>();
        //       SIML prevData= _siml.FindOneBy(ex);
        //    if (prevData == null)
        //    {
        //        Console.WriteLine("auto diag: do diag with before {0} data, not exist", time);
        //        return null;
        //    }
        //    Console.WriteLine("auto diag: do diag with {0} data", prevData.Content.ReadDate);

        //    //
        //    AnlyInformation anlyInfo = GasDiagnose(data.DevID, gasData, prevData.Content, prevData.Content, es, aa.DiagSet);
        //    if (anlyInfo != null)
        //    {
        //        Console.WriteLine("auto diag: {0}", anlyInfo.Desc);
        //    }
        //    else
        //    {
        //        Console.WriteLine("auto diag: no message");
        //    }
        //    return anlyInfo;
        //}


        /// <summary>
        /// 处理微水数据
        /// </summary>
        /// <param name="data"></param>
        //public static bool H2oData_Process(SIML data)
        //{
        //    string deviceID = data.DevID;
        //    RawData h2o = data.Raw;

        //    if (data == null)
        //    {
        //        return false;
        //    }

        //    if (h2o.RawAW == 0 || h2o.RawT == 0)
        //    { return false; }
        //    if (h2o.SampleTime == null)
        //    { return false; }

        //    float temp_t = float.Parse(h2o.RawT.ToString());
        //    float temp_aw = float.Parse(h2o.RawAW.ToString());
        //    //float temp_ppm = float.Parse
        //    /*
        //    //float aw = (float)(2.0625 * h2o.RawAW.Value / 4096 - 0.25);
        //    float aw = (float)(2.0625f * temp_aw / 4096f - 0.25f);
        //    //float t = (float)4125 / 65536 * ((float.Parse(h2o.RawT.Value.ToString()))) - 56.25; 
        //    float t = (4125f / 65536f) * temp_t - 56.25f;
        //    //float t =(float) h2o.RawT.Value;
        //    //获取油品系数A和B
        //    Define.EnvironmentSetting ES = h2o.Analysis.Value.enviroment;
        //    float A = CommonFuncs.ConvertBS(ES.oilFactorA);
        //    float B = CommonFuncs.ConvertBS(ES.oilFactorB);

        //    float ppm = (float)(aw * Math.Pow(10, (B - A / (t + 273.16))));
        //     * */

        //    //奥特迅微水传感器计算方法

        //    //计算AW：aw=（x-4)/16
        //    float aw = (temp_aw * 0.006713f - 4.0f) / 16.0f;
        //    //计算T：t =(x-4)/0.133-40
        //    float t = (temp_t * 0.006713f - 4.0f) / 0.133f - 48.0f;
        //    //计算Mst：A=126.8-t；B=95.176+0.18t；ppm=2.5212*aw*t*(B/A)
        //    float ppm = 2.5212f * aw * t * ((95.176f + 0.18f * t) / (126.8f - t));


        //    //微水数据存入数据库。。。。。。。。。。。。。。。。。。。。。。。。。。                 
        //    data.Content.ReadDate = h2o.SampleTime;
        //    data.Content.AW = aw;
        //    data.Content.T =t;
        //    data.Content.Mst =ppm;
        //    //dataH2O.ConfigInfo =h2o.Config.Value;
             
        //    //dataH2O.Result = "0";
        //    try
        //    {
        //        //保存
                
        //        Expression<Func<SIML, bool>> ex = p => (p.Content.AW == aw && p.Content.T == t && p.Content.Mst == ppm);
        //        _siml.Update(data);

        //    }
        //    catch (Exception ex)
        //    {
        //        //ILog log = new FileLog();
        //        //log.Write(ex.Message + "\r\n" + ex.StackTrace);
        //        //log.Dispose();
        //        //return false;
        //    }

        //    try
        //    {
        //        //从数据库 取得告警配置
        //        //自动告警
        //        AlarmAll aa = _ysp.GetCFG(data.DevID).Alarm;               
        //        if (aa!= null)
        //        {
        //            if (aa.AutoAlarm!=null)
        //            {
        //                //微水注意值告警
        //                GasAlarm at = aa.TAlarm;
        //                if (at != null)
        //                {
        //                    List<AlarmMsgAll> list = Do_Auto_H2OAlarm(aa, data.Content);
        //                    if (list != null && list.Count > 0)
        //                    {
        //                        //保存告警信息
                                
        //                        List<AlarmMsgAll> alarmList=data.AlarmMsg.ToList();
        //                        alarmList.Concat(list);
        //                        data.AlarmMsg = alarmList.ToArray();
        //                        _siml.Update(data);
                                
                               
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //ILog log = new FileLog();
        //        //log.Write(ex.Message + "\r\n" + ex.StackTrace);
        //        //log.Dispose();
        //    }

        //    //sampTime = h2o.SampleTime;
        //    return true;
        //}

        /// <summary>
        /// 微水数据 与 注意值对比，产生告警
        /// </summary>
        /// <param name="at"></param>
        /// <param name="h2o"></param>
        /// <returns></returns>
        //public static List<AlarmMsgAll> Do_Auto_H2OAlarm(AlarmAll aa,RunningState cd)
        //{

        //    if (aa == null)
        //    {   return null;    }

        //    //if (at.Threshold == null)
        //    //{   return null;    }
        //    //object obj = CommonFuncs.BytesToStuct(at.Threshold, typeof(Threshold));
        //    //if (obj == null)
        //    //{ return null; }
        //    //Threshold hold = (Threshold)obj;

        //    AlarmAll hold = aa;
        //    RunningState h2o = cd;

        //    List<AlarmMsgAll> list = new List<AlarmMsgAll>();

        //    if (cd.AW >= hold.AWAlarm.Level1 && cd.AW < hold.AWAlarm.Level2)
        //    {
        //        AlarmMsgAll msg = new AlarmMsgAll();

        //        msg.Type = (int)AlarmType.Content;
        //        msg.CurGasID = h2o.ID;
        //        msg.AlarmDate = h2o.ReadDate;
        //        msg.AlarmValue = (decimal)hold.AWAlarm.Level1;
        //        msg.GasName = "H2O_AW";
        //        msg.Message = "H2O_AW Threshold_Alarm";
        //        msg.RealValue = (decimal)cd.AW;
        //        msg.CurDate = h2o.ReadDate;
        //        msg.Level = 1;
        //        msg.PrevData = DateTime.Now;
        //        list.Add(msg);
        //    }
        //    else if (cd.AW >= hold.AWAlarm.Level2)
        //    {
        //        AlarmMsgAll msg = new AlarmMsgAll();

        //        msg.Type = (int)AlarmType.Content;
        //        msg.CurGasID = h2o.ID;
        //        msg.AlarmDate = h2o.ReadDate;
        //        msg.AlarmValue = (decimal)hold.AWAlarm.Level2;
        //        msg.GasName = "H2O_AW";
        //        msg.Message = "H2O_AW Threshold_Alarm";
        //        msg.RealValue = (decimal)cd.AW;
        //        msg.CurDate = h2o.ReadDate;
        //        msg.Level = 2;
        //        msg.PrevData = DateTime.Now;
        //        list.Add(msg);
        //    }

        //    if (cd.T >= hold.TAlarm.Level1 && h2o.T < hold.TAlarm.Level2)
        //    {
        //        AlarmMsgAll msg = new AlarmMsgAll();

        //        msg.Type = (int)AlarmType.Content;
        //        msg.CurGasID = h2o.ID;
        //        msg.AlarmDate = h2o.ReadDate;
        //        msg.AlarmValue = (decimal)hold.TAlarm.Level1;
        //        msg.GasName = "H2O_T";
        //        msg.Message = "H2O_T Threshold_Alarm";
        //        msg.RealValue = (decimal)cd.T;
        //        msg.CurDate = h2o.ReadDate;
        //        msg.Level = 1;
        //        msg.PrevData = DateTime.Now;
        //        list.Add(msg);
        //    }
        //    else if (cd.T >= hold.TAlarm.Level2)
        //    {
        //        AlarmMsgAll msg = new AlarmMsgAll();
 
        //        msg.Type = (int)AlarmType.Content;
        //        msg.CurGasID = h2o.ID;
        //        msg.AlarmDate = h2o.ReadDate;
        //        msg.AlarmValue = (decimal)hold.TAlarm.Level2;
        //        msg.GasName = "H2O_T";
        //        msg.Message = "H2O_T Threshold_Alarm";
        //        msg.RealValue = (decimal)cd.T;
        //        msg.CurDate = h2o.ReadDate;
        //        msg.Level = 2;
        //        msg.PrevData = DateTime.Now;
        //        list.Add(msg);
        //    }

        //    if (cd.Mst >= hold.PPMAlarm.Level1 && cd.Mst < hold.PPMAlarm.Level2)
        //    {
        //        AlarmMsgAll msg = new AlarmMsgAll();
 
        //        msg.Type = (int)AlarmType.Content;
        //        msg.CurGasID = h2o.ID;
        //        msg.AlarmDate = h2o.ReadDate;
        //        msg.AlarmValue = (decimal)hold.PPMAlarm.Level1;
        //        msg.GasName = "H2O_Mst";
        //        msg.Message = "H2O_Mst Threshold_Alarm";
        //        msg.RealValue = (decimal)cd.Mst;
        //        msg.CurDate = h2o.ReadDate;
        //        msg.Level = 1;
        //        msg.PrevData = DateTime.Now;
        //        list.Add(msg);
        //    }
        //    else if (cd.Mst >= hold.PPMAlarm.Level2)
        //    {
        //        AlarmMsgAll msg = new AlarmMsgAll();
     
        //        msg.Type = (int)AlarmType.Content;
        //        msg.CurGasID = h2o.ID;
        //        msg.AlarmDate = h2o.ReadDate;
        //        msg.AlarmValue = (decimal)hold.PPMAlarm.Level2;
        //        msg.GasName = "H2O_Mst";
        //        msg.Message = "H2O_Mst Threshold_Alarm";
        //        msg.RealValue = (decimal)cd.Mst;
        //        msg.CurDate = h2o.ReadDate;
        //        msg.Level = 2;
        //        msg.PrevData = DateTime.Now;
        //        list.Add(msg);
        //    }

        //    return list;
        //}




        /// <summary>
        /// 告警判断 及 生成
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static List<AlarmMsgAll> GasAlarm(RunningState dg, RunningState absdg, RunningState reldg,
            EnvironmentSetting setting, AlarmAll hold)
        {
            if (dg == null || absdg == null || reldg == null || setting == null || hold == null)
            { return null; }
            if (dg.DevID != absdg.DevID || dg.DevID != reldg.DevID)
            { return null; }

            string devId = dg.DevID.ToString();

            //取得 油总量 和 油密度
            float G = setting.oilTotal;  //用油总量
            float p = setting.oilDensity; //用油密度
            if (p < MIN || G < MIN)     
            { return null; }

            //准备数据
            float[] data = new float[8];
            float[] data_abs = new float[8];
            float[] data_rel = new float[8];
            data[0] = (float)dg.H2; data[1] = (float)dg.CO; data[2] = (float)dg.CH4;
            data[3] = (float)dg.C2H2; data[4] = (float)dg.C2H4; data[5] = (float)dg.C2H6;
            data[6] = (float)dg.CO2; data[7] = (float)(dg.TotHyd);
            data_abs[0] = (float)absdg.H2; data_abs[1] = (float)absdg.CO; data_abs[2] = (float)absdg.CH4;
            data_abs[3] = (float)absdg.C2H2; data_abs[4] = (float)absdg.C2H4; data_abs[5] = (float)absdg.C2H6;
            data_abs[6] = (float)absdg.CO2; data_abs[7] = (float)(absdg.TotHyd);
            data_rel[0] = (float)reldg.H2; data_rel[1] = (float)reldg.CO; data_rel[2] = (float)reldg.CH4;
            data_rel[3] = (float)reldg.C2H2; data_rel[4] = (float)reldg.C2H4; data_rel[5] = (float)reldg.C2H6;
            data_rel[6] = (float)reldg.CO2; data_rel[7] = (float)(reldg.TotHyd);

            //计算绝对产气速率
            int[] absReason = null ;
            float[] absSpeed = Gas_Anly_Step1_AbsSpeed(out absReason, devId, data, dg.ReadDate, data_abs, absdg.ReadDate, G, p);
            int[] relReason = null;
            float[] relSpeed = Gas_Anly_Step1_RelSpeed(out relReason, devId, data, dg.ReadDate, data_rel, reldg.ReadDate);


            List<AlarmMsgAll> msgList = new List<AlarmMsgAll>();
            string[] gasName = { "H2", "CO", "CH4", "C2H2", "C2H4", "C2H6", "CO2", "CXHX" };
            string alarmDesc = "";
            float[] lvValue = new float[8];
            byte[] alm = null;
            
            //获得Mst告警信息            //包装告警信息
            alm = Gas_Anly_Step2_ThresholdAlarm(dg.ID, hold, data, 0, ref lvValue);
            alarmDesc = "Mst Threshold Alarm";
            for (int i = 0; i < 8 && alm[i] > 0; i++)
            {
                AlarmMsgAll msg = new AlarmMsgAll();
          
                msg.Type = (int)AlarmType.Content;
                msg.CurGasID = dg.ID;
                msg.AlarmDate = DateTime.Now;   //    
                
                msg.CompareGasID = dg.ID;
                msg.AlarmValue = Convert.ToDecimal(lvValue[i]);
                msg.GasName = gasName[i];
                msg.RealValue = Convert.ToDecimal(data[i]);
                msg.CurDate = dg.ReadDate;
                msg.PrevData = dg.ReadDate;
                msg.Level = (int)(alm[i]);
                msg.Message = msg.GasName + ", " + alarmDesc + ", L" + msg.Level.ToString();
                msgList.Add(msg);
            }
            //获得绝对产气速率告警信息            //包装告警信息
            alm = Gas_Anly_Step2_ThresholdAlarm(dg.ID, hold, absSpeed, 1, ref lvValue);
            alarmDesc = "Absolute_Gas_Generation_Speed Threshold Alarm";
            for (int i = 0; i < 8 && alm[i] > 0; i++)
            {
                AlarmMsgAll msg = new AlarmMsgAll();
    
                msg.Type = (int)AlarmType.Absolute;
                msg.CurGasID = dg.ID;
                msg.AlarmDate = DateTime.Now;   //
                msg.CompareGasID = absdg.ID;
                msg.AlarmValue = Convert.ToDecimal(lvValue[i]);
                msg.GasName = gasName[i];
                msg.RealValue = Convert.ToDecimal(absSpeed[i]);
                msg.CurDate = dg.ReadDate;
                msg.PrevData = absdg.ReadDate;
                msg.Level = (int)(alm[i]);
                msg.Message = msg.GasName + ", " + alarmDesc + ", L" + msg.Level.ToString();
                msgList.Add(msg);
            }
            //获得相对产气速率告警信息            //包装告警信息
            alm = Gas_Anly_Step2_ThresholdAlarm(dg.ID, hold, relSpeed, 2, ref lvValue);
            alarmDesc = "Relative_Gas_Generation_Speed Threshold Alarm";
            for (int i = 0; i < 8 && alm[i] > 0; i++)
            {
                AlarmMsgAll msg = new AlarmMsgAll();

                msg.Type = (int)AlarmType.Relative;
                msg.CurGasID = dg.ID;
                msg.AlarmDate = DateTime.Now;   //
                msg.CompareGasID = reldg.ID;
                msg.AlarmValue = Convert.ToDecimal(lvValue[i]);
                msg.GasName = gasName[i];
                msg.RealValue = Convert.ToDecimal(relSpeed[i]);
                msg.CurDate = dg.ReadDate;
                msg.PrevData = reldg.ReadDate;
                msg.Level = (int)(alm[i]);
                msg.Message = msg.GasName + ", " + alarmDesc + ", L" + msg.Level.ToString();
                msgList.Add(msg);
            }

            return msgList;
        }

        /// <summary>
        /// 故障诊断 及 生成
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static AnlyInformation GasDiagnose(String devId, RunningState dg, RunningState absdg, RunningState reldg,
            EnvironmentSetting setting, AlarmAll aa)
        {
            if (dg == null || absdg == null || reldg == null || setting == null || aa == null)
            { return null; }
            //if (dg.DeviceID != absdg.DeviceID || dg.DeviceID != reldg.DeviceID)
            //{ return null; }

            //byte devId = (byte)dg.DeviceID;

            //取得 油总量 和 油密度
            float G = setting.oilTotal;   //用油总量
            float p = setting.oilDensity; //用油密度
            if (p < MIN || G < MIN)
            { return null; }

            //准备数据
            float[] data = new float[8];
            float[] data_abs = new float[8];
            float[] data_rel = new float[8];
            data[0] = (float)dg.H2; data[1] = (float)dg.CO; data[2] = (float)dg.CH4;
            data[3] = (float)dg.C2H2; data[4] = (float)dg.C2H4; data[5] = (float)dg.C2H6;
            data[6] = (float)dg.CO2; data[7] = (float)(dg.TotHyd);
            data_abs[0] = (float)absdg.H2; data_abs[1] = (float)absdg.CO; data_abs[2] = (float)absdg.CH4;
            data_abs[3] = (float)absdg.C2H2; data_abs[4] = (float)absdg.C2H4; data_abs[5] = (float)absdg.C2H6;
            data_abs[6] = (float)absdg.CO2; data_abs[7] = (float)(absdg.TotHyd);
            data_rel[0] = (float)reldg.H2; data_rel[1] = (float)reldg.CO; data_rel[2] = (float)reldg.CH4;
            data_rel[3] = (float)reldg.C2H2; data_rel[4] = (float)reldg.C2H4; data_rel[5] = (float)reldg.C2H6;
            data_rel[6] = (float)reldg.CO2; data_rel[7] = (float)(reldg.TotHyd);

            //计算绝对产气速率
            int[] absReason = null;
            int[] relReason = null;
            float[] absSpeed = Gas_Anly_Step1_AbsSpeed(out absReason, devId, data, dg.ReadDate, data_abs, absdg.ReadDate, G, p);
            float[] relSpeed = Gas_Anly_Step1_RelSpeed(out relReason, devId, data, dg.ReadDate, data_rel, reldg.ReadDate);

            //自动诊断
            try
            {
                //判断是否故障
                bool fault = Gas_Anly_Step2_FaultChk(int.Parse(devId), aa, data, absSpeed, relSpeed);
                if (fault == true)
                {
                    //判断故障类型
                    AnlyInformation anlyResult = Gas_Anly_Step3_FaultDiagnosis(data);
                    return anlyResult;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }

            return null;
        }

        public static DiagnoseResult WrapperAnlyResult(string devId, int gasId, AnlyInformation anlyResult)
        {
            if (anlyResult == null)
            { return null; }
           

            DiagnoseResult dr = new DiagnoseResult();
            dr.GasID = gasId;
            dr.DiagnoseDate = DateTime.Now;
            dr.DeviceID = devId;

            string str = "";
            //三比值编码
            str = "C2H2/C2H4=" + anlyResult.threevar.Var1.ToString() + ", ";
            str += "CH4/H2=" + anlyResult.threevar.Var2.ToString() + ", ";
            str += "C2H4/C2H6=" + anlyResult.threevar.Var3.ToString();
            dr.ThreeRatioCode = str;
            dr.ThreeRatioResult = anlyResult.threevar.Desc;
            //大卫三角形编码
            //"C2H2:10%, C2H4:20%, CH4:30%"
            str = "C2H2:" + (Math.Round(anlyResult.devidInfo.percent_C2H2, 3) * 100).ToString() + "%, ";
            str += "C2H4:" + (Math.Round(anlyResult.devidInfo.percent_C2H4, 3) * 100).ToString() + "%, ";
            str += "CH4:" + (Math.Round(anlyResult.devidInfo.percent_CH4, 3) * 100).ToString() + "%";
            dr.DevidCode = str;
            dr.DevidResult = anlyResult.devidInfo.Desc;
            //立体图示法编码
            //"C2H2/C2H4=1, C2H4/C2H6=1, CH4/H2=1"
            str = "C2H2/C2H4=" + Math.Round(anlyResult.ratio.C2H2_C2H4, 3).ToString() + ", ";
            str += "C2H4/C2H6=" + Math.Round(anlyResult.ratio.C2H4_C2H6, 3).ToString() + ", ";
            str += "CH4/H2= " + Math.Round(anlyResult.ratio.CH4_H2, 3).ToString();
            dr.CubeCode = str;
            //
            dr.Result = anlyResult.Desc;

            return dr;
        }




       
    }
}
