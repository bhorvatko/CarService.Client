using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Dto
{
    public class WarrantDto
    {
        public int Id { get; set; }
        public DateTime DeadLine { get; set; }
        public WarrantTypeDto WarrantType { get; set; } = null!;
        public StepDto CurrentStep { get; set; } = null!;
        public string Subject { get; set; } = string.Empty;
        public bool IsUrgent { get; set; }
        public IEnumerable<NoteDto> Notes { get; set; } = new List<NoteDto>();

        public Warrant MapToDomain()
        {
            Warrant warrant = new Warrant();
            MapToDomain(warrant);

            return warrant;
        }

        public void MapToDomain(Warrant warrant)
        {
            warrant.Id = Id;
            warrant.DeadLine = DeadLine;
            warrant.WarrantType = WarrantType.MapToDomain();
            warrant.CurrentStep = CurrentStep.MapToDomain();
            warrant.IsUrgent = IsUrgent;
            warrant.Subject = Subject;
            warrant.Notes = new ObservableCollection<Note>(Notes.Select(n => n.MapToDomain()));
        }
    }
}
