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
                                  ICommandCreator,
                                  ICommandExecutionSetter,
                                  IOverloadsSetter
    {
        private readonly Command _command = new Command();

        /// <summary>
        /// Set name of the command.
        /// </summary>
        /// <param name="Name">Value of the command name.</param>
        /// <returns>CommandBuilder that allows adding overloads or inner commands and building object.</returns>
        public IOverloadsSetter Name(string Name)
        {
            _command.Name = Name;
            return this;
        }

        /// <summary>
        /// Set default overload without parameters.
        /// </summary>
        /// <param name="commandBody">Function that will be invoked on command execution.</param>
        /// <returns>CommandBuilder without settable `body`.</returns>
        public ICommandExecutionSetter Body(Action<List<ArgumentBase>, List<ArgumentBase>> commandBody)
        {
            AddOverload(new Overload() { Body = commandBody });
            return this;
        }

        /// <summary>
        /// Set default overload without parameters with async realisation.
        /// </summary>
        /// <param name="commandBody">Async function that will be invoked and awaited on command execution</param>
        /// <returns>CommandBuilder without settable `body`.</returns>
        public ICommandExecutionSetter Body(Func<List<ArgumentBase>, List<ArgumentBase>, Task> commandBody)
        {
            AddOverload(new Overload() { AsyncBody = commandBody });
            return this;
        }

        /// <summary>
        /// Add overload to the command.
        /// </summary>
        /// <param name="overload">Overload instance.</param>
        /// <returns>CommandBuilder that allows adding overloads or inner commands and building object.</returns>
        public ICommandExecutionSetter AddOverload(Overload overload)
        {
            _command.Overloads.Add(overload);
            return this;
        }
        /// <summary>
        /// Add overload to the command, using OverloadBuilder instructions.
        /// </summary>
        /// <param name="overloadBuilder">Builder that creates overload.</param>
        /// <returns>CommandBuilder that allows adding overload or inner commands and build object.</returns>
        public ICommandExecutionSetter AddOverload(Func<IOverloadSetter, Overload> overloadBuilder)
        {
            _command.Overloads.Add(overloadBuilder?.Invoke(new OverloadBuilder()));
            return this;
        }

        /// <summary>
        /// Add inner command to the configurable command.
        /// </summary>
        /// <param name="innerCommand">Instance of the inner command.</param>
        /// <returns>CommandBuilder that allows adding overload or inner commands and build object.</returns>
        public ICommandExecutionSetter AddInner(Command innerCommand)
        {
            _command.InnerComands.Add(innerCommand);
            return this;
        }

        /// <summary>
        /// Add inner command to the configurable command, using CommandBuilder instructions.
        /// </summary>
        /// <param name="commandBuilder">Builder that creates command.</param>
        /// <returns>CommandBuilder that allows adding overloads or inner commands and build object.</returns>
        public ICommandExecutionSetter AddInner(Func<ICommandNameSetter, Command> commandBuilder)
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
