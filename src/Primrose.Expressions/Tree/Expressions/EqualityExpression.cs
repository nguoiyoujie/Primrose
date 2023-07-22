using System;
using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class EqualityExpression : CExpression
  {
    private readonly bool isUnequal = false;
    private readonly CExpression _first;
    private readonly CExpression _second;

    internal EqualityExpression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // RELATEEXPR == RELATEEXPR ...
      // RELATEEXPR != RELATEEXPR ...

      _first = GetNext(scope, lexer);

      TokenEnum _type = lexer.TokenType;
      if (_type == TokenEnum.EQUAL // ==
        )
      {
        lexer.Next(); //EQUAL
        _second = GetNext(scope, lexer);
      }
      else if (_type == TokenEnum.NOTEQUAL // !=
      )
      {
        lexer.Next(); //NOTEQUAL
        isUnequal = true;
        _second = GetNext(scope, lexer);
      }
    }

    public override CExpression Get()
    {
      if (_second == null)
        return _first.Get();
      return this;
    }

    public override Val Evaluate(IContext context)
    {
      Val result = _first.Evaluate(context);

      Val adden = _second.Evaluate(context);
      if (isUnequal)
        try { result = Ops.IsNotEqual(result, adden); } catch (Exception ex) { throw new EvalException(this, "!=", result, adden, ex); }
      else
        try { result = Ops.IsEqual(result, adden); } catch (Exception ex) { throw new EvalException(this, "==", result, adden, ex); }
      return result;
    }

    public override void Write(StringBuilder sb)
    {
      _first.Write(sb);
      (isUnequal ? TokenEnum.NOTEQUAL : TokenEnum.EQUAL).Write(sb, Writer.Padding.BOTH);
      _second.Write(sb);
    }
  }
}