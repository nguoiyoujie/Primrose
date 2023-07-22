using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A float4x4 quad value</summary>
  public struct float4x4 : IEquatable<float4x4>
  {
    /// <summary>The value of the first row, as a float4</summary>
    public float4 row0;

    /// <summary>The value of the second row, as a float4</summary>
    public float4 row1;

    /// <summary>The value of the third row, as a float4</summary>
    public float4 row2;

    /// <summary>The value of the last row, as a float4</summary>
    public float4 row3;

    /// <summary>
    /// Creates a float4x4 value
    /// </summary>
    /// <param name="r1"></param>
    /// <param name="r2"></param>
    /// <param name="r3"></param>
    /// <param name="r4"></param>
    public float4x4(float4 r1, float4 r2, float4 r3, float4 r4) { this.row0 = r1; this.row1 = r2; this.row2 = r3; this.row3 = r4; }

    /// <summary>The value indexer</summary>
    /// <exception cref="IndexOutOfRangeException">The array is accessed with an invalid index</exception>
    public float4 this[int i]
    {
      get
      {
        switch (i)
        {
          case 0:
            return row0;
          case 1:
            return row1;
          case 2:
            return row2;
          case 3:
            return row3;
          default:
            throw new IndexOutOfRangeException(Resource.Strings.Error_InvalidIndex.F(i, GetType()));
        }
      }
      set
      {
        switch (i)
        {
          case 0:
            row0 = value;
            break;
          case 1:
            row1 = value;
            break;
          case 2:
            row2 = value;
            break;
          case 3:
            row3 = value;
            break;
          default:
            throw new IndexOutOfRangeException(Resource.Strings.Error_InvalidIndex.F(i, GetType()));
        }
      }
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{" + row0 + "," + row1 + "," + row2 + "," + row3 + "}"; }

    /// <summary>Creates a float4[] array from this value</summary>
    /// <returns>An array of length 4 with identical indexed values</returns>
    public float4[] ToArray() { return new float4[] { row0, row1, row2, row3 }; }

    /// <summary>Creates a float4x4 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A float4x4 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 4 can be converted to a float4x4</exception>
    public static float4x4 FromArray(float4[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 4)
        throw new ArrayMismatchException(array.Length, typeof(float4x4));
      else
        return new float4x4(array[0], array[1], array[2], array[3]);
    }

    /// <summary>Parses a float4x4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A float4x4 value</returns>
    public static float4x4 Parse(string s) { return FromArray(Parser.Parse<float4[]>(s.Trim(ArrayConstants.Braces))); }

    /// <summary>Parses a float4x4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A float4x4 value, or the default value if the parsing fails</returns>
    public static float4x4 Parse(string s, IResolver resolver, float4x4 defaultValue)
    {
      float4[] list = Parser.Parse(s.Trim(ArrayConstants.Braces), resolver, new float4[0]);
      float4x4 value = defaultValue;
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

    /// <summary>Parses a float4x4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out float4x4 result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a float4x4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out float4x4 result, IResolver resolver = null, float4x4 defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }


    /// <summary>Performs a memberwise negation of a float4x4 value</summary>
    /// <param name="a"></param><returns></returns>
    public static float4x4 operator -(float4x4 a)
    {
      return new float4x4(-a.row0, -a.row1, -a.row2, -a.row3);
    }

    /// <summary>Performs an addition operation between two float4x4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float4x4 operator +(float4x4 a, float4x4 b)
    {
      return new float4x4(a.row0 + b.row0, a.row1 + b.row1, a.row2 + b.row2, a.row3 + b.row3);
    }

    /// <summary>Performs a subtraction operation between two float4x4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float4x4 operator -(float4x4 a, float4x4 b)
    {
      return new float4x4(a.row0 - b.row0, a.row1 - b.row1, a.row2 - b.row2, a.row3 - b.row3);
    }

    /// <summary>Performs a product operation between two float4x4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float4x4 operator *(float4x4 a, float4x4 b)
    {
      return new float4x4(
        new float4(a[0].x * b[0].x + a[0].y * b[1].x + a[0].z * b[2].x + a[0].w * b[3].x, a[0].x * b[0].y + a[0].y * b[1].y + a[0].z * b[2].y + a[0].w * b[3].y, a[0].x * b[0].z + a[0].y * b[1].z + a[0].z * b[2].z + a[0].w * b[3].z, a[0].x * b[0].w + a[0].y * b[1].w + a[0].z * b[2].w + a[0].w * b[3].w), 
        new float4(a[1].x * b[0].x + a[1].y * b[1].x + a[1].z * b[2].x + a[1].w * b[3].x, a[1].x * b[0].y + a[1].y * b[1].y + a[1].z * b[2].y + a[1].w * b[3].y, a[1].x * b[0].z + a[1].y * b[1].z + a[1].z * b[2].z + a[1].w * b[3].z, a[1].x * b[0].w + a[1].y * b[1].w + a[1].z * b[2].w + a[1].w * b[3].w), 
        new float4(a[2].x * b[0].x + a[2].y * b[1].x + a[2].z * b[2].x + a[2].w * b[3].x, a[2].x * b[0].y + a[2].y * b[1].y + a[2].z * b[2].y + a[2].w * b[3].y, a[2].x * b[0].z + a[2].y * b[1].z + a[2].z * b[2].z + a[2].w * b[3].z, a[2].x * b[0].w + a[2].y * b[1].w + a[2].z * b[2].w + a[2].w * b[3].w), 
        new float4(a[3].x * b[0].x + a[3].y * b[1].x + a[3].z * b[2].x + a[3].w * b[3].x, a[3].x * b[0].y + a[3].y * b[1].y + a[3].z * b[2].y + a[3].w * b[3].y, a[3].x * b[0].z + a[3].y * b[1].z + a[3].z * b[2].z + a[3].w * b[3].z, a[3].x * b[0].w + a[3].y * b[1].w + a[3].z * b[2].w + a[3].w * b[3].w));
    }

    /// <summary>Performs a multiplication operation between a float4x4 value and a float multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float4x4 operator *(float m, float4x4 a)
    {
      return new float4x4(a.row0 * m, a.row1 * m, a.row2 * m, a.row3 * m);
    }

    /// <summary>Performs a multiplication operation between a float4x4 value and a float multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float4x4 operator *(float4x4 a, float m)
    {
      return new float4x4(a.row0 * m, a.row1 * m, a.row2 * m, a.row3 * m);
    }

    /// <summary>Performs a division operation between a float4x4 value and a float divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float4x4 operator /(float4x4 a, float m)
    {
      float d = 1 / m;
      return new float4x4(a.row0 * d, a.row1 * d, a.row2 * d, a.row3 * d);
    }

    /// <summary>Performs a modulus operation between a float4x4 value and a float divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float4x4 operator %(float4x4 a, float m)
    {
      return new float4x4(a.row0 % m, a.row1 % m, a.row2 % m, a.row3 % m);
    }

    /// <summary>Performs a transformation of a float3 vector</summary>
    /// <param name="a">The transformation matrix</param>
    /// <param name="v">The vector</param>
    /// <returns></returns>
    public static float3 operator *(float4x4 a, float3 v)
    {
      return new float3(
        a[0].x * v.x + a[0].y * v.y + a[0].z * v.z, 
        a[1].x * v.x + a[1].y * v.y + a[1].z * v.z, 
        a[2].x * v.x + a[2].y * v.y + a[2].z * v.z);
    }

    /// <summary>Performs a transformation of a float3 vector</summary>
    /// <param name="a">The transformation matrix</param>
    /// <param name="v">The vector</param>
    /// <returns></returns>
    public static float3 operator *(float3 v, float4x4 a)
    {
      return new float3(
        v.x * a[0].x + v.y * a[1].x + v.z * a[2].x, 
        v.x * a[0].y + v.y * a[1].y + v.z * a[2].y, 
        v.x * a[0].z + v.y * a[1].z + v.z * a[2].z);
    }

    /// <summary>Performs a transformation of a float4 vector</summary>
    /// <param name="a">The transformation matrix</param>
    /// <param name="v">The vector</param>
    /// <returns></returns>
    public static float4 operator *(float4x4 a, float4 v)
    {
      return new float4(
        a[0].x * v.x + a[0].y * v.y + a[0].z * v.z + a[0].w * v.w, 
        a[1].x * v.x + a[1].y * v.y + a[1].z * v.z + a[1].w * v.w, 
        a[2].x * v.x + a[2].y * v.y + a[2].z * v.z + a[2].w * v.w, 
        a[3].x * v.x + a[3].y * v.y + a[3].z * v.z + a[3].w * v.w);
    }

    /// <summary>Performs a transformation of a float4 vector</summary>
    /// <param name="a">The transformation matrix</param>
    /// <param name="v">The vector</param>
    /// <returns></returns>
    public static float4 operator *(float4 v, float4x4 a)
    {
      return new float4(
        v.x * a[0].x + v.y * a[1].x + v.z * a[2].x + v.w * a[3].x, 
        v.x * a[0].y + v.y * a[1].y + v.z * a[2].y + v.w * a[3].y, 
        v.x * a[0].z + v.y * a[1].z + v.z * a[2].z + v.w * a[3].z, 
        v.x * a[0].w + v.y * a[1].w + v.z * a[2].w + v.w * a[3].w);
    }

    /// <summary>Returns the absolute difference between two float4x4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float Diff(float4x4 a, float4x4 b)
    {
      return float4.Diff(a.row0, b.row0) + float4.Diff(a.row1, b.row1) + float4.Diff(a.row2, b.row2) + float4.Diff(a.row3, b.row3);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is float4x4 fobj && row0 == fobj.row0 && row1 == fobj.row1 && row2 == fobj.row2 && row3 == fobj.row3;
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(float4x4 other)
    {
      return row0 == other.row0 && row1 == other.row1 && row2 == other.row2 && row3 == other.row3;
    }

    /// <summary>Generates the hash code for this object</summary>
    public override int GetHashCode()
    {
      int hashCode = 1502939027;
      hashCode = hashCode * -1521134295 + row0.GetHashCode();
      hashCode = hashCode * -1521134295 + row1.GetHashCode();
      hashCode = hashCode * -1521134295 + row2.GetHashCode();
      hashCode = hashCode * -1521134295 + row3.GetHashCode();
      return hashCode;
    }

    /// <summary>Determines if two float4x4 values are equal</summary>
    public static bool operator ==(float4x4 a, float4x4 b)
    {
      return a.row0 == b.row0 && a.row1 == b.row1 && a.row2 == b.row2 && a.row3 == b.row3;
    }

    /// <summary>Determines if two float4x4 values are not equal</summary>
    public static bool operator !=(float4x4 a, float4x4 b)
    {
      return a.row0 != b.row0 || a.row1 != b.row1 || a.row2 != b.row2 || a.row3 != b.row3;
    }

    /// <summary>Returns a float4x4 value with all elements set to their default value</summary>
    public static float4x4 Empty { get { return new float4x4(); } }

    /// <summary>Creates an array from this value</summary>
    public static explicit operator Array(float4x4 a) { return a.ToArray(); }

    /// <summary>Creates a float[] array from this value</summary>
    public static explicit operator float4[](float4x4 a) { return a.ToArray(); }

    /// <summary>Creates a float4x4 value from this array</summary>
    public static implicit operator float4x4(float4[] a) { return FromArray(a); }

    // Extensions for transform operations
    /// <summary>Returns a float4x4 value representing the identity transformation matrix</summary>
    public static float4x4 Identity { get { return new float4x4(float4.UnitX, float4.UnitY, float4.UnitZ, float4.UnitW); } }

    /// <summary>Returns a float4x4 value representing a translation transformation matrix</summary>
    public static float4x4 CreateTranslation(float3 translation)
    {
      float4x4 m = Identity;
      m[3] = new float4(translation.x, translation.y, translation.z, 1f);
      return m;
    }
    /// <summary>Returns a float4x4 value representing a rotation transformation matrix</summary>
    public static float4x4 CreateRotationX(float angle)
    {
      float cos = (float)Math.Cos(angle);
      float sin = (float)Math.Sin(angle);
      return new float4x4(float4.UnitX, new float4(0f, cos, sin, 0f), new float4(0f, -sin, cos, 0f), float4.UnitW);
    }

    /// <summary>Returns a float4x4 value representing a rotation transformation matrix</summary>
    public static float4x4 CreateRotationY(float angle)
    {
      float cos = (float)Math.Cos(angle);
      float sin = (float)Math.Sin(angle);
      return new float4x4(new float4(cos, 0f, -sin, 0f), float4.UnitY, new float4(sin, 0f, cos, 0f), float4.UnitW);
    }

    /// <summary>Returns a float4x4 value representing a rotation transformation matrix</summary>
    public static float4x4 CreateRotationZ(float angle)
    {
      float cos = (float)Math.Cos(angle);
      float sin = (float)Math.Sin(angle);
      return new float4x4(new float4(cos, sin, 0f, 0f), new float4(-sin, cos, 0f, 0f), float4.UnitZ, float4.UnitW);
    }

    /// <summary>Returns a float4x4 value representing a rotation transformation matrix</summary>
    public static float4x4 CreateRotation(float3 axis, float angle)
    {
      float cos = (float)Math.Cos(angle);
      float sin = (float)Math.Sin(angle);
      float ncos = 1f - cos;
      return new float4x4(
        new float4(ncos * axis.x * axis.x + cos, ncos * axis.x * axis.y - sin * axis.z, ncos * axis.x * axis.z + sin * axis.y, 0f), 
        new float4(ncos * axis.y * axis.x + sin * axis.z, ncos * axis.y * axis.y + cos, ncos * axis.y * axis.z - sin * axis.x, 0f), 
        new float4(ncos * axis.z * axis.x - sin * axis.y, ncos * axis.z * axis.y + sin * axis.x, ncos * axis.z * axis.z + cos, 0f), 
        float4.UnitW);
    }

    /// <summary>Returns a float4x4 value representing a rotation transformation matrix</summary>
    public static float4x4 CreateFromAxisAngle(float3 axis, float angle)
    {
      float cos = (float)Math.Cos(-angle);
      float sin = (float)Math.Sin(-angle);
      float ncos = 1f - cos;
      axis.Normalize();
      return new float4x4(
        new float4(ncos * axis.x * axis.x + cos, ncos * axis.x * axis.y - sin * axis.z, ncos * axis.x * axis.z + sin * axis.y, 0f), 
        new float4(ncos * axis.y * axis.x + sin * axis.z, ncos * axis.y * axis.y + cos, ncos * axis.y * axis.z - sin * axis.x, 0f), 
        new float4(ncos * axis.z * axis.x - sin * axis.y, ncos * axis.z * axis.y + sin * axis.x, ncos * axis.z * axis.z + cos, 0f), 
        float4.UnitW);
    }

    /// <summary>Returns a float4x4 value representing a scale transformation matrix</summary>
    public static float4x4 CreateScaling(float3 scale)
    {
      return new float4x4(
        new float4(scale.x, 0f, 0f, 0f), 
        new float4(0f, scale.y, 0f, 0f), 
        new float4(0f, 0f, scale.z, 0f), 
        new float4(0f, 0f, 0f, 1f));
    }

    /// <summary>Returns a float4x4 value representing a orthographic projection matrix</summary>
    public static float4x4 CreateOrthographic(float width, float height, float zNear, float zFar)
    {
      return CreateOrthographicOffCenter(-width / 2f, width / 2f, -height / 2f, height / 2f, zNear, zFar);
    }

    /// <summary>Returns a float4x4 value representing a orthographic projection matrix</summary>
    public static float4x4 CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNear, float zFar)
    {
      float inv_width = 1f / (right - left);
      float inv_height = 1f / (top - bottom);
      float inv_depth = 1f / (zFar - zNear);
      return new float4x4(
        new float4(2f * inv_width, 0f, 0f, 0f), 
        new float4(0f, 2f * inv_height, 0f, 0f), 
        new float4(0f, 0f, -2f * inv_depth, 0f), 
        new float4(-(right + left) * inv_width, -(top + bottom) * inv_height, -(zFar + zNear) * inv_depth, 1f));
    }

    /// <summary>Returns a float4x4 value representing a perspective projection matrix</summary>
    public static float4x4 CreatePerspectiveFieldOfView(float fovy, float aspect, float zNear, float zFar)
    {
      if (fovy <= 0f || fovy > Math.PI)
      {
        throw new ArgumentOutOfRangeException("fovy");
      }
      if (aspect <= 0f)
      {
        throw new ArgumentOutOfRangeException("aspect");
      }
      float top = zNear * (float)Math.Tan((0.5f * fovy));
      float bottom = -top;
      float left = bottom * aspect;
      float right = top * aspect;
      return CreatePerspectiveOffCenter(left, right, bottom, top, zNear, zFar);
    }

    /// <summary>Returns a float4x4 value representing a perspective projection matrix</summary>
    public static float4x4 CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float zNear, float zFar)
    {
      if (zFar <= 0f)
      {
        throw new ArgumentOutOfRangeException("zFar");
      }
      if (zNear <= 0f || zNear >= zFar)
      {
        throw new ArgumentOutOfRangeException("zNear");
      }

      float x = 2f * zNear / (right - left);
      float y = 2f * zNear / (top - bottom);
      float x2 = (right + left) / (right - left);
      float y2 = (top + bottom) / (top - bottom);
      float z = -(zFar + zNear) / (zFar - zNear);
      float z2 = -(2f * zFar * zNear) / (zFar - zNear);
      return new float4x4(new float4(x, 0f, 0f, 0f), new float4(0f, y, 0f, 0f), new float4(x2, y2, z, -1f), new float4(0f, 0f, z2, 0f));
    }

    /// <summary>Returns a float4x4 value representing a transformation from a current view point to a specified point</summary>
    public static float4x4 LookAt(float3 eye, float3 target, float3 up)
    {
      float3 vector = (eye - target).Normalize();
      float3 v = float3.Cross(up, vector).Normalize();
      float3 vector2 = float3.Cross(vector, v).Normalize();
      float4x4 m = new float4x4(
        new float4(v.x, vector2.x, vector.x, 0f), 
        new float4(v.y, vector2.y, vector.y, 0f), 
        new float4(v.z, vector2.z, vector.z, 0f), 
        float4.UnitW);
      return CreateTranslation(-eye) * m;
    }

    /// <summary>Returns a float4x4 value representing a transposition of a matrix</summary>
    public float4x4 Transpose()
    {
      return new float4x4(
        new float4(this[0].x, this[1].x, this[2].x, this[3].x), 
        new float4(this[0].y, this[1].y, this[2].y, this[3].y), 
        new float4(this[0].z, this[1].z, this[2].z, this[3].z), 
        new float4(this[0].w, this[1].w, this[2].w, this[3].w));
    }

    /// <summary>Returns a float4x4 value representing a inverse transformation of a matrix</summary>
    public float4x4 Inverse()
    {
      // make a copy of the matrix
      float4x4 matrix = this; // struct copy
      float4x4 identity = float4x4.Identity;
      for (int i = 0; i < 4; i++)
      {
        int num = i;
        for (int j = i + 1; j < 4; j++)
        {
          if (Math.Abs(matrix[j][i]) > Math.Abs(matrix[num][i]))
          {
            num = j;
          }
        }
        matrix.SwapRows(num, i);
        identity.SwapRows(num, i);
        if (matrix[i][i] == 0f)
        {
          throw new Exception("float4x4 was a singular matrix and cannot be inverted.");
        }
        int a;
        identity[a = i] = identity[a] / matrix[i][i];
        int a2;
        matrix[a2 = i] = matrix[a2] / matrix[i][i];
        for (int k = 0; k < 4; k++)
        {
          if (k != i)
          {
            int a3;
            identity[a3 = k] = identity[a3] - matrix[k][i] * identity[i];
            int a4;
            matrix[a4 = k] = matrix[a4] - matrix[k][i] * matrix[i];
          }
        }
      }
      return identity;
    }

    private void SwapRows(int i, int j)
    {
      float4 value = this[i];
      this[i] = this[j];
      this[j] = value;
    }

    /// <summary>Returns a float4x4 value representing a rotation transformation matrix</summary>
    public static float4x4 CreateRotationFromQuaternion(quaternion rotation)
    {
      float4 vector = rotation.ToAxisAngle();
      return float4x4.CreateFromAxisAngle(new float3(vector.x, vector.y, vector.z), rotation.w);
    }
  }
}
