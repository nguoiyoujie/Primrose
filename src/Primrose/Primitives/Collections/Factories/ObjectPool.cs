using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;

namespace Primrose.Primitives.Factories
{
  /// <summary>
  /// Provides a basic object pool for pooling objects for further use  
  /// </summary>
  /// <typeparam name="T">The item type to be pooled</typeparam>
  public partial class ObjectPool<T> : IPool where T : class
  {
    /// <summary>
    /// Creates an object pool
    /// </summary>
    /// <param name="useLocalPool">Defines whether objects shall be retrieved from the global pool or a local pool tied to this pool object</param>
    /// <param name="createFn">The function for creating new instances</param>
    /// <param name="resetFn">The function for reseting instances that are returned to the pool</param>
    /// <exception cref="ArgumentNullException">createFn cannot be null</exception>
    public ObjectPool(bool useLocalPool, Func<T> createFn, Action<T> resetFn = null)
    {
      list = useLocalPool ? new Stack<T>(256) : _staticlist;
      creator = createFn ?? throw new ArgumentNullException(nameof(createFn));
      resetor = resetFn;
    }

    // use a Dictionary<T,T> instead of Stack<T>
    private Stack<T> list;
    private readonly Func<T> creator;
    private readonly Action<T> resetor;
    private readonly object locker = new object();

    /// <summary>Retrieves the number of elements in the pool</summary>
    public int Count { get { lock (locker) { return list.Count; } } }

    /// <summary>Retrieves the number of generated elements in the pool that were not yet collected</summary>
    public int UncollectedCount { get; private set; }

    /// <summary>Defines whether objects shall be retrieved from the global pool or a local pool tied to this pool object</summary>
    public bool UseLocalPool { get { return list != _staticlist; } }

    /// <summary>Returns an instance of <typeparamref name="T"/> from the pool, or creates a new instance if the pool is empty</summary>
    /// <returns>An instance of <typeparamref name="T"/> from the pool, or created from the creator function if the pool is empty</returns>
    public T GetNew()
    {
      lock (locker)
      {
        UncollectedCount++;
        //T item = null;
        //foreach (T t in list)
        //{
        //  // grab any item
        //  item = t;
        //  break;
        //}
        //if (item != null)
        //{
        //  // grabbed one
        //  list.Remove(item);
        //  return item;
        //}
        if (list.Count > 0)
        {
          return list.Pop();
        }
        return creator();
      }
    }

    /// <summary>Returns an instance of <typeparamref name="T"/> to the pool.</summary>
    /// <param name="item">The object to be returned to the pool</param>
    public void Return(T item)
    {
      resetor?.Invoke(item);
      lock (locker)
      {
#if DEBUG
        // do not admit the item if it is already in the pool
        //if (list.Contains(item)) // HashSet<T>.Contains() is much faster than Stack<T>.Contains();
        //  throw new ArgumentException(Resource.Strings.Error_ObjectPoolDuplicateItem.F(typeof(T).Name));
#endif
        list.Push(item);
        UncollectedCount--;
      }
    }

    /// <summary>Removes all instances from the pool</summary>
    public void Clear()
    {
      // this avoids allocation, but this isn't cheap, but generally object pools do not require explicit clearing.
      // A future consideration for conditional clearing will require iteration anyway. 
      //while (list.TryDequeue(out _)) { }
      // list = new ConcurrentQueue<T>();
      lock (locker)
      {
        list.Clear();
      }
    }
  }
}
