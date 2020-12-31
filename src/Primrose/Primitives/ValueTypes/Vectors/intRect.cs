using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>A int rectangle value</summary>
  public struct intRect : IEquatable<intRect>
  {
    /// <summary>The x or [0] value</summary>
    public int x;

    /// <summary>The y or [1] value</summary>
    public int y;

    /// <summary>The w or [2] value</summary>
    public int w;

    /// <summary>The h or [3] value</summary>
    public int h;

    /// <summary>
    /// Creates a intRect value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="w"></param>
    /// <param name="h"></param>
    public intRect(int x, int y, int w, int h) { this.x = x; this.y = y; this.w = w; this.h = h; }

    /// <summary>
    /// Creates a intRect value
    /// </summary>
    /// <param name="position"></param>
    /// <param name="size"></param>
    public intRect(int2 position, int2 size) { this.x = position.x; this.y = position.y; this.w = size.x; this.h = size.y; }


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
    public int2 Position
    {
      get => new int2(x, y);
      set { x = value.x; y = value.y; }
    }

    /// <summary>The position of the rectangle</summary>
    public int2 Size
    {
      get => new int2(w, h);
      set { w = value.x; h = value.y; }
    }


    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "{{{0},{1},{2},{3}}}".F(x, y, w, h); }

    /// <summary>Creates a int[] array from this value</summary>
    /// <returns>An array of length 4 with identical indexed values</returns>
    public int[] ToArray() { return new int[] { x, y, w, h }; }

    /// <summary>Creates a intRect from an array</summary>
    /// <param name="array">The array</param>
    /// <returns>A intRect value</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    /// <exception cref="ArrayMismatchException">Only an array of length 4 can be converted to a intRect</exception>
    public static intRect FromArray(int[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof(array));
      else if (array.Length != 4)
        throw new ArrayMismatchException(array.Length, typeof(intRect));
      else
        return new intRect(array[0], array[1], array[2], array[3]);
    }

    /// <summary>Parses a intRect from a string</summary>
    /// <param name="s">The string value</param>
    /// <returns>A intRect value</returns>
    public static intRect Parse(string s) { return FromArray(Parser.Parse<int[]>(s.Trim('{', '}'))); }

    /// <summary>Parses a intRect from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>A intRect value, or the default value if the parsing fails</returns>
    public static intRect Parse(string s, IResolver resolver, intRect defaultValue)
    {
      int[] list = Parser.Parse(s.Trim('{', '}'), resolver, new int[0]);
      intRect value = defaultValue;
      for (int i = 0; i < list.Length; i++)
        value[i] = list[i];

      return value;
    }

    /// <summary>Parses a intRect from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out intRect result) { result = default; try { result = Parse(s); return true; } catch { return false; } }

    /// <summary>Parses a intRect from a string</summary>
    /// <param name="s">The string value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <param name="result">The parsed value</param>
    /// <returns>True if the parse is successful</returns>
    public static bool TryParse(string s, out intRect result, IResolver resolver = null, intRect defaultValue = default) { result = defaultValue; try { result = Parse(s, resolver, defaultValue); return true; } catch { return false; } }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is intRect fobj && x == fobj.x && y == fobj.y && w == fobj.w && h == fobj.h;
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(intRect other)
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

    /// <summary>Determines if two intRect values are equal</summary>
    public static bool operator ==(intRect a, intRect b)
    {
      return a.x == b.x && a.y == b.y && a.w == b.w && a.h == b.h;
    }

    /// <summary>Determines if two intRect values are not equal</summary>
    public static bool operator !=(intRect a, intRect b)
    {
      return a.x != b.x || a.y != b.y || a.w != b.w || a.h != b.h;
    }

    /// <summary>Returns a intRect value with all elements set to their default value</summary>
    public static intRect Empty { get { return new intRect(); } }

    /// <summary>Determines if a point is inside the rectangle represented</summary>
    public bool ContainsPoint(int2 point)
    {
      int2 p = point - Position;
      return p.x.Within(0, w) && p.y.Within(0, h);
    }

    /// <summary>Determines if at least part of a rectangle intersects with the rectangle represented</summary>
    public bool IntersectsRectangle(int2 topleft, int2 size)
    {
      topleft -= Position;
      int min_x = topleft.x;
      int max_x = topleft.x + size.x;
      int min_y = topleft.y;
      int max_y = topleft.y + size.y;

      return (0.Within(min_x, max_x) || w.Within(min_x, max_x) || min_x.Within(0, w) || max_x.Within(0, w))
          && (0.Within(min_y, max_y) || h.Within(min_y, max_y) || min_y.Within(0, h) || max_y.Within(0, h));
    }

    /// <summary>Determines if at least part of a rectangle intersects with the rectangle represented</summary>
    public bool IntersectsRectangle(intRect otherRect)
    {
      int min_x = otherRect.x - Position.x;
      int max_x = otherRect.x - Position.x + otherRect.w;
      int min_y = otherRect.y - Position.y;
      int max_y = otherRect.y - Position.y + otherRect.h;

      return (0.Within(min_x, max_x) || w.Within(min_x, max_x) || min_x.Within(0, w) || max_x.Within(0, w))
          && (0.Within(min_y, max_y) || h.Within(min_y, max_y) || min_y.Within(0, h) || max_y.Within(0, h));
    }

    /// <summary>Returns the bounding rectangle of the union of two rectangles</summary>
    public intRect Union(intRect other)
    {
      return new intRect(x.Min(other.x)
                        , y.Min(other.y)
                        , w.Max(other.x + other.w - x)
                        , h.Max(other.y + other.h - y)
                        );
    }

    /// <summary>Returns the bounding rectangle of the intersection of two rectangles</summary>
    public intRect Intersect(intRect other)
    {
      return new intRect(x.Max(other.x)
                  , y.Max(other.y)
                  , (x + w - other.x).Clamp(0, other.x + other.w - x)
                  , (y + h - other.y).Clamp(0, other.y + other.h - y)
                  );
    }

    /// <summary>Creates a int[] array from this value</summary>
    public static explicit operator int[](intRect a) { return a.ToArray(); }

    /// <summary>Creates a intRect value from this array</summary>
    public static explicit operator intRect(int[] a) { return FromArray(a); }
  }
}
