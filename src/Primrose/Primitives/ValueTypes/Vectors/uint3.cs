﻿using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A uint3 triple value</summary>
  public struct uint3
  {
    /// <summary>The x or [0] value</summary>
    public uint x;

    /// <summary>The y or [1] value</summary>
    public uint y;

    /// <summary>The z or [2] value</summary>
    public uint z;

    /// <summary>
    /// Creates a uint3 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public uint3(uint x, uint y, uint z) { this.x = x; this.y = y; this.z = z; }

    /// <summary>The value indexer</summary>
    /// <exception cref="IndexOutOfRangeException">The array is accessed with an invalid index</exception>
    public uint this[uint i]
    {
      get
      {
        switch (i)
        {
          case 0:
            return x;
          case 1:
            return y;
          case 2:
            return z;
          default:
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of uint3".F(i));
        }
      }
      set
      {
        switch (i)
        {
          case 0:
            x = value;
            break;
          case 1:
            y = value;
            break;
          case 2:
            z = value;
            break;
          default:
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of uint3".F(i));
        }
      }
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{{{0},{1},{2}}}".F(x, y, z); }

    /// <summary>Creates a uint[] array from this value</summary>
    /// <returns>An array of length 3 with identical indexed values</returns>
    public uint[] ToArray() { return new uint[] { x, y, z }; }

    /// <summary>Creates a uint3 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A uint3 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="InvalidOperationException">Only an array of length 3 can be converted to a uint3</exception>
    public static uint3 FromArray(uint[] array)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      else if (array.Length != 3)
        throw new InvalidOperationException("Attempted assignment of an array of length {0} to a uint3".F(array.Length));
      else
        return new uint3(array[0], array[1], array[2]);
    }

    /// <summary>Parses a uint3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A uint3 value</returns>
    public static uint3 Parse(string s) { return FromArray(Parser.Parse<uint[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a uint3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A uint3 value, or the default value if the parsing fails</returns>
    public static uint3 Parse(string s, uint3 defaultValue)
    {
      uint[] list = Parser.Parse(s, new uint[0]);
      if (list.Length >= 3)
        return new uint3(list[0], list[1], list[2]);
      else if (list.Length == 2)
        return new uint3(list[0], list[1], defaultValue[2]);
      else if (list.Length == 1)
        return new uint3(list[0], defaultValue[1], defaultValue[2]);

      return defaultValue;
    }

    /// <summary>Parses a uint3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out uint3 result) { result = default(uint3); try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a uint3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, uint3 defaultValue, out uint3 result) { result = defaultValue; try { result = Parse(s, defaultValue); return true; } catch { return false; } }

    /// <summary>Performs an addition operation between two uint3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static uint3 operator +(uint3 a, uint3 b)
    {
      return new uint3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    /// <summary>Performs a subtraction operation between two uint3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static uint3 operator -(uint3 a, uint3 b)
    {
      return new uint3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    /// <summary>Performs a memberwise multiplication operation between two uint3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static uint3 operator *(uint3 a, uint3 b)
    {
      return new uint3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    /// <summary>Performs a memberwise division between two uint3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static uint3 operator /(uint3 a, uint3 b)
    {
      return new uint3(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    /// <summary>Performs a multiplication operation between a uint3 value and a uint multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static uint3 operator *(uint3 a, uint m)
    {
      return new uint3(a.x * m, a.y * m, a.z * m);
    }

    /// <summary>Performs a division operation between a uint3 value and a uint divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static uint3 operator /(uint3 a, uint m)
    {
      return new uint3(a.x / m, a.y / m, a.z / m);
    }

    /// <summary>Performs a modulus operation between a uint3 value and a uint divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static uint3 operator %(uint3 a, uint m)
    {
      return new uint3(a.x % m, a.y % m, a.z % m);
    }
  }
}