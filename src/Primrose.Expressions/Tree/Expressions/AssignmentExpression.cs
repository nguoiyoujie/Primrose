using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class AssignmentExpression : CExpression
  {
    private readonly ContextScope _scope;
    private readonly CExpression _varExpr;
    private readonly string _varName;
    //private readonly Variable _variable;
    private readonly TokenEnum _assigntype;
    private readonly CExpression _value;

    internal AssignmentExpression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // VARIABLE = EXPR
      // VARIABLE += EXPR
      // VARIABLE -= EXPR
      // VARIABLE *= EXPR
      // VARIABLE /= EXPR
      // VARIABLE %= EXPR
      // VARIABLE &= EXPR
      // VARIABLE |= EXPR
      // VARIABLE++
      // VARIABLE--
      // or
      // EXPR

      _scope = scope;
      switch (lexer.TokenType)
      {
        case TokenEnum.TYPE:
        case TokenEnum.VARIABLE:
          {
            if (Parser.TypeTokens[lexer.TokenContents] != null)
            {
              DeclVariable d = new DeclVariable(scope, lexer);
              _varExpr = d;
              _varName = d.varName;
            }
            else
            {
              _varName = lexer.TokenContents;
              _varExpr = new IndexedExpression(scope, lexer).Get();
            }

            _assigntype = lexer.TokenType;
            if (_assigntype == TokenEnum.PLUSPLUS
             || _assigntype == TokenEnum.MINUSMINUS
              )
            {
              lexer.Next(); //ASSIGN
            }
            else if (_assigntype == TokenEnum.ASSIGN
             || _assigntype == TokenEnum.PLUSASSIGN
             || _assigntype == TokenEnum.MINUSASSIGN
             || _assigntype == TokenEnum.ASTERISKASSIGN
             || _assigntype == TokenEnum.SLASHASSIGN
             || _assigntype == TokenEnum.PERCENTASSIGN
             || _assigntype == TokenEnum.AMPASSIGN
             || _assigntype == TokenEnum.PIPEASSIGN
              )
            {
              lexer.Next(); //ASSIGN
              _value = GetNext(scope, lexer);
            }
            else
            {
              _assigntype = TokenEnum.NOTHING;
              _value = _varExpr;
            }
          }
          break;
        default:
          {
            _assigntype = TokenEnum.NOTHING;
            _value = GetNext(scope, lexer);
          }
          break;
      }
    }

    public override CExpression Get()
    {
      if (_assigntype == TokenEnum.NOTHING)
        return _value.Get();
      return this;
    }

    public override Val Evaluate(IContext context)
    {
      if (_assigntype != TokenEnum.NOTHING)
      {
        Val v = _scope.GetVar(this, _varName);

        switch (_assigntype)
        {
          case TokenEnum.PLUSPLUS:
            v = Ops.Do(BOp.ADD, v, new Val(1));
            break;
          case TokenEnum.MINUSMINUS:
            v = Ops.Do(BOp.SUBTRACT, v, new Val(1));
            break;
          case TokenEnum.ASSIGN:
            v = _value?.Evaluate(context) ?? Val.NULL;
            break;
          case TokenEnum.PLUSASSIGN:
            v = Ops.Do(BOp.ADD, v, _value?.Evaluate(context) ?? Val.NULL);
            break;
          case TokenEnum.MINUSASSIGN:
            v = Ops.Do(BOp.SUBTRACT, v, _value?.Evaluate(context) ?? Val.NULL);
            break;
          case TokenEnum.ASTERISKASSIGN:
            v = Ops.Do(BOp.MULTIPLY, v, _value?.Evaluate(context) ?? Val.NULL);
            break;
          case TokenEnum.SLASHASSIGN:
            v = Ops.Do(BOp.DIVIDE, v, _value?.Evaluate(context) ?? Val.NULL);
            break;
          case TokenEnum.PERCENTASSIGN:
            v = Ops.Do(BOp.MODULUS, v, _value?.Evaluate(context) ?? Val.NULL);
            break;
          case TokenEnum.AMPASSIGN:
            v = Ops.Do(BOp.LOGICAL_AND, v, _value?.Evaluate(context) ?? Val.NULL);
            break;
          case TokenEnum.PIPEASSIGN:
            v = Ops.Do(BOp.LOGICAL_OR, v, _value?.Evaluate(context) ?? Val.NULL);
            break;
        }

        if (_varExpr is IndexedExpression indexexpr)
        {
          indexexpr.Evaluate(context);
          _scope.SetVar(this, _varName, v, indexexpr._indices);
        }
        else
          _scope.SetVar(this, _varName, v);
      }
      else
      {
        _value?.Evaluate(context);
      }
      return Val.NULL;
    }

    public override void Write(StringBuilder sb)
    {
      _varExpr?.Write(sb);
      if (_assigntype != TokenEnum.NOTHING)
      {
        _assigntype.Write(sb, Writer.Padding.BOTH);
      }
      _value?.Write(sb);

      // statement termination
      //TokenEnum.SEMICOLON.Write(sb, Writer.Padding.SUFFIX);
    }
  }
}