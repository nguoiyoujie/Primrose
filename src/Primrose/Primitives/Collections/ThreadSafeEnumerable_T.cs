using System;
using System.Collections;
using System.Collections.Generic;

namespace Primrose.Primitives.Collections
{
  /// <summary>Represents a thread-safe wrapper over an enumerable collection</summary>
  public struct ThreadSafeEnumerable<T> //: IEnumerable<T>
  {
    private readonly Func<IEnumerator<T>> _func;
    private readonly object _lock;

    /// <summary>Creates a thread-safe wrapper over an enumerable collection</summary>
    /// <param name="inner">The (thread-unsafe) enumerator</param>
    /// <param name="lock">The locking object</param>
    public ThreadSafeEnumerable(IEnumerable<T> inner, object @lock)
    {
      _lock = @lock;
      _func = inner.GetEnumerator;
    }

    /// <summary>Creates a thread-safe wrapper over an enumerable collection</summary>
    /// <param name="func">A function that creates and returns the (thread-unsafe) enumerator</param>
    /// <param name="lock">The locking object</param>
    public ThreadSafeEnumerable(Func<IEnumerator<T>> func, object @lock)
    {
      _lock = @lock;
      _func = func;
    }

    /// <summary>Returns an enumerator that iterates through the collection</summary>
    /// <returns>A System.Collections.Generic.IEnumerator`1 that can be used to iterate through the collection</returns>
    public ThreadSafeEnumerator<T> GetEnumerator()
    {
      return new ThreadSafeEnumerator<T>(_func, _lock);
    }

    //IEnumerator IEnumerable.GetEnumerator()
    //{
    //  return GetEnumerator();
    //}
  }
}
