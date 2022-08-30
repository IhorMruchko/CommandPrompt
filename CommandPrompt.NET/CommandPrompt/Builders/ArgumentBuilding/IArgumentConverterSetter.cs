using CommandPrompt.Validators;
using System;

namespace CommandPrompt.Builders.ArgumentBuilding
{
    public interface IArgumentOptionalsSetter<TArgument>
    {
        IArgumentCreator<TArgument> Converter(Converter<string, TArgument> converter);

        IArgumentCreator<TArgument> Validator(Validator<TArgument> validator);

        IArgumentCreator<TArgument> Validator(Func<TArgument, bool> validator);
    }
}