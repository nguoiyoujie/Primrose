using Primrose.Expressions.Tree.Expressions;
using System.Text;

namespace Primrose.Expressions.Tree.Statements
{
  internal class AssignmentStatement: CStatement
  {
    private readonly ContextScope _scope;
    private readonly Variable _variable;
    private readonly TokenEnum _assigntype;
    private readonly CExpression _value;

    internal AssignmentStatement(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // VARIABLE = EXPR;
      // VARIABLE += EXPR;
      // VARIABLE -= EXPR;
      // VARIABLE *= EXPR;
      // VARIABLE /= EXPR;
      // VARIABLE %= EXPR;
      // VARIABLE &= EXPR;
      // VARIABLE |= EXPR;
      // TO DO: VARIABLE++/--;
      // or
      // EXPR;

      _scope = scope;
      switch (lexer.TokenType)
      {
        case TokenEnum.DECL_BOOL:
        case TokenEnum.DECL_INT:
        case TokenEnum.DECL_FLOAT:
        case TokenEnum.DECL_FLOAT2:
        case TokenEnum.DECL_FLOAT3:
        case TokenEnum.DECL_FLOAT4:
        case TokenEnum.DECL_STRING:
        case TokenEnum.DECL_BOOL_ARRAY:
        case TokenEnum.DECL_INT_ARRAY:
        case TokenEnum.DECL_FLOAT_ARRAY:
        case TokenEnum.DECL_STRING_ARRAY:
        case TokenEnum.VARIABLE:
          {
            if (lexer.TokenType == TokenEnum.VARIABLE)
              _variable = new Variable(scope, lexer).Get() as Variable;
            else
              _variable = new DeclVariable(scope, lexer).Get() as DeclVariable;

            _assigntype = lexer.TokenType;
            if (_assigntype == TokenEnum.ASSIGN
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
              _value = new Expression(scope, lexer).Get();
            }
            else
            {
              _assigntype = TokenEnum.NOTHING;
            }
          }
          break;
        default:
          {
            _assigntype = TokenEnum.NOTHING;
            _value = new Expression(scope, lexer).Get();
          }
          break;
      }
    }

    public override void Evaluate(IContext context)
    {
      if (_assigntype != TokenEnum.NOTHING)
      {
        Val v = _scope.GetVar(this, _variable.varName);

        switch (_assigntype)
        {
          case TokenEnum.ASSIGN:
            v = _value?.Evaluate(context) ?? Val.NULL;
            break;
          case TokenEnum.PLUSASSIGN:
            v = Ops.Do(BOp.ADD, v, _value?.Evaluate(context) ?? Val.NULL); ;
            break;
          case TokenEnum.MINUSASSIGN:
            v = Ops.Do(BOp.SUBTRACT, v, _value?.Evaluate(context) ?? Val.NULL); ;
            break;
          case TokenEnum.ASTERISKASSIGN:
            v = Ops.Do(BOp.MULTIPLY, v, _value?.Evaluate(context) ?? Val.NULL); ;
            break;
          case TokenEnum.SLASHASSIGN:
            v = Ops.Do(BOp.DIVIDE, v, _value?.Evaluate(context) ?? Val.NULL); ;
            break;
          case TokenEnum.PERCENTASSIGN:
            v = Ops.Do(BOp.MODULUS, v, _value?.Evaluate(context) ?? Val.NULL); ;
            break;
          case TokenEnum.AMPASSIGN:
            v = Ops.Do(BOp.LOGICAL_AND, v, _value?.Evaluate(context) ?? Val.NULL); ;
            break;
          case TokenEnum.PIPEASSIGN:
            v = Ops.Do(BOp.LOGICAL_OR, v, _value?.Evaluate(context) ?? Val.NULL); ;
            break;
        }
        _scope.SetVar(this, _variable.varName, v);
      }
      else
      {
        _value?.Evaluate(context);
      }
    }

    public override void Write(StringBuilder sb)
    {
      if (_assigntype != TokenEnum.NOTHING)
      {
        _variable.Write(sb);
        _assigntype.Write(sb, Writer.Padding.BOTH);
      }
      _value.Write(sb);

      // statement termination
      TokenEnum.SEMICOLON.Write(sb, Writer.Padding.SUFFIX);
    }
  }
}