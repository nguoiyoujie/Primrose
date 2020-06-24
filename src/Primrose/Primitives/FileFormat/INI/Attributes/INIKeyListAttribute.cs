using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;

namespace Primitives.FileFormat.INI
{
  //  Visual:
  //    [Section]
  //      value1
  //      value2
  //      value3
  //
  //  (ReadValue set to true)
  //    [Section]
  //      0=value1
  //      1=value2
  //      2=value3 ; key can be anything
  //
  /// <summary>Defines a list of keys from a section of an INI file</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class INIKeyListAttribute : Attribute
  {
    /// <summary>The section name of the INI file where the keys are based on</summary>
    public string Section;

    /// <summary>Defines whether the values are read instead of keys</summary>
    public bool ReadValue;

    /// <summary>Defines whether the INI file must contain this section/key combination</summary>
    public bool Required;

    /// <summary>Defines a list of keys from a section of an INI file</summary>
    /// <param name="section">The section name from which the keys are retrieved</param>
    /// <param name="readValue">Defines whether the values are read instead of keys</param>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INIKeyListAttribute(string section, bool readValue = false, bool required = false)
    {
      Section = section ?? throw new ArgumentNullException(nameof(section));
      ReadValue = readValue;
      Required = required;
    }

    /// <summary>Defines a list of keys from a section of an INI file</summary>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INIKeyListAttribute(bool required = false)
    {
      Section = null;
      Required = required;
    }

    internal string[] Read(Type t, INIFile f, string defaultSection)
    {
      if (t != typeof(string[]))
        throw new InvalidOperationException(Primrose.Properties.Resources.Error_INIKeyListInvalidType.F(t.Name));

      string s = INIAttributeExt.GetSection(Section, defaultSection);
      List<string> vals = new List<string>();
      if (f.HasSection(s))
      {
        foreach (INIFile.INISection.INILine ln in f.GetSection(s).Lines)
          if (ln.HasKey)
            vals.Add(ReadValue ? ln.Value : ln.Key);
      }
      else if (Required)
      {
        throw new INISectionNotFoundException(s);
      }

      return vals.ToArray();
    }

    internal void Write(Type t, INIFile f, string[] value, string defaultSection)
    {
      if (value == null)
        return;

      if (t != typeof(string[]))
        throw new InvalidOperationException(Primrose.Properties.Resources.Error_INIKeyListInvalidType.F(t.Name));

      string s = INIAttributeExt.GetSection(Section, defaultSection);
      foreach (string v in value)
        f.SetEmptyKey(s, v);
    }
  }
}
