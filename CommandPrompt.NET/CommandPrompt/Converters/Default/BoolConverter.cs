using System;
using System.Collections.Generic;

namespace CommandPrompt.Converters.Default
{
    public class BoolConverter : CommonConverter<bool>
    { 
        private List<string> _trueStringValues = new List<string>()
        {
            "true",
            "t",
            "1",
        };

        protected override bool PerformConvertion(string convert)
        => bool.TryParse(convert, out convertedValue)
            || IsExtendedParsable(convert);

        public override ConverterBase Clone() 
            => new BoolConverter();

        private bool IsExtendedParsable(string convert)
        {
            convertedValue = _trueStringValues.Contains(convert);
            return convertedValue;
        }
    }
}
