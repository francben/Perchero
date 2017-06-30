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
    public class TallesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public JsonResult getTalles()
        {
            var query = from c in db.Talles select new { c.Id, c.NumeroTalle, c.AlturaAxila, c.AlturaBusto, c.AlturaCCadera, c.AlturaRodilla, c.AnchoEspalda, c.AnchoTorax, c.Busto, c.Cadera, c.Cintura, c.ContornoBrazo, c.Cuello, c.Hombro, c.LargoCinturaSuelo, c.LargoManga, c.Pinza, c.PunhoAjustado, c.PunhoFlojo, c.SeparacionBusto, c.TalleDelantero, c.TalleEspalda, c.TallePrendas, c.TiroPaantalonDelantero};
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        // GET: Talles
        public ActionResult Index()
        {
            return View(db.Talles.ToList());
        }

        // GET: Talles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talle talle = db.Talles.Find(id);
            if (talle == null)
            {
                return HttpNotFound();
            }
            return View(talle);
        }

        // GET: Talles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Talles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NumeroTalle,Busto,Cintura,Cadera,AnchoEspalda,AnchoTorax,Hombro,Cuello,AlturaBusto,Pinza,SeparacionBusto,ContornoBrazo,PunhAjustado,PunhoFlojo,TalleEspalda,TalleDelantero,AlturaAxila,AlturaRodilla,AlturaCCadera,LargoCinturaSuelo,TiroPaantalonDelantero,LargoManga")] Talle talle)
        {
            if (ModelState.IsValid)
            {
                db.Talles.Add(talle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(talle);
        }

        // GET: Talles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talle talle = db.Talles.Find(id);
            if (talle == null)
            {
                return HttpNotFound();
            }
            return View(talle);
        }

        // POST: Talles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NumeroTalle,Busto,Cintura,Cadera,AnchoEspalda,AnchoTorax,Hombro,Cuello,AlturaBusto,Pinza,SeparacionBusto,ContornoBrazo,PunhoAjustado,PunhoFlojo,TalleEspalda,TalleDelantero,AlturaAxila,AlturaRodilla,AlturaCCadera,LargoCinturaSuelo,TiroPaantalonDelantero,LargoManga")] Talle talle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(talle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(talle);
        }

        // GET: Talles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talle talle = db.Talles.Find(id);
            if (talle == null)
            {
                return HttpNotFound();
            }
            return View(talle);
        }

        // POST: Talles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Talle talle = db.Talles.Find(id);
            db.Talles.Remove(talle);
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
