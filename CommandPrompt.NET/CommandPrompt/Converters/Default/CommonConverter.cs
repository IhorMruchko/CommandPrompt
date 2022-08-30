using System;

namespace CommandPrompt.Converters.Default
{
    public class CommonConverter<TConvert> : ConverterBase
    {
        protected TConvert convertedValue;

        public override object Value => convertedValue;

        public override Type ConvertionType => typeof(TConvert);

        protected Converter<string, TConvert> Converter { get; set; }

        public static implicit operator CommonConverter<TConvert>(Converter<string, TConvert> converter)
        {
            return new CommonConverter<TConvert>()
            {
                Converter = converter
            };
        }

        public override ConverterBase Clone()
        {
            return new CommonConverter<TConvert>();
        }

        public override bool IsAbleToConvert(string convert) 
            => PerformConvertion(convert) || TryConvert(convert);
        
        protected virtual bool PerformConvertion(string convert) 
            => false;
        
        protected bool TryConvert(string convert)
        {
            if (Converter is null)
            {
                return false;
            }
            try
            {
                Converter.Invoke(convert);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
