using Perchero.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Perchero.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (!User.IsInRole("Diseñador"))
            {
                return RedirectToAction("Galeria", "Prendas");
            }
            var userid = User.Identity.GetUserId();
            List<PedidosMes> pedidosLista = new List<PedidosMes>();
            var listaPedidos = db.Pedidoes.Where(x => x.Prenda.UserId == userid).GroupBy(x => x.FechaPedido.Month).Select(s => new { mes = s.FirstOrDefault().FechaPedido.Month, cantidad = s.Count() }).ToList();
            foreach (var item in listaPedidos)
            {
                pedidosLista.Add(new PedidosMes()
                {
                    Mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.mes),
                    Cantidad = item.cantidad,
                });
            }
            var pie = new PedidosMesViewModel()
            {
                Datos = pedidosLista
            };
            ViewBag.Datos = pie.Datos;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}