using Primrose.Primitives.Factories;
using System;

namespace Primrose.Primitives.Cache
{
  /// <summary>Helper class to support caching of widely used functions for the Enum class</summary>
  /// <typeparam name="TEnum">The underlying enum class</typeparam>
  public static class Enum<TEnum> where TEnum : struct, Enum
  {
    private static Registry<TEnum, string> _toStr = new Registry<TEnum, string>();
    private static Registry<string, TEnum> _fromStr = new Registry<string, TEnum>();
    private static TEnum[] _values;
    private static string[] _names;

    static Enum()
    {
      _values = (TEnum[])Enum.GetValues(typeof(TEnum));
      _names = Enum.GetNames(typeof(TEnum));
      Init();
    }

    private static void Init()
    {
      foreach (TEnum v in _values)
      {
        string s = GetName(v);
        Parse(s);
      }
    }

    /// <summary>
    /// Retrieves the array of the values of the constants in enumeration <typeparamref name="TEnum"/>.
    /// </summary>
    /// <returns>An array of the values of the constants in <typeparamref name="TEnum"/>.
    /// The elements of the array are sorted by the binary values of the enumeration constants.</returns>
    public static TEnum[] GetValues()
    {
      TEnum[] result = new TEnum[_values.Length];
      Array.Copy(_values, result, _values.Length);
      return result;
    }

    /// <summary>
    /// Retrieves the array of the values of the constants in enumeration <typeparamref name="TEnum"/>.
    /// </summary>
    /// <returns>An array of the values of the constants in <typeparamref name="TEnum"/>.
    /// The elements of the array are in the same order as in case of the result of the <see cref="GetValues">GetValues</see> method.</returns>
    public static string[] GetNames()
    {
      string[] result = new string[_names.Length];
      Array.Copy(_names, result, _names.Length);
      return result;
    }

    /// <summary>
    /// Retrieves the name of the constant in the specified enumeration that has the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The <see langword="enum"/> value whose name is required.</param>
    /// <returns>A string containing the name of the enumerated <paramref name="value"/>, or <see langword="null"/> if no such constant is found.</returns>
    public static string GetName(TEnum value)
    {
      if (!_toStr.Contains(value))
      {
#pragma warning disable HAA0102 // One time call
        string s = value.ToString();
#pragma warning restore HAA0102
        _toStr[value] = s;
        return s;
      }
      return _toStr[value];
    }

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
    /// </summary>
    public static TEnum Parse(string str)
    {
      if (!_fromStr.Contains(str))
      {
        if (Enum.TryParse(str, out TEnum em))
        {
          _fromStr[str] = em;
        }
        return em;
      }
      return _fromStr[str];
    }

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
    /// </summary>
    public static bool TryParse(string str, out TEnum result)
    {
      if (!_fromStr.Contains(str))
      {
        if (Enum.TryParse(str, out result))
        {
          _fromStr[str] = result;
          return true;
        }
        return false;
      }
      else
      {
        result = _fromStr[str];
        return true;
      }
    }

    /// <summary>Clears the cache for the class</summary>
    public static void Clear()
    {
      _toStr.Clear();
      _fromStr.Clear();
      Init();
    }
  }
}
