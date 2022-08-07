using CommandPrompt.Builders.CommandBuilding;
using CommandPrompt.Executable;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandPrompt
{
    public class CommandRegestry
    {
        private readonly List<Command> _commands = new List<Command>();

        private static readonly CommandRegestry _regestry = new CommandRegestry();

        public static CommandRegestry Register => _regestry;

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

        public static void Invoke(string[] args)
        {
            foreach(var command in _regestry._commands)
            {
                if (command.CheckIsCalled(args.ToList()))
                {
                    command.Execute();
                    return;
                }
            }
        }
    }
}
