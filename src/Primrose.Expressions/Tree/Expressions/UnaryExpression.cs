﻿using System;
using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class UnaryExpression : CExpression
  {
    private readonly CExpression _primary;
    private readonly TokenEnum _type;

    internal UnaryExpression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // + PRIAMRY
      // - PRIAMRY
      // ! PRIAMRY
      // ~ PRIAMRY // not supported yet
      // TO DO: ++/-- PRIMARY;
      // ^

      _type = lexer.TokenType;
      if (_type == TokenEnum.PLUS // +
        || _type == TokenEnum.MINUS // -
        || _type == TokenEnum.NOT // !
        )
      {
        lexer.Next(); // PLUS / MINUS / NOT
        _primary = GetNext(scope, lexer);
      }
      else
      {
        _primary = GetNext(scope, lexer);
        _type = TokenEnum.NOTHING;
      }
    }

    public override CExpression Get()
    {
      if (_type == TokenEnum.NOTHING)
        return _primary.Get();
      return this;
    }

    public override Val Evaluate(IContext context)
    {
      Val result = _primary.Evaluate(context);
      switch (_type)
      {
        default:
        case TokenEnum.PLUS:
        case TokenEnum.NOTHING:
          return result;
        case TokenEnum.MINUS:
          try { return Ops.Do(UOp.NEGATION, result); } catch (Exception ex) { throw new EvalException(this, "-", result, ex); }
        case TokenEnum.NOT:
          try { return Ops.Do(UOp.LOGICAL_NOT, result); } catch (Exception ex) { throw new EvalException(this, "!", result, ex); }
      }
    }

    public override void Write(StringBuilder sb)
    {
      _type.Write(sb);
      _primary.Write(sb);
    }
  }
}