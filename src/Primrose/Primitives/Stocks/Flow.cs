using System;

namespace Primrose.Primitives.Stocks
{
  /// <summary>The result types of a flow check against the stock</summary>
  public enum FlowResultType
  {
    /// <summary>The result is normal</summary>
    NORMAL,

    /// <summary>The result is abnormal and should not be applied</summary>
    INVALID,

    /// <summary>The result is normal and the lower bound of the stock value is reached</summary>
    LOWERBOUNDREACHED,

    /// <summary>The result is normal and the upper bound of the stock value is reached</summary>
    UPPERBOUNDREACHED
  }

  /// <summary>The result of a flow check against the stock</summary>
  public struct FlowResult<T>
  {
    /// <summary>The result type that determines further operations</summary>
    public FlowResultType Result;

    /// <summary>The value of the flow that can be admitted into the stock</summary>
    public T Normal;

    /// <summary>The value of the flow that cannot be admitted into the stock, either limited by lower bound or upper bonds on the stock</summary>
    public T Discrepancy;

    /// <summary>The result of a flow check against the stock</summary>
    public FlowResult(FlowResultType result, T normal, T discrepancy) : this()
    {
      Result = result;
      Normal = normal;
      Discrepancy = discrepancy;
    }
  }

  /// <summary>Defines an input or output flow relative to a stock container.</summary>
  public class Flow<T> where T : struct, IComparable<T>
  {
    /// <summary>Defines the function that determines the flow value</summary>
    public Func<T> FlowValueFunction { get; set; }

    /// <summary>Defines the function that checks the application of the flow to the stock</summary>
    public Func<Stock<T>, T, FlowResult<T>> CheckFunction { get; set; } // Stock + Flow = result

    /// <summary>Defines the action to take when applying the flow to the stock</summary>
    public Action<Stock<T>, T> FlowAction { get; set; } // Stock + Flow

    /// <summary>Defines the action to take for any flow value beyond the upper bound</summary>
    public Action<T> UpperBoundReachedAction { get; set; }

    /// <summary>Defines the action to take for any flow value beyond the lower bound</summary>
    public Action<T> LowerBoundReachedAction { get; set; }

    /// <summary>Performs one round of execution for the flow</summary>
    /// <param name="stock">The stock affected by the flow</param>
    /// <returns>The result of evaluating the flow</returns>
    public FlowResult<T> Execute(Stock<T> stock)
    {
      if (stock == null || CheckFunction == null)
      {
        return new FlowResult<T>(FlowResultType.INVALID, default, default);
      }

      T flowValue = FlowValueFunction != null ? FlowValueFunction() : default;
      FlowResult<T> check = CheckFunction(stock, flowValue);

      switch (check.Result)
      {
        case FlowResultType.NORMAL:
          if (FlowAction != null)
          {
            FlowAction(stock, check.Normal);
          }
          break;
        case FlowResultType.INVALID:
          // do nothing
          break;
        case FlowResultType.LOWERBOUNDREACHED:
          if (FlowAction != null)
          {
            FlowAction(stock, check.Normal);
          }
          LowerBoundReachedAction(check.Discrepancy);
          break;
        case FlowResultType.UPPERBOUNDREACHED:
          if (FlowAction != null)
          {
            FlowAction(stock, check.Normal);
          }
          UpperBoundReachedAction(check.Discrepancy);
          break;
      }

      return check;
    }
  }
}
