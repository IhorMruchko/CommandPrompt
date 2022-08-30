namespace CommandPrompt.Builders.ArgumentBuilding
{
    public interface IArgumentNameSetter<TArument>
    {
        IArgumentCreator<TArument> Name(string name);
    }
}
