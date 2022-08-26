using CommandPrompt.Arguments;
using CommandPrompt.Builders.ArgumentBuilding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommandPrompt.Builders.OverloadBuilding
{
    public interface IOverloadSetter
    {
        IOverloadSetter AddArgument<TArgument>(string argumentName);

        IOverloadSetter AddArgument<TArgument>(Argument<TArgument> argument);
       
        IOverloadSetter AddArgument<TArgument>(Func<IArgumentNameSetter<TArgument>, Argument<TArgument>> argumentBuilder);

        IOverloadSetter AddOptArgument<TArgument>(string argumentName);
       
        IOverloadSetter AddOptArgument<TArgument>(Argument<TArgument> argument);

        IOverloadSetter AddOptArgument<TArgument>(Func<OptionalArgumentBuilder<TArgument>, Argument<TArgument>> argumentBuilder);

        IOverloadCreator Body(Action body);

        IOverloadCreator Body(Func<Task> body);

        IOverloadCreator Body(Action<List<ArgumentBase>, List<ArgumentBase>> body);

        IOverloadCreator Body(Func<List<ArgumentBase>, List<ArgumentBase>, Task> body);
    }
}
