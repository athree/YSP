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

namespace WebApplication1.DevTypes
{
    public partial class Default : System.Web.UI.Page
    {
        protected MongoHelper<DevType> _DevType=new MongoHelper<DevType>();
        protected IQueryable<DevType> types;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Model binding method to get List of DevType_view entries
        // USAGE: <asp:ListView SelectMethod="GetData">
        public IQueryable<IMserver.Models.DevType> GetData()
        {
            try { types= _DevType.FindAll().AsQueryable(); }
            catch (NullReferenceException dbEx)
            {
                Response.Write("<script>alert('" + dbEx.Message +"');</script>");
            }
            return types;
        }
    }
}

