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
    public class TelasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Telas
        public ActionResult Index()
        {
            return View(db.Telas.ToList());
        }

        // GET: Telas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tela tela = db.Telas.Find(id);
            if (tela == null)
            {
                return HttpNotFound();
            }
            return View(tela);
        }

        // GET: Telas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Telas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Color,Precio")] Tela tela)
        {
            if (ModelState.IsValid)
            {
                db.Telas.Add(tela);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tela);
        }

        // GET: Telas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tela tela = db.Telas.Find(id);
            if (tela == null)
            {
                return HttpNotFound();
            }
            return View(tela);
        }

        // POST: Telas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Color,Precio")] Tela tela)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tela).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tela);
        }

        // GET: Telas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tela tela = db.Telas.Find(id);
            if (tela == null)
            {
                return HttpNotFound();
            }
            return View(tela);
        }

        // POST: Telas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tela tela = db.Telas.Find(id);
            db.Telas.Remove(tela);
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
