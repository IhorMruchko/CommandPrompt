using System;

namespace CommandPrompt.Validators
{
    public class RuleFor<TTarget> : Rule
    {
        internal Func<TTarget, bool> Rule { get; set; }

        public override Rule Clone()
        {
            return new RuleFor<TTarget>()
            {
                Rule = Rule,
                Message = Message
            };
        }

        public override bool IsFollowed (object value)
        {
            try
            {
                return value is TTarget target && (Rule(target) ^ IsInverted);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
