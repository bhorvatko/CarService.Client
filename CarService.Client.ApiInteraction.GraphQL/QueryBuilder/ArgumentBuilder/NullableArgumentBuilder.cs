using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder.ArgumentBuilder
{
    internal class NullableArgumentBuilder : ArgumentBuilder
    {
        private readonly ArgumentBuilderFactory argumentBuilderFactory;

        public NullableArgumentBuilder(ArgumentBuilderFactory argumentBuilderFactory)
        {
            this.argumentBuilderFactory = argumentBuilderFactory;
        }

        public override bool ValidForType(Type type) => Nullable.GetUnderlyingType(type) != null;

        public override string GetArgumentValue(object propertyValue, Type propertyType)
        {
            Type underlyingType = Nullable.GetUnderlyingType(propertyType)!;

            return argumentBuilderFactory.GetArgumentBuilder(underlyingType).GetArgumentValue(propertyValue, propertyType);
        }
    }
}
