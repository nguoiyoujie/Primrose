using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class LogicalAndExpression : CExpression
  {
    private readonly CExpression _first;
    private readonly List<CExpression> _set = new List<CExpression>();

    internal LogicalAndExpression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // EQUALEXPR && EQUALEXPR ...

      _first = GetNext(scope, lexer);

      while (lexer.TokenType == TokenEnum.AMPAMP // &&
        )
      {
        lexer.Next(); //AMPAMP
        _set.Add(GetNext(scope, lexer));
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
      if (_set.Count == 0)
        return _first.Evaluate(context);

      Val result = _first.Evaluate(context);
      if (result.Type != ValType.BOOL) throw new EvalException(this, Resource.Strings.Error_EvalException_NonBooleanConditional.F(result.Value));
      if ((bool)result)
      {
        foreach (CExpression _expr in _set)
        {
          Val adden = _expr.Evaluate(context);

          try { result = Ops.Do(BOp.LOGICAL_AND, result, adden); } catch (Exception ex) { throw new EvalException(this, "&&", result, adden, ex); }
          if (!(bool)result)
            break;
        }
      }
      return result;
    }

    public override void Write(StringBuilder sb)
    {
      _first.Write(sb);
      foreach (CExpression _expr in _set)
      {
        TokenEnum.AMPAMP.Write(sb, Writer.Padding.BOTH);
        _expr.Write(sb);
      }
    }
  }
}