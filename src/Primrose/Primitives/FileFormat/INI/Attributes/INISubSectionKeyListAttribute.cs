using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;
using System.Reflection;

namespace Primitives.FileFormat.INI
{
  //  Visual:
  //    [Section]
  //      subsection1
  //      subsection2
  //      subsection3
  //  
  //    [subsection1]
  //      ...
  //  
  //    [subsection2]
  //      ...
  //  
  //    [subsection3]
  //      ...
  //
  //  (ReadValue set to true)
  //    [Section]
  //      0=subsection1
  //      1=subsection2
  //      2=subsection3 ; key can be anything
  //  
  //    [subsection1]
  //      ...
  //  
  //    [subsection2]
  //      ...
  //  
  //    [subsection3]
  //      ...
  //
  /// <summary>Reads a section whose keys redirect to other sections of an INI file</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class INISubSectionKeyListAttribute : Attribute
  {
    /// <summary>The section name of the INI file where the keys are based on</summary>
    public string Section;

    /// <summary>The subsection prefix to be used when writing members</summary>
    public string SubsectionPrefix;

    /// <summary>Defines whether the values are read instead of keys</summary>
    public bool ReadValue;

    /// <summary>Defines whether the INI file must contain this section/key combination</summary>
    public bool Required;

    /// <summary>Defines a value that redirects to other sections of an INI file</summary>
    /// <param name="section">The section name from which the keys are retrieved. If null, the field name is used</param>
    /// <param name="subsectionPrefix">The subsection prefix to be used when writing members. If null, the section name is used</param>
    /// <param name="readValue">Defines whether the values are read instead of keys</param>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INISubSectionKeyListAttribute(string section = null, string subsectionPrefix = null, bool readValue = false, bool required = false)
    {
      Section = section;
      SubsectionPrefix = subsectionPrefix;
      ReadValue = readValue;
      Required = required;
    }

    internal object Read(Type t, INIFile f, string fieldName, string defaultSection, IResolver resolver)
    {
      if (!t.IsArray || t.GetElementType().IsArray)
        throw new InvalidOperationException(Primrose.Properties.Resources.Error_INISubSectionKeyListInvalidType.F(t.Name));

      string s = INIAttributeExt.GetSection(Section, defaultSection ?? fieldName);
      INIKeyListAttribute kattr = new INIKeyListAttribute(s, ReadValue, Required);
      string[] subsections = kattr.Read(typeof(string[]), f, s);

      MethodInfo mRead = GetType().GetMethod(nameof(InnerRead), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t.GetElementType());
      return gmRead.Invoke(this, new object[] { f, subsections, resolver });
    }

    private T[] InnerRead<T>(INIFile f, string[] subsections, IResolver resolver)
    {
      T[] ret = new T[subsections.Length];
      for (int i = 0; i < subsections.Length; i++)
      {
        T instance = Activator.CreateInstance<T>();
        f.LoadByAttribute(ref instance, subsections[i], resolver);
        ret[i] = instance;
      }

      return ret;
    }

    internal void Write(Type t, INIFile f, object value, string fieldName, string defaultSection)
    {
      if (!t.IsArray || t.GetElementType().IsArray)
        throw new InvalidOperationException(Primrose.Properties.Resources.Error_INISubSectionKeyListInvalidType.F(t.Name));

      MethodInfo mRead = GetType().GetMethod(nameof(InnerWrite), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t.GetGenericArguments());
      object val = gmRead.Invoke(this, new object[] { f, value, fieldName, defaultSection });
    }

    private void InnerWrite<T>(INIFile f, T[] value, string fieldName, string defaultSection)
    {
      if (value == default)
        return;

      string s = INIAttributeExt.GetSection(Section, defaultSection ?? fieldName);
      string k = SubsectionPrefix ?? fieldName;
      string[] keys = new string[value.Length];
      for (int i = 0; i < value.Length; i++)
      {
        T o = value[i];
        keys[i] = fieldName + i.ToString();
        f.UpdateByAttribute(ref o, keys[i]);
        i++;
      }
      f.SetValue(s, k, keys);
    }
  }
}
