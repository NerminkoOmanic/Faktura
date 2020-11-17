using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faktura.Areas.Radnik.Models;
using Faktura.Data;
using Microsoft.EntityFrameworkCore;

namespace Faktura.Util
{
    public class UserManagementHelper
    {
        private FakturaDbContext _db;

        public UserManagementHelper(FakturaDbContext db)
        {
            _db = db;
        }

        //Helper za postavljanje FaktureStavkeVM view modela
        public FakturaStavkeVM PrepFaktureStavke(int fakturaId)
        {
            Data.Faktura f = _db.Faktura.Where(x => x.FakturaId == fakturaId)
                .Include(x => x.AppUser).
                Include(x => x.Pdv)
                .FirstOrDefault();

            FakturaStavkeVM model = new FakturaStavkeVM()
            {
                FakturaId = f.FakturaId,
                RadnikId = f.AppUserId,
                ImePrezime = f.AppUser.FirstName + " " + f.AppUser.LastName,
                IznosPdv = f.Pdv.IznosPdv,
                DatumIzdavanja = f.DatumIzdavanja,
                RokPlacanja = f.RokPlacanja,
                Kupac = f.Kupac
            };

            model.Rows = _db.FakturaStavka.Where(x => x.FakturaId == fakturaId).Select(x => new FakturaStavkeVM.Row()
                {
                    FakturaStavkaId = x.FakturaStavkaId,
                    StavkaId = x.StavkaId,
                    OpisStavke = x.Stavka.OpisStavke,
                    CijenaStavkeBezPdv = x.Stavka.Cijena,
                    Kolicina = x.Kolicina,
                    UkupnoZaStavku = x.Kolicina * x.Stavka.Cijena
                })
                .ToList();
            model.UkupnoBezPoreza = model.Rows.Sum(x => x.UkupnoZaStavku);
            model.UkupnoSaPorezom = model.UkupnoBezPoreza + (model.UkupnoBezPoreza * model.IznosPdv);

            return model;
        }

        
    }
}
