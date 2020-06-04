using Primrose.Expressions.Tree.Expressions;
using Primrose.Expressions.Tree.Statements;
using Primrose.Primitives.Factories;
using System;
using System.Text;

namespace Primrose.Expressions.Tree
{
  internal static class ExpressionOrder
  {
    public static Registry<Type, Func<ContextScope, Lexer, CExpression>> NextExpression = new Registry<Type, Func<ContextScope, Lexer, CExpression>>();
    public static Registry<Type, Func<ContextScope, Lexer, CStatement>> NextStatement = new Registry<Type, Func<ContextScope, Lexer, CStatement>>();

    static ExpressionOrder()
    {
      NextExpression.Default = (_, __) => { throw new InvalidOperationException("No candidate for next expression found!"); };
      NextStatement.Default = (_, __) => { throw new InvalidOperationException("No candidate for next statement found!"); };

      NextExpression.Add(typeof(Expression), (s, l) => { return new TernaryExpression(s, l); });
      NextExpression.Add(typeof(TernaryExpression), (s, l) => { return new LogicalOrExpression(s, l); });
      NextExpression.Add(typeof(LogicalOrExpression), (s, l) => { return new LogicalAndExpression(s, l); });
      NextExpression.Add(typeof(LogicalAndExpression), (s, l) => { return new EqualityExpression(s, l); });
      NextExpression.Add(typeof(EqualityExpression), (s, l) => { return new RelationalExpression(s, l); });
      NextExpression.Add(typeof(RelationalExpression), (s, l) => { return new AddExpression(s, l); });
      NextExpression.Add(typeof(AddExpression), (s, l) => { return new MultiplyExpression(s, l); });
      NextExpression.Add(typeof(MultiplyExpression), (s, l) => { return new UnaryExpression(s, l); });
      NextExpression.Add(typeof(UnaryExpression), (s, l) => { return new IndexedExpression(s, l); });
      NextExpression.Add(typeof(IndexedExpression), (s, l) => { return new PrimaryExpression(s, l); });

      NextStatement.Add(typeof(Statement), (s, l) => { return new WhileStatement(s, l); });
      NextStatement.Add(typeof(WhileStatement), (s, l) => { return new ForStatement(s, l); });
      NextStatement.Add(typeof(ForStatement), (s, l) => { return new ForEachStatement(s, l); });
      NextStatement.Add(typeof(ForEachStatement), (s, l) => { return new IfThenElseStatement(s, l); });
      NextStatement.Add(typeof(IfThenElseStatement), (s, l) => { return new SingleStatement(s, l); });
      NextStatement.Add(typeof(SingleStatement), (s, l) => { return new AssignmentStatement(s, l); });
    }
  }
}


