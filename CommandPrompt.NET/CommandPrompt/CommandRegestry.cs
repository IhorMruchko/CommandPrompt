using CommandPrompt.Arguments;
using CommandPrompt.Builders.CommandBuilding;
using CommandPrompt.Executable;
using CommandPrompt.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandPrompt
{
    public class CommandRegestry
    {
        private readonly List<Command> _commands = new List<Command>();

        private static readonly CommandRegestry _regestry = new CommandRegestry();

        public static CommandRegestry Register => _regestry;

        private CommandRegestry() { }

        public CommandRegestry Add(Command command)
        {
            _regestry._commands.Add(command); 
            return _regestry;
        }

        public CommandRegestry Add(Func<ICommandNameSetter, Command> commandBuilder)
        {
            _regestry._commands.Add(commandBuilder.Invoke(new CommandBuilder()));
            return _regestry;
        }

        public static async Task Invoke(string[] args)
        {
            foreach(var command in _regestry._commands)
            {
                if (command.CheckIsCalled(args.ToList()))
                {
                    await command.Execute();
                    return;
                }
            }
        }

        internal static bool IsUnique(string name) 
            => _regestry._commands.Any(c => c.Name == name) == false;

        /// <summary>
        /// Gets first registered command copy.
        /// </summary>
        /// <param name="commandTitle">Title of registered command to copy.</param>
        /// <returns>Copy of registered command.</returns>
        public static Command GetCommand(string commandTitle) 
            => Commands.First(c => c.Name.Equals(commandTitle, StringComparison.CurrentCultureIgnoreCase))
                       .Copy();

        /// <summary>
        /// Get first registered overload copy.
        /// </summary>
        /// <param name="overloadTitle">Title of registered overload to copy.</param>
        /// <returns>Copy of registered overload.</returns>
        public static Overload GetOverload(string overloadTitle) 
            => Overloads.First(o => o.Name == overloadTitle)
                        .Copy();
        /// <summary>
        /// Get first registered argument copy.
        /// </summary>
        /// <param name="argumentName">Title of registered argument to copy.</param>
        /// <returns>Copy of registered argument.</returns>
        public static ArgumentBase GetArgument(string argumentName)
            => Arguments.First(arg => arg.IsCalled(argumentName))
                        .Copy();

        private static IEnumerable<Command> Commands => _regestry._commands
            .Concat(_regestry._commands.SelectMany(c => c.GetCommands()));
        
        private static IEnumerable<Overload> Overloads => Commands.SelectMany(c => c.Overloads);
        
        private static IEnumerable<ArgumentBase> Arguments => Overloads.SelectMany(t => t.RequiredArgs)
                                                                       .Concat(Overloads.SelectMany(t => t.OptionalArgs));
    }
}
