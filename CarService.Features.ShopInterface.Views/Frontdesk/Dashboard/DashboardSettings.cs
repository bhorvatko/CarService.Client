using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Views.Frontdesk.Dashboard
{
    public class DashboardSettings
    {
        public List<TechnicianDashboardSettings> TechnicianDashboardSettings { get; set; } = new List<TechnicianDashboardSettings>();

        public TechnicianDashboardSettings GetTechnicianDashboardSetting(int index)
        {
            while (TechnicianDashboardSettings.Count <= index)
            {
                TechnicianDashboardSettings.Add(new TechnicianDashboardSettings());
            }

            return TechnicianDashboardSettings[index];
        }
    }
}
