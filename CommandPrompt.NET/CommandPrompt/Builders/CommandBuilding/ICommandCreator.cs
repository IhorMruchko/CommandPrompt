using CommandPrompt.Executable;

namespace CommandPrompt.Builders.CommandBuilding
{
    public interface ICommandCreator
    {
        Command Build();
    }
}