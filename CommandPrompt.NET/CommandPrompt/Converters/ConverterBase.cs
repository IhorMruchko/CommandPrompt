using System;

namespace CommandPrompt.Converters
{
    public abstract class ConverterBase
    {
        /// <summary>
        /// Converted value from string.
        /// </summary>
        public abstract object Value { get; }

        /// <summary>
        /// Type to convert to from string.
        /// </summary>
        public abstract Type ConvertionType { get; }

        /// <summary>
        /// Try to convert value from string. 
        /// </summary>
        /// <param name="convert">String value to convert from.</param>
        /// <returns>True if convertion successful. Otherwise - false.</returns>
        public abstract bool IsAbleToConvert(string convert);

        /// <summary>
        /// Creates a copy of the converter.
        /// </summary>
        /// <returns>Conveters copy.</returns>
        public abstract ConverterBase Clone();
    }
}
