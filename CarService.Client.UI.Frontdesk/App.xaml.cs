using CarService.Client.Core.ErrorHandling;
using CarService.Client.Core.MVVM.Navigation;
using CarService.Client.Core.WPF.Services;
using CarService.Client.Core.WPF.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CarService.Client.Core.WPF.Controls.BackgroundError;
using CarService.Features.ShopInterface.Views.Frontdesk.Dashboard;
using CarService.Features.ShopInterface.Views.Frontdesk.Maintenance.Procedures;
using CarService.Features.ShopInterface.Views.Frontdesk.Maintenance.WarrantTypes;
using CarService.Features.ShopInterface.Views.Frontdesk.Maintenance.Technicians;
using CarService.Features.ShopInterface.Views.Frontdesk.CreateWarrant;
using CarService.Features.ShopInterface.Views.Frontdesk.EditWarrant;
using CarService.Features.ShopInterface.Registrations;

namespace CarService.Client.UI.Frontdesk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;
        private ServiceCollection services;

        public App()
        {
            services = new ServiceCollection();

            RegisterInfrastructure();
            RegisterFeatures();
            RegisterUi();

            serviceProvider = services.BuildServiceProvider();
        }

        private void RegisterInfrastructure()
        {
            services.AddCoreWpfServices();
            services.AddGraphQL(ConfigurationManager.AppSettings["GraphQlEndpoint"]!);
        }

        private void RegisterFeatures()
        {
            services.AddShopInterfaceFeature();
        }


        private void RegisterUi()
        {
            // Main page
            services.AddMainView<MainWindow, MainViewModel>();

            // Dasboard
            services.AddView<DashboardView, DashboardViewModel>();
            services.AddTransient<TechnicianDashboardViewModel>();
            services.AddTransient<TechnicianDashboardViewModelFactory>();
            services.AddSettingsProvider<DashboardSettingsProvider>();

            // Procedures
            services.AddView<ProceduresView, ProceduresViewModel>();

            // Warrant types
            services.AddView<WarrantTypesView, WarrantTypesViewModel>();

            // Technicians
            services.AddView<TechniciansView, TechniciansViewModel>();

            // Create warrant
            services.AddView<CreateWarrantView, CreateWarrantViewModel>();

            // Edit warrant
            services.AddView<EditWarrantView, EditWarrantViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow!.Show();
        }
    }
}
