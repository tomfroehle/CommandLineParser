using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CommandLine
{
    public static class SchemaProvider
    {
        private static readonly List<SchemaParameter> SupportedParameters = new()
        {
            new SchemaParameter('#', s => int.Parse(s), null),
            new SchemaParameter('*', s => s, null),
            new SchemaParameter('+', s => float.Parse(s), null),
            new SchemaParameter('$', IPAddress.Parse, null),
            new SchemaParameter('%', s => DateTime.Parse(s), null),
            new SchemaParameter('?', _ => true, false),
            new SchemaParameter('&', Convert.FromBase64String, Array.Empty<byte>())
        };

        private static readonly Dictionary<char, SchemaParameter> SymbolToParameterMap =
            SupportedParameters.ToDictionary(c => c.Symbol);

        public static Dictionary<string, SchemaParameter> Provide(string schemaDefinition)
        {
            return schemaDefinition.Split(',')
                .Select(s => (identifier: s[..^1], symbol: s[^1]))
                .ToDictionary(
                    schemaPart => schemaPart.identifier,
                    schemaPart => SymbolToParameterMap[schemaPart.symbol]
                );
        }
    }
}