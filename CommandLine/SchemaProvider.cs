using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CommandLine
{
    public static class SchemaProvider
    {
        private static readonly List<Parameter> SupportedParameters = new()
        {
            new Parameter('#', s => int.Parse(s), null),
            new Parameter('*', s => s, null),
            new Parameter('+', s => float.Parse(s), null),
            new Parameter('$', IPAddress.Parse, null),
            new Parameter('%', s => DateTime.Parse(s), null),
            new Parameter('?', _ => true, false),
            new Parameter('&', Convert.FromBase64String, Array.Empty<byte>())
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