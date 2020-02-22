using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.ValueTypes;
using System;
using System.Text;

namespace Primrose.Primitives.Parsers
{
  /// <summary>Handles the parsing of primitive values to strings and vice versa</summary>
  public static partial class Parser
  {
    /// <summary>The default list delimiter</summary>
    public static readonly char[] ListDelimiter = new char[] { ',' };

    private static Registry<Type, Func<string, object, object>> _fromStr = new Registry<Type, Func<string, object, object>>();
    private static Registry<Type, int> _tokens = new Registry<Type, int>();
    private static Registry<Type, Func<object, string>> _toStr = new Registry<Type, Func<object, string>>();

    static Parser()
    {
      _fromStr.Add(typeof(bool), (s, d) => Rules.ToBool(s, (bool)d));
      _fromStr.Add(typeof(byte), (s, d) => Rules.ToByte(s, (byte)d));
      _fromStr.Add(typeof(short), (s, d) => Rules.ToShort(s, (short)d));
      _fromStr.Add(typeof(int), (s, d) => Rules.ToInt(s, (int)d));
      _fromStr.Add(typeof(uint), (s, d) => Rules.ToUInt(s, (uint)d));
      _fromStr.Add(typeof(long), (s, d) => Rules.ToLong(s, (long)d));
      _fromStr.Add(typeof(float), (s, d) => Rules.ToFloat(s, (float)d));
      _fromStr.Add(typeof(float2), (s, d) => Rules.ToFloat2(s, (float2)d));
      _fromStr.Add(typeof(float3), (s, d) => Rules.ToFloat3(s, (float3)d));
      _fromStr.Add(typeof(float4), (s, d) => Rules.ToFloat4(s, (float4)d));
      _fromStr.Add(typeof(double), (s, d) => Rules.ToDouble(s, (double)d));
      _fromStr.Add(typeof(string), (s, d) => s);
      _fromStr.Add(typeof(StringBuilder), (s, d) => new StringBuilder(s));

      _toStr.Add(typeof(bool), Rules.ToStrGeneric);
      _toStr.Add(typeof(byte), Rules.ToStrGeneric);
      _toStr.Add(typeof(short), Rules.ToStrGeneric);
      _toStr.Add(typeof(int), Rules.ToStrGeneric);
      _toStr.Add(typeof(uint), Rules.ToStrGeneric);
      _toStr.Add(typeof(long), Rules.ToStrGeneric);
      _toStr.Add(typeof(float), Rules.ToStrGeneric);
      _toStr.Add(typeof(float2), (o) => Rules.FloatNToStr((float2)o));
      _toStr.Add(typeof(float3), (o) => Rules.FloatNToStr((float3)o));
      _toStr.Add(typeof(float4), (o) => Rules.FloatNToStr((float4)o));
      _toStr.Add(typeof(double), Rules.ToStrGeneric);
      _toStr.Add(typeof(string), Rules.ToStrGeneric);
      _toStr.Add(typeof(StringBuilder), Rules.ToStrGeneric);

      _tokens.Default = 1;
      _tokens.Add(typeof(float2), 2);
      _tokens.Add(typeof(float3), 3);
      _tokens.Add(typeof(float4), 4);
    }

    /// <summary>Parses a value from its string representation</summary>
    /// <typeparam name="T">The type of the value</typeparam>
    /// <param name="value">The value to be parsed</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The parsed value</returns>
    public static T Parse<T>(string value, T defaultValue)
    {
      if (value == null)
        return defaultValue;

      Type t = typeof(T); // defaultValue?.GetType() ?? typeof(T);
      if (_fromStr.Contains(t))
      {
        return (T)_fromStr.Get(t).Invoke(value, defaultValue);
      }
      else if (t.IsArray && _fromStr.Contains(t.GetElementType()))
      {
        return Rules.ToArray(value, defaultValue);
      }
      else if (t.IsEnum)
      {
        return Rules.ToEnum(value, defaultValue);
      }
      else if (t.IsArray && t.GetElementType().IsEnum)
      {
        return Rules.ToEnumArray(value, defaultValue);
      }

      throw new InvalidOperationException("Attempted to parse a string to an unsupported type '{0}'".F(t.Name));
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

      throw new InvalidOperationException("Attempted to write a string from an unsupported type '{0}'".F(t.Name));
    }
  }
}
