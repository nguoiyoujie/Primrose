using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>Represents a transformed bounding box on three float dimensions</summary>
  public struct float3obox : IEquatable<float3obox>
  {
    /// <summary>The axis-aligned bounding box prior to transformation</summary>
    public float3box Box;

    /// <summary>The transformation matrix applied to the box</summary>
    public float4x4 Transform;

    /// <summary>
    /// Creates a float3obox value
    /// </summary>
    /// <param name="box"></param>
    /// <param name="transform"></param>
    public float3obox(float3box box, float4x4 transform)
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
      return obj is float3obox box && Equals(box);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(float3obox other)
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

    /// <summary>Determines if two float3obox values are equal</summary>
    public static bool operator ==(float3obox v1, float3obox v2)
    {
      return v1.Box == v2.Box && v1.Transform == v2.Transform;
    }

    /// <summary>Determines if two float3obox values are not equal</summary>
    public static bool operator !=(float3obox v1, float3obox v2)
    {
      return v1.Box != v2.Box || v1.Transform != v2.Transform;
    }

    /// <summary>Determines if a point is inside the rectangle represented</summary>
    public bool ContainsPoint(float3 point)
    {
      // apply inverse transform on the point, then check against the AABB bounds
      float4x4 m = Transform.Inverse();
      float3 p = point * m;
      return Box.ContainsPoint(p);
    }

    /// <summary>Determines if a point is inside the rectangle represented</summary>
    public bool Intersects(float3ray ray)
    {
      return ray.Intersects(this);
    }
  }
}
