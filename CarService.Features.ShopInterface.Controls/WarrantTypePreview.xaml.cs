using CarService.Features.ShopInterface.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarService.Features.ShopInterface.Controls
{
    /// <summary>
    /// Interaction logic for WarrantTypePreview.xaml
    /// </summary>
    public partial class WarrantTypePreview : UserControl
    {
        public WarrantType WarrantType
        {
            get { return (WarrantType)GetValue(WarrantTypeProperty); }
            set { SetValue(WarrantTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WarrantTypeProperty =
            DependencyProperty.Register(nameof(WarrantType), typeof(WarrantType), typeof(WarrantTypePreview));



        public WarrantTypePreview()
        {
            InitializeComponent();
        }
    }
}
