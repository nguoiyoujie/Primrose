using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>Represents an axis-aligned bounding box on three float dimensions</summary>
  public struct float3box : IEquatable<float3box>
  {
    /// <summary>The coordinates of the lower bound</summary>
    public float3 LowerBound;

    /// <summary>The coordinates of the upper bound</summary>
    public float3 UpperBound;

    /// <summary>
    /// Creates a float3box value
    /// </summary>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    public float3box(float3 lowerBound, float3 upperBound)
    {
      this.LowerBound = lowerBound;
      this.UpperBound = upperBound;
    }

    /// <summary>Returns the string representation of this value</summary>
    public override string ToString() { return "[" + LowerBound + "," + UpperBound + "]"; }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is float3box box && Equals(box);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(float3box other)
    {
      return LowerBound.Equals(other.LowerBound) &&
             UpperBound.Equals(other.UpperBound);
    }

    /// <summary>Generates the hash code for this object</summary>
    public override int GetHashCode()
    {
      int hashCode = 1772944719;
      hashCode = hashCode * -1521134295 + LowerBound.GetHashCode();
      hashCode = hashCode * -1521134295 + UpperBound.GetHashCode();
      return hashCode;
    }
    
    /// <summary>Determines if two float3box values are equal</summary>
    public static bool operator ==(float3box v1, float3box v2)
    {
      return v1.LowerBound == v2.LowerBound && v1.UpperBound == v2.UpperBound;
    }

    /// <summary>Determines if two float3box values are not equal</summary>
    public static bool operator !=(float3box v1, float3box v2)
    {
      return v1.LowerBound != v2.LowerBound || v1.UpperBound != v2.UpperBound;
    }

    /// <summary>Returns the size of the bounding box</summary>
    public float3 Size { get { return UpperBound - LowerBound; } }

    /// <summary>Returns the center coordinate of the bounding box</summary>
    public float3 Center { get { return (UpperBound + LowerBound) * 0.5f; } }

    /// <summary>Returns the equivalent bounding radius</summary>
    public float Radius { get { return float3.Distance(UpperBound, LowerBound) * 0.5f; } }

    /// <summary>Returns a new bounding box that includes the given point</summary>
    public void AddPoint(float3 point)
    {
      LowerBound = LowerBound.Min(point);
      UpperBound = UpperBound.Max(point);
    }

    /// <summary>Returns a new bounding box that includes the given box</summary>
    public void AddBox(float3box Box)
    {
      LowerBound = LowerBound.Min(Box.UpperBound);
      UpperBound = UpperBound.Max(Box.UpperBound);
    }

    /// <summary>Returns a point clamped within the bounding box</summary>
    public float3 ClampPoint(float3 point)
    {
      return new float3(
        point.x.Clamp(LowerBound.x, UpperBound.x),
        point.y.Clamp(LowerBound.y, UpperBound.y),
        point.z.Clamp(LowerBound.z, UpperBound.z));
    }

    /// <summary>Performs a transfromation on the bounding box</summary>
    public void Transform(float4x4 TranformMatrix)
    {
      float3 lb = LowerBound;
      float3 ub = UpperBound;
      // clear the current bounds
      LowerBound = default;
      UpperBound = default;
      float3 v = lb;
      // simulate the corners of a cube
      AddPoint(TranformMatrix * v);
      v.z = ub.z;
      AddPoint(TranformMatrix * v);
      v.y = ub.y;
      AddPoint(TranformMatrix * v);
      v.z = lb.z;
      AddPoint(TranformMatrix * v);
      v.x = ub.x;
      AddPoint(TranformMatrix * v);
      v.z = ub.z;
      AddPoint(TranformMatrix * v);
      v.y = lb.y;
      AddPoint(TranformMatrix * v);
      v.z = lb.z;
      AddPoint(TranformMatrix * v);
    }

    /// <summary>Performs a translation transfromation on the bounding box</summary>
    public void Translate(float3 translation)
    {
      LowerBound += translation;
      UpperBound += translation;
    }

    /// <summary>Determines if a point is inside the rectangle represented</summary>
    public bool ContainsPoint(float3 point)
    {
      return point.x.WithinRangeInclusive(LowerBound.x, UpperBound.x) && point.y.WithinRange(LowerBound.y, UpperBound.y);
    }
  }
}
