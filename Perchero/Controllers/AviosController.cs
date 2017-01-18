using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Perchero.Models;

namespace Perchero.Controllers
{
    public class AviosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Avios
        public ActionResult Index()
        {
            return View(db.Avios.ToList());
        }

        // GET: Avios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avio avio = db.Avios.Find(id);
            if (avio == null)
            {
                return HttpNotFound();
            }
            return View(avio);
        }

        // GET: Avios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Avios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Precio")] Avio avio)
        {
            if (ModelState.IsValid)
            {
                db.Avios.Add(avio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(avio);
        }

        // GET: Avios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avio avio = db.Avios.Find(id);
            if (avio == null)
            {
                return HttpNotFound();
            }
            return View(avio);
        }

        // POST: Avios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Precio")] Avio avio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(avio);
        }

        // GET: Avios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avio avio = db.Avios.Find(id);
            if (avio == null)
            {
                return HttpNotFound();
            }
            return View(avio);
        }

        // POST: Avios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Avio avio = db.Avios.Find(id);
            db.Avios.Remove(avio);
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
