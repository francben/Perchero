using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Perchero.Models
{
    public class Talle
    {
        public Talle()
        {
            TallePrendas = new List<TallePrenda>();
        }
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }
        public int NumeroTalle { get; set; }
        public int Busto { get; set; }
        public int Cintura { get; set; }
        public int Cadera { get; set; }
        public int AnchoEspalda { get; set; }
        public int AnchoTorax { get; set; }
        public int Hombro { get; set; }
        public int Cuello { get; set; }
        public int AlturaBusto { get; set; }
        public int Pinza { get; set; }
        public int SeparacionBusto { get; set; }
        public int ContornoBrazo { get; set; }
        public int PunhoAjustado { get; set; }
        public int PunhoFlojo { get; set; }
        public int TalleEspalda { get; set; }
        public int TalleDelantero { get; set; }
        public int AlturaAxila { get; set; }
        public int AlturaRodilla { get; set; }
        public int AlturaCCadera { get; set; }
        public int LargoCinturaSuelo { get; set; }
        public int TiroPaantalonDelantero { get; set; }
        public int LargoManga { get; set; }
        public virtual ICollection<TallePrenda> TallePrendas { get; set; }
    }
}