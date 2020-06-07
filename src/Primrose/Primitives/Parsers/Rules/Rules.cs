using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;
using System;
using System.Collections.Generic;

namespace Primrose.Primitives.Parsers
{
  public static partial class Parser
  {
    /// <summary>Defines parsing rules for interpreting strings into values</summary>
    public static class Rules
    {
      /// <summary>Parses a string to a bool value</summary>
      /// <param name="value">The string to be parsed</param>
      public static bool ToBool(string value)
      {
        string s = value.ToLower().Trim();
        if (s.Equals("0") || s.Trim().Equals("false") || s.Equals("no"))
          return false;
        else if (s.Equals("1") || s.Equals("true") || s.Equals("yes"))
          return true;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to a byte value</summary>
      /// <param name="value">The string to be parsed</param>
      public static byte ToByte(string value)
      {
        byte ret;
        if (value != null && byte.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to a byte2 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static byte2 ToByte2(string value)
      {
        byte2 ret = default(byte2); ;
        if (value != null && byte2.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to a byte3 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static byte3 ToByte3(string value)
      {
        byte3 ret = default(byte3);
        if (value != null && byte3.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to a byte4 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static byte4 ToByte4(string value)
      {
        byte4 ret = default(byte4);
        if (value != null && byte4.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to a short value</summary>
      /// <param name="value">The string to be parsed</param>
      public static short ToShort(string value)
      {
        short ret = default(short);
        if (value != null && short.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to a short2 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static short2 ToShort2(string value)
      {
        short2 ret = default(short2);
        if (value != null && short2.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to a short3 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static short3 ToShort3(string value)
      {
        short3 ret = default(short3);
        if (value != null && short3.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to a short4 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static short4 ToShort4(string value)
      {
        short4 ret = default(short4);
        if (value != null && short4.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to an int value</summary>
      /// <param name="value">The string to be parsed</param>
      public static int ToInt(string value)
      {
        int ret = default(int);
        if (value != null && int.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to an int2 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static int2 ToInt2(string value)
      {
        int2 ret = default(int2);
        if (value != null && int2.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to an int3 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static int3 ToInt3(string value)
      {
        int3 ret = default(int3);
        if (value != null && int3.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to an int4 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static int4 ToInt4(string value)
      {
        int4 ret = default(int4);
        if (value != null && int4.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to an uint value</summary>
      /// <param name="value">The string to be parsed</param>
      public static uint ToUInt(string value)
      {
        uint ret = default(uint);
        if (value != null && uint.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to an uint2 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static uint2 ToUInt2(string value)
      {
        uint2 ret = default(uint2);
        if (value != null && uint2.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to an uint3 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static uint3 ToUInt3(string value)
      {
        uint3 ret = default(uint3);
        if (value != null && uint3.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to an uint4 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static uint4 ToUInt4(string value)
      {
        uint4 ret = default(uint4);
        if (value != null && uint4.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to a long value</summary>
      /// <param name="value">The string to be parsed</param>
      public static long ToLong(string value)
      {
        long ret = default(long);
        if (value != null && long.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to an ulong value</summary>
      /// <param name="value">The string to be parsed</param>
      public static ulong ToULong(string value)
      {
        ulong ret = default(ulong);
        if (value != null && ulong.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to a float value</summary>
      /// <param name="value">The string to be parsed</param>
      public static float ToFloat(string value)
      {
        float ret = default(float);
        if (value != null && float.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to a float2 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static float2 ToFloat2(string value)
      {
        float2 ret = default(float2);
        if (value != null && float2.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to a float3 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static float3 ToFloat3(string value)
      {
        float3 ret = default(float3);
        if (value != null && float3.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to a float4 value</summary>
      /// <param name="value">The string to be parsed</param>
      public static float4 ToFloat4(string value)
      {
        float4 ret = default(float4);
        if (value != null && float4.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(float4), value);
      }

      /// <summary>Parses a string to a double value</summary>
      /// <param name="value">The string to be parsed</param>
      public static double ToDouble(string value)
      {
        double ret = default(double);
        if (value != null && double.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(double), value);
      }

      /// <summary>Parses a string to an array of values</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static T ToArray<T>(string value, IResolver resolver)
      {
        if (string.IsNullOrWhiteSpace(value))
          throw new RuleConversionException(typeof(T), value);

        Type et = typeof(T).GetElementType();

        int n = _tokens.Get(et);
        string[] tokens = new List<string>(value.SplitBy(ListDelimiter[0], n)).ToArray();

        Array array = Array.CreateInstance(et, tokens.Length);
        //object defaultElement = (et.IsValueType) ? Activator.CreateInstance(et) : null;

        for (int i = 0; i < tokens.Length; i++)
        {
          array.SetValue(_fromStr.Get(et).Invoke(tokens[i], resolver), i);
        }

        return (T)(object)array;
      }

      /// <summary>Parses a string to a enumerated value</summary>
      /// <param name="value">The string to be parsed</param>
      public static T ToEnum<T>(string value)
      {
        string s = value.Replace("|", ","); ;
        try { return (T)Enum.Parse(typeof(T), s); }
        catch { throw new RuleConversionException(typeof(T), value); }
      }

      /// <summary>Parses a string to an array of enumerated values</summary>
      /// <param name="value">The string to be parsed</param>
      public static T ToEnumArray<T>(string value)
      {
        if (string.IsNullOrWhiteSpace(value))
          throw new RuleConversionException(typeof(T), value);

        string[] tokens = value.Split(ListDelimiter);

        Type et = typeof(T).GetElementType();
        Array array = Array.CreateInstance(et, tokens.Length);

        for (int i = 0; i < tokens.Length; i++)
        {
          array.SetValue(Enum.Parse(et, tokens[i]), i);
        }
        return (T)(object)array;
      }

      /// <summary>Converts a value to a string</summary>
      /// <param name="value">The value to be converted</param>
      public static string ToStrGeneric<T>(T value)
      {
        return value.ToString();
      }

      /// <summary>Converts an array of values to a string</summary>
      /// <param name="list">The value to be converted</param>
      public static string ArrayToStr<T>(T list)
      {
        return string.Join(ListDelimiter[0].ToString(), list);
      }

      /// <summary>Converts an enumerated value to a string</summary>
      /// <param name="value">The value to be converted</param>
      public static string EnumToStr<T>(T value)
      {
        return value.ToString().Replace(", ", "|");
      }

      /// <summary>Converts an array of enumerated values to a string</summary>
      /// <param name="list">The value to be converted</param>
      public static string EnumArrayToStr<T>(T list)
      {
        return string.Join(ListDelimiter[0].ToString(), list);
      }

      /// <summary>Converts a vectorized value to a string</summary>
      /// <param name="value">The value to be converted</param>
      public static string VecNToStr<T>(T value)
      {
        return value.ToString().Trim('{', '}');
      }
    }
  }
}
