using Primrose.Primitives.Extensions;
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
    private readonly List<string> m_parameters = new List<string>();

    /// <summary>Returns the number of sublevels this scope has derived from the global scope. Evaluates to 0 if this is the global scope</summary>
    public int Level { get; private set; }

    /// <summary>The parent of this scope. Evaluates to null if this is the global scope</summary>
    public ContextScope Parent { get; private set; }

    /// <summary>Returns a child of this scope.</summary>
    public ContextScope Next { get { return new ContextScope() { Parent = this, Level = this.Level + 1}; } }

    /// <summary>The number of parameterized variables at this scope</summary>
    public int ParameterCount { get { return m_parameters.Count; } }


    /// <summary>Clears the context of information</summary>
    public void Clear()
    {
      m_variables.Clear();
      m_parameters.Clear();
    }

    /// <summary>Declares a variable</summary>
    /// <param name="name">The variable name</param>
    /// <param name="type">The variable type</param>
    /// <param name="lexer">The lexer</param>
    /// <param name="isParameter">Denotes if the variable is a function parameter</param>
    /// <exception cref="InvalidOperationException">Duplicate declaration of a variable in the same scope</exception>
    internal void DeclVar(string name, Type type, Lexer lexer, bool isParameter = false)
    {
      if (m_variables.ContainsKey(name))
        throw new ParseException(lexer, Resource.Strings.Error_ParseException_DuplicateVariable.F(name));

      m_variables.Add(name, new Val(type));
      if (isParameter)
        m_parameters.Add(name);
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

    /// <summary>Sets the value of a variable</summary>
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

    /// <summary>Sets the values of a function parameters</summary>
    /// <param name="ps">The list of parameter values</param>
    public void SetParameters(IList<Val> ps)
    {
      for (int i = 0; i < ps.Count; i++)
      {
        SetParameter(i, ps[i]);
      }
    }

    /// <summary>Sets the value of a function parameter</summary>
    /// <param name="index">The parameter index</param>
    /// <param name="val">The Val object containing the value of the variable</param>
    public void SetParameter(int index, Val val)
    {
      if (index >= 0 && index < m_parameters.Count)
      {
        WriteVar(m_parameters[index], val);
      }
    }

    /// <summary>Sets the value of an indexed variable</summary>
    /// <param name="eval">The expression object being evaluated</param>
    /// <param name="name">The variable name</param>
    /// <param name="val">The Val object containing the value of the variable</param>
    /// <param name="indices">The indices to set</param>
    /// <exception cref="InvalidOperationException">Attempted to set the value of an undeclared variable</exception>
    public void SetVar(ITracker eval, string name, Val val, int[][] indices)
    {
      if (m_variables.ContainsKey(name))
      {
        WriteVar(name, val, indices);
      }
      else
      {
        if (Parent != null)
          Parent.SetVar(eval, name, val, indices);
        else
          throw new EvalException(eval, Resource.Strings.Error_EvalException_Set_VariableNotFound.F(name));
      }
    }

    private void WriteVar(string name, Val val)
    {
      Type t = m_variables[name].Type;
      try
      {
        m_variables[name] = Ops.ImplicitCast(t, val);
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException(Resource.Strings.Error_EvalException_InvalidVariableAssignment.F(t, name, ex.Message));
      }
    }

    private void WriteVar(string name, Val val, int[][] indices)
    {
      Type t = m_variables[name].Type;
      try
      {
        Array a = m_variables[name].Cast<Array>();
        object o = a;
        foreach (int[] i2 in indices)
        {
          a = (Array)o;
          o = a.GetValue(i2);
          t = t.GetElementType();
        }

        a.SetValue(Ops.ImplicitCast(t, val).Value, indices[indices.Length - 1]);
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException(Resource.Strings.Error_EvalException_InvalidVariableAssignment.F(t, name, ex.Message));
      }
    }
  }
}
