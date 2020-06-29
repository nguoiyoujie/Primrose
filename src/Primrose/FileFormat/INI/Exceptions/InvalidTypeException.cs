using Primrose.Primitives.Extensions;
using System;

namespace Primrose.FileFormat.INI
{
  /// <summary>Represents an exception as a result of a missing section</summary>
  public class INISectionNotFoundException : InvalidOperationException
  {
    /// <summary>Represents an exception as a result of a missing section</summary>
    /// <param name="section">The missing section</param>
    public INISectionNotFoundException(string section) : base(Primrose.Properties.Resources.Error_INISectionNotFound.F(section)) { }
  }

  /// <summary>Represents an exception as a result of a missing section key</summary>
  public class INIKeyNotFoundException : InvalidOperationException
  {
    /// <summary>Represents an exception as a result of a missing section key</summary>
    /// <param name="section">The section where the key is missing</param>
    /// <param name="key">The missing key</param>
    public INIKeyNotFoundException(string section, string key) : base(Primrose.Properties.Resources.Error_INIKeyNotFound.F(key, section)) { }
  }
}
