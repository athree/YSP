using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using IMserver.DBservice;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using IMserver.Models;

namespace WebApplication1.DevTypes
{
    public partial class Insert : System.Web.UI.Page
    {
        protected MongoHelper<DevType> _DevType;
        protected MongoHelper<DevType> _devType;
        protected MongoHelper<DevSite> _devSite;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // This is the Insert method to insert the entered DevType item
        // USAGE: <asp:FormView InsertMethod="InsertItem">
        public void InsertItem()
        {
            _DevType=new MongoHelper<DevType>();
            var item = new IMserver.Models.DevType();

            TryUpdateModel(item);
           
        
          
            Expression<Func<DevType, bool>> ex = p =>p.TypeId==item.TypeId;
            if (_DevType.FindBy(ex).Count()>0)
            {
                Response.Write("<script>alert('已存在拥有该设备号的设备！无法添加！');</script>");
                return;
            }
            else
            {
                
                if (ModelState.IsValid)
                {
                    try
                    {
                        _DevType.Insert(item);
                    }
                    catch (NullReferenceException dbEx)
                    {

                        Response.Write("<script>alert(" + dbEx.Message + ");</script>");
                    }


                    Response.Redirect("Default");
                }

            }
        }

        public IQueryable<DevType> GetDevType()
        {
            _devType = new MongoHelper<DevType>();
            return _devType.FindAll().AsQueryable();

        }

        public IQueryable<DevSite> GetDevSite()
        {
            _devSite = new MongoHelper<DevSite>();
            return _devSite.FindAll().AsQueryable();
        }

        protected void ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("Default");
            }
        }

    }
}
