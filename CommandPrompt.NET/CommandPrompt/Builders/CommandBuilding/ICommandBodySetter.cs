using CommandPrompt.Arguments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommandPrompt.Builders.CommandBuilding
{
    public interface ICommandBodySetter : IExecutableSetter
    {
        IExecutableSetter Body(Action<List<ArgumentBase>, List<ArgumentBase>> commandBody);

        IExecutableSetter Body(Func<List<ArgumentBase>, List<ArgumentBase>, Task> commandBody);
    }
}