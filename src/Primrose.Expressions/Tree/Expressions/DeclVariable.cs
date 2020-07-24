using Primrose.Primitives.Factories;
using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class DeclVariable : Variable
  {
    private readonly TokenEnum _type;
    private static readonly Registry<TokenEnum, ValType> token_to_valtype = new Registry<TokenEnum, ValType>();

    static DeclVariable()
    {
      token_to_valtype.Default = ValType.NULL;
      token_to_valtype.Add(TokenEnum.DECL_BOOL, ValType.BOOL);
      token_to_valtype.Add(TokenEnum.DECL_INT, ValType.INT);
      token_to_valtype.Add(TokenEnum.DECL_FLOAT, ValType.FLOAT);
      token_to_valtype.Add(TokenEnum.DECL_FLOAT2, ValType.FLOAT2);
      token_to_valtype.Add(TokenEnum.DECL_FLOAT3, ValType.FLOAT3);
      token_to_valtype.Add(TokenEnum.DECL_FLOAT4, ValType.FLOAT4);
      token_to_valtype.Add(TokenEnum.DECL_STRING, ValType.STRING);
      token_to_valtype.Add(TokenEnum.DECL_BOOL_ARRAY, ValType.BOOL_ARRAY);
      token_to_valtype.Add(TokenEnum.DECL_INT_ARRAY, ValType.INT_ARRAY);
      token_to_valtype.Add(TokenEnum.DECL_FLOAT_ARRAY, ValType.FLOAT_ARRAY);
      token_to_valtype.Add(TokenEnum.DECL_STRING_ARRAY, ValType.STRING_ARRAY);
    }

    internal DeclVariable(ContextScope scope, Lexer lexer) : base(scope, lexer, 0)
    {
      _type = lexer.TokenType;
      ValType varType = token_to_valtype[_type];
      if (varType == ValType.NULL)
        throw new ParseException(lexer);

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