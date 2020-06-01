using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;
using System;
using System.Collections.Generic;

namespace Primrose.Primitives.Parsers
{
  public static partial class Parser
  {
    internal static class Rules
    {
      internal static bool ToBool(string value)
      {
        string s = value.ToLower().Trim();
        if (s.Equals("0") || s.Trim().Equals("false") || s.Equals("no"))
          return false;
        else if (s.Equals("1") || s.Equals("true") || s.Equals("yes"))
          return true;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static byte ToByte(string value)
      {
        byte ret;
        if (value != null && byte.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static byte2 ToByte2(string value)
      {
        byte2 ret = default(byte2); ;
        if (value != null && byte2.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static byte3 ToByte3(string value)
      {
        byte3 ret = default(byte3);
        if (value != null && byte3.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static byte4 ToByte4(string value)
      {
        byte4 ret = default(byte4);
        if (value != null && byte4.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static short ToShort(string value)
      {
        short ret = default(short);
        if (value != null && short.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static short2 ToShort2(string value)
      {
        short2 ret = default(short2);
        if (value != null && short2.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static short3 ToShort3(string value)
      {
        short3 ret = default(short3);
        if (value != null && short3.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static short4 ToShort4(string value)
      {
        short4 ret = default(short4);
        if (value != null && short4.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static int ToInt(string value)
      {
        int ret = default(int);
        if (value != null && int.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static int2 ToInt2(string value)
      {
        int2 ret = default(int2);
        if (value != null && int2.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static int3 ToInt3(string value)
      {
        int3 ret = default(int3);
        if (value != null && int3.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static int4 ToInt4(string value)
      {
        int4 ret = default(int4);
        if (value != null && int4.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static uint ToUInt(string value)
      {
        uint ret = default(uint);
        if (value != null && uint.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static uint2 ToUInt2(string value)
      {
        uint2 ret = default(uint2);
        if (value != null && uint2.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static uint3 ToUInt3(string value)
      {
        uint3 ret = default(uint3);
        if (value != null && uint3.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static uint4 ToUInt4(string value)
      {
        uint4 ret = default(uint4);
        if (value != null && uint4.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static long ToLong(string value)
      {
        long ret = default(long);
        if (value != null && long.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static ulong ToULong(string value)
      {
        ulong ret = default(ulong);
        if (value != null && ulong.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static float ToFloat(string value)
      {
        float ret = default(float);
        if (value != null && float.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static float2 ToFloat2(string value)
      {
        float2 ret = default(float2);
        if (value != null && float2.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static float3 ToFloat3(string value)
      {
        float3 ret = default(float3);
        if (value != null && float3.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(bool), value);
      }

      internal static float4 ToFloat4(string value)
      {
        float4 ret = default(float4);
        if (value != null && float4.TryParse(value, ret, out ret))
          return ret;
        throw new RuleConversionException(typeof(float4), value);
      }

      internal static double ToDouble(string value)
      {
        double ret = default(double);
        if (value != null && double.TryParse(value, out ret))
          return ret;
        throw new RuleConversionException(typeof(double), value);
      }

      internal static T ToArray<T>(string value)
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
          array.SetValue(_fromStr.Get(et).Invoke(tokens[i]), i);
        }

        return (T)(object)array;
      }

      internal static T ToEnum<T>(string value)
      {
        string s = value.Replace("|", ","); ;
        try { return (T)Enum.Parse(typeof(T), s); }
        catch { throw new RuleConversionException(typeof(T), value); }
      }

      internal static T ToEnumArray<T>(string value)
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

      internal static string ToStrGeneric<T>(T value)
      {
        return value.ToString();
      }

      internal static string ArrayToStr<T>(T list)
      {
        return string.Join(ListDelimiter[0].ToString(), list);
      }

      internal static string EnumToStr<T>(T value)
      {
        return value.ToString().Replace(", ", "|");
      }

      internal static string EnumArrayToStr<T>(T list)
      {
        return string.Join(ListDelimiter[0].ToString(), list);
      }

      internal static string VecNToStr<T>(T value)
      {
        return value.ToString().Trim('{', '}');
      }
    }
  }
}
