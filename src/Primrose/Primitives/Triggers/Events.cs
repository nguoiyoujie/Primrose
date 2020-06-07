namespace Primrose.Primitives.Triggers
{
  public delegate void ConditionUpdateEventHandler(ConditionUpdateEventArgs args);

  public struct ConditionUpdateEventArgs
  {
    public readonly ICondition Condition;
    public readonly bool Fulfilled;
    public ConditionUpdateEventArgs(ICondition condition, bool fulfilled) { Condition = condition; Fulfilled = fulfilled; }
  }
}
