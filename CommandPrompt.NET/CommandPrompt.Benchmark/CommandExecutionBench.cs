using BenchmarkDotNet.Attributes;
using CommandPrompt.Builders.CommandBuilding;
using CommandPrompt.Executable;
using CommandPrompt.Extensions;

namespace CommandPrompt.Benchmark;

[MemoryDiagnoser]
public class CommandExecutionBench
{
    public readonly Command Command = new CommandBuilder().Name("add")
                              .AddOverload(o => o.AddRequiredArgument<int>(b => b.Name("a").Converter(c => int.Parse(c)).Build())
                                                 .AddOptionalArgument<float>(b => b.Name("b").Converter(c => float.Parse(c)).Validator(b => b != 0).Build())
                                                 .AddRequiredArgument<int>(b => b.Name("b").Converter(c => int.Parse(c)).Build())
                                                 .Body((args, opt) => Console.WriteLine((args.ValueOf("a", 1) + args.ValueOf("b", 1)) / opt.ValueOf("b", 1f)))
                                                 .Build())
                              .AddOverload(o => o.AddRequiredArgument<string>(b => b.Name("title").Converter(c => c).Build())
                                                 .AddRequiredArgument<int>(b => b.Name("times").Converter(c => int.Parse(c)).Build())
                                                 .AddOptionalArgument<char>(b => b.Name("separator").Converter(c => c[0]).Build())
                                                 .Body((args, opt) => Console.WriteLine(string.Join(opt.ValueOf("separator", ','), Enumerable.Range(0, args.ValueOf("times", 3)).Select(i => args.ValueOf("title", "title")))))
                                                 .Build())
                              .AddInner(ib => ib.Name("sub")
                                                .AddOverload(o => o.Body((args, opt) => Console.Clear()).Build())
                                                .Build())
                              .AddInner(ib => ib.Name("mul")
                                                .AddOverload(o => o.AddRequiredArgument<int>(r => r.Name("a")
                                                                                                   .Converter(c => int.Parse(c))
                                                                                                   .Build())
                                                                   .Body((args, opt) => Console.WriteLine(args.ValueOf("a", 0) * 10))
                                                                   .Build())
                                                .Build())
                              .Build();

    public readonly List<string> Arguments = "add 10 -b = 2 4".Split(" ").ToList();

    [Benchmark]
    public void CallCommand()
    {
        Command.CheckIsCalled(Arguments);
    }
}
