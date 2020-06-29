using Primrose.Primitives;
using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Expressions
{
  internal class ParseException : Exception
  {
    internal ParseException(Lexer lexer) : base(Resource.Strings.Error_ParseException_5
                                                                            .F(lexer.TokenContents.Replace("\n", "")
                                                                            , lexer.SourceName
                                                                            , lexer.LineNumber
                                                                            , lexer.TokenPosition
                                                                            , lexer.LineText))
    { }

    internal ParseException(Lexer lexer, TokenEnum expected) : base(Resource.Strings.Error_ParseException_6
                                                                              .F(lexer.TokenContents.Replace("\n", "")
                                                                              , lexer.SourceName
                                                                              , lexer.LineNumber
                                                                              , lexer.TokenPosition
                                                                              , expected.GetEnumName()
                                                                              , lexer.LineText))
    { }

    internal ParseException(Lexer lexer, string message) : base(Resource.Strings.Error_ParseException_5M
                                                                          .F(message
                                                                          , lexer.SourceName
                                                                          , lexer.LineNumber
                                                                          , lexer.TokenPosition
                                                                          , lexer.LineText))
    { }
  }
}


