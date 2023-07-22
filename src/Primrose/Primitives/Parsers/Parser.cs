using Primrose.Primitives.Factories;
using Primrose.Primitives.ValueTypes;
using System;
using System.Text;

namespace Primrose.Primitives.Parsers
{
  /// <summary>Handles the parsing of primitive values to strings and vice versa</summary>
  public static partial class Parser
  {
    // Delegate: Func<string, IResolver, T>
    private static readonly Registry<Type, Delegate> _fromStr = new Registry<Type, Delegate>();
    private static readonly Registry<Type, int> _tokens = new Registry<Type, int>();

    // Delegate: Func<T, string>
    private static readonly Registry<Type, Delegate> _toStr = new Registry<Type, Delegate>();

    static Parser()
    {
      _fromStr.Add(typeof(bool), (Func<string, IResolver, bool>)Rules.ToBool);
      _fromStr.Add(typeof(byte), (Func<string, IResolver, byte>)Rules.ToByte);
      _fromStr.Add(typeof(byte2), (Func<string, IResolver, byte2>)Rules.ToByte2);
      _fromStr.Add(typeof(byte3), (Func<string, IResolver, byte3>)Rules.ToByte3);
      _fromStr.Add(typeof(byte4), (Func<string, IResolver, byte4>)Rules.ToByte4);
      _fromStr.Add(typeof(sbyte), (Func<string, IResolver, sbyte>)Rules.ToSByte);
      _fromStr.Add(typeof(sbyte2), (Func<string, IResolver, sbyte2>)Rules.ToSByte2);
      _fromStr.Add(typeof(sbyte3), (Func<string, IResolver, sbyte3>)Rules.ToSByte3);
      _fromStr.Add(typeof(sbyte4), (Func<string, IResolver, sbyte4>)Rules.ToSByte4);
      _fromStr.Add(typeof(short), (Func<string, IResolver, short>)Rules.ToShort);
      _fromStr.Add(typeof(short2), (Func<string, IResolver, short2>)Rules.ToShort2);
      _fromStr.Add(typeof(short3), (Func<string, IResolver, short3>)Rules.ToShort3);
      _fromStr.Add(typeof(short4), (Func<string, IResolver, short4>)Rules.ToShort4);
      _fromStr.Add(typeof(ushort), (Func<string, IResolver, ushort>)Rules.ToUShort);
      _fromStr.Add(typeof(ushort2), (Func<string, IResolver, ushort2>)Rules.ToUShort2);
      _fromStr.Add(typeof(ushort3), (Func<string, IResolver, ushort3>)Rules.ToUShort3);
      _fromStr.Add(typeof(ushort4), (Func<string, IResolver, ushort4>)Rules.ToUShort4);
      _fromStr.Add(typeof(int), (Func<string, IResolver, int>)Rules.ToInt);
      _fromStr.Add(typeof(int2), (Func<string, IResolver, int2>)Rules.ToInt2);
      _fromStr.Add(typeof(int3), (Func<string, IResolver, int3>)Rules.ToInt3);
      _fromStr.Add(typeof(int4), (Func<string, IResolver, int4>)Rules.ToInt4);
      _fromStr.Add(typeof(intRect), (Func<string, IResolver, intRect>)Rules.ToIntRect);
      _fromStr.Add(typeof(uint), (Func<string, IResolver, uint>)Rules.ToUInt);
      _fromStr.Add(typeof(uint2), (Func<string, IResolver, uint2>)Rules.ToUInt2);
      _fromStr.Add(typeof(uint3), (Func<string, IResolver, uint3>)Rules.ToUInt3);
      _fromStr.Add(typeof(uint4), (Func<string, IResolver, uint4>)Rules.ToUInt4);
      _fromStr.Add(typeof(long), (Func<string, IResolver, long>)Rules.ToLong);
      _fromStr.Add(typeof(ulong), (Func<string, IResolver, ulong>)Rules.ToULong);
      _fromStr.Add(typeof(float), (Func<string, IResolver, float>)Rules.ToFloat);
      _fromStr.Add(typeof(float2), (Func<string, IResolver, float2>)Rules.ToFloat2);
      _fromStr.Add(typeof(float3), (Func<string, IResolver, float3>)Rules.ToFloat3);
      _fromStr.Add(typeof(float4), (Func<string, IResolver, float4>)Rules.ToFloat4);
      _fromStr.Add(typeof(double), (Func<string, IResolver, double>)Rules.ToDouble);
      _fromStr.Add(typeof(string), (Func<string, IResolver, string>)Rules.ToString);
      _fromStr.Add(typeof(StringBuilder), (Func<string, IResolver, StringBuilder>)((s, r) => { StringBuilder sb = ObjectPool<StringBuilder>.GetStaticPool().GetNew(); sb.Append(Rules.ToString(s, r)); return sb; }));

      _toStr.Add(typeof(bool), (Func<bool, string>)Rules.ToStrGeneric);
      _toStr.Add(typeof(byte), (Func<bool, string>)Rules.ToStrGeneric);
      _toStr.Add(typeof(byte2), (Func<byte2, string>)Rules.VecNToStr);
      _toStr.Add(typeof(byte3), (Func<byte3, string>)Rules.VecNToStr);
      _toStr.Add(typeof(byte4), (Func<byte4, string>)Rules.VecNToStr);
      _toStr.Add(typeof(sbyte), (Func<sbyte, string>)Rules.ToStrGeneric);
      _toStr.Add(typeof(sbyte2), (Func<sbyte2, string>)Rules.VecNToStr);
      _toStr.Add(typeof(sbyte3), (Func<sbyte3, string>)Rules.VecNToStr);
      _toStr.Add(typeof(sbyte4), (Func<sbyte4, string>)Rules.VecNToStr);
      _toStr.Add(typeof(short), (Func<short, string>)Rules.ToStrGeneric);
      _toStr.Add(typeof(short2), (Func<short2, string>)Rules.VecNToStr);
      _toStr.Add(typeof(short3), (Func<short3, string>)Rules.VecNToStr);
      _toStr.Add(typeof(short4), (Func<short4, string>)Rules.VecNToStr);
      _toStr.Add(typeof(ushort), (Func<ushort, string>)Rules.ToStrGeneric);
      _toStr.Add(typeof(ushort2), (Func<ushort2, string>)Rules.VecNToStr);
      _toStr.Add(typeof(ushort3), (Func<ushort3, string>)Rules.VecNToStr);
      _toStr.Add(typeof(ushort4), (Func<ushort4, string>)Rules.VecNToStr);
      _toStr.Add(typeof(int), (Func<int, string>)Rules.ToStrGeneric);
      _toStr.Add(typeof(int2), (Func<int2, string>)Rules.VecNToStr);
      _toStr.Add(typeof(int3), (Func<int3, string>)Rules.VecNToStr);
      _toStr.Add(typeof(int4), (Func<int4, string>)Rules.VecNToStr);
      _toStr.Add(typeof(intRect), (Func<intRect, string>)Rules.VecNToStr);
      _toStr.Add(typeof(uint), (Func<uint, string>)Rules.ToStrGeneric);
      _toStr.Add(typeof(uint2), (Func<uint2, string>)Rules.VecNToStr);
      _toStr.Add(typeof(uint3), (Func<uint3, string>)Rules.VecNToStr);
      _toStr.Add(typeof(uint4), (Func<uint4, string>)Rules.VecNToStr);
      _toStr.Add(typeof(long), (Func<long, string>)Rules.ToStrGeneric);
      _toStr.Add(typeof(ulong), (Func<ulong, string>)Rules.ToStrGeneric);
      _toStr.Add(typeof(float), (Func<float, string>)Rules.ToStrGeneric);
      _toStr.Add(typeof(float2), (Func<float2, string>)Rules.VecNToStr);
      _toStr.Add(typeof(float3), (Func<float3, string>)Rules.VecNToStr);
      _toStr.Add(typeof(float4), (Func<float4, string>)Rules.VecNToStr);
      _toStr.Add(typeof(double), (Func<double, string>)Rules.ToStrGeneric);
      _toStr.Add(typeof(string), (Func<string, string>)Rules.ToStrGeneric);
      _toStr.Add(typeof(StringBuilder), (Func<StringBuilder, string>)Rules.ToStrGeneric);

      _tokens.Default = 1;
      _tokens.Add(typeof(byte2), 2);
      _tokens.Add(typeof(byte3), 3);
      _tokens.Add(typeof(byte4), 4);
      _tokens.Add(typeof(sbyte2), 2);
      _tokens.Add(typeof(sbyte3), 3);
      _tokens.Add(typeof(sbyte4), 4);
      _tokens.Add(typeof(short2), 2);
      _tokens.Add(typeof(short3), 3);
      _tokens.Add(typeof(short4), 4);
      _tokens.Add(typeof(ushort2), 2);
      _tokens.Add(typeof(ushort3), 3);
      _tokens.Add(typeof(ushort4), 4);
      _tokens.Add(typeof(int2), 2);
      _tokens.Add(typeof(int3), 3);
      _tokens.Add(typeof(int4), 4);
      _tokens.Add(typeof(intRect), 4);
      _tokens.Add(typeof(uint2), 2);
      _tokens.Add(typeof(uint3), 3);
      _tokens.Add(typeof(uint4), 4);
      _tokens.Add(typeof(float2), 2);
      _tokens.Add(typeof(float3), 3);
      _tokens.Add(typeof(float4), 4);
    }

    /// <summary>Parses a value from its string representation</summary>
    /// <typeparam name="T">The type of the value</typeparam>
    /// <param name="value">The value to be parsed</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The parsed value, or defaultValue if the string is invalid</returns>
    public static T Parse<T>(string value, T defaultValue) { return Parse<T>(value, null, defaultValue); }

    /// <summary>Parses a value from its string representation</summary>
    /// <typeparam name="T">The type of the value</typeparam>
    /// <param name="value">The value to be parsed</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The parsed value, or defaultValue if the string is invalid</returns>
    public static T Parse<T>(string value, IResolver resolver, T defaultValue)
    {
      if (value == null)
        return defaultValue;

      try
      {
        return Parse<T>(value, resolver);
      }
      catch
      {
        return defaultValue;
      }
    }


    /// <summary>Parses a value from its string representation</summary>
    /// <typeparam name="T">The type of the value</typeparam>
    /// <param name="value">The value to be parsed</param>
    /// <param name="resolver">A string resolver function</param>
    /// <returns>The parsed value</returns>
    public static T Parse<T>(string value, IResolver resolver = null)
    {
      if (value == null)
        return default;

      Type t = typeof(T); // defaultValue?.GetType() ?? typeof(T);
      if (_fromStr.Contains(t))
      {
        Func<string, IResolver, T> func = (Func<string, IResolver, T>)_fromStr.Get(t);
        return func.Invoke(value, resolver);
      }
      else if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
      {
        Type et = Nullable.GetUnderlyingType(typeof(T));
        if (_fromStr.Contains(et))
        {
          return Rules.ToNullable<T>(value, resolver);
        }
        else if (et.IsEnum)
        {
          return Rules.ToNullableEnum<T>(value, resolver);
        }
      }
      else if (t.IsArray && _fromStr.Contains(t.GetElementType()))
      {
        return Rules.ToArray<T>(value, resolver);
      }
      else if (t.IsEnum)
      {
        return Rules.ToEnum<T>(value);
      }
      else if (t.IsArray && t.GetElementType().IsEnum)
      {
        return Rules.ToEnumArray<T>(value);
      }

      throw new UnsupportedParseException<T>();
    }

    /// <summary>Converts a value to its string representation</summary>
    /// <typeparam name="T">The type of the value</typeparam>
    /// <param name="value">The value to be converted to a string</param>
    /// <returns>The string representation of the value</returns>
    public static string Write<T>(T value)
    {
      if (value == null)
        return string.Empty;

      Type t = typeof(T); // value?.GetType() ?? typeof(T);
      if (_toStr.Contains(t))
      {
        Func<T, string> func = (Func<T, string>)_toStr.Get(t);
        return func.Invoke(value);
      }
      else if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
      {
        Type et = Nullable.GetUnderlyingType(typeof(T));
        if (_toStr.Contains(et))
        {
          Func<T, string> func = (Func<T, string>)_toStr.Get(et);
          return func.Invoke(value);
        }
        else if (et.IsEnum)
        {
          return Rules.EnumToStr(value);
        }
      }
      else if (t.IsArray && _toStr.Contains(t.GetElementType()))
      {
        return Rules.ArrayToStr(value);
      }
      else if (t.IsEnum)
      {
        return Rules.EnumToStr(value);
      }
      else if (t.IsArray && t.GetElementType().IsEnum)
      {
        return Rules.EnumArrayToStr(value);
      }

      throw new UnsupportedWriteException<T>();
    }

    /// <summary>Adds a rule to convert between an object and its string representation</summary>
    /// <typeparam name="T">The type of the value</typeparam>
    /// <param name="parserRule">The rule for parsing a string to the object</param>
    /// <param name="tokens">The number of comma delimited elements assigned to a single instance of the object, in the case of array initialization</param>
    /// <param name="writerRule">The rule for expression the object as a string</param>
    public static void AddRule<T>(Func<string, IResolver, T> parserRule, int tokens = 1, Func<T, string> writerRule = null)
    {
      _fromStr.Put(typeof(T), parserRule);

      if (writerRule == null)
        _toStr.Put(typeof(T), (Func<T, string>)Rules.ToStrGeneric);
      else
        _toStr.Put(typeof(T), writerRule);

      _tokens.Put(typeof(T), tokens);
    }

    /// <summary>Deletes the conversion rules for an object type</summary>
    /// <typeparam name="T">The type of the value</typeparam>
    public static void DeleteRule<T>()
    {
      _fromStr.Remove(typeof(T));
      _toStr.Remove(typeof(T));
      _tokens.Remove(typeof(T));
    }

    /*
    /// <summary>
    /// Casts <typeparamref name="TIn"/> to <typeparamref name="TOut"/>.
    /// This does not cause boxing for value types.
    /// Useful in generic methods.
    /// </summary>
    /// <typeparam name="TOut">Target type to cast to. Usually a generic type.</typeparam>
    /// <typeparam name="TIn">Input type to cast from. Usually a generic type.</typeparam>
    public static TOut Cast<TOut, TIn>(TIn s)
    {
      if (typeof(TOut) == typeof(string)) return (TOut)(object)(s.ToString()); // override for string

      return Cache<TOut, TIn>.Cast(s);
    }

    private static class Cache<TOut, TIn>
    {
      public static readonly Func<TIn, TOut> Cast = cast();

      private static Func<TIn, TOut> cast()
      {
        ParameterExpression p = Expression.Parameter(typeof(TIn));
        UnaryExpression c = Expression.ConvertChecked(p, typeof(TOut));
        return Expression.Lambda<Func<TIn, TOut>>(c, p).Compile();
      }
    }
    */
  }
}
