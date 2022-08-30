namespace CommandPrompt.Validators.StringValidators
{
    public static class StringValidator
    {
        /// <summary>
        /// Check is string is empty.
        /// </summary>
        public static Validator<string> IsEmpty 
            => new Validator<string>().Should(t => t.Trim().Equals(string.Empty))
                                      .WithMessage("Value is not empty");
        /// <summary>
        /// Check is string is not empty.
        /// </summary>
        public static Validator<string> NotEmpty 
            => new Validator<string>().ShouldNot(t => t.Trim().Equals(string.Empty))
                                      .WithMessage("Value should be empty");
        /// <summary>
        /// Check is string starts with upper case.
        /// </summary>
        public static Validator<string> IsUpperCase
            => NotEmpty.Should(t => char.IsUpper(t[0]))
                       .WithMessage("Value starts not from upper case");
        
        /// <summary>
        /// Check is object is strongly equals to some value.
        /// </summary>
        /// <param name="another">Value to compare with.</param>
        /// <returns><c>Validator&lt;string&gt;</c> that check is object equals to some value.</returns>
        public static Validator<string> EqualTo(string another) 
            => new Validator<string>().Should(value => value.Equals(another))
                                      .WithMessage($"Object should be equal to ${another}");
    }
}
