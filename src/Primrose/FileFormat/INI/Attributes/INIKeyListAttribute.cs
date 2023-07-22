using Primrose.Primitives.Cache;
using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Primrose.FileFormat.INI
{
  //  Visual: ValueSource set to ValueSource.KEY_ONLY
  //    [Section]
  //      value1
  //      value2
  //      value3
  //
  //  Visual: ValueSource set to ValueSource.VALUE_ONLY
  //    [Section]
  //      0=value1
  //      1=value2
  //      random=value3 ; key can be anything
  //
  //  Visual: ValueSource set to ValueSource.VALUE_OR_KEY
  //    [Section]
  //      0=value1
  //      1=value2
  //      value3
  //
  /// <summary>Defines a list of keys from a section of an INI file</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class INIKeyListAttribute : Attribute
  {
    /// <summary>The section name of the INI file where the keys are based on</summary>
    public string Section;

    /// <summary>Defines whether the values are read instead of keys</summary>
    public ValueSource ValueSource;

    /// <summary>Defines whether the INI file must contain this section/key combination</summary>
    public bool Required;

    /// <summary>Defines a list of keys from a section of an INI file</summary>
    /// <param name="section">The section name from which the keys are retrieved</param>
    /// <param name="valueSource">Defines whether the values are read instead of keys</param>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INIKeyListAttribute(string section, ValueSource valueSource = ValueSource.KEY_ONLY, bool required = false)
    {
      Section = section ?? throw new ArgumentNullException(nameof(section));
      ValueSource = valueSource;
      Required = required;
    }

    /// <summary>Defines a list of keys from a section of an INI file</summary>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INIKeyListAttribute(bool required = false)
    {
      Section = null;
      Required = required;
    }

    internal IEnumerable<string> Read(Type t, INIFile f, string defaultSection)
    {
      if (t != typeof(string[]) && t != typeof(List<string>))
        throw new InvalidOperationException(Resource.Strings.Error_INIKeyListInvalidType.F(t.Name));

      string s = INIAttributeExt.GetSection(Section, defaultSection);
      List<string> vals = new List<string>();
      if (f.HasSection(s))
      {
        foreach (INIFile.INISection.INILine ln in f.GetSection(s).Lines)
          if (ln.HasKey)
            vals.Add(GetValue(ValueSource, ln.Key, ln.Value));
      }
      else if (Required)
      {
        throw new INISectionNotFoundException(s);
      }

      if (t == typeof(List<string>))
      {
        return vals.ToList();
      }
      else
      {
        return vals.ToArray();
      }
    }

    private string GetValue(ValueSource vSrc, string key, string value)
    {
      switch (vSrc)
      {
        default:
        case ValueSource.KEY_ONLY:
          return key;

        case ValueSource.VALUE_ONLY:
          return value;

        case ValueSource.VALUE_OR_KEY:
          return value ?? key;
      }
    }

    internal void Write(Type t, INIFile f, IEnumerable<string> value, string defaultSection)
    {
      if (value == null)
        return;

      if (t != typeof(string[]) && t != typeof(List<string>))
        throw new InvalidOperationException(Resource.Strings.Error_INIKeyListInvalidType.F(t.Name));

      string s = INIAttributeExt.GetSection(Section, defaultSection);
      if (f.HasSection(s))
      {
        f.GetSection(s).Clear();
      }
      int count = 0;
      foreach (string v in value)
        SetValue(ValueSource, f, count++, s, v);
    }

    private void SetValue(ValueSource vSrc, INIFile f, int count, string s, string v)
    {
      switch (vSrc)
      {
        default:
        case ValueSource.VALUE_OR_KEY:
        case ValueSource.KEY_ONLY:
          f.SetEmptyKey(s, v);
          break;

        case ValueSource.VALUE_ONLY:
          f.SetString(s, ToStringCache<int>.Get(count), v);
          break;
      }
    }
  }
}
