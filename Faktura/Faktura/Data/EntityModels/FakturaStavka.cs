using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Faktura.Data.EntityModels
{
    public class FakturaStavka
    {
        public int FakturaStavkaId { get; set; }

        [ForeignKey("FakturaId")]
        public int FakturaId { get; set; }
        public virtual  Faktura Faktura {get; set;}


        [ForeignKey("StavkaId")]
        public int StavkaId { get; set; }
        public virtual  Stavka Stavka {get; set;}


        public int Kolicina { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Iznos { get; set; }
    }
}
