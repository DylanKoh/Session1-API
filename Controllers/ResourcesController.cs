using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Session1_API;

namespace Session1_API.Controllers
{
    public class ResourcesController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public ResourcesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: Resources
        [HttpPost]
        public ActionResult Index()
        {
            var resources = db.Resources.Include(r => r.Resource_Type);
            return new JsonResult { Data = resources.ToList() };
        }

        // GET: Resources/Details/5
        [HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return Json("Resource does not exist!");
            }
            return Json(resource);
        }


        // POST: Resources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "resId,resName,resTypeIdFK,remainingQuantity")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Resources.Add(resource);
                db.SaveChanges();
                var newID = db.Resources.Where(x => x.resName == resource.resName).Select(x => x.resId).FirstOrDefault();
                return Json(newID);
            }

            return Json("Unable to create resources");
        }


        // POST: Resources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "resId,resName,resTypeIdFK,remainingQuantity")] Resource resource)
        {
            if (db.Resources.Where(x => x == resource).Select(x => x).FirstOrDefault() == null)
            {
                return Json("Resource cannot be found or does not exist!");
            }
            if (ModelState.IsValid)
            {
                db.Entry(resource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Resource edited successfully!");
            }
            return Json("Unable to edit resource");
        }

       

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return Json("Unable to find resource!");
            }
            db.Resources.Remove(resource);
            db.SaveChanges();
            return Json("Resource has been removed successfully");
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
