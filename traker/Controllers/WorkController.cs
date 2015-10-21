using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using traker.Models;

namespace traker.Controllers
{
    public class WorkController : Controller
    {
        private jobDb db = new jobDb();

        // GET: Work
        public ActionResult Index()
        { 
            return View(db.job.ToList());
        }

        public ActionResult MyJobs()
        {
            var all_jobs = db.job.ToList();
            List<job> thisUserJobs = new List<job>();
            foreach (var job in all_jobs)
            {
                var name = System.Web.HttpContext.Current.User.Identity.Name;
                RolesManager role = new RolesManager();
                var userId = role.getUserId(name);
                if (job.employerId == userId)
                {
                    thisUserJobs.Add(job);
                }
            }

            return View(thisUserJobs);
        }


        public ActionResult getToWork()
        {
            return View();
        }


        // GET: Work/Details/5
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

        // GET: Work/Create
        [Authorize(Roles ="employee")]
        public ActionResult Create(string employer)
        {
            if (employer != null)
            {
                RolesManager role = new RolesManager();
                
            }
            return View();
        }

        public ActionResult hire()
        {
            RolesManager roles = new RolesManager();
            var emplyers = roles.getAllEmployers();

            ViewBag.employers = roles.getAllEmployers();
            return View();
        }

        // POST: Work/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,TotalHoursWorked,PricePerHour")] job job, string employer)
        {
            if (ModelState.IsValid)
            {
                RolesManager roles = new RolesManager();
                job.employeeId = roles.getUserId(System.Web.HttpContext.Current.User.Identity.Name);
                job.employerId = employer;
                db.job.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        // GET: Work/Edit/5
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

        // POST: Work/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,employeeId,employerId,Title,Description,TotalHoursWorked,PricePerHour")] job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // GET: Work/Delete/5
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

        // POST: Work/Delete/5
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
