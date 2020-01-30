using Primrose.Primitives.Extensions;
using System;
using System.IO;
using System.Text;

namespace Primrose.Expressions
{
  public delegate void ScriptReadDelegate(string name);

  public class ScriptFile
  {
    private static char[] seperator = new char[] { ':' };

    public ScriptFile(string filepath)
    {
      if (!File.Exists(filepath))
        throw new FileNotFoundException("Script file '{0}' is not found!".F(Path.GetFullPath(filepath)));

      FilePath = filepath;
    }

    public readonly string FilePath;
    public Action<string> NewScriptEvent;

    public void ReadFile()
    {
      Script script = Script.Registry.Global;
      StringBuilder sb = new StringBuilder();
      int linenumber = 0;
      using (StreamReader sr = new StreamReader(FilePath))
      {
        while (!sr.EndOfStream)
        {
          string line = sr.ReadLine().Trim();

          if (line.EndsWith(":"))
          {
            if (script != null)
            {
              script.AddStatements(sb.ToString(), ref linenumber);
            }

            line = line.TrimEnd(seperator).Trim();

            NewScriptEvent?.Invoke(line);
            script = new Script(line);
            sb.Clear();
          }
          else
          {
            if (script != null)
            {
              sb.Append(line);
              sb.Append(Environment.NewLine);
            }
            else // Globals
            {
              
            }
          }
        }
        if (script != null) // last script
        {
          script.AddStatements(sb.ToString(), ref linenumber);
        }
      }
    }
  }
}
