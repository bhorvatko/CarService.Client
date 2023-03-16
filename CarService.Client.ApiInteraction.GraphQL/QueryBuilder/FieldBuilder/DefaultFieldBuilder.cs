using CarService.Client.ApiInteraction.GraphQL.QueryBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder.FieldBuilder
{
    internal class DefaultFieldBuilder : IFieldBuilder
    {
        public bool ValidForFieldType(Type type) => true;

        public string GetField(string propertyName, Type propertyType) => propertyName.ToCamelCase();
    }
}
