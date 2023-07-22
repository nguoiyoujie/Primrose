using System.Text.RegularExpressions;

namespace Primrose.Expressions
{
  // Reference https://stackoverflow.com/questions/673113/poor-mans-lexer-for-c-sharp
  internal sealed class RegexMatcher : IMatcher
  {
    private readonly Regex m_regex;
    public RegexMatcher(string regex, RegexOptions options) { m_regex = new Regex(string.Format(@"\G({0})", regex), options); }

    public int Match(string text, int startposition)
    {
      var m = m_regex.Match(text, startposition);
      return m.Success ? m.Length : 0;
    }
    public override string ToString() => m_regex.ToString();
  }
}


