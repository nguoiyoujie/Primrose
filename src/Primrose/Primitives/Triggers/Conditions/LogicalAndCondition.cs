namespace Primrose.Primitives.Triggers
{
  /// <summary>Represents a condition from the logical AND of two conditions</summary>
  public class LogicalAndCondition : ACondition
  {
    private readonly ICondition m_cond1;
    private readonly ICondition m_cond2;

    /// <summary>Creates a condition from the logical AND of two conditions</summary>
    public LogicalAndCondition(ICondition cond1, ICondition cond2) : base()
    {
      m_cond1 = cond1;
      m_cond2 = cond2;
      m_cond1.Update += (e) => UpdateResult();
      m_cond2.Update += (e) => UpdateResult();
    }

    /// <summary>Evaluates the condition</summary>
    public override bool Evaluate()
    {
      return m_cond1.Evaluate() && m_cond2.Evaluate();
    }
  }
}
