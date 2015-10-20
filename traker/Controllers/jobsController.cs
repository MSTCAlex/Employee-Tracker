using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using traker.Models;


namespace traker.Controllers
{
    public class jobsController : Controller
    {
        private jobDb db = new jobDb();

        // GET: jobs
        [Authorize(Roles ="employer")]
        public ActionResult Index()
        {
            return View(db.job.ToList());
        }

        [Authorize]
        public ActionResult GetMyJobs()
        {
            RolesManager roles = new RolesManager();
            var rolename = roles.getUserRole(System.Web.HttpContext.Current.User.Identity.Name);
            if (rolename == "employer")
            {
                string userId = roles.getUserId(System.Web.HttpContext.Current.User.Identity.Name);
                try
                {
                    return View(db.job.Where(m => m.employerId.ToString() == userId));
                }
                catch (Exception)
                {
                    return View();
                }
            }
            else
            {
                string userId = roles.getUserId(System.Web.HttpContext.Current.User.Identity.Name);
                try
                {
                    return View(db.job.Where(m => m.employeeId.ToString() == userId));
                }
                catch (Exception)
                {
                    return View();
                }
            }            
        }


        // GET: jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job job = db.job.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: jobs/Create
        [Authorize(Roles = "employee")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,TotalHours")] job job)
        {
            //employeeId,employerId
            if (ModelState.IsValid)
            {
                RolesManager roles = new RolesManager();
                job.employeeId = roles.getUserId(System.Web.HttpContext.Current.User.Identity.Name);
                db.job.Add(job);
                db.SaveChanges();
                return RedirectToAction("GetMyJobs");
            }

            return View(job);
        }

        // GET: jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job job = db.job.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,employeeId,employerId,Title,Description,TotalHours")] job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        //GET: jobs/Apply/5
        public ActionResult Apply(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job job = db.job.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);

        }


        // GET: jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job job = db.job.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            job job = db.job.Find(id);
            db.job.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
