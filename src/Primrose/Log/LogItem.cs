using System;

namespace Primrose
{
  internal readonly struct LogItem
  {
    public readonly LogLevel Level;
    public readonly DateTime Time;
    public readonly string Message;
    public readonly Exception Exception;

    public LogItem(LogLevel level, string message, Exception ex)
    {
      Level = level;
      Time = DateTime.Now;
      Message = message;
      Exception = ex;
    }
  }
}
