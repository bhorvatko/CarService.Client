using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder.FieldBuilder
{
    public interface IFieldBuilder
    {
        bool ValidForFieldType(Type type);
        string GetField(string propertyName, Type propertyType);
    }
}
