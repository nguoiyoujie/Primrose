using Primrose.Primitives.Parsers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Primrose.FileFormat.INI
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

    /// <summary>If the value string matches this value, skip this line when writing to file</summary>
    public object NoWriteValue;

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
      lock (_delInnerRead)
      {
        if (!_delInnerRead.ContainsKey(t))
        {
          _type1r[0] = t;
          MethodInfo mRead = GetType().GetMethod(nameof(InnerRead), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
          MethodInfo gmRead = mRead.MakeGenericMethod(_type1r);
#pragma warning disable HAA0101 // Array allocation for params parameter
          Type delegateType = Expression.GetFuncType(GetType(), typeof(INIFile), typeof(object), typeof(string), typeof(string), typeof(IResolver), typeof(object)); // allocates, but it is called once per type, so it's ok?
#pragma warning restore HAA0101 // Array allocation for params parameter
          _delInnerRead.Add(t, Delegate.CreateDelegate(delegateType, gmRead));
        }
        Func<INIValueAttribute, INIFile, object, string, string, IResolver, object> func = (Func<INIValueAttribute, INIFile, object, string, string, IResolver, object>)_delInnerRead[t];
        return func.Invoke(this, f, defaultValue, fieldName, defaultSection, resolver);
      }
    }

    private object InnerRead<T>(INIFile f, object defaultObj, string fieldName, string defaultSection, IResolver resolver)
    {
      T defaultValue = (T)defaultObj;
      string s = INIAttributeExt.GetSection(Section, defaultSection);
      string k = INIAttributeExt.GetKey(Key, fieldName);
      if (Required && !f.HasKey(s, k))
        throw new INIKeyNotFoundException(s, k);

      return Parser.Parse(f.GetString(s, k, null), resolver, defaultValue);
    }

    internal void Write(Type t, INIFile f, object value, string fieldName, string defaultSection)
    {
      lock (_delInnerWrite)
      {
        if (!_delInnerWrite.ContainsKey(t))
        {
          _type1w[0] = t;
          MethodInfo mRead = GetType().GetMethod(nameof(InnerWrite), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
          MethodInfo gmRead = mRead.MakeGenericMethod(_type1w);
#pragma warning disable HAA0101 // Array allocation for params parameter
          Type delegateType = Expression.GetActionType(GetType(), typeof(INIFile), typeof(object), typeof(string), typeof(string)); // allocates, but it is called once per type, so it's ok?
#pragma warning restore HAA0101 // Array allocation for params parameter
          _delInnerWrite.Add(t, Delegate.CreateDelegate(delegateType, gmRead));
        }
        Action<INIValueAttribute, INIFile, object, string, string> func = (Action<INIValueAttribute, INIFile, object, string, string>)_delInnerWrite[t];
        func.Invoke(this, f, value, fieldName, defaultSection);
      }
    }

    private void InnerWrite<T>(INIFile f, object obj, string fieldName, string defaultSection)
    {
      if (obj == default || !(obj is T value))
        return;

      string s = INIAttributeExt.GetSection(Section, defaultSection);
      string k = INIAttributeExt.GetKey(Key, fieldName);
      string v = Parser.Write(value);
      if (!v.Equals(NoWriteValue))
        f.SetString(s, k, v);
    }

    // cache
    private static object _lock = new object();
    private static Type[] _type1 = new Type[1];
    private static object[] _object4 = new object[4];
    private static object[] _object5 = new object[5];

    // cache
    private static Type[] _type1r = new Type[1];
    private static Type[] _type1w = new Type[1];
    private static Dictionary<Type, Delegate> _delInnerRead = new Dictionary<Type, Delegate>();
    private static Dictionary<Type, Delegate> _delInnerWrite = new Dictionary<Type, Delegate>();
  }
}
