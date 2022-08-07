using CommandPrompt.Arguments;
using CommandPrompt.Builders.ArgumentBuilding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommandPrompt.Builders.OverloadBuilding
{
    public interface IOverloadSetter
    {
        IOverloadSetter AddRequiredArgument<TArgument>(RequiredArgument<TArgument> argument);

        IOverloadSetter AddRequiredArgument<TArgument>(Func<IArgumentNameSetter<TArgument>, Argument<TArgument>> argumentBuilder);

        IOverloadSetter AddOptionalArgument<TArgument>(OptionalArgument<TArgument> argument);

        IOverloadSetter AddOptionalArgument<TArgument>(Func<OptionalArgumentBuilder<TArgument>, Argument<TArgument>> argumentBuilder);

        IOverloadCreator Body(Action<List<ArgumentBase>, List<ArgumentBase>> body);

        IOverloadCreator Body(Func<List<ArgumentBase>, List<ArgumentBase>, Task> body);
    }
}
