using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Dto
{
    public class WarrantTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<StepDto> Steps { get; set; } = new List<StepDto>();

        public WarrantType MapToDomain()
        {
            return new WarrantType
            {
                Id = Id,
                Name = Name,
                Steps = new ObservableCollection<Step>(Steps.Select(s => s.MapToDomain()))
            };
        }
    }
}
