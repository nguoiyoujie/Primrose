using Primrose.Expressions.Tree.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Primrose.Expressions.Tree.Statements
{
  internal class ForEachStatement : CStatement
  {
    private readonly ContextScope _scope;
    private readonly CExpression _enumerable;
    private readonly DeclVariable _var;
    private readonly List<CStatement> _actions = new List<CStatement>();

    internal ForEachStatement(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // FOREACH ( DECL VAR IN EXPR ) STATEMENT 
      // FOREACH ( DECL VAR IN EXPR ) { STATEMENT STATEMENT STATEMENT ... } 
      // or
      // ASSIGNMENTEXPR

      if (lexer.TokenType == TokenEnum.FOREACH)
      {
        lexer.Next(); //FOREACH
        if (lexer.TokenType != TokenEnum.BRACKETOPEN)
          throw new ParseException(lexer, TokenEnum.BRACKETOPEN);
        lexer.Next(); //BRACKETOPEN

        _scope = scope.Next;
        _var = new DeclVariable(_scope, lexer);

        if (lexer.TokenType != TokenEnum.IN)
          throw new ParseException(lexer, TokenEnum.IN);
        lexer.Next(); //IN

        _enumerable = new Expression(_scope, lexer).Get();

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
      if (_enumerable == null)
        return _actions[0];
      return this;
    }

    public override void Evaluate(IContext context)
    {
      if (_enumerable != null)
      {
        Val array = _enumerable.Evaluate(context);
        Array a = array.Cast<Array>();

        foreach (object o in a)
        {
          _scope.SetVar(this, _var.varName, new Val(o));
          foreach (CStatement s in _actions)
            s.Evaluate(context);
        }
      }
    }

    public override void Write(StringBuilder sb)
    {
      TokenEnum.FOREACH.Write(sb, Writer.Padding.SUFFIX);
      TokenEnum.BRACKETOPEN.Write(sb);
      _var.Write(sb);
      TokenEnum.IN.Write(sb, Writer.Padding.BOTH);
      _enumerable.Write(sb);
      TokenEnum.BRACKETCLOSE.Write(sb);

      TokenEnum.BRACEOPEN.Write(sb, Writer.Padding.BOTH);
      foreach (CStatement s in _actions)
        s.Write(sb);
      TokenEnum.BRACECLOSE.Write(sb);
    }
  }
}