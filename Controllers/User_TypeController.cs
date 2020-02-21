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
    public class User_TypeController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public User_TypeController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: User_Type
        public ActionResult Index()
        {
            return new JsonResult { Data = db.User_Type.ToList() };
        }

        // GET: User_Type/Details/5
        [HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Type user_Type = db.User_Type.Find(id);
            if (user_Type == null)
            {
                return Json("User type does not exist!");
            }
            return Json(user_Type);
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
