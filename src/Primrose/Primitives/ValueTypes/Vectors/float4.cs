using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A float4 quad value</summary>
  public struct float4 : IEquatable<float4>
  {
    /// <summary>The x or [0] value</summary>
    public float x;

    /// <summary>The y or [1] value</summary>
    public float y;

    /// <summary>The z or [2] value</summary>
    public float z;

    /// <summary>The w or [3] value</summary>
    public float w;

    /// <summary>
    /// Creates a float4 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="w"></param>
    public float4(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }

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

    /// <summary>Creates a float[] array from this value</summary>
    /// <returns>An array of length 4 with identical indexed values</returns>
    public float[] ToArray() { return new float[] { x, y, z, w }; }

    /// <summary>Creates a float4 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A float4 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 4 can be converted to a float4</exception>
    public static float4 FromArray(float[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 4)
        throw new ArrayMismatchException(array.Length, typeof(float4));
      else
        return new float4(array[0], array[1], array[2], array[3]);
    }

    /// <summary>Creates a float4 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A float4 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 4 can be converted to a float4</exception>
    public static float4 FromArray(int[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 4)
        throw new ArrayMismatchException(array.Length, typeof(float4));
      else
        return new float4(array[0], array[1], array[2], array[3]);
    }

    /// <summary>Parses a float4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A float4 value</returns>
    public static float4 Parse(string s) { return FromArray(Parser.Parse<float[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a float4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A float4 value, or the default value if the parsing fails</returns>
    public static float4 Parse(string s, IResolver resolver, float4 defaultValue)
    {
      float[] list = Parser.Parse(s.Trim('{', '}'), resolver, new float[0]);
      float4 value = defaultValue;
      for (int i = 0; i < list.Length; i++)
        value[i] = list[i];

      return value;
    }

    /// <summary>Parses a float4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out float4 result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a float4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out float4 result, IResolver resolver = null, float4 defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }


    /// <summary>Performs a memberwise negation of a float4 value</summary>
    /// <param name="a"></param><returns></returns>
    public static float4 operator -(float4 a)
    {
      return new float4(-a.x, -a.y, -a.z, -a.w);
    }

    /// <summary>Performs an addition operation between two float4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float4 operator +(float4 a, float4 b)
    {
      return new float4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    }

    /// <summary>Performs a subtraction operation between two float4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float4 operator -(float4 a, float4 b)
    {
      return new float4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    }

    /// <summary>Performs a memberwise multiplication operation between two float4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float4 operator *(float4 a, float4 b)
    {
      return new float4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    }

    /// <summary>Performs a memberwise division between two float4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float4 operator /(float4 a, float4 b)
    {
      return new float4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    }

    /// <summary>Performs a multiplication operation between a float4 value and a float multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float4 operator *(float m, float4 a)
    {
      return new float4(a.x * m, a.y * m, a.z * m, a.w * m);
    }

    /// <summary>Performs a multiplication operation between a float4 value and a float multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float4 operator *(float4 a, float m)
    {
      return new float4(a.x * m, a.y * m, a.z * m, a.w * m);
    }

    /// <summary>Performs a division operation between a float4 value and a float divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float4 operator /(float4 a, float m)
    {
      return new float4(a.x / m, a.y / m, a.z / m, a.w / m);
    }

    /// <summary>Performs a modulus operation between a float4 value and a float divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float4 operator %(float4 a, float m)
    {
      return new float4(a.x % m, a.y % m, a.z % m, a.w % m);
    }

    /// <summary>Returns the absolute difference between two float4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float Diff(float4 a, float4 b)
    {
      return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y) + Math.Abs(a.z - b.z) + Math.Abs(a.w - b.w);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is float4 fobj && x == fobj.x && y == fobj.y && z == fobj.z && w == fobj.w;
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(float4 other)
    {
      return x == other.x && y == other.y && z == other.z && w == other.w;
    }

    /// <summary>Casts a float4 to int4</summary>
    /// <param name="o">The object to cast</param>
    public static explicit operator int4(float4 o)
    {
      return new int4((int)o.x, (int)o.y, (int)o.z, (int)o.w);
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

    /// <summary>Determines if two float4 values are equal</summary>
    public static bool operator ==(float4 a, float4 b)
    {
      return a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
    }

    /// <summary>Determines if two float4 values are not equal</summary>
    public static bool operator !=(float4 a, float4 b)
    {
      return a.x != b.x || a.y != b.y || a.z != b.z || a.w != b.w;
    }

    /// <summary>Returns a float4 value with all elements set to their default value</summary>
    public static float4 Empty { get { return new float4(); } }

    /// <summary>Creates an array from this value</summary>
    public static explicit operator Array(float4 a) { return a.ToArray(); }

    /// <summary>Creates a float[] array from this value</summary>
    public static explicit operator float[](float4 a) { return a.ToArray(); }

    /// <summary>Creates a float4 value from this array</summary>
    public static implicit operator float4(float[] a) { return FromArray(a); }

    /// <summary>Creates a float4 value from this array</summary>
    public static implicit operator float4(int[] a) { return FromArray(a); }

    /// <summary>Converts a int4 value to a float4 value</summary>
    public static implicit operator float4(int4 a) { return new float4(a.x, a.y, a.z, a.w); }
  }
}
