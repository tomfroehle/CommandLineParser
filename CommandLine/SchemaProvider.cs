using System.Collections.Generic;
using System.Linq;

namespace CommandLine
{
    public static class SchemaProvider
    {
        private static readonly List<SchemaParameter> SupportedParameters = new()
        {
            new SchemaParameter('#', s => int.Parse(s), null),
            new SchemaParameter('?', _ => true, false),
            new SchemaParameter('*', s => s, null)
        };

        private static readonly Dictionary<char, SchemaParameter> SymbolToParameterMap =
            SupportedParameters.ToDictionary(c => c.Symbol);


        public static Dictionary<string, SchemaParameter> Provide(string schemaDefinition)
        {
            return schemaDefinition.Split(',')
                .ToDictionary(
                    part => part[..^1],
                    part => SymbolToParameterMap[part[^1]]
                );
        }
    }
}