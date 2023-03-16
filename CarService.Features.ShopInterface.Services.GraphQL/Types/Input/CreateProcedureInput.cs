using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Input
{
    public class CreateProcedureInput
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public CreateProcedureInput(Procedure procedure)
        {
            Name = procedure.Name;
            Color = ColorTranslator.ToHtml(procedure.Color).Replace("#", "");
        }
    }
}
