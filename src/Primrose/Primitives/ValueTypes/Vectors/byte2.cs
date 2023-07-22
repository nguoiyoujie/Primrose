using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A byte2 pair value</summary>
  public struct byte2
  {
    /// <summary>The x or [0] value</summary>
    public byte x;

    /// <summary>The y or [1] value</summary>
    public byte y;

    /// <summary>
    /// Creates a byte2 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public byte2(byte x, byte y) { this.x = x; this.y = y; }

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
          default:
            throw new IndexOutOfRangeException(Resource.Strings.Error_InvalidIndex.F(i, GetType()));
        }
      }
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{{{0},{1}}}".F(x, y); }

    /// <summary>Creates a byte[] array from this value</summary>
    /// <returns>An array of length 2 with identical indexed values</returns>
    public byte[] ToArray() { return new byte[] { x, y }; }

    /// <summary>Creates a byte2 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A byte2 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 2 can be converted to a byte2</exception>
    public static byte2 FromArray(byte[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 2)
        throw new ArrayMismatchException(array.Length, typeof(byte2));
      else
        return new byte2(array[0], array[1]);
    }

    /// <summary>Parses a byte2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A byte2 value</returns>
    public static byte2 Parse(string s) { return FromArray(Parser.Parse<byte[]>(s.Trim(ArrayConstants.Braces))); }

    /// <summary>Parses a byte2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A byte2 value, or the default value if the parsing fails</returns>
    public static byte2 Parse(string s, IResolver resolver, byte2 defaultValue)
    {
      byte[] list = Parser.Parse(s.Trim(ArrayConstants.Braces), resolver, Array<byte>.Empty);
      byte2 value = defaultValue;
      if (list.Length != 0)
      {
        for (int i = 0; i < list.Length; i++)
          value[i] = list[i];
        // fill excluded indices with the same value as the last
        for (int i = list.Length; i < 2; i++)
          value[i] = list[list.Length - 1];
      }
      return value;
    }

    /// <summary>Parses a byte2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out byte2 result) { result = default; try { result = Parse(s); return true; } catch {  return false; } }

    /// <summary>Parses a byte2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out byte2 result, IResolver resolver, byte2 defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is byte2 fobj && x == fobj.x && y == fobj.y;
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(byte2 other)
    {
      return x == other.x && y == other.y;
    }

    /// <summary>Generates the hash code for this object</summary>
    public override int GetHashCode()
    {
      int hashCode = 1502939027;
      hashCode = hashCode * -1521134295 + x.GetHashCode();
      hashCode = hashCode * -1521134295 + y.GetHashCode();
      return hashCode;
    }

    /// <summary>Determines if two byte2 values are equal</summary>
    public static bool operator ==(byte2 a, byte2 b)
    {
      return a.x == b.x && a.y == b.y;
    }

    /// <summary>Determines if two byte2 values are not equal</summary>
    public static bool operator !=(byte2 a, byte2 b)
    {
      return a.x != b.x || a.y != b.y;
    }

    /// <summary>Returns a byte2 value with all elements set to their default value</summary>
    public static byte2 Empty { get { return new byte2(); } }

    /// <summary>Creates a byte[] array from this value</summary>
    public static explicit operator byte[](byte2 a) { return a.ToArray(); }

    /// <summary>Creates a byte2 value from this array</summary>
    public static explicit operator byte2(byte[] a) { return FromArray(a); }
  }
}
