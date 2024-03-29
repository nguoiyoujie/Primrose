﻿using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  ///<summary>Defines a container containing four <see cref="int"/> values</summary>
  public struct int4 : IEquatable<int4>, IComparable<int4>
  {
    /// <summary>The x or [0] value</summary>
    public int x;

    /// <summary>The y or [1] value</summary>
    public int y;

    /// <summary>The z or [2] value</summary>
    public int z;

    /// <summary>The w or [3] value</summary>
    public int w;

    /// <summary>
    /// Creates a <see cref="int4"/> value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="w"></param>
    public int4(int x, int y, int z, int w) { this.x = x; this.y = y; this.z = z; this.w = w; }

    /// <summary>The value indexer</summary>
    /// <exception cref="IndexOutOfRangeException">The array is accessed with an invalid index</exception>
    public int this[int i]
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

    /// <summary>Creates a int[] array from this value</summary>
    /// <returns>An array of length 4 with identical indexed values</returns>
    public int[] ToArray() { return new int[] { x, y, z, w }; }

    /// <summary>Creates a int4 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A int4 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 4 can be converted to a int4</exception>
    public static int4 FromArray(int[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 4)
        throw new ArrayMismatchException(array.Length, typeof(int4));
      else
        return new int4(array[0], array[1], array[2], array[3]);
    }

    /// <summary>Parses a int4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A int4 value</returns>
    public static int4 Parse(string s) { return FromArray(Parser.Parse<int[]>(s.Trim(ArrayConstants.Braces))); }

    /// <summary>Parses a int4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A int4 value, or the default value if the parsing fails</returns>
    public static int4 Parse(string s, IResolver resolver, int4 defaultValue)
    {
      int[] list = Parser.Parse(s.Trim(ArrayConstants.Braces), resolver, new int[0]);
      int4 value = defaultValue;
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

    /// <summary>Parses a int4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out int4 result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a int4 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out int4 result, IResolver resolver = null, int4 defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }


    /// <summary>Performs a memberwise negation of a int4 value</summary>
    /// <param name="a"></param><returns></returns>
    public static int4 operator -(int4 a)
    {
      return new int4(-a.x, -a.y, -a.z, -a.w);
    }

    /// <summary>Performs an addition operation between two int4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static int4 operator +(int4 a, int4 b)
    {
      return new int4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    }

    /// <summary>Performs a subtraction operation between two int4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static int4 operator -(int4 a, int4 b)
    {
      return new int4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    }

    /// <summary>Performs a memberwise multiplication operation between two int4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static int4 operator *(int4 a, int4 b)
    {
      return new int4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    }

    /// <summary>Performs a memberwise division between two int4 values</summary>
    /// <param name="a"></param><param name="b"></param><returns></returns>
    public static int4 operator /(int4 a, int4 b)
    {
      return new int4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    }

    /// <summary>Performs a multiplication operation between a int2 value and a int multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static int4 operator *(int m, int4 a)
    {
      return a * m;
    }

    /// <summary>Performs a multiplication operation between a int4 value and a int multiplier</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static int4 operator *(int4 a, int m)
    {
      return new int4(a.x * m, a.y * m, a.z * m, a.w * m);
    }

    /// <summary>Performs a division operation between a int4 value and a int divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static int4 operator /(int4 a, int m)
    {
      return new int4(a.x / m, a.y / m, a.z / m, a.w / m);
    }

    /// <summary>Performs a modulus operation between a int4 value and a int divisor</summary>
    /// <param name="a"></param><param name="m"></param><returns></returns>
    public static int4 operator %(int4 a, int m)
    {
      return new int4(a.x % m, a.y % m, a.z % m, a.w % m);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is int4 fobj && x == fobj.x && y == fobj.y && z == fobj.z && w == fobj.w;
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(int4 other)
    {
      return x == other.x && y == other.y && z == other.z && w == other.w;
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

    /// <summary>Compares one int3 value to the other</summary>
    public int CompareTo(int4 other)
    {
      int r = w.CompareTo(w);
      if (r == 0) { r = z.CompareTo(other.z); }
      if (r == 0) { r = y.CompareTo(other.y); }
      if (r == 0) { r = x.CompareTo(other.x); }
      return r;
    }

    /// <summary>Determines if two int4 values are equal</summary>
    public static bool operator ==(int4 a, int4 b)
    {
      return a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
    }

    /// <summary>Determines if two int4 values are not equal</summary>
    public static bool operator !=(int4 a, int4 b)
    {
      return a.x != b.x || a.y != b.y || a.z != b.z || a.w != b.w;
    }

    /// <summary>Returns a int4 value with all elements set to their default value</summary>
    public static int4 Empty { get { return new int4(); } }

    /// <summary>Creates an array from this value</summary>
    public static explicit operator Array(int4 a) { return a.ToArray(); }

    /// <summary>Creates a int[] array from this value</summary>
    public static explicit operator int[](int4 a) { return a.ToArray(); }

    /// <summary>Creates a int4 value from this array</summary>
    public static implicit operator int4(int[] a) { return FromArray(a); }
  }
}
