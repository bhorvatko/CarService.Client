using CarService.Client.Core.MVVM.Navigation;
using CarService.Client.Core.WPF.Services;
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
using System.Windows;
using System.Windows.Threading;

namespace CarService.Features.ShopInterface.Views.Frontdesk.Maintenance.Procedures
{
    public partial class ProceduresViewModel : ObservableObject, IViewModel<ProceduresView>
    {
        private readonly IProcedureInteractor procedureInteractor;
        private readonly InteractorHelper interactorHelper;
        private readonly MessageDialogService messageDialogService;
        private Procedure? selectedProcedure;
        private ObservableCollection<Procedure> procedures = new ObservableCollection<Procedure>();
        private bool editProcedureVisible;

        public ObservableCollection<Procedure> Procedures { get => procedures; private set => SetProperty(ref procedures, value); }
        public bool EditProcedureVisibility { get => editProcedureVisible; private set => SetProperty(ref editProcedureVisible, value); }
        public Procedure? SelectedProcedure
        {
            get => selectedProcedure;
            set
            {
                if (this.selectedProcedure?.HasChanged == true)
                {
                    if (messageDialogService.ShowDialog("Promjene nisu spremljene", "Želite li svejedno nastaviti?", "Da", "Otkaži"))
                    {
                        this.selectedProcedure.RestoreSnapshot();
                    }
                    else
                    {
                        var originalValue = selectedProcedure;
                        selectedProcedure = value;
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            selectedProcedure = originalValue;
                            OnPropertyChanged(nameof(SelectedProcedure));
                        }), DispatcherPriority.ContextIdle, null);
                        return;
                    }
                }

                SetProperty(ref selectedProcedure, value);
                OnPropertyChanged(nameof(Procedures));

                EditProcedureVisibility = value != null;

                if (SelectedProcedure != null)
                {
                    SelectedProcedure.CreateSnapshot();
                }
            }
        }

        public Func<bool>? OnNavigatedAway => () => messageDialogService.ShowDiscardChangesDialog(SelectedProcedure);

        public ProceduresViewModel(IProcedureInteractor procedureInteractor,
            InteractorHelper interactorHelper,
            MessageDialogService displayAlertService)
        {
            this.procedureInteractor = procedureInteractor;
            this.interactorHelper = interactorHelper;
            this.messageDialogService = displayAlertService;
        }

        [RelayCommand]
        public async Task SaveProcedure()
        {
            if (SelectedProcedure == null || !SelectedProcedure.HasChanged) return;

            await interactorHelper.Execute(() => procedureInteractor.SaveProcedure(SelectedProcedure));

            SelectedProcedure.CreateSnapshot();
        }

        [RelayCommand]
        public async Task AddNewProcedure()
        {
            Procedure? newProcedure = await interactorHelper.Execute(() => procedureInteractor.CreateProcedure(new Procedure("Nova procedura")));

            if (newProcedure != null)
            {
                Procedures.Add(newProcedure);
            }
        }

        [RelayCommand]
        public async void DeleteProcedure()
        {
            if (SelectedProcedure == null) return;

            if (SelectedProcedure.UsedByWarrantTypes.Any())
            {
                string warrantTypeString = string.Empty;

                foreach (string warrantTypeName in SelectedProcedure.UsedByWarrantTypes)
                {
                    warrantTypeString += warrantTypeName + Environment.NewLine;
                }

                messageDialogService.ShowErrorMessage("Procedura se koristi", "Procedura se ne može obrisati jer se koristi u sljedećim vrstama radnih naloga: " + warrantTypeString);

                return;
            }

            Procedure? deletedProcedure = await interactorHelper.Execute(() => procedureInteractor.DeleteProcedure(SelectedProcedure));

            if (deletedProcedure != null)
            {
                Procedures.Remove(deletedProcedure);
            }
        }

        [RelayCommand]
        public async Task LoadProcedures()
        {
            IEnumerable<Procedure>? results = await interactorHelper.Execute(() => procedureInteractor.GetProcedures());
            Procedures = new ObservableCollection<Procedure>(results ?? new List<Procedure>());
        }
    }
}
