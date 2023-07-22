using System;

namespace Primrose.Primitives.ValueTypes
{
	/// <summary>Represents a ray on two float dimensions</summary>
	public struct float2ray : IEquatable<float2ray>
	{
		// Records the direction of the ray relative to the orthogonal axes
		private enum RayDirType_2 : byte
		{
			MM,
			MP,
			PM,
			PP,
			PO,
			MO,
			OP,
			OM,
			OO,
		}

		/// <summary>The coordinates of the origin</summary>
		public float2 Origin
		{
			get
			{
				return _origin;
			}
			set
			{
				_origin = value;
				_dirty = true;
			}
		}

		/// <summary>The coordinates of the Direction</summary>
		public float2 Direction
		{
			get
			{
				return _direction;
			}
			set
			{
				_direction = value;
				_dirty = true;
			}
		}

		// cached values
		private float2 _origin;
		private float2 _direction;
		private float2 _inverse;
		private RayDirType_2 _dirtype;
		private float _ibyj;
		private float _jbyi;
		private float _c_xy;
		private float _c_yx;
		private bool _dirty;

		/// <summary>
		/// Creates a float2ray value
		/// </summary>
		/// <param name="origin"></param>
		/// <param name="direction"></param>
		public float2ray(float2 origin, float2 direction) : this()
		{
			this.Origin = origin;
			this.Direction = direction;
			UpdateInternals();
		}

		/// <summary>Returns the string representation of this value</summary>
		public override string ToString() { return "{" + Origin + " +" + Direction + "}"; }

		/// <summary>Returns true if the value of another object is equal to this object</summary>
		/// <param name="obj">The object to compare for equality</param>
		public override bool Equals(object obj)
		{
			return obj is float2ray box && Equals(box);
		}

		/// <summary>Returns true if the value of another object is equal to this object</summary>
		/// <param name="other">The object to compare for equality</param>
		public bool Equals(float2ray other)
		{
			return Origin.Equals(other.Origin) &&
						 Direction.Equals(other.Direction);
		}

		/// <summary>Generates the hash code for this object</summary>
		public override int GetHashCode()
		{
			int hashCode = 1772944719;
			hashCode = hashCode * -1521134295 + Origin.GetHashCode();
			hashCode = hashCode * -1521134295 + Direction.GetHashCode();
			return hashCode;
		}

		/// <summary>Determines if two float2ray values are equal</summary>
		public static bool operator ==(float2ray v1, float2ray v2)
		{
			return v1.Origin == v2.Origin && v1.Direction == v2.Direction;
		}

		/// <summary>Determines if two float2ray values are not equal</summary>
		public static bool operator !=(float2ray v1, float2ray v2)
		{
			return v1.Origin != v2.Origin || v1.Direction != v2.Direction;
		}

		/// <summary>Returns the vector of the ray</summary>
		public float2 Vector { get { return Direction - Origin; } }

		/// <summary>Returns the length of the ray</summary>
		public float Length { get { return Direction.Length; } }

		/// <summary>Returns whether the ray intersects with a bounding box</summary>
		public bool Intersects(float2box b)
		{
			if (_dirty)
			{
				UpdateInternals();
			}
			switch (_dirtype)
			{
				case RayDirType_2.MM:
					return _origin.x >= b.LowerBound.x && _origin.y >= b.LowerBound.y && _jbyi * b.LowerBound.x - b.UpperBound.y + _c_xy <= 0f && _ibyj * b.LowerBound.y - b.UpperBound.x + _c_yx <= 0f;
				case RayDirType_2.MP:
					return _origin.x >= b.LowerBound.x && _origin.y <= b.UpperBound.y && _jbyi * b.LowerBound.x - b.LowerBound.y + _c_xy >= 0f && _ibyj * b.UpperBound.y - b.UpperBound.x + _c_yx <= 0f;
				case RayDirType_2.PM:
					return _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _jbyi * b.UpperBound.x - b.UpperBound.y + _c_xy <= 0f && _ibyj * b.LowerBound.y - b.LowerBound.x + _c_yx >= 0f;
				case RayDirType_2.PP:
					return _origin.x <= b.UpperBound.x && _origin.y <= b.UpperBound.y && _jbyi * b.UpperBound.x - b.LowerBound.y + _c_xy >= 0f && _ibyj * b.UpperBound.y - b.LowerBound.x + _c_yx >= 0f;
				case RayDirType_2.PO:
					return _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y;
				case RayDirType_2.MO:
					return _origin.x >= b.LowerBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y;
				case RayDirType_2.OP:
					return _origin.y <= b.UpperBound.y && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y  && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y;
				case RayDirType_2.OM:
					return _origin.y >= b.LowerBound.y && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y <= b.UpperBound.y && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y;
				case RayDirType_2.OO:
					return _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y;
				default:
					return false;
			}
		}

		/// <summary>Returns whether the ray intersects with a bounding box</summary>
		public bool Intersects(float2obox b)
		{
			// apply inverse transform on the point, then check against the AABB bounds
			float3x3 m = b.Transform.Inverse();
			float2ray ray = new float2ray(Origin * m, Direction * m);
			return ray.Intersects(b.Box);
		}

		private void UpdateInternals()
		{
			_inverse = new float2(1f / _direction.x, 1f / _direction.y);
			_ibyj = _direction.x * _inverse.y;
			_jbyi = _direction.y * _inverse.x;
			_c_xy = _origin.y - _jbyi * _origin.x;
			_c_yx = _origin.x - _ibyj * _origin.y;
			if (_direction.x < 0f)
			{
				if (_direction.y < 0f)
				{
					_dirtype = RayDirType_2.MM;
				}
				else if (_direction.y == 0f)
				{
					_dirtype = RayDirType_2.MO;
				}
				else
				{
					_dirtype = RayDirType_2.MP;
				}
			}
			else if (_direction.x > 0f)
			{
				if (_direction.y < 0f)
				{
					_dirtype = RayDirType_2.PM;
				}
				else if (_direction.y == 0f)
				{
					_dirtype = RayDirType_2.PO;
				}
				else
				{
					_dirtype = RayDirType_2.PP;
				}
			}
			else
			{
				if (_direction.y < 0f)
				{
					_dirtype = RayDirType_2.OM;
				}
				else if (_direction.y == 0f)
				{
					_dirtype = RayDirType_2.OO;
				}
				else
				{
					_dirtype = RayDirType_2.OP;
				}
			}
			_dirty = false;
		}
	}
}
