using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.Parsers;
using System;
using System.Reflection;

namespace Primrose.FileFormat.INI
{
  //  Visual: 
  //    [Section]
  //      key1=subsection1
  //      key2=subsection2
  //      key3=subsection3
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
  public class INISubSectionRegistryAttribute : Attribute
  {
    /// <summary>The section name of the INI file where the keys are based on</summary>
    public string Section;

    /// <summary>The subsection prefix to be used when writing members</summary>
    public string SubsectionPrefix;

    /// <summary>Defines whether the INI file must contain this section/key combination</summary>
    public bool Required;

    /// <summary>Defines a value that redirects to other sections of an INI file</summary>
    /// <param name="section">The section name from which the keys are retrieved. If null, the field name is used</param>
    /// <param name="subsectionPrefix">The subsection prefix to be used when writing members. If null, the section name is used</param>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INISubSectionRegistryAttribute(string section = null, string subsectionPrefix = null, bool required = false)
    {
      Section = section;
      SubsectionPrefix = subsectionPrefix;
      Required = required;
    }

    internal object Read(Type t, INIFile f, string fieldName, string defaultSection, IResolver resolver)
    {
      if (t.GetGenericTypeDefinition() != typeof(Registry<,>))
        throw new InvalidOperationException(Resource.Strings.Error_INIRegistryListInvalidType.F(t.Name));

      MethodInfo mRead = GetType().GetMethod(nameof(InnerRead), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t.GetGenericArguments());
      return gmRead.Invoke(this, new object[] { f, defaultSection, resolver });
    }

    private Registry<T, U> InnerRead<T, U>(INIFile f, string defaultSection, IResolver resolver)
    {
      string s = INIAttributeExt.GetSection(Section, defaultSection);
      Registry<T, U> ret = new Registry<T, U>();
      if (f.HasSection(s))
      {
        foreach (INIFile.INISection.INILine ln in f.GetSection(s).Lines)
          if (ln.HasKey)
            ret.Add(Parser.Parse(ln.Key, default(T)), InnerReadItem<U>(f, ln.Value, resolver));
      }
      else if (Required)
      {
        throw new INISectionNotFoundException(s);
      }

      return ret;
    }

    private U InnerReadItem<U>(INIFile f, string subsection, IResolver resolver)
    {
      U instance = Activator.CreateInstance<U>();
      if (subsection != null) { f.LoadByAttribute(ref instance, subsection, resolver); }
      return instance;
    }

    internal void Write(Type t, INIFile f, object value, string fieldName, string defaultSection)
    {
      if (t.GetGenericTypeDefinition() != typeof(Registry<,>))
        throw new InvalidOperationException(Resource.Strings.Error_INIRegistryListInvalidType.F(t.Name));

      MethodInfo mRead = GetType().GetMethod(nameof(InnerWrite), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t.GetGenericArguments());
      object val = gmRead.Invoke(this, new object[] { f, value, fieldName, defaultSection });
    }

    private void InnerWrite<T, U>(INIFile f, Registry<T, U> registry, string fieldName, string defaultSection)
    {
      if (registry == default)
        return;

      string s = INIAttributeExt.GetSection(Section, defaultSection ?? fieldName);
      //string k = SubsectionPrefix ?? fieldName;
      //Registry<string, string> keylinks = new Registry<string, string>();
      foreach(T key in registry.GetKeys())
      {
        string skey = Parser.Write(key);
        U o = registry[key];
        string vkey = "{0}{1}".F(fieldName, skey);
        f.UpdateByAttribute(ref o, vkey);

        f.SetValue(s, skey, vkey);
      }
    }
  }
}
