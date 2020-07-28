using System.Collections.Generic;
using KPI_Dashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace KPI_Dashboard.Controllers
{
    // API-Controller zur Verwaltung von Dashboards

    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardContainer _d;
        public DashboardController(IDashboardContainer d)
        {
            _d = d;
        }

        // Gibt alle Dashboards zurück
        [HttpGet]
        public IEnumerable<Dashboard> GetDashboards()
        {
            return _d.GetDashboards();
        }

        // Gibt das Dashboard mit der jeweiligen ID zurück
        [HttpGet]
        [Route("{id}")]
        public Dashboard GetDashboards(int id)
        {
            foreach(Dashboard d in _d.GetDashboards())
            {
                if (d.id == id)
                    return d;
            }
            return null;
        }

        // Legt ein neues Dashboard an
        [HttpPost]
        public IActionResult New(Dashboard d)
        {
            int id = 1;

            foreach (Dashboard dashboard in _d.GetDashboards())
            {
                if (id != dashboard.id)
                    break;
                id++;
            }

            d.id = id;
            _d.LinkDashboard(d);
            return Ok();
        }
    }
}