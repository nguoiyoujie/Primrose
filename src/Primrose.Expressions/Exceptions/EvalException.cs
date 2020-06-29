using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Expressions
{
  /// <summary>
  /// Provides script evaluation exceptions
  /// </summary>
  public class EvalException : Exception
  {
    /// <summary>
    /// Represents an exception produced when running the script
    /// </summary>
    /// <param name="eval">The object that produced the exception</param>
    /// <param name="reason">The message of the exception</param>
    public EvalException(ITracker eval, string reason) : base(Resource.Strings.Error_EvalException_4
                                                                                .F(eval.SourceName
                                                                                , eval.LineNumber
                                                                                , eval.Position
                                                                                , reason))
    { }

    /// <summary>
    /// Represents an exception produced when attempting to process an operation during script evaluation
    /// </summary>
    /// <param name="eval">The object that produced the exception</param>
    /// <param name="opname">The operation being attempted</param>
    /// <param name="v">The value in the operation</param>
    /// <param name="ex">The inner exception message</param>
    public EvalException(ITracker eval, string opname, Val v, Exception ex) : base(Resource.Strings.Error_EvalException_6 
                                                                                .F(opname
                                                                                , v.Value?.ToString() ?? Resource.Strings.Null
                                                                                , eval.SourceName
                                                                                , eval.LineNumber
                                                                                , eval.Position
                                                                                , ex.Message))
    { }

    /// <summary>
    /// Represents an exception produced when attempting to process an operation during script evaluation
    /// </summary>
    /// <param name="eval">The object that produced the exception</param>
    /// <param name="opname">The operation being attempted</param>
    /// <param name="v1">The first value in the operation</param>
    /// <param name="v2">The second value in the operation</param>
    /// <param name="ex">The inner exception message</param>
    public EvalException(ITracker eval, string opname, Val v1, Val v2, Exception ex) : base(Resource.Strings.Error_EvalException_7
                                                                                .F(opname
                                                                                , v1.Value?.ToString() ?? Resource.Strings.Null
                                                                                , v2.Value?.ToString() ?? Resource.Strings.Null
                                                                                , eval.SourceName
                                                                                , eval.LineNumber
                                                                                , eval.Position
                                                                                , ex.Message))
    { }

  }
}


