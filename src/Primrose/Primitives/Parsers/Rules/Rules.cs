using Primrose.Primitives.Cache;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Primrose.Primitives.Parsers
{
  public static partial class Parser
  {
    /// <summary>Defines parsing rules for interpreting strings into values</summary>
    public static class Rules
    {
      /// <summary>Resolves a string value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static string ToString(string value, IResolver resolver)
      {
        return resolver?.Resolve(value) ?? value;
      }

      /// <summary>Parses a string to a bool value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static bool ToBool(string value, IResolver resolver)
      {
        value = resolver?.Resolve(value) ?? value;
        if (value.Equals("0") || value.Equals("false", StringComparison.InvariantCultureIgnoreCase) || value.Equals("no", StringComparison.InvariantCultureIgnoreCase))
          return false;
        else if (value.Equals("1") || value.Equals("true", StringComparison.InvariantCultureIgnoreCase) || value.Equals("yes", StringComparison.InvariantCultureIgnoreCase))
          return true;
        throw new RuleConversionException(typeof(bool), value);
      }

      /// <summary>Parses a string to a byte value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static byte ToByte(string value, IResolver resolver)
      {
        value = resolver?.Resolve(value) ?? value;
        if (value != null && byte.TryParse(value, out byte ret))
          return ret;
        throw new RuleConversionException(typeof(byte), value);
      }

      /// <summary>Parses a string to a byte2 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static byte2 ToByte2(string value, IResolver resolver)
      {
        if (value != null && byte2.TryParse(value, out byte2 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(byte2), value);
      }

      /// <summary>Parses a string to a byte3 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static byte3 ToByte3(string value, IResolver resolver)
      {

        if (value != null && byte3.TryParse(value, out byte3 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(byte3), value);
      }

      /// <summary>Parses a string to a byte4 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static byte4 ToByte4(string value, IResolver resolver)
      {
        if (value != null && byte4.TryParse(value, out byte4 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(byte4), value);
      }


      /// <summary>Parses a string to a sbyte value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static sbyte ToSByte(string value, IResolver resolver)
      {
        value = resolver?.Resolve(value) ?? value;
        if (value != null && sbyte.TryParse(value, out sbyte ret))
          return ret;
        throw new RuleConversionException(typeof(sbyte), value);
      }

      /// <summary>Parses a string to a sbyte2 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static sbyte2 ToSByte2(string value, IResolver resolver)
      {
        ;
        if (value != null && sbyte2.TryParse(value, out sbyte2 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(sbyte2), value);
      }

      /// <summary>Parses a string to a sbyte3 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static sbyte3 ToSByte3(string value, IResolver resolver)
      {
        if (value != null && sbyte3.TryParse(value, out sbyte3 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(sbyte3), value);
      }

      /// <summary>Parses a string to a sbyte4 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static sbyte4 ToSByte4(string value, IResolver resolver)
      {
        if (value != null && sbyte4.TryParse(value, out sbyte4 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(sbyte4), value);
      }

      /// <summary>Parses a string to a short value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static short ToShort(string value, IResolver resolver)
      {
        value = resolver?.Resolve(value) ?? value;
        if (value != null && short.TryParse(value, out short ret))
          return ret;
        throw new RuleConversionException(typeof(short), value);
      }

      /// <summary>Parses a string to a short2 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static short2 ToShort2(string value, IResolver resolver)
      {
        if (value != null && short2.TryParse(value, out short2 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(short2), value);
      }

      /// <summary>Parses a string to a short3 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static short3 ToShort3(string value, IResolver resolver)
      {
        if (value != null && short3.TryParse(value, out short3 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(short3), value);
      }

      /// <summary>Parses a string to a short4 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static short4 ToShort4(string value, IResolver resolver)
      {
        if (value != null && short4.TryParse(value, out short4 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(short4), value);
      }

      /// <summary>Parses a string to a ushort value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static ushort ToUShort(string value, IResolver resolver)
      {
        value = resolver?.Resolve(value) ?? value;
        if (value != null && ushort.TryParse(value, out ushort ret))
          return ret;
        throw new RuleConversionException(typeof(ushort), value);
      }

      /// <summary>Parses a string to a ushort2 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static ushort2 ToUShort2(string value, IResolver resolver)
      {
        if (value != null && ushort2.TryParse(value, out ushort2 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(ushort2), value);
      }

      /// <summary>Parses a string to a ushort3 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static ushort3 ToUShort3(string value, IResolver resolver)
      {
        if (value != null && ushort3.TryParse(value, out ushort3 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(ushort3), value);
      }

      /// <summary>Parses a string to a ushort4 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static ushort4 ToUShort4(string value, IResolver resolver)
      {
        if (value != null && ushort4.TryParse(value, out ushort4 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(ushort4), value);
      }

      /// <summary>Parses a string to an int value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static int ToInt(string value, IResolver resolver)
      {
        value = resolver?.Resolve(value) ?? value;
        if (value != null && int.TryParse(value, out int ret))
          return ret;
        throw new RuleConversionException(typeof(int), value);
      }

      /// <summary>Parses a string to an int2 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static int2 ToInt2(string value, IResolver resolver)
      {
        if (value != null && int2.TryParse(value, out int2 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(int2), value);
      }

      /// <summary>Parses a string to an int3 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static int3 ToInt3(string value, IResolver resolver)
      {
        if (value != null && int3.TryParse(value, out int3 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(int3), value);
      }

      /// <summary>Parses a string to an int4 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static int4 ToInt4(string value, IResolver resolver)
      {
        if (value != null && int4.TryParse(value, out int4 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(int4), value);
      }

      /// <summary>Parses a string to an intRect value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static intRect ToIntRect(string value, IResolver resolver)
      {
        if (value != null && intRect.TryParse(value, out intRect ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(intRect), value);
      }

      /// <summary>Parses a string to an uint value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static uint ToUInt(string value, IResolver resolver)
      {
        value = resolver?.Resolve(value) ?? value;
        if (value != null && uint.TryParse(value, out uint ret))
          return ret;
        throw new RuleConversionException(typeof(uint), value);
      }

      /// <summary>Parses a string to an uint2 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static uint2 ToUInt2(string value, IResolver resolver)
      {
        if (value != null && uint2.TryParse(value, out uint2 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(uint2), value);
      }

      /// <summary>Parses a string to an uint3 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static uint3 ToUInt3(string value, IResolver resolver)
      {
        if (value != null && uint3.TryParse(value, out uint3 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(uint3), value);
      }

      /// <summary>Parses a string to an uint4 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static uint4 ToUInt4(string value, IResolver resolver)
      {
        if (value != null && uint4.TryParse(value, out uint4 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(uint4), value);
      }

      /// <summary>Parses a string to a long value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static long ToLong(string value, IResolver resolver)
      {
        value = resolver?.Resolve(value) ?? value;
        if (value != null && long.TryParse(value, out long ret))
          return ret;
        throw new RuleConversionException(typeof(long), value);
      }

      /// <summary>Parses a string to an ulong value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static ulong ToULong(string value, IResolver resolver)
      {
        value = resolver?.Resolve(value) ?? value;
        if (value != null && ulong.TryParse(value, out ulong ret))
          return ret;
        throw new RuleConversionException(typeof(ulong), value);
      }

      /// <summary>Parses a string to a float value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static float ToFloat(string value, IResolver resolver)
      {
        value = resolver?.Resolve(value) ?? value;
        if (value != null && float.TryParse(value, out float ret))
          return ret;
        throw new RuleConversionException(typeof(float), value);
      }

      /// <summary>Parses a string to a float2 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static float2 ToFloat2(string value, IResolver resolver)
      {
        if (value != null && float2.TryParse(value, out float2 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(float2), value);
      }

      /// <summary>Parses a string to a float3 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static float3 ToFloat3(string value, IResolver resolver)
      {
        if (value != null && float3.TryParse(value, out float3 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(float3), value);
      }

      /// <summary>Parses a string to a float4 value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static float4 ToFloat4(string value, IResolver resolver)
      {
        if (value != null && float4.TryParse(value, out float4 ret, resolver, default))
          return ret;
        throw new RuleConversionException(typeof(float4), value);
      }

      /// <summary>Parses a string to a double value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static double ToDouble(string value, IResolver resolver)
      {
        value = resolver?.Resolve(value) ?? value;
        if (value != null && double.TryParse(value, out double ret))
          return ret;
        throw new RuleConversionException(typeof(double), value);
      }

      /// <summary>Parses a string to a nullable value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static T ToNullable<T>(string value, IResolver resolver)
      {
        if (string.IsNullOrWhiteSpace(value))
          return default;

        Type et = Nullable.GetUnderlyingType(typeof(T));
        //Func<string, IResolver, T> func = (Func<string, IResolver, T>)_fromStr.Get(et); // doesnot work since we dont T is a nullable type, and we got the base type
        Delegate func = _fromStr.Get(et);
        return (T)func.DynamicInvoke(value, resolver);
      }

      /// <summary>Parses a string to a nullable value</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static T ToNullableEnum<T>(string value, IResolver resolver)
      {
        value = resolver?.Resolve(value) ?? value;
        if (string.IsNullOrWhiteSpace(value))
          return default;

        string s = value.Replace("|", ",");
        Type et = Nullable.GetUnderlyingType(typeof(T));
        return (T)Enum.Parse(et, s);
      }

      // cache
      private static object _lock = new object();
      private static Type[] _type1 = new Type[1];
      private static Dictionary<Type, Delegate> _delToArrayAsElement = new Dictionary<Type, Delegate>();

      /// <summary>Parses a string to an array of values</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static T ToArray<T>(string value, IResolver resolver)
      {
        Type t = typeof(T);
        lock (_lock)
        {
          _type1[0] = typeof(T).GetElementType();
          if (!_delToArrayAsElement.ContainsKey(t))
          {
            MethodInfo method = typeof(Rules).GetMethod(nameof(ToArrayAsElement), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(_type1);
            _delToArrayAsElement.Add(t, Delegate.CreateDelegate(typeof(Func<string, IResolver, T>), method));
          }
          Func<string, IResolver, T> func = (Func<string, IResolver, T>)_delToArrayAsElement[t];
          return func.Invoke(value, resolver);
        }
      }

      /// <summary>Parses a string to an array of values</summary>
      /// <param name="value">The string to be parsed</param>
      /// <param name="resolver">A string resolver function</param>
      public static TElement[] ToArrayAsElement<TElement>(string value, IResolver resolver)
      {
        Type et = typeof(TElement);
        TElement[] array;
        if (string.IsNullOrWhiteSpace(value))
        {
          array = Array<TElement>.Empty;
        }
        else
        {
          int n = _tokens.Get(et);
          int tokencount = 1;
          for (int i = 0; i < value.Length; i++)
          {
            if (value[i] == ArrayConstants.Comma[0])
            {
              tokencount++;
            }
          }
          tokencount /= n;
          var tokens = value.SplitBy(ArrayConstants.Comma[0], n);

          array = new TElement[tokencount];
          int index = 0;
          Func<string, IResolver, TElement> func = (Func<string, IResolver, TElement>)_fromStr.Get(et);
          foreach (string t in tokens)
          {
            array[index++] = func.Invoke(t.Trim(), resolver);
          }
        }
        return array;
      }

      /// <summary>Parses a string to a enumerated value</summary>
      /// <param name="value">The string to be parsed</param>
      public static T ToEnum<T>(string value)
      {
        string s = value.Replace("|", ",");
        try { return (T)Enum.Parse(typeof(T), s); }
        catch { throw new RuleConversionException(typeof(T), value); }
      }

      /// <summary>Parses a string to an array of enumerated values</summary>
      /// <param name="value">The string to be parsed</param>
      public static T ToEnumArray<T>(string value)
      {
        if (string.IsNullOrWhiteSpace(value))
          throw new RuleConversionException(typeof(T), value);

        string s = value.Replace("|", ",");
        string[] tokens = s.Split(ArrayConstants.Comma);

        Type et = typeof(T).GetElementType();
        Array array = Array.CreateInstance(et, tokens.Length);

        for (int i = 0; i < tokens.Length; i++)
        {
          array.SetValue(Enum.Parse(et, tokens[i].Trim()), i);
        }
        return (T)(object)array;
      }

      /// <summary>Converts a value to a string</summary>
      /// <param name="value">The value to be converted</param>
      public static string ToStrGeneric<T>(T value)
      {
        return value.ToString();
      }

      // cache
      private static object _lockb = new object();
      private static Type[] _type1b = new Type[1];
      private static Dictionary<Type, Delegate> _delArrayToStrByElement = new Dictionary<Type, Delegate>();

      /// <summary>Converts an array of values to a string</summary>
      /// <param name="list">The value to be converted</param>
      public static string ArrayToStr<T>(T list)
      {
        Type t = typeof(T);
        lock (_lockb)
        {
          _type1b[0] = typeof(T).GetElementType();
          if (!_delArrayToStrByElement.ContainsKey(t))
          {
            MethodInfo method = typeof(Rules).GetMethod(nameof(ArrayToStrByElement), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(_type1b);
            _delArrayToStrByElement.Add(t, Delegate.CreateDelegate(typeof(Func<T, string>), method));
          }
          Func<T, string> func = (Func<T, string>)_delArrayToStrByElement[t];
          return func.Invoke(list);
        }
      }

      /// <summary>Converts an array of values to a string</summary>
      /// <param name="list">The value to be converted</param>
      public static string ArrayToStrByElement<TElement>(TElement[] list)
      {
        string[] str = new string[list.Length];
        for (int i = 0; i < list.Length; i++)
        {
          Func<TElement, string> func = (Func<TElement, string>)_toStr.Get(typeof(TElement));
          str[i] = func.Invoke(list[i]);
        }

        return string.Join(ArrayConstants.Comma[0].ToString(), str);
      }

      /// <summary>Converts an enumerated value to a string</summary>
      /// <param name="value">The value to be converted</param>
      public static string EnumToStr<T>(T value) 
      {
        return ToStringCache<T>.Get(value).Replace(", ", "|");
      }

      /// <summary>Converts an array of enumerated values to a string</summary>
      /// <param name="list">The value to be converted</param>
      public static string EnumArrayToStr<T>(T list)
      {
        Array array = (Array)(object)list;
        string[] str = new string[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
          str[i] = array.GetValue(i).ToString().Replace(", ", "|");
        }

        return string.Join(ToStringCache<char>.Get(ArrayConstants.Comma[0]), str);
      }

      /// <summary>Converts a vectorized value to a string</summary>
      /// <param name="value">The value to be converted</param>
      public static string VecNToStr<T>(T value)
      {
        return ToStringCache<T>.Get(value).Trim(ArrayConstants.Braces);
      }
    }
  }
}
