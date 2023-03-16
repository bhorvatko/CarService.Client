using CarService.Client.Core.MVVM.Navigation;
using CarService.Client.Core.WPF.Services;
using CarService.Features.ShopInterface.Model;
using CarService.Features.ShopInterface.Services.Interactors;
using CarService.Features.ShopInterface.Views.Frontdesk.Dashboard;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Views.Frontdesk.EditWarrant
{
    public partial class EditWarrantViewModel : ObservableObject, IViewModel<EditWarrantView>
    {
        private readonly InteractorHelper interactorHelper;
        private readonly NavigationService navigationService;
        private readonly MessageDialogService messageDialogService;
        private readonly IWarrantTypeInteractor warrantTypeInteractor;
        private readonly IWarrantInteractor warrantInteractor;
        private ObservableCollection<WarrantType> availableWarrantTypes = new ObservableCollection<WarrantType>();
        private Warrant? warrant;

        public Warrant? Warrant
        {
            get => warrant;
            set
            {
                warrant = value;
                warrant?.CreateSnapshot();
            }
        }
        public ObservableCollection<WarrantType> AvailableWarrantTypes
        {
            get => availableWarrantTypes;
            set
            {
                if (value != null) SetProperty(ref availableWarrantTypes, value);
            }
        }

        public Func<bool>? OnNavigatedAway => () => messageDialogService.ShowDiscardChangesDialog(Warrant);

        public EditWarrantViewModel(InteractorHelper interactorHelper, IWarrantTypeInteractor warrantTypeInteractor, IWarrantInteractor warrantInteractor, NavigationService navigationService, MessageDialogService messageDialogService)
        {
            this.interactorHelper = interactorHelper;
            this.warrantTypeInteractor = warrantTypeInteractor;
            this.warrantInteractor = warrantInteractor;
            this.navigationService = navigationService;
            this.messageDialogService = messageDialogService;
        }

        [RelayCommand]
        public async Task LoadWarrantTypes()
        {
            IEnumerable<WarrantType>? warrantTypes = await interactorHelper.Execute(warrantTypeInteractor.GetWarrantTypes);

            if (warrantTypes != null) AvailableWarrantTypes = new ObservableCollection<WarrantType>(warrantTypes);
        }

        [RelayCommand]
        public async Task SaveWarrant()
        {
            if (Warrant == null) return;

            await interactorHelper.Execute(() => warrantInteractor.UpdateWarrant(Warrant));

            Warrant.CreateSnapshot();

            navigationService.NavigateToView<DashboardViewModel>();
        }

        [RelayCommand]
        public async Task DeleteWarrant()
        {
            if (Warrant == null) return;

            if (!messageDialogService.ShowDialog("Brisanje naloga", "Jeste li sigurni da želite obrisati nalog", "Da", "Odustani")) return;

            await interactorHelper.Execute(() => warrantInteractor.DeleteWarrant(Warrant));

            navigationService.NavigateToView<DashboardViewModel>();
        }
    }
}
