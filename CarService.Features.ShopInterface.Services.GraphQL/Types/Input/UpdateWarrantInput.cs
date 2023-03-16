using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Input
{
    internal class UpdateWarrantInput
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public int WarrantTypeId { get; set; }
        public bool IsUrgent { get; set; }
        public int CurrentStepId { get; set; }
        public string Subject { get; set; }
        public IEnumerable<string> Notes { get; set; }

        public UpdateWarrantInput(Warrant warrant)
        {
            Id = warrant.Id;
            Deadline = warrant.DeadLine;
            WarrantTypeId = warrant.WarrantType.Id;
            IsUrgent = warrant.IsUrgent;
            CurrentStepId = warrant.CurrentStep.Id;
            Subject = warrant.Subject;
            Notes = warrant.Notes.Select(n => n.Content);
        }
    }
}
