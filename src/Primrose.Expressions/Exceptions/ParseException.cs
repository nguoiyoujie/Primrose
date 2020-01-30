using Primrose.Primitives;
using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Expressions
{
  internal class ParseException : Exception
  {
    internal ParseException(Lexer lexer) : base("Unexpected token '{0}' found in function '{1}' line {2}:{3}.\nLine: {4}"
                                                                            .F(lexer.TokenContents.Replace("\n", "")
                                                                            , lexer.SourceName
                                                                            , lexer.LineNumber
                                                                            , lexer.TokenPosition
                                                                            , lexer.LineText))
    { }

    internal ParseException(Lexer lexer, TokenEnum expected) : base("Unexpected token '{0}' found in function '{1}' at line {2}:{3}. Expected: {4}.\nLine: {5}"
                                                                              .F(lexer.TokenContents.Replace("\n", "")
                                                                              , lexer.SourceName
                                                                              , lexer.LineNumber
                                                                              , lexer.TokenPosition
                                                                              , expected.GetEnumName()
                                                                              , lexer.LineText))
    { }

    internal ParseException(Lexer lexer, string message) : base("{0}\nFunction '{1}' at line {2}:{3}. \nLine: {4}"
                                                                          .F(message
                                                                          , lexer.SourceName
                                                                          , lexer.LineNumber
                                                                          , lexer.TokenPosition
                                                                          , lexer.LineText))
    { }
  }
}


