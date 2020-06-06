using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A int4 quad value</summary>
  public struct int4
  {
    /// <summary>The x or [0] value</summary>
    public int x;

    /// <summary>The y or [1] value</summary>
    public int y;

    /// <summary>The z or [2] value</summary>
    public int z;

    /// <summary>The w or [3] value</summary>
    public int w;

    /// <summary>
    /// Creates a int4 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="w"></param>
    public int4(int x, int y, int z, int w) { this.x = x; this.y = y; this.z = z; this.w = w; }

    /// <summary>The value indexer</summary>
    /// <exception cref="IndexOutOfRangeException">The array is accessed with an invalid index</exception>
    public int this[int i]
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
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of int4".F(i));
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
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of int4".F(i));
        }
      }
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{{{0},{1},{2},{3}}}".F(x, y, z, w); }

    /// <summary>Creates a int[] array from this value</summary>
    /// <returns>An array of length 4 with identical indexed values</returns>
    public int[] ToArray() { return new int[] { x, y, z, w }; }

    /// <summary>Creates a int4 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A int4 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="InvalidOperationException">Only an array of length 4 can be converted to a int4</exception>
    public static int4 FromArray(int[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 4)
        throw new InvalidOperationException("Attempted assignment of an array of length {0} to a int4".F(array.Length));
      else
        return new int4(array[0], array[1], array[2], array[3]);
    }

    /// <summary>Parses a int4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A int4 value</returns>
    public static int4 Parse(string s) { return FromArray(Parser.Parse<int[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a int4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A int4 value, or the default value if the parsing fails</returns>
    public static int4 Parse(string s, int4 defaultValue)
    {
      int[] list = Parser.Parse(s, new int[0]);
      if (list.Length >= 4)
        return new int4(list[0], list[1], list[2], list[3]);
      else if (list.Length == 3)
        return new int4(list[0], list[1], list[2], defaultValue[3]);
      else if (list.Length == 2)
        return new int4(list[0], list[1], defaultValue[2], defaultValue[3]);
      else if (list.Length == 1)
        return new int4(list[0], defaultValue[1], defaultValue[2], defaultValue[3]);

      return defaultValue;
    }

    /// <summary>Parses a int4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out int4 result) { result = default(int4); try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a int4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, int4 defaultValue, out int4 result) { result = defaultValue; try { result = Parse(s, defaultValue); return true; } catch { return false; } }


    /// <summary>Performs a memberwise negation of a int4 value</summary>
    /// <param name="a"></param><returns></returns>
    public static int4 operator -(int4 a)
    {
      return new int4(-a.x, -a.y, -a.z, -a.w);
    }

    /// <summary>Performs an addition operation between two int4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static int4 operator +(int4 a, int4 b)
    {
      return new int4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    }

    /// <summary>Performs a subtraction operation between two int4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static int4 operator -(int4 a, int4 b)
    {
      return new int4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    }

    /// <summary>Performs a memberwise multiplication operation between two int4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static int4 operator *(int4 a, int4 b)
    {
      return new int4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    }

    /// <summary>Performs a memberwise division between two int4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static int4 operator /(int4 a, int4 b)
    {
      return new int4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    }

    /// <summary>Performs a multiplication operation between a int4 value and a int multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static int4 operator *(int4 a, int m)
    {
      return new int4(a.x * m, a.y * m, a.z * m, a.w * m);
    }

    /// <summary>Performs a division operation between a int4 value and a int divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static int4 operator /(int4 a, int m)
    {
      return new int4(a.x / m, a.y / m, a.z / m, a.w / m);
    }

    /// <summary>Performs a modulus operation between a int4 value and a int divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static int4 operator %(int4 a, int m)
    {
      return new int4(a.x % m, a.y % m, a.z % m, a.w % m);
    }
  }
}
