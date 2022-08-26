using CommandPrompt.Executable;
using CommandPrompt.Builders;
using CommandPrompt.Extensions;

namespace CommandPrompt.Test.OverloadTests;

public partial class OverloadTest
{
    private readonly Overload _helloOverload = Builder.Overload.Name("hello")
                                                               .AddArgument(Builder.Required<string>()
                                                                                   .Name("name")
                                                                                   .Converter(c => c)
                                                                                   .Build())
                                                               .Body((args, opt) => Console.Write(args.ValueOf<string>("name")))
                                                               .Build();
    
}
