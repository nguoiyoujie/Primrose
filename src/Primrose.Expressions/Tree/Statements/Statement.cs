using System.Text;

namespace Primrose.Expressions.Tree.Statements
{
  internal class Statement : CStatement
  {
    private readonly CStatement _statement;

    internal Statement(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // IFTHENELSE

      if (lexer.TokenType != TokenEnum.NOTHING && lexer.TokenType != TokenEnum.COMMENT)
      {
        _statement = GetNext(scope, lexer);

        // comment (eliminated by lexer)
        //if (lexer.TokenType == TokenEnum.COMMENT)
        //  lexer.Next();
      }
      else
      {
        lexer.Next();
      }
    }

    public override void Evaluate(IContext context)
    {
      _statement.Evaluate(context);
    }

    public override void Write(StringBuilder sb)
    {
      _statement.Write(sb);
    }
  }
}
