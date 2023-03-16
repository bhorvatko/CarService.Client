using CarService.Client.ApiInteraction.GraphQL.QueryBuilder;
using CarService.Client.ApiInteraction.GraphQL.Subscriptions;
using CarService.Client.ApiInteraction.GraphQL;
using CarService.Client.Core.ErrorHandling;
using CarService.Features.ShopInterface.Services.GraphQL.Types.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarService.Features.ShopInterface.Services.GraphQL.Types.Events;

namespace CarService.Features.ShopInterface.Services.GraphQL.Subscriptions
{
    public class WarrantUpdatedSubscription : Subscription<WarrantUpdatedResponse, WarrantUpdatedEventDto>
    {
        public WarrantUpdatedSubscription(GraphQlClient qlClient, GraphQlQueryBuilder queryBuilder, IBackgroundErrorHandler backgroundErrorHandler) : base(qlClient, queryBuilder, backgroundErrorHandler)
        {
        }

        public override string SubscriptionName => "warrantUpdated";
    }

    public class WarrantUpdatedResponse : ISubscriptionResponse<WarrantUpdatedEventDto>
    {
        [JsonProperty(PropertyName = "warrantUpdated")]
        public WarrantUpdatedEventDto Event { get; set; } = null!;
    }
}
