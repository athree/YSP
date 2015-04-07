using System;
using System.Linq;
using IMserver.DBservice;
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

