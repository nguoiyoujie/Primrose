using Primrose.Primitives.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Timers;

namespace Primrose
{
  /// <summary>Handles logging behaviour for the program</summary>
  public static class Log
  {
    /// <summary>The default log extension appended to log files, unless explicitly defined</summary>
    public const string LOG_EXT = "log";
    private static readonly Timer _logTimer = new Timer();
    private static readonly Dictionary<string, TextWriter> Loggers = new Dictionary<string, TextWriter>();
    private static readonly ConcurrentQueue<LogItem> Queue = new ConcurrentQueue<LogItem>();

    static Log()
    {
      _logTimer.Interval = 100;
      _logTimer.AutoReset = false;
      _logTimer.Elapsed += (o,e) => DoWrite();
    }

    private static IEnumerable<string> GetFilename(string dir, string name, string ext)
    {
      for (int i = 0; ; i++)
        yield return Path.Combine(dir, i > 0 ? "{0}_{1}{2}".F(name, i, ext) : "{0}{1}".F(name, ext));
    }

    /// <summary>Creates a log channel associated with a log file. If the channel already exists, update its definition</summary>
    /// <param name="channel">The name used to identify the logging channel</param>
    /// <param name="path">The path of the log file</param>
    public static void Define(string channel, string path) { CreateOrOverwriteInner(channel, path); }

    /// <summary>Gets or creates a logging handle</summary>
    /// <param name="channel">The name used to identify the logging channel</param>
    /// <returns></returns>
    private static TextWriter GetOrCreateInner(string channel) { return GetOrCreateInner(channel, "{0}.{1}".F(channel, LOG_EXT)); }

    /// <summary>Gets or creates a logging handle</summary>
    /// <param name="channel">The name used to identify the logging channel</param>
    /// <param name="path">The path of the log file</param>
    /// <returns></returns>
    private static TextWriter GetOrCreateInner(string channel, string path)
    {
      if (Loggers.TryGetValue(channel, out TextWriter info))
        return info;

      return CreateInner(channel, path);
    }

    private static TextWriter CreateOrOverwriteInner(string channel, string path)
    {
      TextWriter info;
      if (Loggers.TryGetValue(channel, out TextWriter oldinfo))
      {
        Loggers.Remove(channel);
      }
      info = CreateInner(channel, path);
      oldinfo.Dispose();
      return info;
    }

    private static TextWriter CreateInner(string channel, string path)
    {
      path = Path.GetFullPath(path);
      string dir = Path.GetDirectoryName(path);
      string name = Path.GetFileNameWithoutExtension(path);
      string ext = Path.GetExtension(path);

      foreach (string filename in GetFilename(dir, name, ext))
      {
        try
        {
          Directory.CreateDirectory(dir);

          StreamWriter writer = File.CreateText(filename);
          writer.AutoFlush = true;
          TextWriter info = TextWriter.Synchronized(writer);
          Loggers.Add(channel, info);
          return info;
        }
        catch (IOException)
        {
          continue;
        } // if error, continue and attempt generating the next filename
      }
      return null;
    }

    private static TextWriter GetWriter(string channel)
    {
      TextWriter info;
      lock (Loggers)
        if (!Loggers.TryGetValue(channel, out info))
          info = GetOrCreateInner(channel);

      return info;
    }

    /// <summary>Writes a formatted message into a log channel</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="message">The log message</param>
    public static void Write(string channel, string message) { Enqueue(new LogItem(channel, DateTime.Now, message)); }

    /// <summary>Writes a formatted message into a log channel</summary>
    /// <typeparam name="T">The type of the message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="arg">The message argument</param>
    public static void Write<T>(string channel, string format, T arg) { Enqueue(new LogItem(channel, DateTime.Now, format.F(arg))); }

    /// <summary>Writes a formatted message into a log channel</summary>
    /// <typeparam name="T1">The type of the first message argument</typeparam>
    /// <typeparam name="T2">The type of the second message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="a1">The first message argument</param>
    /// <param name="a2">The second message argument</param>
    public static void Write<T1, T2>(string channel, string format, T1 a1, T2 a2) { Enqueue(new LogItem(channel, DateTime.Now, format.F(a1, a2))); }

    /// <summary>Writes a formatted message into a log channel</summary>
    /// <typeparam name="T1">The type of the first message argument</typeparam>
    /// <typeparam name="T2">The type of the second message argument</typeparam>
    /// <typeparam name="T3">The type of the third message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="a1">The first message argument</param>
    /// <param name="a2">The second message argument</param>
    /// <param name="a3">The third message argument</param>
    public static void Write<T1, T2, T3>(string channel, string format, T1 a1, T2 a2, T3 a3) { Enqueue(new LogItem(channel, DateTime.Now, format.F(a1, a2, a3))); }

    /// <summary>Writes a formatted message into a log channel</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="args">The parameterized message arguments</param>
    public static void Write(string channel, string format, params object[] args) { Enqueue(new LogItem(channel, DateTime.Now, format.F(args))); }

    private static void Enqueue(LogItem item)
    {
      Queue.Enqueue(item);
      if (!_logTimer.Enabled)
      {
        _logTimer.Start();
      }
    }

    private static void DoWrite()
    {
      _logTimer.Stop();
      while (Queue.TryDequeue(out LogItem item))
      {
        TextWriter tw = GetWriter(item.Channel);
        if (tw != null)
        {
          DoWrite(tw, item.Time, item.Message);
        }
      }

      if (Queue.Count > 0)
      {
        _logTimer.Start();
      }
    }

    private static void DoWrite(TextWriter tw, DateTime time, string message)
    {
      tw.Write(time.ToString("s"));
      tw.Write("\t");
      //tw.Write("{0,-30}".F(code));
      //tw.Write("\t");
      tw.Write(message);
      tw.WriteLine();
    }

    /// <summary>Writes an error message into the channel</summary>
    /// <param name="channel"></param>
    /// <param name="ex"></param>
    public static void WriteErr(string channel, Exception ex)
    {
      if (ex == null)
        return;

      TextWriter tw = GetWriter(channel);

      if (tw == null)
        return;

      tw.Write("Fatal Error occured at ");
      tw.WriteLine(DateTime.Now.ToString("s"));
      tw.WriteLine("----------------------------------------------------------------");
      tw.Write("Message: ");
      tw.WriteLine(ex.Message);
      tw.WriteLine();
      tw.WriteLine(ex.StackTrace);
      tw.WriteLine();
      tw.WriteLine();
    }
  }
}
