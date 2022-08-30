namespace CommandPrompt.Converters.Default
{
    public class StringConverter : CommonConverter<string>
    {
        public override ConverterBase Clone() 
            => new StringConverter();

        protected override bool PerformConvertion(string convert)
        {
            convertedValue = convert;
            return true;
        }
    }
}
