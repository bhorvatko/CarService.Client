using CarService.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.Interactors
{
    public interface IWarrantTypeInteractor
    {
        Task<IEnumerable<WarrantType>> GetWarrantTypes();
        Task<WarrantType> UpdateWarrantType(WarrantType warrantType);
        Task<WarrantType> AddWarrantType(WarrantType warrantType);
        Task<WarrantType> DeleteWarrantType(WarrantType warrantType);
    }
}
