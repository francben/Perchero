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
        [Display(Name = "Cliente")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Usuario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime FechaPedido { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime FechaEntrega { get; set; }

        [Range(500, 9999999, ErrorMessage = " El monto no debe exeder al precio total de la prenda ")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public double Seña { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public double Saldo { get; set; }
        public bool Estado { get; set; }
        public  bool Terminado { get; set; }
        public bool Eliminado { get; set; }
    }
}