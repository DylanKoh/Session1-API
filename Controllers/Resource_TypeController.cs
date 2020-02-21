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
    public class Resource_TypeController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public Resource_TypeController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: Resource_Type
        [HttpPost]
        public ActionResult Index()
        {
            return new JsonResult { Data = db.Resource_Type.ToList() };
        }

        // GET: Resource_Type/Details/5
        [HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource_Type resource_Type = db.Resource_Type.Find(id);
            if (resource_Type == null)
            {
                return Json("Resource type does not exist!");
            }
            return Json(resource_Type);
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
