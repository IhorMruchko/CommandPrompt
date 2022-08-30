using CommandPrompt.Arguments;
using CommandPrompt.Converters;
using CommandPrompt.Converters.Default;
using CommandPrompt.Validators;
using System;

namespace CommandPrompt.Builders.ArgumentBuilding
{
    public abstract class ArgumentBuilder<TArgument> : IArgumentNameSetter<TArgument>,
                                                       IArgumentOptionalsSetter<TArgument>,
                                                       IArgumentCreator<TArgument>
    {
        protected Argument<TArgument> argument;
        /// <summary>
        /// Set name of the object.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IArgumentCreator<TArgument> Name(string name)
        {
            argument.Name = name;
            return this;
        }
        // TODO: Add exception on changing converter.
        public IArgumentCreator<TArgument> Converter(Converter<string, TArgument> converter)
        {
            argument.Converter = converter;
            return this;
        }

        public IArgumentCreator<TArgument> Validator(Validator<TArgument> validator)
        {
            argument.Validator = validator;
            return this;
        }

        public IArgumentCreator<TArgument> Validator(Func<TArgument, bool> validator)
        {
            argument.Validator = validator;
            return this;
        }

        public Argument<TArgument> Build()
        {
            if (argument.Converter is null)
            {
                argument.Converter = ConverterFactory.GetConverter<TArgument>() as CommonConverter<TArgument>;
            }
            return argument;
        }
    }
}
