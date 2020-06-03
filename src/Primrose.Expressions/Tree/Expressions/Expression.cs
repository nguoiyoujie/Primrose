using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class Expression : CExpression
  {
    private CExpression _expr;

    internal Expression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      _expr = new TernaryExpression(scope, lexer).Get();
    }

    public override CExpression Get()
    {
      return _expr;
    }

    public override Val Evaluate(IContext context)
    {
      return _expr.Evaluate(context);
    }

    public override void Write(StringBuilder sb)
    {
      _expr.Write(sb);
    }
  }
}
