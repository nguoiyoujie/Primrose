using Primrose.Primitives.Collections;
using System;
using System.Collections.Generic;

namespace Primrose.Primitives.Factories
{
  /// <summary>
  /// Allows creation of objects and stores them automatically. Limited to objects with parameterless constructors; for others, use Registry
  /// </summary>
  /// <typeparam name="K">The key type to be stored</typeparam>
  /// <typeparam name="T">The object type to be stored</typeparam>
  public class StructArrayFactory<K, T> : IFactory<K, T> where T : ISetIdentity<K>
  {
    /// <summary>The internal array storage backing the array factory</summary>
    protected ResizableArray<T> _array = new ResizableArray<T>();

    /// <summary>The internal key reference backing the array factory</summary>
    protected Registry<K, int> _ref = new Registry<K, int>() { Default = -1 };

    /// <summary>Determines if removed elements should be preserved for reuse</summary>
    public bool ReuseRemovedElements { get; set; } = true;

    /// <summary>The number of active elements in the array</summary>
    public int Count { get { return _array.Count; } }

    /// <summary>The number of reusable elements in the array</summary>
    public int ReusableCount { get { return _maxCount - _array.Count; } }

    private int _maxCount = 0;
    private List<K> _pendingIndexChange = new List<K>();

    /// <summary>
    /// Creates a new object and stores its reference in its internal registry
    /// </summary>
    /// <param name="id">The identifier for the object</param>
    /// <returns></returns>
    public T Create(K id)
    {
      T ret = default;
      ret.ID = id;
      Add(id, ret);
      return ret;
    }

    /// <summary>
    /// Creates a new object and stores its reference in its internal registry
    /// </summary>
    /// <param name="id">The identifier for the object</param>
    /// <param name="initFn">The initializer function</param>
    /// <returns></returns>
    public T Create(K id, ActionRefDelegate<T> initFn)
    {
      T ret = default;
      ret.ID = id;
      initFn(ref ret);
      Add(id, ret);
      return ret;
    }

    /// <summary>Retrieves the value associated with a key</summary>
    /// <param name="key">The identifier key to check</param>
    /// <returns>The value associated with the key. If the registry does not contain this key, returns Default</returns>
    public T Get(K key)
    {
      int index = _ref[key];
      if (index != -1)
      {
        return _array[index];
      }
      return default;
    }

    /// <summary>Performs an action on a specified index of the internal array</summary>
    /// <param name="key">The identifier key to be accessed</param>
    /// <param name="action">The action to be performed</param>
    public virtual void Do(K key, ActionRefDelegate<T> action)
    {
      int index = _ref[key];
      if (index != -1)
      {
        action.Invoke(ref _array.GetRef(index));
      }
    }

    /// <summary>Performs an action on a specified index of the internal array</summary>
    /// <param name="index">The indext to be accessed</param>
    /// <param name="action">The action to be performed</param>
    public virtual void DoIndex(int index, ActionRefDelegate<T> action)
    {
      if (index < 0 || index < _array.Count)
      {
        throw new ArgumentException("Attempted to access an element with an invalid index");
      }
      action.Invoke(ref _array.GetRef(index));
    }

    /// <summary>Performs an action for all elements in the internal array</summary>
    /// <param name="action">The action to be performed</param>
    public virtual void DoEach(ActionRefDelegate<T> action)
    {
      for (int index = 0; index < _array.Count; index++)
      {
        action.Invoke(ref _array.GetRef(index));
      }
    }

    /// <summary>Retrieves the underlying dictionary</summary>
    public IDictionary<K, T> GetUnderlyingDictionary()
    {
      throw new NotSupportedException();
    }

    /// <summary>Adds an object into the registry</summary>
    /// <param name="key">The identifier key to add</param>
    /// <param name="item">The object to be associated with this key</param>
    public void Add(K key, T item)
    {
      _array.Add(item);
      _ref[key] = _array.Count - 1;
      _maxCount = _array.Count;
    }

    /// <summary>Updates or adds an object into the registry</summary>
    /// <param name="key">The identifier key to add</param>
    /// <param name="item">The object to be associated with this key</param>
    public void Put(K key, T item)
    {
      int index = _ref[key];
      if (index != -1)
      {
        _array[index] = item;
      }
      else
      {
        Add(key, item);
      }
    }

    /// <summary>Removes an object from the registry</summary>
    /// <param name="key">The identifier key to remove</param>
    public bool Remove(K key)
    {
      int index = _ref[key];
      if (index != -1)
      {
        int lastindex = _array.Count - 1;
        if (lastindex != -1)
        {
          if (ReuseRemovedElements)
          {
            // do a swap
            T item = _array[index];
            _array[index] = _array[lastindex];
            _array[lastindex] = item;
          }
          else
          {
            _array[index] = _array[lastindex];
            _array[lastindex] = default;
            _maxCount--;
          }
          _array.Resize(_array.Count - 1);
          _pendingIndexChange.Clear();
          foreach (K k in _ref.EnumerateKeys())
          {
            if (_ref[k] == lastindex)
            {
              _pendingIndexChange.Add(k);
            }
          }
          foreach (K k in _pendingIndexChange)
          {
            _ref[k] = index;
          }
        }
      }
      return _ref.Remove(key);
    }

    /// <summary>Determines whether the registry contains a key</summary>
    /// <param name="key">The identifier key to check</param>
    /// <returns>True if the registry contains this key, False if otherwise</returns>
    public bool Contains(K key) { return _ref.Contains(key); }

    /// <summary>Retrives an array of all the keys in the registry</summary>
    /// <returns></returns>
    public K[] GetKeys() { return _ref.GetKeys(); }

    /// <summary>Retrives an array of all the values in the registry</summary>
    /// <returns></returns>
    public T[] GetValues() 
    { 
      T[] ret = new T[_array.Count]; 
      Array.Copy(_array.InternalArray, ret, ret.Length);
      return ret;
    }

    /// <summary>Enumerates through the values in the registry</summary>
    /// <returns></returns>
    public ArrayEnumerator<T> EnumerateValues() { return new ArrayEnumerator<T>(_array.InternalArray); }

    /// <summary>Enumerates through the values in the registry</summary>
    /// <returns></returns>
    public ArrayEnumerator<T> EnumerateValues(int startindex, int endindex = -1) { return new ArrayEnumerator<T>(_array.InternalArray, startindex, endindex); }

    /// <summary>Purges all data from the registry</summary>
    public void Clear()
    {
      _array.Clear();
      _ref.Clear();
    }

  }
}
