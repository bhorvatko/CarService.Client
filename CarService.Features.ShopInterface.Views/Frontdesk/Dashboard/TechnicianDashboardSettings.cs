using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Views.Frontdesk.Dashboard
{
    public class TechnicianDashboardSettings
    {
        public int? SelectedTechnicianId { get; set; }
        public List<ProcedureFilter> Filters { get; set; } = new List<ProcedureFilter>();

        public ProcedureFilter GetProcedureFilter(int procedureId)
        {
            if (!Filters.Any(f => f.ProcedureId == procedureId))
            {
                Filters.Add(new ProcedureFilter() { ProcedureId = procedureId });
            }

            return Filters.First(f => f.ProcedureId == procedureId);
        }
    }

    public class ProcedureFilter
    {
        public int ProcedureId { get; set; }
        public bool Filter { get; set; }
    }
}
