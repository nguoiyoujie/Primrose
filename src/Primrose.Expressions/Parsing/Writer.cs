﻿using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using System;
using System.Text;

namespace Primrose.Expressions
{
  internal static class Writer
  {
    public static Registry<TokenEnum, string> Definitions = new Registry<TokenEnum, string>();

    [Flags]
    public enum Padding
    {
      NONE = 0,
      PREFIX = 1,
      SUFFIX = 2,
      BOTH = PREFIX | SUFFIX,
    }

    static Writer()
    {
      Definitions.Default = "<???>";
      Definitions.Add(TokenEnum.WHITESPACE, " ");

      Definitions.Add(TokenEnum.DECL_BOOL, "bool");
      Definitions.Add(TokenEnum.DECL_FLOAT, "float");
      Definitions.Add(TokenEnum.DECL_INT, "int");
      Definitions.Add(TokenEnum.DECL_STRING, "string");
      Definitions.Add(TokenEnum.DECL_FLOAT2, "float2");
      Definitions.Add(TokenEnum.DECL_FLOAT3, "float3");
      Definitions.Add(TokenEnum.DECL_FLOAT4, "float4");
      Definitions.Add(TokenEnum.DECL_BOOL_ARRAY, "bool[]");
      Definitions.Add(TokenEnum.DECL_FLOAT_ARRAY, "float[]");
      Definitions.Add(TokenEnum.DECL_STRING_ARRAY, "string[]");
      Definitions.Add(TokenEnum.DECL_INT_ARRAY, "int[]");

      Definitions.Add(TokenEnum.IF, "if");
      Definitions.Add(TokenEnum.THEN, "then");
      Definitions.Add(TokenEnum.ELSE, "else");
      Definitions.Add(TokenEnum.FOREACH, "foreach");
      Definitions.Add(TokenEnum.IN, "in");
      Definitions.Add(TokenEnum.FOR, "for");
      Definitions.Add(TokenEnum.WHILE, "while");

      Definitions.Add(TokenEnum.NULLLITERAL, "<null>");
      Definitions.Add(TokenEnum.BOOLEANLITERAL, "<bool>");
      Definitions.Add(TokenEnum.FUNCTION, "<func>");
      Definitions.Add(TokenEnum.VARIABLE, "<variable>");
      Definitions.Add(TokenEnum.STRINGLITERAL, "<string>");
      Definitions.Add(TokenEnum.HEXINTEGERLITERAL, "<hex_int>");
      Definitions.Add(TokenEnum.DECIMALINTEGERLITERAL, "<int>");
      Definitions.Add(TokenEnum.REALLITERAL, "<float>");

      Definitions.Add(TokenEnum.BRACEOPEN, "{");
      Definitions.Add(TokenEnum.BRACECLOSE, "}");
      Definitions.Add(TokenEnum.BRACKETOPEN, "(");
      Definitions.Add(TokenEnum.BRACKETCLOSE,")");
      Definitions.Add(TokenEnum.SQBRACKETOPEN, "[");
      Definitions.Add(TokenEnum.SQBRACKETCLOSE, "]");

      Definitions.Add(TokenEnum.COMMENT, "//<comment>");

      Definitions.Add(TokenEnum.PLUSPLUS, "++");
      Definitions.Add(TokenEnum.MINUSMINUS, "--");
      Definitions.Add(TokenEnum.PIPEPIPE, "||");
      Definitions.Add(TokenEnum.AMPAMP, "&&");
      Definitions.Add(TokenEnum.NOTEQUAL, "!=");
      Definitions.Add(TokenEnum.EQUAL, "==");
      Definitions.Add(TokenEnum.LESSEQUAL, "<=");
      Definitions.Add(TokenEnum.GREATEREQUAL, ">=");

      Definitions.Add(TokenEnum.PLUSASSIGN, "+=");
      Definitions.Add(TokenEnum.MINUSASSIGN, "-=");
      Definitions.Add(TokenEnum.ASTERISKASSIGN, "*=");
      Definitions.Add(TokenEnum.SLASHASSIGN, "/=");
      Definitions.Add(TokenEnum.PERCENTASSIGN, "%=");
      Definitions.Add(TokenEnum.AMPASSIGN, "&=");
      Definitions.Add(TokenEnum.PIPEASSIGN, "|=");

      Definitions.Add(TokenEnum.ASSIGN, "=");
      Definitions.Add(TokenEnum.SEMICOLON, ";");
      Definitions.Add(TokenEnum.AMP, "&");
      Definitions.Add(TokenEnum.PLUS, "+");
      Definitions.Add(TokenEnum.MINUS, "-");
      Definitions.Add(TokenEnum.NOT, "!");
      Definitions.Add(TokenEnum.ASTERISK, "*");
      Definitions.Add(TokenEnum.SLASH, "/");
      Definitions.Add(TokenEnum.PERCENT, "%");
      Definitions.Add(TokenEnum.QUESTIONMARK, "?");
      Definitions.Add(TokenEnum.COMMA, ",");
      Definitions.Add(TokenEnum.LESSTHAN, "<");
      Definitions.Add(TokenEnum.GREATERTHAN, ">");
      Definitions.Add(TokenEnum.COLON, ":");
    }

    public static string Write(TokenEnum token, Padding padding = Padding.NONE)
    {
      switch (padding)
      {
        case Padding.PREFIX:
          return " " + Definitions[token];

        case Padding.SUFFIX:
          return Definitions[token] + " ";

        case Padding.BOTH:
          return " " + Definitions[token] + " ";

        default:
          return Definitions[token];
      }
    }

    public static void Write(this TokenEnum token, StringBuilder sb, Padding padding = Padding.NONE)
    {
      if ((padding & Padding.PREFIX) > 0)
        sb.Append(" ");
      sb.Append(Definitions[token]);
      if ((padding & Padding.SUFFIX) > 0)
        sb.Append(" ");
    }
  }
}

