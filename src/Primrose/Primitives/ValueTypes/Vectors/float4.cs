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
    public override string ToString() { return "{" + x.ToString() + "," + y.ToString() + "," + z.ToString() + "," + w.ToString() + "}"; }

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
    public static float4 Parse(string s) { return FromArray(Parser.Parse<float[]>(s.Trim(ArrayConstants.Braces))); }

    /// <summary>Parses a float4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A float4 value, or the default value if the parsing fails</returns>
    public static float4 Parse(string s, IResolver resolver, float4 defaultValue)
    {
      float[] list = Parser.Parse(s.Trim(ArrayConstants.Braces), resolver, new float[0]);
      float4 value = defaultValue;
      if (list.Length != 0)
      {
        for (int i = 0; i < list.Length; i++)
          value[i] = list[i];
        // fill excluded indices with the same value as the last
        for (int i = list.Length; i < 4; i++)
          value[i] = list[list.Length - 1];
      }
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
      float d = 1 / m;
      return new float4(a.x * d, a.y * d, a.z * d, a.w * d);
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

    // Extensions for transform operations
    /// <summary>Returns a float4 value with all elements set to 1</summary>
    public static float4 One { get { return new float4(1f, 1f, 1f, 1f); } }

    /// <summary>Returns a float4 value representing a unit vector in the x direction</summary>
    public static float4 UnitX { get { return new float4(1f, 0f, 0f, 0f); } }

    /// <summary>Returns a float4 value representing a unit vector in the y direction</summary>
    public static float4 UnitY { get { return new float4(0f, 1f, 0f, 0f); } }

    /// <summary>Returns a float4 value representing a unit vector in the y direction</summary>
    public static float4 UnitZ { get { return new float4(0f, 0f, 1f, 0f); } }

    /// <summary>Returns a float4 value representing a unit vector in the y direction</summary>
    public static float4 UnitW { get { return new float4(0f, 0f, 0f, 1f); } }

    /// <summary>Calculates the length, or Euclidean distance, of the vector</summary>
    public float Length { get { return (float)Math.Sqrt(x * x + y * y + z * z + w * w); } }

    /// <summary>Calculates the length, or Euclidean distance, of the vector squared</summary>
    public float LengthSquared { get { return x * x + y * y + z * z + w * w; } }

    /// <summary>Calculates the Euclidean distance, between two vectors</summary>
    public static float Distance(float4 v1, float4 v2) { return (v2 - v1).Length; }

    /// <summary>Calculates the Euclidean distance, between two vectors squared</summary>
    public static float DistanceSquared(float4 v1, float4 v2) { return (v2 - v1).LengthSquared; }

    /// <summary>Returns a float4 value of the same direction, normalized to unit length</summary>
    public float4 Normalize() { return (x == 0 && y == 0 && z == 0) ? default : (this / Length); }

    /// <summary>Calculates the dot product between to float4 vectors</summary>
    public static float Dot(float4 v1, float4 v2) { return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z + v1.w * v2.w; }

    /// <summary>Calculates the dot product between to float4 vectors</summary>
    public float Dot(float4 v) { return x * v.x + y * v.y + z * v.z + w * v.w; }

    /// <summary>Swaps two float4 values</summary>
    public static void Swap(ref float4 v1, ref float4 v2)
    {
      float4 temp = v1;
      v1 = v2;
      v2 = temp;
    }

    /// <summary>Swaps two float4 values</summary>
    public void Swap(ref float4 other)
    {
      float4 temp = this;
      this = other;
      other = temp;
    }

    /// <summary>Determines the reflect vector of the given vector and normal</summary>
    public static float4 Reflect(float4 v, float4 normal) { return v - v.Dot(normal) * normal * 2f; }

    /// <summary>Determines the reflect vector of the given vector and normal</summary>
    public float4 Reflect(float4 normal) { return this - Dot(normal) * normal * 2f; }

    /// <summary>
    /// Returns a value linearly interpolated towards a target
    /// </summary>
    /// <param name="value">The starting value</param>
    /// <param name="target">The target value</param>
    /// <param name="frac">The fraction to be interpolated towards the target point</param>
    /// <returns></returns>
    public static float4 Lerp(float4 value, float4 target, float frac)
    {
      return value + (target - value) * frac;
    }

    /// <summary>
    /// Returns a value linearly interpolated towards a target
    /// </summary>
    /// <param name="target">The target value</param>
    /// <param name="frac">The fraction to be interpolated towards the target point</param>
    /// <returns></returns>
    public float4 Lerp(float4 target, float frac)
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
    public static float4 Lerp(float4 value, float4 target, float4 frac)
    {
      return value + (target - value) * frac;
    }

    /// <summary>
    /// Returns a value linearly interpolated towards a target
    /// </summary>
    /// <param name="target">The target value</param>
    /// <param name="frac">The fraction to be interpolated towards the target point</param>
    /// <returns></returns>
    public float4 Lerp(float4 target, float4 frac)
    {
      return this + (target - this) * frac;
    }

    /// <summary>
    /// Returns a float4 value with each component being the greater of two float4 values
    /// </summary>
    public static float4 Max(float4 v1, float4 v2)
    {
      return new float4(v1.x.Max(v2.x), v1.y.Max(v2.y), v1.z.Max(v2.z), v1.w.Max(v2.w));
    }

    /// <summary>
    /// Returns a float4 value with each component being the greater of two float4 values
    /// </summary>
    public float4 Max(float4 other)
    {
      return new float4(x.Max(other.x), y.Max(other.y), z.Max(other.z), w.Max(other.w));
    }

    /// <summary>
    /// Returns a float4 value with each component being the lesser of two float4 values
    /// </summary>
    public static float4 Min(float4 v1, float4 v2)
    {
      return new float4(v1.x.Min(v2.x), v1.y.Min(v2.y), v1.z.Min(v2.z), v1.w.Min(v2.w));
    }

    /// <summary>
    /// Returns a float4 value with each component being the lesser of two float4 values
    /// </summary>
    public float4 Min(float4 other)
    {
      return new float4(x.Min(other.x), y.Min(other.y), z.Min(other.z), w.Min(other.w));
    }

    /// <summary>
    /// Returns a value at most max_delta value closer to a target
    /// </summary>
    /// <param name="value">The starting value</param>
    /// <param name="target">The target value</param>
    /// <param name="max_delta">The max_delta for each dimension</param>
    /// <returns></returns>
    public static float4 Creep(float4 value, float4 target, float max_delta)
    {
      return new float4(value.x.Creep(target.x, max_delta), value.y.Creep(target.y, max_delta), value.z.Creep(target.z, max_delta), value.w.Creep(target.w, max_delta));
    }

    /// <summary>
    /// Returns a value at most max_delta value closer to a target
    /// </summary>
    /// <param name="target">The target value</param>
    /// <param name="max_delta">The max_delta for each dimension</param>
    /// <returns></returns>
    public float4 Creep(float4 target, float max_delta)
    {
      return new float4(x.Creep(target.x, max_delta), y.Creep(target.y, max_delta), z.Creep(target.z, max_delta), w.Creep(target.w, max_delta));
    }

    /// <summary>
    /// Returns a value at most max_delta value closer to a target
    /// </summary>
    /// <param name="value">The starting value</param>
    /// <param name="target">The target value</param>
    /// <param name="max_delta">The max_delta per dimension</param>
    /// <returns></returns>
    public static float4 Creep(float4 value, float4 target, float4 max_delta)
    {
      return new float4(value.x.Creep(target.x, max_delta.x), value.y.Creep(target.y, max_delta.y), value.z.Creep(target.z, max_delta.z), value.w.Creep(target.w, max_delta.w));
    }

    /// <summary>
    /// Returns a value at most max_delta value closer to a target
    /// </summary>
    /// <param name="target">The target value</param>
    /// <param name="max_delta">The max_delta per dimension</param>
    /// <returns></returns>
    public float4 Creep(float4 target, float4 max_delta)
    {
      return new float4(x.Creep(target.x, max_delta.x), y.Creep(target.y, max_delta.y), z.Creep(target.z, max_delta.z), w.Creep(target.w, max_delta.w));
    }

    /// <summary>
    /// Returns the result of (value % (max - min)), scaled so that lies between min and max
    /// </summary>
    /// <param name="value">The input value</param>
    /// <param name="min">The minimum value</param>
    /// <param name="max">The maximum value</param>
    /// <returns>(value % (max - min)), scaled so that lies between min and max</returns>
    public static float4 Modulus(float4 value, float4 min, float4 max)
    {
      return new float4(value.x.Modulus(min.x, max.x), value.y.Modulus(min.y, max.y), value.z.Modulus(min.z, max.z), value.w.Modulus(min.w, max.w));
    }

    /// <summary>
    /// Returns the result of (value % (max - min)), scaled so that lies between min and max
    /// </summary>
    /// <param name="min">The minimum value</param>
    /// <param name="max">The maximum value</param>
    /// <returns>(value % (max - min)), scaled so that lies between min and max</returns>
    public float4 Modulus(float4 min, float4 max)
    {
      return new float4(x.Modulus(min.x, max.x), y.Modulus(min.y, max.y), z.Modulus(min.z, max.z), w.Modulus(min.w, max.w));
    }
  }
}
