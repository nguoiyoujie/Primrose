using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.Parsers;
using System;
using System.Reflection;

namespace Primitives.FileFormat.INI
{
  /// <summary>Defines a list of keys from a section of an INI file</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class INIRegistryAttribute : Attribute
  {
    /// <summary>The section name of the INI file where the keys are based on</summary>
    public string Section;

    /// <summary>Defines whether the INI file must contain this section/key combination</summary>
    public bool Required;

    /// <summary>Defines a list of keys from a section of an INI file</summary>
    /// <param name="section">The section name from which the keys are retrieved</param>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INIRegistryAttribute(string section, bool required = false)
    {
      if (section == null)
        throw new ArgumentNullException("section");

      Section = section;
      Required = required;
    }

    internal object Read(Type t, INIFile f, string sectionOverride)
    {
      if (t.GetGenericTypeDefinition() != typeof(Registry<,>))
        throw new InvalidOperationException("INIRegistry attribute can only be used with Registry<K,T> data types! ({0})".F(t.Name));

      MethodInfo mRead = GetType().GetMethod("InnerRead", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t.GetGenericArguments());
      return gmRead.Invoke(this, new object[] { f, sectionOverride });
    }

    private Registry<T, U> InnerRead<T, U>(INIFile f, string sectionOverride)
    {
      sectionOverride = sectionOverride ?? Section;
      Registry<T, U> ret = new Registry<T, U>();
      if (f.HasSection(sectionOverride))
      {
        foreach (INIFile.INISection.INILine ln in f.GetSection(sectionOverride).Lines)
          if (ln.HasKey)
            ret.Add(Parser.Parse(ln.Key, default(T)), Parser.Parse(ln.Value, default(U)));
      }
      else if (Required)
      {
        throw new InvalidOperationException("Required section is not defined!".F(sectionOverride));
      }

      return ret;
    }

    internal void Write(Type t, INIFile f, object value, string sectionOverride)
    {
      if (t.GetGenericTypeDefinition() != typeof(Registry<,>))
        throw new InvalidOperationException("INIRegistry attribute can only be used with Registry<K,T> data types! ({0})".F(t.Name));

      MethodInfo mRead = GetType().GetMethod("InnerWrite", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t.GetElementType());
      gmRead.Invoke(this, new object[] { f, value, sectionOverride });
    }

    private void InnerWrite<T, U>(INIFile f, Registry<T, U> reg, string sectionOverride)
    {
      sectionOverride = sectionOverride ?? Section;
      foreach (T t in reg.GetKeys())
      {
        string key = Parser.Write(t);
        string value = Parser.Write(reg.Get(t));
        f.SetString(sectionOverride, key, value);
      }
    }
  }
}
