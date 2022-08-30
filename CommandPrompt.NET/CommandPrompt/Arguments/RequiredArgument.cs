using CommandPrompt.Converters.Default;
using CommandPrompt.Validators;
using System;
using System.Collections.Generic;

namespace CommandPrompt.Arguments
{
    public class RequiredArgument<TArgument> : Argument<TArgument>
    {
        public override bool Parse(ref int i, ref List<string> args)
        {
            return TryValidate(args[i]);
        }
        
        public override ArgumentBase Copy()
        {
            return new RequiredArgument<TArgument>()
            {
                Name = Name,
                Converter = Converter?.Clone() as CommonConverter<TArgument>,
                Validator = Validator?.Clone()
            };
        }
    }
}
