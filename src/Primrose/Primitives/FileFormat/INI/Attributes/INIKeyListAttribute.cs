using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;

namespace Primitives.FileFormat.INI
{
  /// <summary>Defines a list of keys from a section of an INI file</summary>
  [AttributeUsage(AttributeTargets.Field)]
  public class INIKeyListAttribute : Attribute
  {
    /// <summary>The section name of the INI file where the keys are based on</summary>
    public string Section;

    /// <summary>Defines whether the INI file must contain this section/key combination</summary>
    public bool Required;

    /// <summary>Defines a list of keys from a section of an INI file</summary>
    /// <param name="section">The section name from which the keys are retrieved</param>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INIKeyListAttribute(string section, bool required = false)
    {
      Section = section;
      Required = required;
    }

    internal string[] Read(INIFile f)
    {
      List<string> vals = new List<string>();
      if (f.HasSection(Section))
      {
        foreach (INIFile.INISection.INILine ln in f.GetSection(Section).Lines)
          if (ln.HasKey)
            vals.Add(ln.Key);
      }
      else if (Required)
      {
        throw new InvalidOperationException("Required section is not defined!".F(Section));
      }

      return vals.ToArray();
    }

    internal void Write(INIFile f, string[] value)
    {
      foreach (string v in value)
        f.SetEmptyKey(Section, v);
    }
  }
}
