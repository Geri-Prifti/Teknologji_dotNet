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
        public ActionResult Index()
        {
            var rezervimet = db.Rezervimet.Include(r => r.Klient).Include(r => r.Makinat);
            return View(rezervimet.ToList());
        }

        // GET: Rezervimet/Details/5
        public ActionResult Details(int? id)
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
            return View(rezervimet);
        }

        // GET: Rezervimet/Create
        public ActionResult Create()
        {
            ViewBag.KlientID = new SelectList(db.Klient, "ID", "Username");
            ViewBag.MakinatID = new SelectList(db.Makinat, "ID", "Modeli");
            return View();
        }

        // POST: Rezervimet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date_Rezervimi,Date_kthimi,Pagesa_totale,KlientID,MakinatID")] Rezervimet rezervimet)
        {
            if (ModelState.IsValid)
            {
                db.Rezervimet.Add(rezervimet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlientID = new SelectList(db.Klient, "ID", "Username", rezervimet.KlientID);
            ViewBag.MakinatID = new SelectList(db.Makinat, "ID", "Modeli", rezervimet.MakinatID);
            return View(rezervimet);
        }

        // GET: Rezervimet/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.KlientID = new SelectList(db.Klient, "ID", "Username", rezervimet.KlientID);
            ViewBag.MakinatID = new SelectList(db.Makinat, "ID", "Modeli", rezervimet.MakinatID);
            return View(rezervimet);
        }

        // POST: Rezervimet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date_Rezervimi,Date_kthimi,Pagesa_totale,KlientID,MakinatID")] Rezervimet rezervimet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rezervimet).State = EntityState.Modified;
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
            return View(rezervimet);
        }

        // POST: Rezervimet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rezervimet rezervimet = db.Rezervimet.Find(id);
            db.Rezervimet.Remove(rezervimet);
            db.SaveChanges();
            return RedirectToAction("Index");
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
