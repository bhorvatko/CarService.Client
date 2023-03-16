using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Dto
{
    public class TechnicianDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<WarrantDto> Warrants { get; set; } = new List<WarrantDto>();

        public Technician MapToDomain()
        {
            return new Technician
            {
                Id = Id,
                Name = Name,
                Warrants = new ObservableCollection<Warrant>(Warrants.Select(w => w.MapToDomain()))
            };
        }
    }
}
