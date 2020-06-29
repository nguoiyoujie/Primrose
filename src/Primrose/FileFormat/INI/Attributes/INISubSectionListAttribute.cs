using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;
using System.Reflection;

namespace Primrose.FileFormat.INI
{
  //  Visual:
  //    [Section]
  //      Key = subsection1,subsection2,subsection3
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
  /// <summary>Defines a value that redirects to other sections of an INI file</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class INISubSectionListAttribute : Attribute
  {
    /// <summary>The section name of the INI file where the keys are based on</summary>
    public string Section;

    /// <summary>The subsection prefix to be used when writing members</summary>
    public string SubsectionPrefix;

    /// <summary>The key name of the INI file where the value is read</summary>
    public string Key;

    /// <summary>Defines whether the INI file must contain this section/key combination</summary>
    public bool Required;

    /// <summary>Defines a value that redirects to other sections of an INI file</summary>
    /// <param name="section">The section name from which the keys are retrieved</param>
    /// <param name="key">The key name from which the value is retrieved</param>
    /// <param name="subsectionPrefix">The subsection prefix to be used when writing members. If null, the key name is used</param>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INISubSectionListAttribute(string section = null, string key = null, string subsectionPrefix = null, bool required = false)
    {
      Section = section;
      Key = key;
      SubsectionPrefix = subsectionPrefix;
      Required = required;
    }

    internal object Read(Type t, INIFile f, string fieldName, string defaultSection, IResolver resolver)
    {
      if (!t.IsArray || t.GetElementType().IsArray)
        throw new InvalidOperationException(Primrose.Properties.Resources.Error_INISubSectionListInvalidType.F(t.Name));

      MethodInfo mRead = GetType().GetMethod(nameof(InnerRead), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t.GetElementType());
      return gmRead.Invoke(this, new object[] { f, fieldName, defaultSection, resolver });
    }

    private T[] InnerRead<T>(INIFile f, string fieldName, string defaultSection, IResolver resolver)
    {
      string s = INIAttributeExt.GetSection(Section, defaultSection);
      string k = INIAttributeExt.GetKey(Key, fieldName);
      string[] list =  f.GetValue(s, k, resolver, new string[0]);
      T[] ret = new T[list.Length];
      for (int i = 0; i < list.Length; i++)
      {
        T instance = Activator.CreateInstance<T>();
        f.LoadByAttribute(ref instance, list[i], resolver);
        ret[i] = instance;
      }

      return ret;
    }

    internal void Write(Type t, INIFile f, object value, string fieldName, string defaultSection)
    {
      if (!t.IsArray || t.GetElementType().IsArray)
        throw new InvalidOperationException(Primrose.Properties.Resources.Error_INISubSectionListInvalidType.F(t.Name));

      MethodInfo mRead = GetType().GetMethod(nameof(InnerWrite), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t.GetGenericArguments());
      object val = gmRead.Invoke(this, new object[] { f, value, fieldName, defaultSection });
    }

    private void InnerWrite<T>(INIFile f, T[] value, string fieldName, string defaultSection)
    {
      if (value == default)
        return;

      string s = INIAttributeExt.GetSection(Section, defaultSection);
      string k = SubsectionPrefix ?? INIAttributeExt.GetKey(Key, fieldName);
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
