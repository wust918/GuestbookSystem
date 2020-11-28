using GuestbookSystem.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GuestbookSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        GBSDBContext db = new GBSDBContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var dbUser = db.Users.Where(a => a.UserName == user.UserName && a.Password == user.Password).FirstOrDefault();
                if (dbUser != null)
                {
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1, dbUser.UserId.ToString(), DateTime.Now, DateTime.Now.AddMinutes(20), false, dbUser.SRole.ToString());

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    authCookie.HttpOnly = true;

                    System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);

                    if (dbUser.SRole.ToString() == "管理员")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (dbUser.SRole.ToString() == "普通用户")
                    {
                        Session["UserId"] = dbUser.UserId;
                        return RedirectToAction("AllWords", "User");
                    }
                }
            }
            ModelState.AddModelError("", "用户名或密码错误");
            return View(user);
        }


      

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}