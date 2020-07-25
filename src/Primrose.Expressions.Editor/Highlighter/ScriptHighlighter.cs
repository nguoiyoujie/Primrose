using Primrose.Expressions.Editor.Checker;
using Primrose.Expressions.Editor.Util;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.ValueTypes;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Primrose.Expressions.Editor
{
  public class ScriptHighlighter : IHighlighter
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
        }
      }
    }

    protected Registry<LintType, Color> LintColors = new Registry<LintType, Color>();

    private ScriptHighlighter()
    {
      LintColors.Default = Color.Black;
      LintColors.Put(LintType.HEADER, Color.DarkOliveGreen);
      LintColors.Put(LintType.COMMENT, Color.ForestGreen);
      LintColors.Put(LintType.KEYWORD, Color.Blue);
      LintColors.Put(LintType.TYPE, Color.DodgerBlue);
      LintColors.Put(LintType.FUNCTION, Color.CadetBlue);
      LintColors.Put(LintType.VARIABLE, Color.Indigo);
      LintColors.Put(LintType.STRINGLITERAL, Color.Brown);
      LintColors.Put(LintType.NUMERICLITERAL, Color.DarkOrange);
      LintColors.Put(LintType.SPECIALLITERAL, Color.DarkRed);
    }

    /*
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
    */

    public string MakeRTF(string[] lines, SortedList<Pair<int, int>, LintType> lints)
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Consolas;}}\r\n");

      Color[] colors = LintColors.GetValues();
      sb.Append("{\\colortbl ;");
      foreach (Color c in colors)
      {
        sb.Append("\\red{0}\\green{1}\\blue{2};".F(c.R, c.G, c.B));
      }
      sb.Append("}\r\n");
      
      sb.Append("\\viewkind4\\uc1\\pard\\cf0\\f0\\fs17");

      List<LintType> lintdef = new List<LintType>(LintColors.GetKeys());

      LintType prev = LintType.NONE;
      LintElement current = new LintElement(0, 0, LintType.NONE);
      foreach (Pair<int, int> pos in lints.Keys)
      {
        LintElement lint = new LintElement(pos.t, pos.u, lints[pos]);
        if (current.Line > 0)
        {
          if (current.Line > lint.Line || (current.Line == lint.Line && current.Position >= lint.Position))
            continue;

          while (current.Line < lint.Line)
          {
            int index = lintdef.IndexOf(current.Lint);
            index++;
            sb.Append("\\cf" + index + " ");
            sb.Append(lines[current.Line - 1].Substring(current.Position)
                                             .Replace("{", "\\{")
                                             .Replace("}", "\\}")
                                             .Replace("\t", "\\tab")
                                             .Replace("\r\n", "\\par\r\n")
                                             .Replace("\r", "\\par\r\n")
                                             .Replace("\n", "\\par\r\n"));
            sb.Append("\\par\r\n");

            current = new LintElement(current.Line + 1, 0, LintType.NONE);
          }
         
          if (current.Line == lint.Line && current.Position < lint.Position)
          {
            if (current.Lint != prev)
            {
              int index = lintdef.IndexOf(current.Lint);
              index++;
              sb.Append("\\cf" + index + " ");
              sb.Append(lines[current.Line - 1].Substring(current.Position, lint.Position - current.Position)
                                               .Replace("{", "\\{")
                                               .Replace("}", "\\}")
                                               .Replace("\t", "\\tab ")
                                               .Replace("\r\n", "\\par\r\n")
                                               .Replace("\r", "\\par\r\n")
                                               .Replace("\n", "\\par\r\n")
                                               );
            }
            else
            {
              sb.Append(lines[current.Line - 1].Substring(current.Position, lint.Position - current.Position)
                                 .Replace("{", "\\{")
                                 .Replace("}", "\\}")
                                 .Replace("\t", "\\tab ")
                                 .Replace("\r\n", "\\par\r\n")
                                 .Replace("\r", "\\par\r\n")
                                 .Replace("\n", "\\par\r\n")
                                 );
            }
          }

        }
        prev = current.Lint;
        current = lint;
      }
      for (int i = current.Line; i < lines.Length + 1; i++)
      {
        sb.Append("\\par\r\n");
      }

      sb.Append("}\r\n");

      return sb.ToString();
    }

    public void Highlight(FlickerFreeRichEditTextBox box)
    {
      box._ignore = true;
      box._Paint = false;
      box.SuspendLayout();
      box.SuspendDrawing();

      int p0 = box.SelectionStart;
      int p1 = box.SelectionLength;

      box.SelectAll();
      box.SelectionColor = Color.Black;

      if (_context != null)
      {
        string txt = box.Text;
        try
        {
          ScriptChecker checker = new ScriptChecker(txt, null);
          SortedList<Pair<int, int>, LintType> lints = checker.Lint(_context);
          string[] lines = box.Lines;
          if (lines.Length > 0)
          {
            box.Rtf = MakeRTF(lines, lints);
            /*
            LinterType prev = LinterType.NONE;
            LinterStep current = new LinterStep(0, 0, LinterType.NONE);
            foreach (LinterStep lint in lints)
            {
              if (current.Line <= lint.Line && current.Position < lint.Position)
              {
                if (current.Lint != prev)
                {
                  box.Select(box.GetFirstCharIndexFromLine(current.Line - 1) + current.Position, lint.Position - current.Position);
                  if (box.SelectionColor == Color.Black)
                    box.SelectionColor = LintColors[current.Lint];
                }
              }
              prev = current.Lint;
              current = lint;
            }
            */
          }
        }
        catch
        {
          box.Text = txt;
        }
      }

      box.SelectionStart = p0;
      box.SelectionLength = p1;

      box._Paint = true;
      box.ResumeLayout();
      box.ResumeDrawing();
      box._ignore = false;
    }

    public void Highlight(FlickerFreeRichEditTextBox box, int line) { }
  }
}
