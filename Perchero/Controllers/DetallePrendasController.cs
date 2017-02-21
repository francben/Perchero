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
    public class DetallePrendasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DetallePrendas
        public ActionResult Index()
        {
            var detallePrendas = db.DetallePrendas.Include(d => d.Avio).Include(d => d.Prenda).Include(d => d.Tela);
            return View(detallePrendas.ToList());
        }

        // GET: DetallePrendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePrenda detallePrenda = db.DetallePrendas.Find(id);
            if (detallePrenda == null)
            {
                return HttpNotFound();
            }
            return View(detallePrenda);
        }

        // GET: DetallePrendas/Create
        public ActionResult Create(int TelaId)
        {
            DetallePrenda detalle = new DetallePrenda();
            Tela t = db.Telas.Find(TelaId);
            detalle.Tela = t;
            detalle.TelaId = t.Id;
            detalle.TelaId = TelaId;
            
            ViewBag.AvioId = new SelectList(db.Avios, "Id", "Nombre");
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId");
            ViewBag.TelaId = new SelectList(db.Telas, "Id", "Nombre");
            return View();
        }

        // POST: DetallePrendas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PrendaId,AvioId,TelaId,MetroTela,CantidadAvio")] DetallePrenda detallePrenda)
        {
            if (ModelState.IsValid)
            {
                db.DetallePrendas.Add(detallePrenda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AvioId = new SelectList(db.Avios, "Id", "Nombre", detallePrenda.AvioId);
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId", detallePrenda.PrendaId);
            ViewBag.TelaId = new SelectList(db.Telas, "Id", "Nombre", detallePrenda.TelaId);
            return View(detallePrenda);
        }

        // GET: DetallePrendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePrenda detallePrenda = db.DetallePrendas.Find(id);
            if (detallePrenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.AvioId = new SelectList(db.Avios, "Id", "Nombre", detallePrenda.AvioId);
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId", detallePrenda.PrendaId);
            ViewBag.TelaId = new SelectList(db.Telas, "Id", "Nombre", detallePrenda.TelaId);
            return View(detallePrenda);
        }

        // POST: DetallePrendas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PrendaId,AvioId,TelaId,MetroTela,CantidadAvio")] DetallePrenda detallePrenda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detallePrenda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AvioId = new SelectList(db.Avios, "Id", "Nombre", detallePrenda.AvioId);
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId", detallePrenda.PrendaId);
            ViewBag.TelaId = new SelectList(db.Telas, "Id", "Nombre", detallePrenda.TelaId);
            return View(detallePrenda);
        }

        // GET: DetallePrendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePrenda detallePrenda = db.DetallePrendas.Find(id);
            if (detallePrenda == null)
            {
                return HttpNotFound();
            }
            return View(detallePrenda);
        }

        // POST: DetallePrendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetallePrenda detallePrenda = db.DetallePrendas.Find(id);
            db.DetallePrendas.Remove(detallePrenda);
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
