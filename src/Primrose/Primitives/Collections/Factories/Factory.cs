using Primrose.Primitives.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace Primrose.Primitives.Factories
{
  /// <summary>
  /// Allows creation of objects and stores them automatically. Limited to objects with parameterless constructors; for others, use Registry
  /// </summary>
  /// <typeparam name="K">The key type to be stored</typeparam>
  /// <typeparam name="T">The object type to be stored</typeparam>
  public class Factory<K, T> : Registry<K, T>, IFactory<K, T> where T : AFactoryObject<K>, new()
  {
    /// <summary>
    /// Creates a new object and stores its reference in its internal registry
    /// </summary>
    /// <param name="id">The identifier for the object</param>
    /// <returns></returns>
    public T Create(K id)
    {
      T ret = new T
      {
        ID = id
      };
      Add(id, ret);
      return ret;
    }

    /// <summary>
    /// Creates a new object and stores its reference in its internal registry
    /// </summary>
    /// <param name="id">The identifier for the object</param>
    /// <param name="initFn">The initializer function</param>
    /// <returns></returns>
    public T Create(K id, ActionDelegate<T> initFn)
    {
      T ret = new T
      {
        ID = id
      };
      initFn(ret);
      Add(id, ret);
      return ret;
    }

    /// <summary>
    /// Adds an existing object and stores its reference in its internal registry
    /// </summary>
    /// <param name="item">The object to be stored</param>
    /// <returns></returns>
    public void Add(T item)
    {
      Add(item.ID, item);
    }

    /// <summary>
    /// Puts an existing object and stores its reference in its internal registry
    /// </summary>
    /// <param name="item">The object to be stored</param>
    /// <returns></returns>
    public void Put(T item)
    {
      Put(item.ID, item);
    }
  }
}
