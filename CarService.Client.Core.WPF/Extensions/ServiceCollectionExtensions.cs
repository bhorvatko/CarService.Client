using CarService.Client.Core.ErrorHandling;
using CarService.Client.Core.MVVM.Navigation;
using CarService.Client.Core.WPF.Controls.BackgroundError;
using CarService.Client.Core.WPF.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CarService.Client.Core.WPF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCoreWpfServices(this ServiceCollection services)
        {
            services.AddTransient<LoadingIndicatorService>();
            services.AddTransient<InteractorHelper>();
            services.AddTransient<MessageDialogService>();
            services.AddSingleton<IBackgroundErrorHandler, BackgroundErrorHandler>();
            services.AddSingleton<BackgroundErrorViewModel>();
            services.AddNavigation();
        }

        public static void AddView<TView, TViewModel>(this IServiceCollection services) 
            where TView : ContentControl
            where TViewModel : class, IViewModel<TView>
        {
            services.AddTransient<TView>();
            services.AddTransient<TViewModel>();
        }

        public static void AddMainView<TMainView, TMainViewModel>(this IServiceCollection services)
            where TMainViewModel : class, IMainViewModel
            where TMainView : class
        {
            services.AddSingleton<IMainViewModel, TMainViewModel>();
            services.AddSingleton<TMainView>();
        }

        private static void AddNavigation(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
                new NavigationService(sp, (vm) => sp.GetService<IMainViewModel>()!.CurrentViewModel = vm, () => sp.GetService<IMainViewModel>()!.CurrentViewModel!));
        }
    }
}
