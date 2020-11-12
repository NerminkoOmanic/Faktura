using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Faktura.Data.EntityModels
{
    public class Stavka
    {
        public int StavkaId {get; set; }
        
        public string OpisStavke {get; set;}

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Cijena { get; set; }
    }
}
