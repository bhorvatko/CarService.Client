using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Input
{
    public class UpdateTechnicianInput
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UpdateTechnicianInput(Technician technician)
        {
            if (!technician.Id.HasValue) throw new ArgumentException("Cannot update default technicain");

            Id = technician.Id.Value;
            Name = technician.Name;
        }
    }
}
