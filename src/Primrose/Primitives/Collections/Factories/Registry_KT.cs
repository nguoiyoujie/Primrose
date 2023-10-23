using Primrose.Primitives.Collections;
using Primrose.Primitives.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Primrose.Primitives.Factories
{
  /// <summary>
  /// Maintains a typed registry of objects.
  /// </summary>
  /// <typeparam name="K">The type of the key</typeparam>
  /// <typeparam name="T">The type of the registered object</typeparam>
  public class Registry<K, T> : IRegistry<K, T>, IEnumerable<KeyValuePair<K, T>>, ICollection<KeyValuePair<K, T>>
  {
    private readonly object locker = new object();

    // 2021.06.02: Create fixed funcs in constructor to eliminate repeat allocation of Func objects.
    private readonly Func<Dictionary<K, T>.Enumerator> _listEnumerator;
    private readonly Func<Dictionary<K, T>.KeyCollection.Enumerator> _keyEnumerator;
    private readonly Func<Dictionary<K, T>.ValueCollection.Enumerator> _valueEnumerator;

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation
    private Dictionary<K, T>.Enumerator GetListEnumerator() { return list.GetEnumerator(); }
    private Dictionary<K, T>.KeyCollection.Enumerator GetKeysEnumerator() { return list.Keys.GetEnumerator(); }
    private Dictionary<K, T>.ValueCollection.Enumerator GetValuesEnumerator() { return list.Values.GetEnumerator(); }
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation

    /// <summary>Creates an object registry</summary>
    public Registry() : this(new Dictionary<K, T>()) { }

    /// <summary>Creates an object registry with an initial capacity</summary>
    /// <param name="capacity">The initial capacity of the registry</param>
    public Registry(int capacity) : this(new Dictionary<K, T>(capacity)) { }

    /// <summary>Creates an object registry that contains elements copied from another registry</summary>
    /// <param name="other">The other registry whose elements are copied to this registry</param>
    public Registry(IRegistry<K, T> other) : this(new Dictionary<K, T>(other.GetUnderlyingDictionary())) { }

    /// <summary>Creates an object registry with an equality comparer</summary>
    /// <param name="comparer">The equality comparer for the registry</param>
    public Registry(IEqualityComparer<K> comparer) : this(new Dictionary<K, T>(comparer)) { }

    /// <summary>Creates an object registry with an initial capacity</summary>
    /// <param name="capacity">The initial capacity of the registry</param>
    /// <param name="comparer">The equality comparer for the registry</param>
    public Registry(int capacity, IEqualityComparer<K> comparer) : this(new Dictionary<K, T>(capacity, comparer)) { }

    /// <summary>Creates an object registry that contains elements copied from another registry</summary>
    /// <param name="other">The other registry whose elements are copied to this registry</param>
    /// <param name="comparer">The equality comparer for the registry</param>
    public Registry(IRegistry<K, T> other, IEqualityComparer<K> comparer) : this(new Dictionary<K, T>(other.GetUnderlyingDictionary(), comparer)) { }

    /// <summary>Creates an object registry that uses the given backing dictionary</summary>
    /// <param name="dictionary">The backing dictionary for this registry</param>
    public Registry(Dictionary<K, T> dictionary)
    {
      list = dictionary ?? new Dictionary<K, T>();
      _listEnumerator = GetListEnumerator;
      _keyEnumerator = GetKeysEnumerator;
      _valueEnumerator = GetValuesEnumerator;
    }

    /// <summary>The container data source</summary>
    protected Dictionary<K, T> list;

    /// <summary>The default value returned</summary>
    public T Default = default;

    /// <summary>Retrieves the underlying dictionary in its concrete form. Useful if avoiding allocations due to IDictionary cast is needed. Use it only when you know what you are doing</summary>
    public Dictionary<K, T> GetUnderlyingConcreteDictionary() { return list; }

    /// <summary>Retrieves the underlying dictionary</summary>
    public IDictionary<K, T> GetUnderlyingDictionary() { return list; }

    /// <summary>Determines whether the registry contains a key</summary>
    /// <param name="key">The identifier key to check</param>
    /// <returns>True if the registry contains this key, False if otherwise</returns>
    public bool Contains(K key) { lock (locker) return list.ContainsKey(key); }

    /// <summary>Retrieves the value associated with a key index</summary>
    /// <param name="id">The identifier key to check</param>
    public T this[K id] { get { return Get(id); } set { Put(id, value); } }

    /// <summary>Retrieves the value associated with a key</summary>
    /// <param name="key">The identifier key to check</param>
    /// <returns>The value associated with the key. If the registry does not contain this key or the key is null, returns Default</returns>
    public virtual T Get(K key) { lock (locker) { if ((!typeof(K).IsValueType && EqualityComparer<K>.Default.Equals(key, default)) || !list.TryGetValue(key, out T t)) return Default; return t; } }

    /// <summary>Retrieves the value associated with a key or a default value if the key is not present</summary>
    /// <param name="key">The identifier key to check</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value associated with the key. If the registry does not contain this key or the key is null, returns defaultValue</returns>
    public virtual T GetD(K key, T defaultValue) { lock (locker) { if ((!typeof(K).IsValueType && EqualityComparer<K>.Default.Equals(key, default)) || !list.TryGetValue(key, out T t)) return defaultValue; return t; } }

    /// <summary>Strictly retrieves the value associated with a key</summary>
    /// <param name="key">The identifier key to check</param>
    /// <returns>The value associated with the key</returns>
    /// <exception cref="KeyNotFoundException">The registry does not contain this key.</exception>
    public virtual T GetX(K key) { lock (locker) return list[key]; }

    /// <summary>Retrives an array of all the keys in the registry</summary>
    /// <returns></returns>
    public K[] GetKeys() { lock (locker) return list.Keys.ToArray(); }

    /// <summary>Enumerates through the keys in the registry</summary>
    /// <returns></returns>
    public ThreadSafeEnumerable<Dictionary<K, T>.KeyCollection.Enumerator, K> EnumerateKeys() { return new ThreadSafeEnumerable<Dictionary<K, T>.KeyCollection.Enumerator, K>(_keyEnumerator, locker); }

    /// <summary>Retrives an array of all the values in the registry</summary>
    /// <returns></returns>
    public T[] GetValues() { lock (locker) return list.Values.ToArray(); }

    /// <summary>Enumerates through the values in the registry</summary>
    /// <returns></returns>
    public ThreadSafeEnumerable<Dictionary<K, T>.ValueCollection.Enumerator, T> EnumerateValues() { return new ThreadSafeEnumerable<Dictionary<K, T>.ValueCollection.Enumerator, T>(_valueEnumerator, locker); }

    /// <summary>Adds an object into the registry</summary>
    /// <param name="key">The identifier key to add</param>
    /// <param name="item">The object to be associated with this key</param>
    public virtual void Add(K key, T item) { lock (locker) list.Add(key, item); }

    /// <summary>Updates or adds an object into the registry</summary>
    /// <param name="key">The identifier key to add</param>
    /// <param name="item">The object to be associated with this key</param>
    public virtual void Put(K key, T item) { lock (locker) list.Put(key, item); }

    /// <summary>Removes an object from the registry</summary>
    /// <param name="key">The identifier key to remove</param>
    public virtual bool Remove(K key) { lock (locker) return list.Remove(key); }

    /// <summary>Purges all data from the registry</summary>
    public virtual void Clear() { lock (locker) list.Clear(); }

    /// <summary>Retrieves the enumerator for the registry</summary>
    public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
    {
      return new ThreadSafeEnumerator<Dictionary<K, T>.Enumerator, KeyValuePair<K, T>>(_listEnumerator, locker);
    }

    /// <summary>Retrieves the enumerator for the registry. This thread-unsafe way allows for the enumerator to be retrieved without allocation</summary>
    public Dictionary<K, T>.Enumerator GetEnumeratorDirect()
    {
      return list.GetEnumerator();
    }

    /// <summary>Retrieves the enumerator for the registry</summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return new ThreadSafeEnumerator<Dictionary<K, T>.Enumerator, KeyValuePair<K, T>>(_listEnumerator, locker);
    }

    /// <summary>Updates or adds an object into the registry</summary>
    /// <param name="item">The key-value pair to be added</param>
    public void Add(KeyValuePair<K, T> item)
    {
      Add(item.Key, item.Value);
    }

    /// <summary>Determines whether the registry contains the key and value pair</summary>
    /// <param name="item">The key-value pair to be compared to</param>
    public bool Contains(KeyValuePair<K, T> item)
    {
      return Contains(item.Key) && (EqualityComparer<T>.Default.Equals(list[item.Key], item.Value));
    }

    /// <summary>Copies an array of objects into the registry</summary>
    /// <param name="array">The array of objects to be copied</param>
    /// <param name="arrayIndex">The index to copy to</param>
    public void CopyTo(KeyValuePair<K, T>[] array, int arrayIndex)
    {
      foreach (var item in array)
      {
        Put(item.Key, item.Value);
      }
    }

    /// <summary>Removes an object from the registry</summary>
    /// <param name="item">The key-value pair to be removed</param>
    public bool Remove(KeyValuePair<K, T> item)
    {
      return Contains(item) && Remove(item.Key);
    }

    /// <summary>The number of elements in this registry</summary>
    public int Count { get { lock (locker) return list.Count; } }

    /// <summary>Determines if this collection is read-only</summary>
    public bool IsReadOnly => false;
  }
}
