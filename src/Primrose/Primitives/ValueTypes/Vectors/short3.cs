using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A short3 triple value</summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Primitive vector struct")]
  public struct short3
  {
    /// <summary>The x or [0] value</summary>
    public short x;

    /// <summary>The y or [1] value</summary>
    public short y;

    /// <summary>The z or [2] value</summary>
    public short z;

    /// <summary>
    /// Creates a short3 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public short3(short x, short y, short z) { this.x = x; this.y = y; this.z = z; }

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
          case 2:
            return z;
          default:
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of short3".F(i));
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
            throw new IndexOutOfRangeException("Attempted to access invalid index '{0}' of short3".F(i));
        }
      }
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{{{0},{1},{2}}}".F(x, y, z); }

    /// <summary>Creates a short[] array from this value</summary>
    /// <returns>An array of length 3 with identical indexed values</returns>
    public short[] ToArray() { return new short[] { x, y, z }; }

    /// <summary>Creates a short3 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A short3 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 3 can be converted to a short3</exception>
    public static short3 FromArray(short[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 3)
        throw new ArrayMismatchException(array.Length, typeof(short3));
      else
        return new short3(array[0], array[1], array[2]);
    }

    /// <summary>Parses a short3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A short3 value</returns>
    public static short3 Parse(string s) { return FromArray(Parser.Parse<short[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a short3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A short3 value, or the default value if the parsing fails</returns>
    public static short3 Parse(string s, IResolver resolver, short3 defaultValue)
    {
      short[] list = Parser.Parse(s.Trim('{', '}'), resolver, new short[0]);
      short3 value = defaultValue;
      for (int i = 0; i < list.Length; i++)
        value[i] = list[i];

      return value;
    }

    /// <summary>Parses a short3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out short3 result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a short3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out short3 result, IResolver resolver = null, short3 defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }
  }
}
