using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using IMserver.DBservice;
using System.Linq.Expressions;
using IMserver.Models;
using MongoDB.Bson;

namespace WebApplication1.DevTypes
{
    public partial class Delete : System.Web.UI.Page
    {
        protected MongoHelper<DevType> _DevType = new MongoHelper<DevType>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // This is the Delete methd to delete the selected DevType_view item
        // USAGE: <asp:FormView DeleteMethod="DeleteItem">
        public void DeleteItem(string TypeId)
        {
            Expression<Func<DevType, bool>> ex = p=>p.TypeId== new ObjectId(TypeId);
            _DevType.Delete(ex);
            Response.Redirect("../Default");
        }

        // This is the Select methd to selects a single DevType_view item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public IMserver.Models.DevType GetItem([FriendlyUrlSegmentsAttribute(0)]string TypeId)
        {
            if (TypeId == "")
            {
                return null;
            }
            Expression<Func<DevType, bool>> ex = p => p.TypeId==new ObjectId(TypeId);
            return _DevType.FindOneBy(ex);
            
        }

        protected void ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("../Default");
            }
        }
    }
}

