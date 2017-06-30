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
    public class TallePrendasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TallePrendas
        public ActionResult Index()
        {
            var tallePrendas = db.TallePrendas.Include(t => t.Prenda).Include(t => t.Talle);
            return View(tallePrendas.ToList());
        }

        // GET: TallePrendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TallePrenda tallePrenda = db.TallePrendas.Find(id);
            if (tallePrenda == null)
            {
                return HttpNotFound();
            }
            return View(tallePrenda);
        }

        // GET: TallePrendas/Create
        public ActionResult Create()
        {
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId");
            ViewBag.TalleId = new SelectList(db.Talles, "Id", "Id");
            return View();
        }

        // POST: TallePrendas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TalleId,PrendaId,NUmeroTalle,Busto,Cintura,Cadera,AnchoEspalda,AnchoTorax,Hombro,Cuello,AlturaBusto,Pinza,SeparacionBusto,ContornoBrazo,PunhoAjustado,PunhoFlojo,TalleEspalda,TalleDelantero,AlturaAxila,AlturaRodilla,AlturaCCadera,LargoCinturaSuelo,TiroPaantalonDelantero,LargoManga")] TallePrenda tallePrenda)
        {
            if (ModelState.IsValid)
            {
                db.TallePrendas.Add(tallePrenda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId", tallePrenda.PrendaId);
            ViewBag.TalleId = new SelectList(db.Talles, "Id", "Id", tallePrenda.TalleId);
            return View(tallePrenda);
        }

        // GET: TallePrendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TallePrenda tallePrenda = db.TallePrendas.Find(id);
            if (tallePrenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId", tallePrenda.PrendaId);
            ViewBag.TalleId = new SelectList(db.Talles, "Id", "Id", tallePrenda.TalleId);
            return View(tallePrenda);
        }

        // POST: TallePrendas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TalleId,PrendaId,NUmeroTalle,Busto,Cintura,Cadera,AnchoEspalda,AnchoTorax,Hombro,Cuello,AlturaBusto,Pinza,SeparacionBusto,ContornoBrazo,PunhoAjustado,PunhoFlojo,TalleEspalda,TalleDelantero,AlturaAxila,AlturaRodilla,AlturaCCadera,LargoCinturaSuelo,TiroPaantalonDelantero,LargoManga")] TallePrenda tallePrenda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tallePrenda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId", tallePrenda.PrendaId);
            ViewBag.TalleId = new SelectList(db.Talles, "Id", "Id", tallePrenda.TalleId);
            return View(tallePrenda);
        }

        // GET: TallePrendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TallePrenda tallePrenda = db.TallePrendas.Find(id);
            if (tallePrenda == null)
            {
                return HttpNotFound();
            }
            return View(tallePrenda);
        }

        // POST: TallePrendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TallePrenda tallePrenda = db.TallePrendas.Find(id);
            db.TallePrendas.Remove(tallePrenda);
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
