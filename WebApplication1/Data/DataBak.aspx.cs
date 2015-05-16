using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysLog;
using System.IO;
using System.Text;

namespace WebApplication1.Data
{
    public partial class DataBak : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {  
            if(!IsPostBack)
            {
                StreamReader sr = new StreamReader(@"d:\data\bakSet.txt", Encoding.Default);
                //读取数据备份间隔，显示到界面                
                string str = sr.ReadToEnd().Substring(9, 1);
                DD_BakInter.SelectedValue = str;
                LB_mIn.Text = str;
                sr.Dispose();
            }            
        }
        protected void BT_Chage_Click(object sender, EventArgs e)
        {
           //设置数据备份间隔,修改对应文件  
            if(DD_BakInter.SelectedValue!=LB_mIn.Text.Substring(0,1))
            {
                StreamWriter sw = new StreamWriter(@"d:\data\bakSet.txt" , false , Encoding.Default);
                sw.WriteLine("数据备份间隔月数:" + DD_BakInter.SelectedValue);
                sw.Dispose();
                LB_mIn.Text = DD_BakInter.SelectedValue;

            }
            
        }
    }
}