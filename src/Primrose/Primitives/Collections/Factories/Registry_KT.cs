﻿using Primrose.Primitives.Collections;
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
  public class Registry<K, T> : IRegistry<K, T>, IEnumerable<KeyValuePair<K, T>>
  {
    private readonly object locker = new object();
    private readonly Func<IEnumerator<KeyValuePair<K, T>>> _listEnumerator;

    /// <summary>Creates an object registry</summary>
    public Registry() { list = new Dictionary<K, T>(); _listEnumerator = () => list.GetEnumerator(); }

    /// <summary>Creates an object registry with an initial capacity</summary>
    /// <param name="capacity">The initial capacity of the registry</param>
    public Registry(int capacity) { list = new Dictionary<K, T>(capacity); _listEnumerator = () => list.GetEnumerator(); }

    /// <summary>Creates an object registry that contains elements copied from another registry</summary>
    /// <param name="other">The other registry whose elements are copied to this registry</param>
    public Registry(IRegistry<K, T> other) { list = new Dictionary<K, T>(other.GetUnderlyingDictionary()); _listEnumerator = () => list.GetEnumerator(); }

    /// <summary>Creates an object registry with an equality comparer</summary>
    /// <param name="comparer">The equality comparer for the registry</param>
    public Registry(IEqualityComparer<K> comparer) { list = new Dictionary<K, T>(comparer); _listEnumerator = () => list.GetEnumerator(); }

    /// <summary>Creates an object registry with an initial capacity</summary>
    /// <param name="capacity">The initial capacity of the registry</param>
    /// <param name="comparer">The equality comparer for the registry</param>
    public Registry(int capacity, IEqualityComparer<K> comparer) { list = new Dictionary<K, T>(capacity, comparer); _listEnumerator = () => list.GetEnumerator(); }

    /// <summary>Creates an object registry that contains elements copied from another registry</summary>
    /// <param name="other">The other registry whose elements are copied to this registry</param>
    /// <param name="comparer">The equality comparer for the registry</param>
    public Registry(IRegistry<K, T> other, IEqualityComparer<K> comparer) { list = new Dictionary<K, T>(other.GetUnderlyingDictionary(), comparer); _listEnumerator = () => list.GetEnumerator(); }

    /// <summary>The container data source</summary>
    protected Dictionary<K, T> list;

    /// <summary>The default value returned</summary>
    public T Default = default;

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
    public T Get(K key) { lock (locker) { if (key.Equals(null) || !list.TryGetValue(key, out T t)) return Default; return t; } }

    /// <summary>Retrieves the value associated with a key or a default value if the key is not present</summary>
    /// <param name="key">The identifier key to check</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value associated with the key. If the registry does not contain this key or the key is null, returns defaultValue</returns>
    public T GetD(K key, T defaultValue) { lock (locker) { if (key.Equals(null) || !list.TryGetValue(key, out T t)) return defaultValue; return t; } }

    /// <summary>Strictly retrieves the value associated with a key</summary>
    /// <param name="key">The identifier key to check</param>
    /// <returns>The value associated with the key</returns>
    /// <exception cref="KeyNotFoundException">The registry does not contain this key.</exception>
    public T GetX(K key) { lock (locker) return list[key]; }

    /// <summary>Retrives an array of all the keys in the registry</summary>
    /// <returns></returns>
    public K[] GetKeys() { lock (locker) return list.Keys.ToArray(); }

    /// <summary>Enumerates through the keys in the registry</summary>
    /// <returns></returns>
    public ThreadSafeEnumerable<K> EnumerateKeys() { return new ThreadSafeEnumerable<K>(list.Keys, locker); }

    /// <summary>Retrives an array of all the values in the registry</summary>
    /// <returns></returns>
    public T[] GetValues() { lock (locker) return list.Values.ToArray(); }

    /// <summary>Enumerates through the values in the registry</summary>
    /// <returns></returns>
    public ThreadSafeEnumerable<T> EnumerateValues() { return new ThreadSafeEnumerable<T>(list.Values, locker); }

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
      return new ThreadSafeEnumerator<KeyValuePair<K, T>>(_listEnumerator, locker);
    }

    /// <summary>Retrieves the enumerator for the registry</summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return new ThreadSafeEnumerator(_listEnumerator, locker);
    }

    /// <summary>The number of elements in this registry</summary>
    public int Count { get { lock (locker) return list.Count; } }
  }
}
