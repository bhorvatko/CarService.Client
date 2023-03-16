using CarService.Client.ApiInteraction.GraphQL.QueryBuilder;
using CarService.Client.ApiInteraction.GraphQL;
using CarService.Features.ShopInterface.Model;
using CarService.Features.ShopInterface.Services.GraphQL.Types.Input;
using CarService.Features.ShopInterface.Services.Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarService.Features.ShopInterface.Services.GraphQL.Types.Dto;

namespace CarService.Features.ShopInterface.Services.GraphQL.Interactors
{
    public class TechnicianInteractor : ITechnicianInteractor
    {
        private readonly GraphQlClient qlClient;
        private readonly GraphQlQueryBuilder queryBuilder;

        public TechnicianInteractor(GraphQlClient qlClient, GraphQlQueryBuilder queryBuilder)
        {
            this.qlClient = qlClient;
            this.queryBuilder = queryBuilder;
        }

        public async Task<IEnumerable<Technician>> GetTechnicians()
        {
            string query = queryBuilder.BuildQuery<TechnicianDto>("allTechnicians", nameof(TechnicianDto.Warrants));

            AllTechniciansResponse response = await qlClient.ExecuteQuery<AllTechniciansResponse>(query);

            return response.AllTechnicians.Select(p => p.MapToDomain());

        }

        public async Task<IEnumerable<Technician>> GetTechniciansWithWarrants()
        {
            string query = queryBuilder.BuildQuery<TechnicianDto>("allTechnicians");

            AllTechniciansResponse response = await qlClient.ExecuteQuery<AllTechniciansResponse>(query);

            return response.AllTechnicians.Select(p => p.MapToDomain());

        }

        public async Task<Technician> AddTechnician(Technician technician)
        {
            string mutation = queryBuilder.BuildMutation<TechnicianDto>("addTechnician", new AddTechnicianInput(technician.Name));

            AddTechnicianResponse response = await qlClient.ExecuteMutation<AddTechnicianResponse>(mutation);

            return response.AddTechnician.MapToDomain();
        }

        public async Task<Technician> UpdateTechnician(Technician technician)
        {
            string mutation = queryBuilder.BuildMutation<TechnicianDto>("updateTechnician", new UpdateTechnicianInput(technician));

            UpdateTechnicianResponse response = await qlClient.ExecuteMutation<UpdateTechnicianResponse>(mutation);

            return response.UpdateTechnician.MapToDomain();
        }

        public async Task<Technician> DeleteTechnician(Technician technician)
        {
            if (!technician.Id.HasValue) throw new ArgumentException("Cannot delete default technician", nameof(technician));

            string mutation = queryBuilder.BuildMutation("deleteTechnician", new DeleteInput(technician.Id.Value));

            await qlClient.ExecuteMutation<DeleteTechnicianResponse>(mutation);

            return technician;
        }

        public class AllTechniciansResponse
        {
            public IEnumerable<TechnicianDto> AllTechnicians { get; set; } = new List<TechnicianDto>();
        }

        public class AddTechnicianResponse
        {
            public TechnicianDto AddTechnician { get; set; } = null!;
        }

        public class UpdateTechnicianResponse
        {
            public TechnicianDto UpdateTechnician { get; set; } = null!;
        }

        public class DeleteTechnicianResponse
        {
            public int Id { get; set; }
        }
    }
}
