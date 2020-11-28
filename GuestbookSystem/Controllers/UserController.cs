using GuestbookSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuestbookSystem.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        GBSDBContext db = new GBSDBContext();
        //所有留言
        public ActionResult AllWords()
        {
            var gb = db.Guestbooks.Where(g=>g.isPass==true).OrderByDescending(g => g.CreatedOn).ToList();
            return View(gb);
        }
        //用户留言
        public ActionResult MyWords()
        {
            int UserId = (int)Session["UserId"];
            var gb = db.Guestbooks.Where(g=>g.UserId==UserId&&g.isPass==true).OrderByDescending(g => g.CreatedOn).ToList();
            return View(gb);
        }
        //用户删除自己留言
        public ActionResult Delete(int id)
        {
            var gb = db.Guestbooks.Find(id);//查找到该留言
            return View(gb);//返回记录
        }
        //删除确定
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var gb = db.Guestbooks.Find(id);
            db.Guestbooks.Remove(gb);
            db.SaveChanges();
            return RedirectToAction("DeleteWords");
        }
        //用户编辑自己留言
        public ActionResult EditWords()
        {
            return View();
        }
        public ActionResult CreateWords()
        {
            return View();
        }
        //用户创建留言
        [HttpPost]
        public ActionResult CreateWords(Guestbook gb)
        {
            if (ModelState.IsValid)
            {
                //gb.CreatedOn = System.DateTime.Now;
                gb.UserId = (int)Session["UserId"];//传入用户id
                gb.isPass = false;//是否已审核
                db.Guestbooks.Add(gb);//数据库中添加留言
                db.SaveChanges();//保存数据库
                return RedirectToAction("AllWords");//返回所有留言
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}