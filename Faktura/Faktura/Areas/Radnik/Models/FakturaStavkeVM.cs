using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faktura.Areas.Radnik.Models
{
    public class FakturaStavkeVM
    {
        public int FakturaId { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public DateTime RokPlacanja {get; set;}
        public decimal UkupnoBezPoreza { get; set; }
        public decimal UkupnoSaPorezom { get; set; }
        public decimal IznosPdv { get; set; }
        public string IznosPdvString { get{return (IznosPdv*100+" % "); }}
        public string RadnikId { get; set; }
        public string ImePrezime { get; set; }
        public string Kupac { get; set; }
        public List<Row> Rows {get; set; }

        public class Row
        {
            public int FakturaStavkaId { get; set; }
            public int StavkaId {get; set;}
            public string OpisStavke {get; set;}
            public int Kolicina {get; set;}
            public decimal CijenaStavkeBezPdv { get; set; }
            public decimal UkupnoZaStavku {get; set;}

        }
    }
}
