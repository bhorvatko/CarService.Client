using CarService.Client.ApiInteraction.GraphQL.QueryBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder.ArgumentBuilder
{
    public abstract class ArgumentBuilder
    {
        public abstract bool ValidForType(Type type);

        public abstract string GetArgumentValue(object propertyValue, Type propertyType);

        public string GetArgumentString(object? propertyValue, string propertyName, Type propertyType)
            => propertyName.ToCamelCase() + ": " + (propertyValue == null ? "null" : GetArgumentValue(propertyValue, propertyType));
    }
}
