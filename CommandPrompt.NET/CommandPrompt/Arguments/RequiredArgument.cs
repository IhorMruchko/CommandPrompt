using System.Collections.Generic;

namespace CommandPrompt.Arguments
{
    public class RequiredArgument<TArgument> : Argument<TArgument>
    {
        public override bool Parse(ref int i, ref List<string> args)
        {
            return TryValidate(args[i]);
        }
    }
}
