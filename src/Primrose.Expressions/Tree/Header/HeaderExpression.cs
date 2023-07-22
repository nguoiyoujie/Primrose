using Primrose.Expressions.Tree.Expressions;
using System.Collections.Generic;
using System.Text;

namespace Primrose.Expressions.Tree.Header
{
  internal class HeaderExpression : CExpression
  {
    public string Name;
    public List<DeclVariable> Variables = new List<DeclVariable>();

    internal HeaderExpression(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // NAME(VARIABLE, VAIRABLE, ...):

      TokenEnum _type = lexer.TokenType;
      if (_type == TokenEnum.FUNCTION || _type == TokenEnum.VARIABLE)
      {
        Name = lexer.TokenContents;
        lexer.Linter.Add(new LintElement(LineNumber, Position, LintType.HEADER));
        lexer.Next(); //FUNCTION / VARIABLE
      }
      else
      {
        throw new ParseException(lexer);
      }

      _type = lexer.TokenType;
      if (_type == TokenEnum.BRACKETOPEN)
      {
        lexer.Next(); //BRACKETOPEN
        Variables.Add(new DeclVariable(scope, lexer, true));

        while (lexer.TokenType == TokenEnum.COMMA)
        {
          lexer.Next(); //COMMA
          Variables.Add(new DeclVariable(scope, lexer, true));
        }
        lexer.Next(); //BRACKETCLOSE
      }

      _type = lexer.TokenType;
      if (_type != TokenEnum.COLON)
      {
        throw new ParseException(lexer);
      }
    }

    public override Val Evaluate(IContext context)
    {
      return Val.NULL;
    }

    public override void Write(StringBuilder sb)
    {
      sb.Append(Name);
      if (Variables.Count > 0)
      {
        TokenEnum.BRACKETOPEN.Write(sb, Writer.Padding.NONE);
        for (int i = 0; i < Variables.Count; i++)
        {
          Variables[i].Write(sb);
          if (i != Variables.Count - 1)
          {
            TokenEnum.COMMA.Write(sb, Writer.Padding.SUFFIX);
          }
        }
        TokenEnum.BRACKETCLOSE.Write(sb, Writer.Padding.NONE);
      }
      TokenEnum.COLON.Write(sb, Writer.Padding.NONE);
    }
  }
}