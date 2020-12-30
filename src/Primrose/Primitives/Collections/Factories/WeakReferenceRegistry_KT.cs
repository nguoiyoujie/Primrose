using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Primrose.Primitives.Factories
{
  /// <summary>
  /// Maintains a typed registry of objects.
  /// </summary>
  /// <typeparam name="K">The type of the key</typeparam>
  /// <typeparam name="T">The type of the registered object</typeparam>
  public class WeakReferenceRegistry<K, T> : Registry<K, WeakReference<T>> where T : class
  {
    private readonly List<T> _active = new List<T>();
    private int _mark;

    /// <summary>Marks the object as active and holds it with a strong reference</summary>
    /// <param name="key">The identifier key</param>
    /// <param name="item">The object associated with this key</param>
    public bool Mark(K key,  out T item)
    {
      item = null;
      if (Get(key)?.TryGetTarget(out item) ?? false)
      {
        _active.Add(item);
        return true;
      }
      return false;
    }

    /// <summary>Unmarks all active objects. Call SetUnmarkedForCollection to allow collection of previously active objects</summary>
    /// <param name="allowCollection">If true, allows collection of objects immediately</param>
    public void Unmark(bool allowCollection)
    {
      if (allowCollection)
      {
        _active.Clear();
        _mark = 0;
      }
      else
      {
        _mark = _active.Count;
      }
    }

    /// <summary>Allows collection of previously active objects that are now unmarked</summary>
    public void SetUnmarkedForCollection()
    {
      _active.RemoveRange(0, _mark);
      _mark = 0;
    }
  }
}
