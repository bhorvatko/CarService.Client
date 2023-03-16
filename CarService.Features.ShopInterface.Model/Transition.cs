using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Model
{
    public class Transition
    {
        private Step? sourceStep;
        private Step? targetStep;

        public int Id { get; set; }
        public int? SourceStepId { get; set; }
        public Step? SourceStep
        {
            get => sourceStep;
            set
            {
                sourceStep = value;
                SourceStepId = value?.Id;
            }
        }
        public int? TargetStepId { get; set; }
        public Step? TargetStep
        {
            get => targetStep;
            set
            {
                targetStep = value;
                TargetStepId = value?.Id;
            }
        }
    }
}
