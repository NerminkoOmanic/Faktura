using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faktura.Areas.Radnik.Models;
using Faktura.Data;
using Faktura.Data.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faktura.Areas.Radnik.Controllers
{
    [Area("Radnik")]
    public class StavkaController : Controller
    {
        private FakturaDbContext _db;

        public StavkaController(FakturaDbContext context)
        {
            _db = context;
        }
        // GET: StavkaController
        public ActionResult Index()
        {
            StavkeListVM model = new StavkeListVM();
            
            model.Rows=_db.Stavka.Select(x => new StavkeListVM.Row()
            {
                StavkaId = x.StavkaId,
                OpisStavke = x.OpisStavke,
                Cijena = x.Cijena
            }).ToList();
            return View(model);
        }

        // GET: StavkaController/Create
        public IActionResult DodajPV()
        {
            StavkeDodajVM model = new StavkeDodajVM();
            return PartialView(model);
        }

        // POST: StavkaController/Create
        [HttpPost]
        public IActionResult DodajPV(StavkeDodajVM input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(input);
            }

            Stavka s = new Stavka()
            {
                OpisStavke = input.OpisStavke,
                Cijena = input.Cijena
            };
            _db.Add(s);
            _db.SaveChanges();
            _db.Dispose();

            return RedirectToAction(nameof(Index));
        }

    }
}
