using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PojisteniWebApp.DAL;
using PojisteniWebApp.Models;

namespace PojisteniWebApp.Controllers
{
    public class PojistkyController : Controller
    {
        private PojisteniWebContext db = new PojisteniWebContext();

        // GET: Pojistky
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var pojistky = from s in db.Pojistky
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                pojistky = pojistky.Where(s => s.Pojistenec.Prijmeni.Contains(searchString)
                                       || s.Pojistenec.Jmeno.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    pojistky = pojistky.OrderByDescending(s => s.Pojistenec.Prijmeni);
                    break;
                case "Date":
                    pojistky = pojistky.OrderBy(s => s.PlatnostDo);
                    break;
                case "date_desc":
                    pojistky = pojistky.OrderByDescending(s => s.PlatnostDo);
                    break;
                default:
                    pojistky = pojistky.OrderBy(s => s.Pojistenec.Prijmeni);
                    break;
            }
                    
            return View(pojistky.ToList());
        }

        // GET: Pojistky/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pojistka pojistka = db.Pojistky.Find(id);
            if (pojistka == null)
            {
                return HttpNotFound();
            }
            return View(pojistka);
        }

        // GET: Pojistky/Create
        public ActionResult Create()
        {
            ViewBag.PojistenecId = new SelectList(db.Pojistenci, "Id", "Prijmeni");
            return View();
        }

        // POST: Pojistky/Create
        // Chcete-li zajistit ochranu před útoky typu OVERPOST, povolte konkrétní vlastnosti, k nimž 
        // chcete vytvořit vazbu. Další informace viz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PredmetPojisteni,PojistenecId,TypPojisteni,Castka,PlatnostOd,PlatnostDo")] Pojistka pojistka)
        {
            if (ModelState.IsValid)
            {
                db.Pojistky.Add(pojistka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PojistenecId = new SelectList(db.Pojistenci, "Id", "Prijmeni", pojistka.PojistenecId);
            return View(pojistka);
        }

        // GET: Pojistky/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pojistka pojistka = db.Pojistky.Find(id);
            if (pojistka == null)
            {
                return HttpNotFound();
            }
            ViewBag.PojistenecId = new SelectList(db.Pojistenci, "Id", "Prijmeni", pojistka.PojistenecId);
            return View(pojistka);
        }

        // POST: Pojistky/Edit/5
        // Chcete-li zajistit ochranu před útoky typu OVERPOST, povolte konkrétní vlastnosti, k nimž 
        // chcete vytvořit vazbu. Další informace viz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PredmetPojisteni,PojistenecId,TypPojisteni,Castka,PlatnostOd,PlatnostDo")] Pojistka pojistka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pojistka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PojistenecId = new SelectList(db.Pojistenci, "Id", "Prijmeni", pojistka.PojistenecId);
            return View(pojistka);
        }

        // GET: Pojistky/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pojistka pojistka = db.Pojistky.Find(id);
            if (pojistka == null)
            {
                return HttpNotFound();
            }
            return View(pojistka);
        }

        // POST: Pojistky/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pojistka pojistka = db.Pojistky.Find(id);
            db.Pojistky.Remove(pojistka);
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
