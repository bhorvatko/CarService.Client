using CarService.Client.ApiInteraction.GraphQL.QueryBuilder;
using CarService.Client.Core.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.Subscriptions
{
    public abstract class Subscription<TResponse, TEvent> where TResponse : ISubscriptionResponse<TEvent>
    {
        public event Action<TEvent>? OnEvent;

        public abstract string SubscriptionName { get; }

        private readonly IBackgroundErrorHandler backgroundErrorHandler;

        public Subscription(GraphQlClient qlClient, GraphQlQueryBuilder queryBuilder, IBackgroundErrorHandler backgroundErrorHandler)
        {
            this.backgroundErrorHandler = backgroundErrorHandler;

            string subscription = queryBuilder.BuildSubscription<TEvent>(SubscriptionName);

            qlClient.Subscribe<TResponse>(subscription, HandleEvent, HandleError);
        }

        public void HandleEvent(TResponse response)
        {
            OnEvent?.Invoke(response.Event);
        }

        public void HandleError(string errorMessage)
        {
            backgroundErrorHandler.HandleBackgroundError(errorMessage);
        }
    }
}
