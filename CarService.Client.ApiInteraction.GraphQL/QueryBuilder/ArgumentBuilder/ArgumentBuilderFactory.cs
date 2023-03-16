using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder.ArgumentBuilder
{
    public class ArgumentBuilderFactory
    {
        private readonly List<ArgumentBuilder> argumentBuilders = new List<ArgumentBuilder>();

        public ArgumentBuilderFactory()
        {
            argumentBuilders.Add(new StringArgumentBuilder());
            argumentBuilders.Add(new PrimitiveArgumentBuilder());
            argumentBuilders.Add(new EnumerableArgumentBuilder(this));
            argumentBuilders.Add(new NullableArgumentBuilder(this));
            argumentBuilders.Add(new ClassArgumentBuilder(this));
            argumentBuilders.Add(new DefaultArgumentBuilder());
        }

        public ArgumentBuilder GetArgumentBuilder(Type fieldType)
        {
            ArgumentBuilder? argumentBuilder = argumentBuilders.FirstOrDefault(w => w.ValidForType(fieldType));

            if (argumentBuilder == null)
            {
                throw new InvalidOperationException($"The type {fieldType.Name} is not supported as an argument type");
            }
            else
            {
                return argumentBuilder;
            }
        }
    }
}
