using System.Collections.Generic;

namespace KPI_Dashboard.Models
{
    // Interface für für den DashboardContainer
    public interface IDashboardContainer
    {
        void LinkDashboard(Dashboard d);
        void UnlinkDashboard(Dashboard d);
        List<Dashboard> GetDashboards();
    }
}
