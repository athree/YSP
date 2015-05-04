using IMserver;
using IMserver.Data_Warehousing;
using IMserver.DBservice;
using IMserver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication1.DevData
{
    public partial class YSP : System.Web.UI.Page
    {
        protected MongoHelper<RunningState> _rs = new MongoHelper<RunningState>();
        protected RunningState myRs;
        public string devId,devSite,devName,devType;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            devId = HttpUtility.UrlDecode(Request.QueryString["DevID"]);
            devSite = HttpUtility.UrlDecode(Request.QueryString["DevSite"]);
            devType = HttpUtility.UrlDecode(Request.QueryString["DevType"]);
            devName = HttpUtility.UrlDecode(Request.QueryString["DevName"]);
            Session["DevID"] = devId;
            Session["DevSite"] = devSite;
            Session["DevType"] = devType;
            Session["DevName"] = devName;
            Session["SelectedData"] = null;
            Session["SelectedAbs"] = null;
            Session["SelectedRel"] = null;
            InitCtrls();
        }
        public void InitCtrls()
        {
            Expression<Func<RunningState, bool>> ex = p => p.DevID == devId;
            myRs = _rs.FindOneBy(ex);
            if (myRs == null)
            {
                Response.Write("<script>alert('需要传输该设备信息！')</script>");
                Dictionary<ushort, object> rsDic = new GetData().GetRS();
                for (int i = 0; i < rsDic.Count; i++)
                {
                    ushort key = rsDic.ElementAt(i).Key;
                    object value = rsDic.ElementAt(i).Value;
                    Label LB = (Label)Page.FindControl("LB_" + key);  //查找前台对应label后进行赋值
                    LB.Text = ((float)value).ToString();
                }
                try
                {
                    AddRunningState.Warehousing(rsDic, Byte.Parse(devId));
                }
                catch (Exception e)
                {

                }
                return;
            }
            else
            {
                LB_89.Text = myRs.Temprature_Out.ToString();
                LB_88.Text = myRs.Temprature_In.ToString();
                LB_145.Text = myRs.GasPressure.ToString();
                LB_87.Text = myRs.OilTemprature.ToString();
                LB_64.Text = myRs.SePuZhuT.ToString();
                LB_62.Text = myRs.SensorRoomT.ToString();
                LB_63.Text = myRs.LengJingT.ToString();

                LB_166.Text = myRs.H2.ToString();
                LB_167.Text = myRs.CO.ToString();
                LB_169.Text = myRs.CO2.ToString();
                LB_168.Text = myRs.CH4.ToString();
                LB_170.Text = myRs.C2H2.ToString();
                LB_171.Text = myRs.C2H4.ToString();
                LB_172.Text = myRs.C2H6.ToString();
                LB_176.Text = myRs.TotHyd.ToString();
                LB_rs.Text = myRs.TotGas.ToString();
                LB_175.Text = myRs.Mst.ToString();
                LB_174.Text = myRs.T.ToString();
                LB_173.Text = myRs.AW.ToString();
            }
           
        }

       
        protected void BT_Data_Click(object sender, EventArgs e)
        {
            BT_Data.Text= "hello";
            //按照用户选取的时间段获得历史数据 
            if (new GetData().GetRS(BeginTime.Text, EndTime.Text))
            { }
            else {
                ///出错处理
            }
                          
        }

       
    }
}