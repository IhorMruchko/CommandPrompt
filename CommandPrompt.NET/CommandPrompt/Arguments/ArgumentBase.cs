using System;
using System.Collections.Generic;

namespace CommandPrompt.Arguments
{
    public abstract class ArgumentBase
    {
        public string Name { get; internal set; }

        public abstract object Value { get; }

        public abstract bool Parse(ref int i, ref List<string> args);

        /// <summary>
        /// Create a copy of argument.
        /// </summary>
        /// <returns>Copy of the argument.</returns>
        public abstract ArgumentBase Copy();

        public virtual bool IsCalled(string v)
        {
            return Name.Equals(v.TrimStart('-').Split('=')[0], 
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
