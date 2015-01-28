using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMserver.DBservice;
using IMserver.Models;
using IMserver.Models.SimlDefine;

namespace WebApplication1.DevInfoes
{
    public partial class SelectData : System.Web.UI.Page
    {
        public class DataInfo {
            public string DataID;
            public DateTime Time;
            public DataInfo(string id,DateTime date)
            {
                this.DataID = id;
                this.Time = date;
            }
            public DataInfo() { }
        }
        public int TotalDataNum
        {
            get
            {
                if (cbl_Data == null || cbl_Data.Items == null)
                {
                    return 0;
                }
                return cbl_Data.Items.Count;
            }
        }

        public int SelDataNum//不知道此属性在什么地方使用过？--ljb
        {
            get
            {
                object obj = ViewState["SelDataNum"];
                if (obj == null)
                {
                    return 0;
                }
                return (int)obj;
            }
            set
            {
                ViewState["TimeZone"] = value;
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                //
                bt_Sel.Text = Language.Selected["Selected"];

                //取得设备号 并 做数据绑定显示

                string deviceid = Request.QueryString["DevID"].ToString();
                BindDataList(deviceid);
               

                //清空Sessions

                //Session["DataID_List"] = null;

                //Session["CO2Data_List"] = null;
                //Session["SixData_List"] = null;
                //Session["SixRawData_List"] = null;
                //Session["ConfigData_List"] = null;
                //Session["DensityInfo_List"] = null;

                //Session["MaxY"] = null; 
                //Session["MinY"] = null;
                //Session["MaxX"] = null;
                //Session["MinX"] = null;
            }



        }

        private void BindDataList(string deviceID)
        {
           
            try
            {
                MongoHelper<SIML> _siml = new MongoHelper<SIML>();
                Expression<Func<SIML, bool>> ex = p => p.DevID == deviceID && p.Raw != null;
                Expression<Func<SIML, DataInfo>> ex1 = p => new DataInfo(p.DataID.ToString(), p.Raw.SampleTime);
                IQueryable<DataInfo> infoList = _siml.FindBy(ex).Select(ex1);
                
                if (infoList != null && infoList.Count() != 0)
                {
                    cbl_Data.DataSource = infoList;
                    cbl_Data.DataTextField = "Time";
                    cbl_Data.DataValueField = "DataID";
                    cbl_Data.DataBind();
                }
                else
                {
                    ViewState["ErrorMsg"]="无数据";
                }
            }
            catch (System.Exception ex)
            {
                ViewState["ErrorMsg"]=ex.Message;
                
            }
            
        
        }



        protected void bt_Sel_Click(object sender, EventArgs e)
        {
            //
            List<DataInfo> dataIDList = new List<DataInfo>();
            for (int i = 0; i < TotalDataNum; i++)
            {
                DataInfo di = new DataInfo();
                if (cbl_Data.Items[i].Selected)     //被勾选
                {
                    di.DataID =cbl_Data.Items[i].Value;
                    di.Time = Convert.ToDateTime(cbl_Data.Items[i].Text);
                    dataIDList.Add(di);
                }
            }

            //
            if (dataIDList.Count == 0)
            {
                //WebDefine.AjaxMsgBox(Language.Selected["SelDataFirst"], UpdatePanel1);
                return;
            }
            //if (dataIDList.Count >= 5)
            //{
            //    WebDefine.AjaxMsgBox(Language.Selected["Max5"], UpdatePanel1);
            //    return;
            //}

            //清空Sessions
            //Session["DataID_List"] = null;

            //Session["CO2Data_List"] = null;
            //Session["SixData_List"] = null;
            //Session["SixRawData_List"] = null;
            //Session["ConfigData_List"] = null;
            //Session["DensityInfo_List"] = null;

            //Session["MaxY"] = null;
            //Session["MinY"] = null;
            //Session["MaxX"] = null;
            //Session["MinX"] = null;

            //将 dataID List放入上下文环境中
            Session["DataID_List"] = dataIDList;

            Response.Redirect("~/DevDebug/YSP_De.aspx");
            //ScriptManager.RegisterStartupScript(bt_Sel, this.GetType(), "reload",
            //    "window.top.location.reload();", true);
            //ScriptManager.RegisterStartupScript(bt_Sel, this.GetType(), "reload",
            //    "window.top.location.assign('Default.aspx?type=4');", true);
            //ScriptManager.RegisterStartupScript(bt_Sel, this.GetType(), "reload",
            //    "window.top.location.href('Default.aspx?type=4');", true);
        }

        protected void cbl_Data_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////第 index 个复选框发生变化？？
            //string s = Request.Form["__EVENTTARGET"];
            //int index = Convert.ToInt32(s.Substring(s.LastIndexOf("$") + 1));
        }

        protected void bt_Return_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DevDebug/YSP_De.aspx");
        }
        
    }
}