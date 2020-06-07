using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A uint2 pair value</summary>
  public struct uint2
  {
    /// <summary>The x or [0] value</summary>
    public uint x;

    /// <summary>The y or [1] value</summary>
    public uint y;

    /// <summary>
    /// Creates a uint2 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public uint2(uint x, uint y) { this.x = x; this.y = y; }

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
          default:
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of uint2".F(i));
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
          default:
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of uint2".F(i));
        }
      }
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{{{0},{1}}}".F(x, y); }

    /// <summary>Creates a uint[] array from this value</summary>
    /// <returns>An array of length 2 with identical indexed values</returns>
    public uint[] ToArray() { return new uint[] { x, y }; }

    /// <summary>Creates a uint2 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A uint2 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 2 can be converted to a uint2</exception>
    public static uint2 FromArray(uint[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 2)
        throw new ArrayMismatchException(array.Length, typeof(uint2));
      else
        return new uint2(array[0], array[1]);
    }

    /// <summary>Parses a uint2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A uint2 value</returns>
    public static uint2 Parse(string s) { return FromArray(Parser.Parse<uint[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a uint2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A uint2 value, or the default value if the parsing fails</returns>
    public static uint2 Parse(string s, uint2 defaultValue)
    {
      uint[] list = Parser.Parse(s, new uint[0]);
      if (list.Length >= 2)
        return new uint2(list[0], list[1]);
      else if (list.Length == 1)
        return new uint2(list[0], defaultValue[1]);

      return defaultValue;
    }

    /// <summary>Parses a uint2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out uint2 result) { result = default(uint2); try { result = Parse(s); return true; } catch {  return false; } }

    /// <summary>Parses a uint2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, uint2 defaultValue, out uint2 result) { result = defaultValue; try { result = Parse(s, defaultValue); return true; } catch { return false; } }


    /// <summary>Performs an addition operation between two uint2 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static uint2 operator +(uint2 a, uint2 b)
    {
      return new uint2(a.x + b.x, a.y + b.y);
    }

    /// <summary>Performs a subtraction operation between two uint2 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static uint2 operator -(uint2 a, uint2 b)
    {
      return new uint2(a.x - b.x, a.y - b.y);
    }

    /// <summary>Performs a memberwise multiplication operation between two uint2 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static uint2 operator *(uint2 a, uint2 b)
    {
      return new uint2(a.x * b.x, a.y * b.y);
    }

    /// <summary>Performs a memberwise division between two uint2 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static uint2 operator /(uint2 a, uint2 b)
    {
      return new uint2(a.x / b.x, a.y / b.y);
    }

    /// <summary>Performs a multiplication operation between a uint2 value and a uint multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static uint2 operator *(uint2 a, uint m)
    {
      return new uint2(a.x * m, a.y * m);
    }

    /// <summary>Performs a division operation between a uint2 value and a uint divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static uint2 operator /(uint2 a, uint m)
    {
      return new uint2(a.x / m, a.y / m);
    }

    /// <summary>Performs a modulus operation between a uint2 value and a uint divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static uint2 operator %(uint2 a, uint m)
    {
      return new uint2(a.x % m, a.y % m);
    }
  }
}
