using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faktura.Areas.Identity.Data;
using Faktura.Areas.Radnik.Models;
using Faktura.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faktura.Areas.Radnik.Controllers
{
    [Authorize(Roles = "Radnik")]
    [Area("Radnik")]
    public class HomeController : Controller
    {
        private FakturaDbContext _db;

        public HomeController(FakturaDbContext context)
        {
            _db = context;
        }
        // GET: HomeController
        public ActionResult Index(string id)
        {
            AppUser user = _db.Users.Where(x => x.Id == id).FirstOrDefault();
            RadnikDetailsVM model = new RadnikDetailsVM(user);
            return View(model);
        }

       
    }
}
