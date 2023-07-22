using System;

namespace Primrose.Primitives.Stocks
{
  /// <summary>This class contains prebuilt functions to assist in the default creation of flows.</summary>
  public static class FlowFunctionHelper
  {
    /// <summary>This function mimics the check functions for a numeric flow by addition</summary>
    public static readonly Func<Stock<int>, int, FlowResult<int>> CheckFunction_Integer = (s, v) =>
    {
      int nv = s.Value + v;
      if (s.HasMinimum && nv < s.Minimum)
        return new FlowResult<int>(FlowResultType.LOWERBOUNDREACHED, s.Value - s.Minimum, s.Minimum - nv);
      else if (s.HasMaximum && nv > s.Maximum)
        return new FlowResult<int>(FlowResultType.UPPERBOUNDREACHED, s.Maximum - s.Value, nv - s.Maximum);
      else
        return new FlowResult<int>(FlowResultType.NORMAL, v, 0);
    };

    /// <summary>This function mimics the check functions for a numeric flow by addition</summary>
    public static readonly Func<Stock<float>, float, FlowResult<float>> CheckFunction_Float = (s, v) =>
    {
      float nv = s.Value + v;
      if (s.HasMinimum && nv < s.Minimum)
        return new FlowResult<float>(FlowResultType.LOWERBOUNDREACHED, s.Value - s.Minimum, s.Minimum - nv);
      else if (s.HasMaximum && nv > s.Maximum)
        return new FlowResult<float>(FlowResultType.UPPERBOUNDREACHED, s.Maximum - s.Value, nv - s.Maximum);
      else
        return new FlowResult<float>(FlowResultType.NORMAL, v, 0);
    };

    /// <summary>This action mimics the application a numeric flow by addition</summary>
    public static readonly Action<Stock<int>, int> FlowAction_Integer = (s, v) => { s.Value += v; };

    /// <summary>This action mimics the application a numeric flow by addition</summary>
    public static readonly Action<Stock<float>, float> FlowAction_Float = (s, v) => { s.Value += v; };

    /// <summary>Creates a new flow with default checks for numeric values</summary>
    public static Flow<int> GetDefaultFlow_Integer()
    {
      return new Flow<int>
      {
        CheckFunction = CheckFunction_Integer,
        FlowAction = FlowAction_Integer
      };
    }

    /// <summary>Creates a new flow with default checks for numeric values</summary>
    public static Flow<float> GetDefaultFlow_Float()
    {
      return new Flow<float>
      {
        CheckFunction = CheckFunction_Float,
        FlowAction = FlowAction_Float
      };
    }
  }
}
