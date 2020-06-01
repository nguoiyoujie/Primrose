using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A short2 pair value</summary>
  public struct short2
  {
    /// <summary>The x or [0] value</summary>
    public short x;

    /// <summary>The y or [1] value</summary>
    public short y;

    /// <summary>
    /// Creates a short2 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public short2(short x, short y) { this.x = x; this.y = y; }

    /// <summary>The value indexer</summary>
    /// <exception cref="IndexOutOfRangeException">The array is accessed with an invalid index</exception>
    public short this[int i]
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
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of short2".F(i));
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
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of short2".F(i));
        }
      }
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{{{0},{1}}}".F(x, y); }

    /// <summary>Creates a short[] array from this value</summary>
    /// <returns>An array of length 2 with identical indexed values</returns>
    public short[] ToArray() { return new short[] { x, y }; }

    /// <summary>Creates a short2 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A short2 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="InvalidOperationException">Only an array of length 2 can be converted to a short2</exception>
    public static short2 FromArray(short[] array)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      else if (array.Length != 2)
        throw new InvalidOperationException("Attempted assignment of an array of length {0} to a short2".F(array.Length));
      else
        return new short2(array[0], array[1]);
    }

    /// <summary>Parses a short2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A short2 value</returns>
    public static short2 Parse(string s) { return FromArray(Parser.Parse<short[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a short2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A short2 value, or the default value if the parsing fails</returns>
    public static short2 Parse(string s, short2 defaultValue)
    {
      short[] list = Parser.Parse(s, new short[0]);
      if (list.Length >= 2)
        return new short2(list[0], list[1]);
      else if (list.Length == 1)
        return new short2(list[0], defaultValue[1]);

      return defaultValue;
    }

    /// <summary>Parses a short2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out short2 result) { result = default(short2); try { result = Parse(s); return true; } catch {  return false; } }

    /// <summary>Parses a short2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, short2 defaultValue, out short2 result) { result = defaultValue; try { result = Parse(s, defaultValue); return true; } catch { return false; } }
  }
}
