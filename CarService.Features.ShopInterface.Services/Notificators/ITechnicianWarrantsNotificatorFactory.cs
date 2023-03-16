using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.Notificators
{
    public interface ITechnicianWarrantsNotificatorFactory
    {
        ITechnicianWarrantsNotificator CreateTechnicianWarrantsNotificator(int? technicianId, Action<Action<Technician>> onTechniciansWarrantsUpdatedAction);
    }
}
