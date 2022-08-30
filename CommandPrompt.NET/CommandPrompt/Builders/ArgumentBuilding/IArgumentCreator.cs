using CommandPrompt.Arguments;
using CommandPrompt.Validators;
using System;

namespace CommandPrompt.Builders.ArgumentBuilding
{
    public interface IArgumentCreator<TArgument>
    {
        IArgumentCreator<TArgument> Validator(Validator<TArgument> validator);

        Argument<TArgument> Build();
    }
}