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
    public class WarrantInteractor : IWarrantInteractor
    {
        private readonly GraphQlClient qlClient;
        private readonly GraphQlQueryBuilder queryBuilder;

        public WarrantInteractor(GraphQlClient qlClient, GraphQlQueryBuilder queryBuilder)
        {
            this.qlClient = qlClient;
            this.queryBuilder = queryBuilder;
        }

        public async Task<Warrant> AddWarrant(Warrant warrant)
        {
            string mutation = queryBuilder.BuildMutation<WarrantDto>("addWarrant", new AddWarrantInput(warrant));

            AddWarrantResponse response = await qlClient.ExecuteMutation<AddWarrantResponse>(mutation);

            return response.AddWarrant.MapToDomain();
        }

        public async Task<IEnumerable<Warrant>> GetUnassignedWarrants()
        {
            string query = queryBuilder.BuildQuery<WarrantDto>("unassignedWarrants");

            UnassignedWarrantsResponse response = await qlClient.ExecuteQuery<UnassignedWarrantsResponse>(query);

            return response.UnassignedWarrants.Select(w => w.MapToDomain());
        }

        public async Task<Warrant> RollbackWarrantToPreviousStep(Warrant warrant)
        {
            string mutation = queryBuilder.BuildMutation<WarrantDto>("rollbackWarrantToPreviousStep", new RollbackWarrantToPreviousStepInput(warrant));

            RollbackWarrantToPreviousStepResponse response = await qlClient.ExecuteMutation<RollbackWarrantToPreviousStepResponse>(mutation);

            return response.RollbackWarrantToPreviousStep.MapToDomain();
        }

        public async Task<Warrant> AdvanceWarrantToNextStep(Warrant warrant)
        {
            string mutation = queryBuilder.BuildMutation<WarrantDto>("advanceWarrantToNextStep", new AdvanceWarrantToNextStepInput(warrant));

            AdvanceWarrantToNextStepResponse response = await qlClient.ExecuteMutation<AdvanceWarrantToNextStepResponse>(mutation);

            return response.AdvanceWarrantToNextStep.MapToDomain();
        }

        public async Task<Warrant> AssignToTechnician(Warrant warrant, Technician technician)
        {
            string mutation = queryBuilder.BuildMutation<WarrantDto>("assignToTechnician", new AssignToTechnicianInput(warrant, technician));

            AssignToTechnicianResponse response = await qlClient.ExecuteMutation<AssignToTechnicianResponse>(mutation);

            return response.AssignToTechnician.MapToDomain();
        }

        public async Task<Warrant> UpdateWarrant(Warrant warrant)
        {
            string mutation = queryBuilder.BuildMutation<WarrantDto>("updateWarrant", new UpdateWarrantInput(warrant));

            UpdateWarrantResponse response = await qlClient.ExecuteMutation<UpdateWarrantResponse>(mutation);

            return response.UpdateWarrant.MapToDomain();
        }

        public async Task<Warrant> DeleteWarrant(Warrant warrant)
        {
            string mutation = queryBuilder.BuildMutation("deleteWarrant", new DeleteInput(warrant.Id));

            DeleteWarrantResponse response = await qlClient.ExecuteMutation<DeleteWarrantResponse>(mutation);

            return warrant;
        }

        public class AddWarrantResponse
        {
            public WarrantDto AddWarrant { get; set; } = null!;
        }

        public class UnassignedWarrantsResponse
        {
            public IEnumerable<WarrantDto> UnassignedWarrants { get; set; } = new List<WarrantDto>();
        }

        public class RollbackWarrantToPreviousStepResponse
        {
            public WarrantDto RollbackWarrantToPreviousStep { get; set; } = null!;
        }

        public class AdvanceWarrantToNextStepResponse
        {
            public WarrantDto AdvanceWarrantToNextStep { get; set; } = null!;
        }

        public class AssignToTechnicianResponse
        {
            public WarrantDto AssignToTechnician { get; set; } = null!;
        }

        public class UpdateWarrantResponse
        {
            public WarrantDto UpdateWarrant { get; set; } = null!;
        }

        public class DeleteWarrantResponse
        {
            public int Id { get; set; }
        }
    }
}
