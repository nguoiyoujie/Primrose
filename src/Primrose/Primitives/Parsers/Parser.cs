using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.ValueTypes;
using System;
using System.Text;

namespace Primrose.Primitives.Parsers
{
  /// <summary>Allows customized resolution of parsing contentions, if any</summary>
  public interface IResolver
  {
    /// <summary>Resolves parsing contentions for a given input</summary>
    /// <param name="input">The input to resolve</param>
    string Resolve(string input);
  }
  
  /// <summary>Handles the parsing of primitive values to strings and vice versa</summary>
  public static partial class Parser
  {
    /// <summary>The default list delimiter</summary>
    public static readonly char[] ListDelimiter = new char[] { ',' };

    private static Registry<Type, Func<string, IResolver, object>> _fromStr = new Registry<Type, Func<string, IResolver, object>>();
    private static Registry<Type, int> _tokens = new Registry<Type, int>();
    private static Registry<Type, Func<object, string>> _toStr = new Registry<Type, Func<object, string>>();

    static Parser()
    {
      _fromStr.Add(typeof(bool), (s, r) => Rules.ToBool(s, r));
      _fromStr.Add(typeof(byte), (s, r) => Rules.ToByte(s, r));
      _fromStr.Add(typeof(byte2), (s, r) => Rules.ToByte2(s, r));
      _fromStr.Add(typeof(byte3), (s, r) => Rules.ToByte3(s, r));
      _fromStr.Add(typeof(byte4), (s, r) => Rules.ToByte4(s, r));
      _fromStr.Add(typeof(sbyte), (s, r) => Rules.ToSByte(s, r));
      _fromStr.Add(typeof(sbyte2), (s, r) => Rules.ToSByte2(s, r));
      _fromStr.Add(typeof(sbyte3), (s, r) => Rules.ToSByte3(s, r));
      _fromStr.Add(typeof(sbyte4), (s, r) => Rules.ToSByte4(s, r));
      _fromStr.Add(typeof(short), (s, r) => Rules.ToShort(s, r));
      _fromStr.Add(typeof(short2), (s, r) => Rules.ToShort2(s, r));
      _fromStr.Add(typeof(short3), (s, r) => Rules.ToShort3(s, r));
      _fromStr.Add(typeof(short4), (s, r) => Rules.ToShort4(s, r));
      _fromStr.Add(typeof(ushort), (s, r) => Rules.ToUShort(s, r));
      _fromStr.Add(typeof(ushort2), (s, r) => Rules.ToUShort2(s, r));
      _fromStr.Add(typeof(ushort3), (s, r) => Rules.ToUShort3(s, r));
      _fromStr.Add(typeof(ushort4), (s, r) => Rules.ToUShort4(s, r));
      _fromStr.Add(typeof(int), (s, r) => Rules.ToInt(s, r));
      _fromStr.Add(typeof(int2), (s, r) => Rules.ToInt2(s, r));
      _fromStr.Add(typeof(int3), (s, r) => Rules.ToInt3(s, r));
      _fromStr.Add(typeof(int4), (s, r) => Rules.ToInt4(s, r));
      _fromStr.Add(typeof(uint), (s, r) => Rules.ToUInt(s, r));
      _fromStr.Add(typeof(uint2), (s, r) => Rules.ToUInt2(s, r));
      _fromStr.Add(typeof(uint3), (s, r) => Rules.ToUInt3(s, r));
      _fromStr.Add(typeof(uint4), (s, r) => Rules.ToUInt4(s, r));
      _fromStr.Add(typeof(long), (s, r) => Rules.ToLong(s, r));
      _fromStr.Add(typeof(ulong), (s, r) => Rules.ToULong(s, r));
      _fromStr.Add(typeof(float), (s, r) => Rules.ToFloat(s, r));
      _fromStr.Add(typeof(float2), (s, r) => Rules.ToFloat2(s, r));
      _fromStr.Add(typeof(float3), (s, r) => Rules.ToFloat3(s, r));
      _fromStr.Add(typeof(float4), (s, r) => Rules.ToFloat4(s, r));
      _fromStr.Add(typeof(double), (s, r) => Rules.ToDouble(s, r));
      _fromStr.Add(typeof(string), (s, r) => s);
      _fromStr.Add(typeof(StringBuilder), (s, r) => new StringBuilder(s));

      _toStr.Add(typeof(bool), Rules.ToStrGeneric);
      _toStr.Add(typeof(byte), Rules.ToStrGeneric);
      _toStr.Add(typeof(byte2), (o) => Rules.VecNToStr((byte2)o));
      _toStr.Add(typeof(byte3), (o) => Rules.VecNToStr((byte3)o));
      _toStr.Add(typeof(byte4), (o) => Rules.VecNToStr((byte4)o));
      _toStr.Add(typeof(sbyte), Rules.ToStrGeneric);
      _toStr.Add(typeof(sbyte2), (o) => Rules.VecNToStr((byte2)o));
      _toStr.Add(typeof(sbyte3), (o) => Rules.VecNToStr((byte3)o));
      _toStr.Add(typeof(sbyte4), (o) => Rules.VecNToStr((byte4)o));
      _toStr.Add(typeof(short), Rules.ToStrGeneric);
      _toStr.Add(typeof(short2), (o) => Rules.VecNToStr((short2)o));
      _toStr.Add(typeof(short3), (o) => Rules.VecNToStr((short3)o));
      _toStr.Add(typeof(short4), (o) => Rules.VecNToStr((short4)o));
      _toStr.Add(typeof(ushort), Rules.ToStrGeneric);
      _toStr.Add(typeof(ushort2), (o) => Rules.VecNToStr((short2)o));
      _toStr.Add(typeof(ushort3), (o) => Rules.VecNToStr((short3)o));
      _toStr.Add(typeof(ushort4), (o) => Rules.VecNToStr((short4)o));
      _toStr.Add(typeof(int), Rules.ToStrGeneric);
      _toStr.Add(typeof(int2), (o) => Rules.VecNToStr((int2)o));
      _toStr.Add(typeof(int3), (o) => Rules.VecNToStr((int3)o));
      _toStr.Add(typeof(int4), (o) => Rules.VecNToStr((int4)o));
      _toStr.Add(typeof(uint), Rules.ToStrGeneric);
      _toStr.Add(typeof(uint2), (o) => Rules.VecNToStr((uint2)o));
      _toStr.Add(typeof(uint3), (o) => Rules.VecNToStr((uint3)o));
      _toStr.Add(typeof(uint4), (o) => Rules.VecNToStr((uint4)o));
      _toStr.Add(typeof(long), Rules.ToStrGeneric);
      _toStr.Add(typeof(ulong), Rules.ToStrGeneric);
      _toStr.Add(typeof(float), Rules.ToStrGeneric);
      _toStr.Add(typeof(float2), (o) => Rules.VecNToStr((float2)o));
      _toStr.Add(typeof(float3), (o) => Rules.VecNToStr((float3)o));
      _toStr.Add(typeof(float4), (o) => Rules.VecNToStr((float4)o));
      _toStr.Add(typeof(double), Rules.ToStrGeneric);
      _toStr.Add(typeof(string), Rules.ToStrGeneric);
      _toStr.Add(typeof(StringBuilder), Rules.ToStrGeneric);

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
        return (T)_fromStr.Get(t).Invoke(value, resolver);
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
        return _toStr.Get(t).Invoke(value);
      }
      else if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
      {
        Type et = Nullable.GetUnderlyingType(typeof(T));
        if (_fromStr.Contains(et))
        {
          return _toStr.Get(et).Invoke(value);
        }
        else if (et.IsEnum)
        {
          return Rules.EnumToStr(value);
        }
      }
      else if (t.IsArray && _fromStr.Contains(t.GetElementType()))
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
      _fromStr.Put(typeof(T), (s, r) => parserRule(s, r));

      if (writerRule == null)
        _toStr.Put(typeof(T), Rules.ToStrGeneric);
      else
        _toStr.Put(typeof(T), (n) => writerRule((T)n));

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
  }
}
