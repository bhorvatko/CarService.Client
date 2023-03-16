using CarService.Client.Core.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.Client.Core.WPF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSettingsProvider<TSettingsProvider>(this ServiceCollection services) 
            where TSettingsProvider : class, ISettingsProvider
        {
            services.AddSingleton<TSettingsProvider>();
        }
    }
}
