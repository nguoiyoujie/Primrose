using System.Text;

namespace Primrose.Expressions.Tree.Statements
{
  internal class CStatement : IStatement, ITracker
  {
    internal CStatement(ContextScope scope, Lexer lexer)
    {
      SourceName = lexer.SourceName;
      LineNumber = lexer.LineNumber;
      Position = lexer.Position;
    }

    public virtual CStatement Get() { return this; }
    public CStatement GetNext(ContextScope scope, Lexer lexer) { return ExpressionOrder.NextStatement[this.GetType()](scope, lexer).Get(); }
    public virtual bool Evaluate(IContext context, ref Val retval) { return false; }
    public virtual void Write(StringBuilder sb) { sb.Append(ToString()); }
    public string Write() { StringBuilder sb = new StringBuilder(); Write(sb); return sb.ToString(); }

    public string SourceName { get; }
    public int LineNumber { get; }
    public int Position { get; }
  }

  internal interface IStatement
  {
    CStatement Get();
    bool Evaluate(IContext context, ref Val retval);
  }
}