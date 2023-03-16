using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.Interactors
{
    public interface ITechnicianInteractor
    {
        Task<IEnumerable<Technician>> GetTechnicians();
        Task<IEnumerable<Technician>> GetTechniciansWithWarrants();
        Task<Technician> AddTechnician(Technician technician);
        Task<Technician> DeleteTechnician(Technician technician);
        Task<Technician> UpdateTechnician(Technician technician);
    }
}
