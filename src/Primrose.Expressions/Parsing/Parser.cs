using Primrose.Expressions.Tree.Expressions;
using Primrose.Expressions.Tree.Statements;
using Primrose.Primitives.Factories;
using Primrose.Primitives.ValueTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Primrose.Expressions
{
  internal class Parser
  {
    public static TokenDefinition[] Definitions =
      new TokenDefinition[]
      {
        // white space collector
        new TokenDefinition(@"\s+", TokenEnum.WHITESPACE, LintType.NONE),

        // keywords
        new TokenDefinition(@"new\b", TokenEnum.NEW, LintType.KEYWORD),
        new TokenDefinition(@"if\b", TokenEnum.IF, LintType.KEYWORD),
        new TokenDefinition(@"then\b", TokenEnum.THEN, LintType.KEYWORD),
        new TokenDefinition(@"else\b", TokenEnum.ELSE, LintType.KEYWORD),
        new TokenDefinition(@"foreach\b", TokenEnum.FOREACH, LintType.KEYWORD),
        new TokenDefinition(@"in\b", TokenEnum.IN, LintType.KEYWORD),
        new TokenDefinition(@"for\b", TokenEnum.FOR, LintType.KEYWORD),
        new TokenDefinition(@"while\b", TokenEnum.WHILE, LintType.KEYWORD),

        // literals
        new TokenDefinition(@"(((N|n)ull)|NULL)\b", TokenEnum.NULLLITERAL, LintType.SPECIALLITERAL),
        new TokenDefinition(@"((T|t)rue|(F|f)alse|TRUE|FALSE)\b", TokenEnum.BOOLEANLITERAL, LintType.SPECIALLITERAL),
        new TokenDefinition(@"[a-zA-Z_][a-zA-Z0-9_\.]*(?=\s*\()", TokenEnum.FUNCTION, LintType.FUNCTION),
        new TokenDefinition(@"[a-zA-Z_][a-zA-Z0-9_\.]*(?!\s*\()", TokenEnum.VARIABLE, LintType.VARIABLE_OR_TYPE),
        new TokenDefinition(@"""(?:[^""\\]|\\.)*""", TokenEnum.STRINGLITERAL, LintType.STRINGLITERAL),
        new TokenDefinition(@"0(x|X)[0-9a-fA-F]+\b", TokenEnum.HEXINTEGERLITERAL, LintType.NUMERICLITERAL),
        new TokenDefinition(@"[0-9]+(?![fFdDMmeE\.])\b", TokenEnum.DECIMALINTEGERLITERAL, LintType.NUMERICLITERAL),
        new TokenDefinition(@"([0-9]+\.[0-9]+([eE][+-]?[0-9]+)?([fFdDMm]?)?)|(\.[0-9]+([eE][+-]?[0-9]+)?([fFdDMm]?)?)|([0-9]+([eE][+-]?[0-9]+)([fFdDMm]?)?)|([0-9]+([fFdDMm]?))\b", TokenEnum.REALLITERAL, LintType.NUMERICLITERAL),

        // Brackets
        new TokenDefinition(@"{\s*", TokenEnum.BRACEOPEN, LintType.NONE),
        new TokenDefinition(@"\s*}", TokenEnum.BRACECLOSE, LintType.NONE),
        new TokenDefinition(@"\(\s*", TokenEnum.BRACKETOPEN, LintType.NONE),
        new TokenDefinition(@"\s*\)", TokenEnum.BRACKETCLOSE, LintType.NONE),
        new TokenDefinition(@"\[\s*", TokenEnum.SQBRACKETOPEN, LintType.NONE),
        new TokenDefinition(@"\s*\]", TokenEnum.SQBRACKETCLOSE, LintType.NONE),

        // Comment
        new TokenDefinition(@"//.*", TokenEnum.COMMENT, LintType.COMMENT),

        // Multi-character Operators
        new TokenDefinition(@"\+\+", TokenEnum.PLUSPLUS, LintType.NONE),
        new TokenDefinition(@"--", TokenEnum.MINUSMINUS, LintType.NONE),
        new TokenDefinition(@"\|\|", TokenEnum.PIPEPIPE, LintType.NONE),
        new TokenDefinition(@"&&", TokenEnum.AMPAMP, LintType.NONE),
        new TokenDefinition(@"!=|<>", TokenEnum.NOTEQUAL, LintType.NONE),
        new TokenDefinition(@"==", TokenEnum.EQUAL, LintType.NONE),
        new TokenDefinition(@"<=", TokenEnum.LESSEQUAL, LintType.NONE),
        new TokenDefinition(@">=", TokenEnum.GREATEREQUAL, LintType.NONE),

        new TokenDefinition(@"\+=", TokenEnum.PLUSASSIGN, LintType.NONE),
        new TokenDefinition(@"\-=", TokenEnum.MINUSASSIGN, LintType.NONE),
        new TokenDefinition(@"\*=", TokenEnum.ASTERISKASSIGN, LintType.NONE),
        new TokenDefinition(@"/=", TokenEnum.SLASHASSIGN, LintType.NONE),
        new TokenDefinition(@"%=", TokenEnum.PERCENTASSIGN, LintType.NONE),
        new TokenDefinition(@"&=", TokenEnum.AMPASSIGN, LintType.NONE),
        new TokenDefinition(@"\|=", TokenEnum.PIPEASSIGN, LintType.NONE),

        // Single-character Operators
        new TokenDefinition(@"=", TokenEnum.ASSIGN, LintType.NONE),
        new TokenDefinition(@";", TokenEnum.SEMICOLON, LintType.NONE),
        new TokenDefinition(@"&(?!&)", TokenEnum.AMP, LintType.NONE),
        //new TokenDefinition(@"\^", TokenEnum.POWER, LinterType.NONE),
        new TokenDefinition(@"\+", TokenEnum.PLUS, LintType.NONE),
        new TokenDefinition(@"-", TokenEnum.MINUS, LintType.NONE),
        new TokenDefinition(@"!", TokenEnum.NOT, LintType.NONE),
        new TokenDefinition(@"\*", TokenEnum.ASTERISK, LintType.NONE),
        new TokenDefinition(@"/", TokenEnum.SLASH, LintType.NONE),
        new TokenDefinition(@"%", TokenEnum.PERCENT, LintType.NONE),
        new TokenDefinition(@"\?", TokenEnum.QUESTIONMARK, LintType.NONE),
        new TokenDefinition(@",", TokenEnum.COMMA, LintType.NONE),
        new TokenDefinition(@"<(?!>)", TokenEnum.LESSTHAN, LintType.NONE),
        new TokenDefinition(@">", TokenEnum.GREATERTHAN, LintType.NONE),
        new TokenDefinition(@":", TokenEnum.COLON, LintType.NONE)
      };

    public static Registry<string, Type> TypeTokens = new Registry<string, Type>()
    {
      {"bool", typeof(bool)},
      {"char", typeof(char)},
      {"byte", typeof(byte)},
      {"sbyte", typeof(sbyte)},
      {"short", typeof(short)},
      {"ushort", typeof(ushort)},
      {"int", typeof(int)},
      {"uint", typeof(uint)},
      {"float", typeof(float)},
      {"float2", typeof(float2)},
      {"float3", typeof(float3)},
      {"float4", typeof(float4)},
      {"int2", typeof(int2)},
      {"int3", typeof(int3)},
      {"int4", typeof(int4)},
      {"string", typeof(string)},
    };

    /*
    public static Registry<TokenEnum, Type> TypeTokens = new Registry<TokenEnum, Type>()
    {
      {TokenEnum.DECL_BOOL, typeof(bool)},
      {TokenEnum.DECL_INT, typeof(int)},
      {TokenEnum.DECL_FLOAT, typeof(float)},
      {TokenEnum.DECL_FLOAT2, typeof(float2)},
      {TokenEnum.DECL_FLOAT3, typeof(float3)},
      {TokenEnum.DECL_FLOAT4, typeof(float4)},
      {TokenEnum.DECL_STRING, typeof(string)},
      {TokenEnum.DECL_BOOL_ARRAY, typeof(bool[])},
      {TokenEnum.DECL_INT_ARRAY, typeof(int[])},
      {TokenEnum.DECL_FLOAT_ARRAY, typeof(float[])},
      {TokenEnum.DECL_STRING_ARRAY, typeof(string[])}
    };
    */

    public static void Parse(ContextScope scope, string text, out RootStatement result, string srcname, ref int linenumber, out List<LintElement> linter)
    {
      using (StringReader reader = new StringReader(text))
      {
        Lexer lex = new Lexer(reader, Definitions, srcname, linenumber);
        result = new RootStatement(scope, lex);
        linenumber = lex.LineNumber;
        linter = lex.Linter;
      }
    }

    public static void Parse(ContextScope scope, string text, out Expression result, string srcname, ref int linenumber, out List<LintElement> linter)
    {
      using (StringReader reader = new StringReader(text))
      {
        Lexer lex = new Lexer(reader, Definitions, srcname, linenumber);
        result = new Expression(scope, lex);
        linenumber = lex.LineNumber;
        linter = lex.Linter;
      }
    }
  }
}


