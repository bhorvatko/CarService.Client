using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Model
{
    public class Step
    {
        public int Id { get; set; }
        public Procedure Procedure { get; set; } = null!;
        public Transition? ForwardTransition { get; set; }
        public Transition? BackTransition { get; set; }

        public override bool Equals(object? obj)
        {
            return Id == (obj as Step)?.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
