﻿using System;
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
    public class PrendasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Prendas
        public ActionResult Index()
        {
            var prendas = db.Prendas.Include(p => p.Tipo).Include(p => p.Usuario);
            return View(prendas.ToList());
        }

        // GET: Prendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prenda prenda = db.Prendas.Find(id);
            if (prenda == null)
            {
                return HttpNotFound();
            }
            return View(prenda);
        }

        // GET: Prendas/Create
        public ActionResult Create()
        {
            ViewBag.TipoId = new SelectList(db.Tipoes, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre");
            return View();
        }

        // POST: Prendas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,TipoId,Nombre,Imagen,Talle,Vitrina,PrecioTotal")] Prenda prenda)
        {
            if (ModelState.IsValid)
            {
                db.Prendas.Add(prenda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoId = new SelectList(db.Tipoes, "Id", "Nombre", prenda.TipoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", prenda.UserId);
            return View(prenda);
        }

        // GET: Prendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prenda prenda = db.Prendas.Find(id);
            if (prenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoId = new SelectList(db.Tipoes, "Id", "Nombre", prenda.TipoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", prenda.UserId);
            return View(prenda);
        }

        // POST: Prendas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,TipoId,Nombre,Imagen,Talle,Vitrina,PrecioTotal")] Prenda prenda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prenda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoId = new SelectList(db.Tipoes, "Id", "Nombre", prenda.TipoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", prenda.UserId);
            return View(prenda);
        }

        // GET: Prendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prenda prenda = db.Prendas.Find(id);
            if (prenda == null)
            {
                return HttpNotFound();
            }
            return View(prenda);
        }

        // POST: Prendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prenda prenda = db.Prendas.Find(id);
            db.Prendas.Remove(prenda);
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
