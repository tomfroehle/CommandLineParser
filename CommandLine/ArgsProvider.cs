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
                var arg = args.Where(a => a.StartsWith($"-{parameterKey}")).ToList();

                if (arg.Count == 0)
                {
                    values.Add(parameterKey, parameter.Fallback);
                }
                else if (arg.Count == 1)
                {
                    var value = arg.Single().Replace($"-{parameterKey}", string.Empty);
                    var parsedValue = parameter.Parse(value);
                    values.Add(parameterKey, parsedValue);
                }
                else
                {
                    var parsedValues = arg.Select(s => s.Replace($"-{parameterKey}", string.Empty))
                        .Select(s => parameter.Parse(s)).ToArray();
                    values.Add(parameterKey, parsedValues);
                }
            }

            return new Args(values);
        }
    }
}