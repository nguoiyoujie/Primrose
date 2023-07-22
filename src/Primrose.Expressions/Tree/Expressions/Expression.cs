using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class Expression : CExpression
  {
    private readonly CExpression _expr;

    internal Expression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      _expr = GetNext(scope, lexer);
    }

    public override CExpression Get()
    {
      return _expr.Get() ?? this;
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
