using CarService.Client.ApiInteraction.GraphQL.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Events
{
    public class WarrantRemovedEventDto : Event
    {
        public int WarrantId { get; set; }
        public int? TechnicianId { get; set; }
    }
}
