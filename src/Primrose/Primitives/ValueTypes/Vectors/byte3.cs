using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A byte3 triple value</summary>
  public struct byte3
  {
    /// <summary>The x or [0] value</summary>
    public byte x;

    /// <summary>The y or [1] value</summary>
    public byte y;

    /// <summary>The z or [2] value</summary>
    public byte z;

    /// <summary>
    /// Creates a byte3 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public byte3(byte x, byte y, byte z) { this.x = x; this.y = y; this.z = z; }

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
          default:
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of byte3".F(i));
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
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of byte3".F(i));
        }
      }
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{{{0},{1},{2}}}".F(x, y, z); }

    /// <summary>Creates a byte[] array from this value</summary>
    /// <returns>An array of length 3 with identical indexed values</returns>
    public byte[] ToArray() { return new byte[] { x, y, z }; }

    /// <summary>Creates a byte3 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A byte3 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="InvalidOperationException">Only an array of length 3 can be converted to a byte3</exception>
    public static byte3 FromArray(byte[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 3)
        throw new InvalidOperationException("Attempted assignment of an array of length {0} to a byte3".F(array.Length));
      else
        return new byte3(array[0], array[1], array[2]);
    }

    /// <summary>Parses a byte3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A byte3 value</returns>
    public static byte3 Parse(string s) { return FromArray(Parser.Parse<byte[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a byte3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A byte3 value, or the default value if the parsing fails</returns>
    public static byte3 Parse(string s, byte3 defaultValue)
    {
      byte[] list = Parser.Parse(s, new byte[0]);
      if (list.Length >= 3)
        return new byte3(list[0], list[1], list[2]);
      else if (list.Length == 2)
        return new byte3(list[0], list[1], defaultValue[2]);
      else if (list.Length == 1)
        return new byte3(list[0], defaultValue[1], defaultValue[2]);

      return defaultValue;
    }

    /// <summary>Parses a byte3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out byte3 result) { result = default(byte3); try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a byte3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, byte3 defaultValue, out byte3 result) { result = defaultValue; try { result = Parse(s, defaultValue); return true; } catch { return false; } }
  }
}
