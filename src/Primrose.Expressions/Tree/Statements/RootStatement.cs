using System.Collections.Generic;
using System.Text;

namespace Primrose.Expressions.Tree.Statements
{
  internal class RootStatement : CStatement
  {
    public List<CStatement> Statements = new List<CStatement>();

    internal RootStatement(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      // STATEMENT STATEMENT ...

      while (!lexer.EndOfStream)
      {
        Statements.Add(new Statement(scope, lexer).Get());
      }
    }

    public override void Evaluate(IContext context)
    {
      foreach (Statement s in Statements)
      {
        s.Evaluate(context);
      }
    }

    public override void Write(StringBuilder sb)
    {
      foreach (Statement s in Statements)
      {
        s.Write(sb);
        sb.AppendLine();
      }
    }
  }
}
