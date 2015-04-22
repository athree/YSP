using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Diagnosis;
using IMserver.DBservice;
using IMserver.Models.SimlDefine;
using IMserver.Models;
using System.Data;
using IMserver;
using IMserver.Data_Warehousing;

namespace WebApplication1.DevAlarm
{
    public partial class YSP_A : System.Web.UI.Page
    {
        public MongoHelper<AlarmState> _as=new MongoHelper<AlarmState>();       
        public string devId = "";
        public string devSite, devType, devName;
        public AlarmState myAs;
        


        private string[] WarringLevel = { "", Language.Selected["Level1"], Language.Selected["Level2"] };
        public static string device = Language.Selected["Warring1"];
        public static string ratio = Language.Selected["Ratio"];
        public static string result = Language.Selected["Result1"];

        public string GasAttention = Language.Selected["GasAttention"];
        public string AbsAttention = Language.Selected["AbsAttention"];
        public string RelAttention = Language.Selected["RelAttention"];
        public string Level1 = Language.Selected["Level1"];
        public string Level2 = Language.Selected["Level2"];
        public string H2 = Language.Selected["H2"];
        public string CO = Language.Selected["CO"];
        public string CH4 = Language.Selected["CH4"];
        public string C2H2 = Language.Selected["C2H2"];
        public string C2H4 = Language.Selected["C2H4"];
        public string C2H6 = Language.Selected["C2H6"];
        public string CO2 = Language.Selected["CO2"];
        public string PPM = Language.Selected["PPM"];
        public string AW = Language.Selected["AW"];
        public string T = Language.Selected["T"];
        public string ZongT = Language.Selected["ZongT"];
        public string Gas = Language.Selected["Gas"];
        public string Device = Language.Selected["Device"];



        protected void Page_Load(object sender, EventArgs e)
        {
            BT_Sel.Text = Language.Selected["SelAnaData"];
            BT_Abs.Text = Language.Selected["AbsData"];
            BT_Rel.Text = Language.Selected["RelData"];
            Panel_Result.GroupingText = Language.Selected["SelectedData"];

            
            if(!IsPostBack)
            {
                
                Diag.ImageUrl = "~/Image/DavidDiag.bmp";               
                ViewState["ErrorMsg"] = "";
                Session["DiagFlag"] = 0;
                
                Timer1.Enabled = false;
                InitCtrls();
                
            }
            if (Session["DevID"] != null)
                devId = Session["DevID"].ToString();
            else
            {
                ViewState["ErrorMsg"]="session出错，请重新从地图页选择设备！";
                return;
            }
            if (Session["DevSite"] != null)
                devSite = Session["DevSite"].ToString();
            if (Session["DevType"] != null)
                devType = Session["DevType"].ToString();
            if (Session["DevName"] != null)
                devName = Session["DevName"].ToString();

            BindSelectedData();
            int i = this.GridView1.Rows.Count;
            if (i != 0)
            {
                this.Panel_Result.Visible = true;
            }

        }

        private void InitCtrls()
        {
            Expression<Func<AlarmState, bool>> ex = p => p.DevID == devId;
            myAs = _as.FindOneBy(ex);
            if (myAs == null)
            {
                Response.Write("<script>alert('需要传输该设备信息！')</script>");
                Dictionary<ushort, object> asDic = new GetData().GetAS();
                for (int i = 0; i < asDic.Count; i++)
                {
                    ushort key = asDic.ElementAt(i).Key;
                    object value = asDic.ElementAt(i).Value;
                    Label LB = (Label)Page.FindControl("LB_" + key);  //查找前台对应label后进行赋值
                    if (LB != null)
                    {
                        LB.Text = ((float)value).ToString();
                    }
                    else
                    {
                        CheckBox CB = (CheckBox)Page.FindControl("SW_" + key);
                        CB.Checked=value.ToString()=="0"?false:true;

                    }
                                    
                }

                try
                {
                    AddAlarmState.Warehousing(asDic, Byte.Parse(devId));
                }
                catch (Exception e)
                {

                }
                return;
            }
            else
            {
                SW_286.Checked = myAs.aa.AutoAlarm == '0' ? false : true;
                SW_287.Checked = myAs.aa.AutoDiagnose == '0' ? false : true;
                LB_288.Text = myAs.aa.Interval.ToString();

                LB_135.Text = myAs.NextSampleTime.ToString();
                LB_145.Text = myAs.GasPressure.ToString();
                
                LB_64.Text = myAs.SePuZhuT.ToString();
                LB_62.Text = myAs.SensorRoomT.ToString();
                LB_63.Text = myAs.LengJingT.ToString();

                LB_136.Text = myAs.SampleInterval.ToString();
                LB_154.Text = myAs.PLCTime.ToString();

                LB_11.Text = myAs.VacuPres.ToString();
                LB_13.Text = myAs.OilPres.ToString();
                LB_14.Text = myAs.YouBeiLevel.ToString();
                LB_15.Text = myAs.QiBeiLevel.ToString();
                LB_16.Text = myAs.QiGangForw.ToString();
                LB_17.Text = myAs.QiGangBackw.ToString();
                LB_18.Text = myAs.YouGangForw.ToString();
                LB_19.Text = myAs.YouGangBackw.ToString();
                LB_7.Text = myAs.TuoQiTimes.ToString();
                LB_8.Text = myAs.ChangeTimes.ToString();
            }
        }

        private void BindSelectedData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(Language.Selected["Col_Data"], typeof(string));
            dt.Columns.Add(Language.Selected["Title_Content"], typeof(DateTime));
            dt.Columns.Add(Language.Selected["H2"], typeof(Decimal));
            dt.Columns.Add(Language.Selected["CO"], typeof(Decimal));
            dt.Columns.Add(Language.Selected["CH4"], typeof(Decimal));
            dt.Columns.Add(Language.Selected["C2H2"], typeof(Decimal));
            dt.Columns.Add(Language.Selected["C2H4"], typeof(Decimal));
            dt.Columns.Add(Language.Selected["C2H6"], typeof(Decimal));
            dt.Columns.Add(Language.Selected["CO2"], typeof(Decimal));
            dt.Columns.Add(Language.Selected["ZongT"], typeof(Decimal));

            if (Session["SelectedData"] != null)
            {
                RunningState gas = (RunningState)Session["SelectedData"];

                DataRow row = dt.NewRow();
                row[Language.Selected["Col_Data"]] = Language.Selected["CompareData"];
                row[Language.Selected["Title_Content"]] = gas.ReadDate;
                row[Language.Selected["H2"]] = gas.H2;
                row[Language.Selected["CO"]] = gas.CO;
                row[Language.Selected["CH4"]] = gas.CH4;
                row[Language.Selected["C2H2"]] = gas.C2H2;
                row[Language.Selected["C2H4"]] = gas.C2H4;
                row[Language.Selected["C2H6"]] = gas.C2H6;
                row[Language.Selected["CO2"]] = gas.CO2;
                row[Language.Selected["ZongT"]] = gas.TotHyd;


                dt.Rows.Add(row);
            }

            if (Session["SelectedAbs"] != null)
            {
                RunningState gas = (RunningState)Session["SelectedAbs"];

                DataRow row = dt.NewRow();
                row[Language.Selected["Col_Data"]] = Language.Selected["AbsData"];
                row[Language.Selected["Title_Content"]] = gas.ReadDate;
                row[Language.Selected["H2"]] = gas.H2;
                row[Language.Selected["CO"]] = gas.CO;
                row[Language.Selected["CH4"]] = gas.CH4;
                row[Language.Selected["C2H2"]] = gas.C2H2;
                row[Language.Selected["C2H4"]] = gas.C2H4;
                row[Language.Selected["C2H6"]] = gas.C2H6;
                row[Language.Selected["CO2"]] = gas.CO2;
                row[Language.Selected["ZongT"]] = gas.TotHyd;


                dt.Rows.Add(row);
            }

            if (Session["SelectedRel"] != null)
            {
                RunningState gas = (RunningState)Session["SelectedRel"];

                DataRow row = dt.NewRow();
                row[Language.Selected["Col_Data"]] = Language.Selected["RelData"];
                row[Language.Selected["Title_Content"]] = gas.ReadDate;
                row[Language.Selected["H2"]] = gas.H2;
                row[Language.Selected["CO"]] = gas.CO;
                row[Language.Selected["CH4"]] = gas.CH4;
                row[Language.Selected["C2H2"]] = gas.C2H2;
                row[Language.Selected["C2H4"]] = gas.C2H4;
                row[Language.Selected["C2H6"]] = gas.C2H6;
                row[Language.Selected["CO2"]] = gas.CO2;
                row[Language.Selected["ZongT"]] = gas.TotHyd;


                dt.Rows.Add(row);
            }

            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
        protected void BT_Sel_Click(object sender, EventArgs e)
        {
            if (devId != "")
            {
                Response.Redirect("../DevInfoes/SelectGasData.aspx?type=data&id=" + devId);
            }
        }
        protected void BT_Abs_Click(object sender, EventArgs e)
        {
            if (devId != "")
            {
                Response.Redirect("../DevInfoes/SelectGasData.aspx?type=abs&id=" + devId);
            }
        }
        protected void BT_Rel_Click(object sender, EventArgs e)
        {
            if (devId != "")
            {
                Response.Redirect("../DevInfoes/SelectGasData.aspx?type=rel&id=" + devId);
            }
        }

        public void get_temp_Map()
        {
            string bmpPath = Server.MapPath("~/Image/DavidDiag.bmp");
            try
            {
                Bitmap bmp = new Bitmap(bmpPath);
            }
            catch (System.Exception ex)
            {
                Response.Write("<script>alert(" + ex + ")</script>");
            }
            double percent_C2H2 = Convert.ToDouble(Session["C2H2"]) / 100.0;
            double percent_CH4 = Convert.ToDouble(Session["CH4"]) / 100.0;
            double percent_C2H4 = Convert.ToDouble(Session["C2H4"]) / 100.0;
            float Left = (float)(304 - 280 * percent_C2H2 - 280 * percent_CH4 * 0.5 - 3);
            float Top = (float)(267 - 280 * percent_CH4 * 1.732 * 0.5 - 3);


            Color col = new Bitmap(bmpPath).GetPixel(Convert.ToInt16(Left), Convert.ToInt16(Top));
         
            //画立体图示的临时图，诊断原因红色显示
            try
            {
                Bitmap bmp_Three = new Bitmap(Server.MapPath("~/Image/ThreeShow.bmp"));
                // Color col = bmp_ThreeShow.GetPixel(Convert.ToInt16(Left), Convert.ToInt16(Top));

                for (int i = 0; i < bmp_Three.Width; i++)
                    for (int j = 0; j < bmp_Three.Height; j++)
                    {
                        Color color = bmp_Three.GetPixel(i, j);
                        if ((color.R == col.R) && (color.G == col.G) && (color.B == col.B))
                        {
                            bmp_Three.SetPixel(i, j, Color.Red);
                        }
                    }

                bmp_Three.Save(Server.MapPath("~/Image/TempImages/ThreeShow_Temp.bmp"), System.Drawing.Imaging.ImageFormat.Bmp);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert("+ex+")</script>") ;
            }


            //画大卫三角形的临时图，诊断原因红色显示
            try 
            {
                Bitmap bmp_David = new Bitmap(Server.MapPath("~/Image/DavidDiag.bmp"));
                for (int i = 0; i < bmp_David.Width; i++)
                    for (int j = 0; j < bmp_David.Height; j++)
                    {
                        Color color = bmp_David.GetPixel(i, j);
                        if ((color.R == col.R) && (color.G == col.G) && (color.B == col.B))
                        {
                            bmp_David.SetPixel(i, j, Color.Red);
                        }
                    }
                bmp_David.Save(Server.MapPath("~/Image/TempImages/DavidDiag_temp.bmp"), System.Drawing.Imaging.ImageFormat.Bmp);
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert(" + ex + ")</script>");
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            string diagType = Session["DiagType"].ToString();
            //DiagFlag标志的变化，控制图片来源，实现闪烁
            Session["DiagFlag"] = Convert.ToInt16(Session["DiagFlag"]) + 1;


            if ((Convert.ToInt16(Session["DiagFlag"]) % 2) == 0)
                this.Diag.ImageUrl = "~/Image/"+diagType+".bmp";
            else
                this.Diag.ImageUrl = "~/Image/TempImages/"+diagType+"_temp.bmp";
        }

        protected void ShowDiagButton_Click(object sender, EventArgs e)
        {
            if (Session["SelectedData"] == null)
            {
                ViewState["ErrorMsg"] = Language.Selected["Alert_SelData"];
                //Response.Write("<script>$(function(){$('#ErrorMsg p').innerText='" + Language.Selected["Alert_SelData"] + "';$('#ErrorMsg').show();})</script>");
                //Response.Write("<script>alert(" + Language.Selected["Alert_SelData"] + "')</script>");
              
                return;
            }
            if (Session["SelectedAbs"] == null)
            {
                ViewState["ErrorMsg"] = Language.Selected["Alert_SelAbs"];
                //Response.Write("<script>alert(" + Language.Selected["Alert_SelAbs"] + "')</script>");                
                return;
            }
            if (Session["SelectedRel"] == null)
            {
                ViewState["ErrorMsg"] = Language.Selected["Alert_SelRel"];
                //Response.Write("<script>alert(" + Language.Selected["Alert_SelRel"] + "')</script>");
                return;
            }
            
            RunningState data = (RunningState)Session["SelectedData"];
            RunningState abs = (RunningState)Session["SelectedAbs"];
            RunningState rel = (RunningState)Session["SelectedRel"];

            if (data.ReadDate <= abs.ReadDate)
            {
                ViewState["ErrorMsg"] = Language.Selected["Alert_TimeErr1"];
                //Response.Write("<script>alert('" + Language.Selected["Alert_TimeErr1"] + "')</script>");
                return;
            }
            if (data.ReadDate <= rel.ReadDate)
            {
                ViewState["ErrorMsg"] = Language.Selected["Alert_TimeErr2"];
                //Response.Write("<script>alert('" + Language.Selected["Alert_TimeErr2"] + "')</script>");
                return;
            }
            ViewState["ErrorMsg"] = "";
            switch (ShowDiagDrop.SelectedValue)
            {
                case "大卫三角形法":
                    Session["DiagType"] = "DavidDiag";                    
                    break;
                case "立体图示法":
                    Session["DiagType"] = "ThreeShow";                    
                    break;               
            }

            



            AnlyInformation anlyInfo=null;
            List<AlarmMsgAll> alarmList=null;

            MongoHelper<Config> _cfg = new MongoHelper<Config>();
            Expression<Func<Config, bool>> ex = p => p.DevID == devId && p.Alarm != null;
            Expression<Func<Config, bool>> ex1 = p => p.DevID == devId && p.AnalyPara.EnviSet != null;
            Config cfg=_cfg.FindOneBy(ex);
            if(cfg==null || cfg.Alarm==null || cfg.AnalyPara.EnviSet==null)
            {
                //。。。。。。。。。。。。。从下位机取。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
            }
            AlarmAll hold = cfg.Alarm;
            EnvironmentSetting setting = cfg.AnalyPara.EnviSet;
            if(hold==null || setting==null)
            {
                //从下位机取。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
            }
            //告警信息
            alarmList = Diagnose.GasAlarm(data, abs, rel, setting, hold);

            ////故障分析。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
            anlyInfo = Diagnose.GasDiagnose(devId, data, abs, rel, setting, hold);
            DiagnoseResult dr = Diagnose.WrapperAnlyResult(devId, 2, anlyInfo);

            TB_Result.Text = "";

            //TB_Result.Text += "\r\n------告警信息(" + alarmList.Length.ToString() + ")条------\r\n";//ljb:alarmList为空的时候不知道有没有Length这个属性（这个地方一直没法运行下去，估计这就是问题）
            TB_Result.Text += "\r\n------告警信息------\r\n";//ljb
            //有告警信息
            if (alarmList != null && alarmList.Count() != 0)
            {
                TB_Result.Text += "\r\n------告警信息(" + alarmList.Count().ToString() + ")条------\r\n";//ljb
                for (int i = 0; i < alarmList.Count(); i++)
                {
                    string msg = "";
                    msg += Language.Selected["Alarm_Device"] + devId + "\r\n";
                    msg += Language.Selected["Alarm_Gas"] + alarmList[i].GasName + "\r\n";

                    string msgType = "";
                    switch (alarmList[i].Type)
                    {
                        case 0:
                            msgType = Language.Selected["Alarm_Type_Content"];
                            break;
                        case 1:
                            msgType = Language.Selected["Alarm_Type_Abs"];
                            break;
                        case 2:
                            msgType = Language.Selected["Alarm_Type_Rel"];
                            break;
                        default:
                            msgType = Language.Selected["Alarm_Type_Content"];
                            break;
                    }

                    msg += Language.Selected["Alarm_Type"] + msgType + "\r\n";
                    msg += Language.Selected["Alarm_Value"] + alarmList[i].AlarmValue.ToString("F1") + ", " + Language.Selected["Alarm_RealValue"] + alarmList[i].RealValue.ToString("F3") + "\r\n";
                    msg += Language.Selected["Alarm_Level"] + WarringLevel[alarmList[i].Level] + "\r\n";

                    TB_Result.Text += msg + "\r\n";
                }
            }
            else
            {
                TB_Result.Text += "\r\n------无信息------\r\n";
            }

            //有故障信息
            TB_Result.Text += "\r\n------故障诊断信息------\r\n";
            if (anlyInfo != null)
            {
                
                //最终 诊断结果
                TB_Result.Text += "\r\n" + Language.Selected["Diag_Result"] + "\r\n" + dr.Result + "\r\n";
                //其它诊断中间数据，三比值、大卫三角形、立体图示
                TB_Result.Text += "\r\n" + Language.Selected["ThreeRatio_Diag_Result"] + "\r\n" + dr.ThreeRatioCode + "(" + dr.ThreeRatioResult + ")\r\n";
                TB_Result.Text += "\r\n" + Language.Selected["David_Result"] + "\r\n" + dr.DevidCode + "(" + dr.DevidResult + ")\r\n";
                TB_Result.Text += "\r\n" + Language.Selected["Cube_Diag_Result"] + "\r\n" + dr.CubeCode + "\r\n";

                //
                LB_Ratio.Text = dr.ThreeRatioCode;

                //大卫三角形数据
                //"C2H2:10%, C2H4:20%, CH4:30%"
                string sr = dr.DevidCode;
                string[] str = sr.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    string[] temp = str[i].Split(':');
                    if (temp[0] == "C2H2" || temp[0] == "C2H4" || temp[0] == "CH4")
                    {
                        Session[temp[0]] = temp[1];
                    }
                }
                if(Session["C2H2"]!=null & Session["C2H4"]!=null && Session["CH4"]!=null)
                {
                    get_temp_Map();
                    Timer1.Enabled = true;
                }

                else
                {
                    Response.Write("<script>alert(" +"session出错，请重新诊断！" + "')</script>");
                }
                //dr = dr.Replace('%', ' ');
                //dr = dr.Replace(',', '&');
                //dr = dr.Replace(":", "=");
                //dr = dr.Replace(" ", "");
                //string str = "javascript:showPopWin('" + Language.Selected["Title_Sample"] + "', 'DevidShow.aspx?";
                //str += dr;
                //str += "',  620, 330, null, true, true);return false;";
                //BT_Devid.OnClientClick = str;
                //BT_Devid.Enabled = true;

                //string str1 = "javascript:showPopWin('" + Language.Selected["CubeShow"] + "', 'ThreeShow.aspx?";
                //str1 += dr;
                //str1 += "',  900, 440, null, true, true);return false;";
                //BT_Cube.OnClientClick = str1;
                //BT_Cube.Enabled = true;
                ////立体图 命令参数
                ////"C2H2/C2H4=1, C2H4/C2H6=1, CH4/H2=1"
//#if false
//                                    //dr = anlyInfo._cubecode;

//                                    //string cmd = anlyInfo.Value.ratio.CH4_H2.ToString() + ",";
//                                    //cmd += anlyInfo.Value.ratio.C2H2_C2H4.ToString() + ",";
//                                    //cmd += anlyInfo.Value.ratio.C2H4_C2H6.ToString();
//                                    //ViewState["CMD"] = cmd;
//#else
//                ViewState["CMD"] = null;//这是谁改的？它和else里面的一样，那ViewState["CMD"]永远为null了，上面这段为什么注释掉？--ljb
//#endif

                
            }
            else
            {
                TB_Result.Text += "\r\n------无信息------\r\n";

                //ljb
                //BT_Devid.Enabled = true;
                //BT_Devid.Enabled = false;
                //BT_Cube.Enabled = false;

                ViewState["CMD"] = null;
            }
        }
    }
}