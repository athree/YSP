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
    public class RoleActions
    {
        protected NameValueCollection config;
        protected MongoRoleProvider roleProvider;
        public RoleActions() {
            config = new NameValueCollection();
            System.Web.Configuration.MembershipSection configSection = (System.Web.Configuration.MembershipSection)ConfigurationManager.GetSection("system.web/membership");
            config.Add(configSection.Providers[0].Parameters);
            roleProvider = new MongoRoleProvider();
            roleProvider.Initialize("MongoMembershipProvider", config);
        }
        internal void createRole()
        {           

            if (!roleProvider.RoleExists("Admin"))
            {
                roleProvider.CreateRole("Admin");
                if (!roleProvider.IsUserInRole("admin", "Admin"))
                {
                    
                    if (createUser("admin", "admin123", null))
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
        public bool createUser(string userName,string password,string email)
        {
            MongoMembershipProvider membershipProvider = new MongoMembershipProvider();
            MembershipCreateStatus createStatus = new MembershipCreateStatus();
            membershipProvider.Initialize("MongoMembershipProvider", config);
            membershipProvider.CreateUser(userName, password, email, null, null, true, null, out createStatus);
            if(createStatus==MembershipCreateStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetRolesForUser(string userName)
        {
            string[] roles=roleProvider.GetRolesForUser(userName);
            if(roles.Length>1)
            {
                for (int k = 1; k < roles.Length;k++ )
                {
                    roles[0] =roles[0]+"|"+ roles[k];
                }
            }
            return roles[0];
            
        }



        }
      
    }

