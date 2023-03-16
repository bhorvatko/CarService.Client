using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Input
{
    internal class DeleteInput
    {
        public int Id { get; set; }

        public DeleteInput(int id)
        {
            Id = id;
        }
    }
}
