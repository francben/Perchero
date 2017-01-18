using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Perchero.Models
{
    public class DetallePrenda
    {
        public DetallePrenda() {
            Avios = new List<Avio>();
            Telas = new List<Tela>();
        }
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }
        public int PrendaId { get; set; }
        [ForeignKey("PrendaId")]
        public virtual Prenda Prenda { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public Double MetroTela { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public int CantidadAvio { get; set; }
        public virtual ICollection<Avio> Avios { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public virtual ICollection<Tela> Telas { get; set; }
    }
}