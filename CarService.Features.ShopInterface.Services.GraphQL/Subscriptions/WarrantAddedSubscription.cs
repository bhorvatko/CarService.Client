using CarService.Client.ApiInteraction.GraphQL.QueryBuilder;
using CarService.Client.ApiInteraction.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarService.Client.ApiInteraction.GraphQL.Subscriptions;
using CarService.Features.ShopInterface.Services.GraphQL.Types.Events;
using CarService.Client.Core.ErrorHandling;

namespace CarService.Features.ShopInterface.Services.GraphQL.Subscriptions
{
    public class WarrantAddedSubscription : Subscription<WarrantAddedResponse, WarrantAddedEventDto>
    {
        public override string SubscriptionName => "warrantAdded";

        public WarrantAddedSubscription(GraphQlClient qlClient, GraphQlQueryBuilder queryBuilder, IBackgroundErrorHandler backgroundErrorHandler)
            : base(qlClient, queryBuilder, backgroundErrorHandler)
        {
        }
    }

    public class WarrantAddedResponse : ISubscriptionResponse<WarrantAddedEventDto>
    {
        [JsonProperty(PropertyName = "warrantAdded")]
        public WarrantAddedEventDto Event { get; set; } = null!;
    }
}
