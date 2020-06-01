using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A byte4 quad value</summary>
  public struct byte4
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
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of byte4".F(i));
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
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of byte4".F(i));
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
    /// <exception cref="InvalidOperationException">Only an array of length 4 can be converted to a byte4</exception>
    public static byte4 FromArray(byte[] array)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      else if (array.Length != 4)
        throw new InvalidOperationException("Attempted assignment of an array of length {0} to a byte4".F(array.Length));
      else
        return new byte4(array[0], array[1], array[2], array[3]);
    }

    /// <summary>Parses a byte4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A byte4 value</returns>
    public static byte4 Parse(string s) { return FromArray(Parser.Parse<byte[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a byte4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A byte4 value, or the default value if the parsing fails</returns>
    public static byte4 Parse(string s, byte4 defaultValue)
    {
      byte[] list = Parser.Parse(s, new byte[0]);
      if (list.Length >= 4)
        return new byte4(list[0], list[1], list[2], list[3]);
      else if (list.Length == 3)
        return new byte4(list[0], list[1], list[2], defaultValue[3]);
      else if (list.Length == 2)
        return new byte4(list[0], list[1], defaultValue[2], defaultValue[3]);
      else if (list.Length == 1)
        return new byte4(list[0], defaultValue[1], defaultValue[2], defaultValue[3]);

      return defaultValue;
    }

    /// <summary>Parses a byte4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out byte4 result) { result = default(byte4); try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a byte4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, byte4 defaultValue, out byte4 result) { result = defaultValue; try { result = Parse(s, defaultValue); return true; } catch { return false; } }
  }
}
