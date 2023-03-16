using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder.ArgumentBuilder
{
    internal class PrimitiveArgumentBuilder : ArgumentBuilder
    {
        public override bool ValidForType(Type type) => type == typeof(int) || type == typeof(bool);

        public override string GetArgumentValue(object propertyValue, Type propertyType) => $"{propertyValue.ToString()!.ToLower()}";
    }
}
