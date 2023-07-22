using Primrose.Expressions.Tree.Expressions;
using System.Collections.Generic;
using System.Text;

namespace Primrose.Expressions.Tree.Statements
{
  internal class ForStatement : CStatement
  {
    private readonly ContextScope _scope;
    private readonly CStatement _begin;
    private readonly CExpression _condition;
    private readonly CExpression _next;
    private readonly List<CStatement> _actions = new List<CStatement>();

    internal ForStatement(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // FOR ( STATEMENT; CONDEXPR; EXPR ) STATEMENT 
      // FOR ( STATEMENT; CONDEXPR; EXPR ) { STATEMENT STATEMENT STATEMENT ... } 
      // or
      // ASSIGNMENTEXPR (GetNext)

      if (lexer.TokenType == TokenEnum.FOR)
      {
        lexer.Next(); //FOR
        if (lexer.TokenType != TokenEnum.BRACKETOPEN)
          throw new ParseException(lexer, TokenEnum.BRACKETOPEN);
        lexer.Next(); //BRACKETOPEN

        _scope = scope.Next;
        _begin = new SingleStatement(_scope, lexer).Get();

        _condition = new Expression(_scope, lexer).Get();

        if (lexer.TokenType != TokenEnum.SEMICOLON)
          throw new ParseException(lexer, TokenEnum.SEMICOLON);
        lexer.Next(); //SEMICOLON

        _next = new AssignmentExpression(_scope, lexer).Get();

        if (lexer.TokenType != TokenEnum.BRACKETCLOSE)
          throw new ParseException(lexer, TokenEnum.BRACKETCLOSE);
        lexer.Next(); //BRACKETCLOSE

        if (lexer.TokenType == TokenEnum.BRACEOPEN)
        {
          lexer.Next(); //BRACEOPEN
          while (lexer.TokenType != TokenEnum.BRACECLOSE)
            _actions.Add(new Statement(_scope, lexer).Get());
          lexer.Next(); //BRACECLOSE
        }
        else
        {
          _actions.Add(new Statement(scope, lexer).Get());
        }
      }
      else
      {
        _actions.Add(GetNext(scope, lexer));
      }
    }

    public override CStatement Get()
    {
      if (_condition == null)
        return _actions[0].Get();
      return this;
    }

    public override bool Evaluate(IContext context, ref Val retval)
    {
      if (_condition != null)
      {
        if (_begin.Evaluate(context, ref retval)) // it should be illegal to put a return statement here...
          return true;

        while (_condition.Evaluate(context).IsTrue)
        {
          foreach (CStatement s in _actions)
            if (s.Evaluate(context, ref retval))
              return true;

          _next.Evaluate(context);
        }
        return false;
      }

      foreach (CStatement s in _actions)
        if (s.Evaluate(context, ref retval))
          return true;
      return false;
    }

    public override void Write(StringBuilder sb)
    {
      TokenEnum.FOR.Write(sb, Writer.Padding.SUFFIX);
      TokenEnum.BRACKETOPEN.Write(sb);
      _begin.Write(sb);
      _condition.Write(sb);
      TokenEnum.SEMICOLON.Write(sb, Writer.Padding.SUFFIX);
      _next.Write(sb);
      TokenEnum.BRACKETCLOSE.Write(sb);

      TokenEnum.BRACEOPEN.Write(sb, Writer.Padding.BOTH);
      foreach (CStatement s in _actions)
        s.Write(sb);
      TokenEnum.BRACECLOSE.Write(sb);
    }
  }
}