﻿using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class DeclVariable : Variable
  {
    private TokenEnum _type;

    internal DeclVariable(ContextScope scope, Lexer lexer) : base(scope, lexer, 0)
    {
      ValType varType;
      _type = lexer.TokenType;
      switch (_type)
      {
        case TokenEnum.DECL_BOOL:
          varType = ValType.BOOL;
          break;

        case TokenEnum.DECL_INT:
          varType = ValType.INT;
          break;

        case TokenEnum.DECL_FLOAT:
          varType = ValType.FLOAT;
          break;

        case TokenEnum.DECL_FLOAT2:
          varType = ValType.FLOAT2;
          break;

        case TokenEnum.DECL_FLOAT3:
          varType = ValType.FLOAT3;
          break;

        case TokenEnum.DECL_FLOAT4:
          varType = ValType.FLOAT4;
          break;

        case TokenEnum.DECL_STRING:
          varType = ValType.STRING;
          break;

        case TokenEnum.DECL_BOOL_ARRAY:
          varType = ValType.BOOL_ARRAY;
          break;

        case TokenEnum.DECL_INT_ARRAY:
          varType = ValType.INT_ARRAY;
          break;

        case TokenEnum.DECL_FLOAT_ARRAY:
          varType = ValType.FLOAT_ARRAY;
          break;

        case TokenEnum.DECL_STRING_ARRAY:
          varType = ValType.STRING_ARRAY;
          break;

        default:
          throw new ParseException(lexer);
      }
      lexer.Next(); //DECL

      if (lexer.TokenType != TokenEnum.VARIABLE)
        throw new ParseException(lexer, TokenEnum.VARIABLE);

      varName = lexer.TokenContents;
      scope.DeclVar(varName, varType, lexer);
      
      lexer.Next(); //VARIABLE
    }

    public override Val Evaluate(IContext context)
    {
      return Val.NULL; 
    }

    public override void Write(StringBuilder sb)
    {
      _type.Write(sb, Writer.Padding.SUFFIX);
      sb.Append(varName);
    }
  }
}