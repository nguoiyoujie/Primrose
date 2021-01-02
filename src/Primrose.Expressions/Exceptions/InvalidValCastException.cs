using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Expressions
{
  /// <summary>Represents an exception as a result of performing an invalid cast with a ValType</summary>
  public class InvalidValCastException : InvalidCastException
  {
    /// <summary>
    /// Represents an exception when performing an invalid cast to another Type/>
    /// </summary>
    public InvalidValCastException(Type type, Type target_type) : base(Resource.Strings.Error_InvalidValCastException.F(type.Name, target_type.Name)) { }
  }
}


