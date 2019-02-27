using DauGiaTrucTuyen.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(DauGiaTrucTuyen.Startup))]
namespace DauGiaTrucTuyen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsersDefault();
        }
        public void createRolesandUsersDefault()
        {
            string emailAdmin = ConfigurationManager.AppSettings["EmailAdmin"];
            string pwdAdmin = ConfigurationManager.AppSettings["PwdAdmin"];
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = emailAdmin;
                user.Email = emailAdmin;
                user.CreateDate = DateTime.Now;
                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = true;

                var addUser = UserManager.Create(user, pwdAdmin);

                //Add default User to Role Admin   
                if (addUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
