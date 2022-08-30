namespace CommandPrompt.Builders.OverloadBuilding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommandPrompt.Arguments;
    using CommandPrompt.Builders.ArgumentBuilding;
    using CommandPrompt.Converters;
    using CommandPrompt.Executable;

    public class OverloadBuilder : IOverloadNameSetter,
                                   IOverloadCreator
    {
        private readonly Overload _overload = new Overload();
        
        public IOverloadSetter Name(string overloadName)
        {
            _overload.Name = overloadName;
            return this;
        }
        
        public IOverloadSetter AddArgument<TArgument>(string argumentName)
        {
            _overload.RequiredArgs.Add(new RequiredArgument<TArgument>()
            {
                Name = argumentName,
            });
            return this;
        }

        public IOverloadSetter AddArgument<TArgument>(Argument<TArgument> argument)
        {
            _overload.RequiredArgs.Add(argument);
            return this;
        }

        public IOverloadSetter AddArgument<TArgument>(
            Func<IArgumentNameSetter<TArgument>,
            Argument<TArgument>> argumentBuilder)
        {
            _overload.RequiredArgs.Add(argumentBuilder.Invoke(new RequiredArgumentBuilder<TArgument>()));
            return this;
        }

        public IOverloadSetter AddOptArgument<TArgument>(string argumentName)
        {
            _overload.OptionalArgs.Add(new OptionalArgument<TArgument>()
            {
                Name = argumentName,
                Converter = ConverterFactory.GetConverter<TArgument>()
            });
            return this;
        }

        public IOverloadSetter AddOptArgument<TArgument>(Argument<TArgument> argument)
        {
            _overload.OptionalArgs.Add(argument);
            return this;
        }

        public IOverloadSetter AddOptArgument<TArgument>(
            Func<OptionalArgumentBuilder<TArgument>,
            Argument<TArgument>> argumentBuilder)
        {
            _overload.OptionalArgs.Add(argumentBuilder.Invoke(new OptionalArgumentBuilder<TArgument>()));
            return this;
        }

        public IOverloadCreator Body(Action body)
        {
            _overload.Body = (args, opt) => body();
            return this;
        }

        public IOverloadCreator Body(Func<Task> body)
        {
            if (_overload.Body is null == false)
            {
                return this;
            }
            _overload.AsyncBody = (args, opt) => body();
            return this;
        }

        public IOverloadCreator Body(Action<List<ArgumentBase>, List<ArgumentBase>> body)
        {
            _overload.Body = body;
            return this;
        }

        public IOverloadCreator Body(Func<List<ArgumentBase>, List<ArgumentBase>, Task> body)
        {
            if (_overload.Body is null == false)
            {
                return this;
            }
            _overload.AsyncBody = body;
            return this;
        }

        public Overload Build()
        {
            return _overload;
        }
    }
}
