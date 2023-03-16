using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.Interactors
{
    public interface IProcedureInteractor
    {
        Task<IEnumerable<Procedure>> GetProcedures();
        Task<Procedure> SaveProcedure(Procedure procedure);
        Task<Procedure> CreateProcedure(Procedure procedure);
        Task<Procedure> DeleteProcedure(Procedure procedure);
    }
}
