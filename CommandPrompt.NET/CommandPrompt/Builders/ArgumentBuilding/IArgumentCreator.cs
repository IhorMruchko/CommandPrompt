using CommandPrompt.Arguments;
using CommandPrompt.Validators;
using System;

namespace CommandPrompt.Builders.ArgumentBuilding
{
    public interface IArgumentCreator<TArgument> : IArgumentOptionalsSetter<TArgument>
    {
        Argument<TArgument> Build();
    }
}