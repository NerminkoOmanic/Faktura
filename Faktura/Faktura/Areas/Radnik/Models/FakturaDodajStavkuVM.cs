using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Faktura.Areas.Radnik.Models
{
    public class FakturaDodajStavkuVM
    {
        public int FakturaId { get; set; }
        public int StavkaId { get; set; }
        public List<SelectListItem> StavkeList { get; set; }
        public int Kolicina {get; set; }

        public FakturaDodajStavkuVM()
        {
            StavkeList = new List<SelectListItem>();
        }
    }
}
