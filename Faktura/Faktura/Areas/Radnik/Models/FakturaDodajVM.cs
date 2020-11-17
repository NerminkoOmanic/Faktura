using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Faktura.Areas.Radnik.Models
{
    public class FakturaDodajVM
    {
        public int FakturaId{get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public DateTime RokPlacanja {get; set;}
        public string PdvId { get; set; }
        public string RadnikId { get; set; }
        public string ImePrezime { get; set; }
        public string Kupac { get; set; }
        public List<SelectListItem> PdvList {get; set;}
        
    }
}
