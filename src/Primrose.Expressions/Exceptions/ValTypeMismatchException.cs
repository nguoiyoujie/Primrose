using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Expressions
{
  /// <summary>Represents an exception as a result of processing an array with an unexpected format</summary>
  public class ValTypeMismatchException : InvalidOperationException
  {
    /// <summary>
    /// Represents an exception when attempting to assign an array to a ValType
    /// </summary>
    public ValTypeMismatchException(int length, ValType type) : base("Attempted assignment of an array of length {0} to {1}".F(length, type)) { }

    /// <summary>
    /// Represents an exception when attempting to assign an array to a ValType
    /// </summary>
    public ValTypeMismatchException(ValType type1, ValType type2) : base("Attempted assignment of value of type '{0}' to {1}".F(type1, type2)) { }
  }
}


