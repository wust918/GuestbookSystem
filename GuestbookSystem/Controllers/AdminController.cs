using GuestbookSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuestbookSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        GBSDBContext db = new GBSDBContext();
        public ActionResult Index()
        {
            var gb = db.Guestbooks.Where(g => g.isPass == true).OrderByDescending(g => g.CreatedOn).ToList();
            return View(gb);
        }
        public ActionResult DeleteWords()
        {
            var gb = db.Guestbooks.Where(g=>g.isPass==true).OrderByDescending(g => g.CreatedOn).ToList();
            return View(gb);
        }
        public ActionResult CheckIndex()
        {
            var gb = db.Guestbooks.Where(g => g.isPass == false).OrderByDescending(g => g.CreatedOn).ToList();
            return View(gb);
        }
        public ActionResult CheckMessage(int id)
        {
            var gb = db.Guestbooks.Find(id);
            return View(gb);
        }
        [HttpPost,ActionName("CheckMessage")]
        public ActionResult CheckMessage1(int id)
        {
            var gb = db.Guestbooks.Find(id);
            gb.isPass = true;
            db.SaveChanges();
            return RedirectToAction("CheckIndex");
        }
        public ActionResult Delete(int id)
        {
            var gb = db.Guestbooks.Find(id);
            return View(gb);
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var gb = db.Guestbooks.Find(id);
            db.Guestbooks.Remove(gb);
            db.SaveChanges();
            return RedirectToAction("DeleteWords");
        }

        public ActionResult CommentSummary()
        {
            var gb = from u in db.Guestbooks
                     where u.isPass == true
                     orderby u.CreatedOn descending
                     select u;
            int count = gb.Count();

            var gb2 = from u in db.Guestbooks
                      where u.isPass == false
                      orderby u.CreatedOn descending
                      select u;
            int count2 = gb2.Count();

            ViewBag.count = count;
            ViewBag.count2 = count2;
            return View();

        }

        public ActionResult UserManage()
        {
            var user = from u in db.Users
                       select u;

            return View("UserManage", user.ToList());
        }


        public ActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("UserManage");
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}