using Primrose.Primitives;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.Parsers;
using Primrose.Primitives.ValueTypes;
using System;
using System.Reflection;
using System.Text;

namespace Primitives.FileFormat.INI
{
  /// <summary>Defines a value from a section and key of an INI file</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class INIValueAttribute : Attribute
  {
    private static Registry<Type, Func<INIFile, string, string, object, object>> _getter = new Registry<Type, Func<INIFile, string, string, object, object>>();
    private static Registry<Type, Action<INIFile, string, string, object>> _setter = new Registry<Type, Action<INIFile, string, string, object>>();

    static INIValueAttribute()
    {
      _getter.Add(typeof(bool), (f, s, k, d) => f.GetBool(s, k, (bool)d));
      _getter.Add(typeof(bool[]), (f, s, k, d) => f.GetBoolArray(s, k, (bool[])d));
      _getter.Add(typeof(double), (f, s, k, d) => f.GetDouble(s, k, (double)d));
      _getter.Add(typeof(double[]), (f, s, k, d) => f.GetDoubleArray(s, k, (double[])d));
      _getter.Add(typeof(float), (f, s, k, d) => f.GetFloat(s, k, (float)d));
      _getter.Add(typeof(float[]), (f, s, k, d) => f.GetFloatArray(s, k, (float[])d));
      _getter.Add(typeof(float2), (f, s, k, d) => f.GetFloat2(s, k, (float2)d));
      _getter.Add(typeof(float2[]), (f, s, k, d) => f.GetFloat2Array(s, k, (float2[])d));
      _getter.Add(typeof(float3), (f, s, k, d) => f.GetFloat3(s, k, (float3)d));
      _getter.Add(typeof(float3[]), (f, s, k, d) => f.GetFloat3Array(s, k, (float3[])d));
      _getter.Add(typeof(float4), (f, s, k, d) => f.GetFloat4(s, k, (float4)d));
      _getter.Add(typeof(float4[]), (f, s, k, d) => f.GetFloat4Array(s, k, (float4[])d));
      _getter.Add(typeof(int), (f, s, k, d) => f.GetInt(s, k, (int)d));
      _getter.Add(typeof(int[]), (f, s, k, d) => f.GetIntArray(s, k, (int[])d));
      _getter.Add(typeof(string), (f, s, k, d) => f.GetString(s, k, (string)d));
      _getter.Add(typeof(string[]), (f, s, k, d) => f.GetStringArray(s, k, (string[])d));
      _getter.Add(typeof(StringBuilder[]), (f, s, k, d) => f.GetStringBuilderArray(s, k, (StringBuilder[])d));
      _getter.Add(typeof(uint), (f, s, k, d) => f.GetUInt(s, k, (uint)d));
      _getter.Add(typeof(uint[]), (f, s, k, d) => f.GetUIntArray(s, k, (uint[])d));
      _getter.Add(typeof(long), (f, s, k, d) => f.GetLong(s, k, (long)d));
      _getter.Add(typeof(long[]), (f, s, k, d) => f.GetLongArray(s, k, (long[])d));

      _setter.Add(typeof(bool), (f, s, k, d) => f.SetBool(s, k, (bool)d));
      _setter.Add(typeof(bool[]), (f, s, k, d) => f.SetBoolArray(s, k, (bool[])d));
      _setter.Add(typeof(double), (f, s, k, d) => f.SetDouble(s, k, (double)d));
      _setter.Add(typeof(double[]), (f, s, k, d) => f.SetDoubleArray(s, k, (double[])d));
      _setter.Add(typeof(float), (f, s, k, d) => f.SetFloat(s, k, (float)d));
      _setter.Add(typeof(float[]), (f, s, k, d) => f.SetFloatArray(s, k, (float[])d));
      _setter.Add(typeof(float2), (f, s, k, d) => f.SetFloat2(s, k, (float2)d));
      _setter.Add(typeof(float2[]), (f, s, k, d) => f.SetFloat2Array(s, k, (float2[])d));
      _setter.Add(typeof(float3), (f, s, k, d) => f.SetFloat3(s, k, (float3)d));
      _setter.Add(typeof(float3[]), (f, s, k, d) => f.SetFloat3Array(s, k, (float3[])d));
      _setter.Add(typeof(float4), (f, s, k, d) => f.SetFloat4(s, k, (float4)d));
      _setter.Add(typeof(float4[]), (f, s, k, d) => f.SetFloat4Array(s, k, (float4[])d));
      _setter.Add(typeof(int), (f, s, k, d) => f.SetInt(s, k, (int)d));
      _setter.Add(typeof(int[]), (f, s, k, d) => f.SetIntArray(s, k, (int[])d));
      _setter.Add(typeof(string), (f, s, k, d) => f.SetString(s, k, (string)d));
      _setter.Add(typeof(string[]), (f, s, k, d) => f.SetStringArray(s, k, (string[])d));
      _setter.Add(typeof(StringBuilder[]), (f, s, k, d) => f.SetStringBuilderArray(s, k, (StringBuilder[])d));
      _setter.Add(typeof(uint), (f, s, k, d) => f.SetUInt(s, k, (uint)d));
      _setter.Add(typeof(uint[]), (f, s, k, d) => f.SetUIntArray(s, k, (uint[])d));
      _setter.Add(typeof(long), (f, s, k, d) => f.SetLong(s, k, (long)d));
      _setter.Add(typeof(long[]), (f, s, k, d) => f.SetLongArray(s, k, (long[])d));
    }

    /// <summary>The section name of the INI file where the key is based on</summary>
    public string Section;

    /// <summary>The key name of the INI file where the value is read</summary>
    public string Key;

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
      if (section == null)
        throw new ArgumentNullException("section");

      if (key == null)
        throw new ArgumentNullException("key");

      Section = section;
      Key = key;
      Required = required;
    }

    /// <summary>
    /// <summary>Defines a value from a section and key of an INI file</summary>
    /// </summary>
    /// <param name="key">The key name from which the value is retrieved</param>
    /// <param name="required">Defines whether the INI file must contain this section/key combination</param>
    public INIValueAttribute(string key, bool required = false)
    {
      if (key == null)
        throw new ArgumentNullException("key");

      Section = null;
      Key = key;
      Required = required;
    }

    internal object Read(Type t, INIFile f, object defaultValue, string defaultSection)
    {
      MethodInfo mRead = GetType().GetMethod("InnerRead", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t);
      return gmRead.Invoke(this, new object[] { f, defaultValue, defaultSection });
    }

    private T InnerRead<T>(INIFile f, T defaultValue, string defaultSection)
    {
      string s = INIAttributeExt.GetSection(Section, defaultSection);
      if (Required && !f.HasKey(s, Key))
        throw new InvalidOperationException("Required key '{0}' in section '{1}' is not defined!".F(Key, s));

      return Parser.Parse(f.GetString(s, Key, null), defaultValue);
    }

    internal void Write(Type t, INIFile f, object value, string defaultSection)
    {
      MethodInfo mRead = GetType().GetMethod("InnerWrite", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      MethodInfo gmRead = mRead.MakeGenericMethod(t);
      gmRead.Invoke(this, new object[] { f, value, defaultSection});
    }

    private void InnerWrite<T>(INIFile f, T value, string defaultSection)
    {
      string s = INIAttributeExt.GetSection(Section, defaultSection);
      f.SetString(s, Key, Parser.Write(value));
    }
  }
}
