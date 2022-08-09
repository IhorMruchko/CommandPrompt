# Content
- [Content](#content)
- [About](#about)
  - [Main info](#main-info)
  - [Planning](#planning)
- [Install](#install)
- [Usage](#usage)
  - [Configuration](#configuration)
  - [Execution](#execution)

# About
This package is created to simplify command configuration for CLIs.
## Main info
Current version: **0.0.0.2**<br/>
.NET vestion: **.NET Standart 2.0**

Version **0.0.0.2** Updates:
* Add proper building sequence.
* Fixed async execution.

## Planning
- [ ] Increase variety of adding overloads/inner commands.
- [ ] Provide default converters.
- [ ] Allow setting help description for command and provide proper help command invoke.
- [ ] Add equality for exectutable objects.
- [ ] Provide attribute configuration.

# Install
- For package manager:
  > `Install-Package CommandPrompt.Configuring -Version 0.0.0.1`
- For .NET CLI:
  > `dotnet add package CommandPrompt.Configuring --version 0.0.0.1`
- Add reference to your project:
  > `<PackageReference Include="CommandPrompt.Configuring" Version="0.0.0.1" />`
# Usage

## Configuration
To start configuring you can use `CommandRegestry` class.
```C#
CommandRegestry.Register.Add(cb => cb.Name("cls")
                                     .Body((args, opt) => Console.Clear())
                                     .Build());
```
In this example command with name `cls` that clears console is added.
You can also add more commands with method `Add`:
```C#
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
                                     .Build());
```
In this example to the command with name `hello` was added new overload with method `AddOverload` that contains one argument of type `string`. Also this argument should start with uppercase symbol. 

To access value of the argument you can use `ValueOf` extension method of List of Arguments. To use this method add `using CommandPrompt.Extension;`.

## Execution
After all commands are added, you may execute one of them. To do this, use method `Invoke` and path array of string.
```C#
await CommandRegestry.Invoke(args);
```