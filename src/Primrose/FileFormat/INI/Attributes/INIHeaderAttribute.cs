using System;

namespace Primrose.FileFormat.INI
{
  /// <summary>Defines a value from a section header of an INI file</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class INIHeaderAttribute : Attribute
  {
    /// <summary>Defines a value from a section header of an INI file</summary>
    public INIHeaderAttribute() { }

    internal string Read(string defaultSection)
    {
      return defaultSection;
    }
  }
}
