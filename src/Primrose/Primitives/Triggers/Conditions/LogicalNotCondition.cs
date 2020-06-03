namespace Primrose.Primitives.Triggers
{
  /// <summary>Represents a condition from the logical negation of another condition</summary>
  public class LogicalNotCondition : ACondition
  {
    private ICondition m_cond;

    /// <summary>Creates a condition from the logical negation of another condition</summary>
    public LogicalNotCondition(ICondition cond) : base()
    {
      m_cond = cond;
      m_cond.Update += (e) => UpdateResult();
    }

    /// <summary>Evaluates the condition</summary>
    public override bool Evaluate()
    {
      return !m_cond.Evaluate();
    }
  }
}
