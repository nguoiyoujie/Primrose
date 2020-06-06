﻿using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;
using System.Reflection;

namespace Primitives.FileFormat.INI
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
    /// <param name="subsectionPrefix">The subsection prefix to be used when writing members</param>
    /// <param name="key">The key name from which the value is retrieved</param>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INISubSectionListAttribute(string section, string subsectionPrefix, string key, bool required = false)
    {
      if (section == null)
        throw new ArgumentNullException(nameof(section));

      if (subsectionPrefix == null)
        throw new ArgumentNullException(nameof(subsectionPrefix));

      if (key == null)
        throw new ArgumentNullException(nameof(key));

      Section = section;
      SubsectionPrefix = subsectionPrefix;
      Key = key;
      Required = required;
    }

    /// <summary>Defines a value that redirects to other sections of an INI file</summary>
    /// <param name="subsectionPrefix">The subsection prefix to be used when writing members</param>
    /// <param name="key">The key name from which the value is retrieved</param>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INISubSectionListAttribute(string subsectionPrefix, string key = null, bool required = false)
    {
      if (subsectionPrefix == null)
        throw new ArgumentNullException(nameof(subsectionPrefix));

      Section = null;
      SubsectionPrefix = subsectionPrefix;
      Key = key;
      Required = required;
    }

    internal object Read(Type t, INIFile f, string fieldName, string defaultSection, IResolver resolver)
    {
      if (!t.IsArray || t.GetElementType().IsArray)
        throw new InvalidOperationException("INISubSectionListAttribute attribute can only be used with a single-level array (T[]) data type! ({0})".F(t.Name));

      MethodInfo mRead = GetType().GetMethod("InnerRead", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t.GetElementType());
      return gmRead.Invoke(this, new object[] { f, fieldName, defaultSection, resolver });
    }

    private T[] InnerRead<T>(INIFile f, string fieldName, string defaultSection, IResolver resolver)
    {
      string s = INIAttributeExt.GetSection(Section, defaultSection);
      string k = INIAttributeExt.GetKey(Key, fieldName);
      string[] list =  f.GetValue(s, k, new string[0]);
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
        throw new InvalidOperationException("INISubSectionListAttribute attribute can only be used with a single-level array (T[]) data type! ({0})".F(t.Name));

      MethodInfo mRead = GetType().GetMethod("InnerWrite", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t.GetGenericArguments());
      object val = gmRead.Invoke(this, new object[] { f, value, fieldName, defaultSection });
    }

    private void InnerWrite<T>(INIFile f, T[] value, string fieldName, string defaultSection)
    {
      string s = INIAttributeExt.GetSection(Section, defaultSection);
      int i = 1;
      foreach (T t in value)
      {
        T o = t;
        f.UpdateByAttribute(ref o, s + i.ToString());
        i++;
      }
    }
  }
}
