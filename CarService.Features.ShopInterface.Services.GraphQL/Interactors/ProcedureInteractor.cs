using CarService.Client.ApiInteraction.GraphQL;
using CarService.Client.ApiInteraction.GraphQL.QueryBuilder;
using CarService.Features.ShopInterface.Model;
using CarService.Features.ShopInterface.Services.GraphQL.Types.Dto;
using CarService.Features.ShopInterface.Services.GraphQL.Types.Input;
using CarService.Features.ShopInterface.Services.Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.GraphQL.Interactors
{
    public class ProcedureInteractor : IProcedureInteractor
    {
        private readonly GraphQlClient qlClient;
        private readonly GraphQlQueryBuilder queryBuilder;

        public ProcedureInteractor(GraphQlClient qlClient, GraphQlQueryBuilder queryBuilder)
        {
            this.qlClient = qlClient;
            this.queryBuilder = queryBuilder;
        }

        public async Task<IEnumerable<Procedure>> GetProcedures()
        {
            string query = queryBuilder.BuildQuery<ProcedureDto>("allProcedures");

            GetProceduresResponse response = await qlClient.ExecuteQuery<GetProceduresResponse>(query);

            return response.AllProcedures.Select(p => p.MapToDomain());
        }

        public async Task<Procedure> SaveProcedure(Procedure procedure)
        {
            string mutation = queryBuilder.BuildMutation<ProcedureDto>("updateProcedure", new UpdateProcedureInput(procedure));

            UpdateProcedureResponse response = await qlClient.ExecuteMutation<UpdateProcedureResponse>(mutation);

            return response.UpdateProcedure!.MapToDomain();
        }

        public async Task<Procedure> CreateProcedure(Procedure procedure)
        {
            string mutation = queryBuilder.BuildMutation<ProcedureDto>("addProcedure", new CreateProcedureInput(procedure));

            return (await qlClient.ExecuteMutation<AddProcedureResponse>(mutation)).AddProcedure!.MapToDomain();
        }

        public async Task<Procedure> DeleteProcedure(Procedure procedure)
        {
            string query = queryBuilder.BuildMutation("deleteProcedure", new DeleteInput(procedure.Id));

            await qlClient.ExecuteMutation<DeleteProcedureResponse>(query);

            return procedure;
        }

        public class GetProceduresResponse
        {
            public List<ProcedureDto> AllProcedures { get; set; } = new List<ProcedureDto>();
        }

        public class UpdateProcedureResponse
        {
            public ProcedureDto? UpdateProcedure { get; set; }
        }

        public class AddProcedureResponse
        {
            public ProcedureDto? AddProcedure { get; set; }
        }

        public class DeleteProcedureResponse
        {
            public int Id { get; set; }
        }
    }
}
