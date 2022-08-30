using CommandPrompt.Converters;
using CommandPrompt.Converters.Default;

namespace CommandPrompt.Test.ConverterTests;

public class ConverterFactoryTests 
{
    [Test]
    public void GetConverterInt_ConvertersNotRegistered_ThrowsKeyNotFoundException()
    {
        var keyNotFoundException = Assert.Throws<KeyNotFoundException>(() => ConverterFactory.GetConverter<ConverterFactoryTests>());

        Assert.That(keyNotFoundException.Message, Is.EqualTo($"Converter for specified [{typeof(ConverterFactoryTests).Name}] type not found."));
    }

    [Test]
    public void GetConverterInt_ConverterRegistered_GetsConverter()
    {
        ConverterFactory.Register(new CharConverter());

        var intConverter = ConverterFactory.GetConverter<char>();

        Assert.That(intConverter.ConvertionType, Is.EqualTo(typeof(char)));
    }
}
