using System;
using System.Linq;
using IMserver.DBservice;
using IMserver.Models;


namespace WebApplication1.DevSites
{
    public partial class Default : System.Web.UI.Page
    {
		protected MongoHelper<DevSite> _devSite=new MongoHelper<DevSite>();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Model binding method to get List of DevSite entries
        // USAGE: <asp:ListView SelectMethod="GetData">
        public IQueryable<IMserver.Models.DevSite> GetData()
        {
            return _devSite.FindAll();
        }
    }
}

