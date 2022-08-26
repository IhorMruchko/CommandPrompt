using CommandPrompt.Arguments;
using CommandPrompt.Builders.CommandBuilding;

namespace CommandPrompt.Test
{
    public class CommandBuilderTest
    {
        private static readonly CommandBuilder _builder = new();
        private static readonly string _alreadyCommandName = "already";

        [OneTimeSetUp]
        public void BeforeTest()
        {
            CommandRegestry.Register.Add(cb => cb.Name(_alreadyCommandName)
                                                 .Body(DefaultBody)
                                                 .Build());
        }

        [Test]
        public void SetName_NameIsValid_True()
        {
            var commandName = "cmd";
            var command = _builder.Name(commandName).Body(DefaultBody).Build();

            Assert.That(command.Name, Is.EqualTo(commandName));
        }

        [Test]
        public void SetName_NameIsNull_ThrowsArumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => _builder.Name(null));
            
            Assert.That(exception.Message, Is.EqualTo("Name should not be null (Parameter 'name')"));
        }

        [Test]
        public void SetName_NameIsNotUnique_ThrowsArumentException()
        {
            Assert.Throws<ArgumentException>(() => _builder.Name(_alreadyCommandName));
        }

        private void DefaultBody(List<ArgumentBase> args, List<ArgumentBase> opt)
        {
            Console.Write("hello");
        }
    }
}
