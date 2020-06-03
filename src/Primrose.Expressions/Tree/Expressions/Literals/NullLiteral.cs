namespace Primrose.Expressions.Tree.Expressions.Literals
{
  internal class NullLiteral : CLiteral
  {
    internal NullLiteral(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      lexer.Next(); //NULL
    }

    public override Val Evaluate(IContext context)
    {
      return Val.NULL;
    }

    public override string ToString()
    {
      return "null";
    }
  }
}
