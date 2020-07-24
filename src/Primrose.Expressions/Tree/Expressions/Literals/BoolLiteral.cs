using Primrose.Primitives.Extensions;

namespace Primrose.Expressions.Tree.Expressions.Literals
{
  internal class BoolLiteral : CLiteral
  {
    // true or false
    private readonly bool _value;

    internal BoolLiteral(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      _value = lexer.TokenContents.ToBool();
      lexer.Next(); //BOOL
    }

    public override Val Evaluate(IContext context)
    {
      return new Val(_value);
    }

    public override string ToString()
    {
      return _value.ToString();
    }
  }
}
