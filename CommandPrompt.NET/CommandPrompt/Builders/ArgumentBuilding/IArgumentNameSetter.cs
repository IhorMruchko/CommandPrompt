namespace CommandPrompt.Builders.ArgumentBuilding
{
    public interface IArgumentNameSetter<TArument>
    {
        IArgumentConverterSetter<TArument> Name(string name);
    }
}
