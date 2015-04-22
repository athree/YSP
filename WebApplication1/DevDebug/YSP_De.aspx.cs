using IMserver;
using IMserver.Data_Warehousing;
using IMserver.DBservice;
using IMserver.Models;
using IMserver.Models.SimlDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;


namespace WebApplication1.DevDebug
{
    public partial class YSP_De : System.Web.UI.Page
    {
        //private static List<double> myPoints = new List<double>();
        protected YSPservice _ysp = new YSPservice();
        protected Config myCfg;
        protected StateCtrol mySC;
        protected string devId;
        protected Dictionary<string,string> gcVal=new Dictionary<string,string>();
        protected Dictionary<string, bool> swVal = new Dictionary<string, bool>();

        #region InitLabel
        public static string Device = Language.Selected["Device"];
        public static string PressLevel = Language.Selected["PressLevel"];
        public static string A = Language.Selected["A"];
        public static string B = Language.Selected["B"];
        public static string Altitude = Language.Selected["Altitude"];
        public static string OilDenisity = Language.Selected["OilDenisity"];
        public static string OilTotal = Language.Selected["OilTotal"];
        public static string H2oVarName = Language.Selected["H2oVarName"];
        public static string H2oVarParamAWA = Language.Selected["H2oVarParamAWA"];
        public static string H2oVarParamAWK = Language.Selected["H2oVarParamAWK"];
        public static string H2oVarParamAWB = Language.Selected["H2oVarParamAWB"];
        public static string H2oVarParamTA = Language.Selected["H2oVarParamTA"];
        public static string H2oVarParamTK = Language.Selected["H2oVarParamTK"];
        public static string H2oVarParamTB = Language.Selected["H2oVarParamTB"];
        public string Gas = Language.Selected["Gas"];
        public string High = Language.Selected["High"];
        public string Left = Language.Selected["Left"];
        public string Right = Language.Selected["Right"];
        public string LeftMin = Language.Selected["LeftMin"];
        public string LeftMax = Language.Selected["LeftMax"];
        public string RightMin = Language.Selected["RightMin"];
        public string RightMax = Language.Selected["RightMax"];
        public string TopWidth = Language.Selected["TopWidth"];
        public string K = Language.Selected["K"];
        public string Erase = Language.Selected["Erase"];
        public string To = Language.Selected["To"];
        public string Text_Var_H2oVarName = Language.Selected["H2oVarName"];
        public string Text_Var_ParamA = Language.Selected["H2oVarParamA"];
        public string Text_Var_ParamK = Language.Selected["H2oVarParamK"];
        public string Text_Var_ParamB = Language.Selected["H2oVarParamB"];


        public string kvalue = Language.Selected["KValue"];
        public string mifixvalue = Language.Selected["MiFixValue"];
        public string nifixvalue = Language.Selected["NiFixValue"];
        public string min = Language.Selected["MinArea"];
        public string max = Language.Selected["MaxArea"];
        public string jizhun = Language.Selected["JiZhun"];

        #endregion

       
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["DevID"] != null)
                    ViewState["DevID"] = Session["DevID"].ToString();
                if (Session["DevSite"] != null)
                    ViewState["DevSite"] = Session["DevSite"].ToString(); 
                if (Session["DevType"] != null)
                    ViewState["DevType"] = Session["DevType"].ToString();
                if (Session["DevName"] != null)
                    ViewState["DevName"] = Session["DevName"].ToString();
                initControl();
                /////////////////测试用
                gcVal.Add("12", "55");
                if (gcVal.Count > 0)
                    Session["gcVal"]=gcVal;
                if(swVal.Count>0)
                    Session["swVal"] = swVal;
            }
            //获得传过来的设备编号,,,,测试直接用11
            //devId = ViewState["DevID"].ToString();
            devId = "11";
            //myCfg=_ysp.GetCFG(devId);
            //mySC = GetStateControl();     
        }

        /// <summary>
        /// 初始化界面数据
        /// </summary>
        public void initControl()
        {
            try
            {
                myCfg = _ysp.GetCFG(devId);
                if (myCfg != null)
                {
                    if(myCfg.SysSet!=null && myCfg.AnalyPara.EnviSet!=null)
                        FillSysSet(myCfg.SysSet,myCfg.AnalyPara.EnviSet);
                    if(myCfg.AnalyPara!=null)
                        FillAnalyPara(myCfg.AnalyPara);
                    if(myCfg.TQSet!=null && myCfg.JCFZSet!=null && myCfg.OutSideSet!=null && myCfg.SampSet!=null)
                        FillCtrlParam(myCfg.TQSet,myCfg.JCFZSet,myCfg.OutSideSet,myCfg.SampSet);
                }
                mySC = _ysp.GetSC(devId);
                if(mySC!=null)
                    FillStateCtrol(mySC);
            }
            catch (Exception e)
            {

            }
           


           
        }
        public void SelectGas_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DevInfoes/SelectData.aspx?devId="+ViewState["DevID"].ToString());
        }
       
        private void FillData()
        {
            // Fill series data 
            double yValue = 50.0;
            int count = Chart1.Series["Series1"].Points.Count;

            if (count > 0)
            {
                
                yValue = Chart1.Series["Series1"].Points.Last().YValues[0];

            }
            Random random = new Random();
            for (int pointIndex = 0; pointIndex < 8; pointIndex++)
            {
                yValue = yValue + (float)(random.NextDouble() * 10.0 - 5.0);
               
                Chart1.Series["Series1"].Points.AddY(yValue);
             
            }
         

        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            FillData();           
        }

        protected void StopButton_Click(object sender, EventArgs e)
        {
          
            Timer1.Enabled = false;
            UpdatePanel1.Update();
        }

        protected void ExecuteButton_Click(object sender, EventArgs e)
        {
            Timer1.Enabled = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strId = "0";
            Set_Jieju(strId);
            int K_NUM = 12;
            //this.DropDownList1.SelectedValue = null;
            TextBox[] TB_KX = { TB_K1, TB_K2, TB_K3, TB_K4, TB_K5, TB_K6, TB_K7, TB_K8, TB_K9, TB_K10, TB_K11, TB_K12 };
            TextBox[] TB_MIX = { TB_MI1, TB_MI2, TB_MI3, TB_MI4, TB_MI5, TB_MI6, TB_MI7, TB_MI8, TB_MI9, TB_MI10, TB_MI11, TB_MI12 };
            TextBox[] TB_NIX = { TB_NI1, TB_NI2, TB_NI3, TB_NI4, TB_NI5, TB_NI6, TB_NI7, TB_NI8, TB_NI9, TB_NI10, TB_NI11, TB_NI12 };
            TextBox[] TB_KX_MaxArea = { TB_K1_MaxArea, TB_K2_MaxArea, TB_K3_MaxArea, TB_K4_MaxArea, TB_K5_MaxArea, TB_K6_MaxArea, TB_K7_MaxArea, TB_K8_MaxArea, TB_K9_MaxArea, TB_K10_MaxArea, TB_K11_MaxArea, TB_K12_MaxArea };
            TextBox[] TB_KX_MinArea = { TB_K1_MinArea, TB_K2_MinArea, TB_K3_MinArea, TB_K4_MinArea, TB_K5_MinArea, TB_K6_MinArea, TB_K7_MinArea, TB_K8_MinArea, TB_K9_MinArea, TB_K10_MinArea, TB_K11_MinArea, TB_K12_MinArea };
            TextBox[] TB_JIZHI = { TB_JIZHI1, TB_JIZHI2, TB_JIZHI3, TB_JIZHI4, TB_JIZHI5, TB_JIZHI6, TB_JIZHI7, TB_JIZHI8, TB_JIZHI9, TB_JIZHI10, TB_JIZHI11, TB_JIZHI12 };
           
            for (int i = 0; i < K_NUM; i++)
            {

                if (i == 0)
                {
                    TB_KX[i].Text = this.TB_K1.Text;
                    TB_KX_MaxArea[i].Text = (((((float.Parse(TB_NIX[i].Text.ToString())) - (float.Parse(TB_MIX[i].Text.ToString()))) / (float.Parse(TB_KX[i].Text.ToString()))) + float.Parse(TB_KX_MinArea[i].Text.ToString())) * float.Parse(TB_JIZHI[i].Text.ToString())).ToString();
                   
                }
                else
                {
                    TB_KX[i].Text = (float.Parse(TB_KX[i - 1].Text.ToString()) * float.Parse(TB_JIZHI[i].Text.ToString())).ToString();
                    //  TB_KX_MaxArea[i].Text = ((((float.Parse(TB_NIX[i].Text.ToString()) - float.Parse(TB_MIX[i].Text.ToString())))/float.Parse(TB_KX[i].Text.ToString())) + float.Parse(TB_NIX[i].Text.ToString())).ToString();
                    TB_KX_MinArea[i].Text = (float.Parse(TB_KX_MaxArea[i - 1].Text.ToString()) + 1.0).ToString();
                    TB_KX_MaxArea[i].Text = ((((float.Parse(TB_NIX[i].Text.ToString())) - (float.Parse(TB_MIX[i].Text.ToString()))) / (float.Parse(TB_KX[i].Text.ToString()))) + float.Parse(TB_KX_MinArea[i].Text.ToString())).ToString();
                    //   TB_NIX[i].Text = TB_KX_MaxArea[i].Text;
                }
            }
            //K_NUM = 0;


            for (int j = 1; j < (K_NUM); j++)
            {
                //TB_NIX[j].Text = TB_KX_MaxArea[j - 1].Text;
                TB_NIX[j].Text = TB_KX_MaxArea[j - 1].Text;
            }
            TB_NI1.Text = "0";
            Set_Null_K();

        }


        protected void Set_Jieju(string _strID)
        {
            switch (_strID)
            {
                case "0":   //H2
                    {
                        TB_MI1.Text = "0";
                        TB_NI1.Text = "10";
                        TB_MI2.Text = "10";
                        TB_NI2.Text = "30";
                        TB_MI3.Text = "30";
                        TB_NI3.Text = "60";
                        TB_MI4.Text = "60";
                        TB_NI4.Text = "100";
                        TB_MI5.Text = "100";
                        TB_NI5.Text = "150";
                        TB_MI6.Text = "150";
                        TB_NI6.Text = "300";
                        TB_MI7.Text = "300";
                        TB_NI7.Text = "600";
                        TB_MI8.Text = "600";
                        TB_NI8.Text = "1000";
                        TB_MI9.Text = "1000";
                        TB_NI9.Text = "1500";
                        TB_MI10.Text = "1500";
                        TB_NI10.Text = "2000";

                        //

                        break;
                    }
                case "1":   //CO
                    {
                        TB_MI1.Text = "0";
                        TB_NI1.Text = "10";
                        TB_MI2.Text = "10";
                        TB_NI2.Text = "40";
                        TB_MI3.Text = "40";
                        TB_NI3.Text = "80";
                        TB_MI4.Text = "80";
                        TB_NI4.Text = "120";
                        TB_MI5.Text = "120";
                        TB_NI5.Text = "180";
                        TB_MI6.Text = "180";
                        TB_NI6.Text = "250";
                        TB_MI7.Text = "250";
                        TB_NI7.Text = "350";
                        TB_MI8.Text = "350";
                        TB_NI8.Text = "500";
                        TB_MI9.Text = "500";
                        TB_NI9.Text = "800";
                        TB_MI10.Text = "800";
                        TB_NI10.Text = "1200";
                        TB_MI11.Text = "1200";
                        TB_NI11.Text = "1500";
                        TB_MI12.Text = "1500";
                        TB_NI12.Text = "5000";

                        //TB_NI4.Text = "199";
                        //TB_MI5.Text = "199";
                        //TB_NI5.Text = "493";
                        //TB_MI6.Text = "493";
                        //TB_NI6.Text = "980";
                        //TB_MI7.Text = "980";
                        //TB_NI7.Text = "1480";
                        //TB_MI8.Text = "1480";
                        //TB_NI8.Text = "1994";
                        //TB_MI9.Text = "1994";
                        //TB_NI9.Text = "10000";
                        break;
                    }
                case "2":   //CH4
                    {
                        TB_MI1.Text = "0";
                        TB_NI1.Text = "5";
                        TB_MI2.Text = "5";
                        TB_NI2.Text = "15";
                        TB_MI3.Text = "15";
                        TB_NI3.Text = "30";
                        TB_MI4.Text = "30";
                        TB_NI4.Text = "50";
                        TB_MI5.Text = "50";
                        TB_NI5.Text = "80";
                        TB_MI6.Text = "80";
                        TB_NI6.Text = "120";
                        TB_MI7.Text = "120";
                        TB_NI7.Text = "180";
                        TB_MI8.Text = "180";
                        TB_NI8.Text = "250";
                        TB_MI9.Text = "250";
                        TB_NI9.Text = "500";
                        TB_MI10.Text = "500";
                        TB_NI10.Text = "1000";
                        break;
                    }
                case "3":   //C2H2
                    {
                        TB_MI1.Text = "0";
                        TB_NI1.Text = "5";
                        TB_MI2.Text = "5";
                        TB_NI2.Text = "10";
                        TB_MI3.Text = "10";
                        //99 150 250 350 2000
                        TB_NI3.Text = "20";
                        TB_MI4.Text = "20";
                        TB_NI4.Text = "40";
                        TB_MI5.Text = "40";
                        TB_NI5.Text = "60";
                        TB_MI6.Text = "60";
                        TB_NI6.Text = "80";
                        TB_MI7.Text = "80";
                        TB_NI7.Text = "150";
                        TB_MI8.Text = "150";
                        TB_NI8.Text = "250";
                        TB_MI9.Text = "250";
                        TB_NI9.Text = "500";
                        TB_MI10.Text = "500";
                        TB_NI10.Text = "1000";
                        //TB_NI10.Text = "250";
                        //TB_MI11.Text = "250";
                        //TB_NI11.Text = "250";
                        //TB_MI12.Text = "250";
                        //TB_NI12.Text = "250";
                        //TB_NI3.Text = "51";
                        //TB_MI4.Text = "51";
                        //TB_NI4.Text = "79.3";
                        //TB_MI5.Text = "79.3";
                        //TB_NI5.Text = "99";
                        //TB_MI6.Text = "99";
                        //TB_NI6.Text = "202";
                        //TB_MI7.Text = "202";
                        //TB_NI7.Text = "2000";
                        //TB_MI8.Text = "2000";
                        //TB_NI8.Text = "";                
                        break;
                    }
                case "4":   //C2H4
                    {
                        TB_MI1.Text = "0";
                        TB_NI1.Text = "5";
                        TB_MI2.Text = "5";
                        TB_NI2.Text = "15";
                        TB_MI3.Text = "15";
                        TB_NI3.Text = "30";
                        TB_MI4.Text = "30";
                        TB_NI4.Text = "50";
                        TB_MI5.Text = "50";
                        TB_NI5.Text = "80";
                        TB_MI6.Text = "80";
                        TB_NI6.Text = "120";
                        TB_MI7.Text = "120";
                        TB_NI7.Text = "180";
                        TB_MI8.Text = "180";
                        TB_NI8.Text = "250";
                        TB_MI9.Text = "250";
                        TB_NI9.Text = "500";
                        TB_MI10.Text = "500";
                        TB_NI10.Text = "1000";

                        //TB_NI6.Text = "508";
                        //TB_MI7.Text = "508";
                        //TB_NI7.Text = "821";
                        //TB_MI8.Text = "821";
                        //TB_NI8.Text = "1134";
                        //TB_MI9.Text = "1134";
                        //TB_NI9.Text = "1447";
                        //TB_MI10.Text = "1447";
                        //TB_NI10.Text = "2000";
                        break;
                    }
                case "5":   //C2H6
                    {
                        TB_MI1.Text = "0";
                        TB_NI1.Text = "5";
                        TB_MI2.Text = "5";
                        TB_NI2.Text = "15";
                        TB_MI3.Text = "15";
                        TB_NI3.Text = "30";
                        TB_MI4.Text = "30";
                        TB_NI4.Text = "50";
                        TB_MI5.Text = "50";
                        TB_NI5.Text = "80";
                        TB_MI6.Text = "80";
                        TB_NI6.Text = "120";
                        TB_MI7.Text = "120";
                        TB_NI7.Text = "180";
                        TB_MI8.Text = "180";
                        TB_NI8.Text = "250";
                        TB_MI9.Text = "250";
                        TB_NI9.Text = "500";
                        TB_MI10.Text = "500";
                        TB_NI10.Text = "1000";
                        //TB_NI10.Text = "2000";
                        //TB_MI11.Text = "787";
                        //TB_NI11.Text = "1082";
                        //TB_MI12.Text = "1082";
                        //TB_NI12.Text = "2000";



                        //TB_NI7.Text = "492";
                        //TB_MI8.Text = "492";
                        //TB_NI8.Text = "787";
                        //TB_MI9.Text = "787";
                        //TB_NI9.Text = "1082";
                        //TB_MI10.Text = "1082";
                        //TB_NI10.Text = "2000";
                        break;
                    }

            }
        }

        //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    TextBox[] TB_JIZHI = { TB_JIZHI1, TB_JIZHI2, TB_JIZHI3, TB_JIZHI4, TB_JIZHI5, TB_JIZHI6, TB_JIZHI7, TB_JIZHI8, TB_JIZHI9, TB_JIZHI10, TB_JIZHI11, TB_JIZHI12 };
        //    switch (this.DropDownList1.SelectedValue)
        //    {
        //        case "0":
        //            {
        //                TB_MI1.Text = "0";
        //                TB_NI1.Text = "21.2";
        //                TB_MI2.Text = "21.2";
        //                TB_NI2.Text = "51.5";
        //                TB_MI3.Text = "51.5";
        //                TB_NI3.Text = "81.1";
        //                TB_MI4.Text = "81.1";
        //                TB_NI4.Text = "197.0";
        //                TB_MI5.Text = "197.0";
        //                TB_NI5.Text = "303.0";
        //                TB_MI6.Text = "303.0";
        //                TB_NI6.Text = "508";
        //                TB_MI7.Text = "508";
        //                TB_NI7.Text = "957";
        //                TB_MI8.Text = "957";
        //                TB_NI8.Text = "1437";
        //                //

        //                break;
        //            }
        //        case "1":
        //            {
        //                TB_MI1.Text = "0";
        //                TB_NI1.Text = "49.1";
        //                TB_MI2.Text = "49.1";
        //                TB_NI2.Text = "78.1";
        //                TB_MI3.Text = "78.1";
        //                TB_NI3.Text = "199";
        //                TB_MI4.Text = "199";
        //                TB_NI4.Text = "493";
        //                TB_MI5.Text = "493";
        //                TB_NI5.Text = "980";
        //                TB_MI6.Text = "980";
        //                TB_NI6.Text = "1480";
        //                TB_MI7.Text = "1480";
        //                TB_NI7.Text = "1994";
        //                TB_MI8.Text = "1994";
        //                TB_NI8.Text = "10000";
        //                break;
        //            }
        //        case "2":
        //            {
        //                TB_MI1.Text = "0";
        //                TB_NI1.Text = "29.9";
        //                TB_MI2.Text = "29.9";
        //                TB_NI2.Text = "50.2";
        //                TB_MI3.Text = "50.2";
        //                TB_NI3.Text = "102";
        //                TB_MI4.Text = "102";
        //                TB_NI4.Text = "149";
        //                TB_MI5.Text = "149";
        //                TB_NI5.Text = "199";
        //                TB_MI6.Text = "199";
        //                TB_NI6.Text = "507";
        //                TB_MI7.Text = "507";
        //                TB_NI7.Text = "2000";
        //                //TB_MI8.Text = "2000";
        //                //TB_NI8.Text = "";                
        //                break;
        //            }
        //        case "3":
        //            {
        //                TB_MI1.Text = "0";
        //                TB_NI1.Text = "19.6";
        //                TB_MI2.Text = "19.6";
        //                TB_NI2.Text = "39.9";
        //                TB_MI3.Text = "39.9";
        //                TB_NI3.Text = "51";
        //                TB_MI4.Text = "51";
        //                TB_NI4.Text = "79.3";
        //                TB_MI5.Text = "79.3";
        //                TB_NI5.Text = "99";
        //                TB_MI6.Text = "99";
        //                TB_NI6.Text = "202";
        //                TB_MI7.Text = "202";
        //                TB_NI7.Text = "2000";
        //                //TB_MI8.Text = "2000";
        //                //TB_NI8.Text = "";                
        //                break;
        //            }
        //        case "4":
        //            {
        //                TB_MI1.Text = "0";
        //                TB_NI1.Text = "27.3";
        //                TB_MI2.Text = "27.3";
        //                TB_NI2.Text = "50.6";
        //                TB_MI3.Text = "50.6";
        //                TB_NI3.Text = "105";
        //                TB_MI4.Text = "105";
        //                TB_NI4.Text = "154";
        //                TB_MI5.Text = "154";
        //                TB_NI5.Text = "195";
        //                TB_MI6.Text = "195";
        //                TB_NI6.Text = "508";
        //                TB_MI7.Text = "508";
        //                TB_NI7.Text = "821";
        //                TB_MI8.Text = "821";
        //                TB_NI8.Text = "1134";
        //                TB_MI9.Text = "1134";
        //                TB_NI9.Text = "1447";
        //                TB_MI10.Text = "1447";
        //                TB_NI10.Text = "2000";
        //                break;
        //            }
        //        case "5":
        //            {
        //                TB_MI1.Text = "0";
        //                TB_NI1.Text = "20.9";
        //                TB_MI2.Text = "20.9";
        //                TB_NI2.Text = "39";
        //                TB_MI3.Text = "39";
        //                TB_NI3.Text = "81.9";
        //                TB_MI4.Text = "81.9";
        //                TB_NI4.Text = "100";
        //                TB_MI5.Text = "100";
        //                TB_NI5.Text = "150";
        //                TB_MI6.Text = "150";
        //                TB_NI6.Text = "197";
        //                TB_MI7.Text = "197";
        //                TB_NI7.Text = "492";
        //                TB_MI8.Text = "492";
        //                TB_NI8.Text = "787";
        //                TB_MI9.Text = "787";
        //                TB_NI9.Text = "1082";
        //                TB_MI10.Text = "1082";
        //                TB_NI10.Text = "2000";
        //                break;
        //            }

        //    }
        //}
        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    //switch (_strID)
        //    //{
        //    //    case "0":   //H2
        //    //        {
        //    //            //默认基值
        //    //            TB_JIZHI1.Text = "1.05";
        //    //            TB_JIZHI2.Text = "1.98";
        //    //            TB_JIZHI3.Text = "1.2";
        //    //            TB_JIZHI4.Text = "1.75";
        //    //            TB_JIZHI5.Text = "1.08";
        //    //            TB_JIZHI6.Text = "2.8";
        //    //            TB_JIZHI7.Text = "1.68";
        //    //            TB_JIZHI8.Text = "1.7";
        //    //            TB_JIZHI9.Text = "1";
        //    //            TB_JIZHI10.Text = "1.48";
        //    //            //默认k值
        //    //            TB_K1.Text = "0.0009275964";
        //    //            TB_K1_MinArea.Text = "1";
        //    //            TB_K1_MaxArea.Text = "22651.53";

        //    //            bDefault = true;
        //    //        }
        //    //        break;
        //    //    case "1":  //co
        //    //        {
        //    //            //默认基值
        //    //            TB_JIZHI1.Text = "1.03";
        //    //            TB_JIZHI2.Text = "3";
        //    //            TB_JIZHI3.Text = "0.35";
        //    //            TB_JIZHI4.Text = "1.6";
        //    //            TB_JIZHI5.Text = "0.98";
        //    //            TB_JIZHI6.Text = "3.8";
        //    //            TB_JIZHI7.Text = "0.19";
        //    //            TB_JIZHI8.Text = "15";
        //    //            TB_JIZHI9.Text = "1.3";
        //    //            TB_JIZHI10.Text = "1.31";
        //    //            TB_JIZHI11.Text = "1";
        //    //            TB_JIZHI12.Text = "1";
        //    //            //默认k值
        //    //            TB_K1.Text = "0.00441";
        //    //            TB_K1_MinArea.Text = "1";
        //    //            TB_K1_MaxArea.Text = "943.434";

        //    //            bDefault = true;
        //    //        }
        //    //        break;
        //    //    case "2":  //CH4
        //    //        {
        //    //            //默认基值
        //    //            TB_JIZHI1.Text = "0.9";
        //    //            TB_JIZHI2.Text = "1.56";
        //    //            TB_JIZHI3.Text = "2.5";
        //    //            TB_JIZHI4.Text = "1.98";
        //    //            TB_JIZHI5.Text = "0.19";
        //    //            TB_JIZHI6.Text = "3";
        //    //            TB_JIZHI7.Text = "4";
        //    //            TB_JIZHI8.Text = "1.2";
        //    //            TB_JIZHI9.Text = "1";

        //    //            //默认k值
        //    //            TB_K1.Text = "0.002631627";
        //    //            TB_K1_MinArea.Text = "1";
        //    //            TB_K1_MaxArea.Text = "10226.51";

        //    //            bDefault = true;
        //    //        }
        //    //        break;
        //    //    case "3":  //C2H2
        //    //        {
        //    //            //默认基值
        //    //            TB_JIZHI1.Text = "0.9";
        //    //            TB_JIZHI2.Text = "1.68";
        //    //            TB_JIZHI3.Text = "1.88";
        //    //            TB_JIZHI4.Text = "0.88";
        //    //            TB_JIZHI5.Text = "1.05";
        //    //            TB_JIZHI6.Text = "1.25";
        //    //            TB_JIZHI7.Text = "3.3";
        //    //            TB_JIZHI8.Text = "1.92";
        //    //            TB_JIZHI9.Text = "1";
        //    //            TB_JIZHI10.Text = "1.31";

        //    //            //默认k值
        //    //            TB_K1.Text = "0.000734";
        //    //            TB_K1_MinArea.Text = "1";
        //    //            TB_K1_MaxArea.Text = "24033.6";

        //    //            bDefault = true;
        //    //        }
        //    //        break;
        //    //    case "4":  //C2H4
        //    //        {
        //    //            //默认基值
        //    //            TB_JIZHI1.Text = "0.96";
        //    //            TB_JIZHI2.Text = "1.72";
        //    //            TB_JIZHI3.Text = "1.9";
        //    //            TB_JIZHI4.Text = "1.98";
        //    //            TB_JIZHI5.Text = "0.32";
        //    //            TB_JIZHI6.Text = "3.8";
        //    //            TB_JIZHI7.Text = "20";
        //    //            TB_JIZHI8.Text = "1.92";
        //    //            TB_JIZHI9.Text = "3.51";
        //    //            TB_JIZHI10.Text = "1.31";

        //    //            //默认k值
        //    //            TB_K1.Text = "0.000521";
        //    //            TB_K1_MinArea.Text = "1";
        //    //            TB_K1_MaxArea.Text = "50304.22";

        //    //            bDefault = true;
        //    //        }
        //    //        break;
        //    //    case "5":  //C2H6
        //    //        {
        //    //            //默认基值
        //    //            TB_JIZHI1.Text = "0.96";
        //    //            TB_JIZHI2.Text = "1.1";
        //    //            TB_JIZHI3.Text = "1.9";
        //    //            TB_JIZHI4.Text = "1.38";
        //    //            TB_JIZHI5.Text = "1.8";
        //    //            TB_JIZHI6.Text = "0.78";
        //    //            TB_JIZHI7.Text = "0.75";
        //    //            TB_JIZHI8.Text = "8";
        //    //            TB_JIZHI9.Text = "1";
        //    //            TB_JIZHI10.Text = "1.01";

        //    //            //默认k值
        //    //            TB_K1.Text = "0.000808";
        //    //            TB_K1_MinArea.Text = "1";
        //    //            TB_K1_MaxArea.Text = "24832.64";

        //    //            bDefault = true;
        //    //        }
        //    //        break;
        //    }


        protected void Set_Null_K()
        {
            int K_COUNT = 12;
            TextBox[] TB_KX = { TB_K1, TB_K2, TB_K3, TB_K4, TB_K5, TB_K6, TB_K7, TB_K8, TB_K9, TB_K10, TB_K11, TB_K12 };
            TextBox[] TB_MIX = { TB_MI1, TB_MI2, TB_MI3, TB_MI4, TB_MI5, TB_MI6, TB_MI7, TB_MI8, TB_MI9, TB_MI10, TB_MI11, TB_MI12 };
            TextBox[] TB_NIX = { TB_NI1, TB_NI2, TB_NI3, TB_NI4, TB_NI5, TB_NI6, TB_NI7, TB_NI8, TB_NI9, TB_NI10, TB_NI11, TB_NI12 };
            TextBox[] TB_KX_MaxArea = { TB_K1_MaxArea, TB_K2_MaxArea, TB_K3_MaxArea, TB_K4_MaxArea, TB_K5_MaxArea, TB_K6_MaxArea, TB_K7_MaxArea, TB_K8_MaxArea, TB_K9_MaxArea, TB_K10_MaxArea, TB_K11_MaxArea, TB_K12_MaxArea };
            TextBox[] TB_KX_MinArea = { TB_K1_MinArea, TB_K2_MinArea, TB_K3_MinArea, TB_K4_MinArea, TB_K5_MinArea, TB_K6_MinArea, TB_K7_MinArea, TB_K8_MinArea, TB_K9_MinArea, TB_K10_MinArea, TB_K11_MinArea, TB_K12_MinArea };

            TextBox[] TB_JIZHI = { TB_JIZHI1, TB_JIZHI2, TB_JIZHI3, TB_JIZHI4, TB_JIZHI5, TB_JIZHI6, TB_JIZHI7, TB_JIZHI8, TB_JIZHI9, TB_JIZHI10, TB_JIZHI11, TB_JIZHI12 };

            for (int i = 0; i < K_COUNT; i++)
            {
                if (TB_JIZHI[i].Text.ToString() == "0")
                {
                    TB_KX[i].Text = "0";
                    TB_MIX[i].Text = "0";
                    TB_NIX[i].Text = "0";
                    TB_KX_MaxArea[i].Text = "0";
                    TB_KX_MinArea[i].Text = "0";
                }
            }
        }

        protected void ShowDiag_Click(object sender, EventArgs e)
        {
            if (Session["DataID_List"] != null)
            {
                List<WebApplication1.DevInfoes.SelectData.DataInfo> dataInfo = (List<WebApplication1.DevInfoes.SelectData.DataInfo>)(Session["DataID_List"]);
                //取数据。。。。。。。。。。。。。。。。。。。。。。。。。画谱图。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
            }
        }

        protected void SetTime_Click(object sender, EventArgs e)
        {
            //给下位机对时
        }

        protected void ReadSys_Click(object sender, EventArgs e)
        {
            //从下位机读取系统设置
            Dictionary<ushort, object> sysDic = new GetData().GetSys();
            for (int i = 0; i < sysDic.Count; i++)
            {
                ushort key = sysDic.ElementAt(i).Key;
                object value = sysDic.ElementAt(i).Value;
                Label LB = (Label)Page.FindControl("LB_" + key);  //查找前台对应label后进行赋值
                if (LB != null)
                {
                    LB.Text = ((float)value).ToString();
                }
                else
                {
                    DropDownList DD = (DropDownList)Page.FindControl("DD_" + key);
                    DD.SelectedIndex = (int)value;
                }
                try
                {
                    AddSIML.Warehousing(sysDic, Byte.Parse(devId));
                }
                catch (Exception exce)
                {

                }
                return;

            }
            //FillSysSet(sysSet);
        }

        protected void SetSys_Click(object sender, EventArgs e)
        {
            //系统设置下设
        }

        protected void ReadCtrl_Click(object sender, EventArgs e)
        {
            //从下位机读取控制参数
            Dictionary<ushort, object> cpDic = new GetData().GetCP(DD_152.SelectedIndex);
            for (int i = 0; i < cpDic.Count; i++)
            {
                ushort key = cpDic.ElementAt(i).Key;
                object value = cpDic.ElementAt(i).Value;
                Label LB = (Label)Page.FindControl("LB_" + key);  //查找前台对应label后进行赋值
                LB.Text = ((float)value).ToString();
                
                try
                {
                    AddSIML.Warehousing(cpDic, Byte.Parse(devId));
                }
                catch (Exception ex)
                {

                }
                return;

            }
            //FillCtrlParam();
        }

        protected void SetCtrl_Click(object sender, EventArgs e)
        {
            //控制参数下设
        }

        protected void ReadState_Click(object sender, EventArgs e)
        {
            //从下位机读取状态/控制信息
            Dictionary<ushort, object> scDic = new GetData().GetCP(DD_152.SelectedIndex);
            for (int i = 0; i < scDic.Count; i++)
            {
                ushort key = scDic.ElementAt(i).Key;
                object value = scDic.ElementAt(i).Value;
                Label LB = (Label)Page.FindControl("LB_" + key);  //查找前台对应label后进行赋值
                if (LB != null)
                {
                    LB.Text = ((float)value).ToString();
                }
                else
                {
                    CheckBox CB = (CheckBox)Page.FindControl("SW_" + key);
                    if (CB != null)
                    {
                        CB.Checked = value.ToString() == "0" ? false : true;
                    }
                    else
                    {
                        TextBox TB = (TextBox)Page.FindControl("TB_"+key);
                        TB.Text = ((float)value).ToString();
                    }                   
                    
                }

                try
                {
                    AddSIML.Warehousing(scDic, Byte.Parse(devId));
                }
                catch (Exception ex)
                {

                }
                return;

            }
            //FillStateCtrol();
        }

        protected void SetState_Click(object sender, EventArgs e)
        {
            //状态控制下设
        }




       
        /// <summary>
        ///  初始化系统设置选项卡数据
        /// </summary>
        public void FillSysSet(SystemSetting SysSet,EnvironmentSetting EnviSet){
           
            try
            {
               
                    DD_150.SelectedValue = SysSet.SuCO2.ToString();
                    DD_151.SelectedValue = SysSet.SuH2O.ToString();
                    DD_152.SelectedValue = SysSet.TuoQi_Mode.ToString();
                    TB_149.Text = SysSet.SoftwareRelease;

                
               
                    
                    TB_182.Text = EnviSet.voltage.ToString();
                    TB_184.Text = EnviSet.altitude.ToString();
                    //TB_183_1.Text = EnviSet.oilFactorA.ToString();
                    //TB_183_2.Text = EnviSet.oilFactorB.ToString();
                    TB_183_1.Text = EnviSet.oilfactor.A.ToString();
                    TB_183_2.Text = EnviSet.oilfactor.B.ToString();
                    TB_180.Text = EnviSet.oilDensity.ToString();
                    TB_181.Text = EnviSet.oilTotal.ToString();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        ///  初始化计算参数选项卡数据
        /// </summary>
        public void FillAnalyPara(AnalysisParameter analyPara) //////////////////////////////未写好
        {
           
            try
            {
                
            }
            catch(Exception ex)
            {
              throw ex;


            }
           
        }

        /// <summary>
        /// 初始化控制参数选项卡数据  
        /// </summary>
        public void FillCtrlParam(TuoQiSetting TQSet,JCFZSetting JCFZSet,OutSideSetting OutSideSet,SampleSetting SampSet){
           
            try
            {
                if (TQSet != null)
                {
                    //真空脱气
                    TB_4.Text = TQSet.Cycle_Tick.ToString();
                    TB_5.Text = TQSet.EvacuTimes.ToString();
                    TB_6.Text = TQSet.CleanTimes.ToString();
                    TB_7.Text = TQSet.TuoQiTimes.ToString();
                    TB_8.Text = TQSet.ChangeTimes.ToString();
                    TB_9.Text = TQSet.TuoQiEnd_Tick.ToString();

                    //膜脱气
                    TB_36.Text = TQSet.YouBengKeep_Tick.ToString();
                    TB_37.Text = TQSet.PaiQiClen_Tick.ToString();
                    TB_38.Text = TQSet.QiBengClean_Tick.ToString();
                    TB_39.Text = TQSet.PaiQiKeep_Tick.ToString();
                    TB_40.Text = TQSet.QiBengKeepOn_Tick.ToString();
                    TB_41.Text = TQSet.PainQiKeepOff_Tick.ToString();
                    TB_42.Text = TQSet.QiBengKeepOff_Tick.ToString();

                    //顶空脱气
                    TB_51.Text = TQSet.StirStart.ToString();
                    TB_52.Text = TQSet.StirWork_Tick.ToString();
                    TB_53.Text = TQSet.CleanPumpStart.ToString();
                    TB_54.Text = TQSet.CleanPumpWork_Time.ToString();
                    TB_55.Text = TQSet.ChangeValveStart.ToString();
                    TB_56.Text = TQSet.ChangeValveWork_Tick.ToString();
                }
                if (JCFZSet != null)
                {
                   
                    //传感器室
                    if (JCFZSet.SensorRoom != null)
                    {
                        TB_78.Text = JCFZSet.SensorRoom.Start.ToString();
                        TB_79.Text = JCFZSet.SensorRoom.Work_Tick.ToString();
                        TB_80.Text = JCFZSet.SensorRoom.TempSet.ToString();
                        TB_81.Text = JCFZSet.SensorRoom.TempP.ToString();
                        TB_82.Text = JCFZSet.SensorRoom.TempI.ToString();
                        TB_83.Text = JCFZSet.SensorRoom.TempD.ToString();
                        TB_84.Text = JCFZSet.SensorRoom.TempPID.ToString();
                    }

                    //冷井
                    if (JCFZSet.LengJing != null)
                    {
                        TB_66.Text = JCFZSet.LengJing.Start.ToString();
                        TB_67.Text = JCFZSet.LengJing.Work_Tick.ToString();
                        TB_68.Text = JCFZSet.LengJing.TempSet.ToString();
                        TB_69.Text = JCFZSet.LengJing.TempP.ToString();
                        TB_70.Text = JCFZSet.LengJing.TempI.ToString();
                        TB_71.Text = JCFZSet.LengJing.TempD.ToString();
                        TB_72.Text = JCFZSet.LengJing.TempPID.ToString();

                    }

                    //色谱柱
                    if (JCFZSet.SePuZhu != null)
                    {
                        TB_73.Text = JCFZSet.SePuZhu.TempSet.ToString();
                        TB_74.Text = JCFZSet.SePuZhu.TempP.ToString();
                        TB_75.Text = JCFZSet.SePuZhu.TempI.ToString();
                        TB_76.Text = JCFZSet.SePuZhu.TempD.ToString();
                        TB_77.Text = JCFZSet.SePuZhu.TempPID.ToString();
                    }

                }
                if (OutSideSet != null)
                {
                   
                    TB_97.Text = OutSideSet.FengShanKeep_Tick.ToString();
                    TB_98.Text = OutSideSet.FengShanWork_Tick.ToString();
                    TB_99.Text = OutSideSet.AirControlStart.ToString();
                    TB_100.Text = OutSideSet.AirControlWork_Tick.ToString();
                    TB_101.Text = OutSideSet.BanReDaiStart_Tick.ToString();
                    TB_102.Text = OutSideSet.BanReDaiWork_Tick.ToString();
                    TB_103.Text = OutSideSet.DrainStart.ToString();
                    TB_104.Text = OutSideSet.DrainWork_Tick.ToString();
                }
                if (SampSet != null)
                {                    
                    TB_131.Text = SampSet.BiaoDingTimes.ToString();
                    TB_107.Text = SampSet.ChuiSaoBefore_Tick.ToString();
                    TB_108.Text = SampSet.DingLiangWork_Tick.ToString();
                    TB_109.Text = SampSet.ChuiSaoDelay_Tick.ToString();
                    TB_110.Text = SampSet.ChuiSaoAfter_Tick.ToString();

                    TB_123.Text = SampSet.SixGasSampInterval.ToString();
                    TB_124.Text = SampSet.SixGasSampNum.ToString();
                    TB_111.Text = SampSet.HuiFuBeforeStart.ToString();
                    TB_112.Text = SampSet.HuiFuBeforeWork_Tick.ToString();
                    TB_113.Text = SampSet.HuiFuAfterStart.ToString();
                    TB_114.Text = SampSet.HuiFuAfterWork_Tick.ToString();
                    TB_120.Text = SampSet.SixGasHeatStart.ToString();
                    TB_121.Text = SampSet.SixGasHeatWork_Tick.ToString();
                    TB_122.Text = SampSet.SixGasAfterSamp_Tick.ToString();

                    TB_125.Text = SampSet.CO2HeatStart.ToString();
                    TB_126.Text = SampSet.CO2HeatWork_Tick.ToString();
                    TB_127.Text = SampSet.CO2SampInterval.ToString();
                    TB_128.Text = SampSet.CO2SampNum.ToString();
                    TB_129.Text = SampSet.CO2GasStart.ToString();
                    TB_130.Text = SampSet.CO2GasWork_Tick.ToString();

                    TB_117.Text = SampSet.H2oSampInterval.ToString();
                    TB_118.Text = SampSet.H2oAwSampNum.ToString();
                    TB_119.Text = SampSet.H2oTSampNum.ToString();
                    TB_115.Text = SampSet.H2oDelayStart_Tick.ToString();
                    TB_116.Text = SampSet.H2oSampStart_Tick.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }


        /// <summary>
        /// //初始化状态控制选项卡数据
        /// </summary>
        public void FillStateCtrol(StateCtrol mySC)
        {
            
            try
            {
                if (mySC != null)
            {
                if (mySC.ZhenKongSC != null)
                {
                    LB_11.Text = mySC.ZhenKongSC.VacuPres.ToString();
                    gcVal.Add("11", LB_11.Text);
                    LB_12.Text = mySC.ZhenKongSC.QiBengPres.ToString();
                    gcVal.Add("12", LB_12.Text);
                    LB_13.Text = mySC.ZhenKongSC.OilPres.ToString();
                    gcVal.Add("13", LB_13.Text);
                    LB_14.Text = mySC.ZhenKongSC.YouBeiLevel.ToString();
                    LB_15.Text = mySC.ZhenKongSC.QiBeiLevel.ToString();
                    LB_16.Text = mySC.ZhenKongSC.QiGangForw.ToString();
                    LB_17.Text = mySC.ZhenKongSC.QiGangBackw.ToString();
                    LB_18.Text = mySC.ZhenKongSC.YouGangForw.ToString();
                    LB_19.Text = mySC.ZhenKongSC.YouGangBackw.ToString();


                    SW_21.Checked = mySC.ZhenKongSC.OilPump == '0' ? false : true;
                    TB_22.Text = mySC.ZhenKongSC.OilPumpRoV.ToString();
                    gcVal.Add("22", TB_22.Text);
                    SW_23.Checked = mySC.ZhenKongSC.OilValve == '0' ? false : true;
                    swVal.Add("23", SW_23.Checked);
                    SW_24.Checked = mySC.ZhenKongSC.YV10 == '0' ? false : true;
                    swVal.Add("24", SW_24.Checked);
                    SW_25.Checked = mySC.ZhenKongSC.YV11 == '0' ? false : true;
                    swVal.Add("25", SW_25.Checked);
                    SW_26.Checked = mySC.ZhenKongSC.YV12 == '0' ? false : true;
                    swVal.Add("26", SW_26.Checked);
                    SW_27.Checked = mySC.ZhenKongSC.YV14 == '0' ? false : true;
                    swVal.Add("27", SW_27.Checked);
                    SW_28.Checked = mySC.ZhenKongSC.YV15 == '0' ? false : true;
                    swVal.Add("28", SW_28.Checked);

                    SW_33.Checked = mySC.ZhenKongSC.QiBeng == '0' ? false : true;
                    SW_29.Checked = mySC.ZhenKongSC.YV4 == '0' ? false : true;
                    swVal.Add("29", SW_29.Checked);
                    SW_30.Checked = mySC.ZhenKongSC.YV5 == '0' ? false : true;
                    swVal.Add("30", SW_30.Checked);
                    SW_31.Checked = mySC.ZhenKongSC.YV6 == '0' ? false : true;
                    swVal.Add("31", SW_31.Checked);
                    SW_32.Checked = mySC.ZhenKongSC.YV7 == '0' ? false : true;
                    swVal.Add("32", SW_32.Checked);
                }
                if (mySC.DingKongSC != null)
                {
                    SW_45.Checked = mySC.DingKongSC.StirSwitch == '0' ? false : true;
                    LB_47.Text = mySC.DingKongSC.LevelA.ToString();
                    LB_48.Text = mySC.DingKongSC.LevelB.ToString();
                    LB_49.Text = mySC.DingKongSC.StirTemprature.ToString();

                }
                if (mySC.JCFZSC != null)
                {
                    SW_59.Checked = mySC.JCFZSC.LengJingSwitch == '0' ? false : true;
                    SW_60.Checked = mySC.JCFZSC.SensorRoomCooler == '0' ? false : true;
                    swVal.Add("60", SW_60.Checked);
                    LB_62.Text = mySC.JCFZSC.SensorRoomT.ToString();
                    gcVal.Add("62", LB_62.Text);
                    LB_63.Text = mySC.JCFZSC.LengJingT.ToString();
                    gcVal.Add("63", LB_63.Text);
                    LB_64.Text = mySC.JCFZSC.SePuZhuT.ToString();
                    gcVal.Add("64", LB_64.Text);
                }
                if (mySC.OutSideSC != null)
                {
                    LB_87.Text = mySC.OutSideSC.OilTemprature.ToString();
                    gcVal.Add("87", LB_87.Text);
                    LB_88.Text = mySC.OutSideSC.Temprature_In.ToString();
                    gcVal.Add("88", LB_88.Text);
                    LB_89.Text = mySC.OutSideSC.Temprature_Out.ToString();
                    gcVal.Add("89", LB_89.Text);
                    LB_96.Text = mySC.OutSideSC.BanReDaiT.ToString();
                    gcVal.Add("96", LB_96.Text);

                    SW_92.Checked = mySC.OutSideSC.QiBengSwitch == '0' ? false : true;
                    SW_93.Checked = mySC.OutSideSC.AirControlSwitch == '0' ? false : true;
                    swVal.Add("93", SW_93.Checked);
                    SW_94.Checked = mySC.OutSideSC.BanReDaiStart == '0' ? false : true;
                    SW_95.Checked = mySC.OutSideSC.DrainSwitch == '0' ? false : true;

                }
                if (mySC.SampSC != null)
                {
                    SW_133.Checked = mySC.SampSC.StandardStart == '0' ? false : true;
                    SW_134.Checked = mySC.SampSC.SampleStart == '0' ? false : true;
                    swVal.Add("134", SW_134.Checked);

                    TB_135.Text = mySC.SampSC.NextSampleTime.ToString();
                    TB_136.Text = mySC.SampSC.SampleInterval.ToString();
                    TB_145.Text = mySC.SampSC.GasPressure.ToString();
                    gcVal.Add("12", TB_145.Text);
                    SW_138.Checked = mySC.SampSC.SixGaxStart == '0' ? false : true;
                    swVal.Add("138", SW_138.Checked);
                    SW_139.Checked = mySC.SampSC.H2oHeatStart == '0' ? false : true;
                    SW_140.Checked = mySC.SampSC.H2oStart == '0' ? false : true;
                    SW_141.Checked = mySC.SampSC.DingLiangStart == '0' ? false : true;
                    SW_142.Checked = mySC.SampSC.ChuiSaoStart == '0' ? false : true;
                    swVal.Add("142", SW_142.Checked);
                    SW_143.Checked = mySC.SampSC.CO2ChangeStart == '0' ? false : true;
                    swVal.Add("143", SW_143.Checked);
                    SW_144.Checked = mySC.SampSC.ChuiQiStart == '0' ? false : true;

                }

                if (mySC.SysSC != null)
                {
                    LB_154.Text = mySC.SysSC.PLCTime.Date.ToString();
                    DD_156.SelectedValue = mySC.SysSC.DevState.ToString();
                    DD_157.SelectedValue = mySC.SysSC.WorkFlow.ToString();
                }
                if (Session["gcVal"] != null)
                    Session["gcVal"] = gcVal;
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            
        }

        protected void H2K_Click(object sender, EventArgs e)
        {
            TB_K1.Text = "1111";
        }

        protected void COK_Click(object sender, EventArgs e)
        {

        }

        protected void CH4K_Click(object sender, EventArgs e)
        {

        }

        protected void C2H2K_Click(object sender, EventArgs e)
        {

        }

        protected void C2H4K_Click(object sender, EventArgs e)
        {

        }

        protected void C2H6K_Click(object sender, EventArgs e)
        {

        }

        protected void CO2K_Click(object sender, EventArgs e)
        {

        }
    }


}