using System;

namespace Primrose.FileFormat.INI
{
  /// <summary>Defines settings for an INI file</summary>
  [AttributeUsage(AttributeTargets.Class)]
  public class INIFileConfigurationAttribute : Attribute
  {
    private static readonly string[] DefaultCommentDelimiters = new string[] { ";" };

    /// <summary>Indicates if section inheritance is supported</summary>
    public bool SupportSectionInheritance = true;

    /// <summary>Indicates a global section (key-values prior to the first section declaration) is allowed</summary>
    public bool AllowGlobalSection = true;

    /// <summary>Determines the policy for handling duplicate sections</summary>
    public DuplicateResolutionPolicy DuplicateSectionPolicy = DuplicateResolutionPolicy.THROW;

    ///// <summary>Determines the policy for handling duplicate keys</summary>
    //public DuplicateResolutionPolicy DuplicateKeyPolicy = DuplicateResolutionPolicy.THROW;

    /// <summary>Defines the delimiters for identifying the start of comments</summary>
    public string[] CommentDelimiters = DefaultCommentDelimiters;

    /// <summary>Defines the delimiter for identifying the start of a section inheritance list</summary>
    public string SectionInheritanceDelimiter = ":";

    /// <summary>Defines the delimiter for a key-value pair</summary>
    public string KeyValueDelimiter = "=";

    /// <summary>Defines settings for an INI file</summary>
    public INIFileConfigurationAttribute() { }
  }
}
