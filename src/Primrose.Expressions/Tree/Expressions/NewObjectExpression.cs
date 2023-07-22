using Primrose.Expressions.Tree.Expressions.Literals;
using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class NewObjectExpression : CExpression
  {
    // new <type_name>()
    // new <type_name>[i,j,k,...][,,...]...

    // Multi-dimensional array example:
    //      float[,][,] f = new float[1, 1][,];
    //      f[0, 0][1, 2] = 1;

    private readonly string _ttypeName;
    private readonly Type _type;
    private readonly CExpression _expr;
    private readonly List<CExpression> _param = new List<CExpression>();
    private readonly int[] _dimensions;
    private readonly CExpression[] _first_indices;
    private readonly ArrayLiteral _first_element;
    private readonly int[] _first_dimensions;
    private readonly object[] oparam;

    internal NewObjectExpression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      if (lexer.TokenType == TokenEnum.NEW)
      {
        lexer.Next(); //NEW

        _ttypeName = lexer.TokenContents;
        _type = Parser.TypeTokens[_ttypeName];
        if (_type == null)
          throw new ParseException(lexer, Resource.Strings.Error_ParseException_Type.F(_ttypeName));

        lexer.Next(); //DECL


        if (lexer.TokenType == TokenEnum.SQBRACKETOPEN)
        {
          lexer.Next(); // SQBRACKETOPEN
          List<int> dimlist = new List<int>();
          List<CExpression> innerlist = new List<CExpression>();
          bool deferred_add = false;

          // if there is no indexes in the first rank, this indicates that the first rank dimensions is to be determined by a differed add operation.
          if (lexer.TokenType == TokenEnum.SQBRACKETCLOSE || lexer.TokenType == TokenEnum.COMMA)
          {
            deferred_add = true;
          }

          if (!deferred_add) { innerlist.Add(new Expression(scope, lexer).Get()); }
          while (lexer.TokenType == TokenEnum.COMMA)
          {
            lexer.Next(); // COMMA
            if (!deferred_add) { innerlist.Add(new Expression(scope, lexer).Get()); }
          }
          if (innerlist.Count > 0) { _first_indices = innerlist.ToArray(); }

          dimlist.Add(innerlist.Count);

          if (lexer.TokenType != TokenEnum.SQBRACKETCLOSE)
            throw new ParseException(lexer, TokenEnum.SQBRACKETCLOSE);
          lexer.Next(); // SQBRACKETCLOSE

          while (lexer.TokenType == TokenEnum.SQBRACKETOPEN)
          {
            lexer.Next(); // SQBRACKETOPEN
            int count = 1;

            while (lexer.TokenType == TokenEnum.COMMA)
            {
              lexer.Next(); // COMMA
              count++;
            }

            dimlist.Add(count);

            if (lexer.TokenType != TokenEnum.SQBRACKETCLOSE)
              throw new ParseException(lexer, TokenEnum.SQBRACKETCLOSE);
            lexer.Next(); // SQBRACKETCLOSE
          }

          if (deferred_add)
          {
            _first_element = new ArrayLiteral(scope, lexer);
            dimlist[0] = _first_element.Dimensions.Length;
          }

          _dimensions = dimlist.ToArray();
        }
        else
        {
          if (lexer.TokenType != TokenEnum.BRACKETOPEN)
            throw new ParseException(lexer, TokenEnum.BRACKETOPEN);
          lexer.Next(); //BRACKETOPEN

          while (lexer.TokenType != TokenEnum.BRACKETCLOSE)
          {
            _param.Add(new Expression(scope, lexer).Get());

            while (lexer.TokenType == TokenEnum.COMMA)
            {
              lexer.Next(); //COMMA
              _param.Add(new Expression(scope, lexer).Get());
            }
          }
          lexer.Next(); //BRACKETCLOSE
        }

        // allocate once
        oparam = new object[_param.Count];
        if (_dimensions != null && _dimensions.Length > 0)
          _first_dimensions = _first_indices != null ? new int[_first_indices.Length] : _first_element.Dimensions;
      }
      else
      {
        _expr = GetNext(scope, lexer);
      }
    }

    public override CExpression Get()
    {
      return (_type == null) ? _expr.Get() : this;
    }

    public override Val Evaluate(IContext context)
    {
      for (int i = 0; i < _param.Count; i++)
      {
        Val p = _param[i].Evaluate(context);
        oparam[i] = p.Value;
      }

      if (_dimensions == null || _dimensions.Length == 0)
      {
        object o = Activator.CreateInstance(_type, oparam);
        return new Val(o);
      }
      else
      {
        int[] x = _first_dimensions;
        if (_first_indices != null)
        {
          for (int i = 0; i < _first_indices.Length; i++)
          {
            Val v = _first_indices[i].Evaluate(context);
            try
            {
              x[i] = v.Cast<int>();
            }
            catch
            {
              throw new EvalException(this, Resource.Strings.Error_EvalException_InvalidArrayIndex);
            }
          }
        }

        Type t = _type;
        for (int i = _dimensions.Length - 1; i > 0; i--)
        {
          int d = _dimensions[i];
          if (d == 1)
            t = t.MakeArrayType(); // vector array
          else
            t = t.MakeArrayType(d); // multidimensional array
        }

        Array a = Array.CreateInstance(t, x);
        if (_first_element != null)
        {
          Val v = _first_element.Evaluate(context);
          try
          {
            Array va = v.Cast<Array>();
            for (int i = 0; i < va.Length; i++)
            {
              int[] index = ArrayLiteral.GetIndex(x, i);
              a.SetValue(va.GetValue(index), index);
            }
          }
          catch
          {
            throw new EvalException(this, Resource.Strings.Error_EvalException_InvalidArrayIndex);
          }
        }
        return new Val(a);
      }
    }

    public override void Write(StringBuilder sb)
    {
      TokenEnum.NEW.Write(sb, Writer.Padding.SUFFIX);
      sb.Append(_ttypeName);
      if (_dimensions == null || _dimensions.Length == 0)
      {
        TokenEnum.BRACKETOPEN.Write(sb);
        if (_param.Count > 0) { _param[0].Write(sb); }
        for (int j = 1; j < _param.Count; j++)
        {
          TokenEnum.COMMA.Write(sb);
          _param[j].Write(sb);
        }
        TokenEnum.BRACKETCLOSE.Write(sb);
      }
      else
      {
        for (int i = 0; i < _dimensions.Length; i++)
        {
          TokenEnum.SQBRACKETOPEN.Write(sb);
          if (_first_indices != null)
          {
            if (i == 0) { _first_indices[0].Write(sb); }
            for (int j = 1; j < _dimensions[i]; j++)
            {
              TokenEnum.COMMA.Write(sb);
              if (i == 0) { _first_indices[j].Write(sb); }
            }
          }
          else
          {
            for (int j = 0; j < _dimensions[i] - 1; j++)
            {
              TokenEnum.COMMA.Write(sb);
            }
          }
          TokenEnum.SQBRACKETCLOSE.Write(sb);
          if (_first_indices == null)
          {
            _first_element.Write(sb);
          }
        }
      }
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      Write(sb);
      return sb.ToString();
    }
  }
}
