using System;

namespace CommandPrompt.Validators
{
    public abstract class Rule
    {
        public string Message { get; internal set; }

        public bool IsInverted { get; internal set; }

        public abstract bool IsFollowed(object value);

        public abstract Rule Clone();
    }
}
