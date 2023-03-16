using CarService.Client.Core.MVVM.Navigation;
using CarService.Client.Core.WPF.Services;
using CarService.Features.ShopInterface.Model;
using CarService.Features.ShopInterface.Services.Interactors;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Views.Frontdesk.CreateWarrant
{
    public partial class CreateWarrantViewModel : ObservableObject, IViewModel<CreateWarrantView>
    {
        private readonly InteractorHelper interactorHelper;
        private readonly IWarrantInteractor warrantInteractor;
        private readonly IWarrantTypeInteractor warrantTypeInteractor;
        private readonly MessageDialogService messageDialogService;
        private IEnumerable<WarrantType> availableWarrantTypes = new List<WarrantType>();

        public Warrant Warrant { get; private set; } = new Warrant() { DeadLine = DateTime.Now };
        public IEnumerable<WarrantType> AvailableWarrantTypes { get => availableWarrantTypes; set => SetProperty(ref availableWarrantTypes, value); }

        public Func<bool>? OnNavigatedAway => () => messageDialogService.ShowDiscardChangesDialog(Warrant);

        public CreateWarrantViewModel(
            InteractorHelper interactorHelper,
            IWarrantInteractor warrantInteractor,
            IWarrantTypeInteractor warrantTypeInteractor,
            MessageDialogService messageDialogService)
        {
            this.interactorHelper = interactorHelper;
            this.warrantInteractor = warrantInteractor;
            this.warrantTypeInteractor = warrantTypeInteractor;
            this.messageDialogService = messageDialogService;

            Warrant.CreateSnapshot();
        }

        [RelayCommand]
        public async Task LoadWarrantTypes()
        {
            IEnumerable<WarrantType>? warrantTypes = await interactorHelper.Execute(warrantTypeInteractor.GetWarrantTypes);

            if (warrantTypes != null) AvailableWarrantTypes = warrantTypes;
        }

        [RelayCommand]
        public async Task CreateWarrant()
        {
            await interactorHelper.Execute(() => warrantInteractor.AddWarrant(Warrant));
        }
    }
}
