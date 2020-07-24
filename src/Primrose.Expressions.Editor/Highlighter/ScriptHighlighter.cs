using Primrose.Primitives.ValueTypes;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Primrose.Expressions.Editor
{
  public class ScriptHighlighter : AHighlighter
  {
    public static ScriptHighlighter Instance = new ScriptHighlighter();
    private IContext _context;
    public IContext Context
    {
      get { return _context; }
      set
      {
        if (_context != value)
        {
          _context = value;
          SetContext(value);
        }
      }
    }

    readonly RegexOptions ropt = RegexOptions.CultureInvariant | RegexOptions.Compiled;

    private ScriptHighlighter()
    {
      ResetRegex();
    }

    private void ResetRegex()
    {
      Regexes.Clear();

      Regexes.Add(new Pair<Regex, Color>(new Regex(@"//.*", ropt), Color.ForestGreen));
      Regexes.Add(new Pair<Regex, Color>(new Regex(@"^.*\:(?=\s*)$", ropt), Color.Indigo));
      //Regexes.Add(new Pair<Regex, Color>(new Regex(@"[a-zA-Z_][a-zA-Z0-9_\.]*", ropt), Color.));
      Regexes.Add(new Pair<Regex, Color>(new Regex(@"\""(\""\""|[^\""])*\""", ropt), Color.Brown));
      Regexes.Add(new Pair<Regex, Color>(new Regex(@"\b(bool|float|float2|float3|float4|int|string)\b", ropt), Color.DodgerBlue));
      Regexes.Add(new Pair<Regex, Color>(new Regex(@"\b(true|false|null|if|then|else|while|foreach|in|for)\b", ropt), Color.Blue));
    }

    private void SetContext(IContext c)
    {
      ResetRegex();

      if (c != null && c.ValFuncRef.Count > 0)
      {
        string reg = string.Concat("(", string.Join("|", c.ValFuncRef), @")(?=\s*\()");
        if (reg != null)
          Regexes.Add(new Pair<Regex, Color>(new Regex(reg, ropt), Color.CadetBlue));
      }
    }
  }
}
