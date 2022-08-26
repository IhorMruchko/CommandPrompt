namespace CommandPrompt.Test.ArgumentTests
{
    public partial class ArgumentTest
    {
        [Test]
        public void ToString_RequiredArgumentInt_ValidString()
        {
            var expectedResult = "integer of type System.Int32";

            Assert.That(_intArgument.ToString(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void ToString_OptionalArgumentStringWithIntegerName_ValidString()
        {
            var expectedResult = "integer of type System.String |opt";

            Assert.That(_optStringArgumentWithIntegerName.ToString(), Is.EqualTo(expectedResult));
        }
    }
}
