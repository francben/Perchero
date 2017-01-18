using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Perchero.Models
{
    public class Pedido
    {
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }
        public int PrendaId { get; set; }
        [ForeignKey("PrendaId")]
        public virtual Prenda Prenda { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Usuario { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string FechaPedido { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string FechaEntrega { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Seña { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Saldo { get; set; }
    }
}