using CommandPrompt;
using CommandPrompt.Extensions;
using CommandPrompt.Validators.StringValidators;


CommandRegestry.Register.Add(cb => cb.Name("hello")
                                     .AddOverload(ob => ob.AddArgument<string>(ab => ab.Name("username")
                                                                                       .Validator(StringValidator.IsUpperCase)
                                                                                       .Build())
                                                         .AddOptArgument<char>("ending")
                                     .Body((args, opt) => Console.Write($"Hello, {args.ValueOf("username", "Unknown?")} {opt.ValueOf("ending", '!')}"))
                                     .Build())
                            .Build())
                        .Add(cb => cb.Name("bye")
                                     .AddOverload(ob => ob.AddArgument(CommandRegestry.GetArgument<string>("username"))
                                                          .AddOptArgument(CommandRegestry.GetArgument<char>("ending"))
                                                          .Body((args, opt) => Console.Write($"Bye, {args.ValueOf("username", "Unknown?")} {opt.ValueOf("ending", '!')}"))
                                                          .Build())
                        .Build());

while (true)
    await CommandRegestry.Invoke(Console.ReadLine()?.Split(" ") ?? Array.Empty<string>());