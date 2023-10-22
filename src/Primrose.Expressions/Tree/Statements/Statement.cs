using System.Text;

namespace Primrose.Expressions.Tree.Statements
{
  internal class Statement : CStatement
  {
    private readonly CStatement _statement;

    internal Statement(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // IFTHENELSE (GetNext)

      while (lexer.TokenType == TokenEnum.NOTHING || lexer.TokenType == TokenEnum.COMMENT)
      {
        if (!lexer.Next())
          break;
      }

      _statement = GetNext(scope, lexer);
    }

    public override CStatement Get()
    {
      return _statement.Get();
    }

    public override bool Evaluate(IContext context, ref Val retval)
    {
      return _statement.Evaluate(context, ref retval);
    }

    public override void Write(StringBuilder sb)
    {
      _statement.Write(sb);
    }
  }
}
