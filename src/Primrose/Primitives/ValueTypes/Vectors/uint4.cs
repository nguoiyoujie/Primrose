using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A uint4 quad value</summary>
  public struct uint4
  {
    /// <summary>The x or [0] value</summary>
    public uint x;

    /// <summary>The y or [1] value</summary>
    public uint y;

    /// <summary>The z or [2] value</summary>
    public uint z;

    /// <summary>The w or [3] value</summary>
    public uint w;

    /// <summary>
    /// Creates a uint4 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="w"></param>
    public uint4(uint x, uint y, uint z, uint w) { this.x = x; this.y = y; this.z = z; this.w = w; }

    /// <summary>The value indexer</summary>
    /// <exception cref="IndexOutOfRangeException">The array is accessed with an invalid index</exception>
    public uint this[int i]
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
          case 3:
            return w;
          default:
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of uint4".F(i));
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
          case 3:
            w = value;
            break;
          default:
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of uint4".F(i));
        }
      }
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{{{0},{1},{2},{3}}}".F(x, y, z, w); }

    /// <summary>Creates a uint[] array from this value</summary>
    /// <returns>An array of length 4 with identical indexed values</returns>
    public uint[] ToArray() { return new uint[] { x, y, z, w }; }

    /// <summary>Creates a uint4 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A uint4 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 4 can be converted to a uint4</exception>
    public static uint4 FromArray(uint[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 4)
        throw new ArrayMismatchException(array.Length, typeof(uint4));
      else
        return new uint4(array[0], array[1], array[2], array[3]);
    }

    /// <summary>Parses a uint4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A uint4 value</returns>
    public static uint4 Parse(string s) { return FromArray(Parser.Parse<uint[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a uint4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A uint4 value, or the default value if the parsing fails</returns>
    public static uint4 Parse(string s, IResolver resolver, uint4 defaultValue)
    {
      uint[] list = Parser.Parse(s.Trim('{', '}'), resolver, new uint[0]);
      uint4 value = defaultValue;
      for (int i = 0; i < list.Length; i++)
        value[i] = list[i];

      return value;
    }

    /// <summary>Parses a uint4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out uint4 result) { result = default(uint4); try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a uint4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out uint4 result, IResolver resolver = null, uint4 defaultValue  = default(uint4)) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }


    /// <summary>Performs an addition operation between two uint4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static uint4 operator +(uint4 a, uint4 b)
    {
      return new uint4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    }

    /// <summary>Performs a subtraction operation between two uint4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static uint4 operator -(uint4 a, uint4 b)
    {
      return new uint4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    }

    /// <summary>Performs a memberwise multiplication operation between two uint4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static uint4 operator *(uint4 a, uint4 b)
    {
      return new uint4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    }

    /// <summary>Performs a memberwise division between two uint4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static uint4 operator /(uint4 a, uint4 b)
    {
      return new uint4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    }

    /// <summary>Performs a multiplication operation between a uint4 value and a uint multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static uint4 operator *(uint4 a, uint m)
    {
      return new uint4(a.x * m, a.y * m, a.z * m, a.w * m);
    }

    /// <summary>Performs a division operation between a uint4 value and a uint divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static uint4 operator /(uint4 a, uint m)
    {
      return new uint4(a.x / m, a.y / m, a.z / m, a.w / m);
    }

    /// <summary>Performs a modulus operation between a uint4 value and a uint divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static uint4 operator %(uint4 a, uint m)
    {
      return new uint4(a.x % m, a.y % m, a.z % m, a.w % m);
    }
  }
}
