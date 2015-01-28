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

namespace WebApplication1.DevInfoes
{
    public partial class Insert : System.Web.UI.Page
    {
        protected MongoHelper<DevInfo> _devInfo;
        protected MongoHelper<DevType> _devType;
        protected MongoHelper<DevSite> _devSite;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // This is the Insert method to insert the entered DevInfo item
        // USAGE: <asp:FormView InsertMethod="InsertItem">
        public void InsertItem()
        {
            _devInfo=new MongoHelper<DevInfo>();
            var item = new IMserver.Models.DevInfo();


            item.Type = (MyFormView.FindControl("MyDevType") as DropDownList).SelectedValue;
            item.CompName = (MyFormView.FindControl("MyCompName") as DropDownList).SelectedValue;
            TryUpdateModel(item);
           
        
          
            Expression<Func<DevInfo, bool>> ex = p =>p.DevID==item.DevID;
            if (_devInfo.FindBy(ex).Count()>0)
            {
                Response.Write("<script>alert('已存在拥有该设备号的设备！无法添加！');</script>");
                return;
            }
            else
            {
                //取得系统时间作为设备添加时间。但是mongo默认为UTC时间会减去8小时，后取出显示时出错，所以在这加8小时
                item.AddTime = DateTime.Now.AddHours(8);

                if (ModelState.IsValid)
                {
                    try
                    {
                        _devInfo.Insert(item);
                    }
                    catch (NullReferenceException dbEx)
                    {

                        Response.Write("<script>alert(" + dbEx.Message + ");</script>");
                    }


                    //_db.DevInfo_view.Add(item);
                    //_db.SaveChanges();

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
