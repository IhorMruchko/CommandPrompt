using System;

namespace CommandPrompt.Validators
{
    public class RuleFor<TTarget> : Rule
    {
        internal Func<TTarget, bool> Rule { get; set; }

        public override bool IsFollowed (object value)
        {
            try
            {
                return value is TTarget target && Rule(target);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
