using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>
  /// A value pair
  /// </summary>
  /// <typeparam name="T">The type of the first value</typeparam>
  /// <typeparam name="U">The type of the second value</typeparam>
  public struct Pair<T, U> : IEquatable<Pair<T, U>>
  {
    /// <summary>The first value</summary>
    public T t;

    /// <summary>The second value</summary>
    public U u;

    /// <summary>
    /// Creates a value pair with given values
    /// </summary>
    /// <param name="t">The first value</param>
    /// <param name="u">The second value</param>
    public Pair(T t, U u) { this.t = t; this.u = u; }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is Pair<T, U> fobj
          && EqualityComparer<T>.Default.Equals(t, fobj.t)
          && EqualityComparer<U>.Default.Equals(u, fobj.u);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(Pair<T, U> other)
    {
      return EqualityComparer<T>.Default.Equals(t, other.t)
          && EqualityComparer<U>.Default.Equals(u, other.u);
    }

    /// <summary>Generates the hash code for this object</summary>
    public override int GetHashCode()
    {
      int hashCode = 1502939027;
      hashCode = hashCode * -1521134295 + t?.GetHashCode() ?? 0;
      hashCode = hashCode * -1521134295 + u?.GetHashCode() ?? 0;
      return hashCode;
    }

    /// <summary>Determines if two short3 values are equal</summary>
    public static bool operator ==(Pair<T, U> a, Pair<T, U> b)
    {
      return EqualityComparer<T>.Default.Equals(a.t, b.t)
          && EqualityComparer<U>.Default.Equals(a.u, b.u);
    }

    /// <summary>Determines if two short3 values are not equal</summary>
    public static bool operator !=(Pair<T, U> a, Pair<T, U> b)
    {
      return !EqualityComparer<T>.Default.Equals(a.t, b.t)
          || !EqualityComparer<U>.Default.Equals(a.u, b.u);
    }

    /// <summary>Provides a string representation of this value</summary>
    public override string ToString()
    {
      return "{{{0},{1}}}".F(t, u);
    }
  }
}
