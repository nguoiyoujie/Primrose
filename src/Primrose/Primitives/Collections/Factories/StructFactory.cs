namespace Primrose.Primitives.Factories
{
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
    public T Create(K id, ActionRefDelegate<T> initFn)
    {
      T ret = default;
      ret.ID = id;
      initFn(ref ret);
      Add(id, ret);
      return ret;
    }
  }
}
