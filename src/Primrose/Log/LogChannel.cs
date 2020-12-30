using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Timers;

namespace Primrose
{
  internal class LogChannel
  {
    public TextWriter Writer { get; private set; }
    public string TimeFormat = "s";
    public LogLevel Levels = LogLevel.ALL;
    public readonly Registry<Action<string>, LogLevel> CallbackList = new Registry<Action<string>, LogLevel>();
    private readonly Timer _logTimer = new Timer();
    private readonly ConcurrentQueue<LogItem> _queue = new ConcurrentQueue<LogItem>();

    public LogChannel(TextWriter writer, LogLevel levels = LogLevel.ALL)
    {
      Writer = writer;
      Levels = levels;
      _logTimer.Interval = 100;
      _logTimer.AutoReset = false;
      _logTimer.Elapsed += (o, e) => DoWrite();
    }

    public LogChannel(string logpath, LogLevel levels = LogLevel.ALL)
    {
      SetWriter(logpath);
      Levels = levels;
      _logTimer.Interval = 100;
      _logTimer.AutoReset = false;
      _logTimer.Elapsed += (o, e) => DoWrite();
    }

    public void Close()
    {
      CallbackList.Clear();
      SetWriter(TextWriter.Null);
    }

    public void SetWriter(TextWriter writer)
    {
      TextWriter old_writer = Writer;
      Writer = writer;
      old_writer?.Dispose();
    }

    public void SetWriter(string path)
    {
      string fpath = Path.GetFullPath(path);
      string dir = Path.GetDirectoryName(fpath);
      string name = Path.GetFileNameWithoutExtension(fpath);
      string ext = Path.GetExtension(fpath);

      Directory.CreateDirectory(dir);
      StreamWriter sw = null;
      foreach (string filename in GetFilename(dir, name, ext))
      {
        try
        {
          sw = File.CreateText(filename);
        }
        catch (IOException)
        {
          continue;
        } // if error, continue and attempt generating the next filename
        break;
      }

      if (sw != null)
      {
        sw.AutoFlush = true;
        SetWriter(TextWriter.Synchronized(sw));
      }
    }

    private static IEnumerable<string> GetFilename(string dir, string name, string ext)
    {
      for (int i = 0; ; i++)
        yield return Path.Combine(dir, i > 0 ? "{0}_{1}{2}".F(name, i, ext) : "{0}{1}".F(name, ext));
    }

    public void Debug(string message) { CheckAndEnqueue(LogLevel.DEBUG, message); }

    public void Info(string message) { CheckAndEnqueue(LogLevel.INFO, message); }

    public void Trace(string message) { CheckAndEnqueue(LogLevel.TRACE, message); }

    public void Warn(string message) { CheckAndEnqueue(LogLevel.WARN, message); }

    public void Error(string message) { CheckAndEnqueue(LogLevel.ERROR, message); }

    public void Error(Exception ex) { CheckAndEnqueue(LogLevel.ERROR, ex); }

    public void Fatal(string message) { CheckAndEnqueue(LogLevel.FATAL, message); }

    public void Fatal(Exception ex) { CheckAndEnqueue(LogLevel.FATAL, ex); }

    public void DoCallback(LogItem item)
    {
      string stime = item.Time.ToString(TimeFormat);
      string slevel = "{0,-8}".F(item.Level);
      string message = "";

      if (item.Message != null)
      {
        message = stime + "\t" + slevel + "\t" + item.Message;
      }

      if (item.Exception != null)
      {
        string sblank = new string(' ', stime.Length) + "\t" + new string(' ', slevel.Length) + "\t";

        message = stime + "\t" + slevel + "\t" + "An exception has been encountered.";
        message += "\n" + sblank + "Message: " + item.Exception.Message;
        message += "\n" + sblank + "StackTrace: ";

        sblank += "           ";
        StackTrace st = new StackTrace(item.Exception, true);
        for (int i = 0; i < st.FrameCount; i++)
        {
          StackFrame sf = st.GetFrame(i);
          message += "\n" + sblank + "{0} ({1}, line {2})".F(sf.GetMethod(), Path.GetFileName(sf.GetFileName()), sf.GetFileLineNumber());
        }
      }
      
      foreach (Action<string> action in CallbackList.EnumerateKeys())
      {
        if ((CallbackList[action] | item.Level) != 0)
        {
          action.Invoke(message);
        }
      }
    }

    private void CheckAndEnqueue(LogLevel level, string message)
    {
      if ((Levels & level) != 0)
      {
        LogItem item = new LogItem(level, message, null);
        Enqueue(item);
        DoCallback(item);
      }
    }

    private void CheckAndEnqueue(LogLevel level, Exception ex)
    {
      if ((Levels & level) != 0)
      {
        LogItem item = new LogItem(level, null, ex);
        Enqueue(item);
        DoCallback(item);
      }
    }

    private void Enqueue(LogItem item)
    {
      _queue.Enqueue(item);
      if (!_logTimer.Enabled)
      {
        _logTimer.Start();
      }
    }

    private void DoWrite()
    {
      _logTimer.Stop();
      while (_queue.TryDequeue(out LogItem item))
      {
        DoWrite(item);
      }

      if (_queue.Count > 0)
      {
        _logTimer.Start();
      }
    }

    private void DoWrite(LogItem item)
    {
      if (Writer != null)
      {
        string stime = item.Time.ToString(TimeFormat);
        string slevel = "{0,-8}".F(item.Level);


        Writer.Write(stime);
        Writer.Write("\t");
        Writer.Write(slevel);
        Writer.Write("\t");

        if (item.Message != null)
        {
          Writer.Write(item.Message);
          Writer.WriteLine();
        }

        if (item.Exception != null)
        {
          string sblank = new string(' ', stime.Length) + "\t" + new string(' ', slevel.Length) + "\t";

          Writer.Write("An exception has been encountered.");
          Writer.WriteLine();

          Writer.Write(sblank);
          Writer.Write("Message: ");
          Writer.WriteLine(item.Exception.Message);
          Writer.WriteLine();

          Writer.Write(sblank);
          Writer.WriteLine("StackTrace: ");
          sblank += "           ";
          StackTrace st = new StackTrace(item.Exception, true);
          for (int i = 0; i < st.FrameCount; i++)
          {
            StackFrame sf = st.GetFrame(i);

            Writer.Write(sblank);
            Writer.WriteLine("{0} ({1}, line {2})".F(sf.GetMethod(), Path.GetFileName(sf.GetFileName()), sf.GetFileLineNumber()));
          }
          Writer.WriteLine();
        }
      }
    }
  }
}
