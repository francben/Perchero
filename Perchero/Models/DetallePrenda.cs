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
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }
        public int PrendaId { get; set; }
        [ForeignKey("PrendaId")]
        public virtual Prenda Prenda { get; set; }
        public int AvioId { get; set; }
        [ForeignKey("AvioId")]
        public virtual Avio Avio { get; set; }
        public int TelaId { get; set; }
        [ForeignKey("TelaId")]
        public virtual Tela Tela { get; set; }
        public Double MetroTela { get; set; }
        public int CantidadAvio { get; set; }
    }
}