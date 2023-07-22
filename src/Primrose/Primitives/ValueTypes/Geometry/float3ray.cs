using System;

namespace Primrose.Primitives.ValueTypes
{
  /// <summary>Represents a ray on three float dimensions</summary>
  public struct float3ray : IEquatable<float3ray>
  {
    // Records the direction of the ray relative to the orthogonal axes
    private enum RayDirType_3 : byte
		{
      MMM,
      MMP,
      MPM,
      MPP,
      PMM,
      PMP,
      PPM,
      PPP,
      POO,
      MOO,
      OPO,
      OMO,
      OOP,
      OOM,
      OMM,
      OMP,
      OPM,
      OPP,
      MOM,
      MOP,
      POM,
      POP,
      MMO,
      MPO,
      PMO,
      PPO
    }

		/// <summary>The coordinates of the origin</summary>
		public float3 Origin
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

		/// <summary>The direction vector</summary>
		public float3 Direction
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
		private float3 _origin;
		private float3 _direction;
		private float3 _inverse;
		private RayDirType_3 _dirtype;
		private float _ibyj;
		private float _jbyi;
		private float _kbyj;
		private float _jbyk;
		private float _ibyk;
		private float _kbyi;
		private float _c_xy;
		private float _c_xz;
		private float _c_yx;
		private float _c_yz;
		private float _c_zx;
		private float _c_zy;
		private bool _dirty;

		/// <summary>
		/// Creates a float3ray value
		/// </summary>
		/// <param name="origin"></param>
		/// <param name="direction"></param>
		public float3ray(float3 origin, float3 direction) : this()
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
      return obj is float3ray box && Equals(box);
    }

    /// <summary>Returns true if the value of another object is equal to this object</summary>
    /// <param name="other">The object to compare for equality</param>
    public bool Equals(float3ray other)
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

		/// <summary>Determines if two float3ray values are equal</summary>
		public static bool operator ==(float3ray v1, float3ray v2)
		{
			return v1.Origin == v2.Origin && v1.Direction == v2.Direction;
		}

		/// <summary>Determines if two float3ray values are not equal</summary>
		public static bool operator !=(float3ray v1, float3ray v2)
		{
			return v1.Origin != v2.Origin || v1.Direction != v2.Direction;
		}

		/// <summary>Returns the vector of the ray</summary>
		public float3 Vector { get { return Direction - Origin; } }

    /// <summary>Returns the length of the ray</summary>
    public float Length { get { return Direction.Length; } }

		/// <summary>Returns whether the ray intersects with a bounding box</summary>
		public bool Intersects(float3box b)
		{
			if (_dirty)
			{
				UpdateInternals();
			}
			switch (_dirtype)
			{
				case RayDirType_3.MMM:
					return _origin.x >= b.LowerBound.x && _origin.y >= b.LowerBound.y && _origin.z >= b.LowerBound.z && _jbyi * b.LowerBound.x - b.UpperBound.y + _c_xy <= 0f && _ibyj * b.LowerBound.y - b.UpperBound.x + _c_yx <= 0f && _jbyk * b.LowerBound.z - b.UpperBound.y + _c_zy <= 0f && _kbyj * b.LowerBound.y - b.UpperBound.z + _c_yz <= 0f && _kbyi * b.LowerBound.x - b.UpperBound.z + _c_xz <= 0f && _ibyk * b.LowerBound.z - b.UpperBound.x + _c_zx <= 0f;
				case RayDirType_3.MMP:
					return _origin.x >= b.LowerBound.x && _origin.y >= b.LowerBound.y && _origin.z <= b.UpperBound.z && _jbyi * b.LowerBound.x - b.UpperBound.y + _c_xy <= 0f && _ibyj * b.LowerBound.y - b.UpperBound.x + _c_yx <= 0f && _jbyk * b.UpperBound.z - b.UpperBound.y + _c_zy <= 0f && _kbyj * b.LowerBound.y - b.LowerBound.z + _c_yz >= 0f && _kbyi * b.LowerBound.x - b.LowerBound.z + _c_xz >= 0f && _ibyk * b.UpperBound.z - b.UpperBound.x + _c_zx <= 0f;
				case RayDirType_3.MPM:
					return _origin.x >= b.LowerBound.x && _origin.y <= b.UpperBound.y && _origin.z >= b.LowerBound.z && _jbyi * b.LowerBound.x - b.LowerBound.y + _c_xy >= 0f && _ibyj * b.UpperBound.y - b.UpperBound.x + _c_yx <= 0f && _jbyk * b.LowerBound.z - b.LowerBound.y + _c_zy >= 0f && _kbyj * b.UpperBound.y - b.UpperBound.z + _c_yz <= 0f && _kbyi * b.LowerBound.x - b.UpperBound.z + _c_xz <= 0f && _ibyk * b.LowerBound.z - b.UpperBound.x + _c_zx <= 0f;
				case RayDirType_3.MPP:
					return _origin.x >= b.LowerBound.x && _origin.y <= b.UpperBound.y && _origin.z <= b.UpperBound.z && _jbyi * b.LowerBound.x - b.LowerBound.y + _c_xy >= 0f && _ibyj * b.UpperBound.y - b.UpperBound.x + _c_yx <= 0f && _jbyk * b.UpperBound.z - b.LowerBound.y + _c_zy >= 0f && _kbyj * b.UpperBound.y - b.LowerBound.z + _c_yz >= 0f && _kbyi * b.LowerBound.x - b.LowerBound.z + _c_xz >= 0f && _ibyk * b.UpperBound.z - b.UpperBound.x + _c_zx <= 0f;
				case RayDirType_3.PMM:
					return _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.z >= b.LowerBound.z && _jbyi * b.UpperBound.x - b.UpperBound.y + _c_xy <= 0f && _ibyj * b.LowerBound.y - b.LowerBound.x + _c_yx >= 0f && _jbyk * b.LowerBound.z - b.UpperBound.y + _c_zy <= 0f && _kbyj * b.LowerBound.y - b.UpperBound.z + _c_yz <= 0f && _kbyi * b.UpperBound.x - b.UpperBound.z + _c_xz <= 0f && _ibyk * b.LowerBound.z - b.LowerBound.x + _c_zx >= 0f;
				case RayDirType_3.PMP:
					return _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.z <= b.UpperBound.z && _jbyi * b.UpperBound.x - b.UpperBound.y + _c_xy <= 0f && _ibyj * b.LowerBound.y - b.LowerBound.x + _c_yx >= 0f && _jbyk * b.UpperBound.z - b.UpperBound.y + _c_zy <= 0f && _kbyj * b.LowerBound.y - b.LowerBound.z + _c_yz >= 0f && _kbyi * b.UpperBound.x - b.LowerBound.z + _c_xz >= 0f && _ibyk * b.UpperBound.z - b.LowerBound.x + _c_zx >= 0f;
				case RayDirType_3.PPM:
					return _origin.x <= b.UpperBound.x && _origin.y <= b.UpperBound.y && _origin.z >= b.LowerBound.z && _jbyi * b.UpperBound.x - b.LowerBound.y + _c_xy >= 0f && _ibyj * b.UpperBound.y - b.LowerBound.x + _c_yx >= 0f && _jbyk * b.LowerBound.z - b.LowerBound.y + _c_zy >= 0f && _kbyj * b.UpperBound.y - b.UpperBound.z + _c_yz <= 0f && _kbyi * b.UpperBound.x - b.UpperBound.z + _c_xz <= 0f && _ibyk * b.LowerBound.z - b.LowerBound.x + _c_zx >= 0f;
				case RayDirType_3.PPP:
					return _origin.x <= b.UpperBound.x && _origin.y <= b.UpperBound.y && _origin.z <= b.UpperBound.z && _jbyi * b.UpperBound.x - b.LowerBound.y + _c_xy >= 0f && _ibyj * b.UpperBound.y - b.LowerBound.x + _c_yx >= 0f && _jbyk * b.UpperBound.z - b.LowerBound.y + _c_zy >= 0f && _kbyj * b.UpperBound.y - b.LowerBound.z + _c_yz >= 0f && _kbyi * b.UpperBound.x - b.LowerBound.z + _c_xz >= 0f && _ibyk * b.UpperBound.z - b.LowerBound.x + _c_zx >= 0f;
				case RayDirType_3.POO:
					return _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y && _origin.z >= b.LowerBound.z && _origin.z <= b.UpperBound.z;
				case RayDirType_3.MOO:
					return _origin.x >= b.LowerBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y && _origin.z >= b.LowerBound.z && _origin.z <= b.UpperBound.z;
				case RayDirType_3.OPO:
					return _origin.y <= b.UpperBound.y && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.z >= b.LowerBound.z && _origin.z <= b.UpperBound.z && _origin.z >= b.LowerBound.z && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y && _origin.z <= b.UpperBound.z && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y;
				case RayDirType_3.OMO:
					return _origin.y >= b.LowerBound.y && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.z >= b.LowerBound.z && _origin.z <= b.UpperBound.z && _origin.y <= b.UpperBound.y && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.z >= b.LowerBound.z && _origin.z <= b.UpperBound.z && _origin.z >= b.LowerBound.z && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y && _origin.z <= b.UpperBound.z && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y;
				case RayDirType_3.OOP:
					return _origin.z <= b.UpperBound.z && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y;
				case RayDirType_3.OOM:
					return _origin.z >= b.LowerBound.z && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y && _origin.z <= b.UpperBound.z && _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y;
				case RayDirType_3.OMM:
					return _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.z >= b.LowerBound.z && _jbyk * b.LowerBound.z - b.UpperBound.y + _c_zy <= 0f && _kbyj * b.LowerBound.y - b.UpperBound.z + _c_yz <= 0f;
				case RayDirType_3.OMP:
					return _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _origin.z <= b.UpperBound.z && _jbyk * b.UpperBound.z - b.UpperBound.y + _c_zy <= 0f && _kbyj * b.LowerBound.y - b.LowerBound.z + _c_yz >= 0f;
				case RayDirType_3.OPM:
					return _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y <= b.UpperBound.y && _origin.z >= b.LowerBound.z && _jbyk * b.LowerBound.z - b.LowerBound.y + _c_zy >= 0f && _kbyj * b.UpperBound.y - b.UpperBound.z + _c_yz <= 0f;
				case RayDirType_3.OPP:
					return _origin.x >= b.LowerBound.x && _origin.x <= b.UpperBound.x && _origin.y <= b.UpperBound.y && _origin.z <= b.UpperBound.z && _jbyk * b.UpperBound.z - b.LowerBound.y + _c_zy >= 0f && _kbyj * b.UpperBound.y - b.LowerBound.z + _c_yz >= 0f;
				case RayDirType_3.MOM:
					return _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y && _origin.x >= b.LowerBound.x && _origin.z >= b.LowerBound.z && _kbyi * b.LowerBound.x - b.UpperBound.z + _c_xz <= 0f && _ibyk * b.LowerBound.z - b.UpperBound.x + _c_zx <= 0f;
				case RayDirType_3.MOP:
					return _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y && _origin.x >= b.LowerBound.x && _origin.z <= b.UpperBound.z && _kbyi * b.LowerBound.x - b.LowerBound.z + _c_xz >= 0f && _ibyk * b.UpperBound.z - b.UpperBound.x + _c_zx <= 0f;
				case RayDirType_3.POM:
					return _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y && _origin.x <= b.UpperBound.x && _origin.z >= b.LowerBound.z && _kbyi * b.UpperBound.x - b.UpperBound.z + _c_xz <= 0f && _ibyk * b.LowerBound.z - b.LowerBound.x + _c_zx >= 0f;
				case RayDirType_3.POP:
					return _origin.y >= b.LowerBound.y && _origin.y <= b.UpperBound.y && _origin.x <= b.UpperBound.x && _origin.z <= b.UpperBound.z && _kbyi * b.UpperBound.x - b.LowerBound.z + _c_xz >= 0f && _ibyk * b.UpperBound.z - b.LowerBound.x + _c_zx >= 0f;
				case RayDirType_3.MMO:
					return _origin.z >= b.LowerBound.z && _origin.z <= b.UpperBound.z && _origin.x >= b.LowerBound.x && _origin.y >= b.LowerBound.y && _jbyi * b.LowerBound.x - b.UpperBound.y + _c_xy <= 0f && _ibyj * b.LowerBound.y - b.UpperBound.x + _c_yx <= 0f;
				case RayDirType_3.MPO:
					return _origin.z >= b.LowerBound.z && _origin.z <= b.UpperBound.z && _origin.x >= b.LowerBound.x && _origin.y <= b.UpperBound.y && _jbyi * b.LowerBound.x - b.LowerBound.y + _c_xy >= 0f && _ibyj * b.UpperBound.y - b.UpperBound.x + _c_yx <= 0f;
				case RayDirType_3.PMO:
					return _origin.z >= b.LowerBound.z && _origin.z <= b.UpperBound.z && _origin.x <= b.UpperBound.x && _origin.y >= b.LowerBound.y && _jbyi * b.UpperBound.x - b.UpperBound.y + _c_xy <= 0f && _ibyj * b.LowerBound.y - b.LowerBound.x + _c_yx >= 0f;
				case RayDirType_3.PPO:
					return _origin.z >= b.LowerBound.z && _origin.z <= b.UpperBound.z && _origin.x <= b.UpperBound.x && _origin.y <= b.UpperBound.y && _jbyi * b.UpperBound.x - b.LowerBound.y + _c_xy >= 0f && _ibyj * b.UpperBound.y - b.LowerBound.x + _c_yx >= 0f;
				default:
					return false;
			}
		}

		/// <summary>Returns whether the ray intersects with a bounding box</summary>
		public bool Intersects(float3obox b)
		{
			// apply inverse transform on the point, then check against the AABB bounds
			float4x4 m = b.Transform.Inverse();
			float3ray ray = new float3ray(Origin * m, Direction * m);
			return ray.Intersects(b.Box);
		}

		private void UpdateInternals()
		{
			_inverse = new float3(1f / _direction.x, 1f / _direction.y, 1f / _direction.z);
			_ibyj = _direction.x * _inverse.y;
			_jbyi = _direction.y * _inverse.x;
			_jbyk = _direction.y * _inverse.z;
			_kbyj = _direction.z * _inverse.y;
			_ibyk = _direction.x * _inverse.z;
			_kbyi = _direction.z * _inverse.x;
			_c_xy = _origin.y - _jbyi * _origin.x;
			_c_xz = _origin.z - _kbyi * _origin.x;
			_c_yx = _origin.x - _ibyj * _origin.y;
			_c_yz = _origin.z - _kbyj * _origin.y;
			_c_zx = _origin.x - _ibyk * _origin.z;
			_c_zy = _origin.y - _jbyk * _origin.z;
			if (_direction.x < 0f)
			{
				if (_direction.y < 0f)
				{
					if (_direction.z < 0f)
					{
						_dirtype = RayDirType_3.MMM;
					}
					else if (_direction.z > 0f)
					{
						_dirtype = RayDirType_3.MMP;
					}
					else
					{
						_dirtype = RayDirType_3.MMO;
					}
				}
				else if (_direction.z < 0f)
				{
					_dirtype = RayDirType_3.MPM;
					if (_direction.y == 0f)
					{
						_dirtype = RayDirType_3.MOM;
					}
				}
				else if (_direction.y == 0f && _direction.z == 0f)
				{
					_dirtype = RayDirType_3.MOO;
				}
				else if (_direction.z == 0f)
				{
					_dirtype = RayDirType_3.MPO;
				}
				else if (_direction.y == 0f)
				{
					_dirtype = RayDirType_3.MOP;
				}
				else
				{
					_dirtype = RayDirType_3.MPP;
				}
			}
			else if (_direction.y < 0f)
			{
				if (_direction.z < 0f)
				{
					_dirtype = RayDirType_3.PMM;
					if (_direction.x == 0f)
					{
						_dirtype = RayDirType_3.OMM;
					}
				}
				else if (_direction.x == 0f && _direction.z == 0f)
				{
					_dirtype = RayDirType_3.OMO;
				}
				else if (_direction.z == 0f)
				{
					_dirtype = RayDirType_3.PMO;
				}
				else if (_direction.x == 0f)
				{
					_dirtype = RayDirType_3.OMP;
				}
				else
				{
					_dirtype = RayDirType_3.PMP;
				}
			}
			else if (_direction.z < 0f)
			{
				if (_direction.x == 0f && _direction.y == 0f)
				{
					_dirtype = RayDirType_3.OOM;
				}
				else if (_direction.x == 0f)
				{
					_dirtype = RayDirType_3.OPM;
				}
				else if (_direction.y == 0f)
				{
					_dirtype = RayDirType_3.POM;
				}
				else
				{
					_dirtype = RayDirType_3.PPM;
				}
			}
			else if (_direction.x == 0f)
			{
				if (_direction.y == 0f)
				{
					_dirtype = RayDirType_3.OOP;
				}
				else if (_direction.z == 0f)
				{
					_dirtype = RayDirType_3.OPO;
				}
				else
				{
					_dirtype = RayDirType_3.OPP;
				}
			}
			else if (_direction.y == 0f && _direction.z == 0f)
			{
				_dirtype = RayDirType_3.POO;
			}
			else if (_direction.y == 0f)
			{
				_dirtype = RayDirType_3.POP;
			}
			else if (_direction.z == 0f)
			{
				_dirtype = RayDirType_3.PPO;
			}
			else
			{
				_dirtype = RayDirType_3.PPP;
			}
			_dirty = false;
		}
	}
}
