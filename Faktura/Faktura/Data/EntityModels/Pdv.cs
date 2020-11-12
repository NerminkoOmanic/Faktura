using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Faktura.Data.EntityModels
{
    public class Pdv
    {
        public int PdvId {get; set; }
        public string Drzava { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal IznosPdv { get; set; }
    }
}
