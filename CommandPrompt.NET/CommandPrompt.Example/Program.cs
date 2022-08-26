using CommandPrompt;
using CommandPrompt.Builders;
using CommandPrompt.Extensions;

var endingArgument = Builder.Optional<char> ()
                                    .Name("ending")
                                    .Converter(c => c[0])
                                    .Build();

 var usernameArgument = Builder.Required<string>()
                                       .Name("username")
                                       .Converter(c => c)
                                       .Validator(c => char.IsUpper(c[0]))
                                       .Build();

var nameOverload = Builder.Overload.Name("runner")
                                   .AddArgument(usernameArgument)
                                   .AddOptArgument(endingArgument)
                                   .Body((args, opt) => Console.Write($"Hello, {args.ValueOf("username", "Unknown?")} {opt.ValueOf("ending", '!')}"))
                                   .Build();

var helloCommand = Builder.Command.Name("hello")
                                  .AddOverload(nameOverload)
                                  .Build();

CommandRegestry.Register.Add(helloCommand);

Console.WriteLine(CommandRegestry.GetCommand("hello"));