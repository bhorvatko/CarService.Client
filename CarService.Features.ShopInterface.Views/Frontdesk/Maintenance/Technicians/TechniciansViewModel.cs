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

namespace CarService.Features.ShopInterface.Views.Frontdesk.Maintenance.Technicians
{
    public partial class TechniciansViewModel : ObservableObject, IViewModel<TechniciansView>
    {
        private readonly InteractorHelper interactorHelper;
        private readonly ITechnicianInteractor technicianInteractor;
        private readonly MessageDialogService messageDialogService;
        private ObservableCollection<Technician> technicians = new ObservableCollection<Technician>();
        private Technician? selectedTechnician;

        public ObservableCollection<Technician> Technicians { get => technicians; set => SetProperty(ref technicians, value); }
        public Technician? SelectedTechnician
        {
            get => selectedTechnician;
            set
            {
                if (selectedTechnician?.HasChanged == true)
                {
                    if (messageDialogService.ShowDialog("Promjene nisu spremljene", "Želite li svejedno nastaviti?", "Da", "Otkaži"))
                    {
                        selectedTechnician.RestoreSnapshot();
                    }
                    else
                    {
                        var originalValue = selectedTechnician;
                        selectedTechnician = value;
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            selectedTechnician = originalValue;
                            OnPropertyChanged(nameof(SelectedTechnician));
                        }), DispatcherPriority.ContextIdle, null);
                        return;
                    }
                }

                SetProperty(ref selectedTechnician, value);

                if (SelectedTechnician != null)
                {
                    SelectedTechnician.CreateSnapshot();
                }
            }
        }

        public Func<bool>? OnNavigatedAway => () => messageDialogService.ShowDiscardChangesDialog(SelectedTechnician);

        public TechniciansViewModel(InteractorHelper interactorHelper, ITechnicianInteractor technicianInteractor, MessageDialogService messageDialogService)
        {
            this.interactorHelper = interactorHelper;
            this.technicianInteractor = technicianInteractor;
            this.messageDialogService = messageDialogService;
        }

        [RelayCommand]
        public async Task LoadTechnicians()
        {
            IEnumerable<Technician>? technicians = await interactorHelper.Execute(technicianInteractor.GetTechnicians);

            if (technicians != null) Technicians = new ObservableCollection<Technician>(technicians);
        }

        [RelayCommand]
        public async Task AddNewTechnician()
        {
            //string? newName = await _messageDialogService.ShowInputDialog("Dodavanje novog tehničara", "Upišite ime novog tehničara", "Ime novoh tehničara");
            string? newName = "Novi tehničar";

            if (newName != null)
            {
                Technician? addedTechnician = await interactorHelper.Execute(() => technicianInteractor.AddTechnician(new Technician() { Name = newName }));

                if (addedTechnician != null) Technicians.Add(addedTechnician);
            }
        }

        [RelayCommand]
        public async Task DeleteTechnician()
        {
            if (SelectedTechnician == null) return;

            Technician? deletedTechnician = await interactorHelper.Execute(() => technicianInteractor.DeleteTechnician(SelectedTechnician));

            if (deletedTechnician != null)
            {
                Technicians.Remove(deletedTechnician);
                SelectedTechnician = null;
            }
        }

        [RelayCommand]
        public async Task UpdateTechnician()
        {
            if (SelectedTechnician == null) return;

            Technician? updatedTechnician = await interactorHelper.Execute(() => technicianInteractor.UpdateTechnician(SelectedTechnician));

            if (updatedTechnician != null) SelectedTechnician.CreateSnapshot();
        }
    }
}
