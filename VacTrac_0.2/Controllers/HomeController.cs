using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

        //Razor view methods
        public IActionResult Index()
        {

            var AllVaccines = _myDbContext.Vaccines;
            ViewData["VFC"] = GetFormattedVaccines(_myDbContext.Vaccines.Where(x => x.Private == "VFC"));
            ViewData["Private"] = GetFormattedVaccines(_myDbContext.Vaccines.Where(x => x.Private == "Private"));
            //ViewData["Inventory"] = _myDbContext.WeeklyCounts.GroupBy(t => t.VaccinesID).Select(grp => grp.OrderByDescending(t => t.Date).FirstOrDefault());

            return View(AllVaccines);
        }
        public IActionResult WeeklyCounts()
        {
            ViewData["Vaccines"] = GetVaccineKeys();
            var weeklyCounts = _myDbContext.WeeklyCounts;
            return View(weeklyCounts);
        }
        public IActionResult CreateWeeklyCount()
        {
            ViewData["Vaccines"] = GetVaccineKeys();
            return View();
        }
        [HttpPost]
        public IActionResult CreateWeeklyCount(WeeklyCounts weeklyCount)
        {
            _myDbContext.WeeklyCounts.Add(weeklyCount);
            _myDbContext.SaveChanges();
            return RedirectToAction("WeeklyCounts");
        }

        public IActionResult EditWeeklyCount(int? id)
        {
            ViewData["Vaccines"] = GetVaccineKeys();
            var weeklyCount = (from v in _myDbContext.WeeklyCounts
                           where v.ID == id
                           select v).FirstOrDefault();

            return View(weeklyCount);
        }
        [HttpPost]
        public IActionResult EditWeeklyCount(WeeklyCounts w)
        {
            var wkc = (from v in _myDbContext.WeeklyCounts
                       where v.ID == w.ID
                       select v).FirstOrDefault();

            wkc.Date = w.Date;
            wkc.AccuVaxCount = w.AccuVaxCount;
            //wkc.Dispensed = w.Dispensed;
            wkc.FridgeCount = w.FridgeCount;
            _myDbContext.SaveChanges();

            return RedirectToAction("WeeklyCounts");
        }

        public IActionResult DeleteWeeklyCount(int? id)
        {

            ViewData["Vaccines"] = GetVaccineKeys();
            var weeklyCount = (from v in _myDbContext.WeeklyCounts
                           where v.ID == id
                           select v).FirstOrDefault();

            return View(weeklyCount);
        }

        [HttpPost]
        public IActionResult DeleteWeeklyCount(WeeklyCounts weeklyCount)
        {
            var wk = (from v in _myDbContext.WeeklyCounts
                       where v.ID == weeklyCount.ID
                       select v).FirstOrDefault();

            _myDbContext.Remove(wk);
            _myDbContext.SaveChanges();

            return RedirectToAction("WeeklyCounts");
        }

        public IActionResult Vaccines()
        {
            var AllVaccines = _myDbContext.Vaccines;
            return View(AllVaccines);
        }

        public IActionResult CreateVaccine()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateVaccine(Vaccines vaccine)
        {
            vaccine.WeeklyPAR = vaccine.MonthlyPAR / 4;
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
            //vac.InventoryAccuvax = vaccine.InventoryAccuvax;
            //vac.InventoryFridge = vaccine.InventoryFridge;
            vac.Private = vaccine.Private;
            vac.VaccineName = vaccine.VaccineName;
            vac.WeeklyPAR = vaccine.MonthlyPAR / 4;
            vac.MonthlyPAR = vaccine.MonthlyPAR;

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

        public IQueryable<Vaccines> GetPrivateVaccines()
        {
            return _myDbContext.Vaccines.Where(x => x.Private == "Private");
            
        }
        public IQueryable<Vaccines> GetVFCVaccines()
        {
           return _myDbContext.Vaccines.Where(x => x.Private == "VFC");
        }
        public IQueryable<Vaccines> GetAllVaccines()
        {
            return _myDbContext.Vaccines;
        }
        public Dictionary<int, string> GetVaccineKeys()
        {
            Dictionary<int, string> keys = new Dictionary<int, string>();

            foreach(var v in GetAllVaccines())
            {
                keys.Add(v.ID, v.VaccineName + " - " + v.Private);
            }

            return keys;
        }

        public IQueryable<VaccinesFormatted> GetFormattedVaccines(IQueryable<Vaccines> vaccines)
        {
            List<VaccinesFormatted> vaccinesFormatteds = new List<VaccinesFormatted>();
            foreach(var v in vaccines)
            {
                var mostRecentWeeklyTotal = _myDbContext.WeeklyCounts.Where(x => x.VaccinesID == v.ID).OrderByDescending(x => x.Date).FirstOrDefault();
                VaccinesFormatted vf = new VaccinesFormatted();
                vf.VaccineName = v.VaccineName;
                vf.Description = v.Description;
                if (mostRecentWeeklyTotal != null)
                {
                    vf.InventoryAccuvax = mostRecentWeeklyTotal.AccuVaxCount;
                    vf.InventoryFridge = mostRecentWeeklyTotal.FridgeCount;
                }
                else
                {
                    vf.InventoryAccuvax = 0;
                    vf.InventoryFridge = 0;
                }
                vf.MonthlyPAR = v.MonthlyPAR;
                vf.WeeklyPAR = v.WeeklyPAR;

                vaccinesFormatteds.Add(vf);
            }

            return vaccinesFormatteds.AsQueryable();
        }

        public string getVaccineMasterListDataTable()
        {
            return "";
        }

        public void saveVaccine(Vaccines vaccine)
        {
            var vac = (from v in _myDbContext.Vaccines
                       where v.ID == vaccine.ID
                       select v).FirstOrDefault();

            vac.Description = vaccine.Description;
            //vac.InventoryAccuvax = vaccine.InventoryAccuvax;
            //vac.InventoryFridge = vaccine.InventoryFridge;
            vac.Private = vaccine.Private;
            vac.VaccineName = vaccine.VaccineName;
            vac.WeeklyPAR = vaccine.MonthlyPAR / 4;
            vac.MonthlyPAR = vaccine.MonthlyPAR;

            _myDbContext.SaveChanges();
        }

        public void deleteVaccine(int ID)
        {
            var vac = (from v in _myDbContext.Vaccines
                       where v.ID == ID
                       select v).FirstOrDefault();

            _myDbContext.Remove(vac);
            _myDbContext.SaveChanges();
        }
    }
}
