using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.Core.WPF.Controls.BackgroundError
{
    public partial class BackgroundErrorViewModel : ObservableObject
    {
        private readonly List<BackgroundError> errors = new List<BackgroundError>();

        public string? TextblockContent => errors.LastOrDefault() == null ? null : errors.LastOrDefault()?.Timestamp + "  |  " + errors.LastOrDefault()?.Text;
        public string? TooltipContent => errors.LastOrDefault()?.Text;

        public void AddError(string error)
        {
            if (errors.LastOrDefault()?.Text != error)
            {
                errors.Add(new BackgroundError(error));
                OnPropertyChanged(nameof(TooltipContent));
                OnPropertyChanged(nameof(TextblockContent));
            }
        }

        [RelayCommand]
        public void DismissCurrentError()
        {
            if (errors.Any())
            {
                errors.Remove(errors.Last());
                OnPropertyChanged(nameof(TooltipContent));
                OnPropertyChanged(nameof(TextblockContent));
            }
        }

    }

    public class BackgroundError
    {
        public string Text { get; set; }
        public string Timestamp { get; set; }

        public BackgroundError(string text)
        {
            Text = text;
            Timestamp = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
