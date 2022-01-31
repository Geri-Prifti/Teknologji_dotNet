using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
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
        [Authorize(Users = "admirimkorici05@gmail.com")]
        public ActionResult Create()
        {
            ViewBag.TipiID = new SelectList(db.Tipi, "ID", "Emri");
            return View();
        }

        // POST: Makinats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "admirimkorici05@gmail.com")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Makinat makinat, HttpPostedFileBase IMG)
        {
            if (ModelState.IsValid)
            {
                WebImage img = new WebImage(IMG.InputStream);
                img.Save(Konstante.PathImgMakinat + IMG.FileName);
                db.Makinat.Add(new Makinat { Modeli = makinat.Modeli, Pershkrimi = makinat.Pershkrimi, Vit_Prodhimi = makinat.Vit_Prodhimi, Kosto1Dite = makinat.Kosto1Dite, IMG = IMG.FileName, TipiID = makinat.TipiID, ERezervuar = makinat.ERezervuar});
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipiID = new SelectList(db.Tipi, "ID", "Emri", makinat.TipiID);
            return View(makinat);
        }

        // GET: Makinats/Edit/
        [Authorize(Users = "admirimkorici05@gmail.com")]
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
        [Authorize(Users = "admirimkorici05@gmail.com")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Makinat makinat, HttpPostedFileBase IMG)
        {
            if (ModelState.IsValid)
            {
                WebImage img = new WebImage(IMG.InputStream);
                img.Save(Konstante.PathImgMakinat + IMG.FileName);
                db.Entry(new Makinat { ID = makinat.ID, Modeli = makinat.Modeli, Pershkrimi = makinat.Pershkrimi, Vit_Prodhimi = makinat.Vit_Prodhimi, Kosto1Dite = makinat.Kosto1Dite, IMG = IMG.FileName, TipiID = makinat.TipiID, ERezervuar = makinat.ERezervuar }).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipiID = new SelectList(db.Tipi, "ID", "Emri", makinat.TipiID);
            return View(makinat);
        }
        //Delete Cars
        [Authorize(Users = "admirimkorici05@gmail.com")]
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
