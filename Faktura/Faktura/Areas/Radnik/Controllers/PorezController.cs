using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faktura.Areas.Radnik.Models;
using Faktura.Data;
using Faktura.Data.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace Faktura.Areas.Radnik.Controllers
{
    [Area("Radnik")]
    public class PorezController : Controller
    {
        
        private FakturaDbContext _db;

        public PorezController(FakturaDbContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            PorezListVM model = new PorezListVM()
            {
                Rows = _db.Pdv.Select(x => new PorezListVM.Row()
                {
                    PdvId = x.PdvId,
                    Drzava = x.Drzava,
                    Iznos = (int)(x.IznosPdv * 100)

                }).ToList()
            };
            return View(model);
        }

        public IActionResult DodajPV()
        {
            PorezDodajVM model = new PorezDodajVM();
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult DodajPV(PorezDodajVM input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(input);
            }

            Pdv v = new Pdv()
            {
                Drzava = input.Drzava,
                IznosPdv = Convert.ToDecimal(input.IznosPoreza)/100
            };
            _db.Add(v);
            _db.SaveChanges();
            _db.Dispose();

            return RedirectToAction(nameof(Index));
        }
    }
}
