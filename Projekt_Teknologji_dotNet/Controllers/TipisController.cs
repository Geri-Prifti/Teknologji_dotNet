using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Web.Helpers;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekt_Teknologji_dotNet.Models;

namespace Projekt_Teknologji_dotNet.Controllers
{
    public class TipisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tipis
        [Authorize(Users = "admirimkorici05@gmail.com")]
        public ActionResult Index()
        {
            return View(db.Tipi.ToList());
        }


        // GET: Tipis/Create
        [Authorize(Users = "admirimkorici05@gmail.com")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "admirimkorici05@gmail.com")]
        public ActionResult Create (Tipi tipi, HttpPostedFileBase Ikona)
        {
            if (ModelState.IsValid)
            {
                WebImage img = new WebImage(Ikona.InputStream);
                img.Save(Konstante.PathImg + Ikona.FileName);
                db.Tipi.Add(new Tipi { Emri = tipi.Emri, Ikona = Ikona.FileName});
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipi);
        }

        // GET: Tipis/Edit/5
        [Authorize(Users = "admirimkorici05@gmail.com")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipi tipi = db.Tipi.Find(id);
            if (tipi == null)
            {
                return HttpNotFound();
            }
            return View(tipi);
        }

        // POST: Tipis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "admirimkorici05@gmail.com")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tipi tipi, HttpPostedFileBase Ikona)
        {
            if (ModelState.IsValid)
            {
                WebImage img = new WebImage(Ikona.InputStream);
                img.Save(Konstante.PathImg + Ikona.FileName);
                db.Entry(new Tipi {ID = tipi.ID, Emri = tipi.Emri, Ikona = Ikona.FileName }).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipi);
        }

        // GET: Tipis/Delete/5
        [Authorize(Users = "admirimkorici05@gmail.com")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipi tipi = db.Tipi.Find(id);
            if (tipi == null)
            {
                return HttpNotFound();
            }
            return View(tipi);
        }

        // POST: Tipis/Delete/5
        [Authorize(Users = "admirimkorici05@gmail.com")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipi tipi = db.Tipi.Find(id);
            db.Tipi.Remove(tipi);
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
