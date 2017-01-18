using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Perchero.Models
{
    public class Tipo
    {
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Precio { get; set; }
    }
}