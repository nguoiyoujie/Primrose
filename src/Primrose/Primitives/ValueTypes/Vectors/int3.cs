using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A int3 triple value</summary>
  public struct int3
  {
    /// <summary>The x or [0] value</summary>
    public int x;

    /// <summary>The y or [1] value</summary>
    public int y;

    /// <summary>The z or [2] value</summary>
    public int z;

    /// <summary>
    /// Creates a int3 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public int3(int x, int y, int z) { this.x = x; this.y = y; this.z = z; }

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
          default:
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of int2".F(i));
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
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of int2".F(i));
        }
      }
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{{{0},{1},{2}}}".F(x, y, z); }

    /// <summary>Creates a int[] array from this value</summary>
    /// <returns>An array of length 3 with identical indexed values</returns>
    public int[] ToArray() { return new int[] { x, y, z }; }

    /// <summary>Creates a int3 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A int3 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 3 can be converted to a int3</exception>
    public static int3 FromArray(int[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 3)
        throw new ArrayMismatchException(array.Length, typeof(int3));
      else
        return new int3(array[0], array[1], array[2]);
    }

    /// <summary>Parses a int3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A int3 value</returns>
    public static int3 Parse(string s) { return FromArray(Parser.Parse<int[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a int3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A int3 value, or the default value if the parsing fails</returns>
    public static int3 Parse(string s, int3 defaultValue)
    {
      int[] list = Parser.Parse(s, new int[0]);
      if (list.Length >= 3)
        return new int3(list[0], list[1], list[2]);
      else if (list.Length == 2)
        return new int3(list[0], list[1], defaultValue[2]);
      else if (list.Length == 1)
        return new int3(list[0], defaultValue[1], defaultValue[2]);

      return defaultValue;
    }

    /// <summary>Parses a int3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out int3 result) { result = default(int3); try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a int3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, int3 defaultValue, out int3 result) { result = defaultValue; try { result = Parse(s, defaultValue); return true; } catch { return false; } }

    /// <summary>Performs a memberwise negation of a int3 value</summary>
    /// <param name="a"></param><returns></returns>
    public static int3 operator -(int3 a)
    {
      return new int3(-a.x, -a.y, -a.z);
    }

    /// <summary>Performs an addition operation between two int3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static int3 operator +(int3 a, int3 b)
    {
      return new int3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    /// <summary>Performs a subtraction operation between two int3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static int3 operator -(int3 a, int3 b)
    {
      return new int3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    /// <summary>Performs a memberwise multiplication operation between two int3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static int3 operator *(int3 a, int3 b)
    {
      return new int3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    /// <summary>Performs a memberwise division between two int3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static int3 operator /(int3 a, int3 b)
    {
      return new int3(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    /// <summary>Performs a multiplication operation between a int3 value and a int multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static int3 operator *(int3 a, int m)
    {
      return new int3(a.x * m, a.y * m, a.z * m);
    }

    /// <summary>Performs a division operation between a int3 value and a int divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static int3 operator /(int3 a, int m)
    {
      return new int3(a.x / m, a.y / m, a.z / m);
    }

    /// <summary>Performs a modulus operation between a int3 value and a int divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static int3 operator %(int3 a, int m)
    {
      return new int3(a.x % m, a.y % m, a.z % m);
    }
  }
}
