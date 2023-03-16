using CarService.Client.ApiInteraction.GraphQL.QueryBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder.FieldBuilder
{
    internal class LiteralFieldBuilder : IFieldBuilder
    {
        public bool ValidForFieldType(Type type)
            => type == typeof(string) || type == typeof(int) || type == typeof(DateTime) || type == typeof(bool);

        public string GetField(string propertyName, Type propertyType) => propertyName.ToCamelCase();
    }
}
