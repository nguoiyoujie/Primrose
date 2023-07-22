using Primrose.Primitives.Cache;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        throw new InvalidOperationException(Resource.Strings.Error_INISubSectionListInvalidType.F(t.Name));

      lock (_delInnerRead)
      {
        if (!_delInnerRead.ContainsKey(t))
        {
          _type1r[0] = t.GetElementType();
          MethodInfo mRead = GetType().GetMethod(nameof(InnerRead), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
          MethodInfo gmRead = mRead.MakeGenericMethod(_type1r);
#pragma warning disable HAA0101 // Array allocation for params parameter
          Type delegateType = Expression.GetFuncType(GetType(), typeof(INIFile), typeof(string), typeof(string), typeof(IResolver), typeof(object)); // allocates, but it is called once per type, so it's ok?
#pragma warning restore HAA0101 // Array allocation for params parameter
          _delInnerRead.Add(t, Delegate.CreateDelegate(delegateType, gmRead));
        }
        Func<INISubSectionListAttribute, INIFile, string, string, IResolver, object> func = (Func<INISubSectionListAttribute, INIFile, string, string, IResolver, object>)_delInnerRead[t];
        return func.Invoke(this, f, fieldName, defaultSection, resolver);
      }
    }

    private T[] InnerRead<T>(INIFile f, string fieldName, string defaultSection, IResolver resolver)
    {
      string s = INIAttributeExt.GetSection(Section, defaultSection);
      string k = INIAttributeExt.GetKey(Key, fieldName);
      string[] list =  f.GetValue(s, k, resolver, Array<string>.Empty);
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
        throw new InvalidOperationException(Resource.Strings.Error_INISubSectionListInvalidType.F(t.Name));

      lock (_delInnerWrite)
      {
        if (!_delInnerWrite.ContainsKey(t))
        {
          _type1w[0] = t.GetElementType();
          MethodInfo mRead = GetType().GetMethod(nameof(InnerWrite), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
          MethodInfo gmRead = mRead.MakeGenericMethod(_type1w);
#pragma warning disable HAA0101 // Array allocation for params parameter
          Type delegateType = Expression.GetActionType(GetType(), typeof(INIFile), typeof(object), typeof(string), typeof(string)); // allocates, but it is called once per type, so it's ok?
#pragma warning restore HAA0101 // Array allocation for params parameter
          _delInnerWrite.Add(t, Delegate.CreateDelegate(delegateType, gmRead));
        }
        Action<INISubSectionListAttribute, INIFile, object, string, string> func = (Action<INISubSectionListAttribute, INIFile, object, string, string>)_delInnerWrite[t];
        func.Invoke(this, f, value, fieldName, defaultSection);
      }
    }

    private void InnerWrite<T>(INIFile f, object obj, string fieldName, string defaultSection)
    {
      if (obj == default || !(obj is T[] value))
        return;

      string s = INIAttributeExt.GetSection(Section, defaultSection);
      if (f.HasSection(s))
      {
        f.GetSection(s).Clear();
      }
      string k = SubsectionPrefix ?? INIAttributeExt.GetKey(Key, fieldName);
      string[] keys = new string[value.Length];
      for (int i = 0; i < value.Length; i++)
      {
        T o = value[i];
        keys[i] = fieldName + ToStringCache<int>.Get(i);
        f.UpdateByAttribute(ref o, keys[i]);
      }
      f.SetValue(s, k, Parser.Write(keys));
    }

    // cache
    private static Type[] _type1r = new Type[1];
    private static Type[] _type1w = new Type[1];
    private static Dictionary<Type, Delegate> _delInnerRead = new Dictionary<Type, Delegate>();
    private static Dictionary<Type, Delegate> _delInnerWrite = new Dictionary<Type, Delegate>();
  }
}
