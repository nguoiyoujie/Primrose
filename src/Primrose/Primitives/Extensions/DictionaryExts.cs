using System;
using System.Collections.Generic;

namespace Primrose.Primitives.Extensions
{
  /// <summary>
  /// Provides extension methods for Dictionaries
  /// </summary>
  public static class DictionaryExts
  {
    /// <summary>
    /// Adds or updates a key-value pair in a dictionary.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="d"></param>
    /// <param name="k"></param>
    /// <param name="v"></param>
    public static void Put<K, V>(this IDictionary<K, V> d, K k, V v)
    {
      if (d.ContainsKey(k))
        d[k] = v;
      else
        d.Add(k, v);
    }

    /// <summary>
    /// Retrieves a value from a dictionary, or a default(<typeparamref name="V"/>) if the key is not in the dictionary.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="d"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static V GetOrDefault<K, V>(this IDictionary<K, V> d, K k)
    {
      if (!d.TryGetValue(k, out V ret))
        return default;
      return ret;
    }

    /// <summary>
    /// Retrieves a value from a dictionary, or adds a new instance of <typeparamref name="V"/> if the key is not in the dictionary.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="d"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static V GetOrAdd<K, V>(this IDictionary<K, V> d, K k)
      where V : new()
    {
      if (!d.TryGetValue(k, out V ret))
        d.Add(k, ret = new V());
      return ret;
    }

    /// <summary>
    /// Retrieves a value from a dictionary, or adds a new instance of <typeparamref name="V"/> if the key is not in the dictionary.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="d"></param>
    /// <param name="k"></param>
    /// <param name="createFn">The function used to create the new instance of <typeparamref name="V"/></param>
    /// <returns></returns>
    public static V GetOrAdd<K, V>(this IDictionary<K, V> d, K k, Func<K, V> createFn)
    {
      if (!d.TryGetValue(k, out V ret))
        d.Add(k, ret = createFn(k));
      return ret;
    }
  }
}
