using Primrose.Primitives.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Primrose.Primitives.Factories
{
  /// <summary>
  /// Maintains a typed registry of objects.
  /// </summary>
  /// <typeparam name="K">The type of the key</typeparam>
  /// <typeparam name="T">The type of the registered object</typeparam>
  public class SortedRegistry<K, T> : IRegistry<K, T>
  {
    private readonly object locker = new object();

    /// <summary>Creates an object registry</summary>
    public SortedRegistry() { list = new SortedDictionary<K, T>(Comparer<K>.Default); }

    /// <summary>Creates an object registry with a sorting comparer</summary>
    /// <param name="comparer">The sorting comparer for the registry</param>
    public SortedRegistry(IComparer<K> comparer) { list = new SortedDictionary<K, T>(comparer); }

    /// <summary>Creates an object registry that contains elements copied from another registry</summary>
    /// <param name="dictionary">The other dictionary whose elements are copied to this registry</param>
    public SortedRegistry(IDictionary<K, T> dictionary) { list = new SortedDictionary<K, T>(dictionary); }

    /// <summary>Creates an object registry that contains elements copied from another registry</summary>
    /// <param name="dictionary">The other dictionary whose elements are copied to this registry</param>
    /// <param name="comparer">The sorting comparer for the registry</param>
    public SortedRegistry(IDictionary<K, T> dictionary, IComparer<K> comparer) { list = new SortedDictionary<K, T>(dictionary, comparer); }

    /// <summary>Creates an object registry that contains elements copied from another registry</summary>
    /// <param name="other">The other registry whose elements are copied to this registry</param>
    public SortedRegistry(IRegistry<K, T> other) { list = new SortedDictionary<K, T>(other.GetUnderlyingDictionary()); }

    /// <summary>Creates an object registry that contains elements copied from another registry</summary>
    /// <param name="other">The other registry whose elements are copied to this registry</param>
    /// <param name="comparer">The sorting comparer for the registry</param>
    public SortedRegistry(IRegistry<K, T> other, IComparer<K> comparer) { list = new SortedDictionary<K, T>(other.GetUnderlyingDictionary(), comparer); }

    /// <summary>The container data source</summary>
    protected readonly SortedDictionary<K, T> list;

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
    public T Get(K key) { lock (locker) { if (key == null || !list.TryGetValue(key, out T t)) return Default; return t; } }

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
    public IEnumerable<K> EnumerateKeys() { return list.Keys; }

    /// <summary>Retrives an array of all the values in the registry</summary>
    /// <returns></returns>
    public T[] GetValues() { lock (locker) return list.Values.ToArray(); }

    /// <summary>Enumerates through the values in the registry</summary>
    /// <returns></returns>
    public IEnumerable<T> EnumerateValues() { return list.Values; }

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
    public virtual void Remove(K key) { lock (locker) list.Remove(key); }

    /// <summary>Purges all data from the registry</summary>
    public virtual void Clear() { lock (locker) list.Clear(); }

    /// <summary>The number of elements in this registry</summary>
    public int Count { get { lock (locker) return list.Count; } }
  }
}
