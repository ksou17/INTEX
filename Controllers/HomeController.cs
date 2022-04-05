
using INTEX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DBContext _context;

        public HomeController(ILogger<HomeController> logger, DBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string city=" ")
        {
            ViewBag.cities = _context.crashes.Select(c => c.CITY).Distinct();
            ViewBag.TableInfo = _context.crashes.ToList();
            ViewBag.selectedCity = city;

            int total_WZR = 0;
            foreach (var w in ViewBag.TableInfo)
            {
                if (w.WORK_ZONE_BOOL == true)
                {

                    total_WZR += 1;
                    ViewBag.WZR = total_WZR;
                }
            }
            return View();
        }
        public IActionResult Prevention()
        {
            return View();
        }

        public IActionResult Crashes(int page = 1, string date = "", string time = "", string city = " ", string county="", int severity = -1)
        {
            var crashes = _context.crashes.AsQueryable();
            if (date != null && date != "")
            {
                DateTime comparison = DateTime.Parse(date).Date;
                crashes = crashes.ToList().Where(c => c.CRASH_DATE.Date == comparison).AsQueryable();
            }
            if (city != null && city != " " && crashes.Count() > 0)
            {
                crashes = crashes.Where(c => c.CITY == city).AsQueryable();
            }
            if (county != null && county != "" && crashes.Count() > 0)
            {
                crashes = crashes.Where(c => c.COUNTY_NAME == county).AsQueryable();
            }
            if (severity != -1 && crashes.Count() > 0)
            {
                crashes = crashes.Where(c => c.CRASH_SEVERITY_ID == severity).AsQueryable();
            }
            
            if (crashes.ToList().Count() > 0)
            {
                ViewBag.crashes = crashes.Skip((page - 1) * 10).Take(10).ToList().OrderBy(c => c.CRASH_DATE);
            }
            else
            {
                ViewBag.crashes = new List<Crash>();
            }
            
            ViewBag.cities = _context.crashes.Select(c => c.CITY).Distinct();
            ViewBag.counties = _context.crashes.Select(c => c.COUNTY_NAME).Distinct();
            ViewBag.severity = _context.crashes.Select(c => c.CRASH_SEVERITY_ID).Distinct();
            ViewBag.page = page;
            ViewBag.totalPages = (crashes.ToList().Count()) / 10 != 0 ? (crashes.ToList().Count()) / 10 : 1;
            ViewBag.selectedCity = city;
            ViewBag.selectedCounty = county;
            ViewBag.selectedSeverity = severity;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int CRASH_ID)
        {
            Crash crash = _context.crashes.First(c => c.CRASH_ID == CRASH_ID);
            _context.crashes.Remove(crash);
            _context.SaveChanges();
            return RedirectToAction("Crashes");
        }
        [HttpGet]
        public IActionResult Edit(int CRASH_ID)
        {
            ViewBag.crash = _context.crashes.First(c => c.CRASH_ID == CRASH_ID);
            return View();
        }
    }
}
