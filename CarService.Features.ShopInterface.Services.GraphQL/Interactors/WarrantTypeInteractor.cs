using CarService.Client.ApiInteraction.GraphQL.QueryBuilder;
using CarService.Client.ApiInteraction.GraphQL;
using CarService.Features.ShopInterface.Model;
using CarService.Features.ShopInterface.Services.Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarService.Features.ShopInterface.Services.GraphQL.Types.Dto;
using CarService.Features.ShopInterface.Services.GraphQL.Types.Input;

namespace CarService.Features.ShopInterface.Services.GraphQL.Interactors
{
    public class WarrantTypeInteractor : IWarrantTypeInteractor
    {
        private readonly GraphQlClient qlClient;
        private readonly GraphQlQueryBuilder queryBuilder;

        public WarrantTypeInteractor(GraphQlClient qlClient, GraphQlQueryBuilder queryBuilder)
        {
            this.qlClient = qlClient;
            this.queryBuilder = queryBuilder;
        }

        public async Task<IEnumerable<WarrantType>> GetWarrantTypes()
        {
            string query = queryBuilder.BuildQuery<WarrantTypeDto>("allWarrantTypes");

            GetWarrantTypesResponse response = await qlClient.ExecuteQuery<GetWarrantTypesResponse>(query);

            return response.AllWarrantTypes.Select(wt => MapTransitions(wt.MapToDomain()));
        }

        public async Task<WarrantType> UpdateWarrantType(WarrantType warrantType)
        {
            string mutation = queryBuilder.BuildMutation<WarrantTypeDto>("updateWarrantType", new UpdateWarrantTypeInput(warrantType));

            return (await qlClient.ExecuteMutation<UpdateWarrantTypesResponse>(mutation)).UpdateWarrantType!.MapToDomain();
        }

        public async Task<WarrantType> AddWarrantType(WarrantType warrantType)
        {
            string mutation = queryBuilder.BuildMutation<WarrantTypeDto>("addWarrantType", new AddWarrantTypeInput(warrantType));

            AddWarrantTypeResponse response = await qlClient.ExecuteMutation<AddWarrantTypeResponse>(mutation);

            return MapTransitions(response.AddWarrantType.MapToDomain());
        }

        public async Task<WarrantType> DeleteWarrantType(WarrantType warrantType)
        {
            string mutation = queryBuilder.BuildMutation("deleteWarrantType", new DeleteInput(warrantType.Id));

            await qlClient.ExecuteMutation<DeleteWarrantTypeResponse>(mutation);

            return warrantType;
        }

        private WarrantType MapTransitions(WarrantType warrantType)
        {
            foreach (Step step in warrantType.Steps)
            {
                if (step.ForwardTransition != null)
                {
                    step.ForwardTransition.TargetStep = warrantType.Steps.SingleOrDefault(s => s.Id == step.ForwardTransition.TargetStepId);
                    step.ForwardTransition.SourceStep = warrantType.Steps.SingleOrDefault(s => s.Id == step.ForwardTransition.SourceStepId);
                }

                if (step.BackTransition != null)
                {
                    step.BackTransition.TargetStep = warrantType.Steps.SingleOrDefault(s => s.Id == step.BackTransition.TargetStepId);
                    step.BackTransition.SourceStep = warrantType.Steps.SingleOrDefault(s => s.Id == step.BackTransition.SourceStepId);
                }
            }

            return warrantType;
        }

        public class GetWarrantTypesResponse
        {
            public IEnumerable<WarrantTypeDto> AllWarrantTypes { get; set; } = new List<WarrantTypeDto>();
        }

        public class UpdateWarrantTypesResponse
        {
            public WarrantTypeDto? UpdateWarrantType { get; set; }
        }

        public class AddWarrantTypeResponse
        {
            public WarrantTypeDto AddWarrantType { get; set; } = null!;
        }

        public class DeleteWarrantTypeResponse
        {
            public int Id { get; set; }
        }
    }
}
