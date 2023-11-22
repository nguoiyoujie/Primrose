using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A float3x3 quad value</summary>
  public struct float3x3 : IEquatable<float3x3>
  {
    /// <summary>The value of the first row, as a float3</summary>
    public float3 row0;

    /// <summary>The value of the second row, as a float3</summary>
    public float3 row1;

    /// <summary>The value of the third row, as a float3</summary>
    public float3 row2;

    /// <summary>
    /// Creates a float3x3 value
    /// </summary>
    /// <param name="r1"></param>
    /// <param name="r2"></param>
    /// <param name="r3"></param>
    public float3x3(float3 r1, float3 r2, float3 r3) { this.row0 = r1; this.row1 = r2; this.row2 = r3; }

    /// <summary>The value indexer</summary>
    /// <exception cref="IndexOutOfRangeException">The array is accessed with an invalid index</exception>
    public float3 this[int i]
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
          default:
            throw new IndexOutOfRangeException(Resource.Strings.Error_InvalidIndex.F(i, GetType()));
        }
      }
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{" + row0 + "," + row1 + "," + row2 + "}"; }

    /// <summary>Creates a float3[] array from this value</summary>
    /// <returns>An array of length 4 with identical indexed values</returns>
    public float3[] ToArray() { return new float3[] { row0, row1, row2 }; }

    /// <summary>Creates a float3x3 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A float3x3 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 4 can be converted to a float3x3</exception>
    public static float3x3 FromArray(float3[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 3)
        throw new ArrayMismatchException(array.Length, typeof(float3x3));
      else
        return new float3x3(array[0], array[1], array[2]);
    }

    /// <summary>Parses a float3x3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A float3x3 value</returns>
    public static float3x3 Parse(string s) { return FromArray(Parser.Parse<float3[]>(s.Trim(ArrayConstants.Braces))); }

    /// <summary>Parses a float3x3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A float3x3 value, or the default value if the parsing fails</returns>
    public static float3x3 Parse(string s, IResolver resolver, float3x3 defaultValue)
    {
      float3[] list = Parser.Parse(s.Trim(ArrayConstants.Braces), resolver, new float3[0]);
      float3x3 value = defaultValue;
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

    /// <summary>Parses a float3x3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out float3x3 result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a float3x3 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out float3x3 result, IResolver resolver = null, float3x3 defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }


    /// <summary>Performs a memberwise negation of a float3x3 value</summary>
    /// <param name="a"></param><returns></returns>
    public static float3x3 operator -(float3x3 a)
    {
      return new float3x3(-a.row0, -a.row1, -a.row2);
    }

    /// <summary>Performs an addition operation between two float3x3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float3x3 operator +(float3x3 a, float3x3 b)
    {
      return new float3x3(a.row0 + b.row0, a.row1 + b.row1, a.row2 + b.row2);
    }

    /// <summary>Performs a subtraction operation between two float3x3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float3x3 operator -(float3x3 a, float3x3 b)
    {
      return new float3x3(a.row0 - b.row0, a.row1 - b.row1, a.row2 - b.row2);
    }

    /// <summary>Performs a product operation between two float3x3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float3x3 operator *(float3x3 a, float3x3 b)
    {
      return new float3x3(
          new float3(a[0].x * b[0].x + a[0].y * b[1].x + a[0].z * b[2].x, a[0].x * b[0].y + a[0].y * b[1].y + a[0].z * b[2].y, a[0].x * b[0].z + a[0].y * b[1].z + a[0].z * b[2].z), 
          new float3(a[1].x * b[0].x + a[1].y * b[1].x + a[1].z * b[2].x, a[1].x * b[0].y + a[1].y * b[1].y + a[1].z * b[2].y, a[1].x * b[0].z + a[1].y * b[1].z + a[1].z * b[2].z), 
          new float3(a[2].x * b[0].x + a[2].y * b[1].x + a[2].z * b[2].x, a[2].x * b[0].y + a[2].y * b[1].y + a[2].z * b[2].y, a[2].x * b[0].z + a[2].y * b[1].z + a[2].z * b[2].z));
    }

    /// <summary>Performs a multiplication operation between a float3x3 value and a float multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float3x3 operator *(float m, float3x3 a)
    {
      return new float3x3(a.row0 * m, a.row1 * m, a.row2 * m);
    }

    /// <summary>Performs a multiplication operation between a float3x3 value and a float multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float3x3 operator *(float3x3 a, float m)
    {
      return new float3x3(a.row0 * m, a.row1 * m, a.row2 * m);
    }

    /// <summary>Performs a division operation between a float3x3 value and a float divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float3x3 operator /(float3x3 a, float m)
    {
      float d = 1 / m;
      return new float3x3(a.row0 * d, a.row1 * d, a.row2 * d);
    }

    /// <summary>Performs a modulus operation between a float3x3 value and a float divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static float3x3 operator %(float3x3 a, float m)
    {
      return new float3x3(a.row0 % m, a.row1 % m, a.row2 % m);
    }

    /// <summary>Performs a transformation of a float2 vector</summary>
    /// <param name="a">The transformation matrix</param>
    /// <param name="v">The vector</param>
    /// <returns></returns>
    public static float2 operator *(float3x3 a, float2 v)
    {
      return new float2(
        a[0].x * v.x + a[0].y * v.y, 
        a[1].x * v.x + a[1].y * v.y);
    }

    /// <summary>Performs a transformation of a float2 vector</summary>
    /// <param name="a">The transformation matrix</param>
    /// <param name="v">The vector</param>
    /// <returns></returns>  
    public static float2 operator *(float2 v, float3x3 a)
    {
      return new float2(
        v.x * a[0].x + v.y * a[1].x, 
        v.x * a[0].y + v.y * a[1].y);
    }

    /// <summary>Performs a transformation of a float3 vector</summary>
    /// <param name="a">The transformation matrix</param>
    /// <param name="v">The vector</param>
    /// <returns></returns>  
    public static float3 operator *(float3x3 a, float3 v)
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
    public static float3 operator *(float3 v, float3x3 a)
    {
      return new float3(
        v.x * a[0].x + v.y * a[1].x + v.z * a[2].x, 
        v.x * a[0].y + v.y * a[1].y + v.z * a[2].y,
        v.x * a[0].z + v.y * a[1].z + v.z * a[2].z);
    }

    /// <summary>Returns the absolute difference between two float3x3 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static float Diff(float3x3 a, float3x3 b)
    {
      return float3.Diff(a.row0, b.row0) + float3.Diff(a.row1, b.row1) + float3.Diff(a.row2, b.row2);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is float3x3 fobj && row0 == fobj.row0 && row1 == fobj.row1 && row2 == fobj.row2;
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(float3x3 other)
    {
      return row0 == other.row0 && row1 == other.row1 && row2 == other.row2;
    }

    /// <summary>Generates the hash code for this object</summary>
    public override int GetHashCode()
    {
      int hashCode = 1502939027;
      hashCode = hashCode * -1521134295 + row0.GetHashCode();
      hashCode = hashCode * -1521134295 + row1.GetHashCode();
      hashCode = hashCode * -1521134295 + row2.GetHashCode();
      return hashCode;
    }

    /// <summary>Determines if two float3x3 values are equal</summary>
    public static bool operator ==(float3x3 a, float3x3 b)
    {
      return a.row0 == b.row0 && a.row1 == b.row1 && a.row2 == b.row2;
    }

    /// <summary>Determines if two float3x3 values are not equal</summary>
    public static bool operator !=(float3x3 a, float3x3 b)
    {
      return a.row0 != b.row0 || a.row1 != b.row1 || a.row2 != b.row2;
    }

    /// <summary>Returns a float3x3 value with all elements set to their default value</summary>
    public static float3x3 Empty { get { return new float3x3(); } }

    /// <summary>Creates an array from this value</summary>
    public static explicit operator Array(float3x3 a) { return a.ToArray(); }

    /// <summary>Creates a float[] array from this value</summary>
    public static explicit operator float3[](float3x3 a) { return a.ToArray(); }

    /// <summary>Creates a float3x3 value from this array</summary>
    public static implicit operator float3x3(float3[] a) { return FromArray(a); }

    // Extensions for transform operations
    /// <summary>Returns a float3x3 value representing the identity transformation matrix</summary>
    public static float3x3 Identity { get { return new float3x3(float3.UnitX, float3.UnitY, float3.UnitZ); } }

    /// <summary>Returns a float3x3 value representing a translation transformation matrix</summary>
    public static float3x3 CreateTranslation(float2 translation)
    {
      float3x3 m = Identity;
      m[3] = new float3(translation.x, translation.y, 1f);
      return m;
    }
    /// <summary>Returns a float3x3 value representing a rotation transformation matrix</summary>
    public static float3x3 CreateRotation(float angle)
    {
      float cos = (float)Math.Cos(angle);
      float sin = (float)Math.Sin(angle);
      return new float3x3(new float3(cos, sin, 0f), new float3(-sin, cos, 0f), float3.UnitZ);
    }

    /// <summary>Returns a float3x3 value representing a scale transformation matrix</summary>
    public static float3x3 CreateScaling(float2 scale)
    {
      return new float3x3(
        new float3(scale.x, 0f, 0f), 
        new float3(0f, scale.y, 0f), 
        new float3(0f, 0f, 1f));
    }

    /// <summary>Returns a float3x3 value representing a transposition of a matrix</summary>
    public float3x3 Transpose()
    {
      return new float3x3(
        new float3(this[0].x, this[1].x, this[2].x), 
        new float3(this[0].y, this[1].y, this[2].y), 
        new float3(this[0].z, this[1].z, this[2].z));
    }

    /// <summary>Returns a float3x3 value representing a inverse transformation of a matrix</summary>
    public float3x3 Inverse()
    {
      float3x3 matrix = this;
      float3x3 identity = Identity;
      for (int i = 0; i < 3; i++)
      {
        int num = i;
        for (int j = i + 1; j < 3; j++)
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
          throw new Exception("float3x3 was a singular matrix and cannot be inverted.");
        }
        int a;
        identity[a = i] = identity[a] / matrix[i][i];
        int a2;
        matrix[a2 = i] = matrix[a2] / matrix[i][i];
        for (int k = 0; k < 3; k++)
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
      float3 value = this[i];
      this[i] = this[j];
      this[j] = value;
    }
  }
}
