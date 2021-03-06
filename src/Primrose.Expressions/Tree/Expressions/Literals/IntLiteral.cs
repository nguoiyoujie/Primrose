﻿using System;

namespace Primrose.Expressions.Tree.Expressions.Literals
{
  internal class IntLiteral : CLiteral
  {
    private readonly int _value;

    internal IntLiteral(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      _value = Convert.ToInt32(lexer.TokenContents);
      lexer.Next(); //INT
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
