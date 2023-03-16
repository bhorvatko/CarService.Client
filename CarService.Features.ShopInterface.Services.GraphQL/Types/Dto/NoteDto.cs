using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Dto
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; }

        public Note MapToDomain()
        {
            return new Note
            {
                Id = Id,
                Content = Content,
                Created = Created
            };
        }
    }
}
