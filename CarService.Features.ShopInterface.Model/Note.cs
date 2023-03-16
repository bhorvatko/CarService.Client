using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Model
{
    public class Note : ObservableObject
    {
        private string content = string.Empty;

        public int Id { get; set; }
        public string Content { get => content; set => SetProperty(ref content, value); }
        public DateTime Created { get; set; }
    }
}
