using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A quaternion value, essentially a float4 with special operation behavior</summary>
  public struct quaternion : IEquatable<quaternion>
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
    /// Creates a quaternion value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="w"></param>
    public quaternion(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }

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

    /// <summary>Creates a quaternion from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A quaternion value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 4 can be converted to a quaternion</exception>
    public static quaternion FromArray(float[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 4)
        throw new ArrayMismatchException(array.Length, typeof(quaternion));
      else
        return new quaternion(array[0], array[1], array[2], array[3]);
    }

    /// <summary>Creates a quaternion from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A quaternion value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 4 can be converted to a quaternion</exception>
    public static quaternion FromArray(int[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 4)
        throw new ArrayMismatchException(array.Length, typeof(quaternion));
      else
        return new quaternion(array[0], array[1], array[2], array[3]);
    }

    /// <summary>Parses a quaternion from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A quaternion value</returns>
    public static quaternion Parse(string s) { return FromArray(Parser.Parse<float[]>(s.Trim(ArrayConstants.Braces))); }

    /// <summary>Parses a quaternion from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A quaternion value, or the default value if the parsing fails</returns>
    public static quaternion Parse(string s, IResolver resolver, quaternion defaultValue)
    {
      float[] list = Parser.Parse(s.Trim(ArrayConstants.Braces), resolver, new float[0]);
      quaternion value = defaultValue;
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

    /// <summary>Parses a quaternion from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out quaternion result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a quaternion from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out quaternion result, IResolver resolver = null, quaternion defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }


    /// <summary>Performs a memberwise negation of a quaternion value</summary>
    /// <param name="a"></param><returns></returns>
    public static quaternion operator -(quaternion a)
    {
      return new quaternion(-a.x, -a.y, -a.z, -a.w);
    }

    /// <summary>Performs an addition operation between two quaternion values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static quaternion operator +(quaternion a, quaternion b)
    {
      return new quaternion(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    }

    /// <summary>Performs a subtraction operation between two quaternion values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static quaternion operator -(quaternion a, quaternion b)
    {
      return new quaternion(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    }

    /// <summary>Performs a memberwise multiplication operation between two quaternion values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static quaternion operator *(quaternion a, quaternion b)
    {
      return new quaternion(a.x * b.w + a.y * b.z - a.z * b.y + a.w * b.x, -a.x * b.z + a.y * b.w + a.z * b.x + a.w * b.y, a.x * b.y - a.y * b.x + a.z * b.w + a.w * b.z, -a.x * b.x - a.y * b.y - a.z * b.z + a.w * b.w);
    }

    /// <summary>Performs a memberwise division between two quaternion values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static quaternion operator /(quaternion a, quaternion b)
    {
      return a * b.Inverse();
    }

    /// <summary>Performs a multiplication operation between a quaternion value and a float multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static quaternion operator *(float m, quaternion a)
    {
      return new quaternion(a.x * m, a.y * m, a.z * m, a.w * m);
    }

    /// <summary>Performs a multiplication operation between a quaternion value and a float multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static quaternion operator *(quaternion a, float m)
    {
      return new quaternion(a.x * m, a.y * m, a.z * m, a.w * m);
    }

    /// <summary>Performs a multiplication operation between a quaternion value and a float3 vector</summary>
    /// <param name="a"></param><param name="v"></param><returns></returns>
    public static float3 operator *(quaternion a, float3 v)
    {
      float3 v2 = new float3(a.x, a.y, a.z);
      float3 vec = float3.Cross(v2, v);
      float3 vec2 = float3.Cross(v2, vec);
      vec *= 2f * a.w;
      vec2 *= 2f;
      return v + vec + vec2;
    }

    /// <summary>Performs a division operation between a quaternion value and a float divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static quaternion operator /(quaternion a, float m)
    {
      float d = 1 / m;
      return new quaternion(a.x * d, a.y * d, a.z * d, a.w * d);
    }

    /// <summary>Performs a modulus operation between a quaternion value and a float divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static quaternion operator %(quaternion a, float m)
    {
      return new quaternion(a.x % m, a.y % m, a.z % m, a.w % m);
    }

    /// <summary>Returns the absolute difference between two quaternion values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float Diff(quaternion a, quaternion b)
    {
      return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y) + Math.Abs(a.z - b.z) + Math.Abs(a.w - b.w);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is quaternion fobj && x == fobj.x && y == fobj.y && z == fobj.z && w == fobj.w;
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(quaternion other)
    {
      return x == other.x && y == other.y && z == other.z && w == other.w;
    }

    /// <summary>Casts a quaternion to float4</summary>
    /// <param name="o">The object to cast</param>
    public static explicit operator float4(quaternion o)
    {
      return new float4((int)o.x, (int)o.y, (int)o.z, (int)o.w);
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

    /// <summary>Determines if two quaternion values are equal</summary>
    public static bool operator ==(quaternion a, quaternion b)
    {
      return a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
    }

    /// <summary>Determines if two quaternion values are not equal</summary>
    public static bool operator !=(quaternion a, quaternion b)
    {
      return a.x != b.x || a.y != b.y || a.z != b.z || a.w != b.w;
    }

    /// <summary>Returns a quaternion value with all elements set to their default value</summary>
    public static quaternion Empty { get { return new quaternion(); } }

    /// <summary>Creates an array from this value</summary>
    public static explicit operator Array(quaternion a) { return a.ToArray(); }

    /// <summary>Creates a float[] array from this value</summary>
    public static explicit operator float[](quaternion a) { return a.ToArray(); }

    /// <summary>Creates a quaternion value from this array</summary>
    public static implicit operator quaternion(float[] a) { return FromArray(a); }

    /// <summary>Creates a quaternion value from this array</summary>
    public static implicit operator quaternion(int[] a) { return FromArray(a); }

    /// <summary>Converts a float4 value to a quaternion value</summary>
    public static implicit operator quaternion(float4 a) { return new quaternion(a.x, a.y, a.z, a.w); }

    // Extensions for transform operations
    /// <summary>Returns the identity quaternion value</summary>
    public static quaternion Identity { get { return new quaternion(0f, 0f, 0f, 1f); } }

    /// <summary>Calculates the length, or Euclidean distance, of the vector</summary>
    public float Length { get { return (float)Math.Sqrt(x * x + y * y + z * z + w * w); } }

    /// <summary>Calculates the length, or Euclidean distance, of the vector squared</summary>
    public float LengthSquared { get { return x * x + y * y + z * z + w * w; } }

    /// <summary>Returns a quaternion value of the same direction, normalized to unit length</summary>
    public quaternion Normalize() { return (x == 0 && y == 0 && z == 0) ? default : (this / Length); }

    /// <summary>Returns a quaternion value of the same direction, normalized to unit length</summary>
    public quaternion Conjugate() { return -this; }

    /// <summary>Calculates the inverse of a quaternion</summary>
    public quaternion Inverse() { return Conjugate() / LengthSquared; }

    /// <summary>Converts a rotation to angle-axis representation</summary>
    public float4 ToAxisAngle()
    {
      quaternion q = this;
      if (q.w > 1f)
      {
        q.Normalize();
      }
      float w = 2f * (float)Math.Acos(q.w);
      float num = (float)Math.Sqrt(1f - q.w * q.w);
      if (num > 0.0001f)
      {
        return new float4(q.x / num, q.y / num, q.z / num, w);
      }
      return new float4(1f, 0f, 0f, w);
    }

    /// <summary>Returns a quaternion value derived from rotations along the three axes</summary>
    public static quaternion FromAxis(float3 xvec, float3 yvec, float3 zvec)
    {
      float4x4 rotation = new float4x4(
        new float4(xvec.x, yvec.x, zvec.x, 0f), 
        new float4(xvec.y, yvec.y, zvec.y, 0f), 
        new float4(xvec.z, yvec.z, zvec.z, 0f), 
        float4.Empty);
      return FromRotationMatrix(rotation);
    }

    /// <summary>Returns a quaternion value derived from a rotation transformation matrix</summary>
    public static quaternion FromRotationMatrix(float4x4 rotation)
    {
      float num = rotation[0].x + rotation[1].y + rotation[2].z;
      float num2;
      if (num > 0.0f)
      {
        quaternion zero = quaternion.Empty;
        num2 = (float)Math.Sqrt(num + 1f);
        zero.w = 0.5f * num2;
        num2 = 0.5f / num2;
        zero.x = (rotation[2].y - rotation[1].z) * num2;
        zero.y = (rotation[0].z - rotation[2].x) * num2;
        zero.z = (rotation[1].x - rotation[0].y) * num2;
        return zero;
      }
      quaternion zero2 = quaternion.Empty;
      int i = 0;
      if (rotation[1].y > rotation[0].x)
      {
        i = 1;
      }
      if (rotation[2].z > rotation[i][i])
      {
        i = 2;
      }
      int j = (i + 1) % 3;
      int k = (j + 1) % 3;
      num2 = (float)Math.Sqrt(rotation[i][i] - rotation[j][j] - rotation[k][k] + 1f);
      zero2[i] = 0.5f * num2;
      num2 = 0.5f / num2;
      zero2.w = (rotation[k][j] - rotation[j][k]) * num2;
      zero2[j] = (rotation[j][i] + rotation[i][j]) * num2;
      zero2[k] = (rotation[k][i] + rotation[i][k]) * num2;
      return zero2;
    }

    /// <summary>Calculates the dot product between to quaternion vectors</summary>
    public static float Dot(quaternion v1, quaternion v2) { return (float)Math.Sqrt(v1.x * v2.x + v1.y * v2.y + v1.z * v2.z + v1.w * v2.w); }

    /// <summary>Calculates the dot product between to quaternion vectors</summary>
    public float Dot(quaternion v2) { return (float)Math.Sqrt(x * v2.x + y * v2.y + z * v2.z + w * v2.w); }

    /// <summary>Swaps two quaternion values</summary>
    public static void Swap(ref quaternion v1, ref quaternion v2)
    {
      quaternion temp = v1;
      v1 = v2;
      v2 = temp;
    }

    /// <summary>Swaps two quaternion values</summary>
    public void Swap(ref quaternion v2)
    {
      quaternion temp = this;
      this = v2;
      v2 = temp;
    }

    /// <summary>Calculates the log of a quaternion</summary>
    public quaternion Log()
    {
      float num = (float)Math.Acos((double)w);
      float num2 = (float)Math.Sin((double)num);
      if (num2 > 0f)
      {
        float n = num / num2;
        return new quaternion(n * x, n * y, n * z, 0f);
      }
      return new quaternion(x, y, z, 0f);
    }

    /// <summary>Calculates the exponent of a quaternion</summary>
    public quaternion Exp()
    {
      float num = (float)Math.Sqrt((double)(x * x + y * y + z * z));
      float num2 = (float)Math.Sin((double)num);
      float w = (float)Math.Cos((double)num);
      if (num > 0f)
      {
        float n = num2 / num;
        return new quaternion(n * x, n * y, n * z, w);
      }
      return new quaternion(x, y, z, w);
    }

    /// <summary>
    /// Returns a value linearly interpolated towards a target
    /// </summary>
    /// <param name="value">The starting value</param>
    /// <param name="target">The target value</param>
    /// <param name="frac">The fraction to be interpolated towards the target point</param>
    /// <returns></returns>
    public static quaternion Lerp(quaternion value, quaternion target, float frac)
    {
      return (value + (target - value) * frac).Normalize();
    }

    /// <summary>
    /// Returns a value spherically interpolated towards a target
    /// </summary>
    /// <param name="value">The starting value</param>
    /// <param name="target">The target value</param>
    /// <param name="frac">The fraction to be interpolated towards the target point</param>
    /// <returns></returns>
    public static quaternion SLerp(quaternion value, quaternion target, float frac)
    {
      float num = value.Dot(target);
      float num2 = 1f;
      if (num < 0f)
      {
        num = -num;
        num2 = -1f;
      }
      float s;
      float num5;
      if (num < 0.999f)
      {
        float num3 = (float)Math.Acos((double)num);
        float num4 = 1f / (float)Math.Sqrt((double)(1f - num * num));
        s = (float)Math.Sin((double)((1f - frac) * num3)) * num4;
        num5 = (float)Math.Sin((double)(frac * num3)) * num4;
      }
      else
      {
        s = 1f - frac;
        num5 = frac;
      }
      return s * value + num2 * num5 * target;
    }

    /// <summary>
    /// Returns a value quadractically interpolated towards a target
    /// </summary>
    /// <param name="q1">The previous quaterion value</param>
    /// <param name="q2">The next quaterion value</param>
    /// <param name="ta">The start of the target segment</param>
    /// <param name="tb">The start of the target segment</param>
    /// <param name="t">The interpolation factor</param>
    /// <returns></returns>
    public static quaternion Squad(quaternion q1, quaternion q2, quaternion ta, quaternion tb, float t)
    {
      float t2 = 2f * t * (1f - t);
      quaternion q3 = quaternion.SLerp(q1, q2, t);
      quaternion q4 = quaternion.SLerp(ta, tb, t);
      return quaternion.SLerp(q3, q4, t2);
    }

    /// <summary>
    /// Returns a value quadractically interpolated towards a target
    /// </summary>
    /// <param name="prev"></param>
    /// <param name="q1"></param>
    /// <param name="q2"></param>
    /// <param name="post"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static quaternion SimpleSquad(quaternion prev, quaternion q1, quaternion q2, quaternion post, float t)
    {
      if (prev.Dot(q1) < 0f)
      {
        q1 = -q1;
      }
      if (q1.Dot(q2) < 0f)
      {
        q2 = -q2;
      }
      if (q2.Dot(post) < 0f)
      {
        post = -post;
      }
      quaternion ta = quaternion.Spline(prev, q1, q2);
      quaternion tb = quaternion.Spline(q1, q2, post);
      return quaternion.Squad(q1, q2, ta, tb, t);
    }

    /// <summary>
    /// Returns a value polynomially interpolated towards a target (cubic interpolation)
    /// </summary>
    /// <param name="pre">The previous quaterion value</param>
    /// <param name="q">The target value</param>
    /// <param name="post">The next quaterion value</param>
    /// <returns></returns>
    public static quaternion Spline(quaternion pre, quaternion q, quaternion post)
    {
      quaternion q2 = q.Conjugate();
      return q * (((q2 * pre).Log() + (q2 * post).Log()) * -0.25f).Exp();
    }
  }
}
