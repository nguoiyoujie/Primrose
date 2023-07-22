using Primrose.Expressions.Tree.Header;
using Primrose.Primitives.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Primrose.Expressions
{
  /// <summary>Represents a delegate for script reading events</summary>
  /// <param name="name">The name of the script being read</param>
  public delegate void ScriptReadDelegate(string name);

  /// <summary>Represents a delegate for script reading events</summary>
  /// <param name="script">The script being read</param>
  public delegate void ScriptReadDelegate2(Script script);

  /// <summary>
  /// Performs reading and creation of scripts into a context from a source file
  /// </summary>
  public class ScriptFile
  {
    /// <summary>Creates a new script file from a source file</summary>
    /// <param name="context">The context where the scripts are loaded into</param>
    public ScriptFile(ContextBase context)
    {
      Registry = context.Scripts;
      ScriptReadEnd2 = context.AddScriptFunc;
    }

    /// <summary>Represents the scripts contained in this file</summary>
    public readonly Script.Registry Registry;

    /// <summary>Represents the linter results based on the scripts in this file</summary>
    public List<LintElement> Linter { get; private set; } = new List<LintElement>(128);

    /// <summary>Invokes when a new script is being loaded</summary>
    public ScriptReadDelegate ScriptReadBegin;

    /// <summary>Invokes when a new script has finished loading</summary>
    public ScriptReadDelegate ScriptReadEnd;

    /// <summary>Invokes when a new script has finished loading</summary>
    public ScriptReadDelegate2 ScriptReadEnd2;

    /// <summary>Opens and reads information from a source file</summary>
    /// <param name="filePath">The file to read from</param>
    public void ReadFromFile(string filePath)
    {
      if (!File.Exists(filePath))
        throw new FileNotFoundException(Resource.Strings.Error_ScriptFileNotFound.F(Path.GetFullPath(filePath)));

      using (StreamReader sr = new StreamReader(filePath))
        ReadFromStream(sr);
    }


    /// <summary>Opens and reads information from a source stream</summary>
    public void ReadFromStream(StreamReader reader)
    {
      Script script = Registry.Global;
      StringBuilder sb = new StringBuilder();
      ScriptReadBegin?.Invoke(nameof(Registry.Global));

      int linenumber = 0;
      char[] headerSeperator = new char[] { ':' }; // cache
      Linter?.Clear();

      while (!reader.EndOfStream)
      {
        string line = reader.ReadLine();

        if (line.Trim().EndsWith(":")) // Warning this does not handle comments as the colon!
        {
          if (script != null)
          {
            script.AddStatements(sb.ToString(), ref linenumber, out List<LintElement> lint);
            Linter.AddRange(lint);
            ScriptReadEnd2.Invoke(script);
            ScriptReadEnd?.Invoke(script.Name ?? nameof(Registry.Global));
          }
          ContextScope scope = Registry.Global.Scope.Next;
          Parser.Parse(scope, line, out HeaderExpression header, nameof(Registry.Global), ref linenumber, out List<LintElement> headerlint);

          Linter.AddRange(headerlint);
          script = new Script(header.Name, Registry, scope);

          ScriptReadBegin?.Invoke(header.Name);
          sb.Clear();
        }
        else
        {
          if (script != null)
          {
            sb.AppendLine(line);
          }
          else // Globals
          {

          }
        }
      }
      if (script != null) // last script
      {
        script.AddStatements(sb.ToString(), ref linenumber, out List<LintElement> lint);
        Linter.AddRange(lint);
        ScriptReadEnd2.Invoke(script);
        ScriptReadEnd?.Invoke(script.Name);
      }
    }
  }
}
