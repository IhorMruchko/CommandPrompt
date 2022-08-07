namespace CommandPrompt.Builders.OverloadBuilding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommandPrompt.Arguments;
    using CommandPrompt.Builders.ArgumentBuilding;
    using CommandPrompt.Executable;

    public class OverloadBuilder : IOverloadSetter,
                                   IOverloadCreator
    {
        private readonly Overload _overload = new Overload();

        public IOverloadSetter AddRequiredArgument<TArgument>(RequiredArgument<TArgument> argument)
        {
            _overload.RequiredArgs.Add(argument);
            return this;
        }

        public IOverloadSetter AddRequiredArgument<TArgument>(
            Func<IArgumentNameSetter<TArgument>,
            Argument<TArgument>> argumentBuilder)
        {
            _overload.RequiredArgs.Add(argumentBuilder.Invoke(new RequiredArgumentBuilder<TArgument>()));
            return this;
        }

        public IOverloadSetter AddOptionalArgument<TArgument>(OptionalArgument<TArgument> argument)
        {
            _overload.OptionalArgs.Add(argument);
            return this;
        }

        public IOverloadSetter AddOptionalArgument<TArgument>(
            Func<OptionalArgumentBuilder<TArgument>,
            Argument<TArgument>> argumentBuilder)
        {
            _overload.OptionalArgs.Add(argumentBuilder.Invoke(new OptionalArgumentBuilder<TArgument>()));
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
