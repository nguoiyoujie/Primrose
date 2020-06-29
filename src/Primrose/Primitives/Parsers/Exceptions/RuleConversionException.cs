using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Primitives.Parsers
{
  /// <summary>Defines an exception for errors encountered in parsing and writing conversions</summary>
  public class RuleConversionException : Exception
  {
    /// <summary>Defines an exception for errors encountered in parsing and writing conversions</summary>
    /// <param name="t">The type of the attempted parse</param>
    /// <param name="value">The string value that failed the parse</param>
    public RuleConversionException(Type t, string value) : base(Resource.Strings.Error_RuleConversionException.F(value, t)) { }
  }
}
