using CommandPrompt.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandPrompt.Executable
{
    public class Overload
    {
        public List<ArgumentBase> RequiredArgs { get; internal set; } = new List<ArgumentBase>();

        public List<ArgumentBase> OptionalArgs { get; internal set; } = new List<ArgumentBase>();

        public Action<List<ArgumentBase>, List<ArgumentBase>> Body { get; internal set; }

        public Func<List<ArgumentBase>, List<ArgumentBase>, Task> AsyncBody { get; internal set; }

        public bool IsCalled { get; internal set; }

        public bool IsAsync => AsyncBody is null == false && Body is null;

        public async Task Invoke()
        {
            if (IsAsync)
            {
                await AsyncBody?.Invoke(RequiredArgs, OptionalArgs);
            }
            else
            {
                Body?.Invoke(RequiredArgs, OptionalArgs);
            }
            RestoreInfo();
        }

        public bool CheckIsCalled(List<string> args)
        {
            args = ParseOptionalArguments(args);
            IsCalled = ParseRequiredArguments(args.Where(a => a.Trim() != string.Empty).ToList());
            return IsCalled;
        }

        public override string ToString()
        {
            return $"Required:\n\t{string.Join("\n\t", RequiredArgs)}\nOptional\n\t{string.Join("\n\t", OptionalArgs)}";

        }

        private bool ParseRequiredArguments(List<string> args)
        {
            if (RequiredArgs.Count != args.Count)
            {
                return false;
            }

            for (var i = 0; i < args.Count; ++i)
            {
                if (RequiredArgs[i].Parse(ref i, ref args) == false)
                {
                    return false;
                };
            }

            return true;
        }

        private List<string> ParseOptionalArguments(List<string> args)
        {
            for (var i = 0; i < args.Count; ++i)
            {
                if (args[i].StartsWith("-"))
                {
                    OptionalArgs.Find(arg => arg.IsCalled(args[i]))?.Parse(ref i, ref args);
                }
            }

            return args;
        }

        private void RestoreInfo()
        {
            IsCalled = false;
            foreach (var arg in RequiredArgs)
            {
                arg.IsValid = false;
            }

            foreach (var arg in OptionalArgs)
            {
                arg.IsValid = false;
            }
        }

    }
}
