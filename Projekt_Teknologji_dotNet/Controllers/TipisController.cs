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
        public ActionResult Index()
        {
            return View(db.Tipi.ToList());
        }

        // GET: Tipis/Details/5
        public ActionResult Details(int? id)
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

        // GET: Tipis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (Tipi tipi, HttpPostedFileBase EmriImazhit)
        {
            if (ModelState.IsValid)
            {
                db.Tipi.Add(new Tipi { Emri = tipi.Emri, Ikona = tipi.Ikona});
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipi);
        }

        // GET: Tipis/Edit/5
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Emri,Ikona")] Tipi tipi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipi);
        }

        // GET: Tipis/Delete/5
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
