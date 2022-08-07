using CommandPrompt.Arguments;

namespace CommandPrompt.Builders.ArgumentBuilding
{
    public class RequiredArgumentBuilder<TArgument> : ArgumentBuilder<TArgument>
    {
        public RequiredArgumentBuilder()
        {
            argument = new RequiredArgument<TArgument>();
        }
    }
}
