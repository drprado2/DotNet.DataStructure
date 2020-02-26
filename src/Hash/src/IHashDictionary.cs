using System.Collections.Generic;

namespace DotNet.DataStructure.Hash
{
    public interface IHashDictionary<TKey, TData>
    {
        IEnumerable<TKey> GetKeys();

        IEnumerable<KeyValuePair<TKey, TData>> GetPairs();
        
        IEnumerable<TData> GetDatas();

        TData GetAt(TKey key);

        bool TryGetAt(TKey key, out TData data);

        bool ContainsKey(TKey key);
        
        TData this[TKey key] { get; set; }

        bool Remove(TKey key);
        
        void AddOrUpdate(TKey key, TData value);
    }
}