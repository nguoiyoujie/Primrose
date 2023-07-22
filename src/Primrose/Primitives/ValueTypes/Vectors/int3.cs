using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A int3 triple value</summary>
  public struct int3 : IEquatable<int3>, IComparable<int3>
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
            throw new IndexOutOfRangeException(Resource.Strings.Error_InvalidIndex.F(i, GetType()));
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
            throw new IndexOutOfRangeException(Resource.Strings.Error_InvalidIndex.F(i, GetType()));
        }
      }
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{" + x.ToString() + "," + y.ToString() + "," + z.ToString() + "}"; }

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
    public static int3 Parse(string s) { return FromArray(Parser.Parse<int[]>(s.Trim(ArrayConstants.Braces))); }

    /// <summary>Parses a int3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A int3 value, or the default value if the parsing fails</returns>
    public static int3 Parse(string s, IResolver resolver, int3 defaultValue)
    {
      int[] list = Parser.Parse(s.Trim(ArrayConstants.Braces), resolver, new int[0]);
      int3 value = defaultValue;
      if (list.Length != 0)
      {
        for (int i = 0; i < list.Length; i++)
          value[i] = list[i];
        // fill excluded indices with the same value as the last
        for (int i = list.Length; i < 3; i++)
          value[i] = list[list.Length - 1];
      }
      return value;
    }

    /// <summary>Parses a int3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out int3 result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a int3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out int3 result, IResolver resolver = null, int3 defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }

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

    /// <summary>Performs a multiplication operation between a int2 value and a int multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static int3 operator *(int m, int3 a)
    {
      return a * m;
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

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is int3 fobj && x == fobj.x && y == fobj.y && z == fobj.z;
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(int3 other)
    {
      return x == other.x && y == other.y && z == other.z;
    }

    /// <summary>Generates the hash code for this object</summary>
    public override int GetHashCode()
    {
      int hashCode = 1502939027;
      hashCode = hashCode * -1521134295 + x.GetHashCode();
      hashCode = hashCode * -1521134295 + y.GetHashCode();
      hashCode = hashCode * -1521134295 + z.GetHashCode();
      return hashCode;
    }

    /// <summary>Compares one int3 value to the other</summary>
    public int CompareTo(int3 other)
    {
      int r = z.CompareTo(z);
      if (r == 0) { r = y.CompareTo(other.y); }
      if (r == 0) { r = x.CompareTo(other.x); }
      return r;
    }

    /// <summary>Determines if two int3 values are equal</summary>
    public static bool operator ==(int3 a, int3 b)
    {
      return a.x == b.x && a.y == b.y && a.z == b.z;
    }

    /// <summary>Determines if two int3 values are not equal</summary>
    public static bool operator !=(int3 a, int3 b)
    {
      return a.x != b.x || a.y != b.y || a.z != b.z;
    }

    /// <summary>Returns a int3 value with all elements set to their default value</summary>
    public static int3 Empty { get { return new int3(); } }

    /// <summary>Creates an array from this value</summary>
    public static explicit operator Array(int3 a) { return a.ToArray(); }

    /// <summary>Creates a int[] array from this value</summary>
    public static explicit operator int[](int3 a) { return a.ToArray(); }

    /// <summary>Creates a int3 value from this array</summary>
    public static implicit operator int3(int[] a) { return FromArray(a); }
  }
}
