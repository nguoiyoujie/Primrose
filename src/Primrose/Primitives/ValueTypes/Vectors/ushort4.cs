using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A ushort4 quad value</summary>
  public struct ushort4 : IEquatable<ushort4>
  {
    /// <summary>The x or [0] value</summary>
    public ushort x;

    /// <summary>The y or [1] value</summary>
    public ushort y;

    /// <summary>The z or [2] value</summary>
    public ushort z;

    /// <summary>The w or [3] value</summary>
    public ushort w;

    /// <summary>
    /// Creates a ushort4 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="w"></param>
    public ushort4(ushort x, ushort y, ushort z, ushort w) { this.x = x; this.y = y; this.z = z; this.w = w; }

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
    public override string ToString() { return "{" + x.ToString() + "," + y.ToString() + "," + z.ToString() + "," + w.ToString() + "}"; }

    /// <summary>Creates a ushort[] array from this value</summary>
    /// <returns>An array of length 4 with identical indexed values</returns>
    public ushort[] ToArray() { return new ushort[] { x, y, z, w }; }

    /// <summary>Creates a ushort4 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A ushort4 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 4 can be converted to a ushort4</exception>
    public static ushort4 FromArray(ushort[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 4)
        throw new ArrayMismatchException(array.Length, typeof(ushort4));
      else
        return new ushort4(array[0], array[1], array[2], array[3]);
    }

    /// <summary>Parses a ushort4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A ushort4 value</returns>
    public static ushort4 Parse(string s) { return FromArray(Parser.Parse<ushort[]>(s.Trim(ArrayConstants.Braces))); }

    /// <summary>Parses a ushort4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A ushort4 value, or the default value if the parsing fails</returns>
    public static ushort4 Parse(string s, IResolver resolver, ushort4 defaultValue)
    {
      ushort[] list = Parser.Parse(s.Trim(ArrayConstants.Braces), resolver, new ushort[0]);
      ushort4 value = defaultValue;
      if (list.Length != 0)
      {
        for (int i = 0; i < list.Length; i++)
          value[i] = list[i];
        // fill excluded indices with the same value as the last
        for (int i = list.Length; i < 4; i++)
          value[i] = list[list.Length - 1];
      }
      return value;
    }

    /// <summary>Parses a ushort4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out ushort4 result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a ushort4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out ushort4 result, IResolver resolver = null, ushort4 defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is ushort4 fobj && x == fobj.x && y == fobj.y && z == fobj.z && w == fobj.w;
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(ushort4 other)
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

    /// <summary>Determines if two ushort4 values are equal</summary>
    public static bool operator ==(ushort4 a, ushort4 b)
    {
      return a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
    }

    /// <summary>Determines if two ushort4 values are not equal</summary>
    public static bool operator !=(ushort4 a, ushort4 b)
    {
      return a.x != b.x || a.y != b.y || a.z != b.z || a.w != b.w;
    }

    /// <summary>Returns a ushort4 value with all elements set to their default value</summary>
    public static ushort4 Empty { get { return new ushort4(); } }

    /// <summary>Creates a ushort[] array from this value</summary>
    public static explicit operator ushort[](ushort4 a) { return a.ToArray(); }

    /// <summary>Creates a ushort4 value from this array</summary>
    public static explicit operator ushort4(ushort[] a) { return FromArray(a); }
  }
}
