using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using KPI_Dashboard.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KPI_Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // Diese Klasse liest die aktuellen Maschinendaten aus der Datenbank aus und stellt sie über eine API bereit.
    public class KpiController : ControllerBase
    {
        private readonly KPIContext _context;
        private readonly IDashboardContainer _d;

        // Im Konstruktor wird eine Referenz auf die Konfiguration und den Datenbankkontext übergeben
        public KpiController(IDashboardContainer d, KPIContext context, IConfiguration config, IHostingEnvironment env)
        {
            _context = context;
            _d = d;
        }

        // Liest die Daten zu allen Produktionszellen aus der Datenbank aus und gibt diese im JSON-Format zurück.
        [HttpGet]
        public IEnumerable<Kpi> GetKpi()
        {
            return _context.Kpi.OrderBy(item => item.CellDescription);
        }
    }
}