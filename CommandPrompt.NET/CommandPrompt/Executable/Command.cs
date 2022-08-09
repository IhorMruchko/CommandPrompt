using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandPrompt.Executable
{
    public class Command
    {
        private CallingState _state = CallingState.NotCalled;

        public string Name { get; internal set; }

        public List<Overload> Overloads { get; internal set; } = new List<Overload>();

        public List<Command> InnerComands { get; internal set; } = new List<Command>();

        public bool IsCalled => _state != CallingState.NotCalled;

        public bool CheckIsCalled(List<string> args)
        {
            if (args.Count <= 0 ||
                args[0].Equals(Name, StringComparison.InvariantCultureIgnoreCase) == false)
            {
                return false;
            }

            if (Overloads.Any(o => o.CheckIsCalled(args.Skip(1).ToList())))
            {
                _state = CallingState.CalledOverload;
            }

            if (InnerComands.Any(i => i.CheckIsCalled(args.Skip(1).ToList())))
            {
                _state |= CallingState.CalledInnerCommand;
            }

            return IsCalled;
        }

        public async Task Execute()
        {
            if (_state == CallingState.CalledInnerCommand)
            {
                await InnerComands.FirstOrDefault(i => i.IsCalled)?.Execute();
            }

            if (_state == CallingState.CalledOverload)
            {
                await Overloads.FirstOrDefault(i => i.IsCalled)?.Invoke();
            }

            if (_state == (CallingState.CalledOverload | CallingState.CalledInnerCommand))
            {
                await InnerComands.FirstOrDefault(i => i.IsCalled)?.Execute();
            }
            _state = CallingState.NotCalled;
        }
    }
}
