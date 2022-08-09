using CommandPrompt.Arguments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommandPrompt.Builders.CommandBuilding
{
    public interface ICommandBodySetter
    {
        ICommandExecutionSetter Body(Action<List<ArgumentBase>, List<ArgumentBase>> commandBody);

        ICommandExecutionSetter Body(Func<List<ArgumentBase>, List<ArgumentBase>, Task> commandBody);
    }
}