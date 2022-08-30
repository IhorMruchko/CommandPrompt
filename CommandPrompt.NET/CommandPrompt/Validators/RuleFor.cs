using System;

namespace CommandPrompt.Validators
{
    public class RuleFor<TTarget> : Rule
    {
        internal Func<TTarget, string> MessageBuilder { get; set; }
        
        internal Func<TTarget, bool> Rule { get; set; }

        public override Rule Clone()
        {
            return new RuleFor<TTarget>()
            {
                Rule = Rule,
                Message = Message,
                IsInverted = IsInverted,
                MessageBuilder = MessageBuilder?.Clone() as Func<TTarget, string>
            };
        }

        public override string Exception(object value)
        {
            return MessageBuilder is null
                 ? Message
                 : MessageBuilder((TTarget)value);
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
