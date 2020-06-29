using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A float3 triple value</summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Primitive vector struct")]
  public struct float3
  {
    /// <summary>The x or [0] value</summary>
    public float x;

    /// <summary>The y or [1] value</summary>
    public float y;

    /// <summary>The z or [2] value</summary>
    public float z;

    /// <summary>
    /// Creates a float3 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public float3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }

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
          default:
            throw new IndexOutOfRangeException(Resource.Strings.Error_InvalidIndex.F(i, GetType()));
        }
      }
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{{{0},{1},{2}}}".F(x, y, z); }

    /// <summary>Creates a float[] array from this value</summary>
    /// <returns>An array of length 3 with identical indexed values</returns>
    public float[] ToArray() { return new float[] { x, y, z }; }

    /// <summary>Creates a float3 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A float3 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 3 can be converted to a float3</exception>
    public static float3 FromArray(float[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 3)
        throw new ArrayMismatchException(array.Length, typeof(float3));
      else
        return new float3(array[0], array[1], array[2]);
    }

    /// <summary>Creates a float3 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A float3 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 3 can be converted to a float3</exception>
    public static float3 FromArray(int[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 3)
        throw new ArrayMismatchException(array.Length, typeof(float3));
      else
        return new float3(array[0], array[1], array[2]);
    }

    /// <summary>Parses a float3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A float3 value</returns>
    public static float3 Parse(string s) { return FromArray(Parser.Parse<float[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a float3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A float3 value, or the default value if the parsing fails</returns>
    public static float3 Parse(string s, IResolver resolver, float3 defaultValue)
    {
      float[] list = Parser.Parse(s.Trim('{', '}'), resolver, new float[0]);
      float3 value = defaultValue;
      for (int i = 0; i < list.Length; i++)
        value[i] = list[i];

      return value;
    }

    /// <summary>Parses a float3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out float3 result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a float4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out float3 result, IResolver resolver, float3 defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }


    /// <summary>Performs a memberwise negation of a float3 value</summary>
    /// <param name="a"></param><returns></returns>
    public static float3 operator -(float3 a)
    {
      return new float3(-a.x, -a.y, -a.z);
    }

    /// <summary>Performs an addition operation between two float3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float3 operator +(float3 a, float3 b)
    {
      return new float3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    /// <summary>Performs a subtraction operation between two float3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float3 operator -(float3 a, float3 b)
    {
      return new float3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    /// <summary>Performs a memberwise multiplication operation between two float3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float3 operator *(float3 a, float3 b)
    {
      return new float3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    /// <summary>Performs a memberwise division between two float3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float3 operator /(float3 a, float3 b)
    {
      return new float3(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    /// <summary>Performs a multiplication operation between a float3 value and a float multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float3 operator *(float3 a, float m)
    {
      return new float3(a.x * m, a.y * m, a.z * m);
    }

    /// <summary>Performs a division operation between a float3 value and a float divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float3 operator /(float3 a, float m)
    {
      return new float3(a.x / m, a.y / m, a.z / m);
    }

    /// <summary>Performs a modulus operation between a float3 value and a float divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float3 operator %(float3 a, float m)
    {
      return new float3(a.x % m, a.y % m, a.z % m);
    }

    /// <summary>Returns the absolute difference between two float3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float Diff(float3 a, float3 b)
    {
      return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y) + Math.Abs(a.z - b.z);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is float3 fobj && x == fobj.x && y == fobj.y && z == fobj.z;
    }

    /// <summary>Generates the hash code for this object</summary>
    public override int GetHashCode()
    {
      int hashCode = 1502939027;
      hashCode = hashCode * -1521134295 + x.GetHashCode();
      hashCode = hashCode * -1521134295 + y.GetHashCode();
      hashCode = hashCode * -1521134295 + z.GetHashCode();
      return hashCode;
    }

    /// <summary>Determines if two float3 values are equal</summary>
    public static bool operator ==(float3 a, float3 b)
    {
      return a.Equals(b);
    }

    /// <summary>Determines if two float3 values are not equal</summary>
    public static bool operator !=(float3 a, float3 b)
    {
      return !a.Equals(b);
    }

    /// <summary>Returns a float3 value with all elements set to their default value</summary>
    public static float3 Empty { get { return new float3(); } }
  }
}
