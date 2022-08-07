using CommandPrompt.Arguments;

namespace CommandPrompt.Builders.ArgumentBuilding
{
    public class OptionalArgumentBuilder<TArgument> : ArgumentBuilder<TArgument>
    {
        public OptionalArgumentBuilder()
        {
            argument = new OptionalArgument<TArgument>();
        }
    }
}
