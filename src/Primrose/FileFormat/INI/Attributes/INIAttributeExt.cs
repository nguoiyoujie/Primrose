namespace Primrose.FileFormat.INI
{
  /// <summary>Defines external methods for INI attributes</summary>
  public static class INIAttributeExt
  {
    internal static string GetSection(string section, string defaultSection)
    {
      return section ?? defaultSection ?? INIFile.PreHeaderSectionName;
    }

    internal static string GetKey(string key, string defaultKey)
    {
      return key ?? defaultKey;
    }
  }
}
