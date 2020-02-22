using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using System;
using System.Reflection;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>Passes the elements into the fields of another class</summary>
    public void LoadByAttribute<T>(ref T target, string sectionOverride = null)
    {
      Type tt = target.GetType();
      object boxed = target; // for this to work on structs/valuetypes it is either boxing or using SetValueDirect/TypedReference.
      Load(boxed, tt, sectionOverride);
      target = (T)boxed;
    }

    internal void Load(object obj, Type tt, string sectionOverride)
    {
      LoadFields(obj, tt, sectionOverride);
      LoadProperties(obj, tt, sectionOverride);

      if (tt.BaseType != null)
        Load(obj, tt.BaseType, sectionOverride);
    }

    private void LoadFields(object obj, Type tt, string sectionOverride)
    {
      foreach (FieldInfo fi in tt.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
      {
        Type t = fi.FieldType;
        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIEmbedObjectAttribute), true))
        {
          object fObj = fi.GetValue(obj) ?? Activator.CreateInstance(t);
          LoadByAttribute(ref fObj, sectionOverride);
          fi.SetValue(obj, fObj);
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIValueAttribute), true))
        {
          fi.SetValue(obj, ((INIValueAttribute)a).Read(t, this, fi.GetValue(obj), sectionOverride));
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIKeyListAttribute), true))
        {
          fi.SetValue(obj, ((INIKeyListAttribute)a).Read(t, this, sectionOverride));
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIRegistryAttribute), true))
        {
          fi.SetValue(obj, ((INIRegistryAttribute)a).Read(t, this, sectionOverride));
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INISubSectionListAttribute), true))
        {
          fi.SetValue(obj, ((INISubSectionListAttribute)a).Read(t, this, sectionOverride));
        }
      }
    }

    private void LoadProperties(object obj, Type tt, string sectionOverride)
    {
      foreach (PropertyInfo pi in tt.GetProperties())
      {
        Type t = pi.PropertyType;
        if (pi.GetIndexParameters().Length == 0) // ignore indexed properties
        {
          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIEmbedObjectAttribute), true))
          {
            object fObj = pi.GetValue(obj, null) ?? Activator.CreateInstance(t);
            LoadByAttribute(ref fObj, sectionOverride);
            pi.SetValue(obj, fObj, null);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIValueAttribute), true))
          {
            pi.SetValue(obj, ((INIValueAttribute)a).Read(t, this, pi.GetValue(obj, null), sectionOverride), null);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIKeyListAttribute), true))
          {
            pi.SetValue(obj, ((INIKeyListAttribute)a).Read(t, this, sectionOverride), null);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIRegistryAttribute), true))
          {
            pi.SetValue(obj, ((INIRegistryAttribute)a).Read(t, this, sectionOverride), null);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INISubSectionListAttribute), true))
          {
            pi.SetValue(obj, ((INISubSectionListAttribute)a).Read(t, this, sectionOverride), null);
          }
        }
      }
    }

    /// <summary>Updates the elements from the fields of another class</summary>
    public void UpdateByAttribute<T>(ref T target, string sectionOverride = null)
    {
      Type tt = target.GetType();
      object boxed = target; // unfortunately, it is either boxing or using TypedReference
      Update(boxed, tt, sectionOverride);
    }

    private void Update(object obj, Type tt, string sectionOverride)
    {
      UpdateFields(obj, tt, sectionOverride);
      UpdateProperties(obj, tt, sectionOverride);

      if (tt.BaseType != null)
        Load(obj, tt.BaseType, sectionOverride);
    }

    private void UpdateFields(object obj, Type tt, string sectionOverride)
    {
      foreach (FieldInfo fi in tt.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
      {
        Type t = fi.FieldType;
        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIEmbedObjectAttribute), true))
        {
          object fObj = fi.GetValue(obj) ?? Activator.CreateInstance(t);
          UpdateByAttribute(ref fObj, sectionOverride);
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIValueAttribute), false))
        {
          ((INIValueAttribute)a).Write(t, this, fi.GetValue(obj), sectionOverride);
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIKeyListAttribute), false))
        {
          ((INIKeyListAttribute)a).Write(t, this, (string[])fi.GetValue(obj), sectionOverride);
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIRegistryAttribute), true))
        {
          ((INIRegistryAttribute)a).Write(t, this, fi.GetValue(obj), sectionOverride);
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INISubSectionListAttribute), true))
        {
          ((INISubSectionListAttribute)a).Write(t, this, fi.GetValue(obj), sectionOverride);
        }
      }
    }

    private void UpdateProperties(object obj, Type tt, string sectionOverride)
    {
      foreach (PropertyInfo pi in tt.GetProperties())
      {
        Type t = pi.PropertyType;
        if (pi.GetIndexParameters().Length == 0) // ignore indexed properties
        {
          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIEmbedObjectAttribute), true))
          {
            object fObj = pi.GetValue(obj, null) ?? Activator.CreateInstance(t);
            UpdateByAttribute(ref fObj, sectionOverride);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIValueAttribute), true))
          {
            ((INIValueAttribute)a).Write(t, this, pi.GetValue(obj, null), sectionOverride);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIKeyListAttribute), true))
          {
            ((INIKeyListAttribute)a).Write(t, this, (string[])pi.GetValue(obj, null), sectionOverride);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIRegistryAttribute), true))
          {
            ((INIRegistryAttribute)a).Write(t, this, pi.GetValue(obj, null), sectionOverride);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INISubSectionListAttribute), true))
          {
            ((INISubSectionListAttribute)a).Write(t, this, pi.GetValue(obj, null), sectionOverride);
          }
        }
      }
    }

  }
}
