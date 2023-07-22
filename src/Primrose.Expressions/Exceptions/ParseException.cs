using Primrose.Primitives.Cache;
using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Expressions
{
  internal class ParseException : Exception
  {
    // add TokenPosition with 1 because Col are one-based. Line is already incremented with 1
    internal ParseException(Lexer lexer) : base(Resource.Strings.Error_ParseException_5
                                                                            .F(lexer.TokenContents.Replace("\n", "")
                                                                            , lexer.SourceName
                                                                            , lexer.LineNumber
                                                                            , lexer.TokenPosition + 1
                                                                            , lexer.LineText))
    { }

    internal ParseException(Lexer lexer, TokenEnum expected) : base(Resource.Strings.Error_ParseException_6
                                                                              .F(lexer.TokenContents.Replace("\n", "")
                                                                              , lexer.SourceName
                                                                              , lexer.LineNumber
                                                                              , lexer.TokenPosition + 1
                                                                              , Enum<TokenEnum>.GetName(expected)
                                                                              , lexer.LineText))
    { }

    internal ParseException(Lexer lexer, string message) : base(Resource.Strings.Error_ParseException_5M
                                                                          .F(message
                                                                          , lexer.SourceName
                                                                          , lexer.LineNumber
                                                                          , lexer.TokenPosition + 1
                                                                          , lexer.LineText))
    { }
  }
}


