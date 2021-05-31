using System.Collections;
using System.Collections.Generic;

namespace Primrose.Primitives.Collections
{
  /// <summary>Represents a thread-safe wrapper over an enumerable collection</summary>
  public class ThreadSafeEnumerable<T> : IEnumerable<T>
  {
    private readonly IEnumerable<T> m_Inner;
    private readonly object m_Lock;

    /// <summary>Creates a thread-safe wrapper over an enumerable collection</summary>
    /// <param name="inner">The (thread-unsafe) enumerator</param>
    /// <param name="lock">The locking object</param>
    public ThreadSafeEnumerable(IEnumerable<T> inner, object @lock)
    {
      m_Lock = @lock;
      m_Inner = inner;
    }

    /// <summary>Returns an enumerator that iterates through the collection</summary>
    /// <returns>A System.Collections.Generic.IEnumerator`1 that can be used to iterate through the collection</returns>
    public IEnumerator<T> GetEnumerator()
    {
      IEnumerable<T> inner = m_Inner;
      return new ThreadSafeEnumerator<T>(() => inner.GetEnumerator(), m_Lock);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
