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
using PagedList;

namespace PojisteniWebApp.Controllers
{
    public class PojistenciController : Controller
    {
        private PojisteniWebContext db = new PojisteniWebContext();

        // GET: Pojistenci
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var pojistenci = from s in db.Pojistenci
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                pojistenci = pojistenci.Where(s => s.Prijmeni.Contains(searchString)
                                       || s.Jmeno.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    pojistenci = pojistenci.OrderByDescending(s => s.Prijmeni);
                    break;
                case "Date":
                    pojistenci = pojistenci.OrderBy(s => s.DatumNarozeni);
                    break;
                case "date_desc":
                    pojistenci = pojistenci.OrderByDescending(s => s.DatumNarozeni);
                    break;
                default:
                    pojistenci = pojistenci.OrderBy(s => s.Prijmeni);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(pojistenci.ToPagedList(pageNumber, pageSize));
        }

        // GET: Pojistenci/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pojistenec pojistenec = db.Pojistenci.Find(id);
            if (pojistenec == null)
            {
                return HttpNotFound();
            }
            return View(pojistenec);
        }

        // GET: Pojistenci/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pojistenci/Create
        // Chcete-li zajistit ochranu před útoky typu OVERPOST, povolte konkrétní vlastnosti, k nimž 
        // chcete vytvořit vazbu. Další informace viz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Jmeno,Prijmeni,Email,Heslo,DatumNarozeni,TelCislo,Ulice,Mesto,Psc")] Pojistenec pojistenec)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Pojistenci.Add(pojistenec);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Nepodařilo se uložit změny. Zkuste to znovu nebo kontaktujte administrátora.");
            }

            return View(pojistenec);
        }

        // GET: Pojistenci/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pojistenec pojistenec = db.Pojistenci.Find(id);
            if (pojistenec == null)
            {
                return HttpNotFound();
            }
            return View(pojistenec);
        }

        // POST: Pojistenci/Edit/5
        // Chcete-li zajistit ochranu před útoky typu OVERPOST, povolte konkrétní vlastnosti, k nimž 
        // chcete vytvořit vazbu. Další informace viz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Jmeno,Prijmeni,Email,Heslo,DatumNarozeni,TelCislo,Ulice,Mesto,Psc")] Pojistenec pojistenec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pojistenec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pojistenec);
        }

        // GET: Pojistenci/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pojistenec pojistenec = db.Pojistenci.Find(id);
            if (pojistenec == null)
            {
                return HttpNotFound();
            }
            return View(pojistenec);
        }

        // POST: Pojistenci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pojistenec pojistenec = db.Pojistenci.Find(id);
            db.Pojistenci.Remove(pojistenec);
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
