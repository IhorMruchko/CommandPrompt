namespace CommandPrompt.Builders.CommandBuilding
{
    public interface ICommandNameSetter
    {
        ICommandBodySetter Name(string Name);
    }
}