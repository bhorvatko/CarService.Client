using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder.ArgumentBuilder
{
    internal class StringArgumentBuilder : ArgumentBuilder
    {
        public override bool ValidForType(Type type) => type == typeof(string);

        public override string GetArgumentValue(object propertyValue, Type propertyType) => $"\"{propertyValue}\"";
    }
}
