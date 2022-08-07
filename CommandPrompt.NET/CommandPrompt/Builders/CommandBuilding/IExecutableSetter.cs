using CommandPrompt.Builders.OverloadBuilding;
using CommandPrompt.Executable;
using System;

namespace CommandPrompt.Builders.CommandBuilding
{
    public interface IExecutableSetter : ICommandCreator
    {
        IExecutableSetter AddOverload(Overload overload);

        IExecutableSetter AddOverload(Func<IOverloadSetter, Overload> overloadBuilder);

        IExecutableSetter AddInner(Command innerCommand);

        IExecutableSetter AddInner(Func<ICommandNameSetter, Command> commandBuilder);
    }
}