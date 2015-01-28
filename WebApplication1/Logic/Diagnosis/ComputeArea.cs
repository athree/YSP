using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using System.IO;
using MathWorks.MATLAB.NET.Arrays;


namespace WebApplication1.Diagnosis
{
    
    
    
    public class AreaStruct
    {
        
        public float[] ProcessedPoints;                 //经过处理的数据
        
        public float[] Areas;                           //峰的面积
        
        public ushort[] PeakLeft;                       //峰的左起点
        
        public ushort[] PeakMid;                        //峰的中点
        
        public ushort[] PeakRight;                      //峰的右端点
        
        public float Co2Areas;                          //CO2的面积

        
        public byte[] ToBytes()
        {
            using(MemoryStream m = new MemoryStream())
            {
                BinaryFormatter f = new BinaryFormatter();
                f.Serialize(m, this);

                return m.ToArray();
            }
        }

        
        public static AreaStruct BytesToStruct(byte[] bytes)
        {
            using (MemoryStream m = new MemoryStream(bytes))
            {
                BinaryFormatter f = new BinaryFormatter();
                return (AreaStruct)f.Deserialize(m);
            }
        }
    }

    public class ComputeArea
    {
        private const int _peekNum = 6;                           //峰个数
        private double[,] _rawData;
        private int _len;
        private double[] _mid = new double[_peekNum];
        private double[] _left = new double[_peekNum];
        private double[] _right = new double[_peekNum];

        private double[] MinLYGrade = { 2, 2, 2, 2, 2, 2 };
        private double[] MaxLXGrade = { 40, 40, 60, 100, 100, 100 };
        private double[] MinRYGrade = { 2, 2, 2, 2, 2, 2 };
        private double[] MaxRXGrade = { 50, 50, 90, 110, 110, 110 };
        private double[] MaxHighWidth = { 10, 10, 10, 30, 30, 30 };

        

        public ComputeArea(float[] rawData, int datalen, ushort[] peekMid, ushort[] peekLeft, ushort[] peekRight )
        {
            Debug.Assert(rawData != null && peekLeft != null && peekMid != null && peekRight != null);

            if (rawData == null || peekLeft == null || peekMid == null || peekRight == null || rawData.Length != datalen)
            {
                throw new Exception("Invalid ComputeArea_function Parameters！");
            }

            _len = datalen;
            _rawData = new double[_len, 1];
            for (int i = 0; i < datalen; i++ )
            {
                _rawData[i, 0] = rawData[i];
            }

            peekMid.CopyTo(_mid, 0);
            peekLeft.CopyTo(_left, 0);
            peekRight.CopyTo(_right, 0);
        }


        public ComputeArea(ushort[] rawData, int datalen, ushort[] peekMid, ushort[] peekLeft, ushort[] peekRight,
            ushort[] ly, ushort[] lx, ushort[] ry, ushort[] rx, ushort[] width)
        {
            Debug.Assert(rawData != null && peekLeft != null && peekMid != null && peekRight != null);
            Debug.Assert(ly != null && lx != null && ry != null && rx != null && width != null);
            
            if (rawData == null || peekLeft == null || peekMid == null || peekRight == null || 
                rawData.Length != datalen || ly == null || lx == null || ry == null ||
                rx == null || width == null)
            {
                throw new Exception("Invalid ComputeArea_function Parameters！");
            }

            _len = datalen;
            _rawData = new double[_len, 1];
            for (int i = 0; i < datalen; i++ )
            {
                _rawData[i, 0] = rawData[i];
            }

            peekMid.CopyTo(_mid, 0);
            peekLeft.CopyTo(_left, 0);
            peekRight.CopyTo(_right, 0);
            ly.CopyTo(MinLYGrade, 0);
            lx.CopyTo(MaxLXGrade, 0);
            ry.CopyTo(MinRYGrade, 0);
            rx.CopyTo(MaxRXGrade, 0);
            width.CopyTo(MaxHighWidth, 0);
        }

       
        
        public AreaStruct Compute()
        {


            SePuAnly.SePuAnly analysis = new SePuAnly.SePuAnly();

            //sw = new StreamWriter("c:\\time_flag.txt");
            //sw.Write("2");
            //sw.Close();
            

            //输入参数

            //模式，0：运行模式，1：调试模式
            MWNumericArray mode = 0;
            //if (GlobalVar.g_FengCaclDebug == true)
            //{
            //    mode = 1;
            //}

            MWNumericArray m = _mid;
            MWNumericArray l = _left;
            MWNumericArray r = _right;

            MWNumericArray ly = MinLYGrade;
            MWNumericArray lx = MaxLXGrade;
            MWNumericArray ry = MinRYGrade;
            MWNumericArray rx = MaxRXGrade;
            MWNumericArray width = MaxHighWidth;

            MWNumericArray data = _rawData;

            //返回值
            MWArray[] output = new MWArray[5];


            //DLL函数调用

            output = analysis.fun_SePuAnly(output.Length, mode, data, (MWNumericArray)_len, 
                (MWNumericArray)_peekNum, m, l, r, ly, lx, ry, rx, width);

            //返回值转化
            AreaStruct ret = new AreaStruct();
            double[,] convertData;

            //处理后的数据
            MWNumericArray temp = output[0] as MWNumericArray;
            convertData = (double[,])temp.ToArray(MWArrayComponent.Real);
            ret.ProcessedPoints = new float[convertData.Length];
            for (int i = 0; i < convertData.Length; i++ )
            {
                ret.ProcessedPoints[i] = Convert.ToSingle(convertData[i, 0]);
            }

            //峰的面积
            temp = output[1] as MWNumericArray;
            convertData = (double[,])temp.ToArray(MWArrayComponent.Real);
            ret.Areas = new float[convertData.Length];
            for (int i = 0; i < convertData.Length; i++)
            {
                ret.Areas[i] = Convert.ToSingle(convertData[0, i]);
            }

            //峰的左边界
            temp = output[2] as MWNumericArray;
            convertData = (double[,])temp.ToArray(MWArrayComponent.Real);
            ret.PeakLeft = new ushort[convertData.Length];
            for (int i = 0; i < convertData.Length; i++ )
            {
                ret.PeakLeft[i] = Convert.ToUInt16(convertData[0, i]);
            }

            //峰的最高点
            temp = output[3] as MWNumericArray;
            convertData = (double[,])temp.ToArray(MWArrayComponent.Real);
            ret.PeakMid = new ushort[convertData.Length];
            for (int i = 0; i < convertData.Length; i++)
            {
                ret.PeakMid[i] = Convert.ToUInt16(convertData[0, i]);
            }

            //峰的最右点
            temp = output[4] as MWNumericArray;
            convertData = (double[,])temp.ToArray(MWArrayComponent.Real);
            ret.PeakRight = new ushort[convertData.Length];
            for (int i = 0; i < convertData.Length; i++)
            {
                ret.PeakRight[i] = Convert.ToUInt16(convertData[0, i]);
            }
            
            analysis.Dispose();
          
            return ret;
        }

        public static float Compute_Co2(ushort[] co2SampleData, int co2Left, int co2Right)
        {
            float ret =0.0f;

            if(co2SampleData == null)
            { return ret; }
            if (co2Left>=co2Right || co2Left > co2SampleData.Length || co2Right > co2SampleData.Length)
            {   return ret; }

            //CO2浓度就是出峰区间内的最大值
            ushort max = co2SampleData[co2Left];
            for (int i = co2Left; i < co2Right; i++)
            {
                if (co2SampleData[i] >= max)
                {
                    max = co2SampleData[i];
                }
            }

            ret = (float)max;

            return ret;
        }
        //public void theout(object source, System.Timers.ElapsedEventArgs e)
        //{
        //    process();
        //}
        //private static string Path = "D:\\Oil_Chromatograph_Monitor\\DMS\\DMSApp.exe";
        //private static string AppName = "DMSApp";
        //public static void process()
        //{
        //    string s;
        //    int ProgressCount = 0123456;//判断进程是否运行的标识
        //    Process[] prc = Process.GetProcesses();
        //    string time_flag_str = File.ReadAllText("c:\\time_flag.txt");
        //    if (time_flag_str == "1")
        //    {
        //        foreach (Process pr in prc) //遍历整个进程
        //        {
        //            s = pr.ProcessName;
        //            if (AppName == pr.ProcessName) //如果进程存在
        //            {
                        
        //                ProgressCount = 0; //计数器清空
        //                return;
        //            }

        //        }
        //        if (ProgressCount != 0)//如果计数器不为0，说名所指定程序没有运行
        //        {
        //            start_process(Path);

        //        }
        //    }
        //}
        //public static void start_process(string AppPath)
        //{
        //    System.Diagnostics.Process.Start(AppPath);
        //}

       

    }
}
