using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A ushort2 pair value</summary>
  public struct ushort2 : IEquatable<ushort2>
  {
    /// <summary>The x or [0] value</summary>
    public ushort x;

    /// <summary>The y or [1] value</summary>
    public ushort y;

    /// <summary>
    /// Creates a ushort2 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public ushort2(ushort x, ushort y) { this.x = x; this.y = y; }

    /// <summary>The value indexer</summary>
    /// <exception cref="IndexOutOfRangeException">The array is accessed with an invalid index</exception>
    public ushort this[int i]
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

    /// <summary>Creates a ushort[] array from this value</summary>
    /// <returns>An array of length 2 with identical indexed values</returns>
    public ushort[] ToArray() { return new ushort[] { x, y }; }

    /// <summary>Creates a ushort2 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A ushort2 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 2 can be converted to a ushort2</exception>
    public static ushort2 FromArray(ushort[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 2)
        throw new ArrayMismatchException(array.Length, typeof(ushort2));
      else
        return new ushort2(array[0], array[1]);
    }

    /// <summary>Parses a ushort2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A ushort2 value</returns>
    public static ushort2 Parse(string s) { return FromArray(Parser.Parse<ushort[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a ushort2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A ushort2 value, or the default value if the parsing fails</returns>
    public static ushort2 Parse(string s, IResolver resolver, ushort2 defaultValue)
    {
      ushort[] list = Parser.Parse(s.Trim('{', '}'), resolver, new ushort[0]);
      ushort2 value = defaultValue;
      for (int i = 0; i < list.Length; i++)
        value[i] = list[i];

      return value;
    }

    /// <summary>Parses a ushort2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out ushort2 result) { result = default; try { result = Parse(s); return true; } catch {  return false; } }

    /// <summary>Parses a ushort2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out ushort2 result, IResolver resolver = null, ushort2 defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is ushort2 fobj && x == fobj.x && y == fobj.y;
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(ushort2 other)
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

    /// <summary>Determines if two ushort2 values are equal</summary>
    public static bool operator ==(ushort2 a, ushort2 b)
    {
      return a.x == b.x && a.y == b.y;
    }

    /// <summary>Determines if two ushort2 values are not equal</summary>
    public static bool operator !=(ushort2 a, ushort2 b)
    {
      return a.x != b.x || a.y != b.y;
    }

    /// <summary>Returns a ushort2 value with all elements set to their default value</summary>
    public static ushort2 Empty { get { return new ushort2(); } }
  }
}
