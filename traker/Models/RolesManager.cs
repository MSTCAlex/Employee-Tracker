using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace traker.Models
{
    internal class RolesManager
    {
        internal void AddUserAndRole(String useremail, String rolename, String password)
        {
            // Access the application context and create result variables.
            Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(context);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Then, you create the "canEdit" role if it doesn't already exist.
            if (!roleMgr.RoleExists(rolename))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = rolename });
            }

            // Create a UserManager object based on the UserStore object and the ApplicationDbContext  
            // object. Note that you can create new objects and use them as parameters in
            // a single line of code, rather than using multiple lines of code, as you did
            // for the RoleManager object.
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            {
                UserName = useremail,
                Email = useremail
            };
            IdUserResult = userMgr.Create(appUser, password);

            // If the new "canEdit" user was successfully created, 
            // add the "canEdit" user to the "canEdit" role. 
            if (!userMgr.IsInRole(userMgr.FindByEmail(useremail).Id, rolename))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail(useremail).Id, rolename);
            }

        }

        public string getUserRole(string user_name)
        {
            Models.ApplicationDbContext context = new ApplicationDbContext();
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            var userRoleId = userMgr.FindByEmail(user_name).Roles.First().RoleId;
            var rolename = roleMgr.Roles.Where(m => m.Id == userRoleId).First().Name;

            return rolename;
        }

        public List<ApplicationUser> getAllEmployers()
        {
            Models.ApplicationDbContext context = new ApplicationDbContext();
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            List<ApplicationUser> allEmployers = new List<ApplicationUser>();
            foreach (var user in userMgr.Users)
            {
                var username = user.UserName;
                var userRole = getUserRole(username);
                if (userRole == "employer")
                {
                    allEmployers.Add(user);
                }
            }
            return allEmployers;        
        }

        public string getUserId(string user_name)
        {
            Models.ApplicationDbContext context = new ApplicationDbContext();
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var Id = userMgr.FindByEmail(user_name).Id;
            return Id;
        }


    }
}