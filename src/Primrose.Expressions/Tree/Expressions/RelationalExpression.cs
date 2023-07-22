using System;
using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class RelationalExpression : CExpression
  {
    private readonly CExpression _first;
    private readonly TokenEnum _type = TokenEnum.NOTHING;
    private readonly CExpression _second;

    internal RelationalExpression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // ADDEXPR < ADDEXPR
      // ADDEXPR > ADDEXPR
      // ADDEXPR <= ADDEXPR
      // ADDEXPR <= ADDEXPR

      _first = GetNext(scope, lexer);

      _type = lexer.TokenType;
      if (_type == TokenEnum.LESSTHAN // <
        || _type == TokenEnum.GREATERTHAN // >
        || _type == TokenEnum.LESSEQUAL // <=
        || _type == TokenEnum.GREATEREQUAL // >=
        )
      {
        lexer.Next(); //LESSTHAN / GREATERTHAN / LESSEQUAL / GREATEREQUAL
        _second = GetNext(scope, lexer);
      }
      else
      {
        _type = TokenEnum.NOTHING;
      }
    }

    public override CExpression Get()
    {
      if (_type == TokenEnum.NOTHING)
        return _first.Get();
      return this;
    }

    public override Val Evaluate(IContext context)
    {
      Val v1 = _first.Evaluate(context);
      Val v2 = _second.Evaluate(context);

      switch (_type)
      {
        case TokenEnum.LESSTHAN:
          try { return Ops.Do(BOp.LESS_THAN, v1, v2); } catch (Exception ex) { throw new EvalException(this, "<", v1, v2, ex); }
        case TokenEnum.GREATERTHAN:
          try { return Ops.Do(BOp.MORE_THAN, v1, v2); } catch (Exception ex) { throw new EvalException(this, ">", v1, v2, ex); }
        case TokenEnum.LESSEQUAL:
          try { return Ops.Do(BOp.LESS_THAN_OR_EQUAL_TO, v1, v2); } catch (Exception ex) { throw new EvalException(this, "<=", v1, v2, ex); }
        case TokenEnum.GREATEREQUAL:
          try { return Ops.Do(BOp.MORE_THAN_OR_EQUAL_TO, v1, v2); } catch (Exception ex) { throw new EvalException(this, ">=", v1, v2, ex); }
        default:
          return v1;
      }
    }

    public override void Write(StringBuilder sb)
    {
      _first.Write(sb);
      _type.Write(sb, Writer.Padding.BOTH);
      _second.Write(sb);
    }
  }
}