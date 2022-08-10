using CommandPrompt.Validators;
using System;

namespace CommandPrompt.Arguments
{
    public abstract class Argument<TArgument> : ArgumentBase
    {
        public Converter<string, TArgument> Converter { get; internal set; }

        public Validator<TArgument> Validator { get; internal set; }

        public override string ToString()
        {
            return $"{Value} of type {typeof(TArgument)}";
        }

        protected bool TryConvert(string argValue)
        {
            try
            {
                Value = Converter(argValue);
                return Value != default;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected bool Validate()
        {
            if (Validator.IsValid((TArgument)Value))
            {
                return true;
            }
            Console.Write(Validator.GetMessage(Name));
            return false;
        }

        protected bool TryValidate(string argValue)
        {
            IsValid = TryConvert(argValue) && Validate();
            return IsValid;
        }
    }
}
