using CommandPrompt.Converters.Default;
using CommandPrompt.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandPrompt.Converters
{
    /// <summary>
    /// Register classes that relialize interface <see cref="ConverterBase"/>.
    /// </summary>
    public static class ConverterFactory
    {
        private static readonly Validator<Type> _typeValidation =
            new Validator<Type>().ShouldNot(t => t.IsAbstract)
                                 .WithMessage(t => $"Type {t.Name} should not be abstract")
                                 .ShouldNot(t => t.IsGenericTypeDefinition)
                                 .WithMessage(t => $"Type {t.Name} should not be generic")
                                 .Should(t => t.IsSubclassOf(typeof(ConverterBase)))
                                 .WithMessage(t => $"Type {t.Name} should be son of [CommonConverter] but {t.BaseType}");
        private static List<ConverterBase> _converters = new List<ConverterBase>();

        //TODO: Add proper convertion getting.
        static ConverterFactory()
        {
            _converters = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => _typeValidation.Validate(t))
                .Select(t => Activator.CreateInstance(t) as ConverterBase)
                .ToList();
            Console.Write(string.Join("\n", _converters));
        }

        /// <summary>
        /// Returns registered converter of <typeparamref name="TConvertion"/> type.
        /// </summary>
        /// <typeparam name="TConvertion">Target convertion type.</typeparam>
        /// <returns><see cref="ConverterBase"></see> of specified type.</returns>
        /// <exception cref="KeyNotFoundException">Converter for specified <typeparamref name="TConvertion"/> type not found.</exception>
        public static CommonConverter<TConvertion> GetConverter<TConvertion>()
            => _converters.FirstOrDefault(t => t.ConvertionType == typeof(TConvertion)) as CommonConverter<TConvertion>
            ?? throw new KeyNotFoundException($"Converter for specified [{typeof(TConvertion).Name}] type not found.");
        


        // TODO: Remove duplicates of types.
        // TODO: Add registration settings.
        // TODO: Provide testabe return type.
        /// <summary>
        /// Register converters manualy.
        /// </summary>
        /// <param name="converters">Converters to register.</param>
        public static void Register(params ConverterBase[] converters) 
            => _converters.AddRange(converters);
    }
}
