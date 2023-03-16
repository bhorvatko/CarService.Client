using CarService.Client.ApiInteraction.Session;
using CarService.Features.ShopInterface.Model;
using CarService.Features.ShopInterface.Services.GraphQL.Subscriptions;
using CarService.Features.ShopInterface.Services.GraphQL.Types.Events;
using CarService.Features.ShopInterface.Services.Notificators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Notificators
{
    public class TechnicianWarrantsNotificator : ITechnicianWarrantsNotificator
    {
        private readonly SessionProvider sessionProvider;
        private int? technicianId;

        public int? TechnicianId => technicianId;
        public event Action<Action<Technician>>? OnTechnicianWarrantsUpdated;

        public TechnicianWarrantsNotificator(
            WarrantUpdatedSubscription warrantUpdatedSubscription,
            WarrantAddedSubscription warrantAddedSubscription,
            WarrantRemovedSubscription warrantRemovedSubscription,
            SessionProvider sessionProvider,
            int? technicianId)
        {
            this.technicianId = technicianId;
            this.sessionProvider = sessionProvider;

            warrantUpdatedSubscription.OnEvent += OnWarrantUpdate;
            warrantAddedSubscription.OnEvent += OnWarrantAdded;
            warrantRemovedSubscription.OnEvent += OnWarrantRemoved;
        }

        private void OnWarrantUpdate(WarrantUpdatedEventDto e)
        {
            Action<Technician> action = t =>
            {
                foreach (Warrant war in t.Warrants.Where(w => w.Id == e.Warrant.Id))
                {
                    e.Warrant.MapToDomain(war);
                    if (!sessionProvider.IsIdOfCurrentSession(e.InitiatorSessionId)) war.NotifyExternalUpdateOccured();
                }
            };

            OnTechnicianWarrantsUpdated?.Invoke(action);
        }

        private void OnWarrantAdded(WarrantAddedEventDto e)
        {
            if (e.TechnicianId == technicianId)
            {
                Action<Technician> action = t =>
                {
                    Warrant addedWarrant = e.Warrant.MapToDomain();
                    t.Warrants.Add(addedWarrant);
                    if (!sessionProvider.IsIdOfCurrentSession(e.InitiatorSessionId)) addedWarrant.NotifyExternalUpdateOccured();
                };


                OnTechnicianWarrantsUpdated?.Invoke(action);
            }
        }

        private void OnWarrantRemoved(WarrantRemovedEventDto e)
        {
            if (e.TechnicianId == technicianId)
            {
                Action<Technician> action = t =>
                {
                    Warrant? warrantToRemove = t.Warrants.FirstOrDefault(w => w.Id == e.WarrantId);

                    if (warrantToRemove != null) t.Warrants.Remove(warrantToRemove);
                };

                OnTechnicianWarrantsUpdated?.Invoke(action);
            }
        }
    }
}
