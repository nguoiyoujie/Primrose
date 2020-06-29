using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;

namespace Primrose.Primitives
{
  /// <summary>
  /// Provides a basic thread-safe paired dictionary for multithreaded updates  
  /// </summary>
  /// <typeparam name="T">The item type to be stored as keys in this dictionary</typeparam>
  /// <typeparam name="U">The item type to be stored as values in this dictionary</typeparam>
  public class ThreadSafeDictionary<T, U>
  {
    private readonly object locker_write = new object();
    private readonly object locker_modify = new object();
    private Dictionary<T, U> _list = new Dictionary<T, U>();
    private readonly Dictionary<T, U> _pending_list = new Dictionary<T, U>();
    private bool _dirty = true;

    /// <summary>
    /// Defines whether updates should be triggered explicitly. If true, call SetDirty() to update.
    /// </summary>
    public bool ExplicitUpdateOnly = false;

    /// <summary>
    /// Creates a thread-safe dictionary
    /// </summary>
    /// <param name="dict">The initial dictionary source</param>
    public ThreadSafeDictionary(IDictionary<T, U> dict = null) { if (dict != null) _pending_list = new Dictionary<T, U>(dict); }

    /// <summary>Retrieves the number of elements in the dictionary</summary>
    public int Count
    {
      get
      {
        Update();
        return _list.Count;
      }
    }

    /// <summary>Returns whether the key exists in the dictionary</summary>
    /// <param name="key">The key to check</param>
    /// <returns>True if the key exists, false otherwise</returns>
    public bool ContainsKey(T key)
    {
      Update();
      return _list.ContainsKey(key);
    }

    /// <summary>Returns whether the value exists in the dictionary</summary>
    /// <param name="value">The value to check</param>
    /// <returns>True if the value exists, false otherwise</returns>
    public bool ContainsValue(U value)
    {
      Update();
      return _list.ContainsValue(value);
    }

    /// <summary>Retrieves a value using a key</summary>
    /// <param name="key">The key to check</param>
    /// <returns>Returns the value associated with this key</returns>
    public U this[T key]
    {
      get { return Get(key); }
      set { Set(key, value); }
    }

    /// <summary>Obtains last updated collection</summary>
    /// <returns></returns>
    public IDictionary<T, U> GetDictionary()
    {
      Update();
      return new Dictionary<T, U>(_list);
    }

    /// <summary>Obtains last updated keys</summary>
    /// <returns></returns>
    public IEnumerable<T> Keys
    {
      get
      {
        Update();
        return _list.Keys;
      }
    }

    /// <summary>Obtains last updated values</summary>
    /// <returns></returns>
    public IEnumerable<U> Values
    {
      get
      {
        Update();
        return _list.Values;
      }
    }

    /// <summary>Retrieves the value associated with this key</summary>
    /// <param name="key">The key to check</param>
    /// <returns></returns>
    public U Get(T key)
    {
      Update();
      _list.TryGetValue(key, out U ret);
      return ret;
    }

    private void Update()
    {
      lock (locker_write)
        if (_dirty)
        {
          _list = new Dictionary<T, U>(_pending_list);
          _dirty = false;
        }
    }

    /// <summary>Explicity triggers the list for update</summary>
    public void SetDirty()
    {
      lock (locker_write)
        _dirty = true;
    }

    /// <summary>Forces a refresh</summary>
    public void Refresh()
    {
      lock (locker_write)
      {
        _list = new Dictionary<T, U>(_pending_list);
      }
    }

    /// <summary>Adds an item to the collection</summary>
    public void Add(T key, U value)
    {
      try
      {
        lock (locker_write)
        {
          _pending_list.Add(key, value);
          if (!ExplicitUpdateOnly)
            _dirty = true;
        }
      }
      catch (ArgumentNullException ex)
      {
        throw new ArgumentNullException(Properties.Resources.Error_CollectionAddNullKey.F(GetType().Name), ex);
      }
      catch (ArgumentException ex)
      {
        throw new ArgumentException(Properties.Resources.Error_CollectionAddDuplicateKey.F(key, GetType().Name), ex);
      }
    }

    /// <summary>Sets an item to the collection</summary>
    public void Set(T key, U value)
    {
      try
      {
        lock (locker_write)
        {
          _pending_list[key] = value;
          if (!ExplicitUpdateOnly)
            _dirty = true;
        }
      }
      catch (ArgumentNullException ex)
      {
        throw new ArgumentNullException(Properties.Resources.Error_CollectionSetNullKey.F(GetType().Name), ex);
      }
      catch (KeyNotFoundException ex)
      {
        throw new ArgumentException(Properties.Resources.Error_CollectionSetKeyNotFound.F(key, GetType().Name), ex);
      }
    }

    /// <summary>Adds or Sets an item to the collection</summary>
    public void Put(T key, U value)
    {
      try
      {
        lock (locker_write)
        {
          if (_pending_list.ContainsKey(key))
            _pending_list[key] = value;
          else
            _pending_list.Add(key, value);

          if (!ExplicitUpdateOnly)
            _dirty = true;
        }
      }
      catch (ArgumentNullException ex)
      {
        throw new ArgumentNullException(Properties.Resources.Error_CollectionPutNullKey.F(GetType().Name), ex);
      }
    }

    /// <summary>Clears the collection</summary>
    public void Clear()
    {
      lock (locker_write)
      {
        _pending_list.Clear();
        if (!ExplicitUpdateOnly)
          _dirty = true;
      }
    }

    /// <summary>Removes an item from the collection</summary>
    public bool Remove(T key)
    {
      bool ret = false;
      lock (locker_write)
      {
        if (_pending_list.ContainsKey(key))
          ret = _pending_list.Remove(key);

        if (!ExplicitUpdateOnly)
          _dirty = true;
      }
      return ret;
    }

    /// <summary>
    /// Performs a thread-safe modification on a value using an index
    /// </summary>
    /// <param name="key">The key to check</param>
    /// <param name="func">The modify function</param>
    public void Modify(T key, Func<U, U> func)
    {
      lock (locker_modify)
      {
        Set(key, func(Get(key)));
      }
    }
  }
}
