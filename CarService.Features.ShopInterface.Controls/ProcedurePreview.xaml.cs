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
    /// Interaction logic for ProcedurePreview.xaml
    /// </summary>
    public partial class ProcedurePreview : UserControl
    {
        public Procedure Procedure
        {
            get => (Procedure)GetValue(ProcedureProperty);
            set
            {
                SetValue(ProcedureProperty, value);
            }
        }

        public static readonly DependencyProperty ProcedureProperty = DependencyProperty.Register(nameof(Procedure), typeof(Procedure), typeof(ProcedurePreview));




        public int CustomFontSize
        {
            get { return (int)GetValue(CustomFontSizeProperty); }
            set { SetValue(CustomFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CustomFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomFontSizeProperty =
            DependencyProperty.Register("CustomFontSize", typeof(int), typeof(ProcedurePreview), new PropertyMetadata(20));



        public ProcedurePreview()
        {
            InitializeComponent();
        }
    }
}
