using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Input
{
    public class AdvanceWarrantToNextStepInput
    {
        public int Id { get; set; }

        public AdvanceWarrantToNextStepInput(Warrant warrant)
        {
            Id = warrant.Id;
        }
    }
}
