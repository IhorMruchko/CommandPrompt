using CommandPrompt.Builders.ArgumentBuilding;
using CommandPrompt.Builders.CommandBuilding;
using CommandPrompt.Builders.OverloadBuilding;

namespace CommandPrompt.Builders
{
    public static class Builder
    {
        public static ICommandNameSetter Command => new CommandBuilder();

        public static IOverloadNameSetter Overload => new OverloadBuilder();

        public static IArgumentNameSetter<TArgument> Required<TArgument>() 
            => new RequiredArgumentBuilder<TArgument>();

        public static IArgumentNameSetter<TArgument> Optional<TArgument>()
            => new OptionalArgumentBuilder<TArgument>();
    }
}
