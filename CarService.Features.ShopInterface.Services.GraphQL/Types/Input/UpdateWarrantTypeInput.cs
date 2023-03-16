using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Input
{
    public class UpdateWarrantTypeInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> ProcedureIds { get; set; }

        public UpdateWarrantTypeInput(WarrantType warrantType)
        {
            Id = warrantType.Id;
            Name = warrantType.Name;
            ProcedureIds = warrantType.Steps.Select(s => s.Procedure.Id);
        }
    }
}
