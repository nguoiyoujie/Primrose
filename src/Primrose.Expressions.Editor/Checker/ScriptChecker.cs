using Primrose.Primitives.ValueTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Primrose.Expressions.Editor.Checker
{
  public class ScriptChecker
  {
    public string Content;
    public Action<string> Log;
    public string Error;
    public ScriptChecker(string content, Action<string> log)
    {
      Content = content;
      Log = log;
    }

    public bool Verify(ContextBase context)
    {
      try
      {
        ScriptFile f = new ScriptFile(context);
        if (Log != null)
          f.ScriptReadBegin += Log.Invoke;

        context.Reset();
        using (StreamReader sr = new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(Content))))
          f.ReadFromStream(sr);
        return true;
      }
      catch (Exception ex)
      {
        Error = ex.Message + Environment.NewLine + ex.StackTrace;
        return false;
      }
    }

    public SortedList<Pair<int, int>, LintType> Lint(ContextBase context)
    {
      ScriptFile f = new ScriptFile(context);
      try
      {
        if (Log != null)
          f.ScriptReadBegin += Log.Invoke;

        context.Reset();
        using (StreamReader sr = new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(Content))))
          f.ReadFromStream(sr);
      }
      catch {} // don't care

      SortedList<Pair<int, int>, LintType> ret = new SortedList<Pair<int, int>, LintType>(new PosComparer());
      foreach (LintElement step in f.Linter)
      {
        Pair<int, int> pos = new Pair<int, int>(step.Line, step.Position);
        if (!ret.ContainsKey(pos))
          ret.Add(pos, step.Lint);
      }
      return ret;
    }

    private class PosComparer : IComparer<Pair<int, int>>
    {
      public int Compare(Pair<int, int> x, Pair<int, int> y)
      {
        int cmp = Comparer<int>.Default.Compare(x.t, y.t);
        if (cmp == 0)
          cmp = Comparer<int>.Default.Compare(x.u, y.u);

        return cmp;
      }
    }
  }
}
