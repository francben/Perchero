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
    public class PedidoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //get
        public ActionResult Pedido(int PrendaId)
        {
            var userid = User.Identity.GetUserId();
            Pedido pedido = new Pedido();
            Prenda p = db.Prendas.Find(PrendaId);
            pedido.Prenda = p;
            pedido.PrendaId = p.Id;
            pedido.PrendaId = PrendaId;
            pedido.UserId = userid;
            string fechaP = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime fPedido = DateTime.ParseExact(fechaP, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string fechaE = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime fEntrega = DateTime.ParseExact(fechaE, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            pedido.FechaPedido = fPedido;
            pedido.FechaEntrega = fEntrega;
            pedido.Seña = Math.Round(p.PrecioTotal * 0.20, 0);
            pedido.Saldo = p.PrecioTotal - pedido.Seña;
            return View(pedido);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pedido(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                Pedido p = new Pedido();
                string fechaP = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime fPedido = DateTime.ParseExact(fechaP, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                p.FechaPedido = fPedido;
                db.Pedidoes.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pedido);
        }

        // GET: Pedidoes
        public ActionResult Index()
        {
            var pedidoes = db.Pedidoes.Include(p => p.Prenda).Include(p => p.Usuario);
            if (User.IsInRole("Administrador"))
            {
                return View(pedidoes.ToList());
            }
            else
            {
                var userid = User.Identity.GetUserId();
                return View(pedidoes.Where(x => x.UserId == userid).ToList());
            }

        }

        public JsonResult getBarPedidos()
        {
            var query = db.Pedidoes.GroupBy(x => x.FechaPedido.Month).Select(s => new { mes = s.FirstOrDefault().FechaPedido.Month, cantidad = s.Count() } ).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        // GET: Pedidoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidoes/Create
        public ActionResult Create()
        {
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre");
            return View();
        }

        // POST: Pedidoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PrendaId,UserId,FechaPedido,FechaEntrega,Seña,Saldo")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Pedidoes.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId", pedido.PrendaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", pedido.UserId);
            return View(pedido);
        }

        [Authorize(Roles = "Diseñador")]
        public ActionResult Entrantes()
        {
            var UserId = User.Identity.GetUserId();
            var pedidoes = db.Pedidoes.Include(p => p.Usuario).Include(p => p.Prenda);
            return View(pedidoes.Where(x => x.Prenda.UserId == UserId).Where(x => x.Estado == false).ToList());
        }

        [Authorize(Roles = "Diseñador")]
        public ActionResult Terminados()
        {
            var UserId = User.Identity.GetUserId();
            var pedidoes = db.Pedidoes.Include(p => p.Usuario).Include(p => p.Prenda);
            return View(pedidoes.Where(x => x.Prenda.UserId == UserId).Where(x => x.Estado == true).ToList());
        }

        public ActionResult NotificacionEntrantes()
        {
            var UserId = User.Identity.GetUserId();
            List<Pedido> lista = db.Pedidoes.Where(x => x.Estado == false).Where(x => x.Prenda.UserId == UserId).ToList();
            if (lista != null  || lista.Count > 0)
            {
                return View(lista);
            }
            return View();
        }

        // GET: Pedidoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId", pedido.PrendaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", pedido.UserId);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PrendaId,UserId,FechaPedido,FechaEntrega,Seña,Saldo")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId", pedido.PrendaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", pedido.UserId);
            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
        public ActionResult Pago(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            pedido.Seña = pedido.Saldo;

            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId", pedido.PrendaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", pedido.UserId);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pago([Bind(Include = "Id,PrendaId,UserId,FechaPedido,FechaEntrega,Seña,Saldo")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                if (pedido.Saldo == 0.0)
                {
                    pedido.Estado = true;
                }
                else
                {
                    pedido.Estado = false;
                }
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                if (pedido.Estado)
                {
                    return RedirectToAction("Terminados");
                }
                return RedirectToAction("Entrantes");
            }
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId", pedido.PrendaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", pedido.UserId);
            return View(pedido);
            
        }


        // GET: Pedidoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidoes.Find(id);
            db.Pedidoes.Remove(pedido);
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
