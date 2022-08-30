# Content
- [Content](#content)
- [About](#about)
  - [Main info](#main-info)
- [Updates](#updates)
  - [Version _0.0.0.2_ updates:](#version-0002-updates)
  - [Version _0.0.0.3_ updates:](#version-0003-updates)
- [Install](#install)
- [Usage](#usage)
  - [Configuration](#configuration)
  - [Execution](#execution)
  - [Copying from register](#copying-from-register)

# About
This package is created to simplify command configuration for CLIs.
## Main info
Current version: **0.0.0.3**
.NET vestion: **.NET Standart 2.0**

# Updates

## Version _0.0.0.2_ updates:
* Add proper building sequence.
* Fixed async execution.

## Version _0.0.0.3_ updates:
* Update validation;
  > Provide default validators for `string`.
* Update convertion
  > Set default converters for `char`, `bool` and `string`;
* Add copying of objects;
  > All objects can be copied and used from [`CommandRegestry`](#copying-from-register)
* Add equaliity check for arguments and overloads.

# Install
- For package manager:
  > `Install-Package CommandPrompt.Configuring -Version 0.0.0.3`
- For .NET CLI:
  > `dotnet add package CommandPrompt.Configuring --version 0.0.0.3`
- Add reference to your project:
  > `<PackageReference Include="CommandPrompt.Configuring" Version="0.0.0.3" />`
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

## Copying from register
```C#

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
```
Get already registered _argument_, _overload_ or _command_ with `CommandRegestry.GetArgument<>()`, `CommandRegestry.GetOverload()`, `CommandRegestry.GetCommand`.