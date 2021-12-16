using System;
using System.Collections.Generic;
using System.Linq;

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

        public T[] GetArray<T>(string key)
        {
            var parameterKey = key.ToLowerInvariant();
            return _values[parameterKey] switch
            {
                null => default,
                object[] values => values.Cast<T>().ToArray(),
                _ => throw new InvalidOperationException(
                    $"Expected type of parameter {parameterKey} to be Object[] but was {_values[parameterKey].GetType()}")
            };
        }
    }
}