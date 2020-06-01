using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.Parsers;
using System;
using System.Reflection;

namespace Primitives.FileFormat.INI
{
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
        throw new ArgumentNullException("section");

      if (subsectionPrefix == null)
        throw new ArgumentNullException("subsectionPrefix");

      if (key == null)
        throw new ArgumentNullException("key");

      Section = section;
      SubsectionPrefix = subsectionPrefix;
      Key = key;
      Required = required;
    }

    /// <summary>Defines a value that redirects to other sections of an INI file</summary>
    /// <param name="subsectionPrefix">The subsection prefix to be used when writing members</param>
    /// <param name="key">The key name from which the value is retrieved</param>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INISubSectionListAttribute(string subsectionPrefix, string key, bool required = false)
    {
      if (subsectionPrefix == null)
        throw new ArgumentNullException("subsectionPrefix");

      if (key == null)
        throw new ArgumentNullException("key");

      Section = null;
      SubsectionPrefix = subsectionPrefix;
      Key = key;
      Required = required;
    }

    internal object Read(Type t, INIFile f, string defaultSection)
    {
      if (!t.IsArray || t.GetElementType().IsArray)
        throw new InvalidOperationException("INIRegistry attribute can only be used with a single-level array (T[]) data type! ({0})".F(t.Name));

      MethodInfo mRead = GetType().GetMethod("InnerRead", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t.GetElementType());
      return gmRead.Invoke(this, new object[] { f, defaultSection });
    }

    private T[] InnerRead<T>(INIFile f, string defaultSection)
    {
      string s = INIAttributeExt.GetSection(Section, defaultSection);
      string[] list =  f.GetStringArray(s, Key, new string[0]);
      T[] ret = new T[list.Length];
      for (int i = 0; i < list.Length; i++)
      {
        T instance = Activator.CreateInstance<T>();
        f.LoadByAttribute(ref instance, list[i]);
        ret[i] = instance;
      }

      return ret;
    }

    internal void Write(Type t, INIFile f, object value, string defaultSection)
    {
      if (!t.IsArray || t.GetElementType().IsArray)
        throw new InvalidOperationException("INIRegistry attribute can only be used with a single-level array (T[]) data type! ({0})".F(t.Name));

      MethodInfo mRead = GetType().GetMethod("InnerWrite", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t.GetGenericArguments());
      object val = gmRead.Invoke(this, new object[] { f, value, defaultSection });
    }

    private void InnerWrite<T>(INIFile f, T[] value, string defaultSection)
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
