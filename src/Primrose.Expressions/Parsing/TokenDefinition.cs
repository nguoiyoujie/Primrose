using System.Text.RegularExpressions;

namespace Primrose.Expressions
{
  // Reference https://stackoverflow.com/questions/673113/poor-mans-lexer-for-c-sharp
  internal sealed class TokenDefinition
  {
    internal readonly IMatcher Matcher;
    public readonly TokenEnum Token;
    public readonly LintType Lint;

    public TokenDefinition(string regex, TokenEnum token, LintType lint)
    {
      Matcher = new RegexMatcher(regex, RegexOptions.CultureInvariant);
      Token = token;
      Lint = lint;
    }
  }
}


