using System;

namespace CommandLine
{
    public record SchemaParameter(char Symbol, Func<string, object> Parse, object Fallback);
}