using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.ValueTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class DeclVariable : CVariable
  {
    private readonly string _declClassName;
    private readonly int[] _dimensions;

    internal DeclVariable(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // <typename>[...][...]... <variable_name>

      // Multi-dimensional array example:
      //      float[,][,] f = new float[1, 1][,];
      //      f[0, 0][1, 2] = 1;

      _declClassName = lexer.TokenContents;
      Type type = Parser.TypeTokens[_declClassName];
      if (type == null)
        throw new ParseException(lexer, "Type identifier expected, read '{0}' instead.".F(_declClassName));

      lexer.Next(); //DECL

      if (lexer.TokenType == TokenEnum.SQBRACKETOPEN)
      {
        List<int> dimlist = new List<int>();
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
        _dimensions = dimlist.ToArray();

        for (int i = _dimensions.Length - 1; i >= 0; i--)
        {
          int d = _dimensions[i];
          if (d == 1)
            type = type.MakeArrayType(); // vector array
          else
            type = type.MakeArrayType(d); // multidimensional array
        }
      }

      if (lexer.TokenType != TokenEnum.VARIABLE)
        throw new ParseException(lexer, TokenEnum.VARIABLE);

      varName = lexer.TokenContents;
      scope.DeclVar(varName, type, lexer);
      
      lexer.Next(); //VARIABLE
    }

    public override Val Evaluate(IContext context)
    {
      return Val.NULL; 
    }

    public override void Write(StringBuilder sb)
    {
      sb.Append(_declClassName);
      if (_dimensions != null)
      {
        for (int i = 0; i < _dimensions.Length; i++)
        {
          TokenEnum.SQBRACKETOPEN.Write(sb);
          for (int j = 0; j < _dimensions[i] - 1; j++)
          {
            TokenEnum.COMMA.Write(sb);
          }
          TokenEnum.SQBRACKETCLOSE.Write(sb);
        }
      }
      sb.Append(" ");
      sb.Append(varName);
    }
  }
}