using CarService.Client.Core.WPF.Services;
using CarService.Features.ShopInterface.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarService.Features.ShopInterface.Controls
{
    /// <summary>
    /// Interaction logic for EditWarrantControl.xaml
    /// </summary>
    public partial class EditWarrantControl : UserControl
    {
        public Note? SelectedNote { get; set; }

        public Warrant Warrant
        {
            get { return (Warrant)GetValue(WarrantProperty); }
            set { SetValue(WarrantProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WarrantProperty =
            DependencyProperty.Register(nameof(Warrant), typeof(Warrant), typeof(EditWarrantControl));



        public IEnumerable<WarrantType> AvailableWarrantTypes
        {
            get { return (IEnumerable<WarrantType>)GetValue(AvailableWarrantTypesProperty); }
            set { SetValue(AvailableWarrantTypesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AvailableWarrantTypesProperty =
            DependencyProperty.Register(nameof(AvailableWarrantTypes), typeof(IEnumerable<WarrantType>), typeof(EditWarrantControl));




        public bool ShowAllProperties
        {
            get { return (bool)GetValue(ShowAllPropertiesProperty); }
            set { SetValue(ShowAllPropertiesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowAllProperties.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowAllPropertiesProperty =
            DependencyProperty.Register("ShowAllProperties", typeof(bool), typeof(EditWarrantControl), new PropertyMetadata(false));




        public EditWarrantControl()
        {
            InitializeComponent();
        }

        [RelayCommand]
        public void AddNotes()
        {
            string? newNote = InputDialog.ShowInputDialog("Nova bilješka", "Upišite sadržaj bilješke", "");

            if (newNote != null) Warrant.Notes.Add(new Note() { Content = newNote });
        }

        [RelayCommand]
        public void EditNote()
        {
            if (SelectedNote == null) return;

            string? newContent = InputDialog.ShowInputDialog("Uređivanje bilješke", "Upišite sadržaj bilješke", SelectedNote.Content);

            if (newContent != null) SelectedNote.Content = newContent;
        }

        [RelayCommand]
        public void DeleteNote()
        {
            if (SelectedNote != null) Warrant.Notes.Remove(SelectedNote);
        }
    }
}
