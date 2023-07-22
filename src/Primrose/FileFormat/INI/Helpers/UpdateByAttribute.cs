using Primrose.Primitives.Parsers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Primrose.FileFormat.INI
{
  public partial class INIFile
  {
    // Delegate: Func<T, TValue>
    //private Dictionary<string, Delegate> _getter = new Dictionary<string, Delegate>();

    // Delegate: Action<T, TValue>
    //private Dictionary<string, Delegate> _setter = new Dictionary<string, Delegate>();

    /// <summary>Passes the elements into the fields of another class</summary>
    public void LoadByAttribute<T>(ref T target, string defaultSection = null, IResolver resolver = null)
    {
      Type tt = target.GetType();
      object boxed = target; // for this to work on structs/valuetypes it is either boxing or using SetValueDirect/TypedReference.
      Load(boxed, tt, defaultSection, resolver);
      target = (T)boxed;
    }

    internal void Load(object obj, Type tt, string defaultSection, IResolver resolver)
    {
      LoadFields(obj, tt, defaultSection, resolver);
      LoadProperties(obj, tt, defaultSection, resolver);

      if (tt.BaseType != null)
        Load(obj, tt.BaseType, defaultSection, resolver);
    }

    private void LoadFields(object obj, Type tt, string defaultSection, IResolver resolver)
    {
      foreach (FieldInfo fi in tt.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
      {
        Type t = fi.FieldType;
        foreach (Attribute a in fi.GetCustomAttributes( true)) // allocates
        {
          if (a is INIEmbedObjectAttribute iniEmbedObjectAttribute)
          {
            object fObj = fi.GetValue(obj) ?? Activator.CreateInstance(t);
            LoadByAttribute(ref fObj, iniEmbedObjectAttribute.Section ?? defaultSection, resolver);
            fi.SetValue(obj, fObj);
          }
          else if (a is INIHeaderAttribute iniHeaderAttribute)
          {
            fi.SetValue(obj, iniHeaderAttribute.Read(defaultSection));
          }
          else if (a is INIValueAttribute iniValueAttribute)
          {
            fi.SetValue(obj, iniValueAttribute.Read(t, this, fi.GetValue(obj), fi.Name, defaultSection, resolver));
          }
          else if (a is INIKeyListAttribute iniKeyListAttribute)
          {
            fi.SetValue(obj, iniKeyListAttribute.Read(t, this, defaultSection));
          }
          else if (a is INIRegistryAttribute iniRegistryAttribute)
          {
            fi.SetValue(obj, iniRegistryAttribute.Read(t, this, defaultSection, resolver));
          }
          else if (a is INISubSectionListAttribute iniSubSectionListAttribute)
          {
            fi.SetValue(obj, iniSubSectionListAttribute.Read(t, this, fi.Name, defaultSection, resolver));
          }
          else if (a is INISubSectionKeyListAttribute iniSubSectionKeyListAttribute)
          {
            fi.SetValue(obj, iniSubSectionKeyListAttribute.Read(t, this, fi.Name, defaultSection, resolver));
          }
          else if (a is INISubSectionRegistryAttribute iniSubSectionRegistryAttribute)
          {
            fi.SetValue(obj, iniSubSectionRegistryAttribute.Read(t, this, fi.Name, defaultSection, resolver));
          }
        }
      }
    }

    private void LoadProperties(object obj, Type tt, string defaultSection, IResolver resolver)
    {
      foreach (PropertyInfo pi in tt.GetProperties())
      {
        Type t = pi.PropertyType;
        if (pi.GetIndexParameters().Length == 0) // ignore indexed properties
        {
          foreach (Attribute a in pi.GetCustomAttributes(true)) // allocates
          {
            if (a is INIEmbedObjectAttribute iniEmbedObjectAttribute)
            {
              object fObj = pi.GetValue(obj, null) ?? Activator.CreateInstance(t);
              LoadByAttribute(ref fObj, iniEmbedObjectAttribute.Section ?? defaultSection, resolver);
              pi.SetValue(obj, fObj, null);
            }
            else if (a is INIHeaderAttribute iniHeaderAttribute)
            {
              pi.SetValue(obj, iniHeaderAttribute.Read(defaultSection), null);
            }
            else if (a is INIValueAttribute iniValueAttribute)
            {
              pi.SetValue(obj, iniValueAttribute.Read(t, this, pi.GetValue(obj, null), pi.Name, defaultSection, resolver), null);
            }
            else if (a is INIKeyListAttribute iniKeyListAttribute)
            {
              pi.SetValue(obj, iniKeyListAttribute.Read(t, this, defaultSection), null);
            }
            else if (a is INIRegistryAttribute iniRegistryAttribute)
            {
              pi.SetValue(obj, iniRegistryAttribute.Read(t, this, defaultSection, resolver), null);
            }
            else if (a is INISubSectionListAttribute iniSubSectionListAttribute)
            {
              pi.SetValue(obj, iniSubSectionListAttribute.Read(t, this, pi.Name, defaultSection, resolver), null);
            }
            else if (a is INISubSectionKeyListAttribute iniSubSectionKeyListAttribute)
            {
              pi.SetValue(obj, iniSubSectionKeyListAttribute.Read(t, this, pi.Name, defaultSection, resolver), null);
            }
            else if (a is INISubSectionRegistryAttribute iniSubSectionRegistryAttribute)
            {
              pi.SetValue(obj, iniSubSectionRegistryAttribute.Read(t, this, pi.Name, defaultSection, resolver), null);
            }
          }
        }
      }
    }

    /// <summary>Updates the elements from the fields of another class</summary>
    public void UpdateByAttribute<T>(ref T target, string defaultSection = null)
    {
      Type tt = target.GetType();
#pragma warning disable HAA0601 // Explicit boxing for use by reflection
      // unfortunately, it is either boxing or using TypedReference
      object boxed = target;
#pragma warning restore HAA0601
      Update(boxed, tt, defaultSection);
    }

    private void Update(object obj, Type tt, string defaultSection)
    {
      UpdateFields(obj, tt, defaultSection);
      UpdateProperties(obj, tt, defaultSection);

      if (tt.BaseType != null)
        Update(obj, tt.BaseType, defaultSection);
    }

    private void UpdateFields(object obj, Type tt, string defaultSection)
    {
      foreach (FieldInfo fi in tt.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
      {
        Type t = fi.FieldType;
        foreach (Attribute a in fi.GetCustomAttributes(true)) // allocates
        {
          if (a is INIHeaderAttribute iniHeaderAttribute)
          {
            defaultSection = fi.GetValue(obj)?.ToString() ?? defaultSection;
          }
          else if (a is INIEmbedObjectAttribute iniEmbedObjectAttribute)
          {
            object fObj = fi.GetValue(obj) ?? Activator.CreateInstance(t);
            UpdateByAttribute(ref fObj, iniEmbedObjectAttribute.Section ?? defaultSection);
          }
          else if (a is INIValueAttribute iniValueAttribute)
          {
            iniValueAttribute.Write(t, this, fi.GetValue(obj), fi.Name, defaultSection);
          }
          else if (a is INIKeyListAttribute iniKeyListAttribute)
          {
            iniKeyListAttribute.Write(t, this, (IEnumerable<string>)fi.GetValue(obj), defaultSection);
          }
          else if (a is INIRegistryAttribute iniRegistryAttribute)
          {
            iniRegistryAttribute.Write(t, this, fi.GetValue(obj), defaultSection);
          }
          else if (a is INISubSectionListAttribute iniSubSectionListAttribute)
          {
            iniSubSectionListAttribute.Write(t, this, fi.GetValue(obj), fi.Name, defaultSection);
          }
          else if (a is INISubSectionKeyListAttribute iniSubSectionKeyListAttribute)
          {
            iniSubSectionKeyListAttribute.Write(t, this, fi.GetValue(obj), fi.Name, defaultSection);
          }
          else if (a is INISubSectionRegistryAttribute iniSubSectionRegistryAttribute)
          {
            iniSubSectionRegistryAttribute.Write(t, this, fi.GetValue(obj), fi.Name, defaultSection);
          }
        }
      }
    }

    private void UpdateProperties(object obj, Type tt, string defaultSection)
    {
      foreach (PropertyInfo pi in tt.GetProperties())
      {
        Type t = pi.PropertyType;
        if (pi.GetIndexParameters().Length == 0) // ignore indexed properties
        {
          foreach (Attribute a in pi.GetCustomAttributes(true)) // allocates
          {
            if (a is INIHeaderAttribute iniHeaderAttribute)
            {
              defaultSection = pi.GetValue(obj, null)?.ToString() ?? defaultSection;
            }
            else if (a is INIEmbedObjectAttribute iniEmbedObjectAttribute)
            {
              object fObj = pi.GetValue(obj, null) ?? Activator.CreateInstance(t);
              UpdateByAttribute(ref fObj, iniEmbedObjectAttribute.Section ?? defaultSection);
            }
            else if (a is INIValueAttribute iniValueAttribute)
            {
              iniValueAttribute.Write(t, this, pi.GetValue(obj, null), pi.Name, defaultSection);
            }
            else if (a is INIKeyListAttribute iniKeyListAttribute)
            {
              iniKeyListAttribute.Write(t, this, (IEnumerable<string>)pi.GetValue(obj, null), defaultSection);
            }
            else if (a is INIRegistryAttribute iniRegistryAttribute)
            {
              iniRegistryAttribute.Write(t, this, pi.GetValue(obj, null), defaultSection);
            }
            else if (a is INISubSectionListAttribute iniSubSectionListAttribute)
            {
              iniSubSectionListAttribute.Write(t, this, pi.GetValue(obj, null), pi.Name, defaultSection);
            }
            else if (a is INISubSectionKeyListAttribute iniSubSectionKeyListAttribute)
            {
              iniSubSectionKeyListAttribute.Write(t, this, pi.GetValue(obj, null), pi.Name, defaultSection);
            }
            else if (a is INISubSectionRegistryAttribute iniSubSectionRegistryAttribute)
            {
              iniSubSectionRegistryAttribute.Write(t, this, pi.GetValue(obj, null), pi.Name, defaultSection);
            }
          }
        }
      }
    }

  }
}
