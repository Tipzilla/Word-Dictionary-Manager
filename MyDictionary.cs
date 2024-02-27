using System;
using System.Collections.Generic;

namespace Word_Dictionary_Manager
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
            if (entries.Any(entry => EqualityComparer<TKey>.Default.Equals(entry.Key, key)))
            {
                entries.RemoveAll(entry => EqualityComparer<TKey>.Default.Equals(entry.Key, key));
                Console.WriteLine($"Word '{key}' deleted from the dictionary.");
            }
            else
            {
                Console.WriteLine($"Word '{key}' not found in the dictionary.");
            }
        }

        public void Clear()
        {
            entries.Clear();
        }


        public void Print()
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
            }
        }

        public List<KeyValuePair<TKey, TValue>> GetEntries()
        {
            return entries;
        }
    }
}
