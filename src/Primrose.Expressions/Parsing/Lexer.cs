using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Primrose.Expressions
{
  // Reference: https://stackoverflow.com/questions/673113/poor-mans-lexer-for-c-sharp
  internal sealed class Lexer : IDisposable
  {
    private readonly TextReader m_reader;
    private readonly TokenDefinition[] m_tokenDefinitions;
    public string SourceName { get; }
    public string LineText { get; private set; }
    public string TokenContents { get; private set; }
    public int TokenPosition { get; private set; }
    public TokenEnum TokenType { get; private set; }

    public int LineNumber { get; private set; }
    public int Position { get; private set; }
    public LintType Lint { get; private set; }
    public List<LintElement> Linter { get; private set; }

    public bool EndOfStream { get { return m_reader.Peek() == -1 && Position >= (LineText?.Length ?? 0); } } // both current line is done, and there are no more lines to read

    public Lexer(TextReader reader, TokenDefinition[] tokenDefinitions, string srcname, int linenumber)
    {
      SourceName = srcname;
      m_reader = reader;
      m_tokenDefinitions = tokenDefinitions;
      LineNumber = linenumber;
      Lint = LintType.NONE;
      Linter = new List<LintElement>(256);
      nextLine();
      Next();
    }

    private bool nextLine()
    {
      if (m_reader.Peek() == -1) // nothing more to read
      {
        return false;
      }
      do
      {
        LineText = m_reader.ReadLine();
        if (LineNumber > 0)
        {
          Linter?.Add(new LintElement(LineNumber, Position, LintType.NONE));
          Lint = LintType.NONE;
        }

        ++LineNumber;
        Position = 0;
        TokenPosition = 0;
      } while (m_reader.Peek() != -1 && Position >= LineText.Length);
      return true;
    }

    public TokenEnum Peek()
    {
      LookAhead(out TokenEnum token, out string _, out int _, out _);
      return token;
    }

    private int LookAhead(out TokenEnum token, out string content, out int position, out LintType lint)
    {
      if (LineText == null || Position >= LineText.Length) // end of line or end of text
      {
        token = 0;
        content = "";
        position = Position;
        lint = LintType.NONE;
        return 0;
      }
      foreach (var def in m_tokenDefinitions)
      {
        var matched = def.Matcher.Match(LineText, Position);
        if (matched > 0)
        {
          position = Position + matched;
          token = def.Token;
          lint = def.Lint;
          content = LineText.Substring(Position, matched);

          // special case for linting for type 
          //if (lint == LintType.VARIABLE_OR_TYPE && token == TokenEnum.VARIABLE)
          //{
          //  if (Parser.TypeTokens.Contains(content))
          //    lint = LintType.TYPE;
          //  else
          //    lint = LintType.VARIABLE;
          //}

          // whitespace elimination
          if (string.IsNullOrWhiteSpace(content))
          {
            DoNext(matched, token, content, position);
            return LookAhead(out token, out content, out position, out lint);
          }

          // comment elimination
          if (token == TokenEnum.COMMENT)
          {
            Linter.Add(new LintElement(LineNumber, Position, LintType.COMMENT));
            if (!nextLine())
            {
              token = 0;
              content = "";
              lint = LintType.NONE;
              return 0;
            }
            return LookAhead(out token, out content, out position, out lint);
          }

          return matched;
        }
      }
      throw new Exception(Resource.Strings.Error_Lexer_InvalidToken.F(LineNumber, Position, LineText.Substring(Position)));
    }

    public bool Next()
    {
      int matched = LookAhead(out TokenEnum token, out string content, out int position, out LintType lint);
      if (Lint != lint) { Linter.Add(new LintElement(LineNumber, Position, lint)); Lint = lint; }

      return DoNext(matched, token, content, position);
    }

    private bool DoNext(int matched, TokenEnum token, string content, int position)
    {
      TokenType = token;
      TokenContents = content;
      TokenPosition = Position;
      Position = position;
      if (LineText == null || Position >= LineText.Length)
        nextLine();
      return matched > 0;
    }

    public void Dispose() => m_reader.Dispose();
  }
}


