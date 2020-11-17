using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faktura.Areas.Radnik.Models
{
    public class FaktureListVM
    {
        public string RadnikId{get; set;}
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int FakturaId {get; set; }
            public string DatumIzdavanja { get; set; }
            public string RokPlacanja {get; set; }
            public decimal UkupnaCijenaBezPdv { get; set; }
            public decimal UkupnaCijenaSaPdv { get; set; }
            public string RadnikId { get; set; }
            public string ImePrezime { get; set; }
            public string Kupac {get; set;}
            public decimal PdvIznos {get; set;}
            public string PdvIznosString { get{return(PdvIznos*100+" % ");}  }
        }
    }
}
