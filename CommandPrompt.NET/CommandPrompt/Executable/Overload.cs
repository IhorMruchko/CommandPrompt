using CommandPrompt.Arguments;
using CommandPrompt.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandPrompt.Executable
{
    public class Overload
    {
        public string Name { get; internal set; } = string.Empty;
        
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
        }

        public bool CheckIsCalled(List<string> args)
        {
            args = ParseOptionalArguments(args);
            IsCalled = ParseRequiredArguments(args.Where(a => a.Trim() != string.Empty).ToList());
            return IsCalled;
        }

        /// <summary>
        /// Crete a copy of the overload with copied all agruments.
        /// </summary>
        /// <returns>Copy of the overload.</returns>
        public Overload Copy() => new Overload()
        {
            Name = Name,
            RequiredArgs = RequiredArgs.Select(req => req.Copy()).ToList(),
            OptionalArgs = OptionalArgs.Select(t => t.Copy()).ToList(),
            Body = Body?.Clone() as Action<List<ArgumentBase>, List<ArgumentBase>>,
            AsyncBody = AsyncBody?.Clone() as Func<List<ArgumentBase>, List<ArgumentBase>, Task>
        };

        /// <summary>
        /// Check is overload are equals by name and parameters equality.
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns>Is object is overload with same name and parameters.</returns>
        public override bool Equals(object obj) =>
            obj is Overload overload
            && overload.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase)
            && overload.RequiredArgs.SequenceEqual(RequiredArgs)
            && overload.OptionalArgs.SequenceEqual(OptionalArgs);

        /// <summary>
        /// Return hash code of argument based on <c>Name</c>, <c>RequiredArgs</c> and <c>OptionalArgs</c>.
        /// </summary>
        /// <remarks>
        /// To generate hash code <see href="https://stackoverflow.com/a/34006336">hash code combiner</see> based on seed and factor was used.
        /// </remarks>
        /// <returns>Hash code of the overload.</returns>
        public override int GetHashCode() => 
            new HashCode().Add(Name)
                          .AddMany(RequiredArgs)
                          .AddMany(OptionalArgs)
                          .ToHashCode();

        /// <summary>
        /// Convert overload to string.
        /// </summary>
        /// <returns>Information about the overload.</returns>
        public override string ToString() =>
            $"Required:\n\t{string.Join("\n\t", RequiredArgs)}\nOptional\n\t{string.Join("\n\t", OptionalArgs)}";

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
    }
}
