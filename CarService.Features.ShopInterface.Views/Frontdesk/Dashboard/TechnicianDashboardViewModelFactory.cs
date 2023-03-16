using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Views.Frontdesk.Dashboard
{
    public class TechnicianDashboardViewModelFactory
    {
        private readonly IServiceProvider serviceProvider;

        public TechnicianDashboardViewModelFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public TechnicianDashboardViewModel CreateSingle(IEnumerable<Technician> availableTechnicians, int vmIndex)
        {
            TechnicianDashboardViewModel newVm = (TechnicianDashboardViewModel)serviceProvider.GetService(typeof(TechnicianDashboardViewModel))!;

            newVm.AvailableTechnicians = availableTechnicians;
            newVm.VmIndex = vmIndex;

            return newVm;
        }

        public IEnumerable<TechnicianDashboardViewModel> Create(IEnumerable<Technician> availableTechnicians, int numOfViewModels)
        {
            List<TechnicianDashboardViewModel> vms = new List<TechnicianDashboardViewModel>();

            for (int i = 0; i < numOfViewModels; i++)
            {
                vms.Add(CreateSingle(availableTechnicians, i));
            }

            return vms;
        }
    }
}
