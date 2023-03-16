using CarService.Client.Core.WPF.Services;
using CarService.Features.ShopInterface.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CarService.Client.Core.MVVM.Navigation;
using CarService.Features.ShopInterface.Services.Interactors;
using CarService.Features.ShopInterface.Views.Frontdesk.EditWarrant;
using CarService.Features.ShopInterface.Views.Frontdesk.Dashboard;
using CarService.Features.ShopInterface.Controls;

namespace CarService.Features.ShopInterface.Views.Frontdesk.Dashboard
{
    public partial class TechnicianDashboardViewModel : ObservableObject
    {
        private readonly InteractorHelper interactorHelper;
        private readonly NavigationService navigationService;
        private readonly IWarrantInteractor warrantInteractor;
        private IEnumerable<Technician>? availableTechnicians;
        private Technician? selectedTechnician;
        private DashboardSettingsProvider settingsProvider;
        private TechnicianDashboardSettings? settings;
        private IEnumerable<ProcedureFilterViewModel>? filters;

        public IEnumerable<Technician>? AvailableTechnicians
        {
            get => availableTechnicians;
            set
            {
                SetProperty(ref availableTechnicians, value);

                if (availableTechnicians != null)
                {
                    SelectedTechnician = availableTechnicians.FirstOrDefault(t => t.Id == GetSettings().SelectedTechnicianId);
                }
            }
        }
        public IEnumerable<Procedure>? Procedures
        {
            set
            {
                if (value != null)
                {
                    Filters = value.Select(p => new ProcedureFilterViewModel(p, GetSettings().GetProcedureFilter(p.Id)));
                }
            }
        }
        public IEnumerable<ProcedureFilterViewModel>? Filters { get => filters; set => SetProperty(ref filters, value); }
        public Technician? SelectedTechnician
        {
            get => selectedTechnician;
            set
            {
                SetProperty(ref selectedTechnician, value);
                if (settings != null) settings.SelectedTechnicianId = value?.Id;
            }
        }
        public int VmIndex { get; set; }
        public Func<Warrant, Task> RollbackAction => Rollback;
        public Func<Warrant, Task> AdvanceAction => Advance;

        public TechnicianDashboardViewModel(
            InteractorHelper interactorHelper,
            NavigationService navigationService,
            IWarrantInteractor warrantInteractor,
            DashboardSettingsProvider settingsProvider)
        {
            this.interactorHelper = interactorHelper;
            this.navigationService = navigationService;
            this.warrantInteractor = warrantInteractor;
            this.settingsProvider = settingsProvider;
        }

        [RelayCommand]
        public void WarrantDragStart(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                WarrantPreview procedurePreview = (WarrantPreview)e.Source;
                DragDrop.DoDragDrop(procedurePreview, procedurePreview.Warrant, DragDropEffects.Move);
            }
        }

        [RelayCommand]
        public async Task WarrantDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Warrant)) && SelectedTechnician != null)
            {
                Warrant draggedWarrant = (Warrant)e.Data.GetData(typeof(Warrant));

                await AssignWarrant(draggedWarrant, SelectedTechnician);
            }
        }

        [RelayCommand]
        public void EditWarrant(Warrant warrant)
        {
            navigationService.NavigateToView<EditWarrantViewModel>(vm => vm.Warrant = warrant);
        }

        public async Task Rollback(Warrant warrant)
        {
            Warrant? updatedWarrant = await interactorHelper.Execute(() => warrantInteractor.RollbackWarrantToPreviousStep(warrant));

            if (updatedWarrant != null) warrant.CurrentStep = updatedWarrant.CurrentStep;
        }

        public async Task Advance(Warrant warrant)
        {
            Warrant? updatedWarrant = await interactorHelper.Execute(() => warrantInteractor.AdvanceWarrantToNextStep(warrant));

            if (updatedWarrant != null) warrant.CurrentStep = updatedWarrant.CurrentStep;
        }

        public async Task AssignWarrant(Warrant warrant, Technician technician)
        {
            Warrant? updatedWarrant = await interactorHelper.Execute(() => warrantInteractor.AssignToTechnician(warrant, technician));
        }

        private TechnicianDashboardSettings GetSettings()
        {
            return settings ??= settingsProvider.GetSettings().GetTechnicianDashboardSetting(VmIndex)!;
        }

        public class ProcedureFilterViewModel
        {
            public Procedure Procedure { get; private set; }
            public ProcedureFilter ProcedureFilter { get; private set; }

            public ProcedureFilterViewModel(Procedure procedure, ProcedureFilter procedureFilter)
            {
                Procedure = procedure;
                ProcedureFilter = procedureFilter;
            }
        }
    }
}
