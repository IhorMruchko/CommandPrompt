using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandPrompt.Validators
{
    public class Validator<TValidate>
    {
        private List<RuleFor<TValidate>> _rules = new List<RuleFor<TValidate>>();
        
        private readonly TValidate _value;

        public Validator() { }

        public Validator(TValidate value)
        {
            _value = value;
        }

        public List<string> Exceptions { get; private set; }

        public string Exception => Exceptions.FirstOrDefault();

        public Validator<TValidate> WithMessage(string message)
        {
            if (_rules.Any() == false)
            {
                throw new InvalidOperationException("Add rule first!");
            }
            _rules[_rules.Count - 1].Message = message;
            return this;
        }

        public Validator<TValidate> WithMessage(Func<TValidate, string> message)
        {
            if (_rules.Any() == false)
            {
                throw new InvalidOperationException("Add rule first!");
            }
            _rules[_rules.Count - 1].MessageBuilder = message;
            return this;
        }

        public Validator<TValidate> Should(Func<TValidate, bool> constraint)
        {
            _rules.Add(new RuleFor<TValidate>() { Rule = constraint });
            return this;
        }

        public Validator<TValidate> ShouldNot(Func<TValidate, bool> constraint)
        {
            _rules.Add(new RuleFor<TValidate>() { Rule = constraint, IsInverted = true });
            return this;
        }

        public bool Validate()
        {
            Exceptions = _rules.Where(rule => rule.IsFollowed(_value) == false)
                               .Select(r => r.Message)
                               .ToList();
            return Exceptions.Any() == false;
        }

        public bool Validate(TValidate value)
        {
            Exceptions = _rules.Where(rule => rule.IsFollowed(value) == false).Select(r => r.Exception(value)).ToList();
            return Exceptions.Any() == false;
        }

        public Validator<TValidate> Clone()
        {
            return new Validator<TValidate>(_value)
            {
                _rules = _rules.Select(r => r.Clone() as RuleFor<TValidate>).ToList()
            };
        }

        public static implicit operator Validator<TValidate>(Func<TValidate, bool> function)
        {
            return new Validator<TValidate>().Should(function);
        }
    }
}
