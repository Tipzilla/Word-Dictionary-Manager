using System;
using System.Collections.Generic;

namespace Assessment_1
{
    public class MyDictionary<TKey, TValue>
    {
        private List<KeyValuePair<TKey, TValue>> entries;

        public MyDictionary()
        {
            entries = new List<KeyValuePair<TKey, TValue>>();
        }

        public void Add(TKey key, TValue value)
        {
            entries.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public bool ContainsKey(TKey key)
        {
            return entries.Any(entry => EqualityComparer<TKey>.Default.Equals(entry.Key, key));
        }

        public TValue Find(TKey key)
        {
            var entry = entries.FirstOrDefault(e => EqualityComparer<TKey>.Default.Equals(e.Key, key));
            return entry.Value;
        }

        public void Delete(TKey key)
        {
            entries.RemoveAll(entry => EqualityComparer<TKey>.Default.Equals(entry.Key, key));
        }

        public void Print()
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
            }
        }

        // Public method to get entries
        public List<KeyValuePair<TKey, TValue>> GetEntries()
        {
            return entries;
        }
    }
}
