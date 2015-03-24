using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dundas.Gauges.WebControl;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace WebApplication1.DevDebug
{
    public partial class YSP_Gauge : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
           
            string tt = "{this:6}";
            
            string nn = JsonConvert.SerializeObject(tt);
         

        }
        public class A
        {
            public string name{get;set;}
        }
    }
}