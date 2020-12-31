using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A byte4 quad value</summary>
  public struct byte4 : IEquatable<byte4>
  {
    /// <summary>The x or [0] value</summary>
    public byte x;

    /// <summary>The y or [1] value</summary>
    public byte y;

    /// <summary>The z or [2] value</summary>
    public byte z;

    /// <summary>The w or [3] value</summary>
    public byte w;

    /// <summary>
    /// Creates a byte4 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="w"></param>
    public byte4(byte x, byte y, byte z, byte w) { this.x = x; this.y = y; this.z = z; this.w = w; }

    /// <summary>The value indexer</summary>
    /// <exception cref="IndexOutOfRangeException">The array is accessed with an invalid index</exception>
    public byte this[int i]
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
          case 3:
            w = value;
            break;
          default:
            throw new IndexOutOfRangeException(Resource.Strings.Error_InvalidIndex.F(i, GetType()));
        }
      }
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{{{0},{1},{2},{3}}}".F(x, y, z, w); }

    /// <summary>Creates a byte[] array from this value</summary>
    /// <returns>An array of length 4 with identical indexed values</returns>
    public byte[] ToArray() { return new byte[] { x, y, z, w }; }

    /// <summary>Creates a byte4 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A byte4 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 4 can be converted to a byte4</exception>
    public static byte4 FromArray(byte[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 4)
        throw new ArrayMismatchException(array.Length, typeof(byte4));
      else
        return new byte4(array[0], array[1], array[2], array[3]);
    }

    /// <summary>Parses a byte4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A byte4 value</returns>
    public static byte4 Parse(string s) { return FromArray(Parser.Parse<byte[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a byte4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A byte4 value, or the default value if the parsing fails</returns>
    public static byte4 Parse(string s, IResolver resolver, byte4 defaultValue)
    {
      byte[] list = Parser.Parse(s.Trim('{', '}'), resolver, new byte[0]);
      byte4 value = defaultValue;
      for (int i = 0; i < list.Length; i++)
        value[i] = list[i];

      return value;
    }

    /// <summary>Parses a byte4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out byte4 result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a byte4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out byte4 result, IResolver resolver, byte4 defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is byte4 fobj && x == fobj.x && y == fobj.y && z == fobj.z && w == fobj.w;
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(byte4 other)
    {
      return x == other.x && y == other.y && z == other.z && w == other.w;
    }

    /// <summary>Generates the hash code for this object</summary>
    public override int GetHashCode()
    {
      int hashCode = 1502939027;
      hashCode = hashCode * -1521134295 + x.GetHashCode();
      hashCode = hashCode * -1521134295 + y.GetHashCode();
      hashCode = hashCode * -1521134295 + z.GetHashCode();
      hashCode = hashCode * -1521134295 + w.GetHashCode();
      return hashCode;
    }

    /// <summary>Determines if two byte4 values are equal</summary>
    public static bool operator ==(byte4 a, byte4 b)
    {
      return a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
    }

    /// <summary>Determines if two byte4 values are not equal</summary>
    public static bool operator !=(byte4 a, byte4 b)
    {
      return a.x != b.x || a.y != b.y || a.z != b.z || a.w != b.w;
    }

    /// <summary>Returns a byte4 value with all elements set to their default value</summary>
    public static byte4 Empty { get { return new byte4(); } }

    /// <summary>Creates a byte[] array from this value</summary>
    public static explicit operator byte[](byte4 a) { return a.ToArray(); }

    /// <summary>Creates a byte4 value from this array</summary>
    public static explicit operator byte4(byte[] a) { return FromArray(a); }
  }
}
