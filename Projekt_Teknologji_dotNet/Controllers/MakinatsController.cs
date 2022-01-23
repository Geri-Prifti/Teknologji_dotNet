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
    public class MakinatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Makinats
        public ActionResult Index(string tipi)
        {
            var makinat = db.Makinat.Include(m => m.Tipi);
            if (!string.IsNullOrEmpty(tipi))
            {
                makinat = makinat.Where(m => m.Tipi.Emri == tipi);
                
                if(makinat.Count() == 0)
                {
                    ViewBag.msg = "Nuk u gjete asnje makine per tipin: " + tipi;
                }
            }
            ViewBag.msg2 = tipi;
            return View(makinat.ToList());
        }

        // GET: Makinats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makinat makinat = db.Makinat.Find(id);
            if (makinat == null)
            {
                return HttpNotFound();
            }
            return View(makinat);
        }

        // GET: Makinats/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.TipiID = new SelectList(db.Tipi, "ID", "Emri");
            return View();
        }

        // POST: Makinats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Modeli,Pershkrimi,Vit_Prodhimi,Kosto1Dite,IMG,TipiID,ERezervuar")] Makinat makinat)
        {
            if (ModelState.IsValid)
            {
                db.Makinat.Add(makinat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipiID = new SelectList(db.Tipi, "ID", "Emri", makinat.TipiID);
            return View(makinat);
        }

        // GET: Makinats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makinat makinat = db.Makinat.Find(id);
            if (makinat == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipiID = new SelectList(db.Tipi, "ID", "Emri", makinat.TipiID);
            return View(makinat);
        }

        // POST: Makinats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Modeli,Pershkrimi,Vit_Prodhimi,Kosto1Dite,IMG,TipiID")] Makinat makinat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(makinat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipiID = new SelectList(db.Tipi, "ID", "Emri", makinat.TipiID);
            return View(makinat);
        }

        /* GET: Makinats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makinat makinat = db.Makinat.Find(id);
            if (makinat == null)
            {
                return HttpNotFound();
            }
            return View(makinat);
        }

        // POST: Makinats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Makinat makinat = db.Makinat.Find(id);
            db.Makinat.Remove(makinat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

        [HttpPost]
        [Route("Makinats/Delete/{id}")]
        public JsonResult Delete(int? id)
        {
            Makinat makinat = db.Makinat.Find(id);
            db.Makinat.Remove(makinat);
            int result = db.SaveChanges();
            return Json(result, JsonRequestBehavior.AllowGet);
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
