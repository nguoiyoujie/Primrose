using Primrose.Primitives.Parsers;
using System;
using System.Reflection;

namespace Primitives.FileFormat.INI
{
  //  Visual:
  //    [Section]
  //      Key = value
  //
  /// <summary>Defines a value from a section and key of an INI file</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class INIValueAttribute : Attribute
  {
    /// <summary>The section name of the INI file where the key is based on</summary>
    public string Section;

    /// <summary>The key name of the INI file where the value is read</summary>
    public string Key;

    /// <summary>Defines whether the INI file must contain this section/key combination</summary>
    public bool Required;

    /// <summary>
    /// <summary>Defines a value from a section and key of an INI file</summary>
    /// </summary>
    /// <param name="section">The section name from which the key-value is retrieved</param>
    /// <param name="key">The key name from which the value is retrieved</param>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INIValueAttribute(string section, string key, bool required = false)
    {
      Section = section ?? throw new ArgumentNullException(nameof(section));
      Key = key ?? throw new ArgumentNullException(nameof(key));
      Required = required;
    }

    /// <summary>
    /// <summary>Defines a value from a section and key of an INI file</summary>
    /// </summary>
    /// <param name="key">The key name from which the value is retrieved</param>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INIValueAttribute(string key = null, bool required = false)
    {
      Section = null;
      Key = key;
      Required = required;
    }

    internal object Read(Type t, INIFile f, object defaultValue, string fieldName, string defaultSection, IResolver resolver)
    {
      MethodInfo mRead = GetType().GetMethod(nameof(InnerRead), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t);
      return gmRead.Invoke(this, new object[] { f, defaultValue, fieldName, defaultSection, resolver});
    }

    private T InnerRead<T>(INIFile f, T defaultValue, string fieldName, string defaultSection, IResolver resolver)
    {
      string s = INIAttributeExt.GetSection(Section, defaultSection);
      string k = INIAttributeExt.GetKey(Key, fieldName);
      if (Required && !f.HasKey(s, k))
        throw new INIKeyNotFoundException(s, k);

      return Parser.Parse(f.GetString(s, k, null), resolver, defaultValue);
    }

    internal void Write(Type t, INIFile f, object value, string fieldName, string defaultSection)
    {
      MethodInfo mRead = GetType().GetMethod(nameof(InnerWrite), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t);
      gmRead.Invoke(this, new object[] { f, value, fieldName, defaultSection });
    }

    private void InnerWrite<T>(INIFile f, T value, string fieldName, string defaultSection)
    {
      if (value == null || value.Equals(default(T)))
        return;

      string s = INIAttributeExt.GetSection(Section, defaultSection);
      string k = INIAttributeExt.GetKey(Key, fieldName);
      f.SetString(s, k, Parser.Write(value));
    }
  }
}
