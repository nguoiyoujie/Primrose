namespace Primrose.Primitives.Triggers
{
  /// <summary>Represents a condition to be fulfilled</summary>
  public abstract class ACondition : ICondition
  {
    private event ConditionUpdateEventHandler _update;

    /// <summary>Gets a value indicating whether the condition is currently met</summary>
    public bool Result { get { return _result; } }
    private bool _result;

    /// <summary>Updates the result and triggers any event based on the result</summary>
    protected void UpdateResult()
    {
      _result = Evaluate();
      OnUpdate(Result);
    }

    /// <summary>Evaluates the condition</summary>
    public abstract bool Evaluate();

    /// <summary>Occurs when the condition result is updated</summary>
    public event ConditionUpdateEventHandler Update { add { _update += value; } remove { _update -= value; } }

    /// <summary>Raises the Update event</summary>
    protected void OnUpdate(bool fulfilled)
    {
      _update?.Invoke(new ConditionUpdateEventArgs(this, fulfilled));
    }

    /// <summary>Creates a new condition as a logical AND with another condition</summary>
    public ICondition And(ICondition other)
    {
      return new LogicalAndCondition(this, other);
    }

    /// <summary>Creates a new condition as a logical OR with another condition</summary>
    public ICondition Or(ICondition other)
    {
      return new LogicalOrCondition(this, other);
    }

    /// <summary>Creates a new condition as a logical negation of itself</summary>
    public ICondition Not()
    {
      return new LogicalNotCondition(this);
    }
  }
}
