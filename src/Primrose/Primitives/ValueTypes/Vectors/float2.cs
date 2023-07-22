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
    public override string ToString() { return "{" + x.ToString() + "," + y.ToString() + "}"; }

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
    public static float2 Parse(string s) { return FromArray(Parser.Parse<float[]>(s.Trim(ArrayConstants.Braces))); }

    /// <summary>Parses a float2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A float2 value, or the default value if the parsing fails</returns>
    public static float2 Parse(string s, IResolver resolver, float2 defaultValue)
    {
      float[] list = Parser.Parse(s.Trim(ArrayConstants.Braces), resolver, new float[0]);
      float2 value = defaultValue;
      if (list.Length != 0)
      {
        for (int i = 0; i < list.Length; i++)
          value[i] = list[i];
        // fill excluded indices with the same value as the last
        for (int i = list.Length; i < 2; i++)
          value[i] = list[list.Length - 1];
      }
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
    public static float2 operator *(float m, float2 a)
    {
      return new float2(a.x * m, a.y * m);
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
      float d = 1 / m;
      return new float2(a.x * d, a.y * d);
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

    /// <summary>Casts a float2 to int2</summary>
    /// <param name="o">The object to cast</param>
    public static explicit operator int2(float2 o)
    {
      return new int2((int)o.x, (int)o.y);
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

    /// <summary>Creates an array from this value</summary>
    public static explicit operator Array(float2 a) { return a.ToArray(); }

    /// <summary>Creates a float[] array from this value</summary>
    public static explicit operator float[](float2 a) { return a.ToArray(); }

    /// <summary>Creates a float2 value from this array</summary>
    public static implicit operator float2(float[] a) { return FromArray(a); }

    /// <summary>Creates a float2 value from this array</summary>
    public static implicit operator float2(int[] a) { return FromArray(a); }

    /// <summary>Converts a int2 value to a float2 value</summary>
    public static implicit operator float2(int2 a) { return new float2(a.x, a.y); }

    // Extensions for transform operations
    /// <summary>Returns a float2 value with all elements set to 1</summary>
    public static float2 One { get { return new float2(1f, 1f); } }

    /// <summary>Returns a float2 value representing a unit vector in the x direction</summary>
    public static float2 UnitX { get { return new float2(1f, 0f); } }

    /// <summary>Returns a float2 value representing a unit vector in the y direction</summary>
    public static float2 UnitY { get { return new float2(0f, 1f); } }

    /// <summary>Calculates the length, or Euclidean distance, of the vector</summary>
    public float Length { get { return (float)Math.Sqrt(x * x + y * y); } }

    /// <summary>Calculates the length, or Euclidean distance, of the vector squared</summary>
    public float LengthSquared { get { return x * x + y * y; } }

    /// <summary>Calculates the Euclidean distance, between two vectors</summary>
    public static float Distance(float2 v1, float2 v2) { return (v2 - v1).Length; }

    /// <summary>Calculates the Euclidean distance, between two vectors squared</summary>
    public static float DistanceSquared(float2 v1, float2 v2) { return (v2 - v1).LengthSquared; }

    /// <summary>Returns a float2 value of the same direction, normalized to unit length</summary>
    public float2 Normalize() { return (x == 0 && y == 0) ? default : (this / Length); }

    /// <summary>Calculates the dot product between to float2 vectors</summary>
    public static float Dot(float2 v1, float2 v2) { return v1.x * v2.x + v1.y * v2.y; }

    /// <summary>Calculates the dot product between to float2 vectors</summary>
    public float Dot(float2 v) { return x * v.x + y * v.y; }

    /// <summary>Calculates the cross product between to float2 vectors</summary>
    public static float Cross(float2 v1, float2 v2) { return v1.x * v2.y - v1.y * v2.x; }

    /// <summary>Calculates the cross product between to float2 vectors</summary>
    public float Cross(float2 v) { return x * v.y - y * v.x; }

    /// <summary>Swaps two float2 values</summary>
    public static void Swap(ref float2 v1, ref float2 v2)
    {
      float2 temp = v1;
      v1 = v2;
      v2 = temp;
    }

    /// <summary>Swaps two float2 values</summary>
    public void Swap(ref float2 other)
    {
      float2 temp = this;
      this = other;
      other = temp;
    }

    /// <summary>Determines the reflect vector of the given vector and normal</summary>
    public static float2 Reflect(float2 v, float2 normal) { return v - v.Dot(normal) * normal * 2f; }

    /// <summary>Determines the reflect vector of the given vector and normal</summary>
    public float2 Reflect(float2 normal) { return this - Dot(normal) * normal * 2f; }

    /// <summary>
    /// Returns a value linearly interpolated towards a target
    /// </summary>
    /// <param name="value">The starting value</param>
    /// <param name="target">The target value</param>
    /// <param name="frac">The fraction to be interpolated towards the target point</param>
    /// <returns></returns>
    public static float2 Lerp(float2 value, float2 target, float frac)
    {
      return value + (target - value) * frac;
    }

    /// <summary>
    /// Returns a value linearly interpolated towards a target
    /// </summary>
    /// <param name="target">The target value</param>
    /// <param name="frac">The fraction to be interpolated towards the target point</param>
    /// <returns></returns>
    public float2 Lerp(float2 target, float frac)
    {
      return this + (target - this) * frac;
    }

    /// <summary>
    /// Returns a value linearly interpolated towards a target
    /// </summary>
    /// <param name="value">The starting value</param>
    /// <param name="target">The target value</param>
    /// <param name="frac">The fraction to be interpolated towards the target point</param>
    /// <returns></returns>
    public static float2 Lerp(float2 value, float2 target, float2 frac)
    {
      return value + (target - value) * frac;
    }

    /// <summary>
    /// Returns a value linearly interpolated towards a target
    /// </summary>
    /// <param name="target">The target value</param>
    /// <param name="frac">The fraction to be interpolated towards the target point</param>
    /// <returns></returns>
    public float2 Lerp(float2 target, float2 frac)
    {
      return this + (target - this) * frac;
    }

    /// <summary>
    /// Returns a float2 value with each component being the greater of two float2 values
    /// </summary>
    public static float2 Max(float2 v1, float2 v2)
    {
      return new float2(v1.x.Max(v2.x), v1.y.Max(v2.y));
    }

    /// <summary>
    /// Returns a float2 value with each component being the greater of two float2 values
    /// </summary>
    public float2 Max(float2 other)
    {
      return new float2(x.Max(other.x), y.Max(other.y));
    }

    /// <summary>
    /// Returns a float2 value with each component being the lesser of two float2 values
    /// </summary>
    public static float2 Min(float2 v1, float2 v2)
    {
      return new float2(v1.x.Min(v2.x), v1.y.Min(v2.y));
    }

    /// <summary>
    /// Returns a float2 value with each component being the lesser of two float2 values
    /// </summary>
    public float2 Min(float2 other)
    {
      return new float2(x.Min(other.x), y.Min(other.y));
    }

    /// <summary>
    /// Returns a value at most max_delta value closer to a target
    /// </summary>
    /// <param name="value">The starting value</param>
    /// <param name="target">The target value</param>
    /// <param name="max_delta">The max_delta for each dimension</param>
    /// <returns></returns>
    public static float2 Creep(float2 value, float2 target, float max_delta)
    {
      return new float2(value.x.Creep(target.x, max_delta), value.y.Creep(target.y, max_delta));
    }

    /// <summary>
    /// Returns a value at most max_delta value closer to a target
    /// </summary>
    /// <param name="target">The target value</param>
    /// <param name="max_delta">The max_delta for each dimension</param>
    /// <returns></returns>
    public float2 Creep(float2 target, float max_delta)
    {
      return new float2(x.Creep(target.x, max_delta), y.Creep(target.y, max_delta));
    }

    /// <summary>
    /// Returns a value at most max_delta value closer to a target
    /// </summary>
    /// <param name="value">The starting value</param>
    /// <param name="target">The target value</param>
    /// <param name="max_delta">The max_delta per dimension</param>
    /// <returns></returns>
    public static float2 Creep(float2 value, float2 target, float2 max_delta)
    {
      return new float2(value.x.Creep(target.x, max_delta.x), value.y.Creep(target.y, max_delta.y));
    }

    /// <summary>
    /// Returns a value at most max_delta value closer to a target
    /// </summary>
    /// <param name="target">The target value</param>
    /// <param name="max_delta">The max_delta per dimension</param>
    /// <returns></returns>
    public float2 Creep(float2 target, float2 max_delta)
    {
      return new float2(x.Creep(target.x, max_delta.x), y.Creep(target.y, max_delta.y));
    }

    /// <summary>
    /// Returns the result of (value % (max - min)), scaled so that lies between min and max
    /// </summary>
    /// <param name="value">The input value</param>
    /// <param name="min">The minimum value</param>
    /// <param name="max">The maximum value</param>
    /// <returns>(value % (max - min)), scaled so that lies between min and max</returns>
    public static float2 Modulus(float2 value, float2 min, float2 max)
    {
      return new float2(value.x.Modulus(min.x, max.x), value.y.Modulus(min.y, max.y));
    }

    /// <summary>
    /// Returns the result of (value % (max - min)), scaled so that lies between min and max
    /// </summary>
    /// <param name="min">The minimum value</param>
    /// <param name="max">The maximum value</param>
    /// <returns>(value % (max - min)), scaled so that lies between min and max</returns>
    public float2 Modulus(float2 min, float2 max)
    {
      return new float2(x.Modulus(min.x, max.x), y.Modulus(min.y, max.y));
    }
  }
}
