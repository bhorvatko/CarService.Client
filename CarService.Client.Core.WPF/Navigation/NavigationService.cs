using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.Core.MVVM.Navigation
{
    public class NavigationService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Action<IViewModel> setViewModelAction;
        private readonly Func<IViewModel> getCurrentViewModel;

        public NavigationService(IServiceProvider serviceProvider, Action<IViewModel> setViewModelAction, Func<IViewModel> getCurrentViewModel)
        {
            this.serviceProvider = serviceProvider;
            this.setViewModelAction = setViewModelAction;
            this.getCurrentViewModel = getCurrentViewModel;
        }

        public void NavigateToView<TViewModel>(Action<TViewModel>? vmMutation = null) where TViewModel : IViewModel
        {
            TViewModel vm = GetViewModel<TViewModel>();

            if (vmMutation != null) vmMutation.Invoke(vm);

            if (getCurrentViewModel.Invoke()?.OnNavigatedAway?.Invoke() == true) return;

            setViewModelAction.Invoke(vm);
        }

        private TViewModel GetViewModel<TViewModel>() where TViewModel : IViewModel
        {
            TViewModel? viewModel = serviceProvider.GetService<TViewModel>();

            if (viewModel == null) throw new InvalidOperationException($"No views of type {typeof(TViewModel).Name} registered to the DI container.");

            return viewModel;
        }
    }
}
