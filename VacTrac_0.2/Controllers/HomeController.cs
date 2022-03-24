using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VacTrac.Models;

namespace VacTrac.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBCtx _myDbContext = new DBCtx();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var vaccines = _myDbContext.Vaccines.First();
            return View(vaccines);
        }

        public IActionResult Vaccines()
        {
            var vaccines = _myDbContext.Vaccines;
            return View(vaccines);
        }
        public IActionResult Private()
        {
            return View();
        }

        public IActionResult CreateVaccine()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateVaccine(Vaccines vaccine)
        {
            _myDbContext.Vaccines.Add(vaccine);
            _myDbContext.SaveChanges();
            return RedirectToAction("Vaccines");
        }


        public IActionResult EditVaccine(int? id)
        {
            var vaccine = (from v in _myDbContext.Vaccines
                           where v.ID == id
                           select v).FirstOrDefault();

            return View(vaccine);
        }
        [HttpPost]
        public IActionResult EditVaccine(Vaccines vaccine)
        {
            var vac = (from v in _myDbContext.Vaccines
                           where v.ID == vaccine.ID
                           select v).FirstOrDefault();

            vac.Description = vaccine.Description;
            vac.InventoryTotal = vaccine.InventoryTotal;
            vac.Private = vaccine.Private;
            vac.VaccineName = vaccine.VaccineName;
            vac.WeeklyPAR = vaccine.WeeklyPAR;

            _myDbContext.SaveChanges();

            return RedirectToAction("Vaccines");
        }


        public IActionResult VaccineDetails(int? id)
        {
            var vaccine = (from v in _myDbContext.Vaccines
                           where v.ID == id
                           select v).FirstOrDefault();

            return View(vaccine);
        }


        public IActionResult DeleteVaccine(int? id)
        {
            var vaccine = (from v in _myDbContext.Vaccines
                           where v.ID == id
                           select v).FirstOrDefault();

            return View(vaccine);
        }
        [HttpPost]
        public IActionResult DeleteVaccine(Vaccines vaccine)
        {
            var vac = (from v in _myDbContext.Vaccines
                       where v.ID == vaccine.ID
                       select v).FirstOrDefault();

            _myDbContext.Remove(vac);
            _myDbContext.SaveChanges();

            return RedirectToAction("Vaccines");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
