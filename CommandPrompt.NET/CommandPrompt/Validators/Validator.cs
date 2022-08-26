﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandPrompt.Validators
{
    public class Validator<TValidate>
    {
        private readonly List<Rule> _rules = new List<Rule>();
        
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

        public Validator<TValidate> Should(Func<TValidate, bool> constraint)
        {
            _rules.Add(new RuleFor<TValidate>() { Rule = constraint });
            return this;
        }

        public Validator<TValidate> ShouldNot(Func<TValidate, bool> constraint)
        {
            _rules.Add(new RuleFor<TValidate>() { Rule = (validator) => constraint(validator) == false });
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
            Exceptions = _rules.Where(rule => rule.IsFollowed(value) == false).Select(r => r.Message).ToList();
            return Exceptions.Any() == false;
        }
    }
}
