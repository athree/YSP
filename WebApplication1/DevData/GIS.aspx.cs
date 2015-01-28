using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.DevData
{
    public partial class GIS : System.Web.UI.Page
    {
        public string devId, devSite, devType, devName;
        protected void Page_Load(object sender, EventArgs e)
        {
            devId = HttpUtility.UrlDecode(Request.QueryString["DevID"]);
            devSite = HttpUtility.UrlDecode(Request.QueryString["DevSite"]);
            devType = HttpUtility.UrlDecode(Request.QueryString["DevType"]);
            devName = HttpUtility.UrlDecode(Request.QueryString["DevName"]);
        }
    }
}