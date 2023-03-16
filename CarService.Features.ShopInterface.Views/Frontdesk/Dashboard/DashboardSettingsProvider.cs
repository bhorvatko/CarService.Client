using CarService.Client.Core.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Views.Frontdesk.Dashboard
{
    public class DashboardSettingsProvider : SettingsProvider<DashboardSettings>
    {
        public DashboardSettingsProvider() : base("dashboardSettings")
        {
        }
    }
}
