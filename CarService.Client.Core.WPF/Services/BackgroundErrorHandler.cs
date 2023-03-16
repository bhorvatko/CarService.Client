using CarService.Client.Core.ErrorHandling;
using CarService.Client.Core.WPF.Controls.BackgroundError;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.Core.WPF.Services
{
    public class BackgroundErrorHandler : IBackgroundErrorHandler
    {
        private readonly BackgroundErrorViewModel backgroundErrorViewModel;
        private object lockObj = new object();

        public BackgroundErrorHandler(BackgroundErrorViewModel backgroundErrorViewModel)
        {
            this.backgroundErrorViewModel = backgroundErrorViewModel;
        }

        public void HandleBackgroundError(string errorMessage)
        {
            lock (lockObj) backgroundErrorViewModel.AddError(errorMessage);
        }
    }
}
