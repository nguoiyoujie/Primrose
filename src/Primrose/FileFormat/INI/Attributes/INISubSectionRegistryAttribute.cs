using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.Parsers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;

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

      lock (_delInnerRead)
      {
        if (!_delInnerRead.ContainsKey(t))
        {
          MethodInfo mRead = GetType().GetMethod(nameof(InnerRead), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
          MethodInfo gmRead = mRead.MakeGenericMethod(t.GetGenericArguments());
#pragma warning disable HAA0101 // Array allocation for params parameter
          Type delegateType = Expression.GetFuncType(GetType(), typeof(INIFile), typeof(string), typeof(IResolver), typeof(object)); // allocates, but it is called once per type, so it's ok?
#pragma warning restore HAA0101 // Array allocation for params parameter
          _delInnerRead.Add(t, Delegate.CreateDelegate(delegateType, gmRead));
        }
        Func<INISubSectionRegistryAttribute, INIFile, string, IResolver, object> func = (Func<INISubSectionRegistryAttribute, INIFile, string, IResolver, object>)_delInnerRead[t];
        return func.Invoke(this, f, defaultSection, resolver);
      }
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

      lock (_delInnerWrite)
      {
        if (!_delInnerWrite.ContainsKey(t))
        {
          MethodInfo mRead = GetType().GetMethod(nameof(InnerWrite), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
          MethodInfo gmRead = mRead.MakeGenericMethod(t.GetGenericArguments());
#pragma warning disable HAA0101 // Array allocation for params parameter
          Type delegateType = Expression.GetActionType(GetType(), typeof(INIFile), typeof(object), typeof(string), typeof(string)); // allocates, but it is called once per type, so it's ok?
#pragma warning restore HAA0101 // Array allocation for params parameter
          _delInnerWrite.Add(t, Delegate.CreateDelegate(delegateType, gmRead));
        }
        Action<INISubSectionRegistryAttribute, INIFile, object, string, string> func = (Action<INISubSectionRegistryAttribute, INIFile, object, string, string>)_delInnerWrite[t];
        func.Invoke(this, f, value, fieldName, defaultSection);
      }
    }

    private void InnerWrite<T, U>(INIFile f, object obj, string fieldName, string defaultSection)
    {
      if (obj == default || !(obj is Registry<T, U> reg))
        return;

      string s = INIAttributeExt.GetSection(Section, defaultSection ?? fieldName);
      if (f.HasSection(s))
      {
        f.GetSection(s).Clear();
      }
      string k = SubsectionPrefix ?? fieldName;
      //Registry<string, string> keylinks = new Registry<string, string>();
      foreach(T key in reg.GetKeys())
      {
        string skey = Parser.Write(key);
        U o = reg[key];
        string vkey = "{0}{1}".F(k, skey);
        f.UpdateByAttribute(ref o, vkey);

        f.SetValue(s, skey, vkey);
      }
    }

    // cache
    private static Dictionary<Type, Delegate> _delInnerRead = new Dictionary<Type, Delegate>();
    private static Dictionary<Type, Delegate> _delInnerWrite = new Dictionary<Type, Delegate>();
  }
}
