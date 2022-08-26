namespace CommandPrompt.Test
{
    public class CommadRegisterTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            CommandRegestry.Register.Add(ab => ab.Name("hello").Body((args, opt) => Console.Write("hello")).Build())
                                    .Add(ab => ab.Name("isCalled").Body((args, opt) => Console.Write("No")).Build());
        }

        [Test]
        public void NameIsAdded_IsUnique_False()
        {
            var commandName = "hello";
            var isCalled = CommandRegestry.IsUnique(commandName);

            Assert.That(isCalled, Is.False);
        }

        [Test]
        public void NameNotAdded_IsUnique_True()
        {
            var commandName = "help";
            var isCalled = CommandRegestry.IsUnique(commandName);

            Assert.That(isCalled, Is.True);
        }
    }
}
