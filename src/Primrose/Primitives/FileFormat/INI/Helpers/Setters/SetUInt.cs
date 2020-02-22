﻿namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Sets an uint value in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="value">The value to be set</param>
    public void SetUInt(string section, string key, uint value) { SetString(section, key, value.ToString()); }
  }
}
