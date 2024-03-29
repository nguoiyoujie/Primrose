﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class MultiplyExpression : CExpression
  {
    private readonly CExpression _first;
    private readonly Dictionary<CExpression, TokenEnum> _set = new Dictionary<CExpression, TokenEnum>();

    internal MultiplyExpression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // UNARYEXPR * UNARYEXPR ...
      // UNARYEXPR / UNARYEXPR ...
      // UNARYEXPR % UNARYEXPR ...

      _first = GetNext(scope, lexer);

      while (lexer.TokenType == TokenEnum.ASTERISK // *
        || lexer.TokenType == TokenEnum.SLASH // /
        || lexer.TokenType == TokenEnum.PERCENT // %
        )
      {
        TokenEnum _type = lexer.TokenType;
        lexer.Next(); //ASTERISK / SLASH / PERCENT
        _set.Add(GetNext(scope, lexer), _type);
      }
    }

    public override CExpression Get()
    {
      if (_set.Count == 0)
        return _first.Get();
      return this;
    }

    public override Val Evaluate(IContext context)
    {
      Val result = _first.Evaluate(context);
      foreach (var kvp in _set)
      {
        CExpression _expr = kvp.Key;
        Val adden = _expr.Evaluate(context);

        switch (_set[_expr])
        {
          case TokenEnum.ASTERISK:
            try { result = Ops.Do(BOp.MULTIPLY, result, adden); } catch (Exception ex) { throw new EvalException(this, "*", result, adden, ex); }
            break;
          case TokenEnum.SLASH:
            try { result = Ops.Do(BOp.DIVIDE, result, adden); } catch (Exception ex) { throw new EvalException(this, "/", result, adden, ex); }
            break;
          case TokenEnum.PERCENT:
            try { result = Ops.Do(BOp.MODULUS, result, adden); } catch (Exception ex) { throw new EvalException(this, "%", result, adden, ex); }
            break;
        }
      }

      return result;
    }

    public override void Write(StringBuilder sb)
    {
      _first.Write(sb);
      foreach (var kvp in _set)
      {
        CExpression _expr = kvp.Key;
        _set[_expr].Write(sb, Writer.Padding.BOTH);
        _expr.Write(sb);
      }
    }
  }
}