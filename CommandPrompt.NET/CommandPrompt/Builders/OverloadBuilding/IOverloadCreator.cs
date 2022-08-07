using CommandPrompt.Executable;

namespace CommandPrompt.Builders.OverloadBuilding
{
    public interface IOverloadCreator
    {
        Overload Build();
    }
}