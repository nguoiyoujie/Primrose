using Primrose.Expressions;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;
using System;
using System.Collections.Generic;

namespace Primrose.Expressions
{
  /// <summary>Defines the contract for handling parameters between primitive types and Val types</summary>
  internal static class ValParamContract
  {
    private static readonly Func<Val, Val> v_self = new Func<Val, Val>((v) => { return v; });

    /// <summary>Converts a Val to a typed variable</summary>
    /// <typeparam name="T">The type of the target output</typeparam>
    /// <param name="caller">The tracking object calling this method</param>
    /// <param name="function">The function calling this method</param>
    /// <param name="argnum">The function argument number representing the Val</param>
    /// <param name="v">The Val object to be converted</param>
    /// <returns></returns>
    public static T Convert<T>(ITracker caller, string function, int argnum, Val v)
    {
      try
      {
        if (typeof(T) == typeof(Val))
        {
          Func<Val, T> self_func = (Func<Val, T>)(object)v_self;
          return self_func(v);
        }

        return v.Cast<T>();
      }
      catch (InvalidValCastException)
      {
        throw new EvalException(caller, Resource.Strings.Error_EvalException_ArgumentTypeMismatch.F(argnum, function, typeof(T).Name, v.Type.Name));
      }
    }
  }
}
