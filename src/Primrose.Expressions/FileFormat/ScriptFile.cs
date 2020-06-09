using Primrose.Primitives.Extensions;
using System;
using System.IO;
using System.Text;

namespace Primrose.Expressions
{
  /// <summary>Represents a delegate for script reading events</summary>
  /// <param name="name">The name of the script being read</param>
  public delegate void ScriptReadDelegate(string name);

  /// <summary>
  /// Performs reading and creation of scripts into a context from a source file
  /// </summary>
  public class ScriptFile
  {
    /// <summary>Creates a new script file from a source file</summary>
    /// <param name="filepath">The path of the source file</param>
    /// <param name="context">The context where the scripts are loaded into</param>
    public ScriptFile(string filepath, IContext context)
    {
      if (!File.Exists(filepath))
        throw new FileNotFoundException("Script file '{0}' is not found!".F(Path.GetFullPath(filepath)));

      FilePath = filepath;
      Registry = context.Scripts;
    }

    /// <summary>The path of the source file</summary>
    public readonly string FilePath;

    /// <summary>Represents the scripts contained in this file</summary>
    public readonly Script.Registry Registry;

    /// <summary>Invokes when a new script is being loaded</summary>
    public ScriptReadDelegate ScriptReadBegin;

    /// <summary>Invokes when a new script has finished loading</summary>
    public ScriptReadDelegate ScriptReadEnd;

    /// <summary>Reads and populates the script registry</summary>
    public void ReadFile()
    {
      Script script = Registry.Global;
      StringBuilder sb = new StringBuilder();
      int linenumber = 0;
      char[] headerSeperator = new char[] { ':' }; // cache
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
              ScriptReadEnd?.Invoke(script.Name ?? nameof(Registry.Global));
            }

            string header = line.TrimEnd(headerSeperator).Trim();

            ScriptReadBegin?.Invoke(header);
            script = new Script(line, Registry);
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
          ScriptReadEnd?.Invoke(script.Name);
        }
      }
    }
  }
}
