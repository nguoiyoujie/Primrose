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
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of byte2".F(i));
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
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of byte2".F(i));
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
    /// <exception cref="InvalidOperationException">Only an array of length 2 can be converted to a byte2</exception>
    public static byte2 FromArray(byte[] array)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      else if (array.Length != 2)
        throw new InvalidOperationException("Attempted assignment of an array of length {0} to a byte2".F(array.Length));
      else
        return new byte2(array[0], array[1]);
    }

    /// <summary>Parses a byte2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A byte2 value</returns>
    public static byte2 Parse(string s) { return FromArray(Parser.Parse<byte[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a byte2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A byte2 value, or the default value if the parsing fails</returns>
    public static byte2 Parse(string s, byte2 defaultValue)
    {
      byte[] list = Parser.Parse(s, new byte[0]);
      if (list.Length >= 2)
        return new byte2(list[0], list[1]);
      else if (list.Length == 1)
        return new byte2(list[0], defaultValue[1]);

      return defaultValue;
    }

    /// <summary>Parses a byte2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out byte2 result) { result = default(byte2); try { result = Parse(s); return true; } catch {  return false; } }

    /// <summary>Parses a byte2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, byte2 defaultValue, out byte2 result) { result = defaultValue; try { result = Parse(s, defaultValue); return true; } catch { return false; } }
  }
}
