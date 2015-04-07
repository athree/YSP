using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysLog;

namespace WebApplication1.Data
{
    public partial class DataBak : System.Web.UI.Page
    {
        protected FileLog bakFile = new FileLog(@"d:\data\bakSet.txt");
        protected void Page_Load(object sender, EventArgs e)
        {  
            if(!IsPostBack)
            {
                //读取数据备份间隔，显示到界面                
                string str = bakFile.Read().Substring(9, 1);
                DD_BakInter.SelectedValue = str;
                LB_mIn.Text = str;
            }            
        }
        protected void BT_Chage_Click(object sender, EventArgs e)
        {
           //设置数据备份间隔,修改对应文件  
            if(DD_BakInter.SelectedValue!=LB_mIn.Text.Substring(0,1))
            {
                bakFile.Write("数据备份间隔月数:" + DD_BakInter.SelectedValue);
                bakFile.Dispose();
                LB_mIn.Text = DD_BakInter.SelectedValue;

            }
            
        }
    }
}