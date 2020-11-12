using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faktura.Areas.Identity.Data;
using Faktura.Data.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Faktura.Data
{
    public class FakturaDbContext : IdentityDbContext<AppUser>
    {
        public FakturaDbContext(DbContextOptions<FakturaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Stavka> Stavka { get; set; }
        public DbSet<Faktura> Faktura {get; set;} 
        public DbSet<FakturaStavka> FakturaStavka { get; set; }
        public DbSet<Pdv> Pdv { get; set; }
        
    }
}
