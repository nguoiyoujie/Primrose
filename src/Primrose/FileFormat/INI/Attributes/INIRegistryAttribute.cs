using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.Parsers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Primrose.FileFormat.INI
{
  //  Visual:
  //    [Section]
  //      Key1 = value1
  //      Key2 = value2
  //      Key3 = value3
  //      Key4 = value4
  //
  /// <summary>Defines a list of keys from a section of an INI file</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class INIRegistryAttribute : Attribute
  {
    /// <summary>The section name of the INI file where the keys are based on</summary>
    public string Section;

    /// <summary>Defines whether the INI file must contain this section/key combination</summary>
    public bool Required;

    /// <summary>If the value string matches this value, skip this line when writing to file</summary>
    public string NoWriteValue;

    /// <summary>Defines a list of keys from a section of an INI file</summary>
    /// <param name="section">The section name from which the keys are retrieved</param>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INIRegistryAttribute(string section, bool required = false)
    {
      Section = section ?? throw new ArgumentNullException(nameof(section));
      Required = required;
    }

    /// <summary>Defines a list of keys from a section of an INI file</summary>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INIRegistryAttribute(bool required = false)
    {
      Section = null;
      Required = required;
    }

    internal object Read(Type t, INIFile f, string defaultSection, IResolver resolver)
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
        Func<INIRegistryAttribute, INIFile, string, IResolver, object> func = (Func<INIRegistryAttribute, INIFile, string, IResolver, object>)_delInnerRead[t];
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
            ret.Add(Parser.Parse(ln.Key, default(T)), Parser.Parse(ln.Value, resolver, default(U)));
      }
      else if (Required)
      {
        throw new INISectionNotFoundException(s);
      }

      return ret;
    }

    internal void Write(Type t, INIFile f, object value, string defaultSection)
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
          Type delegateType = Expression.GetActionType(GetType(), typeof(INIFile), typeof(object), typeof(string)); // allocates, but it is called once per type, so it's ok?
#pragma warning restore HAA0101 // Array allocation for params parameter
          _delInnerWrite.Add(t, Delegate.CreateDelegate(delegateType, gmRead));
        }
        Action<INIRegistryAttribute, INIFile, object, string> func = (Action<INIRegistryAttribute, INIFile, object, string>)_delInnerWrite[t];
        func.Invoke(this, f, value, defaultSection);
      }
    }

    private void InnerWrite<T, U>(INIFile f, object obj, string defaultSection)
    {
      if (obj == default || !(obj is Registry<T, U> reg))
        return;

      string s = INIAttributeExt.GetSection(Section, defaultSection);
      if (f.HasSection(s))
      {
        f.GetSection(s).Clear();
      }
      foreach (T t in reg.GetKeys())
      {
        string key = Parser.Write(t);
        string value = Parser.Write(reg.Get(t));
        if (value != (string)NoWriteValue)
          f.SetString(s, key, value);
      }
    }

    // cache
    private static Dictionary<Type, Delegate> _delInnerRead = new Dictionary<Type, Delegate>();
    private static Dictionary<Type, Delegate> _delInnerWrite = new Dictionary<Type, Delegate>();
  }
}
