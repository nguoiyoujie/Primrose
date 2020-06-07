using Primrose.Primitives.Extensions;
using System;

namespace Primitives.FileFormat.INI
{
  /// <summary>Represents an exception as a result of a missing section</summary>
  public class INISectionNotFoundException : InvalidOperationException
  {
    /// <summary>Represents an exception as a result of a missing section</summary>
    /// <param name="section">The missing section</param>
    public INISectionNotFoundException(string section) : base("Required section '{0}' is not defined!".F(section)) { }
  }

  /// <summary>Represents an exception as a result of a missing section key</summary>
  public class INIKeyNotFoundException : InvalidOperationException
  {
    /// <summary>Represents an exception as a result of a missing section key</summary>
    /// <param name="section">The section where the key is missing</param>
    /// <param name="key">The missing key</param>
    public INIKeyNotFoundException(string section, string key) : base("Required key '{0}' in section '{1}' is not defined!".F(key, section)) { }
  }
}
