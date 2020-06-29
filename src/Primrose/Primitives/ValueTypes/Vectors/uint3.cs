using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A uint3 triple value</summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Primitive vector struct")]
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
    public override string ToString() { return "{{{0},{1},{2}}}".F(x, y, z); }

    /// <summary>Creates a uint[] array from this value</summary>
    /// <returns>An array of length 3 with identical indexed values</returns>
    public uint[] ToArray() { return new uint[] { x, y, z }; }

    /// <summary>Creates a uint3 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A uint3 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 3 can be converted to a uint3</exception>
    public static uint3 FromArray(uint[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 3)
        throw new ArrayMismatchException(array.Length, typeof(uint3));
      else
        return new uint3(array[0], array[1], array[2]);
    }

    /// <summary>Parses a uint3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A uint3 value</returns>
    public static uint3 Parse(string s) { return FromArray(Parser.Parse<uint[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a uint3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A uint3 value, or the default value if the parsing fails</returns>
    public static uint3 Parse(string s, IResolver resolver, uint3 defaultValue)
    {
      uint[] list = Parser.Parse(s.Trim('{', '}'), resolver, new uint[0]);
      uint3 value = defaultValue;
      for (int i = 0; i < list.Length; i++)
        value[i] = list[i];

      return value;
    }

    /// <summary>Parses a uint3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out uint3 result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a uint3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out uint3 result, IResolver resolver = null, uint3 defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }

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

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is uint3 fobj && x == fobj.x && y == fobj.y && z == fobj.z;
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

    /// <summary>Determines if two uint3 values are equal</summary>
    public static bool operator ==(uint3 a, uint3 b)
    {
      return a.Equals(b);
    }

    /// <summary>Determines if two uint3 values are not equal</summary>
    public static bool operator !=(uint3 a, uint3 b)
    {
      return !a.Equals(b);
    }

    /// <summary>Returns a uint3 value with all elements set to their default value</summary>
    public static uint3 Empty { get { return new uint3(); } }
  }
}
