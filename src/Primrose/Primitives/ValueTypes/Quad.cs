using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>
  /// A value quad
  /// </summary>
  /// <typeparam name="T">The type of the first value</typeparam>
  /// <typeparam name="U">The type of the second value</typeparam>
  /// <typeparam name="V">The type of the third value</typeparam>
  /// <typeparam name="W">The type of the fourth value</typeparam>
  public struct Quad<T, U, V, W> : IEquatable<Quad<T, U, V, W>>
  {
    /// <summary>The first value</summary>
    public T t;

    /// <summary>The second value</summary>
    public U u;

    /// <summary>The third value</summary>
    public V v;

    /// <summary>The fourth value</summary>
    public W w;

    /// <summary>
    /// Creates a value triple with given values
    /// </summary>
    /// <param name="t">The first value</param>
    /// <param name="u">The second value</param>
    /// <param name="v">The third value</param>
    /// <param name="w">The fourth value</param>
    public Quad(T t, U u, V v, W w) { this.t = t; this.u = u; this.v = v; this.w = w; }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is Quad<T, U, V, W> fobj
          && EqualityComparer<T>.Default.Equals(t, fobj.t)
          && EqualityComparer<U>.Default.Equals(u, fobj.u)
          && EqualityComparer<V>.Default.Equals(v, fobj.v)
          && EqualityComparer<W>.Default.Equals(w, fobj.w);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(Quad<T, U, V, W> other)
    {
      return EqualityComparer<T>.Default.Equals(t, other.t)
          && EqualityComparer<U>.Default.Equals(u, other.u)
          && EqualityComparer<V>.Default.Equals(v, other.v)
          && EqualityComparer<W>.Default.Equals(w, other.w);
    }

    /// <summary>Generates the hash code for this object</summary>
    public override int GetHashCode()
    {
      int hashCode = 1502939027;
      hashCode = hashCode * -1521134295 + t?.GetHashCode() ?? 0;
      hashCode = hashCode * -1521134295 + u?.GetHashCode() ?? 0;
      hashCode = hashCode * -1521134295 + v?.GetHashCode() ?? 0;
      hashCode = hashCode * -1521134295 + w?.GetHashCode() ?? 0;
      return hashCode;
    }

    /// <summary>Determines if two short3 values are equal</summary>
    public static bool operator ==(Quad<T, U, V, W> a, Quad<T, U, V, W> b)
    {
      return EqualityComparer<T>.Default.Equals(a.t, b.t)
          && EqualityComparer<U>.Default.Equals(a.u, b.u)
          && EqualityComparer<V>.Default.Equals(a.v, b.v)
          && EqualityComparer<W>.Default.Equals(a.w, b.w);
    }

    /// <summary>Determines if two short3 values are not equal</summary>
    public static bool operator !=(Quad<T, U, V, W> a, Quad<T, U, V, W> b)
    {
      return !EqualityComparer<T>.Default.Equals(a.t, b.t)
          || !EqualityComparer<U>.Default.Equals(a.u, b.u)
          || !EqualityComparer<V>.Default.Equals(a.v, b.v)
          || !EqualityComparer<W>.Default.Equals(a.w, b.w);
    }

    /// <summary>Provides a string representation of this value</summary>
    public override string ToString()
    {
      return "{{{0},{1},{2},{3}}}".F(t, u, v, w);
    }
  }
}
