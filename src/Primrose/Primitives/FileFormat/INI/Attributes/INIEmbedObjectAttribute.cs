using System;

namespace Primitives.FileFormat.INI
{
  /// <summary>Signals to the INIFile LoadField method to load the fields contained in the class / struct </summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class INIEmbedObjectAttribute : Attribute
  {
    /// <summary>Signals to the INIFile LoadField method to load the fields contained in the class / struct </summary>
    /// <param name="overrideSection">The section name of the INI file where the key is based on</param>
    public INIEmbedObjectAttribute(string overrideSection) { Section = overrideSection; }

    /// <summary>Signals to the INIFile LoadField method to load the fields contained in the class / struct </summary>
    public INIEmbedObjectAttribute() { }

    /// <summary>The section name of the INI file where the keys in the attributed object are based on</summary>
    public string Section;
  }
}
