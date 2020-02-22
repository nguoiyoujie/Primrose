using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;

namespace Primitives.FileFormat.INI
{
  /// <summary>Defines a list of keys from a section of an INI file</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
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
      if (section == null)
        throw new ArgumentNullException("section");

      Section = section;
      Required = required;
    }

    internal string[] Read(Type t, INIFile f, string sectionOverride)
    {
      if (t != typeof(string[]))
        throw new InvalidOperationException("INIKeyList attribute can only be used with string[] data types! ({0})".F(t.Name));

      sectionOverride = sectionOverride ?? Section;
      List<string> vals = new List<string>();
      if (f.HasSection(sectionOverride))
      {
        foreach (INIFile.INISection.INILine ln in f.GetSection(sectionOverride).Lines)
          if (ln.HasKey)
            vals.Add(ln.Key);
      }
      else if (Required)
      {
        throw new InvalidOperationException("Required section is not defined!".F(sectionOverride));
      }

      return vals.ToArray();
    }

    internal void Write(Type t, INIFile f, string[] value, string sectionOverride)
    {
      if (t != typeof(string[]))
        throw new InvalidOperationException("INIKeyList attribute can only be used with string[] data types! ({0})".F(t.Name));

      sectionOverride = sectionOverride ?? Section;
      foreach (string v in value)
        f.SetEmptyKey(sectionOverride, v);
    }
  }
}
