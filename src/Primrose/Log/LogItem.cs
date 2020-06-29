using System;

namespace Primrose
{
  internal struct LogItem
  {
    public string Channel;
    public DateTime Time;
    public string Message;

    public LogItem(string channel, DateTime time, string message)
    {
      Channel = channel;
      Time = time;
      Message = message;
    }
  }
}
