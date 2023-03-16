using CarService.Client.Core.MVVM.Navigation;
using CarService.Client.Core.WPF.Services;
using CarService.Features.ShopInterface.Model;
using CarService.Features.ShopInterface.Services.Interactors;
using CarService.Features.ShopInterface.Services.Notificators;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarService.Features.ShopInterface.Views.Frontdesk.Dashboard
{
    public partial class DashboardViewModel : ObservableObject, IViewModel<DashboardView>
    {
        private readonly InteractorHelper interactorHelper;
        private readonly ITechnicianInteractor technicianInteractor;
        private readonly IWarrantInteractor warrantInteractor;
        private readonly IProcedureInteractor procedureInteractor;
        private readonly ITechnicianWarrantsNotificatorFactory technicianWarrantsNotificatorFactory;
        private readonly List<ITechnicianWarrantsNotificator> technicianWarrantNotificators = new List<ITechnicianWarrantsNotificator>();
        private readonly DashboardSettingsProvider settingsProvider;
        private ObservableCollection<Technician> technicians = new ObservableCollection<Technician>();
        private ObservableCollection<Procedure> procedures = new ObservableCollection<Procedure>();

        public ObservableCollection<Technician> Technicians { get => technicians; set => SetProperty(ref technicians, value); }
        public ObservableCollection<TechnicianDashboardViewModel> TechnicianDashboardViewModels { get; set; }

        public Func<bool>? OnNavigatedAway => () =>
        {
            settingsProvider.SaveSettings();
            return false;
        };

        public DashboardViewModel(
            InteractorHelper interactorHelper,
            ITechnicianInteractor technicianInteractor,
            IWarrantInteractor warrantInteractor,
            IProcedureInteractor procedureInteractor,
            ITechnicianWarrantsNotificatorFactory technicianWarrantsNotificatorFactory,
            TechnicianDashboardViewModelFactory technicianDashboardViewModelFactory,
            DashboardSettingsProvider settingsProvider)
        {
            this.interactorHelper = interactorHelper;
            this.technicianInteractor = technicianInteractor;
            this.warrantInteractor = warrantInteractor;
            this.procedureInteractor = procedureInteractor;
            this.technicianWarrantsNotificatorFactory = technicianWarrantsNotificatorFactory;
            this.settingsProvider = settingsProvider;

            TechnicianDashboardViewModels = new ObservableCollection<TechnicianDashboardViewModel>(technicianDashboardViewModelFactory.Create(Technicians, 3));
        }

        [RelayCommand]
        public async Task LoadTechnicians()
        {
            IEnumerable<Technician>? loadedTechnicians = await interactorHelper.Execute(technicianInteractor.GetTechniciansWithWarrants);
            if (loadedTechnicians != null) Technicians = new ObservableCollection<Technician>(loadedTechnicians);

            Technician unassignedTechnician = new Technician() { Name = "< Nedodjeljeni >" };
            IEnumerable<Warrant>? unassignedWarrants = await interactorHelper.Execute(warrantInteractor.GetUnassignedWarrants);
            if (unassignedWarrants != null) unassignedTechnician.Warrants = new ObservableCollection<Warrant>(unassignedWarrants);
            Technicians.Add(unassignedTechnician);

            foreach (Technician technician in Technicians)
            {
                if (!technicianWarrantNotificators.Any(n => n.TechnicianId == technician.Id))
                {
                    ITechnicianWarrantsNotificator notificator = technicianWarrantsNotificatorFactory.CreateTechnicianWarrantsNotificator(technician.Id, updateAction =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            updateAction.Invoke(technician);
                        });
                    });

                    technicianWarrantNotificators.Add(notificator);
                }
            }

            IEnumerable<Procedure>? loadedProcedures = await interactorHelper.Execute(procedureInteractor.GetProcedures);
            if (loadedProcedures != null) procedures = new ObservableCollection<Procedure>(loadedProcedures);

            foreach (var vm in TechnicianDashboardViewModels)
            {
                vm.AvailableTechnicians = Technicians;
                vm.Procedures = procedures;
            }
        }
    }
}
