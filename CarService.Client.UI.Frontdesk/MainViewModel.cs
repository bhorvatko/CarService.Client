using CarService.Client.Core.MVVM.Navigation;
using CarService.Client.Core.WPF.Controls.BackgroundError;
using CarService.Client.Core.WPF.ViewModels;
using CarService.Features.ShopInterface.Views.Frontdesk.CreateWarrant;
using CarService.Features.ShopInterface.Views.Frontdesk.Dashboard;
using CarService.Features.ShopInterface.Views.Frontdesk.Maintenance.Procedures;
using CarService.Features.ShopInterface.Views.Frontdesk.Maintenance.Technicians;
using CarService.Features.ShopInterface.Views.Frontdesk.Maintenance.WarrantTypes;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.UI.Frontdesk
{
    public partial class MainViewModel : MainViewModelBase
    {
        public BackgroundErrorViewModel BackgroundErrorViewModel { get; private set; }

        public MainViewModel(NavigationService navigationService, BackgroundErrorViewModel backgroundErrorViewModel) : base(navigationService)
        {
            BackgroundErrorViewModel = backgroundErrorViewModel;
        }

        [RelayCommand]
        public void NavigateToMainView()
        {
            NavigationService.NavigateToView<DashboardViewModel>();
        }

        [RelayCommand]
        public void NavigateToProceduresView()
        {
            NavigationService.NavigateToView<ProceduresViewModel>();
        }

        [RelayCommand]
        public void NavigateToWarrantTypesView()
        {
            NavigationService.NavigateToView<WarrantTypesViewModel>();
        }

        [RelayCommand]
        public void NavigateToTechniciansView()
        {
            NavigationService.NavigateToView<TechniciansViewModel>();
        }

        [RelayCommand]
        public void NavigateToCreateWarrantView()
        {
            NavigationService.NavigateToView<CreateWarrantViewModel>();
        }
    }
}
