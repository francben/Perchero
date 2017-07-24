using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Perchero.Models;
using Microsoft.AspNet.Identity;

namespace Perchero.Controllers
{
    public class CajasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cajas
        public ActionResult Index()
        {
            var cajas = db.Cajas.Include(c => c.Usuario);
            return View(cajas.ToList());
        }

        // GET: Cajas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caja caja = db.Cajas.Find(id);
            if (caja == null)
            {
                return HttpNotFound();
            }
            return View(caja);
        }

        // GET: Cajas/Create
        public ActionResult Create()
        {
            var userid = User.Identity.GetUserId();
            Caja caja = new Caja();
            string fechaP = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime fPedido = DateTime.ParseExact(fechaP, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            caja.Fecha_Apertura = fPedido;
            caja.UserId = userid;
            caja.Cierre = 0;
            caja.Entrada = 0;
            caja.Estado = false;
            caja.Operaciones = 0;
            caja.Salida = 0;
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre");
            return View(caja);
        }

        // POST: Cajas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha_Apertura,Inicial,Fecha_Cierre,Entrada,Salida,Cierre,Operaciones,UserId,Estado")] Caja caja)
        {
            if (ModelState.IsValid)
            {
                caja.Entrada = caja.Inicial;
                caja.Cierre = caja.Inicial;
                string fechaP = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime fInicial = DateTime.ParseExact(fechaP, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                caja.Fecha_Apertura = fInicial;
                MovimientoCaja movCaja = new MovimientoCaja();
                movCaja.CajaId = caja.Id;
                movCaja.Concepto = "Apertura de caja";
                movCaja.Egreso = 0;
                movCaja.Fecha = fInicial;
                movCaja.Ingreso = caja.Inicial;
                movCaja.Movimiento = "Entrada";
                movCaja.Saldo = caja.Inicial;
                List<MovimientoCaja> movimientos = new List<MovimientoCaja>();
                movimientos.Add(movCaja);
                caja.detallesCaja = movimientos;
                caja.Estado = false;
                caja.Operaciones = 1;

                db.Cajas.Add(caja);
                db.SaveChanges();
                return RedirectToAction("Details", "Cajas", new {id = caja.Id});
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", caja.UserId);
            return View(caja);
        }

        // GET: Cajas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caja caja = db.Cajas.Find(id);
            if (caja == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", caja.UserId);
            return View(caja);
        }

        // POST: Cajas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha_Apertura,Inicial,Fecha_Cierre,Entrada,Salida,Cierre,Operaciones,UserId,Estado")] Caja caja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", caja.UserId);
            return View(caja);
        }

        // GET: Cajas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caja caja = db.Cajas.Find(id);
            if (caja == null)
            {
                return HttpNotFound();
            }
            return View(caja);
        }

        // POST: Cajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Caja caja = db.Cajas.Find(id);
            db.Cajas.Remove(caja);
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
