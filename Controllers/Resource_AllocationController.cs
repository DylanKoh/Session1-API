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
    public class Resource_AllocationController : Controller
    {
        private Session1Entities db = new Session1Entities();
        public Resource_AllocationController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: Resource_Allocation
        [HttpPost]
        public ActionResult Index()
        {
            var resource_Allocation = db.Resource_Allocation;
            return new JsonResult { Data = resource_Allocation.ToList() };
        }

        // GET: Resource_Allocation/Details/5
        [HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource_Allocation resource_Allocation = db.Resource_Allocation.Find(id);
            if (resource_Allocation == null)
            {
                return Json("Resource allocation does not exist!");
            }
            return Json(resource_Allocation);
        }

        

        // POST: Resource_Allocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "allocId,resIdFK,skillIdFK")] Resource_Allocation resource_Allocation)
        {
            if (ModelState.IsValid)
            {
                db.Resource_Allocation.Add(resource_Allocation);
                db.SaveChanges();
                return Json("Allocation created successfully!");
            }
            return Json("Unable to create allocation!");
        }


        // POST: Resource_Allocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "allocId,resIdFK,skillIdFK")] Resource_Allocation resource_Allocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resource_Allocation).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Edited allocation of resource is successful!");
            }
            return Json("Unable to edit allocation of resource");
        }


        // POST: Resource_Allocation/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Resource_Allocation resource_Allocation = db.Resource_Allocation.Find(id);
            db.Resource_Allocation.Remove(resource_Allocation);
            db.SaveChanges();
            return Json("Deleted allocation successful!");
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
