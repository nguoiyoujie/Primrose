using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>Represents a plane surface on three float dimensions</summary>
  public struct float3plane : IEquatable<float3plane>
  {
		/// <summary>Denotes the side of a plane, used as a return value for intersection evaluations</summary>
		public enum PlaneSide
		{
			/// <summary>Represents the back side of a plane</summary>
			Negative,

			/// <summary>Represents the front side of a plane</summary>
			Positive,

			/// <summary>Represents both sides of a plane</summary>
			Both
		}

		/// <summary>The distance of the plane from the origin</summary>
		public float D;

		/// <summary>The normal vector of the plane</summary>
		public float3 Normal;

		/// <summary>
		/// Creates a float3plane value
		/// </summary>
		/// <param name="d"></param>
		/// <param name="normal"></param>
		public float3plane(float d, float3 normal) : this()
    {
      this.D = d;
      this.Normal = normal;
    }

		/// <summary>
		/// Creates a float3plane value
		/// </summary>
		public float3plane(float3 p0, float3 p1, float3 p2) : this()
		{
			FromPoints(p0, p1, p2);
		}

		/// <summary>
		/// Creates a float3plane value
		/// </summary>
		public float3plane(float nx, float ny, float nz, float d)
		{
			Normal = new float3(nx, ny, nz);
			D = d;
		}

		private void FromPoints(float3 p0, float3 p1, float3 p2)
		{
			float3 v = p1 - p0;
			float3 v2 = p2 - p0;
			Normal = float3.Cross(v, v2).Normalize();
			D = float3.Dot(-Normal, p0);
		}

		/// <summary>Returns the string representation of this value</summary>
		public override string ToString() { return "{" + Normal + " |" + D + "}"; }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="obj">The object to compare for equality</param>
    public override bool Equals(object obj)
    {
      return obj is float3plane box && Equals(box);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(float3plane other)
    {
      return D.Equals(other.D) &&
						 Normal.Equals(other.Normal);
    }

    /// <summary>Generates the hash code for this object</summary>
    public override int GetHashCode()
    {
      int hashCode = 1772944719;
      hashCode = hashCode * -1521134295 + D.GetHashCode();
      hashCode = hashCode * -1521134295 + Normal.GetHashCode();
      return hashCode;
    }

		/// <summary>Determines if two float3plane values are equal</summary>
		public static bool operator ==(float3plane v1, float3plane v2)
		{
			return v1.Normal == v2.Normal && v1.D == v2.D;
		}

		/// <summary>Determines if two float3plane values are not equal</summary>
		public static bool operator !=(float3plane v1, float3plane v2)
		{
			return v1.Normal != v2.Normal || v1.D != v2.D;
		}

		/// <summary>Determines if the plane intersects an axis-aligned bounding box</summary>
		public PlaneSide Intersects(float3box box)
		{
			float d = DistanceFromPoint(box.Center);
			float r = Math.Abs(float3.Dot(Normal, box.Size * 0.5f));
			if (d < -r)
			{
				return PlaneSide.Negative;
			}
			if (d > r)
			{
				return PlaneSide.Positive;
			}
			return PlaneSide.Both;
		}

		/// <summary>Determines the normal distance of a point from the plane</summary>
		public float DistanceFromPoint(float3 point)
		{
			return float3.Dot(Normal, point) + D;
		}
	}
}
