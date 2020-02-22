using System;

namespace Primitives.FileFormat.INI
{
  /// <summary>Signals to the INIFile LoadField method to load the fields contained in the class / struct </summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class INIEmbedObjectAttribute : Attribute
  {
    /// <summary>Signals to the INIFile LoadField method to </summary>
    public INIEmbedObjectAttribute() { }
  }
}
