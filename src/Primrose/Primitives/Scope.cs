﻿using Primrose.Primitives.Factories;
using System;
using System.Collections.Concurrent;

namespace Primrose.Primitives
{
  /// <summary>
  /// Attaches objects to a scope
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public static class ScopedManager<T> where T : class
  {
    static ScopedManager()
    {
      ObjectPool<ScopedItem>.CreateStaticPool(() => new ScopedItem());
    }

    //private static readonly ObjectPool<ScopedItem> pool;

    /// <summary>The number of scope objects in the pool</summary>
    public static int PoolCount { get { return ObjectPool<ScopedItem>.GetStaticPool().Count; } }

    /// <summary>
    /// Attaches a scope to an item
    /// </summary>
    /// <param name="item">The item to be scoped</param>
    /// <returns>The assigned scope</returns>
    public static ScopedItem Scope(T item)
    {
      if (item == null)
        return null;

      ScopedItem e = ObjectPool<ScopedItem>.GetStaticPool().GetNew();
      e.ScopeOne(item);
      return e;
    }

    /// <summary>
    /// Checks a scope to an item
    /// </summary>
    /// <param name="item">The item to be scoped</param>
    /// <returns>The reference counter in the scope</returns>
    public static int Check(T item)
    {
      return ScopedItem.Check(item);
    }

    /// <summary>Represents a scope</summary>
    public class ScopedItem : IDisposable
    {
      private static readonly ConcurrentDictionary<T, int> _counter = new ConcurrentDictionary<T, int>();
      /// <summary>The item in scope</summary>
      public T Value { get; private set; }

      internal static int Check(T item)
      {
        _counter.TryGetValue(item, out int ret);
        return ret;
      }

      internal void ScopeOne(T item)
      {
        Value = item;

        _counter.TryGetValue(item, out int i);
        i++;
        _counter[item] = i;
      }

      private void ReleaseOne()
      {
        if (Value != null)
        {
          _counter.TryGetValue(Value, out int i);
          i--;
          _counter[Value] = i;
          Value = null;
        }
      }

      void IDisposable.Dispose()
      {
        ReleaseOne();
        GC.SuppressFinalize(this);
      }
    }
  }
}
