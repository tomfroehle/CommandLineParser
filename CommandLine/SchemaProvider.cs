using System.Collections.Generic;
using System.Linq;

namespace CommandLine
{
    public static class SchemaProvider
    {
        private static readonly List<Parameter> SupportedParameters = new()
        {
            new Parameter('#', s => int.Parse(s), null),
            new Parameter('?', _ => true, false),
            new Parameter('*', s => s, null)
        };

        private static readonly Dictionary<char, Parameter> SymbolToParameterMap =
            SupportedParameters.ToDictionary(c => c.Symbol);


        public static Dictionary<string, Parameter> Provide(string schemaDefinition)
        {
            return schemaDefinition.Split(',')
                .ToDictionary(
                    part => part[..^1],
                    part => SymbolToParameterMap[part[^1]]
                );
        }
    }
}