using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekt_Teknologji_dotNet.Models;

namespace Projekt_Teknologji_dotNet.Controllers
{
    public class RezervimetController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rezervimet
        [Authorize]
        public ActionResult Index()
        {
            var user = System.Web.HttpContext.Current.User.Identity.Name;
            var rezervimet = db.Rezervimet.Include(r => r.Klient).Include(r => r.Makinat).Where(r => r.Klient.Username == user);
                
            return View(rezervimet.ToList());
        }
        [Authorize(Users = "admirimkorici05@gmail.com")]
        public ActionResult AllReservation()
        {
            var rezervimet = db.Rezervimet.Include(r => r.Klient).Include(r => r.Makinat);
            return View(rezervimet.ToList());
        }

        // GET: Rezervimet/Create
        [Authorize]
        public ActionResult Create(int? makineID)
        {
            var makine = db.Makinat.Where(m => m.ID == makineID).ToList();
            foreach (var item in makine)
            {
                if (item.ERezervuar == true)
                {
                    ViewBag.msg = "Error";
                }
                ViewBag.Emakine = item.Modeli;
            }
            ViewBag.KlientID = new SelectList(db.Klient, "ID", "Username");
            ViewBag.MakinatID = new SelectList(db.Makinat, "ID", "Modeli");
            return View();
        }

        // POST: Rezervimet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rezervimet rezervimet, int? makineId)
        {
            if (ModelState.IsValid)
            {
                var user = System.Web.HttpContext.Current.User.Identity.Name;
                var klient = db.Klient.Where(m => m.Username == user).SingleOrDefault();
                var makine = db.Makinat.Where(m => m.ID == makineId).SingleOrDefault();
                decimal pagesDite = makine.Kosto1Dite;
                var klientId = klient.ID;
                //scripti per llogaritjen e pageses totale per makinen
                DateTime dt1 = rezervimet.Date_Rezervimi;
                DateTime dt2 = rezervimet.Date_kthimi;
                TimeSpan span = dt2.Subtract(dt1);
                var result = span.Days;
                decimal result2 = result * pagesDite;

                db.Rezervimet.Add(new Rezervimet { Date_Rezervimi = rezervimet.Date_Rezervimi, Date_kthimi = rezervimet.Date_kthimi, Pagesa_totale = result2, KlientID = klientId, MakinatID = makineId });

                var result1 = db.Makinat.Where(m => m.ID == makineId).SingleOrDefault();
                result1.ERezervuar = true;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            ViewBag.KlientID = new SelectList(db.Klient, "ID", "Username", rezervimet.KlientID);
            ViewBag.MakinatID = new SelectList(db.Makinat, "ID", "Modeli", rezervimet.MakinatID);
            return View(rezervimet);
        }


        // GET: Rezervimet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervimet rezervimet = db.Rezervimet.Find(id);
            if (rezervimet == null)
            {
                return HttpNotFound();
            }
            else
            {
                var rezervim = db.Rezervimet.Where(m => m.ID == id).SingleOrDefault();
                var makineid = rezervim.MakinatID;
                var result = db.Makinat.Where(m => m.ID == makineid).SingleOrDefault();
                result.ERezervuar = false;
                db.SaveChanges();
            }
            
            return View(rezervimet);
        }

        // POST: Rezervimet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rezervimet rezervimet = db.Rezervimet.Find(id);
            db.Rezervimet.Remove(rezervimet);

            /*var rezervim = db.Rezervimet.Where(m => m.ID == id).SingleOrDefault();
            var makineid = rezervim.MakinatID;
            var result = db.Makinat.Where(m => m.ID == makineid).SingleOrDefault();
            result.ERezervuar = false;*/
            db.SaveChanges();
            return RedirectToAction("AllReservation");
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
