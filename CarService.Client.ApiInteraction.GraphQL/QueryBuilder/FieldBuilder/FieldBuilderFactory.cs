using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder.FieldBuilder
{
    public class FieldBuilderFactory
    {
        private readonly List<IFieldBuilder> fieldBuilders = new List<IFieldBuilder>();

        public FieldBuilderFactory()
        {
            fieldBuilders.Add(new NullableFieldBuilder(this));
            fieldBuilders.Add(new LiteralFieldBuilder());
            fieldBuilders.Add(new EnumerableFieldBuilder(this));
            fieldBuilders.Add(new ClassFieldBuilder(this));
            fieldBuilders.Add(new DefaultFieldBuilder());
        }

        public IFieldBuilder GetFieldBuilder(Type fieldType)
        {
            IFieldBuilder? fieldBuilder = fieldBuilders.FirstOrDefault(w => w.ValidForFieldType(fieldType));

            if (fieldBuilder == null)
            {
                throw new InvalidOperationException($"The type {fieldType.Name} is not supported as a query field type");
            }
            else
            {
                return fieldBuilder;
            }
        }
    }
}
