using CarService.Client.Core.WPF.Services;
using CarService.Features.ShopInterface.Controls;
using CarService.Features.ShopInterface.Model;
using CarService.Features.ShopInterface.Services.Interactors;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;
using CarService.Client.Core.MVVM.Navigation;

namespace CarService.Features.ShopInterface.Views.Frontdesk.Maintenance.WarrantTypes
{
    public partial class WarrantTypesViewModel : ObservableObject, IViewModel<WarrantTypesView>
    {
        private readonly InteractorHelper interactorHelper;
        private readonly IWarrantTypeInteractor warrantTypeInteractor;
        private readonly IProcedureInteractor procedureInteractor;
        private readonly MessageDialogService messageDialogService;
        private ObservableCollection<WarrantType> warrantTypes = new ObservableCollection<WarrantType>();
        private ObservableCollection<Procedure> availableProcedures = new ObservableCollection<Procedure>();
        private WarrantType? selectedWarrantType;
        private bool isEditWarrantTypeVisible = false;


        public ObservableCollection<WarrantType> WarrantTypes { get => warrantTypes; set => SetProperty(ref warrantTypes, value); }
        public ObservableCollection<Procedure> AvailableProcedures { get => availableProcedures; set => SetProperty(ref availableProcedures, value); }
        public bool IsEditWarrantTypeVisible { get => isEditWarrantTypeVisible; set => SetProperty(ref isEditWarrantTypeVisible, value); }
        public Procedure? DraggedProcedure { get; set; }
        public IEnumerable<Procedure>? Procedures { get; private set; }
        public WarrantType? SelectedWarrantType
        {
            get => selectedWarrantType;
            set
            {
                if (selectedWarrantType?.HasChanged == true)
                {
                    if (messageDialogService.ShowDialog("Promjene nisu spremljene", "Želite li svejedno nastaviti?", "Da", "Otkaži"))
                    {
                        selectedWarrantType.RestoreSnapshot();
                    }
                    else
                    {
                        var originalValue = selectedWarrantType;
                        selectedWarrantType = value;
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            selectedWarrantType = originalValue;
                            OnPropertyChanged(nameof(SelectedWarrantType));
                        }), DispatcherPriority.ContextIdle, null);
                        return;
                    }
                }

                SetProperty(ref selectedWarrantType, value);
                IsEditWarrantTypeVisible = value != null;

                if (SelectedWarrantType != null)
                {
                    SelectedWarrantType.CreateSnapshot();
                }

                UpdateAvailableProcedures();
            }
        }

        public Func<bool>? OnNavigatedAway => () => messageDialogService.ShowDiscardChangesDialog(SelectedWarrantType);

        public WarrantTypesViewModel(IWarrantTypeInteractor warrantTypeInteractor,
            IProcedureInteractor procedureInteractor,
            InteractorHelper interactorHelper,
            MessageDialogService messageDialogService)
        {
            this.warrantTypeInteractor = warrantTypeInteractor;
            this.procedureInteractor = procedureInteractor;
            this.interactorHelper = interactorHelper;
            this.messageDialogService = messageDialogService;
        }

        [RelayCommand]
        public async Task OnNavigatedTo()
        {
            await interactorHelper.Execute(async () =>
            {
                IEnumerable<WarrantType>? warrantTypes = await warrantTypeInteractor.GetWarrantTypes();
                WarrantTypes = new ObservableCollection<WarrantType>(warrantTypes ?? new List<WarrantType>());
                Procedures = await procedureInteractor.GetProcedures();
            });
        }

        [RelayCommand]
        public void DeleteStep(Step step)
        {
            SelectedWarrantType?.RemoveStep(step);

            UpdateAvailableProcedures();
        }

        [RelayCommand]
        public void ProcedureDragStarting(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ProcedurePreview procedurePreview = (ProcedurePreview)e.Source;
                DraggedProcedure = procedurePreview.Procedure;
                DragDrop.DoDragDrop(procedurePreview, procedurePreview.Procedure, DragDropEffects.Move);
            }
        }

        [RelayCommand]
        public void ProcedureDrop()
        {
            if (DraggedProcedure == null || SelectedWarrantType == null) return;

            SelectedWarrantType.AddStep(DraggedProcedure);

            UpdateAvailableProcedures();
        }

        private void UpdateAvailableProcedures()
        {
            AvailableProcedures = new ObservableCollection<Procedure>(Procedures?.Where(p => SelectedWarrantType?.Steps.Select(s => s.Procedure).Select(x => x.Id).Contains(p.Id) == false) ?? new List<Procedure>());
        }

        [RelayCommand]
        public async Task SaveWarrantType()
        {
            if (SelectedWarrantType == null) return;

            await interactorHelper.Execute(() => warrantTypeInteractor.UpdateWarrantType(SelectedWarrantType));

            SelectedWarrantType.CreateSnapshot();
        }

        [RelayCommand]
        public async Task AddNewWarrantType()
        {
            //string? warrantTypeName = await messageDialogService.ShowInputDialog("Nova vrsta naloga", "Upišite ime nove vrste naloga", "Ime nove vrste naloga");
            string? warrantTypeName = "Nova vrsta naloga";

            if (warrantTypeName == null) return;

            WarrantType? addedWarrantType = await interactorHelper.Execute<WarrantType>(() => warrantTypeInteractor.AddWarrantType(new WarrantType() { Name = warrantTypeName }));

            if (addedWarrantType != null)
            {
                WarrantTypes.Add(addedWarrantType);
            }
        }

        [RelayCommand]
        public async Task DeleteWarrantType()
        {
            if (SelectedWarrantType == null) return;

            WarrantType? deletedWarrantType = await interactorHelper.Execute(() => warrantTypeInteractor.DeleteWarrantType(SelectedWarrantType));

            if (deletedWarrantType != null) WarrantTypes.Remove(deletedWarrantType);
        }
    }
}
