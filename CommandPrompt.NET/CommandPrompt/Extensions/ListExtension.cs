using CommandPrompt.Arguments;
using System.Collections.Generic;

namespace CommandPrompt.Extensions
{
    public static class ListExtension
    {
        public static TValue ValueOf<TValue>(this List<ArgumentBase> container, string argName, TValue defaultValue = default)
        {
            var argument = container.Find(arg => arg.IsCalled(argName) && arg.Value is TValue && arg.IsValid);

            return argument is null
                ? defaultValue
                : (TValue)argument.Value;
        }
    }
}
