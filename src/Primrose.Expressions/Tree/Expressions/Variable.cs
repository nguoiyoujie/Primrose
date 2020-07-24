using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class Variable : CExpression
  {
    private readonly ContextScope _scope;
    public string varName { get; protected set; }

    internal Variable(ContextScope scope, Lexer lexer, int skip) : base(scope, lexer) { }

    internal Variable(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      varName = lexer.TokenContents;
      _scope = scope;
      lexer.Next(); // VARIABLE
    }

    public override Val Evaluate(IContext context)
    {
      return _scope.GetVar(this, varName);
    }

    public override void Write(StringBuilder sb)
    {
      sb.Append(varName);
    }
  }
}