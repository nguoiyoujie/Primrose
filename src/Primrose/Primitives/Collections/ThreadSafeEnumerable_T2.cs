using System;
using System.Collections;
using System.Collections.Generic;

namespace Primrose.Primitives.Collections
{
  /// <summary>Represents a thread-safe wrapper over an enumerable collection</summary>
  public struct ThreadSafeEnumerable<TEnumerator, T> //: IEnumerable<T>
    where TEnumerator : IEnumerator<T>
  {
    private readonly Func<TEnumerator> _func;
    private readonly object _lock;

    /// <summary>Creates a thread-safe wrapper over an enumerable collection</summary>
    /// <param name="func">A function that creates and returns the (thread-unsafe) enumerator</param>
    /// <param name="lock">The locking object</param>
    public ThreadSafeEnumerable(Func<TEnumerator> func, object @lock)
    {
      _lock = @lock;
      _func = func;
    }

    /// <summary>Returns an enumerator that iterates through the collection</summary>
    /// <returns>The enumerator that can be used to iterate through the collection</returns>
    public ThreadSafeEnumerator<TEnumerator, T> GetEnumerator()
    {
      return new ThreadSafeEnumerator<TEnumerator, T>(_func, _lock);
    }

    //IEnumerator IEnumerable.GetEnumerator()
    //{
    //  return GetEnumerator();
    //}
  }
}
