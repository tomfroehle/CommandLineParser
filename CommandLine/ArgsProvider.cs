using System.Collections.Generic;
using System.Linq;

namespace CommandLine
{
    public static class ArgsProvider
    {
        public static Args Provide(string schemaDefinition, string[] args)
        {
            var schema = SchemaProvider.Provide(schemaDefinition);
            return GetArgs(args, schema);
        }

        private static Args GetArgs(string[] args, Dictionary<string, SchemaParameter> parameters)
        {
            var values = new Dictionary<string, object>();

            foreach (var (parameterKey, parameter) in parameters)
            {
                var arg = args.SingleOrDefault(a => a.StartsWith($"-{parameterKey}"));
                var value = arg?.Replace($"-{parameterKey}", string.Empty);
                var parsedValue = value is null ? parameter.Fallback : parameter.Parse(value);
                values.Add(parameterKey, parsedValue);
            }

            return new Args(values);
        }
    }
}