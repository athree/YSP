using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class SelectGasData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                BT_Return.Text = Language.Selected["Return"];

                //选择的数据类型： 分析数据、对比数据ABS、对比数据REL
                ViewState["type"] = Request.QueryString["type"];
                if (Request.QueryString["id"] == null)
                {
                    Response.Write("<script>alert('" + Language.Selected["IDError"] + "')</script>");
                    return;
                }

                //设备ID
                string id ="";
                if (Request.QueryString["id"]!="")
                {
                    id=Request.QueryString["id"].ToString();
                    this.BindData(id);
                    ViewState["id"] = id;
                }
                else
                {
                    Response.Write("<script>alert('" + Language.Selected["IDError"] + "')</script>");
                }
            }
        }

        private void BindData(string deviceID)
        {
            

                MongoHelper<SIML> _siml = new MongoHelper<SIML>();
                Expression<Func<SIML, bool>> ex = p => p.DevID == deviceID && p.Content != null;
                ContentData[] gasList = _siml.FindBy(ex).Select(p=>p.Content).ToArray();

                ViewState["gasList"] = gasList;

                DataTable dt = new DataTable();
                dt.Columns.Add("Num", typeof(int));
                dt.Columns.Add(Language.Selected["Title_Content"], typeof(DateTime));
                dt.Columns.Add(Language.Selected["H2"], typeof(Decimal));
                dt.Columns.Add(Language.Selected["CO"], typeof(Decimal));
                dt.Columns.Add(Language.Selected["CH4"], typeof(Decimal));
                dt.Columns.Add(Language.Selected["C2H2"], typeof(Decimal));
                dt.Columns.Add(Language.Selected["C2H4"], typeof(Decimal));
                dt.Columns.Add(Language.Selected["C2H6"], typeof(Decimal));
                dt.Columns.Add(Language.Selected["CO2"], typeof(Decimal));
                dt.Columns.Add(Language.Selected["ZongT"], typeof(Decimal));


                int rowCount = gasList.Length;
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow row = dt.NewRow();
                    row["Num"] = i;
                    row[Language.Selected["Title_Content"]] = gasList[i].ReadDate;
                    row[Language.Selected["H2"]] = gasList[i].H2;
                    row[Language.Selected["CO"]] = gasList[i].CO;
                    row[Language.Selected["CH4"]] = gasList[i].CH4;
                    row[Language.Selected["C2H2"]] = gasList[i].C2H2;
                    row[Language.Selected["C2H4"]] = gasList[i].C2H4;
                    row[Language.Selected["C2H6"]] = gasList[i].C2H6;
                    row[Language.Selected["CO2"]] = gasList[i].CO2;
                    row[Language.Selected["ZongT"]] = gasList[i].TotHyd;


                    dt.Rows.Add(row);
                }
                DataView dv = new DataView(dt);//ljb20131204
                dv.Sort = "Num DESC";//ljb20131204

                //this.GridView1.DataSource = dt;//ljb20131204
                this.GridView1.DataSource = dv;//ljb20131204
                this.GridView1.DataBind();

            }
        

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int num = (int)GridView1.SelectedValue;
            ContentData[] gasList = (ContentData[])ViewState["gasList"];

            string type = ViewState["type"].ToString();
            switch (type)
            {
                case "data":
                    Session["SelectedData"] = gasList[num];
                    break;

                case "abs":
                    Session["SelectedAbs"] = gasList[num];
                    break;

                case "rel":
                    Session["SelectedRel"] = gasList[num];
                    break;

                default:
                    Session["SelectedData"] = gasList[num];
                    break;
            }

            //比较选择的数据的时间是否正确

        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[1].Visible = false;
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {

        }

        protected void BT_Return_Click(object sender, EventArgs e)
        {
            Response.Redirect("../DevAlarm/YSP_A.aspx?id=" + ((string)ViewState["id"]).ToString());
           
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData((string)ViewState["id"]);
        }
    }
}