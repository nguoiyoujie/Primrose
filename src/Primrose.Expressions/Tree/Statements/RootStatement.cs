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

    public override bool Evaluate(IContext context, ref Val retval)
    {
      foreach (CStatement s in Statements)
        if (s.Evaluate(context, ref retval))
          return true;
      return false;
    }

    public override void Write(StringBuilder sb)
    {
      foreach (CStatement s in Statements)
      {
        s.Write(sb);
        sb.AppendLine();
      }
    }
  }
}
