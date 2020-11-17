using System;
using System.Linq;
using Faktura.Areas.Identity.Data;
using Faktura.Areas.Radnik.Models;
using Faktura.Data;
using Faktura.Data.EntityModels;
using Faktura.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Faktura.Areas.Radnik.Controllers
{
    [Area("Radnik")]
    public class FakturaController : Controller
    {
        private FakturaDbContext _db;
        private UserManagementHelper _userManagementHelper;
        public FakturaController(FakturaDbContext context)
        {
            _db = context;
            _userManagementHelper = new UserManagementHelper(_db);

        }
        //GET: FakturaController
        public ActionResult Index(string radnikId)
        {

            FaktureListVM model = new FaktureListVM()
            {
                RadnikId = radnikId
            };
            model.Rows = _db.Faktura.Where(x => x.AppUserId == radnikId)
                .Include(x => x.AppUser)
                .Include(x => x.Pdv)
                .Select(x => new FaktureListVM.Row()

                {
                    FakturaId = x.FakturaId,
                    DatumIzdavanja = x.DatumIzdavanja.ToShortDateString(),
                    RokPlacanja = x.RokPlacanja.ToShortDateString(),
                    Kupac = x.Kupac,
                    UkupnaCijenaBezPdv = x.Iznos,
                    PdvIznos = x.Pdv.IznosPdv,
                    UkupnaCijenaSaPdv = x.IznosSaPdv,
                    ImePrezime = x.AppUser.FirstName + " " + x.AppUser.LastName,
                    RadnikId = x.AppUserId
                }).ToList();
            return View(model);
        }


        //GET: FakturaController/Create
        public ActionResult Dodaj(string radnikId)
        {
            AppUser user = _db.Users.Find(radnikId);
            FakturaDodajVM model = new FakturaDodajVM()
            {
                RadnikId = radnikId,
                ImePrezime = user.FirstName + " " + user.LastName,
                DatumIzdavanja = System.DateTime.Now
            };
            model.PdvList = _db.Pdv.Select(x => new SelectListItem((x.Drzava + "(" + (int)(x.IznosPdv * 100) + "%)"), x.PdvId.ToString())).ToList();

            return View(model);
        }

        // POST: FakturaController/Create
        [HttpPost]
        public ActionResult Dodaj(FakturaDodajVM input)
        {
            Data.Faktura fsearch = _db.Faktura.Find(input.FakturaId);

            if (fsearch == null)
            {
                Data.Faktura fnew = new Data.Faktura()
                {
                    AppUserId = input.RadnikId,
                    DatumIzdavanja = System.DateTime.Now,
                    RokPlacanja = input.RokPlacanja,
                    Kupac = input.Kupac,
                    PdvId = Int32.Parse(input.PdvId)
                };
                _db.Faktura.Add(fnew);
                _db.SaveChanges();
                return RedirectToAction(nameof(DodajStavke), new { fnew.FakturaId });

            }

            fsearch.Kupac = input.Kupac;
            fsearch.PdvId = Int32.Parse(input.PdvId);
            fsearch.RokPlacanja = input.RokPlacanja;
            _db.SaveChanges();

            return RedirectToAction(nameof(DodajStavke), new { fsearch.FakturaId });
        }
        //Kod izrade fakture pri dodavanju stavki radnik ima opciju vracanja na korak pripreme i izmjene osnovnih podataka
        public IActionResult BackEdit(int fakturaId)
        {
            Data.Faktura f = _db.Faktura.Where(x => x.FakturaId == fakturaId).Include(x => x.AppUser).FirstOrDefault();

            FakturaDodajVM model = new FakturaDodajVM()
            {
                FakturaId = f.FakturaId,
                RadnikId = f.AppUserId,
                ImePrezime = f.AppUser.FirstName + " " + f.AppUser.LastName,
                DatumIzdavanja = f.DatumIzdavanja
            };
            model.PdvList = _db.Pdv.Select(x => new SelectListItem((x.Drzava + "(" + (int)(x.IznosPdv * 100) + "%)"), x.PdvId.ToString())).ToList();
            return View(nameof(Dodaj), model);
        }

        //Dodaje stave na kreiranu fakturu
        public IActionResult DodajStavke(int fakturaId)
        {
            FakturaStavkeVM model = _userManagementHelper.PrepFaktureStavke(fakturaId);
            return View(model);
        }

        [HttpPost]
        public IActionResult DodajStavke(FakturaStavkeVM input)
        {

            Data.Faktura f = _db.Faktura.Find(input.FakturaId);
            f.Iznos = input.UkupnoBezPoreza;
            f.IznosSaPdv = input.UkupnoSaPorezom;

            _db.SaveChanges();

            return RedirectToAction(nameof(Index), new { radnikId = f.AppUserId });
        }


        //Priprema model i otvara partial view za izbor stavke i unos kolicine
        public IActionResult DodajStavkePV(int fakturaId)
        {

            FakturaDodajStavkuVM model = new FakturaDodajStavkuVM()
            {
                FakturaId = fakturaId
            };
            model.StavkeList = _db.Stavka.Select(x => new SelectListItem() { Text = x.OpisStavke, Value = x.StavkaId.ToString() }).ToList();

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult DodajStavkePV(FakturaDodajStavkuVM input)
        {
            decimal ukupnoStavka = input.Kolicina * _db.Stavka.Where(x => x.StavkaId == input.StavkaId)
                .Select(x => x.Cijena).FirstOrDefault();


            FakturaStavka novaStavka = new FakturaStavka()
            {
                FakturaId = input.FakturaId,
                StavkaId = input.StavkaId,
                Kolicina = input.Kolicina,
                Iznos = ukupnoStavka
            };


            _db.FakturaStavka.Add(novaStavka);
            _db.SaveChanges();


            return RedirectToAction(nameof(DodajStavke), new { input.FakturaId });
        }

        //Brisanje stavke iz fakture
        public IActionResult IzbrisiStavku(int id) //FakturaStavkaId
        {
            FakturaStavka zaBrisanje = _db.FakturaStavka.Find(id);


            int fakturaId = zaBrisanje.FakturaId;


            _db.FakturaStavka.Remove(zaBrisanje);
            _db.SaveChanges();


            return RedirectToAction(nameof(DodajStavke), new { fakturaId });
        }


        //Detalji fakture sa svim stavkama
        public IActionResult Detalji(int fakturaId)
        {
            FakturaStavkeVM model = _userManagementHelper.PrepFaktureStavke(fakturaId);
            return View(model);
        }

        public IActionResult DetaljiRadnik(string radnikId)
        {
            AppUser user = _db.Users.Where(x => x.Id == radnikId).FirstOrDefault();
            RadnikDetailsVM model = new RadnikDetailsVM(user);
            return View("FakturaRadnikDetalji",model);
        }
    }
}
