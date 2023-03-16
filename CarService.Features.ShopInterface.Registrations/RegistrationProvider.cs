using CarService.Features.ShopInterface.Services.GraphQL.Interactors;
using CarService.Features.ShopInterface.Services.GraphQL.Notificators;
using CarService.Features.ShopInterface.Services.GraphQL.Subscriptions;
using CarService.Features.ShopInterface.Services.Interactors;
using CarService.Features.ShopInterface.Services.Notificators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Registrations
{
    public static class RegistrationProvider
    {
        public static void AddShopInterfaceFeature(this IServiceCollection services)
        {
            services.AddTransient<IProcedureInteractor, ProcedureInteractor>();
            services.AddTransient<IWarrantTypeInteractor, WarrantTypeInteractor>();
            services.AddTransient<ITechnicianInteractor, TechnicianInteractor>();
            services.AddTransient<IWarrantInteractor, WarrantInteractor>();
            services.AddTransient<ITechnicianWarrantsNotificator, TechnicianWarrantsNotificator>();
            services.AddTransient<ITechnicianWarrantsNotificatorFactory, TechnicianWarrantsNotificatorFactory>();
            services.AddSingleton<WarrantUpdatedSubscription>();
            services.AddSingleton<WarrantAddedSubscription>();
            services.AddSingleton<WarrantRemovedSubscription>();

        }
    }
}
