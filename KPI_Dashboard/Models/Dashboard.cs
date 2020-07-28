using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPI_Dashboard.Models
{
    // Diese Klasse repräsentiert genau ein Dashboard als Objekt
    public class Dashboard
    {
        [Key]
        public int id { get; set; }
        public String name { get; set; }
        public String title { get; set; }
        public List<String> cells;
    }
}
