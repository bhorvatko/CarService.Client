using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.Core.MVVM.Navigation
{
    public interface IMainViewModel
    {
        bool LoadingIndicatorVisible { get; set; }
        IViewModel? CurrentViewModel { get; set; }
    }
}
