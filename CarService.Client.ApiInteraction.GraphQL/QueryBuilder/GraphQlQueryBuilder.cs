using CarService.Client.ApiInteraction.GraphQL.QueryBuilder.ArgumentBuilder;
using CarService.Client.ApiInteraction.GraphQL.QueryBuilder.Extensions;
using CarService.Client.ApiInteraction.GraphQL.QueryBuilder.FieldBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder
{
    /// <summary>
    /// Represents a class for building usable GraphQL queries by providing types and objects containing the
    /// response data and argument values.
    /// </summary>
    public class GraphQlQueryBuilder
    {
        private readonly FieldBuilderFactory fieldBuilderFactory = new FieldBuilderFactory();
        private readonly ArgumentBuilderFactory argumentBuilderFactory = new ArgumentBuilderFactory();

        /// <summary>
        /// Build a GraphQL query using the provided response type to create the query fields.
        /// </summary>
        /// <param name="queryName">Name of the query</param>
        /// <param name="responseType">Type containing the query field definitions</param>
        /// <returns>A usable GraphQL query string</returns>
        public string BuildQuery(string queryName, Type responseType)
            => Build("query", queryName, null, responseType);

        /// <summary>
        /// Build a GraphQL query using the provided response type to create the query fields,
        /// and using the argument object to populate the argument fields.
        /// </summary>
        /// <param name="queryName">Name of the query</param>
        /// <param name="argumentsObject">Object containing the argument field definitions and values</param>
        /// <param name="responseType">Type containing the query field definitions</param>
        /// <returns>A usable GraphQL query string</returns>
        public string BuildQuery(string queryName, object argumentsObject, Type responseType)
            => Build("query", queryName, argumentsObject, responseType);

        /// <summary>
        /// Build a GraphQL query using the provided response type to create the query fields.
        /// </summary>
        /// <typeparam name="TResponseType">Type containing the query field definitions</typeparam>
        /// <param name="queryName">Name of the query</param>
        /// <returns>A usable GraphQL query string</returns>
        public string BuildQuery<TResponseType>(string queryName)
            => BuildQuery(queryName, typeof(TResponseType));

        /// <summary>
        /// Build a GraphQL query using the provided response type to create the query fields,
        /// and using the argument object to populate the argument fields.        
        /// </summary>
        /// <typeparam name="TResponseType">Type containing the query field definitions</typeparam>
        /// <param name="queryName">Name of the query</param>
        /// <param name="argumentsObject">Object containing the argument field definitions and values</param>
        /// <returns>A usable GraphQL query string</returns>
        public string BuildQuery<TResponseType>(string queryName, object argumentsObject)
            => BuildQuery(queryName, argumentsObject, typeof(TResponseType));

        /// <summary>
        /// Builds a GraphQL mutation using the provided argument object to populate the argument fields.
        /// </summary>
        /// <param name="mutationName">Name of the mutation</param>
        /// <param name="argumentsObject">Object containing the argument field definitions and values</param>
        /// <returns>A usable GraphQL mutation string</returns>
        public string BuildMutation(string mutationName, object argumentsObject)
            => Build("mutation", mutationName, argumentsObject, null);

        /// <summary>
        /// Build a GraphQL mutation using the provided response type to create the query fields,
        /// and using the argument object to populate the argument fields.   
        /// </summary>
        /// <param name="mutationName">Name of the mutation</param>
        /// <param name="argumentsObject">Object containing the argument field definitions and values</param>
        /// <param name="responseType">Type containing the query field definitions</param>
        /// <returns>A usable GraphQL mutation string</returns>
        public string BuildMutation(string mutationName, object argumentsObject, Type responseType)
            => Build("mutation", mutationName, argumentsObject, responseType);

        /// <summary>
        /// Build a GraphQL mutation using the provided response type to create the query fields,
        /// and using the argument object to populate the argument fields.  
        /// </summary>
        /// <typeparam name="TResponseType">Type containing the query field definitions</typeparam>
        /// <param name="mutationName">Name of the mutation</param>
        /// <param name="argumentsObject">Object containing the argument field definitions and values</param>
        /// <returns>A usable GraphQL mutation string</returns>
        public string BuildMutation<TResponseType>(string mutationName, object argumentsObject)
            => BuildMutation(mutationName, argumentsObject, typeof(TResponseType));

        /// <summary>
        /// Build a GraphQL subscription using the provided response type to create the query fields.
        /// </summary>
        /// <param name="subscriptionName">Name of the subscription</param>
        /// <param name="responseType">Type containing the query field definitions</param>
        /// <returns>A usable GraphQL subscription string</returns>
        public string BuildSubscription(string subscriptionName, Type responseType)
            => Build("subscription", subscriptionName, null, responseType);

        /// <summary>
        /// Build a GraphQL subscription using the provided response type to create the query fields.
        /// </summary>
        /// <typeparam name="TResponseType">Type containing the query field definitions</typeparam>
        /// <param name="subscriptionName">Name of the subscription</param>
        /// <returns>A usable GraphQL subscription string</returns>
        public string BuildSubscription<TResponseType>(string subscriptionName)
            => BuildSubscription(subscriptionName, typeof(TResponseType));

        private string Build(string queryType, string name, object? argumentsObject, Type? responseType)
        {
            return $"{queryType} {{{name} {BuildArgumentsString(argumentsObject)} {BuildQueryFieldsString(responseType)}}}";
        }

        private string BuildQueryFieldsString(Type? responseType)
        {
            if (responseType == null) return string.Empty;

            string result = "{ ";

            result += responseType
                .GetProperties()
                .Select(p => fieldBuilderFactory.GetFieldBuilder(p.PropertyType).GetField(p.Name, p.PropertyType))
                .ConcatenateStrings((str1, str2) => str1 + ", " + str2);

            return result + " }";
        }

        private string BuildArgumentsString(object? argumentsObject)
        {
            if (argumentsObject == null) return string.Empty;

            string result = "(";

            result += argumentsObject
                .GetType()
                .GetProperties()
                .Select(p => argumentBuilderFactory.GetArgumentBuilder(p.PropertyType).GetArgumentString(p.GetValue(argumentsObject)!, p.Name, p.PropertyType))
                .ConcatenateStrings((str1, str2) => str1 + ", " + str2);

            return result + ")";
        }

    }
}
