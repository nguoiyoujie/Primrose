﻿using System.Text;

namespace Primrose.Expressions.Tree.Expressions
{
  internal class CLiteral : CExpression
  {
    internal CLiteral(ContextScope scope, Lexer lexer) : base(scope, lexer) { }
  }

  internal class CExpression : IExpression, ITracker
  {
    internal CExpression(ContextScope scope, Lexer lexer) { SourceName = lexer.SourceName; LineNumber = lexer.LineNumber; Position = lexer.Position; }
    public virtual CExpression Get() { return this; }
    public virtual Val Evaluate(IContext context) { return default(Val); }
    public virtual void Write(StringBuilder sb) { sb.Append(ToString()); }
    public string Write() { StringBuilder sb = new StringBuilder(); Write(sb); return sb.ToString(); }

    public string SourceName { get; }
    public int LineNumber { get; }
    public int Position { get; }
  }

  internal interface IExpression
  {
    CExpression Get();
    Val Evaluate(IContext context);
  }
}
