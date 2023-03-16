using CarService.Client.Core.MVVM.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.Core.WPF.Services
{
    public class LoadingIndicatorService
    {
        private readonly IMainViewModel mainViewModel;

        public LoadingIndicatorService(IMainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public async Task ShowLoadingIndicatorForAction(Func<Task> task)
        {
            mainViewModel.LoadingIndicatorVisible = true;

            try
            {
                Task taskToExecute = task.Invoke();

                await taskToExecute;
            }
            finally
            {
                mainViewModel.LoadingIndicatorVisible = false;
            }
        }

        public async Task<TResponse> ShowLoadingIndicatorForTask<TResponse>(Func<Task<TResponse>> task)
        {
            mainViewModel.LoadingIndicatorVisible = true;

            try
            {
                TResponse response = await task.Invoke();
                return response;
            }
            finally
            {
                mainViewModel.LoadingIndicatorVisible = false;
            }
        }
    }
}
