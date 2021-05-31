// Adapted from: https://stackoverflow.com/questions/1189144/c-sharp-non-boxing-conversion-of-generic-enum-to-int

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Primrose.Expressions
{
  /// <summary>
  /// Performs and caches functions for basic operations
  /// </summary>
  /// <typeparam name="TIn">Input type</typeparam>
  public static class UnaryOp<TIn>
  {
    /// <summary>
    /// Casts <typeparamref name="TIn"/> to <typeparamref name="TOut"/>.
    /// This does not cause boxing for value types.
    /// Useful in generic methods.
    /// </summary>
    /// <typeparam name="TOut">Target type to cast to. Usually a generic type.</typeparam>
    public static TOut Cast<TOut>(TIn s)
    {
      if (typeof(TOut) == typeof(string)) return (TOut)(object)(s.ToString()); // override for string

      return Cache<TOut>.Cast(s);
    }

    /// <summary>
    /// Casts <typeparamref name="TIn"/> to <typeparamref name="TOut"/>.
    /// This does not cause boxing for value types.
    /// Useful in generic methods.
    /// </summary>
    /// <typeparam name="TOut">Target type to cast to. Usually a generic type.</typeparam>
    public static TOut CastIntermediate<TOut>(TIn s, Type intermediate)
    {
      if (typeof(TOut) == typeof(string)) return (TOut)(object)(s.ToString()); // override for string

      MethodInfo mRead = typeof(UnaryOp<TIn>).GetMethod(nameof(Cast), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
      MethodInfo gmRead = mRead.MakeGenericMethod(new Type[] { intermediate });
      return (TOut)gmRead.Invoke(null, new object[] { s });
    }

    private static class Cache<TOut>
    {
      public static readonly Func<TIn, TOut> Cast = cast();

      private static Func<TIn, TOut> cast()
      {
        ParameterExpression p = Expression.Parameter(typeof(TIn));
        UnaryExpression c = Expression.ConvertChecked(p, typeof(TOut));
        return Expression.Lambda<Func<TIn, TOut>>(c, p).Compile();
      }
    }
  }
}
