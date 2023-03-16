using CarService.Client.Core.MVVM.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.Core.WPF.ViewModels
{
    public abstract partial class MainViewModelBase : ObservableObject, IMainViewModel
    {
        private readonly NavigationService navigationService;
        private bool loadingIndicatorVisible;
        private IViewModel? currentViewModel;

        protected NavigationService NavigationService => navigationService;
        public bool LoadingIndicatorVisible { get => loadingIndicatorVisible; set => SetProperty(ref loadingIndicatorVisible, value); }
        public IViewModel? CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                if (currentViewModel?.GetType() == value?.GetType()) return;
                SetProperty(ref currentViewModel, value);
            }
        }

        public MainViewModelBase(NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        [RelayCommand]
        public void Closing()
        {
            if (CurrentViewModel?.OnNavigatedAway?.Invoke() == true) return;
        }
    }
}
