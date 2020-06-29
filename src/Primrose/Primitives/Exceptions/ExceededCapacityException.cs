using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Primitives
{
  /// <summary>Represents an exception as a result of pushing in a value into a collection beyond its capacity</summary>
  public class CapacityExceededException<T> : InvalidOperationException
  {
    /// <summary>Represents an exception as a result of pushing in a value into a collection beyond its capacity</summary>
    public CapacityExceededException(int capacity) : base(Resource.Strings.Error_CapacityExceededException.F(typeof(T), capacity)) { }
  }
}


