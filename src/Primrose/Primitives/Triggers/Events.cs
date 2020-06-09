namespace Primrose.Primitives.Triggers
{
  /// <summary>An event delegate for handling condition updates</summary>
  /// <param name="args"></param>
  public delegate void ConditionUpdateEventHandler(ConditionUpdateEventArgs args);

  /// <summary>Represents a condition update</summary>
  public struct ConditionUpdateEventArgs
  {
    /// <summary>The condition being updated</summary>
    public readonly ICondition Condition;

    /// <summary>Indicates whether the condition is currently met</summary>
    public readonly bool Fulfilled;

    /// <summary>Represents a condition update</summary>
    public ConditionUpdateEventArgs(ICondition condition, bool fulfilled) { Condition = condition; Fulfilled = fulfilled; }
  }
}
