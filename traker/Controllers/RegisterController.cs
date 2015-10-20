using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using traker.Models;

namespace traker.Controllers
{
    public class RegisterController : Controller
    {
        
        // GET: Register/Create
        public ActionResult Create(string role)
        {
            ViewBag.role = role;
            return View();
        }

        // POST: Register/Create
        [HttpPost]
        public ActionResult Create(string name , string password, string role)
        {
            try
            {
                RolesManager create_role = new RolesManager();
                create_role.AddUserAndRole(name , role, password);
                return RedirectToAction("Login", "Account");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
