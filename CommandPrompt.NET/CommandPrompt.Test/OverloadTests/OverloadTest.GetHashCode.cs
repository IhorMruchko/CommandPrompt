namespace CommandPrompt.Test.OverloadTests
{
    public partial class OverloadTest
    {
        [Test]
        public void GetHashCode_OverloadEqualCopiedOverload_True()
        {
            var helloOverloadCopy = _helloOverload.Copy();
            Assert.That(helloOverloadCopy.GetHashCode(), Is.EqualTo(_helloOverload.GetHashCode()));
        }

        [Test]
        public void GetHashCode_OverloadEqualCopiedOverloadWithDifferentName_False()
        {
            var helloOverloadCopy = _helloOverload.Copy();
            helloOverloadCopy.Name = "helloCopy";

            Assert.That(helloOverloadCopy.GetHashCode(), Is.Not.EqualTo(_helloOverload.GetHashCode()));
        }

        [Test]
        public void GetHashCode_OverloadEqualCopiedOverloadWithDifferentRequiredArgsCount_False()
        {
            var helloOverloadCopy = _helloOverload.Copy();
            helloOverloadCopy.RequiredArgs.Clear();

            Assert.That(helloOverloadCopy.GetHashCode(), Is.Not.EqualTo(_helloOverload.GetHashCode()));
        }

        [Test]
        public void GetHashCode_OverloadEqualCopierdOverloadWithAnotherRequiredArgument_False()
        {
            var helloOverloadCopy = _helloOverload.Copy();
            var argument = helloOverloadCopy.RequiredArgs[0].Copy();
            var newArgument = Builder.Required<int>().Name("amount").Converter(c => int.Parse(c)).Build();
            helloOverloadCopy.RequiredArgs.Clear();
            helloOverloadCopy.RequiredArgs.Add(newArgument);

            Assert.Multiple(() =>
            {
                Assert.That(argument.GetHashCode(), Is.Not.EqualTo(newArgument.GetHashCode()));
                Assert.That(_helloOverload.GetHashCode(), Is.Not.EqualTo(helloOverloadCopy.GetHashCode()));
            });
        }
    }
}
