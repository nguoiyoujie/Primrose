using Primrose.Primitives;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.ValueTypes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Primrose.Expressions
{
  /// <summary>
  /// The operation class
  /// </summary>
  public static class Ops
  {
    internal static Dictionary<Pair<UOp, Type>, Func<Val, Val>> unaryops
      = new Dictionary<Pair<UOp, Type>, Func<Val, Val>>
      {
        // IDENTITY
        { new Pair<UOp, Type>(UOp.IDENTITY, typeof(bool)), (a) => { return a; } },
        { new Pair<UOp, Type>(UOp.IDENTITY, typeof(int)), (a) => { return a; } },
        { new Pair<UOp, Type>(UOp.IDENTITY, typeof(float)), (a) => { return a; } },
        { new Pair<UOp, Type>(UOp.IDENTITY, typeof(string)), (a) => { return a; } },

        // NEGATION
        { new Pair<UOp, Type>(UOp.LOGICAL_NOT, typeof(bool)), (a) => { return new Val(!(bool)a); }},

        // INVERSE
        { new Pair<UOp, Type>(UOp.NEGATION, typeof(int)), (a) => { return  new Val(-(int)a); }},
        { new Pair<UOp, Type>(UOp.NEGATION, typeof(float)), (a) => { return  new Val(-(float)a); }},
      };

    internal static Dictionary<Trip<BOp, Type, Type>, Func<Val, Val, Val>> binaryops
      = new Dictionary<Trip<BOp, Type, Type>, Func<Val, Val, Val>>
      {
        // ADD
        {new Trip<BOp, Type, Type>(BOp.ADD, typeof(int), typeof(int)), (a,b) => { return new Val((int)a + (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.ADD, typeof(int), typeof(float)), (a,b) => { return new Val((int)a + (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.ADD, typeof(float), typeof(int)), (a,b) => { return new Val((float)a + (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.ADD, typeof(float), typeof(float)), (a,b) => { return new Val((float)a + (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.ADD, typeof(int), typeof(string)), (a,b) => { return new Val((int)a + (string)b); }},
        {new Trip<BOp, Type, Type>(BOp.ADD, typeof(string), typeof(int)), (a,b) => { return new Val((string)a + (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.ADD, typeof(string), typeof(string)), (a,b) => { return new Val((string)a + (string)b); }},
        {new Trip<BOp, Type, Type>(BOp.ADD, typeof(float), typeof(string)), (a,b) => { return new Val((float)a + (string)b); }},
        {new Trip<BOp, Type, Type>(BOp.ADD, typeof(string), typeof(float)), (a,b) => { return new Val((string)a + (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.ADD, typeof(float2), typeof(float2)), (a,b) => { return new Val((float2)a + (float2)b); }},
        {new Trip<BOp, Type, Type>(BOp.ADD, typeof(float3), typeof(float3)), (a,b) => { return new Val((float3)a + (float3)b); }},
        {new Trip<BOp, Type, Type>(BOp.ADD, typeof(float4), typeof(float4)), (a,b) => { return new Val((float4)a + (float4)b); }},
        //{new Trip<BOp, Type, Type>(BOp.ADD, typeof(float[]), typeof(float[])), (a,b) => { return new Val(MemberwiseAdd((float[])a, (float[])b)); }},
        //{new Trip<BOp, Type, Type>(BOp.ADD, typeof(float[]), typeof(int[])), (a,b) => { return new Val(MemberwiseAdd((float[])a, (int[])b)); }},
        //{new Trip<BOp, Type, Type>(BOp.ADD, typeof(int[]), typeof(float[])), (a,b) => { return new Val(MemberwiseAdd((int[])a, (float[])b)); }},
        //{new Trip<BOp, Type, Type>(BOp.ADD, typeof(int[]), typeof(int[])), (a,b) => { return new Val(MemberwiseAdd((int[])a, (int[])b)); }},

        // SUBTRACT
        {new Trip<BOp, Type, Type>(BOp.SUBTRACT, typeof(int), typeof(int)), (a,b) => { return new Val((int)a - (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.SUBTRACT, typeof(int), typeof(float)), (a,b) => { return new Val((int)a - (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.SUBTRACT, typeof(float), typeof(int)), (a,b) => { return new Val((float)a - (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.SUBTRACT, typeof(float), typeof(float)), (a,b) => { return new Val((float)a - (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.SUBTRACT, typeof(float2), typeof(float2)), (a,b) => { return new Val((float2)a - (float2)b); }},
        {new Trip<BOp, Type, Type>(BOp.SUBTRACT, typeof(float3), typeof(float3)), (a,b) => { return new Val((float3)a - (float3)b); }},
        {new Trip<BOp, Type, Type>(BOp.SUBTRACT, typeof(float4), typeof(float4)), (a,b) => { return new Val((float4)a - (float4)b); }},
        //{new Trip<BOp, Type, Type>(BOp.SUBTRACT, typeof(float[]), typeof(float[])), (a,b) => { return new Val(MemberwiseSubstract((float[])a, (float[])b)); }},
        //{new Trip<BOp, Type, Type>(BOp.SUBTRACT, typeof(float[]), typeof(int[])), (a,b) => { return new Val(MemberwiseSubstract((float[])a, (int[])b)); }},
        //{new Trip<BOp, Type, Type>(BOp.SUBTRACT, typeof(int[]), typeof(float[])), (a,b) => { return new Val(MemberwiseSubstract((int[])a, (float[])b)); }},
        //{new Trip<BOp, Type, Type>(BOp.SUBTRACT, typeof(int[]), typeof(int[])), (a,b) => { return new Val(MemberwiseSubstract((int[])a, (int[])b)); }},

        // MULTIPLY
        {new Trip<BOp, Type, Type>(BOp.MULTIPLY, typeof(int), typeof(int)), (a,b) => { return new Val((int)a * (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.MULTIPLY, typeof(int), typeof(float)), (a,b) => { return new Val((int)a * (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.MULTIPLY, typeof(float), typeof(int)), (a,b) => { return new Val((float)a * (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.MULTIPLY, typeof(float), typeof(float)), (a,b) => { return new Val((float)a * (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.MULTIPLY, typeof(float2), typeof(float)), (a,b) => { return new Val((float2)a * (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.MULTIPLY, typeof(float3), typeof(float)), (a,b) => { return new Val((float3)a * (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.MULTIPLY, typeof(float4), typeof(float)), (a,b) => { return new Val((float4)a * (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.MULTIPLY, typeof(float2), typeof(int)), (a,b) => { return new Val((float2)a * (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.MULTIPLY, typeof(float3), typeof(int)), (a,b) => { return new Val((float3)a * (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.MULTIPLY, typeof(float4), typeof(int)), (a,b) => { return new Val((float4)a * (int)b); }},

        // DIVIDE
        {new Trip<BOp, Type, Type>(BOp.DIVIDE, typeof(int), typeof(int)), (a,b) => { return new Val((int)a / (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.DIVIDE, typeof(int), typeof(float)), (a,b) => { return new Val((int)a / (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.DIVIDE, typeof(float), typeof(int)), (a,b) => { return new Val((float)a / (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.DIVIDE, typeof(float), typeof(float)), (a,b) => { return new Val((float)a / (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.DIVIDE, typeof(float2), typeof(float)), (a,b) => { return new Val((float2)a / (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.DIVIDE, typeof(float3), typeof(float)), (a,b) => { return new Val((float3)a / (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.DIVIDE, typeof(float4), typeof(float)), (a,b) => { return new Val((float4)a / (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.DIVIDE, typeof(float2), typeof(int)), (a,b) => { return new Val((float2)a / (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.DIVIDE, typeof(float3), typeof(int)), (a,b) => { return new Val((float3)a / (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.DIVIDE, typeof(float4), typeof(int)), (a,b) => { return new Val((float4)a / (int)b); }},

        // MODULUS
        {new Trip<BOp, Type, Type>(BOp.MODULUS, typeof(int), typeof(int)), (a,b) => { return new Val((int)a % (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.MODULUS, typeof(int), typeof(float)), (a,b) => { return new Val((int)a % (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.MODULUS, typeof(float), typeof(int)), (a,b) => { return new Val((float)a % (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.MODULUS, typeof(float), typeof(float)), (a,b) => { return new Val((float)a % (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.MODULUS, typeof(float2), typeof(float)), (a,b) => { return new Val((float2)a % (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.MODULUS, typeof(float3), typeof(float)), (a,b) => { return new Val((float3)a % (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.MODULUS, typeof(float4), typeof(float)), (a,b) => { return new Val((float4)a % (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.MODULUS, typeof(float2), typeof(int)), (a,b) => { return new Val((float2)a % (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.MODULUS, typeof(float3), typeof(int)), (a,b) => { return new Val((float3)a % (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.MODULUS, typeof(float4), typeof(int)), (a,b) => { return new Val((float4)a % (int)b); }},

        // LOGICAL_OR
        {new Trip<BOp, Type, Type>(BOp.LOGICAL_OR, typeof(bool), typeof(bool)), (a,b) => { return new Val((bool)a || (bool)b); }},
        //{new Trip<BOp, Type, Type>(BOp.LOGICAL_OR, typeof(int), typeof(int)), (a,b) => { return new Val(a.ValueI | b.ValueI); }},

        // LOGICAL_AND
        {new Trip<BOp, Type, Type>(BOp.LOGICAL_AND, typeof(bool), typeof(bool)), (a,b) => { return new Val((bool)a & (bool)b); }},
        //{new Trip<BOp, Type, Type>(BOp.LOGICAL_AND, typeof(int), typeof(int)), (a,b) => { return new Val(a.ValueI & b.ValueI); }},

        // EQUAL_TO
        {new Trip<BOp, Type, Type>(BOp.EQUAL_TO, typeof(string), null), (a,b) => { return new Val((string)a == null); }},
        {new Trip<BOp, Type, Type>(BOp.EQUAL_TO, null, typeof(string)), (a,b) => { return new Val((string)b == null); }},
        {new Trip<BOp, Type, Type>(BOp.EQUAL_TO, typeof(int), typeof(float)), (a,b) => { return new Val((int)a == (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.EQUAL_TO, typeof(float), typeof(int)), (a,b) => { return new Val((float)a == (int)b); }},

        // NOT_EQUAL_TO
        {new Trip<BOp, Type, Type>(BOp.NOT_EQUAL_TO, typeof(string), null), (a,b) => { return new Val((string)a != null); }},
        {new Trip<BOp, Type, Type>(BOp.NOT_EQUAL_TO, null, typeof(string)), (a,b) => { return new Val((string)b != null); }},
        {new Trip<BOp, Type, Type>(BOp.NOT_EQUAL_TO, typeof(int), typeof(float)), (a,b) => { return new Val((int)a != (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.NOT_EQUAL_TO, typeof(float), typeof(int)), (a,b) => { return new Val((float)a != (int)b); }},

        // MORE_THAN
        {new Trip<BOp, Type, Type>(BOp.MORE_THAN, typeof(int), typeof(int)), (a,b) => { return new Val((int)a > (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.MORE_THAN, typeof(int), typeof(float)), (a,b) => { return new Val((int)a > (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.MORE_THAN, typeof(float), typeof(int)), (a,b) => { return new Val((float)a > (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.MORE_THAN, typeof(float), typeof(float)), (a,b) => { return new Val((float)a > (float)b); }},

        // MORE_THAN_OR_EQUAL_TO
        {new Trip<BOp, Type, Type>(BOp.MORE_THAN_OR_EQUAL_TO, typeof(int), typeof(int)), (a,b) => { return new Val((int)a >= (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.MORE_THAN_OR_EQUAL_TO, typeof(int), typeof(float)), (a,b) => { return new Val((int)a >= (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.MORE_THAN_OR_EQUAL_TO, typeof(float), typeof(int)), (a,b) => { return new Val((float)a >= (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.MORE_THAN_OR_EQUAL_TO, typeof(float), typeof(float)), (a,b) => { return new Val((float)a >= (float)b); }},

        // LESS_THAN
        {new Trip<BOp, Type, Type>(BOp.LESS_THAN, typeof(int), typeof(int)), (a,b) => { return new Val((int)a < (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.LESS_THAN, typeof(int), typeof(float)), (a,b) => { return new Val((int)a < (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.LESS_THAN, typeof(float), typeof(int)), (a,b) => { return new Val((float)a < (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.LESS_THAN, typeof(float), typeof(float)), (a,b) => { return new Val((float)a < (float)b); }},

        // LESS_THAN_OR_EQUAL_TO
        {new Trip<BOp, Type, Type>(BOp.LESS_THAN_OR_EQUAL_TO, typeof(int), typeof(int)), (a,b) => { return new Val((int)a <= (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.LESS_THAN_OR_EQUAL_TO, typeof(int), typeof(float)), (a,b) => { return new Val((int)a <= (float)b); }},
        {new Trip<BOp, Type, Type>(BOp.LESS_THAN_OR_EQUAL_TO, typeof(float), typeof(int)), (a,b) => { return new Val((float)a <= (int)b); }},
        {new Trip<BOp, Type, Type>(BOp.LESS_THAN_OR_EQUAL_TO, typeof(float), typeof(float)), (a,b) => { return new Val((float)a <= (float)b); } }
      };

    internal static Dictionary<Type, Func<Val, int[], Val>> indexops
  = new Dictionary<Type, Func<Val, int[], Val>>
  {
        //{ typeof(int2), (a, ind) => { if (ind != null && ind.Length == 1) { return new Val(((int2)a)[ind[0]]); } else { throw new IndexOutOfRangeException(); } } },
        //{ typeof(int3), (a, ind) => { if (ind != null && ind.Length == 1) { return new Val(((int3)a)[ind[0]]); } else { throw new IndexOutOfRangeException(); } } },
        //{ typeof(int4), (a, ind) => { if (ind != null && ind.Length == 1) { return new Val(((int4)a)[ind[0]]); } else { throw new IndexOutOfRangeException(); } } },
        { typeof(float2), (a, ind) => { if (ind != null && ind.Length == 1) { return new Val(((float2)a)[ind[0]]); } else { throw new IndexOutOfRangeException(); } } },
        { typeof(float3), (a, ind) => { if (ind != null && ind.Length == 1) { return new Val(((float3)a)[ind[0]]); } else { throw new IndexOutOfRangeException(); } } },
        { typeof(float4), (a, ind) => { if (ind != null && ind.Length == 1) { return new Val(((float4)a)[ind[0]]); } else { throw new IndexOutOfRangeException(); } } },
  };


    /// <summary>
    /// Performs an unary operation 
    /// </summary>
    /// <param name="op">The operator</param>
    /// <param name="v">The value</param>
    /// <returns></returns>
    public static Val Do(UOp op, Val v)
    {
      if (!unaryops.TryGetValue(new Pair<UOp, Type>(op, v.Type), out Func<Val, Val> fn))
        throw new ArgumentException(Resource.Strings.Error_IncompatibleUOp.F(op, v.Type));

      return fn.Invoke(v);
    }

    /// <summary>
    /// Performs a binary operation 
    /// </summary>
    /// <param name="op">The operator</param>
    /// <param name="v1">The first value</param>
    /// <param name="v2">The second value</param>
    /// <returns></returns>
    public static Val Do(BOp op, Val v1, Val v2)
    {
      ImplicitCast(ref v1, ref v2);

      if (!binaryops.TryGetValue(new Trip<BOp, Type, Type>(op, v1.Type, v2.Type), out Func<Val, Val, Val> fn))
        throw new ArgumentException(Resource.Strings.Error_IncompatibleBOp.F(op, v1.Type, v2.Type));

      return fn.Invoke(v1, v2);
    }

    /// <summary>
    /// Performs an equal operation 
    /// </summary>
    /// <param name="v1">The first value</param>
    /// <param name="v2">The second value</param>
    /// <returns></returns>
    public static Val IsEqual(Val v1, Val v2)
    {
      ImplicitCast(ref v1, ref v2);

      if (binaryops.TryGetValue(new Trip<BOp, Type, Type>(BOp.EQUAL_TO, v1.Type, v2.Type), out Func<Val, Val, Val> fn))
        return fn.Invoke(v1, v2);

      if (v1.Type == v2.Type)
        return new Val(v1.Value?.Equals(v2.Value) ?? (v2.Value == null));

      return new Val(false);
    }

    /// <summary>
    /// Performs a not equal operation 
    /// </summary>
    /// <param name="v1">The first value</param>
    /// <param name="v2">The second value</param>
    /// <returns></returns>
    public static Val IsNotEqual(Val v1, Val v2)
    {
      ImplicitCast(ref v1, ref v2);

      if (binaryops.TryGetValue(new Trip<BOp, Type, Type>(BOp.NOT_EQUAL_TO, v1.Type, v2.Type), out Func<Val, Val, Val> fn))
        return fn.Invoke(v1, v2);

      if (v1.Type == v2.Type)
        return new Val(!(v1.Value?.Equals(v2.Value) ?? (v2.Value == null)));

      return new Val(true);
    }

    /// <summary>
    /// Performs an index operation 
    /// </summary>
    /// <param name="v">The value</param>
    /// <param name="indices">The indices</param>
    /// <returns></returns>
    public static Val GetIndex(Val v, int[] indices)
    {
      if (indexops.TryGetValue(v.Type, out Func<Val, int[], Val> fn))
      {
        return fn.Invoke(v, indices);
      }
      else
      {
        Array a;
        try
        {
          a = v.Cast<Array>();
        }
        catch
        {
          throw new EvalException(null, Resource.Strings.Error_EvalException_IndexOnNonArray.F(v));
        }

        try
        {
          return new Val(a.GetValue(indices));
        }
        catch (IndexOutOfRangeException)
        {
          int len = a?.Length ?? 0;
          throw new EvalException(null, Resource.Strings.Error_EvalException_IndexOutOfRange.F(indices.Length, len));
        }
        catch
        {
          throw new EvalException(null, Resource.Strings.Error_EvalException_IndexOnNonArray.F(v));
        }
      }
    }

    private static float[] MemberwiseAdd(float[] v1, float[] v2)
    {
      if (v1.Length != v2.Length)
        throw new ArrayMismatchException(v1.Length, v2.Length);

      float[] ret = new float[v1.Length];
      for (int i = 0; i < ret.Length; i++)
        ret[i] = v1[i] + v2[i];

      return ret;
    }

    private static float[] MemberwiseSubstract(float[] v1, float[] v2)
    {
      if (v1.Length != v2.Length)
        throw new ArrayMismatchException(v1.Length, v2.Length);

      float[] ret = new float[v1.Length];
      for (int i = 0; i < ret.Length; i++)
        ret[i] = v1[i] - v2[i];

      return ret;
    }

    private static int[] MemberwiseAdd(int[] v1, int[] v2)
    {
      if (v1.Length != v2.Length)
        throw new ArrayMismatchException(v1.Length, v2.Length);

      int[] ret = new int[v1.Length];
      for (int i = 0; i < ret.Length; i++)
        ret[i] = v1[i] + v2[i];

      return ret;
    }

    private static int[] MemberwiseSubstract(int[] v1, int[] v2)
    {
      if (v1.Length != v2.Length)
        throw new ArrayMismatchException(v1.Length, v2.Length);

      int[] ret = new int[v1.Length];
      for (int i = 0; i < ret.Length; i++)
        ret[i] = v1[i] - v2[i];

      return ret;
    }

    private static float[] MemberwiseAdd(float[] v1, int[] v2)
    {
      if (v1.Length != v2.Length)
        throw new ArrayMismatchException(v1.Length, v2.Length);

      float[] ret = new float[v1.Length];
      for (int i = 0; i < ret.Length; i++)
        ret[i] = v1[i] + v2[i];

      return ret;
    }

    private static float[] MemberwiseSubstract(float[] v1, int[] v2)
    {
      if (v1.Length != v2.Length)
        throw new ArrayMismatchException(v1.Length, v2.Length);

      float[] ret = new float[v1.Length];
      for (int i = 0; i < ret.Length; i++)
        ret[i] = v1[i] - v2[i];

      return ret;
    }

    private static float[] MemberwiseAdd(int[] v1, float[] v2)
    {
      if (v1.Length != v2.Length)
        throw new ArrayMismatchException(v1.Length, v2.Length);

      float[] ret = new float[v1.Length];
      for (int i = 0; i < ret.Length; i++)
        ret[i] = v1[i] + v2[i];

      return ret;
    }

    private static float[] MemberwiseSubstract(int[] v1, float[] v2)
    {
      if (v1.Length != v2.Length)
        throw new ArrayMismatchException(v1.Length, v2.Length);

      float[] ret = new float[v1.Length];
      for (int i = 0; i < ret.Length; i++)
        ret[i] = v1[i] - v2[i];

      return ret;
    }

    /// <summary>Perform implicit casting</summary>
    public static Val ImplicitCast(Type t, Val val)
    {
      Type vt = val.Type;
      if (t == vt)
        return val;

      if (ImplicitConversionTable.HasImplicitConversion(vt, t, out Type type))
      {
        if (val.Value is IConvertible)
        {
          return new Val(Convert.ChangeType(val.Value, t));
        }
        else
        {
          MethodInfo mRead = typeof(Ops).GetMethod(nameof(Cast), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
          MethodInfo gmRead = mRead.MakeGenericMethod(new Type[] { vt, t });
          object o = gmRead.Invoke(null, new object[] { val.Value, type });
          return new Val(o);
        }
      }
      else
        throw new ValTypeMismatchException(val.Type, t);
    }

    private static TOut Cast<TIn, TOut>(TIn value, Type type)
    {
      return UnaryOp<TIn>.CastIntermediate<TOut>(value, type);
    }

    /// <summary>Perform implicit casting towards a common type</summary>
    public static void ImplicitCast(ref Val v1, ref Val v2)
    {
      Type vt1 = v1.Type;
      Type vt2 = v2.Type;
      if (vt1 == vt2)
        return;

      if (ImplicitConversionTable.HasImplicitConversion(vt1, vt2, out _)) { v1 = ImplicitCast(vt2, v1); }
      else if (ImplicitConversionTable.HasImplicitConversion(vt2, vt1, out _)) { v2 = ImplicitCast(vt1, v2); }
      //else throw new ValTypeMismatchException(vt1, vt2);

      return;
    }
  }
}
