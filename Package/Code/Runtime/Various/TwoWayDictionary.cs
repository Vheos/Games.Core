namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class TwoWayDictionary<TKey, TValue> : IReadOnlyTwoWayDictionary<TKey, TValue>
    {
        // Privates
        private readonly Dictionary<TKey, TValue> _valuesByKeys = new();
        private readonly Dictionary<TValue, TKey> _keysByValues = new();

        // Publics
        public void Add(TKey key, TValue value)
        {
            if (_valuesByKeys.ContainsKey(key) || _keysByValues.ContainsKey(value))
                throw new ArgumentException("Duplicate key or value");

            _valuesByKeys.Add(key, value);
            _keysByValues.Add(value, key);
        }
        public bool TryAdd(TKey key, TValue value)
        {
            if (_valuesByKeys.ContainsKey(key) || _keysByValues.ContainsKey(value))
                return false;

            _valuesByKeys.Add(key, value);
            _keysByValues.Add(value, key);
            return true;
        }
        public bool TryGetValue(TKey key, out TValue value)
        => _valuesByKeys.TryGetValue(key, out value);
        public bool TryGetKey(TValue value, out TKey key)
        => _keysByValues.TryGetValue(value, out key);
        public IEnumerable<TKey> Keys
        {
            get
            {
                foreach (var kvp in _valuesByKeys)
                    yield return kvp.Key;
            }
        }
        public IEnumerable<TValue> Values
        {
            get
            {
                foreach (var kvp in _valuesByKeys)
                    yield return kvp.Value;
            }
        }
        public IEnumerable<KeyValuePair<TKey, TValue>> KeyValuePairs
        {
            get
            {
                foreach (var kvp in _valuesByKeys)
                    yield return kvp;
            }
        }

        // Operators
        public TValue this[TKey key]
        => _valuesByKeys[key];
        public TKey this[TValue value]
        => _keysByValues[value];
    }
}