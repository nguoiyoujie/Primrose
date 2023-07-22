using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class SelfAssigningVariable : CVariable
  {
    private readonly ContextScope _scope;
    private readonly bool assignFirst;
    private readonly TokenEnum assignType;

    // ++VARIABLE
    // --VARIABLE
    // VARIABLE++
    // VARIABLE--

    internal SelfAssigningVariable(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      switch (lexer.TokenType)
      {
        case TokenEnum.VARIABLE:
          varName = lexer.TokenContents;
          assignFirst = false;
          lexer.Next(); // VARIABLE
          switch (lexer.TokenType)
          {
            case TokenEnum.PLUSPLUS:
            case TokenEnum.MINUSMINUS:
              assignType = lexer.TokenType;
              lexer.Next();
              break;
            default:
              assignType = TokenEnum.NOTHING;
              break;
          }
          break;

        case TokenEnum.PLUSPLUS:
        case TokenEnum.MINUSMINUS:
          assignType = lexer.TokenType;
          assignFirst = true;
          lexer.Next(); // PLUSPLUS / MINUSMINUS
          switch (lexer.TokenType)
          {
            case TokenEnum.VARIABLE:
              varName = lexer.TokenContents;
              lexer.Next();
              break;
            default:
              throw new ParseException(lexer, TokenEnum.VARIABLE);
          }
          break;
      }
      _scope = scope;
    }

    public override Val Evaluate(IContext context)
    {
      Val current = _scope.GetVar(this, varName);
      Val changed = current;
      switch (assignType)
      {
        case TokenEnum.NOTHING:
          break;

        case TokenEnum.PLUSPLUS:
          changed = Ops.Do(BOp.ADD, current, new Val(1));
          _scope.SetVar(this, varName, changed);
          break;

        case TokenEnum.MINUSMINUS:
          changed = Ops.Do(BOp.SUBTRACT, current, new Val(1));
          _scope.SetVar(this, varName, changed);
          break;
      }
      return assignFirst ? changed : current;
    }

    public override void Write(StringBuilder sb)
    {
      if (assignFirst && assignType != TokenEnum.NOTHING)
        assignType.Write(sb);
      sb.Append(varName);
      if (!assignFirst && assignType != TokenEnum.NOTHING)
        assignType.Write(sb);
    }

    public override string ToString()
    {
      return "var " + varName;
    }
  }
}