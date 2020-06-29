using Primrose.Primitives.Extensions;
using System;
using System.Runtime.Versioning;

namespace Primrose.Expressions
{
  /// <summary>Represents an exception as a result of processing an array with an unexpected format</summary>
  public class ValTypeMismatchException : InvalidOperationException
  {
    /// <summary>
    /// Represents an exception when attempting to assign an array to a ValType
    /// </summary>
    public ValTypeMismatchException(int length, ValType type) : base(Resource.Strings.Error_ValTypeMismatchException_Length.F(length, type)) { }

    /// <summary>
    /// Represents an exception when attempting to assign an array to a ValType
    /// </summary>
    public ValTypeMismatchException(ValType type1, ValType type2) : base(Resource.Strings.Error_ValTypeMismatchException_Type.F(type1, type2)) { }
  }
}


