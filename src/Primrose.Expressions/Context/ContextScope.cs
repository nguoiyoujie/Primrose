using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;
using System;
using System.Collections.Generic;

namespace Primrose.Expressions
{
  /// <summary>
  /// Provides a context scope for the script
  /// </summary>
  public class ContextScope
  {
    private readonly Dictionary<string, Val> m_variables = new Dictionary<string, Val>();

    /// <summary>Returns the number of sublevels this scope has derived from the global scope. Evaluates to 0 if this is the global scope</summary>
    public int Level { get; private set; }

    /// <summary>The parent of this scope. Evaluates to null if this is the global scope</summary>
    public ContextScope Parent { get; private set; }

    /// <summary>Returns a child of this scope.</summary>
    public ContextScope Next { get { return new ContextScope() { Parent = this, Level = this.Level + 1}; } }

    /// <summary>Clears the context of information</summary>
    public void Clear()
    {
      m_variables.Clear();
    }

    /// <summary>Declares a variable</summary>
    /// <param name="name">The variable name</param>
    /// <param name="type">The variable type</param>
    /// <param name="lexer">The lexer</param>
    /// <exception cref="InvalidOperationException">Duplicate declaration of a variable in the same scope</exception>
    internal void DeclVar(string name, ValType type, Lexer lexer)
    {
      if (m_variables.ContainsKey(name))
        throw new ParseException(lexer, Resource.Strings.Error_ParseException_DuplicateVariable.F(name));

      m_variables.Add(name, new Val(type));
    }

    /// <summary>Retrieves the value of a variable</summary>
    /// <param name="eval">The expression object being evaluated</param>
    /// <param name="name">The variable name</param>
    /// <returns>The Val object containing the value of the variable</returns>
    /// <exception cref="InvalidOperationException">Attempted to get the value from an undeclared variable</exception>
    public Val GetVar(ITracker eval, string name)
    {
      if (!m_variables.TryGetValue(name, out Val ret))
        if (Parent != null)
          return Parent.GetVar(eval, name);
        else
          throw new EvalException(eval, Resource.Strings.Error_EvalException_Get_VariableNotFound.F(name));
      return ret;
    }

    /// <summary>Set the value of a variable</summary>
    /// <param name="eval">The expression object being evaluated</param>
    /// <param name="name">The variable name</param>
    /// <param name="val">The Val object containing the value of the variable</param>
    /// <exception cref="InvalidOperationException">Attempted to set the value of an undeclared variable</exception>
    public void SetVar(ITracker eval, string name, Val val)
    {
      if (m_variables.ContainsKey(name))
      {
        WriteVar(name, val);
      }
      else
      {
        if (Parent != null)
          Parent.SetVar(eval, name, val);
        else
          throw new EvalException(eval, Resource.Strings.Error_EvalException_Set_VariableNotFound.F(name));
      }
    }

    private void WriteVar(string name, Val val)
    {
      ValType t = m_variables[name].Type;
      try
      {
        m_variables[name] = Ops.Coerce(t, val);
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException(Resource.Strings.Error_EvalException_InvalidVariableAssignment.F(t, name, ex.Message));
      }
    }
  }
}
