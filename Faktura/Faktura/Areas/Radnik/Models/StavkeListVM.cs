using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faktura.Data.EntityModels;

namespace Faktura.Areas.Radnik.Models
{
    public class StavkeListVM
    {
        public List<Row> Rows{get; set;}
        
        public class Row
        {
            public int StavkaId { get; set; }
            public string OpisStavke { get; set; }
            public decimal Cijena {get; set;}
        }

    }
}
