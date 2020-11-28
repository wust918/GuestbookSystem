using GuestbookSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuestbookSystem.Controllers
{
    public class HomeController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        public ActionResult Index()
        {
            var gb = db.Guestbooks.Where(g => g.isPass == true).OrderByDescending(g => g.CreatedOn).ToList();
            return View(gb);
        }
    }
}