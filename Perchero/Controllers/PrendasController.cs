using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Perchero.Models;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.IO;

namespace Perchero.Controllers
{
    public class PrendasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //get
        public ActionResult Calcular()
        {
            var UserId = User.Identity.GetUserId();
            Prenda prenda = new Prenda();
            prenda.UserId = UserId;

            ViewBag.TipoId = new SelectList(db.Tipoes, "Id", "Categoria");
            ViewBag.AvioId = new SelectList(db.Avios, "Id", "Nombre");
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId");
            ViewBag.TelaId = new SelectList(db.Telas, "Id", "Nombre");
            return View(prenda);
        }

        //post
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public int Calcular(Prenda prenda, int[] TelaId = null, int[] Metros = null, int[] Cantidad = null, int[] AvioId = null)
        //{
        //    Tipo tipoPrenda = db.Tipoes.Where(x => x.Id == prenda.TipoId).FirstOrDefault();
        //    var manoobra = tipoPrenda.Precio;
        //    var preciotela = 0;
        //    var precioavio = 0;
        //    var precioprenda = 0;
        //    foreach (var item in prenda.DetallePrendas)
        //    {
        //        Tela tela = db.Telas.Find(item.TelaId);
        //        Avio avio = db.Avios.Find(item.AvioId);
        //        preciotela += tela.Precio * (int)item.MetroTela;
        //        precioavio += avio.Precio * item.CantidadAvio;
        //    }
        //    precioprenda = manoobra + preciotela + precioavio;
        //    return precioprenda;
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calcular(Prenda prenda)
        {
            Tipo tipoPrenda = db.Tipoes.Where(x => x.Id == prenda.TipoId).FirstOrDefault();
            var manoobra = tipoPrenda.Precio;
            var preciotela = 0;
            var precioavio = 0;
            var precioprenda = 0;
            foreach (var item in prenda.DetallePrendas)
            {
                Tela tela = db.Telas.Find(item.TelaId);
                Avio avio = db.Avios.Find(item.AvioId);
                preciotela += tela.Precio * (int)item.MetroTela;
                precioavio += avio.Precio * item.CantidadAvio;
                item.Avio = avio;
                item.Tela = tela;
            }
            precioprenda = manoobra + preciotela + precioavio;
            prenda.PrecioTotal = precioprenda;

            ViewBag.TipoId = new SelectList(db.Tipoes, "Id", "Categoria");
            ViewBag.AvioId = new SelectList(db.Avios, "Id", "Nombre");
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId");
            ViewBag.TelaId = new SelectList(db.Telas, "Id", "Nombre");
            //return precioprenda;
            return View(prenda);
        }


        public ActionResult Galeria()
        {
            var prendas = db.Prendas.Include(p => p.Tipo).Include(p => p.Usuario);
            return View(prendas.Where(x => x.Vitrina == true).ToList());
        }
        // GET: Prendas
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();
            var prendas = db.Prendas.Include(p => p.Tipo).Include(p => p.Usuario);
            return View(prendas.Where(x => x.UserId == UserId).OrderByDescending(x => x.Id).ToList());
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
            Prenda prenda = new Prenda();
            var UserId = "";
            if (User.IsInRole("Diseñador"))
            {
                UserId = User.Identity.GetUserId();
                prenda.UserId = UserId;
            }

            var rol = db.Roles.Where(x => x.Name == "Diseñador").FirstOrDefault();
            var usuarios = db.Users.Where(u => u.Roles.Any(y => y.RoleId == rol.Id));

            ViewBag.TipoId = new SelectList(db.Tipoes, "Id", "Categoria");
            ViewBag.AvioId = new SelectList(db.Avios, "Id", "Nombre");
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId");
            ViewBag.TelaId = new SelectList(db.Telas, "Id", "Nombre");
            ViewBag.UserId = new SelectList(usuarios, "Id", "Nombre", prenda.UserId);
            return View(prenda);
                        
        }

        // POST: Prendas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Prenda prenda, HttpPostedFileBase Imagen)
        {
            prenda.Imagen = ProximoId().ToString();
            if (ModelState.IsValid)
            {
                db.Prendas.Add(prenda);
                db.SaveChanges();
                if (Imagen != null)
                {
                    var nombrearchivo = CrearImagen(Imagen, prenda);
                    CrearMiniatura(Imagen, prenda);
                }
                return RedirectToAction("Index");
            }

            ViewBag.TipoId = new SelectList(db.Tipoes, "Id", "Categoria", prenda.TipoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", prenda.UserId);

            ViewBag.AvioId = new SelectList(db.Avios, "Id", "Nombre");
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId");
            ViewBag.TelaId = new SelectList(db.Telas, "Id", "Nombre");
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
            ViewBag.TipoId = new SelectList(db.Tipoes, "Id", "Categoria", prenda.TipoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", prenda.UserId);

            ViewBag.AvioId = new SelectList(db.Avios, "Id", "Nombre");
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId");
            ViewBag.TelaId = new SelectList(db.Telas, "Id", "Nombre");
            return View(prenda);
        }



        // POST: Prendas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Prenda prenda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prenda).State = EntityState.Modified;
                db.SaveChanges();
                using (var contexto = new ApplicationDbContext())
                {
                    foreach (var item in prenda.DetallePrendas)
                    {
                        var detalles = new DetallePrenda()
                        {
                            Id = item.Id,
                            AvioId = item.AvioId,
                            CantidadAvio = item.CantidadAvio,
                            MetroTela = item.MetroTela,
                            PrendaId = item.PrendaId,
                            TelaId = item.TelaId
                        };
                        contexto.DetallePrendas.Add(detalles);
                        contexto.Entry(detalles).State = EntityState.Modified;
                        contexto.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.TipoId = new SelectList(db.Tipoes, "Id", "Categoria", prenda.TipoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", prenda.UserId);

            ViewBag.AvioId = new SelectList(db.Avios, "Id", "Nombre");
            ViewBag.PrendaId = new SelectList(db.Prendas, "Id", "UserId");
            ViewBag.TelaId = new SelectList(db.Telas, "Id", "Nombre");
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

        public int ProximoId()
        {
            //var maxId = db.Prendas.DefaultIfEmpty().Max(e => e == null ? 0 : e.Id);
            var maxId = db.Prendas.Select(p => new { id = p.Id }).OrderByDescending(g => g.id).FirstOrDefault();
            if (maxId != null)
            {
                return maxId.id + 1;
            }
            else
            {
                return 1;
            }
        }

        // agregar imagenes
        #region public void CreateImagen(HttpPostedFileBase Imagen)
        public string CrearImagen(HttpPostedFileBase Imagen, Prenda prenda)
        {
            var fileName = Path.GetFileName(prenda.Id.ToString());
            var path = Server.MapPath("~/Content/imgprenda/imgori/" + prenda.Id);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string outputPath = Path.Combine(path, prenda.Id + ".png");
            Imagen.SaveAs(outputPath);
            return fileName;
        }

        #endregion
        #region public void CreateThumbnail(HttpPostedFileBase Imagen, ApplicationUser user)
        public void CrearMiniatura(HttpPostedFileBase Imagen, Prenda prenda)
        {
            using (var image = Image.FromStream(Imagen.InputStream, true, true))
            {
                using (var thumb = image.GetThumbnailImage(200, /* width*/ 200, /* height*/ () => false, IntPtr.Zero))
                {
                    var jpgInfo = ImageCodecInfo.GetImageEncoders().Where(codecInfo => codecInfo.MimeType == "image/png").First(); /* Returns array of image encoder objects built into GDI+ */
                    using (var encParams = new EncoderParameters(1))
                    {
                        var appDataThumbnailPath = Server.MapPath("~/Content/imgprenda/imgprev/" + prenda.Id);
                        if (!Directory.Exists(appDataThumbnailPath))
                        {
                            Directory.CreateDirectory(appDataThumbnailPath);
                        }
                        string outputPath = Path.Combine(appDataThumbnailPath, prenda.Id + ".png");
                        long quality = 8;
                        encParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                        thumb.Save(outputPath, jpgInfo, encParams);
                    }
                }
            }
        }
        #endregion
        [HttpPost]
        public ContentResult UploadFiles()
        {
            var r = new List<UploadFilesResult>();

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                string savedFileName = Path.Combine(Server.MapPath("~/App_Data"), Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);

                r.Add(new UploadFilesResult()
                {
                    Name = hpf.FileName,
                    Length = hpf.ContentLength,
                    Type = hpf.ContentType
                });
            }
            return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
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
