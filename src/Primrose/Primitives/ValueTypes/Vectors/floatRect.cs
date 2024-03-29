﻿using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A float rectangle value</summary>
  public struct floatRect : IEquatable<floatRect>
  {
    /// <summary>The x or [0] value</summary>
    public float x;

    /// <summary>The y or [1] value</summary>
    public float y;

    /// <summary>The w or [2] value</summary>
    public float w;

    /// <summary>The h or [3] value</summary>
    public float h;

    /// <summary>
    /// Creates a floatRect value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="w"></param>
    /// <param name="h"></param>
    public floatRect(float x, float y, float w, float h) { this.x = x; this.y = y; this.w = w; this.h = h; }

    /// <summary>
    /// Creates a floatRect value
    /// </summary>
    /// <param name="position"></param>
    /// <param name="size"></param>
    public floatRect(float2 position, float2 size) { this.x = position.x; this.y = position.y; this.w = size.x; this.h = size.y; }


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
            return w;
          case 3:
            return h;
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
            w = value;
            break;
          case 3:
            h = value;
            break;
          default:
            throw new IndexOutOfRangeException(Resource.Strings.Error_InvalidIndex.F(i, GetType()));
        }
      }
    }

    /// <summary>The position of the rectangle</summary>
    public float2 Position
    {
      get => new float2(x, y);
      set { x = value.x; y = value.y; }
    }

    /// <summary>The position of the rectangle</summary>
    public float2 Size
    {
      get => new float2(w, h);
      set { w = value.x; h = value.y; }
    }


    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{" + x.ToString() + "," + y.ToString() + "," + w + "," + h + "}"; }

    /// <summary>Creates a float[] array from this value</summary>
    /// <returns>An array of length 4 with identical indexed values</returns>
    public float[] ToArray() { return new float[] { x, y, w, h }; }

    /// <summary>Creates a floatRect from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A floatRect value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 4 can be converted to a floatRect</exception>
    public static floatRect FromArray(float[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 4)
        throw new ArrayMismatchException(array.Length, typeof(floatRect));
      else
        return new floatRect(array[0], array[1], array[2], array[3]);
    }

    /// <summary>Creates a floatRect from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A floatRect value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 4 can be converted to a floatRect</exception>
    public static floatRect FromArray(int[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 4)
        throw new ArrayMismatchException(array.Length, typeof(floatRect));
      else
        return new floatRect(array[0], array[1], array[2], array[3]);
    }


    /// <summary>Parses a floatRect from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A floatRect value</returns>
    public static floatRect Parse(string s) { return FromArray(Parser.Parse<float[]>(s.Trim(ArrayConstants.Braces))); }

    /// <summary>Parses a floatRect from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A floatRect value, or the default value if the parsing fails</returns>
    public static floatRect Parse(string s, IResolver resolver, floatRect defaultValue)
    {
      float[] list = Parser.Parse(s.Trim(ArrayConstants.Braces), resolver, new float[0]);
      floatRect value = defaultValue;
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

    /// <summary>Parses a floatRect from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out floatRect result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a floatRect from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out floatRect result, IResolver resolver = null, floatRect defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is floatRect fobj && x == fobj.x && y == fobj.y && w == fobj.w && h == fobj.h;
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(floatRect other)
    {
      return x == other.x && y == other.y && w == other.w && h == other.h;
    }

    /// <summary>Generates the hash code for this object</summary>
    public override int GetHashCode()
    {
      int hashCode = 1502939027;
      hashCode = hashCode * -1521134295 + x.GetHashCode();
      hashCode = hashCode * -1521134295 + y.GetHashCode();
      hashCode = hashCode * -1521134295 + w.GetHashCode();
      hashCode = hashCode * -1521134295 + h.GetHashCode();
      return hashCode;
    }

    /// <summary>Determines if two floatRect values are equal</summary>
    public static bool operator ==(floatRect a, floatRect b)
    {
      return a.x == b.x && a.y == b.y && a.w == b.w && a.h == b.h;
    }

    /// <summary>Determines if two floatRect values are not equal</summary>
    public static bool operator !=(floatRect a, floatRect b)
    {
      return a.x != b.x || a.y != b.y || a.w != b.w || a.h != b.h;
    }

    /// <summary>Returns a floatRect value with all elements set to their default value</summary>
    public static floatRect Empty { get { return new floatRect(); } }

    /// <summary>Determines if a point is inside the rectangle represented</summary>
    public bool ContainsPoint(float2 point)
    {
      float2 p = point - Position;
      return p.x.WithinRangeInclusive(0, w) && p.y.WithinRange(0, h);
    }

    /// <summary>Determines if at least part of a rectangle intersects with the rectangle represented</summary>
    public bool IntersectsRectangle(float2 topleft, float2 size)
    {
      topleft -= Position;
      float min_x = topleft.x;
      float max_x = topleft.x + size.x;
      float min_y = topleft.y;
      float max_y = topleft.y + size.y;

      return (0f.WithinRangeInclusive(min_x, max_x) || w.WithinRangeInclusive(min_x, max_x) || min_x.WithinRangeInclusive(0, w) || max_x.WithinRangeInclusive(0, w))
          && (0f.WithinRangeInclusive(min_y, max_y) || h.WithinRangeInclusive(min_y, max_y) || min_y.WithinRangeInclusive(0, h) || max_y.WithinRangeInclusive(0, h));
    }

    /// <summary>Determines if at least part of a rectangle intersects with the rectangle represented</summary>
    public bool IntersectsRectangle(floatRect otherRect)
    {
      float min_x = otherRect.x - Position.x;
      float max_x = otherRect.x - Position.x + otherRect.w;
      float min_y = otherRect.y - Position.y;
      float max_y = otherRect.y - Position.y + otherRect.h;

      return (0f.WithinRangeInclusive(min_x, max_x) || w.WithinRangeInclusive(min_x, max_x) || min_x.WithinRangeInclusive(0, w) || max_x.WithinRangeInclusive(0, w))
          && (0f.WithinRangeInclusive(min_y, max_y) || h.WithinRangeInclusive(min_y, max_y) || min_y.WithinRangeInclusive(0, h) || max_y.WithinRangeInclusive(0, h));
    }

    /// <summary>Returns the bounding rectangle of the union of two rectangles</summary>
    public floatRect Union(floatRect other)
    {
      return new floatRect(x.Min(other.x)
                        , y.Min(other.y)
                        , w.Max(other.x + other.w - x)
                        , h.Max(other.y + other.h - y)
                        );
    }

    /// <summary>Returns the bounding rectangle of the floatersection of two rectangles</summary>
    public floatRect Interesect(floatRect other)
    {
      return new floatRect(x.Max(other.x)
                  , y.Max(other.y)
                  , (x + w - other.x).Clamp(0, other.x + other.w - x)
                  , (y + h - other.y).Clamp(0, other.y + other.h - y)
                  );
    }

    /// <summary>Creates a float[] array from this value</summary>
    public static explicit operator float[](floatRect a) { return a.ToArray(); }

    /// <summary>Creates a floatRect value from this array</summary>
    public static explicit operator floatRect(float[] a) { return FromArray(a); }

    /// <summary>Creates a floatRect value from this array</summary>
    public static explicit operator floatRect(int[] a) { return FromArray(a); }

    /// <summary>Converts a intRect value to a floatRect value</summary>
    public static implicit operator floatRect(intRect a) { return new floatRect(a.x, a.y, a.w, a.h); }
  }
}
