using Primrose.Expressions.Tree.Expressions;
using System.Text;

namespace Primrose.Expressions.Tree.Statements
{
  internal class SingleStatement : CStatement
  {
    // Generally for statements that take

    private readonly CExpression _expr;

    internal SingleStatement(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      _expr = new AssignmentExpression(scope, lexer);

      if (lexer.TokenType == TokenEnum.SEMICOLON)
        lexer.Next(); // SEMICOLON
      else
        throw new ParseException(lexer, TokenEnum.SEMICOLON);
    }

    //public override CStatement Get()
    //{
    //  return _statement;
    //}

    public override bool Evaluate(IContext context, ref Val retval)
    {
      _expr.Evaluate(context);
      return false;
    }

    public override void Write(StringBuilder sb)
    {
      _expr.Write(sb);

      // statement termination
      TokenEnum.SEMICOLON.Write(sb, Writer.Padding.SUFFIX);
    }
  }
}
