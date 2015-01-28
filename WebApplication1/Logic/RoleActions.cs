using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using IMserver.Models;

namespace WebApplication1.Logic
{
    internal class RoleActions
    {
        internal void createRole()
        {
            // 创建用户上下文
            ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;


            // 由上下文生成roleStore，相当于管理角色的接口
            var roleStore = new RoleStore<IdentityRole>(context);

            // 由roleStore生成roleManager相当于角色管理器 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

             //若不存在则创建Administrator（管理员）角色
            try {
                 if (!roleMgr.RoleExists("Administrator"))
                 {
                    IdRoleResult = roleMgr.Create(new IdentityRole("Administrator"));
                    if (!IdRoleResult.Succeeded)
                    {
                        // 错误处理
                    }
                 }
            }
            catch(Exception ex){
                Console.WriteLine("<script>alert("+ex.Message+");</script>");
            };
           
          


                // 若不存在则创建User（普通用户）角色
                if (!roleMgr.RoleExists("User"))
                {
                    IdRoleResult = roleMgr.Create(new IdentityRole("User"));
                    if (!IdRoleResult.Succeeded)
                    {
                        // 错误处理 
                    }
                }


                // 创建用户管理器 
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                //创建用户名为Admin的用户
                var appUser = new ApplicationUser()
                {
                    UserName = "Admin",
                };
                IdUserResult = userMgr.Create(appUser, "123456");


                // 创建成功则将 Admin 用户添加为管理员角色
                if (IdUserResult.Succeeded)
                {

                    IdUserResult = userMgr.AddToRole(appUser.Id, "Administrator");
                    if (!IdUserResult.Succeeded)
                    {
                        // 错误处理
                    }
                }
                else
                {
                    // 错误处理 
                }
            }
        }
    }

