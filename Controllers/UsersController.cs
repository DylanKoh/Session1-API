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
    public class UsersController : Controller
    {
        private Session1Entities db = new Session1Entities();
        public UsersController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: Users
        [HttpPost]
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.User_Type);
            return new JsonResult { Data = users.ToList() };
        }

        // GET: Users/Details/5
        [HttpPost]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return Json("User does not exist!");
            }
            return Json(user);
        }


        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "userId,userName,userPw,userTypeIdFK")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Json("User created successful!");
            }
            return Json("Unable to create account!");
        }


        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "userId,userName,userPw,userTypeIdFK")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Edit User account successful!");
            }
            return Json("Unable to edit User account!");
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return Json("Account deleted!");
        }

        [HttpPost]
        public ActionResult Login(string userID, string password)
        {
            var user = db.Users.Where(x => x.userId == userID && x.userPw == password).Select(x => x).FirstOrDefault();
            if (user == null)
            {
                return Json("Invalid User!");
            }
            else
            {
                return Json(user);
            }
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
