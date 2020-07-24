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
    private static readonly Func<Val, bool> v_bool = new Func<Val, bool>((v) => { return (bool)v; });
    private static readonly Func<Val, int> v_int = new Func<Val, int>((v) => { return (int)v; });
    private static readonly Func<Val, float> v_float = new Func<Val, float>((v) => { return (float)v; });
    private static readonly Func<Val, float2> v_float2 = new Func<Val, float2>((v) => { return (float2)v; });
    private static readonly Func<Val, float3> v_float3 = new Func<Val, float3>((v) => { return (float3)v; });
    private static readonly Func<Val, float4> v_float4 = new Func<Val, float4>((v) => { return (float4)v; });
    private static readonly Func<Val, string> v_string = new Func<Val, string>((v) => { return (string)v; });
    private static readonly Func<Val, bool[]> v_bool_a = new Func<Val, bool[]>((v) => { return (bool[])v; });
    private static readonly Func<Val, int[]> v_int_a = new Func<Val, int[]>((v) => { return (int[])v; });
    private static readonly Func<Val, float[]> v_float_a = new Func<Val, float[]>((v) => { return (float[])v; });
    private static readonly Func<Val, string[]> v_string_a = new Func<Val, string[]>((v) => { return (string[])v; });
    private static readonly Func<Val, Val> v_val = new Func<Val, Val>((v) => { return v; });
    private static readonly Dictionary<Pair<Type, ValType>, object> contracts = new Dictionary<Pair<Type, ValType>, object>
    {
      { new Pair<Type, ValType>(typeof(bool), ValType.BOOL), v_bool },
      { new Pair<Type, ValType>(typeof(int), ValType.INT), v_int },
      { new Pair<Type, ValType>(typeof(float), ValType.INT), v_float },
      { new Pair<Type, ValType>(typeof(float), ValType.FLOAT), v_float },
      { new Pair<Type, ValType>(typeof(string), ValType.BOOL), v_string },
      { new Pair<Type, ValType>(typeof(string), ValType.INT), v_string },
      { new Pair<Type, ValType>(typeof(string), ValType.FLOAT), v_string },
      { new Pair<Type, ValType>(typeof(string), ValType.STRING), v_string },
      { new Pair<Type, ValType>(typeof(float2), ValType.FLOAT2), v_float2 },
      { new Pair<Type, ValType>(typeof(float2), ValType.FLOAT_ARRAY), v_float2 },
      { new Pair<Type, ValType>(typeof(float2), ValType.INT_ARRAY), v_float2 },
      { new Pair<Type, ValType>(typeof(float3), ValType.FLOAT3), v_float3 },
      { new Pair<Type, ValType>(typeof(float3), ValType.FLOAT_ARRAY), v_float3 },
      { new Pair<Type, ValType>(typeof(float3), ValType.INT_ARRAY), v_float3 },
      { new Pair<Type, ValType>(typeof(float4), ValType.FLOAT4), v_float4 },
      { new Pair<Type, ValType>(typeof(float4), ValType.FLOAT_ARRAY), v_float4 },
      { new Pair<Type, ValType>(typeof(float4), ValType.INT_ARRAY), v_float4 },
      { new Pair<Type, ValType>(typeof(bool[]), ValType.BOOL_ARRAY), v_bool_a },
      { new Pair<Type, ValType>(typeof(int[]), ValType.INT_ARRAY), v_int_a },
      { new Pair<Type, ValType>(typeof(float[]), ValType.FLOAT_ARRAY), v_float_a },
      { new Pair<Type, ValType>(typeof(float[]), ValType.FLOAT2), v_float_a },
      { new Pair<Type, ValType>(typeof(float[]), ValType.FLOAT3), v_float_a },
      { new Pair<Type, ValType>(typeof(float[]), ValType.FLOAT4), v_float_a },
      { new Pair<Type, ValType>(typeof(string[]), ValType.STRING_ARRAY), v_string_a },

      { new Pair<Type, ValType>(typeof(Val), ValType.BOOL), v_val },
      { new Pair<Type, ValType>(typeof(Val), ValType.INT), v_val },
      { new Pair<Type, ValType>(typeof(Val), ValType.FLOAT), v_val },
      { new Pair<Type, ValType>(typeof(Val), ValType.STRING), v_val },
      { new Pair<Type, ValType>(typeof(Val), ValType.FLOAT2), v_val },
      { new Pair<Type, ValType>(typeof(Val), ValType.FLOAT3), v_val },
      { new Pair<Type, ValType>(typeof(Val), ValType.FLOAT4), v_val },
      { new Pair<Type, ValType>(typeof(Val), ValType.BOOL_ARRAY), v_val },
      { new Pair<Type, ValType>(typeof(Val), ValType.INT_ARRAY), v_val },
      { new Pair<Type, ValType>(typeof(Val), ValType.FLOAT_ARRAY), v_val },
      { new Pair<Type, ValType>(typeof(Val), ValType.STRING_ARRAY), v_val },
    };

    /// <summary>Converts a Val to a typed variable</summary>
    /// <typeparam name="T">The type of the target output</typeparam>
    /// <param name="caller">The tracking object calling this method</param>
    /// <param name="function">The function calling this method</param>
    /// <param name="argnum">The function argument number representing the Val</param>
    /// <param name="v">The Val object to be converted</param>
    /// <returns></returns>
    public static T Convert<T>(ITracker caller, string function, int argnum, Val v)
    {
      Pair<Type, ValType> tv = new Pair<Type, ValType>(typeof(T), v.Type);
      if (!contracts.TryGetValue(tv, out object ofunc))
        throw new EvalException(caller, Resource.Strings.Error_EvalException_ArgumentTypeMismatch.F(argnum, function, typeof(T).Name, v.Type));

      Func<Val, T> func = (Func<Val, T>)ofunc;
      return func(v);
    }
  }
}
