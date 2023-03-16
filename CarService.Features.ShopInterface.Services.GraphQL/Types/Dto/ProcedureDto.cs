using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Dto
{
    public class ProcedureDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public IEnumerable<string> UsedByWarrantTypes { get; set; } = new List<string>();

        public Procedure MapToDomain()
        {
            return new Procedure()
            {
                Id = Id,
                Name = Name,
                Color = ColorTranslator.FromHtml("#" + Color),
                UsedByWarrantTypes = UsedByWarrantTypes
            };
        }
    }
}
