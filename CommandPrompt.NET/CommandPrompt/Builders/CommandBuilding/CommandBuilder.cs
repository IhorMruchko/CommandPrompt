using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandPrompt.Arguments;
using CommandPrompt.Builders.OverloadBuilding;
using CommandPrompt.Executable;

namespace CommandPrompt.Builders.CommandBuilding
{
    /// <summary>
    /// Creates new command step-by-step in declarative way.
    /// </summary>
    public class CommandBuilder : ICommandNameSetter,
                                  ICommandBodySetter,
                                  IExecutableSetter,
                                  ICommandCreator
    {
        private readonly Command _command = new Command();

        /// <summary>
        /// Set name of the command.
        /// </summary>
        /// <param name="Name">Value of the command name.</param>
        /// <returns>CommandBuilder that allows adding overloads or inner commands and building object.</returns>
        public ICommandBodySetter Name(string Name)
        {
            _command.Name = Name;
            return this;
        }

        public IExecutableSetter Body(Action<List<ArgumentBase>, List<ArgumentBase>> commandBody)
        {
            AddOverload(new Overload() { Body = commandBody });
            return this;
        }

        public IExecutableSetter Body(Func<List<ArgumentBase>, List<ArgumentBase>, Task> commandBody)
        {
            AddOverload(new Overload() { AsyncBody = commandBody });
            return this;
        }

        /// <summary>
        /// Add overload to the command.
        /// </summary>
        /// <param name="overload">Overload instance.</param>
        /// <returns>CommandBuilder that allows adding overloads or inner commands and building object.</returns>
        public IExecutableSetter AddOverload(Overload overload)
        {
            _command.Overloads.Add(overload);
            return this;
        }
        /// <summary>
        /// Add overload to the command, using OverloadBuilder instructions.
        /// </summary>
        /// <param name="overloadBuilder">Builder that creates overload.</param>
        /// <returns>CommandBuilder that allows adding overload or inner commands and build object.</returns>
        public IExecutableSetter AddOverload(Func<IOverloadSetter, Overload> overloadBuilder)
        {
            _command.Overloads.Add(overloadBuilder?.Invoke(new OverloadBuilder()));
            return this;
        }

        /// <summary>
        /// Add inner command to the configurable command.
        /// </summary>
        /// <param name="innerCommand">Instance of the inner command.</param>
        /// <returns>CommandBuilder that allows adding overload or inner commands and build object.</returns>
        public IExecutableSetter AddInner(Command innerCommand)
        {
            _command.InnerComands.Add(innerCommand);
            return this;
        }

        /// <summary>
        /// Add inner command to the configurable command, using CommandBuilder instructions.
        /// </summary>
        /// <param name="commandBuilder">Builder that creates command.</param>
        /// <returns>CommandBuilder that allows adding overloads or inner commands and build object.</returns>
        public IExecutableSetter AddInner(Func<ICommandNameSetter, Command> commandBuilder)
        {
            _command.InnerComands.Add(commandBuilder?.Invoke(new CommandBuilder()));
            return this;
        }

        /// <summary>
        /// Creates command.
        /// </summary>
        /// <returns>Configured command.</returns>
        public Command Build()
        {
            return _command;
        }
    }
}
