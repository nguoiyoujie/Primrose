﻿namespace Primitives.FileFormat.INI
{
  /// <summary>Defines external methods for INI attributes</summary>
  public static class INIAttributeExt
  {
    internal static string GetSection(string section, string defaultSection)
    {
      return section ?? defaultSection ?? INIFile.PreHeaderSectionName;
    }
  }
}
