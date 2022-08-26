namespace CommandPrompt.Test.ArgumentTests;

public partial class ArgumentTest
{
    [Test]
    public void Equal_SameOptionalArgs_True()
    {
        var optIntArgumentCopy = _optIntArgument.Copy();
        Assert.Multiple(() =>
        {
            Assert.That(optIntArgumentCopy, Is.EqualTo(_optIntArgument));
            Assert.That(ReferenceEquals(optIntArgumentCopy, _optIntArgument), Is.False);
        });
    }

    [Test]
    public void Equal_SameRequiredArgs_True()
    {
        var intArgumentCopy = _intArgument.Copy();
        Assert.Multiple(() =>
        {
            Assert.That(intArgumentCopy, Is.EqualTo(_intArgument));
            Assert.That(ReferenceEquals(intArgumentCopy, _intArgument), Is.False);
        });
    }

    [Test]
    public void Equal_SameTypesDifferentNames_False()
    {
        Assert.That(_stringArgument, Is.Not.EqualTo(_stringArgumentWithIntegerName));
    }

    [Test]
    public void Equal_SameOptTypesDifferentNames_False()
    {
        Assert.That(_optStringArgument, Is.Not.EqualTo(_optStringArgumentWithIntegerName));
    }
}
