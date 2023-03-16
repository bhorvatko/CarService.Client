using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarService.Client.Core.WPF.Services
{
    /// <summary>
    /// Interaction logic for InputDialog.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        public string Input { get; set; } = string.Empty;
        public string Caption { get; set; }
        public string? Result { get; set; }

        public InputDialog(string title, string caption, string initialInput)
        {
            Title = title;
            Caption = caption;
            Input = initialInput;

            InitializeComponent();
        }

        public static string? ShowInputDialog(string title, string caption, string initialInput)
        {
            InputDialog inputDialog = new InputDialog(title, caption, initialInput);

            inputDialog.ShowDialog();

            return inputDialog.Result;
        }

        [RelayCommand]
        public void Confirm()
        {
            Result = Input;
            Close();
        }
    }
}
