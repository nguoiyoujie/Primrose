using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Primrose.Expressions.Tree.Expressions.Literals
{
  internal class ArrayLiteral : CLiteral
  {
    // {x,y,z}
    // {{x1,x2},{y1,y2}}
    // interpreted as an typed array initializer
    // elements must be of the same type or can be implicitly cast to the same type. Otherwise, the new object expression (e.g. new float[]{1,2,3}) should be used.

    private readonly Array _param; // nested CExpression
    private readonly int[] _dimensions;
    private readonly int _elementCount;
    private readonly Array _eval;
    public int[] Dimensions { get => _dimensions; }

    internal ArrayLiteral(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      if (lexer.TokenType != TokenEnum.BRACEOPEN)
        throw new ParseException(lexer, TokenEnum.BRACEOPEN);

      int maxrank = 1;
      object[] olist = GetExpression(scope, lexer, 1, ref maxrank);
      _dimensions = new int[maxrank];
      _elementCount = 1;

      object[] p = olist;
      for (int i = 0; i < maxrank; i++)
      {
        _dimensions[i] = p.Length;
        _elementCount *= _dimensions[i];
        if (i < maxrank - 1)
        {
          if (p.Length > 0)
            p = (object[])p[0];
          else break;
        }
      }

      // check dimensions
      if (!Check(_dimensions, olist, 0))
      {
      }

      _param = Array.CreateInstance(typeof(CExpression), _dimensions);
      for (int i = 0; i < _elementCount; i++)
      {
        int[] index = GetIndex(_dimensions, i);
        _param.SetValue(Get(index, olist, 0), index);
      }

      // allocate once
      _eval = Array.CreateInstance(typeof(Val), _dimensions);
    }

    private static bool Check(int[] dimensions, object[] array, int rank)
    {
      if (array.Length != dimensions[rank])
        return false;

      if (rank == dimensions.Length - 1)
        return true;

      bool ret = true;
      for (int i = 0; i < dimensions[rank]; i++)
      {
        ret &= Check(dimensions, (object[])array[i], rank + 1);
      }
      return ret;
    }

    public static int[] GetIndex(int[] dimensions, int index)
    {
      int[] innerindex = new int[dimensions.Length];
      int n = index;
      for (int i = dimensions.Length - 1; i >= 0; i--)
      {
        innerindex[i] = n % dimensions[i];
        n /= dimensions[i];
      }
      return innerindex;
    }

    private static object Get(int[] index, object[] array, int rank)
    {
      if (rank == index.Length - 1)
        return array[index[rank]];
      else
        return Get(index, (object[])array[index[rank]], rank + 1);
    }

    private static object[] GetExpression(ContextScope scope, Lexer lexer, int rank, ref int out_rank)
    {
      out_rank = out_rank.Max(rank);
      List<object> list = new List<object>();
      if (lexer.TokenType != TokenEnum.BRACEOPEN)
        return list.ToArray();

      lexer.Next(); //BRACEOPEN

      if (lexer.TokenType != TokenEnum.BRACECLOSE)
      {
        if (lexer.TokenType != TokenEnum.BRACEOPEN)
          list.Add(new Expression(scope, lexer).Get());
        else
          list.Add(GetExpression(scope, lexer, rank + 1, ref out_rank));

        while (lexer.TokenType == TokenEnum.COMMA)
        {
          lexer.Next(); //COMMA
          if (lexer.TokenType != TokenEnum.BRACEOPEN)
            list.Add(new Expression(scope, lexer).Get());
          else
            list.Add(GetExpression(scope, lexer, rank + 1, ref out_rank));
        }
        lexer.Next(); //BRACECLOSE
      }
      return list.ToArray();
    }

    public override Val Evaluate(IContext context)
    {
      Type t = null;
      for (int i = 0; i < _elementCount; i++)
      {
        int[] index = GetIndex(_dimensions, i);
        Val v = ((CExpression)_param.GetValue(index)).Evaluate(context);
        _eval.SetValue(v, index);
        Type vt = v.Type;
        if (t == null)
          t = vt;
        else if (t != vt)
        {
          if (ImplicitConversionTable.HasImplicitConversion(vt, t, out _)) { }
          else if (ImplicitConversionTable.HasImplicitConversion(t, vt, out _)) { t = vt; }
          else throw new EvalException(this, Resource.Strings.Error_EvalException_IncompatibleArrayElement.F(t.Name, vt.Name));
        }
      }

      Array a = Array.CreateInstance(t, _dimensions);
      for (int i = 0; i < _elementCount; i++)
      {
        int[] index = GetIndex(_dimensions, i);
        a.SetValue(((Val)_eval.GetValue(index)).Value, index);
      }
      return new Val(a);
    }

    public override void Write(StringBuilder sb)
    {
      int index = 0;
      WriteExpression(sb, _dimensions, 0, ref index);
    }

    private void WriteExpression(StringBuilder sb, int[] dimensions, int rank, ref int index)
    {
      TokenEnum.BRACEOPEN.Write(sb);
      if (dimensions[rank] != 0) 
      {
        if (rank == dimensions.Length - 1)
          ((CExpression)_param.GetValue(GetIndex(dimensions, index++))).Write(sb);
        else
          WriteExpression(sb, dimensions, rank + 1, ref index);
      }
      for (int j = 1; j < dimensions[rank]; j++)
      {
        TokenEnum.COMMA.Write(sb);
        if (rank == dimensions.Length - 1)
          ((CExpression)_param.GetValue(GetIndex(dimensions, index++))).Write(sb);
        else
          WriteExpression(sb, dimensions, rank + 1, ref index);
      }
      TokenEnum.BRACECLOSE.Write(sb);
    }
  }
}
