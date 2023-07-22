using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Primrose.Primitives.Extensions
{
  /// <summary>
  /// Provides extension methods for Type values
  /// </summary>
  public static class TypeExts
  {
    /// <summary>
    /// Enumerates of the base class types of a given type
    /// </summary>
    /// <param name="t">The type to be enumerated</param>
    /// <returns>A enumeration of types that is inherited by <paramref name="t"/></returns>
    public static IEnumerable<Type> BaseTypes(this Type t)
    {
      while (t != null)
      {
        yield return t;
        t = t.BaseType;
      }
    }

    /// <summary>
    /// Determines the size, in bytes, of a type, usually a struct
    /// </summary>
    /// <param name="t">The type to inspect</param>
    /// <returns>The size, in bytes, of one instance of the given type</returns>
    public static int GetSizeInBytes(this Type t)
    {
      if (t == null) throw new ArgumentNullException("t");

      return _sizeof_cache.GetOrAdd(t, t2 =>
      {
        var dm = new DynamicMethod("$", typeof(int), Type.EmptyTypes);
        ILGenerator il = dm.GetILGenerator();
        il.Emit(OpCodes.Sizeof, t2);
        il.Emit(OpCodes.Ret);

        var func = (Func<int>)dm.CreateDelegate(typeof(Func<int>));
        return func();
      });
    }

    private static readonly ConcurrentDictionary<Type, int> _sizeof_cache = new ConcurrentDictionary<Type, int>();
  }
}
