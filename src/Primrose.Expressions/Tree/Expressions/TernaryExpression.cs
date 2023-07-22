using Primrose.Primitives.Extensions;
using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class TernaryExpression : CExpression
  {
    private readonly CExpression _question;
    private readonly CExpression _true;
    private readonly CExpression _false;

    internal TernaryExpression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // OREXPR ? EXPR : EXPR 

      _question = GetNext(scope, lexer);

      if (lexer.TokenType == TokenEnum.QUESTIONMARK)
      {
        lexer.Next(); // QUESTIONMARK
        _true = new Expression(scope, lexer).Get();

        if (lexer.TokenType == TokenEnum.COLON)
        {
          lexer.Next(); // COLON
          _false = new Expression(scope, lexer).Get();
        }
        else
        {
          throw new ParseException(lexer, TokenEnum.COLON);
        }
      }
    }

    public override CExpression Get()
    {
      if (_true == null)
        return _question.Get();
      return this;
    }

    public override Val Evaluate(IContext context)
    {
      Val result = _question.Evaluate(context);
      if (result.Type != typeof(bool)) throw new EvalException(this, Resource.Strings.Error_EvalException_NonBooleanConditional.F(result.Value));
      if ((bool)result)
        return _true?.Evaluate(context) ?? new Val();
      else
        return _false?.Evaluate(context) ?? new Val();
    }

    public override void Write(StringBuilder sb)
    {
      _question.Write(sb);
      TokenEnum.QUESTIONMARK.Write(sb, Writer.Padding.BOTH);
      _true.Write(sb);
      TokenEnum.COLON.Write(sb, Writer.Padding.BOTH);
      _false.Write(sb);
    }
  }
}