using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.Interactors
{
    public interface IWarrantInteractor
    {
        Task<Warrant> AddWarrant(Warrant warrant);
        Task<IEnumerable<Warrant>> GetUnassignedWarrants();
        Task<Warrant> RollbackWarrantToPreviousStep(Warrant warrant);
        Task<Warrant> AdvanceWarrantToNextStep(Warrant warrant);
        Task<Warrant> UpdateWarrant(Warrant warrant);
        Task<Warrant> AssignToTechnician(Warrant warrant, Technician technician);
        Task<Warrant> DeleteWarrant(Warrant warrant);
    }
}
