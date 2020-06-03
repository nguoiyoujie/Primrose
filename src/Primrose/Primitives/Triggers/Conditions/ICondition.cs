namespace Primrose.Primitives.Triggers
{
  /// <summary>Describes an interface for a condition</summary>
  public interface ICondition
  {
    /// <summary>Occurs when the condition result is updated</summary>
    event ConditionUpdateEventHandler Update;

    /// <summary>Gets a value indicating whether the condition is currently met</summary>
    bool Result { get; }

    /// <summary>Evaluates the condition</summary>
    bool Evaluate();

    /// <summary>Creates a new condition as a logical AND with another condition</summary>
    ICondition And(ICondition conditon);

    /// <summary>Creates a new condition as a logical OR with another condition</summary>
    ICondition Or(ICondition conditon);

    /// <summary>Creates a new condition as a logical negation of itself</summary>
    ICondition Not();
  }
}
