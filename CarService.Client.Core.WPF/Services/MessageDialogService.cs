using CarService.Client.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarService.Client.Core.WPF.Services
{
    public class MessageDialogService
    {
        public bool ShowDialog(string title, string message, string accept, string cancel)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(message, title, System.Windows.MessageBoxButton.YesNo);

            return messageBoxResult == MessageBoxResult.Yes;
        }

        public void ShowErrorMessage(string title, string message)
        {
            System.Windows.MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool ShowDiscardChangesDialog(RestorableObject? obj)
        {
            if (obj?.HasChanged == true)
            {
                return !ShowDialog("Promjene nisu spremljene", "Promjene nisu spremljene. Želite li odbaciti promjene?", "Da", "Odustani");
            }
            else
            {
                return false;
            }
        }
    }
}
