using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Perchero.Models
{
    public class Prenda
    {
        public Prenda()
        {
            DetallePrendas = new List<DetallePrenda>();
        }
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }
        [Display(Name = "Diseñador")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Usuario { get; set; }
        public int TipoId { get; set; }
        [ForeignKey("TipoId")]
        public virtual Tipo Tipo { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Talle { get; set; }
        public Boolean Vitrina { get; set; }
        public int PrecioTotal { get; set; }
        public virtual ICollection<DetallePrenda> DetallePrendas { get; set; }

    }
}