using CarService.Client.ApiInteraction.GraphQL.QueryBuilder.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder.ArgumentBuilder
{
    internal class EnumerableArgumentBuilder : ArgumentBuilder
    {
        private readonly ArgumentBuilderFactory argumentBuilderFactory;

        public EnumerableArgumentBuilder(ArgumentBuilderFactory argumentBuilderFactory)
        {
            this.argumentBuilderFactory = argumentBuilderFactory;
        }

        public override bool ValidForType(Type type) => type.IsAssignableTo(typeof(IEnumerable));

        public override string GetArgumentValue(object propertyValue, Type propertyType)
        {
            string result = "[";

            result += ((IEnumerable)propertyValue)
                .Cast<object>()
                .Select(e => argumentBuilderFactory.GetArgumentBuilder(e.GetType()).GetArgumentValue(e, propertyType))
                .ConcatenateStrings((str1, str2) => str1 + ", " + str2);

            result += "]";

            return result;
        }
    }
}
