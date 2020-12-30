using System;
using System.Collections.Generic;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>
  /// A value triple
  /// </summary>
  /// <typeparam name="T">The type of the first value</typeparam>
  /// <typeparam name="U">The type of the second value</typeparam>
  /// <typeparam name="V">The type of the third value</typeparam>
  public struct Trip<T, U, V> : IEquatable<Trip<T, U, V>>
  {
    /// <summary>The first value</summary>
    public T t;

    /// <summary>The second value</summary>
    public U u;

    /// <summary>The third value</summary>
    public V v;

    /// <summary>
    /// Creates a value triple with given values
    /// </summary>
    /// <param name="t">The first value</param>
    /// <param name="u">The second value</param>
    /// <param name="v">The third value</param>
    public Trip(T t, U u, V v) { this.t = t; this.u = u; this.v = v; }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is Trip<T, U, V> fobj
          && EqualityComparer<T>.Default.Equals(t, fobj.t)
          && EqualityComparer<U>.Default.Equals(u, fobj.u)
          && EqualityComparer<V>.Default.Equals(v, fobj.v);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(Trip<T, U, V> other)
    {
      return EqualityComparer<T>.Default.Equals(t, other.t)
          && EqualityComparer<U>.Default.Equals(u, other.u)
          && EqualityComparer<V>.Default.Equals(v, other.v);
    }

    /// <summary>Generates the hash code for this object</summary>
    public override int GetHashCode()
    {
      int hashCode = 1502939027;
      hashCode = hashCode * -1521134295 + t.GetHashCode();
      hashCode = hashCode * -1521134295 + u.GetHashCode();
      hashCode = hashCode * -1521134295 + v.GetHashCode();
      return hashCode;
    }

    /// <summary>Determines if two short3 values are equal</summary>
    public static bool operator ==(Trip<T, U, V> a, Trip<T, U, V> b)
    {
      return EqualityComparer<T>.Default.Equals(a.t, b.t)
          && EqualityComparer<U>.Default.Equals(a.u, b.u)
          && EqualityComparer<V>.Default.Equals(a.v, b.v);
    }

    /// <summary>Determines if two short3 values are not equal</summary>
    public static bool operator !=(Trip<T, U, V> a, Trip<T, U, V> b)
    {
      return !EqualityComparer<T>.Default.Equals(a.t, b.t)
          || !EqualityComparer<U>.Default.Equals(a.u, b.u)
          || !EqualityComparer<V>.Default.Equals(a.v, b.v);
    }
  }
}