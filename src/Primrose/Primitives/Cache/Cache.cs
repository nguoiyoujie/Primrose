using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using System;
using System.Collections.Generic;

namespace Primrose.Primitives
{
  /// <summary>
  /// Provides a basic cache functionality in a dictionary-like lookup.
  /// </summary>
  /// <typeparam name="TKey">The key type.</typeparam>
  /// <typeparam name="Token">The token type. The token is invalidated when the compared tokens are not equal</typeparam>
  /// <typeparam name="TValue">The value type.</typeparam>
  /// <typeparam name="TParam">A parameter type used to generate updated values when the cached value is invalidated</typeparam>
  public class Cache<TKey, Token, TValue, TParam> where Token : struct
  {
    private readonly Dictionary<TKey, CacheItem<Token, TValue, TParam>> cache;
    private readonly List<TKey> remove;

    static Cache()
    {
      // this is generally uniquw because CacheItem is contained within this class 
      ObjectPool<CacheItem<Token, TValue, TParam>>.CreateStaticPool(() => new CacheItem<Token, TValue, TParam>(default), (a) => a.ExpiryToken = default);
    }

    /// <summary>Creates a cache</summary>
    public Cache()
    {
      cache = new Dictionary<TKey, CacheItem<Token, TValue, TParam>>();
      remove = new List<TKey>();
    }

    /// <summary>Creates a cache with an initial capacity</summary>
    /// <param name="capacity"></param>
    public Cache(int capacity)
    {
      cache = new Dictionary<TKey, CacheItem<Token, TValue, TParam>>(capacity);
      remove = new List<TKey>();
    }

    /// <summary>Retrieves the number of elements in the cache</summary>
    public int Count { get { return cache.Count; } }

    /// <summary>Retrieves the number of elements in the pool</summary>
    public int PoolCount { get { return ObjectPool<CacheItem<Token, TValue, TParam>>.GetStaticPool().Count; } }

    /// <summary>Retrieves the number of elements outside the pool. Ideally this should be equal to Count, otherwise garbage leak is suspected</summary>
    public int PoolUncollectedCount { get { return ObjectPool<CacheItem<Token, TValue, TParam>>.GetStaticPool().UncollectedCount; } }

    /// <summary>
    /// Defines a cache key if the key does not exist, otherwise flushes the key.
    /// </summary>
    /// <param name="key">The key containing the cached token and value</param>
    /// <param name="token">The new token to be compared with the cached token</param>
    public void Define(TKey key, Token token)
    {
      CacheItem<Token, TValue, TParam> ret = ObjectPool<CacheItem<Token, TValue, TParam>>.GetStaticPool().GetNew();
      ret.ExpiryToken = token;
      if (cache.ContainsKey(key))
      {
        ObjectPool<CacheItem<Token, TValue, TParam>>.GetStaticPool().Return(cache[key]);
        cache[key] = ret;
      }
      else
        cache.Add(key, ret);
    }

    /// <summary>
    /// Retrieves the value based on the cached key.
    /// </summary>
    /// <param name="key">The key containing the cached token and value</param>
    /// <param name="token">The new token to be compared with the cached token</param>
    /// <param name="func">The function used to generate the updated value</param>
    /// <param name="p">The parameter value used to generate the updated value</param>
    /// <param name="cmp">The token comparer</param>
    /// <returns>If the tokens match, return the cached value, otherwise update this value with func(p) and returns the new value</returns>
    /// <exception cref="KeyNotFoundException">Attempted to get an non-existent key from a cache.</exception>
    public TValue Get(TKey key, Token token, Func<TParam, TValue> func, TParam p, IEqualityComparer<Token> cmp)
    {
      if ((!typeof(TKey).IsValueType && EqualityComparer<TKey>.Default.Equals(key, default)) || !cache.TryGetValue(key, out CacheItem<Token, TValue, TParam> item))
        throw new KeyNotFoundException(Resource.Strings.Error_CacheKeyNotFound.F(key));

      return item.Get(token, func, p, cmp);
    }

    /// <summary>
    /// Defines a cache key if the key does not exist, then retrieves the value.
    /// </summary>
    /// <param name="key">The key containing the cached token and value</param>
    /// <param name="token">The new token to be compared with the cached token</param>
    /// <param name="func">The function used to generate the updated value</param>
    /// <param name="p">The parameter value used to generate the updated value</param>
    /// <param name="cmp">The token comparer</param>
    /// <returns>If the tokens match, return the cached value, otherwise update this value with func(p) and returns the new value</returns>
    public TValue GetOrDefine(TKey key, Token token, Func<TParam, TValue> func, TParam p, IEqualityComparer<Token> cmp)
    {
      if ((!typeof(TKey).IsValueType && EqualityComparer<TKey>.Default.Equals(key, default)) || !cache.TryGetValue(key, out CacheItem<Token, TValue, TParam> item))
      {
        cache.Add(key, ObjectPool<CacheItem<Token, TValue, TParam>>.GetStaticPool().GetNew());
        item = cache[key];
      }
      return item.Get(token, func, p, cmp);
    }

    /// <summary>Clears the cache of entries depending on a conditional function.</summary>
    /// <param name="func">The function that determines whether the entry should be cleared. Use null to clear the entire cache.</param>
    /// <returns>The number of items cleared</returns>
    public int Clear(Func<Token, bool> func = null)
    {
      int count;
      if (func == null)
      {
        count = cache.Count;
        foreach (var kvp in cache)
          ObjectPool<CacheItem<Token, TValue, TParam>>.GetStaticPool().Return(kvp.Value);
        cache.Clear();
      }
      else
      {
        foreach (var kvp in cache)
          if (func(cache[kvp.Key].ExpiryToken))
            remove.Add(kvp.Key);

        count = remove.Count;
        foreach (TKey rm in remove)
        {
          ObjectPool<CacheItem<Token, TValue, TParam>>.GetStaticPool().Return(cache[rm]);
          cache.Remove(rm);
        }
        remove.Clear();
      }
      return count;
    }

    /// <summary>Clears the cache of entries depending on a conditional function.</summary>
    /// <param name="func">The function that determines whether the entry should be cleared. Use null to clear the entire cache.</param>
    /// <returns>The number of items cleared</returns>
    public int ClearAndPoolInBackgound(Func<Token, bool> func = null)
    {
      int count = 0;
      if (func == null)
      {
        count = cache.Count;
        foreach (var kvp in cache)
          ObjectPool<CacheItem<Token, TValue, TParam>>.GetStaticPool().Return(kvp.Value);
        cache.Clear();
      }
      else
      {
        foreach (var kvp in cache)
          if (func(cache[kvp.Key].ExpiryToken))
            remove.Add(kvp.Key);

        count = remove.Count;
        foreach (TKey rm in remove)
        {
          ObjectPool<CacheItem<Token, TValue, TParam>>.GetStaticPool().Return(cache[rm]);
          cache.Remove(rm);
        }
        remove.Clear();
      }
      return count;
    }

    private class CacheItem<E, T, TP> where E : struct
    {
      internal E ExpiryToken;
      private T val;

      public CacheItem(E token)
      {
        ExpiryToken = token;
        val = default;
      }

      public T Get(E token, Func<TP, T> func, TP p)
      {
        if (!EqualityComparer<E>.Default.Equals(ExpiryToken, token))
        {
          val = func(p);
          ExpiryToken = token;
        }
        return val;
      }

      public T Get(E token, Func<TP, T> func, TP p, IEqualityComparer<E> cmp)
      {
        if (!cmp.Equals(ExpiryToken, token))
        {
          val = func(p);
          ExpiryToken = token;
        }
        return val;
      }
    }
  }
}

