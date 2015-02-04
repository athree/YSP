using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Data
{
    public partial class DataBak : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BT_Chage_Click(object sender, EventArgs e)
        {
            int mInterval = Int32.Parse(DD_BakInter.SelectedValue);
            if (mInterval != 1)
            {
                Global.MInterval = mInterval;
            }

        }
    }
}