using CarService.Client.ApiInteraction.GraphQL.QueryBuilder;
using CarService.Client.ApiInteraction.GraphQL;
using CarService.Client.ApiInteraction.Session;
using CarService.Client.Core.ErrorHandling;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.Client.Core.WPF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddGraphQL(this ServiceCollection services, string graphQlEndpoint)
        {
            services.AddSingleton<GraphQLHttpClientOptions>(sp => new GraphQLHttpClientOptions()
            {
                EndPoint = new Uri(graphQlEndpoint)
            });
            services.AddTransient<IGraphQLWebsocketJsonSerializer, NewtonsoftJsonSerializer>();
            services.AddSingleton<GraphQLHttpClient>();
            services.AddTransient<GraphQlClient>();
            services.AddTransient<GraphQlQueryBuilder>();
            services.AddSingleton<SessionProvider>();
        }
    }
}
