using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using IMserver.Models;
using IMserver.Models.SimlDefine;


namespace WebApplication1.Diagnosis
{
    /// <summary>
    /// 三比值法
    /// </summary>
    public class ThreeRatio
    {
        private const decimal MIN = 0.00000000000001m;
        //定义出错信息
        private const string ANLYINFO_PD = "Partial Discharge!^设备故障 局部放电";
        private const string ANLYINFO_LD = "Low power discharge!^设备故障 低能放电";
        private const string ANLYINFO_HD = "High power discharge!^设备故障 高能放电";
        private const string ANLYINFO_T1 = "Temperature lower than 150c!^设备故障 低温过热（低于150度）";
        private const string ANLYINFO_T2 = "Temperature 150c to 300c!^设备故障 低温过热（150度-300度）";
        private const string ANLYINFO_T2A = "Temperature lower than 300c!^设备故障 低温过热（低于300度）";
        private const string ANLYINFO_T3 = "Temperature 300c to 700c!^设备故障 中温过热（300度-700度）";
        private const string ANLYINFO_T4 = "Temperature higher than 700c!^设备故障 高温过热（高于700度）";
        private const string ANLYINFO_DT = "Low power discharge and Temperature too high!^设备故障 低能放电兼过热";
        private const string ANLYINFO_AD = "Arc discharge!^设备故障 电弧放电";
        private const string ANLYINFO_ADT = "Arc discharge and Temperature too high!^设备故障 电弧放电兼过热";

        private const string ANLYINFO_UNKNOWN = "Device Error, Unkown!^设备故障 未知";


        public static void Diagnose(decimal H2, decimal CH4, decimal C2H2, decimal C2H4, decimal C2H6, ref AnlyInformation info)
        {
            //if (Math.Abs(H2) < MIN || Math.Abs(CH4) < MIN || Math.Abs(C2H2) < MIN || Math.Abs(C2H4) < MIN || Math.Abs(C2H6) < MIN)
            //{
            //    return;
            //}
            
            #region  高亮
            /*
            info = new AnlyInfo();
            info.devidInfo = new DevidTriAnlyInfo();
            info.devidInfo.Desc = "";
            info.ratio = new Ratio();
            info.threevar = new ThreeValue();
            info.threevar.Desc = "";
            info.chDesc = "";
            info.enDesc = "";
            info.Desc = "";




            //----------立体图示法（每一个比值的范围 0-10，10代表无穷大，参考GB/T 7252-2001 附录E）
            info.ratio.C2H2_C2H4 = info.ratio.C2H4_C2H6 = info.ratio.CH4_H2 = -1.0m;        //-1.0代表无法计算判断

            bool canCalc = true;
            if ((Math.Abs(C2H4) < MIN && Math.Abs(C2H2) < MIN)
                || (Math.Abs(C2H6) < MIN && Math.Abs(C2H4) < MIN)
                || (Math.Abs(H2) < MIN && Math.Abs(CH4) < MIN))         // 0.0/0.0 无意义
            {
                canCalc = false;
            }

            if (canCalc)
            {
                if (Math.Abs(C2H4) >= MIN)       //if(C2H4!=0)
                {
                    info.ratio.C2H2_C2H4 = C2H2 / C2H4;
                }
                else
                {
                    info.ratio.C2H2_C2H4 = 10.0m;
                }
                if (info.ratio.C2H2_C2H4 > 10.0m)
                {
                    info.ratio.C2H2_C2H4 = 10.0m;
                }

                if (Math.Abs(C2H6) >= MIN)       //if(C2H6!=0)
                {
                    info.ratio.C2H4_C2H6 = C2H4 / C2H6;
                }
                else
                {
                    info.ratio.C2H4_C2H6 = 10.0m;
                }
                if (info.ratio.C2H4_C2H6 > 10.0m)
                {
                    info.ratio.C2H4_C2H6 = 10.0m;
                }

                if (Math.Abs(H2) >= MIN)       //if(C2H6!=0)
                {
                    info.ratio.CH4_H2 = CH4 / H2;
                }
                else
                {
                    info.ratio.CH4_H2 = 10.0m;
                }
                if (info.ratio.CH4_H2 > 10.0m)
                {
                    info.ratio.CH4_H2 = 10.0m;
                }
            }


            //----------三比值法
                //根据各气体浓度值编码
            int[] a = new int[3];
            a[0] = a[1] = a[2] = -1;       //-1表示相应比值无效或无意义

            canCalc = true;
            if ((Math.Abs(C2H2) < MIN && Math.Abs(C2H4) < MIN)
                || (Math.Abs(CH4) < MIN && Math.Abs(H2) < MIN)
                || (Math.Abs(C2H4) < MIN && Math.Abs(C2H6) < MIN))         // 0.0/0.0 无意义
            {
                canCalc = false;
            }

            if (canCalc)
            {
                if (Math.Abs(C2H4) < MIN)       //c2h4==0
                {
                    a[0] = 2;
                }
                else
                {
                    if (C2H2 / C2H4 < 0.1m)
                    {
                        a[0] = 0;
                    }
                    if (C2H2 / C2H4 >= 0.1m && C2H2 / C2H4 < 3.0m)
                    {
                        a[0] = 1;
                    }
                    if (C2H2 / C2H4 >= 3.0m)
                    {
                        a[0] = 2;
                    }
                }

                if (Math.Abs(H2) < MIN)       //H2==0
                {
                    a[1] = 2;
                }
                else
                {
                    if (CH4 / H2 < 0.1m)
                    {
                        a[1] = 1;
                    }
                    if (CH4 / H2 < 1.0m && CH4 / H2 >= 0.1m)
                    {
                        a[1] = 0;
                    }
                    if (CH4 / H2 >= 1.0m)
                    {
                        a[1] = 2;
                    }
                }

                if (Math.Abs(C2H6) < MIN)       //C2H6==0
                {
                    a[2] = 2;
                }
                else
                {
                    if (C2H4 / C2H6 < 1.0m)
                    {
                        a[2] = 0;
                    }
                    if (C2H4 / C2H6 < 3.0m && C2H4 / C2H6 >= 1.0m)
                    {
                        a[2] = 1;
                    }
                    if (C2H4 / C2H6 >= 3.0m)
                    {
                        a[2] = 2;
                    }
                }
            }
            info.threevar.Var1 = a[0];
            info.threevar.Var2 = a[1];
            info.threevar.Var3 = a[2];


            //----------图形三比值法
            decimal Percent_C2H2 = -1.0m, Percent_C2H4 = -1.0m, Percent_CH4 = -1.0m;        //-1表示无法计算判断
            if (Math.Abs((C2H2 + C2H4 + CH4)) > MIN
                && Math.Abs(C2H2) > MIN
                && Math.Abs(C2H4) > MIN
                && Math.Abs(CH4) > MIN)
            {
                Percent_C2H2 = C2H2 / (C2H2 + C2H4 + CH4);
                Percent_C2H4 = C2H4 / (C2H2 + C2H4 + CH4);
                Percent_CH4 = CH4 / (C2H2 + C2H4 + CH4);
            }
            info.devidInfo.percent_C2H2 = Percent_C2H2;
            info.devidInfo.percent_C2H4 = Percent_C2H4;
            info.devidInfo.percent_CH4 = Percent_CH4;






            string desc = "";
            //----------三比值法 判断故障类型
            if (a[0] >= 0 && a[1] >= 0 && a[2] >= 0)         //比值有效时诊断
            {
                if (0 == a[0] && 0 == a[1] && 1 == a[2])
                {
                    desc = ANLYINFO_T1;
                }
                else if ((0 == a[0] || (-1) == a[0]) && 2 == a[1] && 0 == a[2])
                {
                    desc = ANLYINFO_T2;
                }
                else if (0 == a[0] && 2 == a[1] && 1 == a[2])
                {
                    desc = ANLYINFO_T3;
                }
                else if (0 == a[0] && 3 > a[1] && 0 <= a[1] && 2 == a[2])
                {
                    desc = ANLYINFO_T4;
                }
                else if ((0 == a[0] || (-1) == a[0]) && 1 == a[1] && 0 == a[2])
                {
                    desc = ANLYINFO_PD;
                }
                else if (1 == a[0] && 2 > a[1] && 0 <= a[1] && 3 > a[2] && 0 <= a[2])
                {
                    desc = ANLYINFO_LD;
                }
                else if (1 == a[0] && 2 == a[1] && 3 > a[2] && 0 <= a[2])
                {
                    desc = ANLYINFO_DT;
                }
                else if (2 == a[0] && 2 > a[1] && 0 <= a[1] && 3 > a[2] && 0 <= a[2])
                {
                    desc = ANLYINFO_AD;
                }
                else if (2 == a[0] && 2 == a[1] && 0 <= a[2] && 3 > a[2])
                {
                    desc = ANLYINFO_ADT;
                }
            }
            if (desc != "")
            {
                info.threevar.Desc = desc;
            }
            else
            {
                info.threevar.Desc = ANLYINFO_UNKNOWN;     //未知故障
            }


            //----------图形法 判断故障类型
            desc = "";
            if (Percent_C2H2 >= 0.0m && Percent_C2H4 >= 0.0m && Percent_CH4 >= 0.0m)    //计算结果有效时诊断
            {
                if (Percent_CH4 > 0.98m)
                {
                    desc = ANLYINFO_PD;
                }
                if (Percent_C2H2 > 0.13m && Percent_C2H4 < 0.23m)
                {
                    desc = ANLYINFO_LD;
                }
                if (Percent_C2H4 > 0.23m && Percent_C2H2 > 0.13m && Percent_C2H2 > 0.29m && Percent_C2H4 > 0.23m && Percent_C2H4 > 0.38m)
                {
                    desc = ANLYINFO_HD;
                }
                if (Percent_C2H2 < 0.04m && Percent_C2H4 < 0.10m)
                {
                    desc = ANLYINFO_T2A;	//低温过热，小于300°
                }
                if (Percent_C2H2 < 0.04m && Percent_C2H4 > 0.10m && Percent_C2H4 < 0.50m)
                {
                    desc = ANLYINFO_T3; //热故障，300°~ 700°
                }
                if (Percent_C2H2 < 0.15m && C2H4 > 0.50m)
                {
                    desc = ANLYINFO_T4; //热故障，> 700℃
                }
            }
            if (desc != "")
            {
                info.devidInfo.Desc = desc;
            }
            else
            {
                info.devidInfo.Desc = ANLYINFO_UNKNOWN;     //未知故障
            }



            //---最终故障结论
            if (info.threevar.Desc != ANLYINFO_UNKNOWN)
            {
                info.Desc = info.threevar.Desc;     //优先采用三比值判断结果
            }
            else if (info.devidInfo.Desc != ANLYINFO_UNKNOWN)
            {
                info.Desc = info.devidInfo.Desc;    //三比值不能判断时，用图形法判断结果
            }

            if (info.Desc == "")
            {
                info.Desc = ANLYINFO_UNKNOWN;     //无故障结果（无法判断）
            }

            int len = info.Desc.Length;
            int index = info.Desc.IndexOf("^");
            info.enDesc = info.Desc.Substring(0, index);
            info.chDesc = info.Desc.Substring(index + 1, len - index - 1);
            */
            #endregion
            
            #region  Code
            info = new AnlyInformation();
            info.devidInfo = new DevidTriAnlyInfo();
            info.devidInfo.Desc = "";
            info.ratio = new Ratio();
            info.threevar = new ThreeValue();
            info.threevar.Desc = "";
            info.chDesc = "";
            info.enDesc = "";
            info.Desc = "";




            //----------立体图示法（每一个比值的范围 0-10，10代表无穷大，参考GB/T 7252-2001 附录E）
            info.ratio.C2H2_C2H4 = info.ratio.C2H4_C2H6 = info.ratio.CH4_H2 = -1.0m;        //-1.0代表无法计算判断

            bool canCalc = true;
            if ((Math.Abs(C2H4) < MIN && Math.Abs(C2H2) < MIN)
                || (Math.Abs(C2H6) < MIN && Math.Abs(C2H4) < MIN)
                || (Math.Abs(H2) < MIN && Math.Abs(CH4) < MIN))         // 0.0/0.0 无意义
            {
                canCalc = false;
            }

            if (canCalc)
            {
                if (Math.Abs(C2H4) >= MIN)       //if(C2H4!=0)
                {
                    info.ratio.C2H2_C2H4 = C2H2 / C2H4;
                }
                else
                {
                    info.ratio.C2H2_C2H4 = 10.0m;
                }
                if (info.ratio.C2H2_C2H4 > 10.0m)
                {
                    info.ratio.C2H2_C2H4 = 10.0m;
                }

                if (Math.Abs(C2H6) >= MIN)       //if(C2H6!=0)
                {
                    info.ratio.C2H4_C2H6 = C2H4 / C2H6;
                }
                else
                {
                    info.ratio.C2H4_C2H6 = 10.0m;
                }
                if (info.ratio.C2H4_C2H6 > 10.0m)
                {
                    info.ratio.C2H4_C2H6 = 10.0m;
                }

                if (Math.Abs(H2) >= MIN)       //if(C2H6!=0)
                {
                    info.ratio.CH4_H2 = CH4 / H2;
                }
                else
                {
                    info.ratio.CH4_H2 = 10.0m;
                }
                if (info.ratio.CH4_H2 > 10.0m)
                {
                    info.ratio.CH4_H2 = 10.0m;
                }
            }


            //----------三比值法
            //根据各气体浓度值编码
            int[] a = new int[3];
            a[0] = a[1] = a[2] = -1;       //-1表示相应比值无效或无意义

            canCalc = true;
            if ((Math.Abs(C2H2) < MIN && Math.Abs(C2H4) < MIN)
                || (Math.Abs(CH4) < MIN && Math.Abs(H2) < MIN)
                || (Math.Abs(C2H4) < MIN && Math.Abs(C2H6) < MIN))         // 0.0/0.0 无意义
            {
                canCalc = false;
            }

            if (canCalc)
            {
                if (Math.Abs(C2H4) < MIN || Math.Abs(C2H2 / C2H4) >2.0m)       //c2h4==0
                {
                 //   a[0] = 2;
                }
                else
                {
                    if (C2H2 / C2H4 < 0.1m)
                    {
                        a[0] = 0;
                    }
                    if (C2H2 / C2H4 >= 0.1m && C2H2 / C2H4 <= 1.0m)
                    {
                        a[0] = 1;
                    }
                    if (C2H2 / C2H4 > 1.0m && C2H2 / C2H4 <= 2.0m)
                    {
                        a[0] = 2;
                    }
                }

                if (Math.Abs(H2) < MIN || Math.Abs(CH4 / H2) > 2.0m)       //H2==0
                {
                 //   a[1] = 2;
                }
                else
                {
                    if (CH4 / H2 < 0.1m)
                    {
                        a[1] = 0;
                    }
                    if (CH4 / H2 < 1.0m && CH4 / H2 >= 0.1m)
                    {
                        a[1] = 1;
                    }
                    if (CH4 / H2 >= 1.0m)
                    {
                        a[1] = 2;
                    }
                }

                if (Math.Abs(C2H6) < MIN || Math.Abs(C2H4 / C2H6) > 2.0m)       //C2H6==0
                {
                  //  a[2] = 2;
                }
                else
                {
                    if (C2H4 / C2H6 < 0.1m)
                    {
                        a[2] = 0;
                    }
                    if (C2H4 / C2H6 < 1.0m && C2H4 / C2H6 >= 0.1m)
                    {
                        a[2] = 1;
                    }
                    if (C2H4 / C2H6 >= 1.0m && C2H4 / C2H6 <= 2.0m)
                    {
                        a[2] = 2;
                    }
                }
            }
            info.threevar.Var1 = a[0];
            info.threevar.Var2 = a[1];
            info.threevar.Var3 = a[2];


            //----------图形三比值法
            decimal Percent_C2H2 = -1.0m, Percent_C2H4 = -1.0m, Percent_CH4 = -1.0m;        //-1表示无法计算判断
            if (Math.Abs((C2H2 + C2H4 + CH4)) > MIN
                && Math.Abs(C2H2) > MIN
                && Math.Abs(C2H4) > MIN
                && Math.Abs(CH4) > MIN)
            {
                Percent_C2H2 = C2H2 / (C2H2 + C2H4 + CH4);
                Percent_C2H4 = C2H4 / (C2H2 + C2H4 + CH4);
                Percent_CH4 = CH4 / (C2H2 + C2H4 + CH4);
            }
            info.devidInfo.percent_C2H2 = Percent_C2H2;
            info.devidInfo.percent_C2H4 = Percent_C2H4;
            info.devidInfo.percent_CH4 = Percent_CH4;






            string desc = "";
            //----------三比值法 判断故障类型
            if (a[0] >= 0 && a[1] >= 0 && a[2] >= 0)         //比值有效时诊断
            {
                if (0 == a[0] && 0 == a[1] && 1 == a[2])
                {
                    desc = ANLYINFO_T1;
                }
                else if (0 == a[0] && 2 == a[1] && 0 == a[2])
                {
                    desc = ANLYINFO_T2;
                }
                else if (0 == a[0] && 2 == a[1] && 1 == a[2])
                {
                    desc = ANLYINFO_T3;
                }
                else if (0 == a[0] && 2 >= a[1] && 0 <= a[1] && 2 == a[2])
                {
                    desc = ANLYINFO_T4;
                }
                else if (0 == a[0] && 1 == a[1] && 0 == a[2])
                {
                    desc = ANLYINFO_PD;
                }
                else if (1 == a[0] && 1 >= a[1] && 0 <= a[1] && 2 >= a[2] && 0 <= a[2])
                {
                    desc = ANLYINFO_LD;
                }
                else if (1 == a[0] && 2 == a[1] && 2 >= a[2] && 0 <= a[2])
                {
                    desc = ANLYINFO_DT;
                }
                else if (2 == a[0] && 1 >= a[1] && 0 <= a[1] && 2 >= a[2] && 0 <= a[2])
                {
                    desc = ANLYINFO_AD;
                }
                else if (2 == a[0] && 2 == a[1] && 0 <= a[2] && 2 >= a[2])
                {
                    desc = ANLYINFO_ADT;
                }
            }
            if (desc != "")
            {
                info.threevar.Desc = desc;
            }
            else
            {
                info.threevar.Desc = ANLYINFO_UNKNOWN;     //未知故障
            }


            //----------图形法 判断故障类型
            desc = "";
            if (Percent_C2H2 >= 0.0m && Percent_C2H4 >= 0.0m && Percent_CH4 >= 0.0m)    //计算结果有效时诊断
            {
                if (Percent_CH4 > 0.98m)
                {
                    desc = ANLYINFO_PD;
                }
                if (Percent_C2H2 > 0.13m && Percent_C2H4 < 0.23m)
                {
                    desc = ANLYINFO_LD;
                }
                if (Percent_C2H4 > 0.23m && Percent_C2H2 > 0.13m && Percent_C2H2 > 0.29m && Percent_C2H4 > 0.23m && Percent_C2H4 > 0.38m)
                {
                    desc = ANLYINFO_HD;
                }
                if (Percent_C2H2 < 0.04m && Percent_C2H4 < 0.10m)
                {
                    desc = ANLYINFO_T2A;	//低温过热，小于300°
                }
                if (Percent_C2H2 < 0.04m && Percent_C2H4 > 0.10m && Percent_C2H4 < 0.50m)
                {
                    desc = ANLYINFO_T3; //热故障，300°~ 700°
                }
                if (Percent_C2H2 < 0.15m && C2H4 > 0.50m)
                {
                    desc = ANLYINFO_T4; //热故障，> 700℃
                }
            }
            if (desc != "")
            {
                info.devidInfo.Desc = desc;
            }
            else
            {
                info.devidInfo.Desc = ANLYINFO_UNKNOWN;     //未知故障
            }



            //---最终故障结论
            if (info.threevar.Desc != ANLYINFO_UNKNOWN)
            {
                info.Desc = info.threevar.Desc;     //优先采用三比值判断结果
            }
            else if (info.devidInfo.Desc != ANLYINFO_UNKNOWN)
            {
                info.Desc = info.devidInfo.Desc;    //三比值不能判断时，用图形法判断结果
            }

            if (info.Desc == "")
            {
                info.Desc = ANLYINFO_UNKNOWN;     //无故障结果（无法判断）
            }

            int len = info.Desc.Length;
            int index = info.Desc.IndexOf("^");
            info.enDesc = info.Desc.Substring(0, index);
            info.chDesc = info.Desc.Substring(index + 1, len - index - 1);


            #endregion
        }
    }
}
