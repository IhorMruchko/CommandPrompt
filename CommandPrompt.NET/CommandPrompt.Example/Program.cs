using CommandPrompt;
using CommandPrompt.Builders;
using CommandPrompt.Extensions;
using CommandPrompt.Validators.StringValidators;

var endingArgument = Builder.Optional<char>()
                                    .Name("ending")
                                    .Converter(c => c[0])
                                    .Build();

var usernameArgument = Builder.Required<string>()
                                       .Name("username")
                                       .Converter(c => c)
                                       .Validator(StringValidator.IsUpperCase)
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

while (true)
    await CommandRegestry.Invoke(Console.ReadLine()?.Split() ?? Array.Empty<string>());