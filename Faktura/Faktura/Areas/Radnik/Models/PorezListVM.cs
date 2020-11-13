using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faktura.Areas.Radnik.Models
{
    public class PorezListVM
    {
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int PdvId {get; set; }
            public string Drzava {get; set; }
            public int Iznos {get; set; }
        }
    }
}
