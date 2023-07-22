using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>Represents a transformed bounding box on two float dimensions</summary>
  public struct float2obox : IEquatable<float2obox>
  {
    /// <summary>The axis-aligned bounding box prior to transformation</summary>
    public float2box Box;

    /// <summary>The transformation matrix applied to the box</summary>
    public float3x3 Transform;

    /// <summary>
    /// Creates a float2obox value
    /// </summary>
    /// <param name="box"></param>
    /// <param name="transform"></param>
    public float2obox(float2box box, float3x3 transform)
    {
      Box = box;
      Transform = transform;
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "[" + Box + "->" + Transform + "]"; }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is float2obox box && Equals(box);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(float2obox other)
    {
      return Box.Equals(other.Box) &&
             Transform.Equals(other.Transform);
    }

    /// <summary>Generates the hash code for this object</summary>
    public override int GetHashCode()
    {
      int hashCode = 1772944719;
      hashCode = hashCode * -1521134295 + Box.GetHashCode();
      hashCode = hashCode * -1521134295 + Transform.GetHashCode();
      return hashCode;
    }

    /// <summary>Determines if two float2obox values are equal</summary>
    public static bool operator ==(float2obox v1, float2obox v2)
    {
      return v1.Box == v2.Box && v1.Transform == v2.Transform;
    }

    /// <summary>Determines if two float2obox values are not equal</summary>
    public static bool operator !=(float2obox v1, float2obox v2)
    {
      return v1.Box != v2.Box || v1.Transform != v2.Transform;
    }

    /// <summary>Determines if a point is inside the rectangle represented</summary>
    public bool ContainsPoint(float2 point)
    {
      // apply inverse transform on the point, then check against the AABB bounds
      float3x3 m = Transform.Inverse();
      float2 p = point * m;
      return Box.ContainsPoint(p);
    }

    /// <summary>Determines if a point is inside the rectangle represented</summary>
    public bool Intersects(float2ray ray)
    {
      return ray.Intersects(this);
    }
  }
}
