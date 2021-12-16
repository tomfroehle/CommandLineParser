using System;
using System.Collections.Generic;

namespace CommandLine
{
    public class Args
    {
        private readonly Dictionary<string, object> _values;

        public Args(Dictionary<string, object> values)
        {
            _values = values;
        }

        public T Get<T>(string key)
        {
            var parameterKey = key.ToLowerInvariant();
            return _values[parameterKey] switch
            {
                null => default,
                T value => value,
                _ => throw new InvalidOperationException(
                    $"Expected type of parameter {parameterKey} to be {typeof(T)} but was {_values[parameterKey].GetType()}")
            };
        }
    }
}