using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPI_Dashboard.Models
{
    // Datenklasse die eine Produktionszelle (aus der Datenbank) als Objekt darstellt
    public partial class Kpi
    {
        [Key]
        public string CellDescription { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Yield { get; set; }
        public decimal? Util { get; set; }
        public decimal? Oee { get; set; }
        public decimal? ProdTimeTd { get; set; }
        public decimal? UnprodTimeTd { get; set; }
        public decimal? PlanUnprodTimeTd { get; set; }
        public string RunStatus { get; set; }
        public double? ChangeOverTargetDuration { get; set; }
        public int? ChangeOverDuration { get; set; }
    }
}
