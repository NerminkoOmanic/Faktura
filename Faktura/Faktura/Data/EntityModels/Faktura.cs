using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Faktura.Areas.Identity.Data;
using Faktura.Data.EntityModels;

namespace Faktura.Data
{
    public class Faktura
    {
        public int FakturaId {get; set;}
        

        public DateTime DatumIzdavanja {get; set; }
        public DateTime RokPlacanja {get; set; }
        
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Iznos { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal IznosSaPdv { get; set; }


        [ForeignKey("AppUserId")]
        public string AppUserId {get; set;}
        public virtual AppUser AppUser { get; set; }


        [ForeignKey("PdvId")]
        public string PdvId {get; set;}
        public virtual Pdv Pdv { get; set; }


        public string Kupac {get; set;}
        public List<FakturaStavka> Stavke { get; set; }

        public Faktura()
        {
            Stavke = new List<FakturaStavka>();
        }
    }
}
