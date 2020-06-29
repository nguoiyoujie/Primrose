using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Expressions
{
  /// <summary>Represents an exception as a result of performing an invalid cast with a ValType</summary>
  public class InvalidValCastException : InvalidCastException
  {
    /// <summary>
    /// Represents an exception when performing an invalid cast with a ValType
    /// </summary>
    public InvalidValCastException(Type type, ValType valType) : base(Resource.Strings.Error_InvalidValCastException.F(type.Name, valType)) { }
  }
}


