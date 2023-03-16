using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CarService.Client.Core.MVVM.Navigation
{
    public interface IViewModel<TView> : IViewModel where TView : ContentControl
    {
    }

    public interface IViewModel
    {
        Func<bool>? OnNavigatedAway { get; }
    }
}
