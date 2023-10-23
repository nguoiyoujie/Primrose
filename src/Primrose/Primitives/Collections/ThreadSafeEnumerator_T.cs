using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Primrose.Primitives.Collections
{
  /// <summary>Represents a thread-safe wrapper over an enumerator</summary>
  /// <typeparam name="T">The enumerated type</typeparam>
  public struct ThreadSafeEnumerator<T> : IEnumerator<T>
  {
    // Credit: Adapted from https://www.codeproject.com/articles/56575/thread-safe-enumeration-in-c

    // this is the (thread-unsafe) enumerator of the underlying collection
    private IEnumerator<T> m_Inner; // do not use readonly, see ThreadSafeEnumerator<TEnumerator, T>
    private readonly object m_Lock;

    /// <summary>Creates a thread-safe wrapper over an enumerator</summary>
    /// <param name="inner">The (thread-unsafe) enumerator</param>
    /// <param name="lock">The locking object</param>
    public ThreadSafeEnumerator(Func<IEnumerator<T>> inner, object @lock)
    {
      m_Lock = @lock;
      // entering lock in constructor
      Monitor.Enter(m_Lock);
      m_Inner = inner();
    }

    /// <summary>Disposes the wrapper and releases the lock</summary>
    public void Dispose()
    {
      // .. and exiting lock on Dispose()
      // This will be called when foreach loop finishes
      Monitor.Exit(m_Lock);
    }

    // we just delegate actual implementation
    // to the inner enumerator, that actually iterates
    // over some collection

    /// <summary>Advances the enumerator to the next element of the collection</summary>
    /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection</returns>
    public bool MoveNext()
    {
      return m_Inner.MoveNext();
    }

    /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection</summary>
    public void Reset()
    {
      m_Inner.Reset();
    }

    /// <summary>Gets the current element in the collection</summary>
    public T Current
    {
      get { return m_Inner.Current; }
    }

    object IEnumerator.Current => Current;
  }
}
