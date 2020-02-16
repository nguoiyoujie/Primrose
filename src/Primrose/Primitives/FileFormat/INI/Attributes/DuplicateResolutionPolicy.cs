using System;

namespace Primitives.FileFormat.INI
{
  /// <summary>Determines the policy for resolving duplicate entries</summary>
  public enum DuplicateResolutionPolicy
  {
    /// <summary>Throws an exception</summary>
    THROW = 0,

    /// <summary>Use the old value</summary>
    OLD = 1,

    /// <summary>Use the new value</summary>
    NEW = 2,

    /// <summary>Write both values</summary>
    BOTH = 3
  }
}
