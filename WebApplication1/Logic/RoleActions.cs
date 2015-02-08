using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using IMserver.Models;
using MongoMembership.Providers;
using System.Collections.Specialized;
using System.Web.Security;
using System.Configuration;

namespace WebApplication1.Logic
{
    internal class RoleActions
    {
        internal void createRole()
        {
            MongoRoleProvider roleProvider = new MongoRoleProvider();
            NameValueCollection config = new NameValueCollection();

            System.Web.Configuration.MembershipSection configSection = (System.Web.Configuration.MembershipSection)ConfigurationManager.GetSection("system.web/membership");
            config.Add(configSection.Providers[0].Parameters);


            roleProvider.Initialize("MongoMembershipProvider", config);

            if (!roleProvider.RoleExists("Admin"))
            {
                roleProvider.CreateRole("Admin");
                if (!roleProvider.IsUserInRole("admin", "Admin"))
                {
                    MongoMembershipProvider membershipProvider = new MongoMembershipProvider();
                    MembershipCreateStatus createStatus = new MembershipCreateStatus();
                    membershipProvider.Initialize("MongoMembershipProvider", config);
                    membershipProvider.CreateUser("admin", "admin123", null, null, null, true, null, out createStatus);
                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        roleProvider.AddUsersToRoles(new[] { "admin" }, new[] { "Admin" });
                    }
                    else
                    {
                        //出错处理
                    }
                }
            }
            
            }
        }
    }

