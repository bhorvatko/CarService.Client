using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Dto
{
    public class StepDto
    {
        public int Id { get; set; }
        public ProcedureDto Procedure { get; set; } = null!;
        public TransitionDto? ForwardTransition { get; set; }
        public TransitionDto? BackTransition { get; set; }

        public Step MapToDomain()
        {
            return new Step()
            {
                Id = Id,
                Procedure = Procedure.MapToDomain(),
                ForwardTransition = ForwardTransition?.MapToDomain(),
                BackTransition = BackTransition?.MapToDomain()
            };
        }
    }
}
