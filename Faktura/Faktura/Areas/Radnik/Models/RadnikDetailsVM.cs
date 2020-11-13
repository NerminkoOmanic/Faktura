using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faktura.Areas.Identity.Data;

namespace Faktura.Areas.Radnik.Models
{
    public class RadnikDetailsVM
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Jmbg { get; set; }


        public RadnikDetailsVM(AppUser user)
        {
            Fname = user.FirstName;
            Lname = user.LastName;
            Email = user.Email;
            Jmbg = user.JMBG;
        }
    }
}
