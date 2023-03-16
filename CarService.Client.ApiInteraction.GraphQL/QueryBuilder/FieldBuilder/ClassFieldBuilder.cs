using CarService.Client.ApiInteraction.GraphQL.QueryBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder.FieldBuilder
{
    internal class ClassFieldBuilder : IFieldBuilder
    {
        private readonly FieldBuilderFactory fieldBuilderFactory;

        public ClassFieldBuilder(FieldBuilderFactory fieldBuilderFactory)
        {
            this.fieldBuilderFactory = fieldBuilderFactory;
        }

        public bool ValidForFieldType(Type type) => type.GetProperties().Any();

        public string GetField(string propertyName, Type propertyType)
        {
            string result = propertyName.ToCamelCase() + " {";

            result += propertyType
                .GetProperties()
                .Select(p => fieldBuilderFactory.GetFieldBuilder(p.PropertyType).GetField(p.Name, p.PropertyType))
                .ConcatenateStrings((str1, str2) => str1 + ", " + str2);

            result += "}";

            return result;
        }
    }
}
