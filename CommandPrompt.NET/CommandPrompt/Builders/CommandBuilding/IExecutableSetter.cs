using CommandPrompt.Builders.OverloadBuilding;
using CommandPrompt.Executable;
using System;

namespace CommandPrompt.Builders.CommandBuilding
{
    public interface IExecutableSetter
    {
        ICommandExecutionSetter AddOverload(Overload overload);

        ICommandExecutionSetter AddOverload(Func<IOverloadNameSetter, Overload> overloadBuilder);

        ICommandExecutionSetter AddInner(Command innerCommand);

        ICommandExecutionSetter AddInner(Func<ICommandNameSetter, Command> commandBuilder);
    }
}