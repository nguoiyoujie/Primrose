using Primrose.Expressions.Tree.Expressions.Literals;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class PrimaryExpression : CExpression
  {
    private readonly CExpression _child;

    internal PrimaryExpression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      TokenEnum t = lexer.TokenType;
      switch (t)
      {
        case TokenEnum.BRACKETOPEN:
          _child = new ParenthesizedExpression(scope, lexer).Get();
          break;
        case TokenEnum.FUNCTION:
          _child = new Function(scope, lexer).Get();
          break;

        case TokenEnum.PLUSPLUS:
        case TokenEnum.MINUSMINUS:
        case TokenEnum.VARIABLE:
          _child = new SelfAssigningVariable(scope, lexer).Get(); // Variable
          break;

        case TokenEnum.NEW:
          _child = new NewObjectExpression(scope, lexer).Get();
          break;

        //Literals
        case TokenEnum.NULLLITERAL:
          _child = new NullLiteral(scope, lexer).Get();
          break;
        case TokenEnum.BOOLEANLITERAL:
          _child = new BoolLiteral(scope, lexer).Get();
          break;
        case TokenEnum.DECIMALINTEGERLITERAL:
          _child = new IntLiteral(scope, lexer).Get();
          break;
        case TokenEnum.HEXINTEGERLITERAL:
          _child = new HexLiteral(scope, lexer).Get();
          break;
        case TokenEnum.REALLITERAL:
          _child = new SingleLiteral(scope, lexer).Get();
          break;
        case TokenEnum.STRINGLITERAL:
          _child = new StringLiteral(scope, lexer).Get();
          break;
        case TokenEnum.BRACEOPEN:
          _child = new ArrayLiteral(scope, lexer).Get();
          break;

        default:
          throw new ParseException(lexer);
      }
    }

    public override CExpression Get()
    {
      return _child.Get();
    }

    public override Val Evaluate(IContext context)
    {
      return _child.Evaluate(context);
    }
  }
}
