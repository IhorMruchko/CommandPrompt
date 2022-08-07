using CommandPrompt.Arguments;
using System;

namespace CommandPrompt.Builders.ArgumentBuilding
{
    public interface IArgumentCreator<TArgument>
    {
        IArgumentCreator<TArgument> Validator(Func<TArgument, bool> validator);
        
        Argument<TArgument> Build();
    }
}