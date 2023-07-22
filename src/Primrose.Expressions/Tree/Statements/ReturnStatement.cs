using Primrose.Expressions.Tree.Expressions;
using System.Text;

namespace Primrose.Expressions.Tree.Statements
{
  internal class ReturnStatement : CStatement
  {
    private readonly CExpression _value;
    private readonly CStatement _statement;

    internal ReturnStatement(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // RETURN EXPR;
      // or
      // ASSIGNMENTEXPR (GetNext)
      if (lexer.TokenType == TokenEnum.RETURN)
      {
        lexer.Next();
        _value = new Expression(scope, lexer).Get();
        lexer.Next(); // semicolon
      }
      else
      {
        _statement = GetNext(scope, lexer);
      }
    }

    public override CStatement Get()
    {
      return _statement?.Get() ?? this;
    }

    public override bool Evaluate(IContext context, ref Val retval)
    {
      if (_value != null)
      {
        retval = _value.Evaluate(context);
        return true;
      }
      else
      {
        return _statement.Evaluate(context, ref retval);
      }
    }

    public override void Write(StringBuilder sb)
    {
      TokenEnum.RETURN.Write(sb, Writer.Padding.SUFFIX);
      _value.Write(sb);

      // statement termination
      TokenEnum.SEMICOLON.Write(sb, Writer.Padding.SUFFIX);
    }
  }
}