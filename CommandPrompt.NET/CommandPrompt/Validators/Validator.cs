using System;

namespace CommandPrompt.Validators
{
    public class Validator<TValidate>
    {
        private readonly Func<TValidate, bool> _validator;

        private Validator(Func<TValidate, bool> validator)
        {
            _validator = validator;
        }

        public string Message { get; internal set; }
        
        public bool IsValid(TValidate value)
        {
            try
            {
                return _validator?.Invoke(value) ?? false;
            }
            catch
            {
                return false;
            }
        }

        public string GetMessage(string argName = null)
        {
            return Message is null
                ? $"Argument {argName} is invalid due to constrain"
                : $"Argument {argName} is invalid: {Message.ToLower()}";
        }

        public static implicit operator Func<TValidate, bool>(Validator<TValidate> value)
        {
            return value._validator;
        }

        public static implicit operator Validator<TValidate>(Func<TValidate, bool> func)
        {
            return new Validator<TValidate>(func);
        }
    }
}
