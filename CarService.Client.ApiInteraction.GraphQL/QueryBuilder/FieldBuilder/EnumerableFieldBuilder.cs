using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder.FieldBuilder
{
    internal class EnumerableFieldBuilder : IFieldBuilder
    {
        private readonly FieldBuilderFactory fieldBuilderFactory;

        public EnumerableFieldBuilder(FieldBuilderFactory fieldBuilderFactory)
        {
            this.fieldBuilderFactory = fieldBuilderFactory;
        }

        public bool ValidForFieldType(Type type) => type.IsAssignableTo(typeof(IEnumerable));

        public string GetField(string propertyName, Type propertyType)
        {
            Type genericTypeParam = propertyType.GetGenericArguments().First();

            return fieldBuilderFactory.GetFieldBuilder(genericTypeParam).GetField(propertyName, genericTypeParam);
        }
    }
}
