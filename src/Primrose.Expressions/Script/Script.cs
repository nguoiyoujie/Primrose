﻿using System.Collections.Generic;
using Primrose.Expressions.Tree.Statements;
using System.Text;
using Primrose.Expressions.Tree.Header;

namespace Primrose.Expressions
{
  /// <summary>
  /// Represents a set of parsable statements
  /// </summary>
  public partial class Script
  {
    private readonly List<RootStatement> m_statements = new List<RootStatement>();

    /// <summary>The base context scope of the script</summary>
    public ContextScope Scope;

    /// <summary>The name of the script</summary>
    public readonly string Name;

    internal Script() { Scope = new ContextScope(); }

    /// <summary>Creates a script</summary>
    /// <param name="scriptname">The name of the script</param>
    /// <param name="registry">The script registry to reference for global scope</param>
    public Script(string scriptname, Registry registry)
    {
      Name = scriptname;
      registry.Add(scriptname, this);
      Scope = registry.Global.Scope.Next;
    }

    /// <summary>Creates a script</summary>
    /// <param name="scriptname">The name of the script</param>
    /// <param name="registry">The script registry to reference for global scope</param>
    /// <param name="scope">The scope to be used</param>
    public Script(string scriptname, Registry registry, ContextScope scope)
    {
      Name = scriptname;
      registry.Add(scriptname, this);
      Scope = scope;
    }

    /// <summary>Lists the statements contained in the script</summary>
    internal List<RootStatement> Statements
    {
      get { return m_statements; }
    }

    /// <summary>Adds one or more statements to the script</summary>
    /// <param name="text">The string text to be parsed</param>
    /// <param name="linenumber">The current line number</param>
    public void AddStatements(string text, ref int linenumber)
    {
      Parser.Parse(Scope, text, out RootStatement statement, Name, ref linenumber, out _);
      m_statements.Add(statement);
    }

    /// <summary>Adds one or more statements to the script, and produces a set of linting hints for a linter</summary>
    /// <param name="text">The string text to be parsed</param>
    /// <param name="linenumber">The current line number</param>
    /// <param name="linter">The set of linting hints describing the statements added</param>
    public void AddStatements(string text, ref int linenumber, out List<LintElement> linter)
    {
      Parser.Parse(Scope, text, out RootStatement statement, Name, ref linenumber, out linter);
      m_statements.Add(statement);
    }

    /// <summary>Adds one or more statements to the script, and produces a set of linting hints for a linter</summary>
    /// <param name="text">The string text to be parsed</param>
    /// <param name="linenumber">The current line number</param>
    /// <param name="linter">The set of linting hints describing the statements added</param>
    internal HeaderExpression ReadHeader(string text, ref int linenumber, out List<LintElement> linter)
    {
      Parser.Parse(Scope, text, out HeaderExpression statement, Name, ref linenumber, out linter);
      return statement;
    }

    /// <summary>Clears the script of information</summary>
    public void Clear()
    {
      m_statements.Clear();
      Scope.Clear();
    }

    /// <summary>Evaluates the script</summary>
    /// <param name="context">The script context</param>
    public Val Run(IContext context)
    {
      Val retval = Val.NULL;
      foreach (RootStatement statement in m_statements)
        if (statement.Evaluate(context, ref retval))
          break;
      return retval;
    }

    /// <summary>Prints the script as a continuous string</summary>
    public string Write()
    {
      StringBuilder sb = new StringBuilder();
      foreach (RootStatement statement in m_statements)
      {
        statement.Write(sb);
      }
      return sb.ToString();
    }
  }
}
