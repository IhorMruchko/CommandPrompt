using CommandPrompt.Executable;

namespace CommandPrompt.Test.CommandTests;

public partial class CommandTest
{
    private readonly Command _command = Builder.Command
        .Name("Hello")
        .Body(() => Console.Write("hello"))
        .AddOverload(ob => ob.AddArgument<string>(ab => ab.Name("name")
                                                          .Converter(c => c)
                                                          .Build())
                             .Body((args, opt) => Console.Write($"You wrote {args.ValueOf<string>("name")}"))
                             .Build())
        .AddInner(cb => cb.Name("Hello").Body((args, opt) => Console.Write($"Double inner with test")).Build())
        .Build();    
}
