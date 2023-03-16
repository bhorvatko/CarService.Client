using CarService.Client.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Model
{
    public class Technician : RestorableObject
    {
        private string name = string.Empty;
        private ObservableCollection<Warrant> warrants = new ObservableCollection<Warrant>();

        public int? Id { get; set; }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public ICollection<Warrant> Warrants { get => warrants; set => SetProperty(ref warrants, new ObservableCollection<Warrant>(value)); }
    }
}
