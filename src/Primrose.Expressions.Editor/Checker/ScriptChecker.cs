using System;

namespace Primrose.Expressions.Editor.Checker
{
  public class ScriptChecker
  {
    public string Path;
    public Action<string> Log;
    public string Error;
    public ScriptChecker(string path, Action<string> log)
    {
      Path = path;
      Log = log;
    }

    public bool Verify()
    {
      try
      {
        Script.Registry.Clear();
        ScriptFile f = new ScriptFile(Path);
        if (Log != null)
          f.NewScriptEvent = (s) => { Log(s); };
        f.ReadFile();
        return true;
      }
      catch (Exception ex)
      {
        Error = ex.Message + Environment.NewLine + ex.StackTrace;
        return false;
      }
    }
  }
}
