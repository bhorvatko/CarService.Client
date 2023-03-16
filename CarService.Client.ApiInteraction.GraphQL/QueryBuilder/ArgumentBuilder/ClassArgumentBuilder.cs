using CarService.Client.ApiInteraction.GraphQL.QueryBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder.ArgumentBuilder
{
    internal class ClassArgumentBuilder : ArgumentBuilder
    {
        private readonly ArgumentBuilderFactory argumentBuilderFactory;

        public ClassArgumentBuilder(ArgumentBuilderFactory argumentBuilderFactory)
        {
            this.argumentBuilderFactory = argumentBuilderFactory;
        }

        public override bool ValidForType(Type type) => type.GetProperties().Any() && !type.IsValueType;

        public override string GetArgumentValue(object propertyValue, Type propertyType)
        {
            string result = "{";

            result += propertyValue
                .GetType()
                .GetProperties()
                .Select(p => argumentBuilderFactory.GetArgumentBuilder(p.PropertyType).GetArgumentString(p.GetValue(propertyValue)!, p.Name, propertyType))
                .ConcatenateStrings((str1, str2) => str1 + ", " + str2);

            result += "}";

            return result;
        }
    }
}
