using CarService.Client.ApiInteraction.Session;
using GraphQL;
using GraphQL.Client.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL
{
    public class GraphQlClient
    {
        private readonly GraphQLHttpClient client;

        public GraphQlClient(GraphQLHttpClient client, SessionProvider sessionProvider)
        {
            this.client = client;

            if (!client.HttpClient.DefaultRequestHeaders.Contains("SessionId")) client.HttpClient.DefaultRequestHeaders.Add("SessionId", sessionProvider.Session.Id.ToString());
        }

        public async Task<TResponse> ExecuteQuery<TResponse>(string query)
        {
            return await Execute(query, (request) => client.SendQueryAsync<TResponse>(request));
        }

        public async Task<TResponse> ExecuteMutation<TResponse>(string mutation)
        {
            return await Execute(mutation, (request) => client.SendMutationAsync<TResponse>(request));
        }

        public void Subscribe<TResponse>(string subscriptionQuery, Action<TResponse> onSuccess, Action<string> onError)
        {
            GraphQLRequest request = new GraphQLRequest(subscriptionQuery);

            IObservable<GraphQLResponse<TResponse>> subscriptionStream = client.CreateSubscriptionStream<TResponse>(request, e => onError.Invoke(e.Message));

            var subscription = subscriptionStream.Subscribe(response =>
            {
                if (response.Errors?.Any() == true)
                {
                    onError.Invoke(GetFlattenedErrorMessage(response.Errors) + Environment.NewLine + "Subscription query: " + subscriptionQuery);
                }
                else
                {
                    onSuccess.Invoke(response.Data);
                }
            });
        }

        private async Task<TResponse> Execute<TResponse>(string requestContent, Func<GraphQLRequest, Task<GraphQLResponse<TResponse>>> func)
        {
            GraphQLRequest request = new GraphQLRequest(requestContent);

            try
            {
                GraphQLResponse<TResponse> response = await func.Invoke(request);

                if (response.Errors?.Any() == true)
                {
                    throw new Exception(GetFlattenedErrorMessage(response.Errors));
                }

                return response.Data;

            }
            catch (GraphQLHttpRequestException ex)
            {
                JObject json = JObject.Parse(ex.Content!);

                throw new Exception("GraphQL server internal error: " + json["errors"]![0]!["extensions"]!["message"]);
            }
        }

        private string GetFlattenedErrorMessage(IEnumerable<GraphQLError> errors)
        {
            string message = "GraphQL response contained errors: ";

            foreach (var error in errors)
            {
                string path = string.Empty;

                if (error.Path != null)
                {
                    foreach (string pathSegment in error.Path)
                    {
                        path += pathSegment + "/";
                    }
                }

                message += path + ": " + error.Message + Environment.NewLine;
            }

            return message;
        }
    }
}
