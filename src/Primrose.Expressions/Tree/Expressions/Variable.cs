using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class Variable : CVariable
  {
    private readonly ContextScope _scope;

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

    public override string ToString()
    {
      return "var " + varName;
    }
  }
}