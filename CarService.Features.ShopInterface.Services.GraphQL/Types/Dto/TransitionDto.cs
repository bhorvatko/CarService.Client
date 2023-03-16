using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Dto
{
    public class TransitionDto
    {
        public int Id { get; set; }
        public int? SourceStepId { get; set; }
        public int? TargetStepId { get; set; }

        public Transition MapToDomain()
        {
            return new Transition
            {
                Id = Id,
                SourceStepId = SourceStepId,
                TargetStepId = TargetStepId
            };
        }
    }
}
