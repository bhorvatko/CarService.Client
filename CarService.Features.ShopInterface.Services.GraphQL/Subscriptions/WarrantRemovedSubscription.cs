using CarService.Client.ApiInteraction.GraphQL.QueryBuilder;
using CarService.Client.ApiInteraction.GraphQL.Subscriptions;
using CarService.Client.ApiInteraction.GraphQL;
using CarService.Client.Core.ErrorHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarService.Features.ShopInterface.Services.GraphQL.Types.Events;

namespace CarService.Features.ShopInterface.Services.GraphQL.Subscriptions
{
    public class WarrantRemovedSubscription : Subscription<WarrantRemovedResponse, WarrantRemovedEventDto>
    {
        public WarrantRemovedSubscription(GraphQlClient qlClient, GraphQlQueryBuilder queryBuilder, IBackgroundErrorHandler backgroundErrorHandler) : base(qlClient, queryBuilder, backgroundErrorHandler)
        {
        }

        public override string SubscriptionName => "warrantRemoved";
    }

    public class WarrantRemovedResponse : ISubscriptionResponse<WarrantRemovedEventDto>
    {
        [JsonProperty(PropertyName = "warrantRemoved")]
        public WarrantRemovedEventDto Event { get; set; } = null!;
    }
}
