using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KPI_Dashboard.Models;

namespace KPI_Dashboard.Controllers
{
    // Diese Klasse ist für das Ändern der Einstellungen eines Dashboards verantwortlich. Die Einstellungen werden von der View gesendet (HttpPost) 
    //und durch diese Klasse wird das entsprechende Objekt geändert, oder gelöscht
    public class SettingsController : Controller
    {
        private readonly KPIContext _context;
        private readonly IDashboardContainer _d;

        public SettingsController(IDashboardContainer d, KPIContext context)
        {
            _context = context;
            _d = d;
        }

        // Lädt die verfügbaren Produktionszellen aus der Datenbank und gibt leitet diese an die View weiter.
        [HttpGet]
        [Route("/settings/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            return View(await _context.Kpi.OrderBy(item => item.CellDescription).ToListAsync());
        }

        // Führt die gesendete Aktion aus. Entweder werden die Einstellungen des 
        // Dashboards geändert, oder das Dashboard aus dem Container gelöscht.
        [HttpPost]
        [Route("/settings/{id}")]
        public IActionResult Index([FromRoute]int id, String submit, String name, String title, List<String> Cells)
        {
            switch (submit)
            {
                case "Speichern":
                    if(name != null && title != null)
                    {
                        Dashboard d = _d.GetDashboards().Find(dashboard => dashboard.id.Equals(id));
                        d.name = name;
                        d.title = title;
                        d.cells = Cells;
                    }
                    break;

                case "Löschen":
                    _d.UnlinkDashboard(_d.GetDashboards().Find(dashboard => dashboard.id.Equals(id)));
                    return RedirectToAction("Index", "Dashboards");
            }
            return RedirectToAction("Dashboard", "Dashboards", new { id });
        }
    }
}
