using CommandPrompt;
using CommandPrompt.Extensions;

CommandRegestry.Register.Add(cb => cb.Name("cls")
                                     .Body((args, opt) => Console.Clear())
                                     .Build())
                        .Add(cb => cb.Name("hello")
                                     .AddOverload(ob => ob.AddRequiredArgument<string>(ab => ab.Name("name")
                                                                                               .Converter(c => c)
                                                                                               .Validator(n => char.IsUpper(n[0]))
                                                                                               .Build())
                                                          .Body((args, opt) => Console.Write($"Hello, {args.ValueOf<string>("name")}!"))
                                                          .Build())
                                     .Build())
                        .Add(cb => cb.Name("opt").Build());

CommandRegestry.Invoke(args);