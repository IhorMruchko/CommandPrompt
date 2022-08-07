using System;

namespace CommandPrompt.Builders.ArgumentBuilding
{
    public interface IArgumentConverterSetter<TArgument>
    {
        IArgumentCreator<TArgument> Converter(Converter<string, TArgument> converter);
    }
}