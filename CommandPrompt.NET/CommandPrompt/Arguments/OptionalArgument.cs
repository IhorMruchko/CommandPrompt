using System;
using System.Collections.Generic;

namespace CommandPrompt.Arguments
{
    public class OptionalArgument<TArgument> : Argument<TArgument>
    {
        private delegate void ListChanger(ref int index, ref List<string> args);

        private readonly List<(Func<int, List<string>, bool> IsParsable, ListChanger Parse)> _parsers;

        public OptionalArgument()
        {
            _parsers = new List<(Func<int, List<string>, bool>, ListChanger)>()
            {
                (new Func<int, List<string>, bool>((i, args) => args[i].Contains("=") && TryValidate(args[i].Split('=')[1])), new ListChanger(ParseIfContains)),
                (new Func<int, List<string>, bool>((i, args) => args.Count - 2 > i && args[i + 1] == "=" && TryValidate(args[i + 2])), new ListChanger(ParseIfNext)),
                (new Func<int, List<string>, bool>((i, args) => args.Count - 1 > i && TryValidate(args[i+1])), new ListChanger(ParseIfWithout)),
            };
        }

        public override bool Equals(object obj)
        {
            return obj is OptionalArgument<TArgument> arg &&
                arg.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase);
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Name} of type {typeof(TArgument)} |opt";
        }

        public override bool Parse(ref int i, ref List<string> args)
        {
            foreach (var parser in _parsers)
            {
                if (parser.IsParsable(i, args))
                {
                    parser.Parse(ref i, ref args);
                    return true;
                }
            }

            return false;
        }

        public override ArgumentBase Copy()
        {
            return new OptionalArgument<TArgument>()
            {
                Name = Name,
                Converter = Converter?.Clone() as Converter<string, TArgument>,
                Validator = Validator?.Clone() as Func<TArgument, bool>
            };
        }

        private void ParseIfContains(ref int i, ref List<string> args)
        {
            args.RemoveAt(i);
            --i;
        }

        private void ParseIfNext(ref int i, ref List<string> args)
        {
            args.RemoveRange(i, 3);
            i -= 2;
        }

        private void ParseIfWithout(ref int i, ref List<string> args)
        {
            args.RemoveRange(i, 2);
            --i;
        }

    }
}
