using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Types.Input
{
    public class AddWarrantInput
    {
        public DateTime Deadline { get; set; }
        public bool IsUrgent { get; set; }
        public string Subject { get; set; } = string.Empty;
        public int WarrantTypeId { get; set; }

        public AddWarrantInput(Warrant warrant)
        {
            if (warrant.WarrantType == null) throw new InvalidOperationException("Warrant must have a defined warrant type.");

            Deadline = warrant.DeadLine;
            IsUrgent = warrant.IsUrgent;
            Subject = warrant.Subject;
            WarrantTypeId = warrant.WarrantType.Id;
        }
    }
}
