using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Input
{
    public class AssignToTechnicianInput
    {
        public int WarrantId { get; set; }
        public int? TechnicianId { get; set; }


        public AssignToTechnicianInput(Warrant warrant, Technician technician)
        {
            WarrantId = warrant.Id;
            TechnicianId = technician.Id;
        }
    }
}
