using System;

namespace CommandLine
{
    public record Parameter(char Symbol, Func<string, object> Parse, object Fallback);
}