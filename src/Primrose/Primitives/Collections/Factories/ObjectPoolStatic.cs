using System;
using System.Collections.Generic;
using System.Text;
using Primrose.Primitives.Extensions;

namespace Primrose.Primitives.Factories
{
  /// <summary>
  /// Provides a basic object pool for pooling objects for further use  
  /// </summary>
  public interface IPool
  {
    /// <summary>Retrieves the number of elements in the pool</summary>
    int Count { get; }

    /// <summary>Retrieves the number of generated elements in the pool that were not yet collected</summary>
    int UncollectedCount { get; }
  }

  internal static class StaticObjectPoolInitializer
  {
    private static bool _initialized = false;

    internal static void InitStaticObjectPools()
    {
      if (_initialized) { return; }
      ObjectPool<StringBuilder>.CreateStaticPoolInner(() => new StringBuilder(128), (sb) => sb.Clear());
      ObjectPool<StringBuilder, int>.CreateStaticPoolInner((i) => new StringBuilder(i), (sb) => sb.Clear());

      _initialized = true;
    }
  }

  public partial class ObjectPool<T> : IPool where T : class
  {
    private static ObjectPool<T> _staticpool;
    private static readonly Stack<T> _staticlist = new Stack<T>(256);

    /// <summary>Returns whether a static instance exists for this object pool</summary>
    public bool HasStaticPool { get { StaticObjectPoolInitializer.InitStaticObjectPools(); return _staticpool == null; } }

    /// <summary>
    /// Creates a static version of the object pool for global use
    /// </summary>
    /// <param name="createFn">The function for creating new instances</param>
    /// <param name="resetFn">The function for reseting instances that are returned to the pool</param>
    /// <param name="replaceExisting">Denotes whether this method should replace the existing static pool</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">The background object pool has already been created.</exception>
    public static ObjectPool<T> CreateStaticPool(Func<T> createFn, Action<T> resetFn = null, bool replaceExisting = true)
    {
      StaticObjectPoolInitializer.InitStaticObjectPools();
      return CreateStaticPoolInner(createFn, resetFn, replaceExisting);
    }

    internal static ObjectPool<T> CreateStaticPoolInner(Func<T> createFn, Action<T> resetFn = null, bool replaceExisting = true)
    {
      if (!replaceExisting && _staticpool != null)
        throw new InvalidOperationException(Resource.Strings.Error_ObjectPoolDuplicateBackgroundPool.F(typeof(T).Name));

      _staticpool = new ObjectPool<T>(false, createFn, resetFn);
      return _staticpool;
    }

    /// <summary>
    /// Retrieves the static version of the pool if created
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">The background object pool has not been created.</exception>
    public static ObjectPool<T> GetStaticPool()
    {
      StaticObjectPoolInitializer.InitStaticObjectPools();
      if (_staticpool == null)
        throw new InvalidOperationException(Resource.Strings.Error_ObjectPoolBackgroundPoolNotCreated.F(typeof(T).Name));

      return _staticpool;
    }
  }

  public partial class ObjectPool<T, TParam> : IPool where T : class
  {
    private static ObjectPool<T, TParam> _staticpool;

    /// <summary>
    /// Creates a static version of the object pool for global use
    /// </summary>
    /// <param name="createFn">The function for creating new instances</param>
    /// <param name="resetFn">The function for reseting instances that are returned to the pool</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">The background object pool has already been created.</exception>
    public static ObjectPool<T, TParam> CreateStaticPool(Func<TParam, T> createFn, Action<T> resetFn = null)
    {
      StaticObjectPoolInitializer.InitStaticObjectPools();
      return CreateStaticPoolInner(createFn, resetFn);
    }

    internal static ObjectPool<T, TParam> CreateStaticPoolInner(Func<TParam, T> createFn, Action<T> resetFn = null)
    { 
      if (_staticpool != null)
        throw new InvalidOperationException(Resource.Strings.Error_ObjectPoolDuplicateBackgroundPool.F(typeof(T).Name));

      _staticpool = new ObjectPool<T, TParam>(createFn, resetFn);
      return _staticpool;
    }

    /// <summary>
    /// Retrieves the static version of the pool if created
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">The background object pool has not been created.</exception>
    public static ObjectPool<T, TParam> GetStaticPool()
    {
      StaticObjectPoolInitializer.InitStaticObjectPools();
      if (_staticpool == null)
        throw new InvalidOperationException(Resource.Strings.Error_ObjectPoolBackgroundPoolNotCreated.F(typeof(T).Name));

      return _staticpool;
    }
  }
}
