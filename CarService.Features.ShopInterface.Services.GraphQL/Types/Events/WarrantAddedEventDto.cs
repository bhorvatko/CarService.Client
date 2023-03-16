using CarService.Client.ApiInteraction.GraphQL.Subscriptions;
using CarService.Features.ShopInterface.Services.GraphQL.Types.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Events
{
    public class WarrantAddedEventDto : Event
    {
        public WarrantDto Warrant { get; set; } = null!;
        public int? TechnicianId { get; set; }
    }
}
