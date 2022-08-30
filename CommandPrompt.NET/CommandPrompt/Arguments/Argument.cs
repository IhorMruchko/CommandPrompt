using CommandPrompt.Converters.Default;
using CommandPrompt.Services;
using CommandPrompt.Validators;
using System;

namespace CommandPrompt.Arguments
{
    public abstract class Argument<TArgument> : ArgumentBase
    {
        public CommonConverter<TArgument> Converter { get; internal set; }

        public Validator<TArgument> Validator { get; internal set; }

        public override object Value => Converter.Value; 

        /// <summary>
        /// Check is object is the same type with save <c>Name</c>.
        /// </summary>
        /// <param name="obj">Another object.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
            => obj.GetType() == GetType() && ((Argument<TArgument>)obj).Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Return hash code of argument based on <c>Name</c> and <c>TArgument</c>.
        /// </summary>
        /// <remarks>
        /// To generate hash code <see href="https://stackoverflow.com/a/34006336">hash code combiner</see> based on seed and factor was used.
        /// </remarks>
        /// <returns>Hash code of the argument.</returns>
        public override int GetHashCode() 
            => new HashCode().Add(Name)
                             .Add(typeof(TArgument))
                             .Add(GetType())
                             .ToHashCode();
        /// <summary>
        /// Convert argument to string.
        /// </summary>
        /// <returns>Information about the argument.</returns>
        public override string ToString() => $"{Name} of type {typeof(TArgument)}";

        protected bool TryConvert(string argValue)
        {
            try
            {
                return Converter?.IsAbleToConvert(argValue) ?? false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected bool Validate()
        {
            if (Validator?.Validate((TArgument)Converter.Value) ?? true)
            {
                return true;
            }
            Console.Write(Validator.Exception);
            return false;
        }

        protected bool TryValidate(string argValue)
        {
            return TryConvert(argValue) && Validate();
        }
    }
}
