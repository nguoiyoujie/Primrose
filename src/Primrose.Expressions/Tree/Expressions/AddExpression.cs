﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class AddExpression : CExpression
  {
    private CExpression _first;
    private Dictionary<CExpression, TokenEnum> _set = new Dictionary<CExpression, TokenEnum>();

    internal AddExpression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // MULTIEXPR + MULTIEXPR ...
      // MULTIEXPR - MULTIEXPR ...

      _first = GetNext(scope, lexer);

      while (lexer.TokenType == TokenEnum.PLUS // +
        || lexer.TokenType == TokenEnum.MINUS // -
        )
      {
        TokenEnum _type = lexer.TokenType;
        lexer.Next(); //PLUS / MINUS
        _set.Add(GetNext(scope, lexer), _type);
      }
    }

    public override CExpression Get()
    {
      if (_set.Count == 0)
        return _first;
      return this;
    }

    public override Val Evaluate(IContext context)
    {
      Val result = _first.Evaluate(context);
      foreach (CExpression _expr in _set.Keys)
      {
        Val adden = _expr.Evaluate(context);
        switch (_set[_expr])
        {
          case TokenEnum.PLUS:
            try { result = Ops.Do(BOp.ADD, result, adden); } catch (Exception ex) { throw new EvalException(this, "+", result, adden, ex); }
            break;
          case TokenEnum.MINUS:
            try { result = Ops.Do(BOp.SUBTRACT, result, adden); ; } catch (Exception ex) { throw new EvalException(this, "-", result, adden, ex); }
            break;
        }
      }
      return result;
    }

    public override void Write(StringBuilder sb)
    {
      _first.Write(sb);
      foreach (CExpression _expr in _set.Keys)
      {
        _set[_expr].Write(sb, Writer.Padding.BOTH);
        _expr.Write(sb);
      }
    }
  }
}