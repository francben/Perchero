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
            pedido.FechaPedido = DateTime.Now;
            pedido.FechaEntrega = DateTime.Now;
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
                p.FechaPedido = DateTime.Now;
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
            return View(pedidoes.ToList());
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
