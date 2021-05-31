using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Primrose.Primitives.Extensions
{
  /// <summary>Helper class for referencing empty arrays without allocation</summary>
  /// <typeparam name="T">The element type of the array</typeparam>
  public class Array<T>
  {
    private static readonly T[] empty = new T[0];

    /// <summary>Retrieves a reference to the empty array</summary>
    public static T[] Empty { get { return empty; } }
  }
}
