using System.Collections.Generic;
using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class IndexedExpression : CExpression
  {
    private readonly CExpression _expression;
    private readonly CExpression[][] _indices_expr;
    public readonly int[][] _indices;

    internal IndexedExpression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // EXPR[EXPR,...][EXPR,...]...
      // ^

      // Multi-dimensional array example:
      //      float[,][,] f = new float[1, 1][,];
      //      f[0, 0][1, 2] = 1;

      _expression = GetNext(scope, lexer);

      if (lexer.TokenType != TokenEnum.SQBRACKETOPEN)
        return;

      // indexer
      if (lexer.TokenType == TokenEnum.SQBRACKETOPEN)
      {
        List<CExpression[]> indexlist = new List<CExpression[]>();
        while (lexer.TokenType == TokenEnum.SQBRACKETOPEN)
        {
          lexer.Next(); // SQBRACKETOPEN
          List<CExpression> innerlist = new List<CExpression>
          {
            new Expression(scope, lexer).Get()
          };

          while (lexer.TokenType == TokenEnum.COMMA)
          {
            lexer.Next(); // COMMA
            innerlist.Add(new Expression(scope, lexer).Get());
          }

          indexlist.Add(innerlist.ToArray());

          if (lexer.TokenType != TokenEnum.SQBRACKETCLOSE)
            throw new ParseException(lexer, TokenEnum.SQBRACKETCLOSE);
          lexer.Next(); // SQBRACKETCLOSE
        }
        _indices_expr = indexlist.ToArray();

        _indices = new int[_indices_expr.Length][];
        for (int i = 0; i < _indices_expr.Length; i++)
        {
          _indices[i] = new int[_indices_expr[i].Length];
        }
      }
    }

    public override CExpression Get()
    {
      return (_indices_expr == null) ? _expression.Get() : this;
    }

    public override Val Evaluate(IContext context)
    {
      Val c = _expression.Evaluate(context);
      if (_indices_expr == null)
        return c;

      for (int i = 0; i < _indices_expr.Length; i++)
      {
        for (int i2 = 0; i2 < _indices_expr[i].Length; i2++)
        {
          Val v = _indices_expr[i][i2].Evaluate(context);
          try
          {
            _indices[i][i2] = v.Cast<int>();
          }
          catch
          {
            throw new EvalException(this, Resource.Strings.Error_EvalException_InvalidArrayIndex);
          }
        }
      }

      Val cv = c;
      foreach (int[] i2 in _indices)
      {
        cv = Ops.GetIndex(cv, i2);
      }
      return cv;

      /*
      Array a;
      try
      {
        a = c.Cast<Array>();
      }
      catch
      {
        throw new EvalException(this, Resource.Strings.Error_EvalException_IndexOnNonArray.F(c));
      }

      try
      {
        object o = a;
        foreach (int[] i2 in _indices)
        {
          a = (Array)o;
          o = a.GetValue(i2);
        }

        return new Val(o);
      }
      catch (IndexOutOfRangeException)
      {
        int len = a?.Length ?? 0;
        throw new EvalException(this, Resource.Strings.Error_EvalException_IndexOutOfRange.F(_indices.Length, len));
      }
      catch
      {
        throw new EvalException(this, Resource.Strings.Error_EvalException_IndexOnNonArray.F(c));
      }
      */
    }

    public override void Write(StringBuilder sb)
    {
      _expression.Write(sb);
      if (_indices_expr != null)
      {
        foreach (CExpression[] expr in _indices_expr)
        {
          TokenEnum.SQBRACKETOPEN.Write(sb);
          expr[0]?.Write(sb);
          for (int i = 1; i < expr.Length; i++)
          {
            sb.Append(",");
            expr[i]?.Write(sb);
          }
          TokenEnum.SQBRACKETCLOSE.Write(sb);
        }
      }
    }
  }
}
