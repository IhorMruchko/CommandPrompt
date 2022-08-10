using CommandPrompt.Arguments;
using System;

namespace CommandPrompt.Builders.ArgumentBuilding
{
    public abstract class ArgumentBuilder<TArgument> : IArgumentNameSetter<TArgument>,
                                                       IArgumentConverterSetter<TArgument>,
                                                       IArgumentCreator<TArgument>
    {
        protected Argument<TArgument> argument;

        public IArgumentConverterSetter<TArgument> Name(string name)
        {
            argument.Name = name;
            return this;
        }

        public IArgumentCreator<TArgument> Converter(Converter<string, TArgument> converter)
        {
            argument.Converter = converter;
            return this;
        }

        public IArgumentCreator<TArgument> Validator(Func<TArgument, bool> validator)
        {
            argument.Validator = validator;
            return this;
        }

        public IArgumentCreator<TArgument> Message(string validationMessage)
        {
            argument.Validator.Message = validationMessage;
            return this;
        }

        public Argument<TArgument> Build()
        {
            return argument;
        }
    }
}
