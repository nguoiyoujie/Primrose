using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A float3 triple value</summary>
  public struct float3 : IEquatable<float3>
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
    public override string ToString() { return "{" + x.ToString() + "," + y.ToString() + "," + z.ToString() + "}"; }

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
    public static float3 Parse(string s) { return FromArray(Parser.Parse<float[]>(s.Trim(ArrayConstants.Braces))); }

    /// <summary>Parses a float3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A float3 value, or the default value if the parsing fails</returns>
    public static float3 Parse(string s, IResolver resolver, float3 defaultValue)
    {
      float[] list = Parser.Parse(s.Trim(ArrayConstants.Braces), resolver, new float[0]);
      float3 value = defaultValue;
      if (list.Length != 0)
      {
        for (int i = 0; i < list.Length; i++)
          value[i] = list[i];
        // fill excluded indices with the same value as the last
        for (int i = list.Length; i < 3; i++)
          value[i] = list[list.Length - 1];
      }
      return value;
    }

    /// <summary>Parses a float3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out float3 result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a float3 from a string</summary>
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
    public static float3 operator *(float m, float3 a)
    {
      return new float3(a.x * m, a.y * m, a.z * m);
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
      float d = 1 / m;
      return new float3(a.x * d, a.y * d, a.z * d);
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

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(float3 other)
    {
      return x == other.x && y == other.y && z == other.z;
    }

    /// <summary>Casts a float3 to int3</summary>
    /// <param name="o">The object to cast</param>
    public static explicit operator int3(float3 o)
    {
      return new int3((int)o.x, (int)o.y, (int)o.z);
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
      return a.x == b.x && a.y == b.y && a.z == b.z;
    }

    /// <summary>Determines if two float3 values are not equal</summary>
    public static bool operator !=(float3 a, float3 b)
    {
      return a.x != b.x || a.y != b.y || a.z != b.z;
    }

    /// <summary>Returns a float3 value with all elements set to their default value</summary>
    public static float3 Empty { get { return new float3(); } }

    /// <summary>Creates an array from this value</summary>
    public static explicit operator Array(float3 a) { return a.ToArray(); }

    /// <summary>Creates a float[] array from this value</summary>
    public static explicit operator float[](float3 a) { return a.ToArray(); }

    /// <summary>Creates a float3 value from this array</summary>
    public static implicit operator float3(float[] a) { return FromArray(a); }

    /// <summary>Creates a float3 value from this array</summary>
    public static implicit operator float3(int[] a) { return FromArray(a); }

    /// <summary>Converts a int3 value to a float3 value</summary>
    public static implicit operator float3(int3 a) { return new float3(a.x, a.y, a.z); }

    // Extensions for transform operations
    /// <summary>Returns a float3 value with all elements set to 1</summary>
    public static float3 One { get { return new float3(1f, 1f, 1f); } }

    /// <summary>Returns a float3 value representing a unit vector in the x direction</summary>
    public static float3 UnitX { get { return new float3(1f, 0f, 0f); } }

    /// <summary>Returns a float3 value representing a unit vector in the y direction</summary>
    public static float3 UnitY { get { return new float3(0f, 1f, 0f); } }

    /// <summary>Returns a float3 value representing a unit vector in the y direction</summary>
    public static float3 UnitZ { get { return new float3(0f, 0f, 1f); } }

    /// <summary>Calculates the length, or Euclidean distance, of the vector</summary>
    public float Length { get { return (float)Math.Sqrt(x * x + y * y + z * z); } }

    /// <summary>Calculates the length, or Euclidean distance, of the vector squared</summary>
    public float LengthSquared { get { return x * x + y * y + z * z; } }

    /// <summary>Calculates the Euclidean distance, between two vectors</summary>
    public static float Distance(float3 v1, float3 v2) { return (v2 - v1).Length; }

    /// <summary>Calculates the Euclidean distance, between two vectors squared</summary>
    public static float DistanceSquared(float3 v1, float3 v2) { return (v2 - v1).LengthSquared; }

    /// <summary>Returns a float3 value of the same direction, normalized to unit length</summary>
    public float3 Normalize() { return (x == 0 && y == 0 && z == 0) ? default : (this / Length); }

    /// <summary>Calculates the dot product between to float3 vectors</summary>
    public static float Dot(float3 v1, float3 v2) { return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z; }

    /// <summary>Calculates the dot product between to float3 vectors</summary>
    public float Dot(float3 v) { return x * v.x + y * v.y + z * v.z; }

    /// <summary>Calculates the cross product between to float3 vectors</summary>
    public static float3 Cross(float3 v1, float3 v2) { return new float3(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x); }

    /// <summary>Calculates the cross product between to float3 vectors</summary>
    public float3 Cross(float3 v) { return new float3(y * v.z - z * v.y, z * v.x - x * v.z, x * v.y - y * v.x); }

    /// <summary>Swaps two float3 values</summary>
    public static void Swap(ref float3 v1, ref float3 v2)
    {
      float3 temp = v1;
      v1 = v2;
      v2 = temp;
    }

    /// <summary>Swaps two float3 values</summary>
    public void Swap(ref float3 other)
    {
      float3 temp = this;
      this = other;
      other = temp;
    }

    /// <summary>Determines the reflect vector of the given vector and normal</summary>
    public static float3 Reflect(float3 v, float3 normal) { return v - v.Dot(normal) * normal * 2f; }

    /// <summary>Determines the reflect vector of the given vector and normal</summary>
    public float3 Reflect(float3 normal) { return this - Dot(normal) * normal * 2f; }

    /// <summary>
    /// Returns a value linearly interpolated towards a target
    /// </summary>
    /// <param name="value">The starting value</param>
    /// <param name="target">The target value</param>
    /// <param name="frac">The fraction to be interpolated towards the target point</param>
    /// <returns></returns>
    public static float3 Lerp(float3 value, float3 target, float frac)
    {
      return value + (target - value) * frac;
    }

    /// <summary>
    /// Returns a value linearly interpolated towards a target
    /// </summary>
    /// <param name="target">The target value</param>
    /// <param name="frac">The fraction to be interpolated towards the target point</param>
    /// <returns></returns>
    public float3 Lerp(float3 target, float frac)
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
    public static float3 Lerp(float3 value, float3 target, float3 frac)
    {
      return value + (target - value) * frac;
    }

    /// <summary>
    /// Returns a value linearly interpolated towards a target
    /// </summary>
    /// <param name="target">The target value</param>
    /// <param name="frac">The fraction to be interpolated towards the target point</param>
    /// <returns></returns>
    public float3 Lerp(float3 target, float3 frac)
    {
      return this + (target - this) * frac;
    }

    /// <summary>
    /// Returns a float3 value with each component being the greater of two float3 values
    /// </summary>
    public static float3 Max(float3 v1, float3 v2)
    {
      return new float3(v1.x.Max(v2.x), v1.y.Max(v2.y), v1.z.Max(v2.z));
    }

    /// <summary>
    /// Returns a float3 value with each component being the greater of two float3 values
    /// </summary>
    public float3 Max(float3 other)
    {
      return new float3(x.Max(other.x), y.Max(other.y), z.Max(other.z));
    }

    /// <summary>
    /// Returns a float3 value with each component being the lesser of two float3 values
    /// </summary>
    public static float3 Min(float3 v1, float3 v2)
    {
      return new float3(v1.x.Min(v2.x), v1.y.Min(v2.y), v1.z.Min(v2.z));
    }

    /// <summary>
    /// Returns a float3 value with each component being the lesser of two float3 values
    /// </summary>
    public float3 Min(float3 other)
    {
      return new float3(x.Min(other.x), y.Min(other.y), z.Min(other.z));
    }

    /// <summary>
    /// Returns a value at most max_delta value closer to a target
    /// </summary>
    /// <param name="value">The starting value</param>
    /// <param name="target">The target value</param>
    /// <param name="max_delta">The max_delta for each dimension</param>
    /// <returns></returns>
    public static float3 Creep(float3 value, float3 target, float max_delta)
    {
      return new float3(value.x.Creep(target.x, max_delta), value.y.Creep(target.y, max_delta), value.z.Creep(target.z, max_delta));
    }

    /// <summary>
    /// Returns a value at most max_delta value closer to a target
    /// </summary>
    /// <param name="target">The target value</param>
    /// <param name="max_delta">The max_delta for each dimension</param>
    /// <returns></returns>
    public float3 Creep(float3 target, float max_delta)
    {
      return new float3(x.Creep(target.x, max_delta), y.Creep(target.y, max_delta), z.Creep(target.z, max_delta));
    }

    /// <summary>
    /// Returns a value at most max_delta value closer to a target
    /// </summary>
    /// <param name="value">The starting value</param>
    /// <param name="target">The target value</param>
    /// <param name="max_delta">The max_delta per dimension</param>
    /// <returns></returns>
    public static float3 Creep(float3 value, float3 target, float3 max_delta)
    {
      return new float3(value.x.Creep(target.x, max_delta.x), value.y.Creep(target.y, max_delta.y), value.z.Creep(target.z, max_delta.z));
    }

    /// <summary>
    /// Returns a value at most max_delta value closer to a target
    /// </summary>
    /// <param name="target">The target value</param>
    /// <param name="max_delta">The max_delta per dimension</param>
    /// <returns></returns>
    public float3 Creep(float3 target, float3 max_delta)
    {
      return new float3(x.Creep(target.x, max_delta.x), y.Creep(target.y, max_delta.y), z.Creep(target.z, max_delta.z));
    }

    /// <summary>
    /// Returns the result of (value % (max - min)), scaled so that lies between min and max
    /// </summary>
    /// <param name="value">The input value</param>
    /// <param name="min">The minimum value</param>
    /// <param name="max">The maximum value</param>
    /// <returns>(value % (max - min)), scaled so that lies between min and max</returns>
    public static float3 Modulus(float3 value, float3 min, float3 max)
    {
      return new float3(value.x.Modulus(min.x, max.x), value.y.Modulus(min.y, max.y), value.z.Modulus(min.z, max.z));
    }

    /// <summary>
    /// Returns the result of (value % (max - min)), scaled so that lies between min and max
    /// </summary>
    /// <param name="min">The minimum value</param>
    /// <param name="max">The maximum value</param>
    /// <returns>(value % (max - min)), scaled so that lies between min and max</returns>
    public float3 Modulus(float3 min, float3 max)
    {
      return new float3(x.Modulus(min.x, max.x), y.Modulus(min.y, max.y), z.Modulus(min.z, max.z));
    }
  }
}
