using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace KPI_Dashboard.Models
{
    // Verwaltung einer Liste mit allen Dashboards inklusive persitentem laden und speichern
    // Ist als Singleton implementiert! Es darf keine zusätzliche Instanz dieser Klasse geben!
    public class DashboardContainer : IDashboardContainer
    {
        private readonly IHostingEnvironment _env;
        private readonly List<Dashboard> dashboards;

        public DashboardContainer(IHostingEnvironment env)
        {
            _env = env;
            dashboards = LoadDashboards();
        }

        // Hinzufügen eines Dashboards
        public void LinkDashboard(Dashboard d)
        {
            dashboards.Add(d);
            SaveDashboards();
        }

        // Löschen eines Dashboards
        public void UnlinkDashboard(Dashboard d)
        {
            dashboards.Remove(d);
            SaveDashboards();
        }

        // Gibt die Liste mit allen Dashboards zurück
        public List<Dashboard> GetDashboards()
        {
            List<Dashboard> clone = new List<Dashboard>();

            foreach (Dashboard d in dashboards)
            {
                clone.Add(d);
            }
            return clone;
        }

        // Lädt die Dashboards aus der Datei dashboards.json
        private List<Dashboard> LoadDashboards()
        {
            List<Dashboard> dashboards = new List<Dashboard>();

            using (StreamReader r = new StreamReader(Path.Combine(_env.ContentRootPath, "dashboards.json")))
            {
                String file = r.ReadToEnd();
                JArray json = JArray.Parse(file);

                foreach (JObject o in json.Children<JObject>())
                {
                    Dashboard d = new Dashboard();
                    d.id = o["id"].Value<int>();
                    d.name = o["name"].Value<String>();
                    d.title = o["title"].Value<String>();
                    var cellsArray = o["cells"].Value<JArray>();

                    List<String> cells = new List<string>();

                    IList collection = (IList)cellsArray;

                    for (int i = 0; i < collection.Count; i++)
                    {
                        cells.Add(collection[i].ToString());
                    }

                    d.cells = cells;
                    dashboards.Add(d);
                }
            }
            return dashboards;
        }

        // Speichert die Dashboards in der Datei dashboards.json
        private void SaveDashboards()
        {
            using (StreamWriter w = new StreamWriter(Path.Combine(_env.ContentRootPath, "dashboards.json")))
            {
                var json = JsonConvert.SerializeObject(dashboards, Formatting.Indented);
                w.Write(json);
            }
        }
    }
}
