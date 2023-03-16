using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Input
{
    public class AddTechnicianInput
    {
        public string Name { get; set; } = string.Empty;

        public AddTechnicianInput(string name)
        {
            Name = name;
        }
    }
}
