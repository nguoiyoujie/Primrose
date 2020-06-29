using Primrose.Primitives.Parsers;
using System;
using System.Reflection;

namespace Primrose.FileFormat.INI
{
  public partial class INIFile
  {
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
        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIEmbedObjectAttribute), true))
        {
          object fObj = fi.GetValue(obj) ?? Activator.CreateInstance(t);
          LoadByAttribute(ref fObj, ((INIEmbedObjectAttribute)a).Section ?? defaultSection, resolver);
          fi.SetValue(obj, fObj);
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIHeaderAttribute), true))
        {
          fi.SetValue(obj, ((INIHeaderAttribute)a).Read(defaultSection));
        }
        
        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIValueAttribute), true))
        {
          fi.SetValue(obj, ((INIValueAttribute)a).Read(t, this, fi.GetValue(obj), fi.Name, defaultSection, resolver));
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIKeyListAttribute), true))
        {
          fi.SetValue(obj, ((INIKeyListAttribute)a).Read(t, this, defaultSection));
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIRegistryAttribute), true))
        {
          fi.SetValue(obj, ((INIRegistryAttribute)a).Read(t, this, defaultSection, resolver));
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INISubSectionListAttribute), true))
        {
          fi.SetValue(obj, ((INISubSectionListAttribute)a).Read(t, this, fi.Name, defaultSection, resolver));
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INISubSectionKeyListAttribute), true))
        {
          fi.SetValue(obj, ((INISubSectionKeyListAttribute)a).Read(t, this, fi.Name, defaultSection, resolver));
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
          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIEmbedObjectAttribute), true))
          {
            object fObj = pi.GetValue(obj, null) ?? Activator.CreateInstance(t);
            LoadByAttribute(ref fObj, ((INIEmbedObjectAttribute)a).Section ?? defaultSection, resolver);
            pi.SetValue(obj, fObj, null);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIHeaderAttribute), true))
          {
            pi.SetValue(obj, ((INIHeaderAttribute)a).Read(defaultSection), null);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIValueAttribute), true))
          {
            pi.SetValue(obj, ((INIValueAttribute)a).Read(t, this, pi.GetValue(obj, null), pi.Name, defaultSection, resolver), null);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIKeyListAttribute), true))
          {
            pi.SetValue(obj, ((INIKeyListAttribute)a).Read(t, this, defaultSection), null);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIRegistryAttribute), true))
          {
            pi.SetValue(obj, ((INIRegistryAttribute)a).Read(t, this, defaultSection, resolver), null);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INISubSectionListAttribute), true))
          {
            pi.SetValue(obj, ((INISubSectionListAttribute)a).Read(t, this, pi.Name, defaultSection, resolver), null);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INISubSectionKeyListAttribute), true))
          {
            pi.SetValue(obj, ((INISubSectionKeyListAttribute)a).Read(t, this, pi.Name, defaultSection, resolver), null);
          }
        }
      }
    }

    /// <summary>Updates the elements from the fields of another class</summary>
    public void UpdateByAttribute<T>(ref T target, string defaultSection = null)
    {
      Type tt = target.GetType();
      object boxed = target; // unfortunately, it is either boxing or using TypedReference
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
        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIEmbedObjectAttribute), true))
        {
          object fObj = fi.GetValue(obj) ?? Activator.CreateInstance(t);
          UpdateByAttribute(ref fObj, ((INIEmbedObjectAttribute)a).Section ?? defaultSection);
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIValueAttribute), false))
        {
          ((INIValueAttribute)a).Write(t, this, fi.GetValue(obj), fi.Name, defaultSection);
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIKeyListAttribute), false))
        {
          ((INIKeyListAttribute)a).Write(t, this, (string[])fi.GetValue(obj), defaultSection);
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIRegistryAttribute), true))
        {
          ((INIRegistryAttribute)a).Write(t, this, fi.GetValue(obj), defaultSection);
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INISubSectionListAttribute), true))
        {
          ((INISubSectionListAttribute)a).Write(t, this, fi.GetValue(obj), fi.Name, defaultSection);
        }

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INISubSectionKeyListAttribute), true))
        {
          ((INISubSectionKeyListAttribute)a).Write(t, this, fi.GetValue(obj), fi.Name, defaultSection);
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
          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIEmbedObjectAttribute), true))
          {
            object fObj = pi.GetValue(obj, null) ?? Activator.CreateInstance(t);
            UpdateByAttribute(ref fObj, ((INIEmbedObjectAttribute)a).Section ?? defaultSection);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIValueAttribute), true))
          {
            ((INIValueAttribute)a).Write(t, this, pi.GetValue(obj, null), pi.Name, defaultSection);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIKeyListAttribute), true))
          {
            ((INIKeyListAttribute)a).Write(t, this, (string[])pi.GetValue(obj, null), defaultSection);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INIRegistryAttribute), true))
          {
            ((INIRegistryAttribute)a).Write(t, this, pi.GetValue(obj, null), defaultSection);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INISubSectionListAttribute), true))
          {
            ((INISubSectionListAttribute)a).Write(t, this, pi.GetValue(obj, null), pi.Name, defaultSection);
          }

          foreach (Attribute a in pi.GetCustomAttributes(typeof(INISubSectionKeyListAttribute), true))
          {
            ((INISubSectionKeyListAttribute)a).Write(t, this, pi.GetValue(obj, null), pi.Name, defaultSection);
          }
        }
      }
    }

  }
}
