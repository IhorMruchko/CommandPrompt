namespace CommandPrompt.Builders.OverloadBuilding
{
    public interface IOverloadNameSetter : IOverloadSetter
    {
        IOverloadSetter Name(string overloadName);
    }
}
