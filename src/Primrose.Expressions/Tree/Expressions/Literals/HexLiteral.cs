﻿using System;

namespace Primrose.Expressions.Tree.Expressions.Literals
{
  internal class HexLiteral : CLiteral
  {
    private readonly int _value;

    internal HexLiteral(ContextScope scope, Lexer lexer) : base(scope, lexer)
    {
      _value = Convert.ToInt32(lexer.TokenContents.Substring(2, lexer.TokenContents.Length - 2), 16);
      lexer.Next(); //HEXINT
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
