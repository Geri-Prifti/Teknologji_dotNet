using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Projekt_Teknologji_dotNet.Models;

namespace Projekt_Teknologji.NET.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Tipi.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Contact c)
        {
            if (ModelState.IsValid)
            {
                string subject = c.Subjekti;
                string body = "Derguesi: " + c.Emri + ", Email: " + c.Email + ", Mesazhi: " + c.Mesazhi;
                string email_rec = "admirimkorici05@gmail.com";

                WebMail.Send(email_rec, subject, body, null, null, null, true, null, null, null, null, null, null);
                ViewBag.Msg = "Mesazhi u dergua!";
            }
            ModelState.Clear();

            return View();
        }
        public ActionResult Kerko(string q)
        {
            var makinat = db.Makinat.Include(m => m.Tipi);
            if (!string.IsNullOrEmpty(q))
            {
                makinat = makinat.Where(m => m.Modeli == q); 
                if(makinat.Count() == 0)
                {
                    ViewBag.msg = "Nuk u gjete asnje rezultat";
                }
            } else
            {
                ViewBag.msg = "Nuk u gjete asnje rezultat";
            }
            ViewBag.msg2 = q;
            return View(makinat.ToList());
        }
    }
}