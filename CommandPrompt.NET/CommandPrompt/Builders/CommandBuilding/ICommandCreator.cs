using CommandPrompt.Executable;

namespace CommandPrompt.Builders.CommandBuilding
{
    public interface ICommandBuilder
    {
        Command Build();
    }
}