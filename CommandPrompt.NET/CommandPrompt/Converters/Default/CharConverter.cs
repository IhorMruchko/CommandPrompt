using System;

namespace CommandPrompt.Converters.Default
{
    public class CharConverter : CommonConverter<char>
    {
        protected override bool PerformConvertion(string convert)
        {
            var isConvertable = convert.Length == 1;
            if (isConvertable)
            {
                convertedValue = convert[0];
            }
            return isConvertable;
        }
        public override ConverterBase Clone()
            => new CharConverter();
    }
}
