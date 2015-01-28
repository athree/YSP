using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using IMserver.DBservice;
using System.Data.Entity.Validation;
using IMserver.Models;

namespace WebApplication1.DevInfoes
{
    public partial class Default : System.Web.UI.Page
    {
        protected MongoHelper<DevInfo> _devInfo=new MongoHelper<DevInfo>();
        protected IQueryable<DevInfo> devInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Model binding method to get List of DevInfo_view entries
        // USAGE: <asp:ListView SelectMethod="GetData">
        public IQueryable<IMserver.Models.DevInfo> GetData()
        {
            var a=_devInfo.FindAll();
            try { devInfo= _devInfo.FindAll().AsQueryable(); }
            catch (NullReferenceException dbEx)
            {
                Response.Write("<script>alert('" + dbEx.Message +"');</script>");
            }
            return devInfo;
        }
    }
}

