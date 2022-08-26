namespace CommandPrompt.Test.ArgumentTests;

public partial class ArgumentTest
{
    [Test]
    public void GetHashCode_SameRquiredArguments_EqualHashes()
    {
        var intArgumentCopy = _intArgument.Copy();
        Assert.Multiple(() =>
        {
            Assert.That(intArgumentCopy.GetHashCode(), Is.EqualTo(_intArgument.GetHashCode()));
            Assert.That(ReferenceEquals(intArgumentCopy, _intArgument), Is.False);
        });
    }

    [Test]
    public void GetHashCode_DifferentRquiredArgumentsNames_NotEqualHashes()
    {
        var intArgumentCopy = _intArgument.Copy();
        intArgumentCopy.Name = "changed name";
        Assert.That(_intArgument.GetHashCode(), Is.Not.EqualTo(intArgumentCopy.GetHashCode()));
    }

    [Test]
    public void GetHashCode_DifferentRquiredArgumentsTypes_NotEqualHashes()
    {
        Assert.That(_intArgument.GetHashCode(), Is.Not.EqualTo(_stringArgumentWithIntegerName.GetHashCode()));
    }

    [Test]
    public void GetHashCode_DifferentArgumentsTypesSameValues_NotEqualHashes()
    {
        Assert.That(_intArgument.GetHashCode(), Is.Not.EqualTo(_optIntArgument.GetHashCode()));
    }
}
