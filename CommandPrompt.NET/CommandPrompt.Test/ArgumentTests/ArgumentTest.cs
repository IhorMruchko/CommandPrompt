using CommandPrompt.Arguments;

namespace CommandPrompt.Test.ArgumentTests;

public partial class ArgumentTest
{
    private readonly RequiredArgument<int> _intArgument = new() { Name = "integer", };
    private readonly RequiredArgument<string> _stringArgument = new() { Name = "string" };
    private readonly RequiredArgument<string> _stringArgumentWithIntegerName = new() { Name = "integer" };
    private readonly OptionalArgument<int> _optIntArgument = new() { Name = "integer", };
    private readonly OptionalArgument<string> _optStringArgument = new() { Name = "string" };
    private readonly OptionalArgument<string> _optStringArgumentWithIntegerName = new() { Name = "integer" };
}
