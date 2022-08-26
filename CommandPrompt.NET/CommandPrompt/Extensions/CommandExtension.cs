using CommandPrompt.Executable;
using System.Collections.Generic;
using System.Linq;

namespace CommandPrompt.Extensions
{
    public static class CommandExtension
    {
        public static IEnumerable<Command> GetCommands(this Command command)
        {
            return command.InnerComands.Concat(command.InnerComands.GetCommands());
        }
    }
}
