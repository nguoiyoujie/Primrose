using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Expressions
{
  /// <summary>Represents an exception as a result of processing an array with an unexpected format</summary>
  public class ValTypeMismatchException : InvalidOperationException
  {
    /// <summary>
    /// Represents an exception when attempting to assign an array to another Type
    /// </summary>
    public ValTypeMismatchException(int length, Type type) : base(Resource.Strings.Error_ValTypeMismatchException_Length.F(length, type.Name)) { }

    /// <summary>
    /// Represents an exception when attempting to assign a Type to another Type
    /// </summary>
    public ValTypeMismatchException(Type type1, Type type2) : base(Resource.Strings.Error_ValTypeMismatchException_Type.F(type1.Name, type2.Name)) { }
  }
}


