using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class ParenthesizedExpression : CExpression
  {
    private ContextScope _scope;
    private CExpression _expression;

    internal ParenthesizedExpression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // ( TERN_EXPR )
      // ^

      bool parans = false;
      if (lexer.TokenType == TokenEnum.BRACKETOPEN)
      {
        parans = true;
        _scope = scope.Next;
        lexer.Next(); // BRACKETOPEN
      }
      else
        _scope = scope;

      _expression = new Expression(_scope, lexer).Get();

      if (parans)
      {
        if (lexer.TokenType != TokenEnum.BRACKETCLOSE)
          throw new ParseException(lexer, TokenEnum.BRACKETCLOSE);
        else
          lexer.Next(); // BRACKETCLOSE
      }
    }

    public override CExpression Get()
    {
      return this;
    }

    public override Val Evaluate(IContext context)
    {
      return _expression.Evaluate(context);
    }

    public override void Write(StringBuilder sb)
    {
      TokenEnum.BRACKETOPEN.Write(sb);
      _expression.Write(sb);
      TokenEnum.BRACKETCLOSE.Write(sb);
    }
  }
}