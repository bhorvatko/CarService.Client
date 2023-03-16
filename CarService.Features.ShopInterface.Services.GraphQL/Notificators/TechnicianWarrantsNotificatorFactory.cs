using CarService.Client.ApiInteraction.Session;
using CarService.Features.ShopInterface.Model;
using CarService.Features.ShopInterface.Services.GraphQL.Subscriptions;
using CarService.Features.ShopInterface.Services.Notificators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Notificators
{
    public class TechnicianWarrantsNotificatorFactory : ITechnicianWarrantsNotificatorFactory
    {
        private readonly WarrantAddedSubscription warrantAddedSubscription;
        private readonly WarrantUpdatedSubscription warrantUpdatedSubscription;
        private readonly WarrantRemovedSubscription warrantRemovedSubscription;
        private readonly SessionProvider sessionProvider;

        public TechnicianWarrantsNotificatorFactory(
            WarrantAddedSubscription warrantAddedSubscription,
            WarrantUpdatedSubscription warrantUpdatedSubscription,
            WarrantRemovedSubscription warrantRemovedSubscription,
            SessionProvider sessionProvider)
        {
            this.warrantAddedSubscription = warrantAddedSubscription;
            this.warrantUpdatedSubscription = warrantUpdatedSubscription;
            this.warrantRemovedSubscription = warrantRemovedSubscription;
            this.sessionProvider = sessionProvider;
        }

        public ITechnicianWarrantsNotificator CreateTechnicianWarrantsNotificator(int? technicianId,
            Action<Action<Technician>> onTechniciansWarrantsUpdatedAction)
        {
            ITechnicianWarrantsNotificator notificator = new TechnicianWarrantsNotificator(warrantUpdatedSubscription, warrantAddedSubscription, warrantRemovedSubscription, sessionProvider, technicianId);

            notificator.OnTechnicianWarrantsUpdated += onTechniciansWarrantsUpdatedAction;

            return notificator;
        }
    }
}
