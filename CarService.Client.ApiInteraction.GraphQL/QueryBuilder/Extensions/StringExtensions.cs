using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.GraphQL.QueryBuilder.Extensions
{
    internal static class StringExtensions
    {
        public static string ToCamelCase(this string input) => Char.ToLowerInvariant(input[0]) + input.Substring(1);

        public static string ConcatenateStrings(this IEnumerable<string> input, Func<string, string, string> concatenation)
        {
            string? result = input.FirstOrDefault();

            if (result == null) return string.Empty;

            foreach (string str in input.Skip(1))
            {
                result = concatenation(result, str);
            }

            return result;
        }
    }
}
