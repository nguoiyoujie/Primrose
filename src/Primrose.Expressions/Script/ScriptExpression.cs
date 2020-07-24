using Primrose.Expressions.Tree.Expressions;

namespace Primrose.Expressions
{
  /// <summary>
  /// Represents a parsable expression
  /// </summary>
  public partial class ScriptExpression
  {
    private readonly Expression m_expression;
    internal ContextScope Scope;

    /// <summary>Creates a script containing an expression</summary>
    public ScriptExpression(string text)
    {
      int dummy = 0;
      Scope = new ContextScope();
      Parser.Parse(Scope, text, out m_expression, "", ref dummy);
    }

    /// <summary>Evaluates the script</summary>
    /// <param name="context">The script context</param>
    public Val Evaluate(IContext context)
    {
       return m_expression.Evaluate(context);
    }
  }
}
