using CommandPrompt;
using CommandPrompt.Extensions;

CommandRegestry.Register.Add(cb => cb.Name("cls")
                                     .Body((args, opt) => Console.Clear())
                                     .Build())
                        .Add(cb => cb.Name("hello")
                                     .AddOverload(ob => ob.AddRequiredArgument<string>(ab => ab.Name("name")
                                                                                               .Converter(c => c)
                                                                                               .Validator(n => char.IsUpper(n[0]))
                                                                                               .Message("Value should starts with uppercase!")
                                                                                               .Build())
                                                          .Body((args, opt) => Console.Write($"Hello, {args.ValueOf<string>("name")}!"))
                                                          .Build())
                                     .Build())
                        .Add(cb => cb.Name("async")
                                     .Body(async (args, opt) =>
                                     {
                                         Console.WriteLine("Hi");
                                         await Task.Delay(1500);
                                         Console.Write("Hello");
                                     })
                                     .Build())
                        .Add(cb => cb.Name("as")
                                     .AddOverload(ob => ob.AddRequiredArgument<string>(ab => ab.Name("say")
                                                                                               .Converter(c => c)
                                                                                               .Build())
                                                          .Body(async (args, opt) =>
                                                          {
                                                              Console.WriteLine("Hi");
                                                              await Task.Delay(1500);
                                                              Console.Write($"Hello {args.ValueOf<string>("say")}");
                                                          })
                                                          .Build())
                                     .Build())
                        .Add(cb => cb.Name("help")
                                     .AddOverload(ob => ob.AddRequiredArgument<string>("help")
                                                          .Body((args, opt) => Console.Write("Hello"))
                                                          .Build())
                                     .Build());

await CommandRegestry.Invoke(args);
