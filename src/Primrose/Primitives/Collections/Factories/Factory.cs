namespace Primrose.Primitives.Factories
{
  /// <summary>
  /// Allows creation of objects and stores them automatically. Limited to objects with parameterless constructors; for others, use Registry
  /// </summary>
  /// <typeparam name="K">The key type to be stored</typeparam>
  /// <typeparam name="T">The object type to be stored</typeparam>
  public class Factory<K, T> : Registry<K, T>, IFactory<K, T> where T : AFactoryObject<K>, new()
  {
    /// <summary>Represents a delegate for object modification operations</summary>
    public delegate T InitDelegate(T t);

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
    public T Create(K id, InitDelegate initFn)
    {
      T ret = new T
      {
        ID = id
      };
      ret = initFn(ret);
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

  /// <summary>
  /// Allows creation of objects and stores them automatically. Limited to objects with parameterless constructors; for others, use Registry
  /// </summary>
  /// <typeparam name="K">The key type to be stored</typeparam>
  /// <typeparam name="T">The object type to be stored</typeparam>
  public class StructFactory<K, T> : Registry<K, T>, IFactory<K, T> where T : ISetIdentity<K>
  {
    /// <summary>Represents a delegate for object modification operations</summary>
    public delegate T InitDelegate(T t);

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
    public T Create(K id, InitDelegate initFn)
    {
      T ret = default;
      ret.ID = id;
      ret = initFn(ret);
      Add(id, ret);
      return ret;
    }
  }
}
