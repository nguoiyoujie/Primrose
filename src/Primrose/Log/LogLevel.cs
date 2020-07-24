using System;

namespace Primrose
{
  /// <summary>Defines the level of a log entry</summary>
  [Flags]
  public enum LogLevel
  {
    /// <summary>Indicates no log categories</summary>
    NONE = 0,

    /// <summary>Indicates that the log item is used for debugging</summary>
    DEBUG = 0x1,

    /// <summary>Indicates that the log item is used for general information</summary>
    INFO = 0x2,

    /// <summary>Indicates that the log item is used to trace a function or object</summary>
    TRACE = 0x4,

    /// <summary>Indicates that the log item is used as a warning</summary>
    WARN = 0x8,

    /// <summary>Indicates that the log item describes an error</summary>
    ERROR = 0x10,

    /// <summary>Indicates that the log item describes a fatal error</summary>
    FATAL = 0x20,

    /// <summary>Indicates all log categories</summary>
    ALL = 0xFF
  }
}
