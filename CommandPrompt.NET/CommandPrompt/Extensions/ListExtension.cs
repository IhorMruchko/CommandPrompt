using CommandPrompt.Arguments;
using CommandPrompt.Executable;
using System.Collections.Generic;
using System.Linq;

namespace CommandPrompt.Extensions
{
    public static class ListExtension
    {
        public static TValue ValueOf<TValue>(this List<ArgumentBase> container, string argName, TValue defaultValue = default)
        {
            var argument = container.Find(arg => arg.IsCalled(argName) && arg.Value is TValue);

            return argument is null
                ? defaultValue
                : (TValue)argument.Value;
        }

        public static IEnumerable<Command> GetCommands(this List<Command> container)
        {
            return container.SelectMany(c => c.InnerComands.GetCommands());
        }
    }
}
