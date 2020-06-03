﻿using Primrose.Expressions.Tree.Expressions;
using System.Collections.Generic;
using System.Text;

namespace Primrose.Expressions.Tree.Statements
{
  internal class WhileStatement : CStatement
  {
    private CExpression _condition;
    private List<CStatement> _actions = new List<CStatement>();

    internal WhileStatement(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // WHILE ( EXPR ) STATEMENT 
      // WHILE ( EXPR ) { STATEMENT STATEMENT STATEMENT ... } 
      // or
      // 

      if (lexer.TokenType == TokenEnum.WHILE)
      {
        lexer.Next(); //WHILE
        if (lexer.TokenType != TokenEnum.BRACKETOPEN)
          throw new ParseException(lexer, TokenEnum.BRACKETOPEN);
        lexer.Next(); //BRACKETOPEN

        _condition = new Expression(scope, lexer).Get();

        if (lexer.TokenType != TokenEnum.BRACKETCLOSE)
          throw new ParseException(lexer, TokenEnum.BRACKETCLOSE);
        lexer.Next(); //BRACKETCLOSE

        if (lexer.TokenType == TokenEnum.BRACEOPEN)
        {
          lexer.Next(); //BRACEOPEN
          while (lexer.TokenType != TokenEnum.BRACECLOSE)
            _actions.Add(new Statement(scope, lexer).Get());
          lexer.Next(); //BRACECLOSE
        }
        else
        {
          _actions.Add(new Statement(scope, lexer).Get());
        }
      }
      else
      {
        _actions.Add(new ForStatement(scope, lexer).Get());
      }
    }

    public override CStatement Get()
    {
      if (_condition == null)
        return _actions[0];
      return this;
    }

    public override void Evaluate(IContext context)
    {
      while (_condition.Evaluate(context).IsTrue)
      {
        foreach (CStatement s in _actions)
          s.Evaluate(context);
      }
    }

    public override void Write(StringBuilder sb)
    {
      TokenEnum.WHILE.Write(sb);
      TokenEnum.BRACKETOPEN.Write(sb);
      _condition.Write(sb);
      TokenEnum.BRACKETCLOSE.Write(sb);

      TokenEnum.BRACEOPEN.Write(sb, Writer.Padding.BOTH);
      foreach (CStatement s in _actions)
        s.Write(sb);
      TokenEnum.BRACECLOSE.Write(sb);
    }
  }
}