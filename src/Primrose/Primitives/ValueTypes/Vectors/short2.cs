﻿using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A short2 pair value</summary>
  public struct short2 : IEquatable<short2>
  {
    /// <summary>The x or [0] value</summary>
    public short x;

    /// <summary>The y or [1] value</summary>
    public short y;

    /// <summary>
    /// Creates a short2 value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public short2(short x, short y) { this.x = x; this.y = y; }

    /// <summary>The value indexer</summary>
    /// <exception cref="IndexOutOfRangeException">The array is accessed with an invalid index</exception>
    public short this[int i]
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

    /// <summary>Creates a short[] array from this value</summary>
    /// <returns>An array of length 2 with identical indexed values</returns>
    public short[] ToArray() { return new short[] { x, y }; }

    /// <summary>Creates a short2 from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A short2 value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 2 can be converted to a short2</exception>
    public static short2 FromArray(short[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 2)
        throw new ArrayMismatchException(array.Length, typeof(short2));
      else
        return new short2(array[0], array[1]);
    }

    /// <summary>Parses a short2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A short2 value</returns>
    public static short2 Parse(string s) { return FromArray(Parser.Parse<short[]>(s.Trim(ArrayConstants.Braces))); }

    /// <summary>Parses a short2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A short2 value, or the default value if the parsing fails</returns>
    public static short2 Parse(string s, IResolver resolver, short2 defaultValue)
    {
      short[] list = Parser.Parse(s.Trim(ArrayConstants.Braces), resolver, new short[0]);
      short2 value = defaultValue;
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

    /// <summary>Parses a short2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out short2 result) { result = default; try { result = Parse(s); return true; } catch {  return false; } }

    /// <summary>Parses a short2 from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out short2 result, IResolver resolver = null, short2 defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is short2 fobj && x == fobj.x && y == fobj.y;
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(short2 other)
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

    /// <summary>Determines if two short2 values are equal</summary>
    public static bool operator ==(short2 a, short2 b)
    {
      return a.x == b.x && a.y == b.y;
    }

    /// <summary>Determines if two short2 values are not equal</summary>
    public static bool operator !=(short2 a, short2 b)
    {
      return a.x != b.x || a.y != b.y;
    }

    /// <summary>Returns a short2 value with all elements set to their default value</summary>
    public static short2 Empty { get { return new short2(); } }

    /// <summary>Creates a short[] array from this value</summary>
    public static explicit operator short[](short2 a) { return a.ToArray(); }

    /// <summary>Creates a short2 value from this array</summary>
    public static explicit operator short2(short[] a) { return FromArray(a); }
  }
}
