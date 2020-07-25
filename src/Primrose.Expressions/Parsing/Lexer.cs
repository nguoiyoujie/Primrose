using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;
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

    private string lineRemaining;
    public bool EndOfStream { get { return lineRemaining == null; } }

    public Lexer(TextReader reader, TokenDefinition[] tokenDefinitions, string srcname, int linenumber)
    {
      SourceName = srcname;
      m_reader = reader;
      m_tokenDefinitions = tokenDefinitions;
      LineNumber = linenumber;
      Lint = LintType.NONE;
      Linter = new List<LintElement>();
      nextLine();
      Next();
    }

    private void nextLine()
    {
      do
      {
        lineRemaining = m_reader.ReadLine();
        LineText = lineRemaining;
        if (LineNumber > 0)
        {
          Linter.Add(new LintElement(LineNumber, Position, LintType.NONE));
          Lint = LintType.NONE;
        }

        ++LineNumber;
        Position = 0;
        TokenPosition = 0;
      } while (lineRemaining != null && lineRemaining.Length == 0);
    }

    public TokenEnum Peek()
    {
      LookAhead(out TokenEnum token, out string _, out int _, out _);
      return token;
    }

    private int LookAhead(out TokenEnum token, out string content, out int position, out LintType lint)
    {
      if (lineRemaining == null)
      {
        token = 0;
        content = "";
        position = Position;
        lint = LintType.NONE;
        return 0;
      }
      foreach (var def in m_tokenDefinitions)
      {
        var matched = def.Matcher.Match(lineRemaining);
        if (matched > 0)
        {
          position = Position + matched;
          token = def.Token;
          lint = def.Lint;
          content = lineRemaining.Substring(0, matched);

          // whitespace elimination
          if (content.Trim().Length == 0)
          {
            DoNext(matched, token, content, position);
            return LookAhead(out token, out content, out position, out lint);
          }

          // comment elimination
          if (token == TokenEnum.COMMENT)
          {
            Linter.Add(new LintElement(LineNumber, Position, LintType.COMMENT));
            nextLine();
            return LookAhead(out token, out content, out position, out lint);
          }

          return matched;
        }
      }
      throw new Exception(Resource.Strings.Error_Lexer_InvalidToken.F(LineNumber, Position, lineRemaining));
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

      if (matched > 0)
      {
        lineRemaining = lineRemaining.Substring(matched);
        if (lineRemaining.Length == 0)
          nextLine();
        return true;
      }
      return false;
    }

    public void Dispose() => m_reader.Dispose();
  }
}


