using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.Subscriptions
{
    public interface ISubscriptionResponse<TEvent>
    {
        TEvent Event { get; }
    }
}
