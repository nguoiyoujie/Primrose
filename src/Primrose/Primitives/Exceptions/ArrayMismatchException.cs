using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Primitives
{
  /// <summary>Represents an exception as a result of processing an array with an unexpected format</summary>
  public class ArrayMismatchException : InvalidOperationException
  {
    /// <summary>
    /// Represents an exception in comparing two arrays of different lengths
    /// </summary>
    public ArrayMismatchException() : base("Attempted operation between two arrays of different length") { }

    /// <summary>
    /// Represents an exception in comparing two arrays of different lengths
    /// </summary>
    public ArrayMismatchException(int length1, int length2) : base("Attempted operation between two arrays of different lengths '{0}' and '{1}'".F(length1, length2)) { }

    /// <summary>
    /// Represents an exception when attempting to assign an array to a typed value
    /// </summary>
    public ArrayMismatchException(int length, Type type) : base("Attempted assignment of an array of length {0} to {1}".F(length, type)) { }
  }
}


