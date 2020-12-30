using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A float2 pair value</summary>
  public struct float2 : IEquatable<float2>
  {
    /// <summary>The x or [0] value</summary>
    public float x;

    /// <summary>The y or [1] value</summary>
    public float y;

    /// <summary>
    /// Creates a float2 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public float2(float x, float y) { this.x = x; this.y = y; }

    /// <summary>The value indexer</summary>
    /// <exception cref="IndexOutOfRangeException">The array is accessed with an invalid index</exception>
    public float this[int i]
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

    /// <summary>Creates a float[] array from this value</summary>
    /// <returns>An array of length 2 with identical indexed values</returns>
    public float[] ToArray() { return new float[] { x, y }; }

    /// <summary>Creates a float2 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A float2 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 2 can be converted to a float2</exception>
    public static float2 FromArray(float[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 2)
        throw new ArrayMismatchException(array.Length, typeof(float2));
      else
        return new float2(array[0], array[1]);
    }

    /// <summary>Creates a float2 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A float2 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 2 can be converted to a float2</exception>
    public static float2 FromArray(int[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 2)
        throw new ArrayMismatchException(array.Length, typeof(float2));
      else
        return new float2(array[0], array[1]);
    }

    /// <summary>Parses a float2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A float2 value</returns>
    public static float2 Parse(string s) { return FromArray(Parser.Parse<float[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a float2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A float2 value, or the default value if the parsing fails</returns>
    public static float2 Parse(string s, IResolver resolver, float2 defaultValue)
    {
      float[] list = Parser.Parse(s.Trim('{', '}'), resolver, new float[0]);
      float2 value = defaultValue;
      for (int i = 0; i < list.Length; i++)
        value[i] = list[i];

      return value;
    }

    /// <summary>Parses a float2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out float2 result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a float2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out float2 result, IResolver resolver = null, float2 defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }


    /// <summary>Performs a memberwise negation of a float2 value</summary>
    /// <param name="a"></param><returns></returns>
    public static float2 operator -(float2 a)
    {
      return new float2(-a.x, -a.y);
    }

    /// <summary>Performs an addition operation between two float2 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float2 operator +(float2 a, float2 b)
    {
      return new float2(a.x + b.x, a.y + b.y);
    }

    /// <summary>Performs a subtraction operation between two float2 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float2 operator -(float2 a, float2 b)
    {
      return new float2(a.x - b.x, a.y - b.y);
    }

    /// <summary>Performs a memberwise multiplication operation between two float2 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float2 operator *(float2 a, float2 b)
    {
      return new float2(a.x * b.x, a.y * b.y);
    }

    /// <summary>Performs a memberwise division between two float2 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float2 operator /(float2 a, float2 b)
    {
      return new float2(a.x / b.x, a.y / b.y);
    }

    /// <summary>Performs a multiplication operation between a float2 value and a float multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float2 operator *(float2 a, float m)
    {
      return new float2(a.x * m, a.y * m);
    }

    /// <summary>Performs a division operation between a float2 value and a float divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float2 operator /(float2 a, float m)
    {
      return new float2(a.x / m, a.y / m);
    }

    /// <summary>Performs a modulus operation between a float2 value and a float divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float2 operator %(float2 a, float m)
    {
      return new float2(a.x % m, a.y % m);
    }

    /// <summary>Returns the absolute difference between two float2 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float Diff(float2 a, float2 b)
    {
      return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is float2 fobj && x == fobj.x && y == fobj.y;
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(float2 other)
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

    /// <summary>Determines if two float2 values are equal</summary>
    public static bool operator ==(float2 a, float2 b)
    {
      return a.x == b.x && a.y == b.y;
    }

    /// <summary>Determines if two float2 values are not equal</summary>
    public static bool operator !=(float2 a, float2 b)
    {
      return a.x != b.x || a.y != b.y;
    }

    /// <summary>Returns a float2 value with all elements set to their default value</summary>
    public static float2 Empty { get { return new float2(); } }
  }
}
