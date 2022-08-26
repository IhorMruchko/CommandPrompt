using CommandPrompt.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandPrompt.Services
{
    internal class HashCode
    {
        private const int _factor = 9176;
        private const int _seed = 1009;
        private readonly List<object> _objectToHash = new List<object>();
        private int _hashCode = _seed;

        public HashCode Add(object value)
        {
            _objectToHash.Add(value);
            return this;
        }

        public HashCode AddMany(IEnumerable<object> values)
        {
            _objectToHash.AddRange(values);
            return this;
        }

        public int ToHashCode()
        {
            foreach(var obj in _objectToHash)
            {
                _hashCode = (_hashCode * _factor) + obj.GetHashCode();
            }
            return _hashCode;
        }
    }
}
