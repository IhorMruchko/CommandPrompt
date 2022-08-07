using System;
using System.Collections.Generic;

namespace CommandPrompt.Arguments
{
    public abstract class ArgumentBase
    {
        public string Name { get; internal set; }

        public object Value { get; internal set; }

        public bool IsValid { get; internal set; }

        public abstract bool Parse(ref int i, ref List<string> args);

        public virtual bool IsCalled(string v)
        {
            return Name.Equals(v.TrimStart('-').Split('=')[0]);
        }
    }
}
