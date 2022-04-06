namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public interface IReadOnlyTwoWayDictionary<TKey, TValue>
    {
        public bool TryGetValue(TKey key, out TValue value);
        public bool TryGetKey(TValue value, out TKey key);
        public IEnumerable<TKey> Keys
        { get; }
        public IEnumerable<TValue> Values
        { get; }
        public IEnumerable<KeyValuePair<TKey, TValue>> KeyValuePairs
        { get; }
        public TValue this[TKey key]
        { get; }
        public TKey this[TValue value]
        { get; }
    }
}