using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KPI_Dashboard.Models;

namespace KPI_Dashboard.Controllers
{
    // Klasse, die die Startseite verwaltet
    // Diese Klasse enthält alle Aktionen die von der Startseite aus
    // aufgerufen werden können (nur Dashboard hinzufügen)
    public class DashboardsController : Controller
    {
        private readonly KPIContext _context;
        private readonly DashboardContainer  _d;
        
        public DashboardsController(IDashboardContainer d, KPIContext context)
        {
            _d = (DashboardContainer)d;
            _context = context;
        }

        // Gibt die Startseite zurück mit der Auflistung aller Dashboards
        public IActionResult Index()
        {
            return View(_d.GetDashboards());
        }

        // Gibt die Seite zum hinzufügen von Dashboards zurück
        [HttpGet]
        public async Task<IActionResult> New()
        {
           return View(await _context.Kpi.OrderBy(item => item.CellDescription).ToListAsync());
        }

        // Nimmt die Daten eines neu angelegten Dashboards entgegen und fügt des dem Dashboard Container hinzu
        [HttpPost]
        public IActionResult New(String submit, String name, String title, List<String> cells)
        {
            if(submit.Equals("Hinzufügen") && name!= null && title != null)
            {
                Dashboard d = new Dashboard();
                int id = 1;

                foreach (Dashboard dashboard in _d.GetDashboards())
                {
                    if (id != dashboard.id)
                        break;
                    id++;
                }

                d.id = id;
                d.name = name;
                d.title = title;
                d.cells = cells;
                _d.LinkDashboard(d);
            }
            return RedirectToAction("Index", "Dashboards");
        }

        // Gibt das Dashboard mit der entsprechenden ID zurück
        [Route("/Dashboards/Dashboard/{id}")]
        public IActionResult Dashboard(int id)
        {
            return View(_d.GetDashboards().Find(dashboard => dashboard.id.Equals(id)));
        }
    }
}
