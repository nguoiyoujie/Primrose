using System.Text;

namespace Primrose.Expressions.Tree.Statements
{
  internal class SingleStatement : CStatement
  {
    private CStatement _statement;

    internal SingleStatement(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      _statement = GetNext(scope, lexer);

      if (lexer.TokenType == TokenEnum.SEMICOLON)
        lexer.Next(); // SEMICOLON
      else
        throw new ParseException(lexer, TokenEnum.SEMICOLON);
    }

    public override CStatement Get()
    {
      return _statement;
    }

    public override void Evaluate(IContext context)
    {
      _statement.Evaluate(context);
    }

    public override void Write(StringBuilder sb)
    {
      _statement.Write(sb);

      // statement termination
      TokenEnum.SEMICOLON.Write(sb, Writer.Padding.SUFFIX);
    }
  }
}
