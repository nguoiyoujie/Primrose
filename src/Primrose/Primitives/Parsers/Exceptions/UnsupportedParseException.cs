using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Primitives.Parsers
{
  /// <summary>Defines an exception for errors encountered by an attempt to parse to an unsupported type</summary>
  /// <typeparam name="T">The unsupported type</typeparam>
  public class UnsupportedParseException<T> : InvalidOperationException
  {
    /// <summary>Defines an exception for errors encountered by an attempt to parse to an unsupported type</summary>
    public UnsupportedParseException() : base(Properties.Resources.Error_UnsupportedParseException.F(typeof(T).Name)) { }
  }
}
