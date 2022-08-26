namespace CommandPrompt.Test.OverloadTests;

public partial class OverloadTest
{
    [Test]
    public void Equals_OverloadEqualCopiedOverload_True()
    {
        var helloOverloadCopy = _helloOverload.Copy();
        Assert.Multiple(() =>
        {
            Assert.That(helloOverloadCopy, Is.EqualTo(_helloOverload));
            Assert.That(ReferenceEquals(helloOverloadCopy, _helloOverload), Is.False);
        });
    }

    [Test]
    public void Equals_OverloadEqualCopiedOverloadWithDifferentName_False()
    {
        var helloOverloadCopy = _helloOverload.Copy();
        helloOverloadCopy.Name = "helloCopy";

        Assert.That(helloOverloadCopy, Is.Not.EqualTo(_helloOverload));
    }

    [Test]
    public void Equals_OverloadEqualCopiedOverloadWithDifferentRequiredArgsCount_False()
    {
        var helloOverloadCopy = _helloOverload.Copy();
        helloOverloadCopy.RequiredArgs.Clear();

        Assert.That(helloOverloadCopy, Is.Not.EqualTo(_helloOverload));
    }

    [Test]
    public void Equals_OverloadEqualCopierdOverloadWithAnotherRequiredArgument_False()
    {
        var helloOverloadCopy = _helloOverload.Copy();
        var argument = helloOverloadCopy.RequiredArgs[0].Copy();
        var newArgument = Builder.Required<int>().Name("amount").Converter(c => int.Parse(c)).Build();
        helloOverloadCopy.RequiredArgs.Clear();
        helloOverloadCopy.RequiredArgs.Add(newArgument);

        Assert.Multiple(() =>
        {
            Assert.That(argument, Is.Not.EqualTo(newArgument));
            Assert.That(_helloOverload, Is.Not.EqualTo(helloOverloadCopy));
        });
    }
}
