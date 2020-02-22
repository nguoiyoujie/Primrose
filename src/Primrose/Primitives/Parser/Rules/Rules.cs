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
      internal static bool ToBool(string value, bool defaultValue = false)
      {
        string s = value.ToLower().Trim();
        if (s.Equals("0") || s.Trim().Equals("false") || s.Equals("no"))
          return false;
        else if (s.Equals("1") || s.Equals("true") || s.Equals("yes"))
          return true;
        return defaultValue;
      }

      internal static byte ToByte(string value, byte defaultValue = 0)
      {
        byte ret = defaultValue;
        if (value != null && byte.TryParse(value, out ret))
          return ret;
        return defaultValue;
      }

      internal static short ToShort(string value, short defaultValue = 0)
      {
        short ret = defaultValue;
        if (value != null && short.TryParse(value, out ret))
          return ret;
        return defaultValue;
      }

      internal static int ToInt(string value, int defaultValue = 0)
      {
        int ret = defaultValue;
        if (value != null && int.TryParse(value, out ret))
          return ret;
        return defaultValue;
      }

      internal static uint ToUInt(string value, uint defaultValue = 0)
      {
        uint ret = defaultValue;
        if (value != null && uint.TryParse(value, out ret))
          return ret;
        return defaultValue;
      }

      internal static long ToLong(string value, long defaultValue = 0)
      {
        long ret = defaultValue;
        if (value != null && long.TryParse(value, out ret))
          return ret;
        return defaultValue;
      }

      internal static float ToFloat(string value, float defaultValue = 0)
      {
        float ret = defaultValue;
        if (value != null && float.TryParse(value, out ret))
          return ret;
        return defaultValue;
      }

      internal static float2 ToFloat2(string value, float2 defaultValue = default(float2))
      {
        float2 ret = defaultValue;
        if (value != null && float2.TryParse(value, defaultValue, out ret))
          return ret;
        return defaultValue;
      }

      internal static float3 ToFloat3(string value, float3 defaultValue = default(float3))
      {
        float3 ret = defaultValue;
        if (value != null && float3.TryParse(value, defaultValue, out ret))
          return ret;
        return defaultValue;
      }

      internal static float4 ToFloat4(string value, float4 defaultValue = default(float4))
      {
        float4 ret = defaultValue;
        if (value != null && float4.TryParse(value, defaultValue, out ret))
          return ret;
        return defaultValue;
      }

      internal static double ToDouble(string value, double defaultValue = 0)
      {
        double ret = defaultValue;
        if (value != null && double.TryParse(value, out ret))
          return ret;
        return defaultValue;
      }



      internal static T ToArray<T>(string value, T defaultList)
      {
        if (string.IsNullOrWhiteSpace(value))
          return defaultList;

        Type et = typeof(T).GetElementType();

        int n = _tokens.Get(et);
        string[] tokens = new List<string>(value.SplitBy(ListDelimiter[0], n)).ToArray();

        Array array = Array.CreateInstance(et, tokens.Length);
        object defaultElement = (et.IsValueType) ? Activator.CreateInstance(et) : null;

        for (int i = 0; i < tokens.Length; i++)
        {
          try { array.SetValue(_fromStr.Get(et).Invoke(tokens[i], defaultElement), i); }
          catch { }
        }

        return (T)(object)array;
      }

      internal static T ToEnum<T>(string value, T defaultValue)
      {
        string s = value.Replace("|", ","); ;
        try { return (T)Enum.Parse(typeof(T), s); }
        catch { return defaultValue; }
      }

      internal static T ToEnumArray<T>(string value, T defaultList)
      {
        if (string.IsNullOrWhiteSpace(value))
          return defaultList;

        string[] tokens = value.Split(ListDelimiter);

        Type et = typeof(T).GetElementType();
        Array array = Array.CreateInstance(et, tokens.Length);

        for (int i = 0; i < tokens.Length; i++)
        {
          try { array.SetValue(Enum.Parse(et, tokens[i]), i); }
          catch { }
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

      internal static string FloatNToStr<T>(T value)
      {
        return value.ToString().Trim('{', '}');
      }
    }
  }
}
