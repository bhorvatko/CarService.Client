using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.Core.WPF.Services
{
    public class InteractorHelper
    {
        private readonly LoadingIndicatorService loadingIndicatorService;
        private readonly MessageDialogService messageDialogService;

        public InteractorHelper(LoadingIndicatorService loadingIndicatorService, MessageDialogService messageDialogService)
        {
            this.loadingIndicatorService = loadingIndicatorService;
            this.messageDialogService = messageDialogService;
        }

        public async Task Execute(Func<Task> interactorTask)
        {
            try
            {
                await loadingIndicatorService.ShowLoadingIndicatorForAction(interactorTask);
            }
            catch (Exception ex)
            {
                messageDialogService.ShowErrorMessage("Greška", ex.Message);
            }
        }

        public async Task<TResponse?> Execute<TResponse>(Func<Task<TResponse>> interactorTask) where TResponse : class
        {
            try
            {
                return await loadingIndicatorService.ShowLoadingIndicatorForTask(interactorTask);
            }
            catch (Exception ex)
            {
                messageDialogService.ShowErrorMessage("Greška", ex.Message);

                return null;
            }
        }
    }
}
