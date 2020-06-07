using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Primitives
{
  /// <summary>Represents an exception as a result of receiving a value that is not an enumerable</summary>
  public class ExpectedEnumException<T> : InvalidOperationException
  {
    /// <summary>Represents an exception as a result of receiving a value that is not an enumerable</summary>
    public ExpectedEnumException() : base("Type '{0}' is not an enum".F(typeof(T))) { }
  }
}


