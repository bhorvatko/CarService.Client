using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Input
{
    public class AddWarrantTypeInput
    {
        public string Name { get; set; }
        public IEnumerable<int> ProcedureIds { get; set; }

        public AddWarrantTypeInput(WarrantType warrantType)
        {
            Name = warrantType.Name;
            ProcedureIds = warrantType.Steps.Select(s => s.Procedure.Id);
        }
    }
}
