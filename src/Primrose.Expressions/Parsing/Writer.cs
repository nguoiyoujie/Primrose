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
      Definitions.Default = Resource.Strings.Unknown;
      Definitions.Add(TokenEnum.WHITESPACE, " ");

      Definitions.Add(TokenEnum.RETURN, "return");
      Definitions.Add(TokenEnum.NEW, "new");
      Definitions.Add(TokenEnum.IF, "if");
      Definitions.Add(TokenEnum.THEN, "then");
      Definitions.Add(TokenEnum.ELSE, "else");
      Definitions.Add(TokenEnum.FOREACH, "foreach");
      Definitions.Add(TokenEnum.IN, "in");
      Definitions.Add(TokenEnum.FOR, "for");
      Definitions.Add(TokenEnum.WHILE, "while");

      Definitions.Add(TokenEnum.TYPE, Resource.Strings.Type);

      Definitions.Add(TokenEnum.NULLLITERAL, Resource.Strings.Null);
      Definitions.Add(TokenEnum.BOOLEANLITERAL, Resource.Strings.Bool);
      Definitions.Add(TokenEnum.FUNCTION, Resource.Strings.Function);
      Definitions.Add(TokenEnum.VARIABLE, Resource.Strings.Variable);
      Definitions.Add(TokenEnum.STRINGLITERAL, Resource.Strings.String);
      Definitions.Add(TokenEnum.HEXINTEGERLITERAL, Resource.Strings.HexInt);
      Definitions.Add(TokenEnum.DECIMALINTEGERLITERAL, Resource.Strings.Int);
      Definitions.Add(TokenEnum.REALLITERAL, Resource.Strings.Float);

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

    public static string Write(this TokenEnum token, Padding padding = Padding.NONE)
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


