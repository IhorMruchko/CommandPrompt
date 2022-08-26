using CommandPrompt.Validators;

namespace CommandPrompt.Extensions
{
    public static class ValidationExtension
    {
        public static Validator<TValidation> Is<TValidation>(this object value) 
            => value is TValidation validation 
            ? new Validator<TValidation>(validation)
            : new Validator<TValidation>().Should(t => t is TValidation);

        public static Validator<TValidation> ToValidator<TValidation>(this TValidation value) 
            => new Validator<TValidation>(value);
    }
}
