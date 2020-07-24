using System;

namespace Primrose.Expressions.Tree.Expressions.Literals
{
  internal class SingleLiteral : CLiteral
  {
    private readonly float _value;

    internal SingleLiteral(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      _value = Convert.ToSingle(lexer.TokenContents);
      lexer.Next(); //FLOAT
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