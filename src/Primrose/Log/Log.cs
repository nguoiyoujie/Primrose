using Primrose.Primitives.Cache;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using System;
using System.IO;

namespace Primrose
{
  /// <summary>Handles logging behaviour for the program</summary>
  public static class Log
  {
    /// <summary>The default log extension appended to log files, unless explicitly defined</summary>
    public const string LOG_EXT = ".log";

    /// <summary>The log directory path specified for created log channels.</summary>
    public static string DirectoryPath = "./";

    private static LogChannel NullChannel = new LogChannel(TextWriter.Null, LogLevel.NONE);

    private static readonly Registry<string, LogChannel> Channels = new Registry<string, LogChannel>();

    /// <summary>Associates a log channel with a log file. If the channel does not yet exist, create it</summary>
    /// <param name="channel">The name used to identify the logging channel</param>
    /// <param name="path">The path of the log file</param>
    public static void Define(string channel, string path) 
    {
      LogChannel ch = GetOrCreateDefault(channel);
      ch.SetWriter(Path.Combine(DirectoryPath, path)); 
    }

    /// <summary>Associates a log channel with a TextWriter. If the channel does not yet exist, create it</summary>
    /// <param name="channel">The name used to identify the logging channel</param>
    /// <param name="writer">The TextWriter to associate with the log channel</param>
    public static void Define(string channel, TextWriter writer)
    {
      LogChannel ch = GetOrCreateDefault(channel);
      ch.SetWriter(writer);
    }

    /// <summary>Set the logging levels for a log channel. If the channel does not yet exist, create it</summary>
    /// <param name="channel">The name used to identify the logging channel</param>
    /// <param name="logLevel">The logging levels that will be printed into the log channel</param>
    public static void SetLogLevel(string channel, LogLevel logLevel)
    {
      LogChannel ch = GetOrCreateDefault(channel);
      ch.Levels = logLevel;
    }

    /// <summary>Adds the logging levels for a log channel. If the channel does not yet exist, create it</summary>
    /// <param name="channel">The name used to identify the logging channel</param>
    /// <param name="logLevel">The logging levels that will be printed into the log channel</param>
    public static void AddLogLevel(string channel, LogLevel logLevel)
    {
      LogChannel ch = GetOrCreateDefault(channel);
      ch.Levels |= logLevel;
    }

    /// <summary>Removes the logging levels for a log channel. If the channel does not yet exist, create it</summary>
    /// <param name="channel">The name used to identify the logging channel</param>
    /// <param name="logLevel">The logging levels that will be printed into the log channel</param>
    public static void RemoveLogLevel(string channel, LogLevel logLevel)
    {
      LogChannel ch = GetOrCreateDefault(channel);
      ch.Levels &= ~logLevel;
    }


    #region Writer Creation and Access Internals

    /// <summary>Gets or creates a logging handle</summary>
    /// <param name="channel">The name used to identify the logging channel</param>
    /// <returns></returns>
    private static LogChannel GetOrCreateDefault(string channel) 
    {
      if (channel == null)
        return NullChannel;

      LogChannel ch = Channels[channel];
      if (ch == null)
      {
        string chname = channel;
        foreach (char c in Path.GetInvalidFileNameChars())
        {
          string s = ToStringCache<char>.Get(c);
          if (chname.Contains(s))
            chname = chname.Replace(s, "");
        }

        ch = new LogChannel(Path.Combine(DirectoryPath, chname + LOG_EXT));
        Channels.Put(channel, ch);
      }

      return ch;   
    }

    /// <summary>Closes a logging handle</summary>
    /// <param name="channel">The name used to identify the logging channel</param>
    /// <returns></returns>
    public static void Close(string channel)
    {
      LogChannel ch = Channels[channel];
      if (ch != null)
      {
        ch.Close();
      }
      Channels.Remove(channel);
    }


    #endregion

    #region Logging

    /// <summary>Writes a formatted message into a log channel at the DEBUG level</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="message">The log message</param>
    public static void Debug(string channel, string message) { GetOrCreateDefault(channel).Debug(message); }

    /// <summary>Writes a formatted message into a log channel at the DEBUG level</summary>
    /// <typeparam name="T">The type of the message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="arg">The message argument</param>
    public static void Debug<T>(string channel, string format, T arg) { GetOrCreateDefault(channel).Debug(format.F(arg)); }

    /// <summary>Writes a formatted message into a log channel at the DEBUG level</summary>
    /// <typeparam name="T1">The type of the first message argument</typeparam>
    /// <typeparam name="T2">The type of the second message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="a1">The first message argument</param>
    /// <param name="a2">The second message argument</param>
    public static void Debug<T1, T2>(string channel, string format, T1 a1, T2 a2) { GetOrCreateDefault(channel).Debug(format.F(a1, a2)); }

    /// <summary>Writes a formatted message into a log channel at the DEBUG level</summary>
    /// <typeparam name="T1">The type of the first message argument</typeparam>
    /// <typeparam name="T2">The type of the second message argument</typeparam>
    /// <typeparam name="T3">The type of the third message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="a1">The first message argument</param>
    /// <param name="a2">The second message argument</param>
    /// <param name="a3">The third message argument</param>
    public static void Debug<T1, T2, T3>(string channel, string format, T1 a1, T2 a2, T3 a3) { GetOrCreateDefault(channel).Debug(format.F(a1, a2, a3)); }

    /// <summary>Writes a formatted message into a log channel at the DEBUG level</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="args">The parameterized message arguments</param>
    public static void Debug(string channel, string format, params object[] args) { GetOrCreateDefault(channel).Debug(format.F(args)); }

    /// <summary>Writes a formatted message into a log channel at the INFO level</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="message">The log message</param>
    public static void Info(string channel, string message) { GetOrCreateDefault(channel).Info(message); }

    /// <summary>Writes a formatted message into a log channel at the INFO level</summary>
    /// <typeparam name="T">The type of the message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="arg">The message argument</param>
    public static void Info<T>(string channel, string format, T arg) { GetOrCreateDefault(channel).Info(format.F(arg)); }

    /// <summary>Writes a formatted message into a log channel at the INFO level</summary>
    /// <typeparam name="T1">The type of the first message argument</typeparam>
    /// <typeparam name="T2">The type of the second message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="a1">The first message argument</param>
    /// <param name="a2">The second message argument</param>
    public static void Info<T1, T2>(string channel, string format, T1 a1, T2 a2) { GetOrCreateDefault(channel).Info(format.F(a1, a2)); }

    /// <summary>Writes a formatted message into a log channel at the INFO level</summary>
    /// <typeparam name="T1">The type of the first message argument</typeparam>
    /// <typeparam name="T2">The type of the second message argument</typeparam>
    /// <typeparam name="T3">The type of the third message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="a1">The first message argument</param>
    /// <param name="a2">The second message argument</param>
    /// <param name="a3">The third message argument</param>
    public static void Info<T1, T2, T3>(string channel, string format, T1 a1, T2 a2, T3 a3) { GetOrCreateDefault(channel).Info(format.F(a1, a2, a3)); }

    /// <summary>Writes a formatted message into a log channel at the INFO level</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="args">The parameterized message arguments</param>
    public static void Info(string channel, string format, params object[] args) { GetOrCreateDefault(channel).Info(format.F(args)); }

    /// <summary>Writes a formatted message into a log channel at the TRACE level</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="message">The log message</param>
    public static void Trace(string channel, string message) { GetOrCreateDefault(channel).Trace(message); }

    /// <summary>Writes a formatted message into a log channel at the TRACE level</summary>
    /// <typeparam name="T">The type of the message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="arg">The message argument</param>
    public static void Trace<T>(string channel, string format, T arg) { GetOrCreateDefault(channel).Trace(format.F(arg)); }

    /// <summary>Writes a formatted message into a log channel at the TRACE level</summary>
    /// <typeparam name="T1">The type of the first message argument</typeparam>
    /// <typeparam name="T2">The type of the second message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="a1">The first message argument</param>
    /// <param name="a2">The second message argument</param>
    public static void Trace<T1, T2>(string channel, string format, T1 a1, T2 a2) { GetOrCreateDefault(channel).Trace(format.F(a1, a2)); }

    /// <summary>Writes a formatted message into a log channel at the TRACE level</summary>
    /// <typeparam name="T1">The type of the first message argument</typeparam>
    /// <typeparam name="T2">The type of the second message argument</typeparam>
    /// <typeparam name="T3">The type of the third message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="a1">The first message argument</param>
    /// <param name="a2">The second message argument</param>
    /// <param name="a3">The third message argument</param>
    public static void Trace<T1, T2, T3>(string channel, string format, T1 a1, T2 a2, T3 a3) { GetOrCreateDefault(channel).Trace(format.F(a1, a2, a3)); }

    /// <summary>Writes a formatted message into a log channel at the TRACE level</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="args">The parameterized message arguments</param>
    public static void Trace(string channel, string format, params object[] args) { GetOrCreateDefault(channel).Trace(format.F(args)); }

    /// <summary>Writes a formatted message into a log channel at the WARNING level</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="message">The log message</param>
    public static void Warn(string channel, string message) { GetOrCreateDefault(channel).Warn(message); }

    /// <summary>Writes a formatted message into a log channel at the WARNING level</summary>
    /// <typeparam name="T">The type of the message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="arg">The message argument</param>
    public static void Warn<T>(string channel, string format, T arg) { GetOrCreateDefault(channel).Warn(format.F(arg)); }

    /// <summary>Writes a formatted message into a log channel at the WARNING level</summary>
    /// <typeparam name="T1">The type of the first message argument</typeparam>
    /// <typeparam name="T2">The type of the second message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="a1">The first message argument</param>
    /// <param name="a2">The second message argument</param>
    public static void Warn<T1, T2>(string channel, string format, T1 a1, T2 a2) { GetOrCreateDefault(channel).Warn(format.F(a1, a2)); }

    /// <summary>Writes a formatted message into a log channel at the WARNING level</summary>
    /// <typeparam name="T1">The type of the first message argument</typeparam>
    /// <typeparam name="T2">The type of the second message argument</typeparam>
    /// <typeparam name="T3">The type of the third message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="a1">The first message argument</param>
    /// <param name="a2">The second message argument</param>
    /// <param name="a3">The third message argument</param>
    public static void Warn<T1, T2, T3>(string channel, string format, T1 a1, T2 a2, T3 a3) { GetOrCreateDefault(channel).Warn(format.F(a1, a2, a3)); }

    /// <summary>Writes a formatted message into a log channel at the WARNING level</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="args">The parameterized message arguments</param>
    public static void Warn(string channel, string format, params object[] args) { GetOrCreateDefault(channel).Warn(format.F(args)); }

    /// <summary>Writes a formatted message into a log channel at the ERROR level</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="message">The log message</param>
    public static void Error(string channel, string message) { GetOrCreateDefault(channel).Error(message); }

    /// <summary>Writes a formatted message into a log channel at the ERROR level</summary>
    /// <typeparam name="T">The type of the message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="arg">The message argument</param>
    public static void Error<T>(string channel, string format, T arg) { GetOrCreateDefault(channel).Error(format.F(arg)); }

    /// <summary>Writes a formatted message into a log channel at the ERROR level</summary>
    /// <typeparam name="T1">The type of the first message argument</typeparam>
    /// <typeparam name="T2">The type of the second message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="a1">The first message argument</param>
    /// <param name="a2">The second message argument</param>
    public static void Error<T1, T2>(string channel, string format, T1 a1, T2 a2) { GetOrCreateDefault(channel).Error(format.F(a1, a2)); }

    /// <summary>Writes a formatted message into a log channel at the ERROR level</summary>
    /// <typeparam name="T1">The type of the first message argument</typeparam>
    /// <typeparam name="T2">The type of the second message argument</typeparam>
    /// <typeparam name="T3">The type of the third message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="a1">The first message argument</param>
    /// <param name="a2">The second message argument</param>
    /// <param name="a3">The third message argument</param>
    public static void Error<T1, T2, T3>(string channel, string format, T1 a1, T2 a2, T3 a3) { GetOrCreateDefault(channel).Error(format.F(a1, a2, a3)); }

    /// <summary>Writes a formatted message into a log channel at the ERROR level</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="args">The parameterized message arguments</param>
    public static void Error(string channel, string format, params object[] args) { GetOrCreateDefault(channel).Error(format.F(args)); }

    /// <summary>Writes a formatted message into a log channel at the ERROR level</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="ex">The exception to log</param>
    public static void Error(string channel, Exception ex) { GetOrCreateDefault(channel).Error(ex); }

    /// <summary>Writes a formatted message into a log channel at the FATAL level</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="message">The log message</param>
    public static void Fatal(string channel, string message) { GetOrCreateDefault(channel).Fatal(message); }

    /// <summary>Writes a formatted message into a log channel at the FATAL level</summary>
    /// <typeparam name="T">The type of the message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="arg">The message argument</param>
    public static void Fatal<T>(string channel, string format, T arg) { GetOrCreateDefault(channel).Fatal(format.F(arg)); }

    /// <summary>Writes a formatted message into a log channel at the FATAL level</summary>
    /// <typeparam name="T1">The type of the first message argument</typeparam>
    /// <typeparam name="T2">The type of the second message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="a1">The first message argument</param>
    /// <param name="a2">The second message argument</param>
    public static void Fatal<T1, T2>(string channel, string format, T1 a1, T2 a2) { GetOrCreateDefault(channel).Fatal(format.F(a1, a2)); }

    /// <summary>Writes a formatted message into a log channel at the FATAL level</summary>
    /// <typeparam name="T1">The type of the first message argument</typeparam>
    /// <typeparam name="T2">The type of the second message argument</typeparam>
    /// <typeparam name="T3">The type of the third message argument</typeparam>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="a1">The first message argument</param>
    /// <param name="a2">The second message argument</param>
    /// <param name="a3">The third message argument</param>
    public static void Fatal<T1, T2, T3>(string channel, string format, T1 a1, T2 a2, T3 a3) { GetOrCreateDefault(channel).Fatal(format.F(a1, a2, a3)); }

    /// <summary>Writes a formatted message into a log channel at the FATAL level</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="format">The log message format</param>
    /// <param name="args">The parameterized message arguments</param>
    public static void Fatal(string channel, string format, params object[] args) { GetOrCreateDefault(channel).Fatal(format.F(args)); }

    /// <summary>Writes a formatted message into a log channel at the FATAL level</summary>
    /// <param name="channel">The log channel to write to</param>
    /// <param name="ex">The exception to log</param>
    public static void Fatal(string channel, Exception ex) { GetOrCreateDefault(channel).Fatal(ex); }

    /// <summary>Registers a callback function to a log channel</summary>
    /// <param name="channel">The log channel to handle the callback</param>
    /// <param name="callback">The callback function to register</param>
    /// <param name="level">The logging levels were the callback will be triggered</param>
    public static void RegisterCallback(string channel, MessageDelegate callback, LogLevel level = LogLevel.ALL) { GetOrCreateDefault(channel).CallbackList.Put(callback, level); }

    /// <summary>Removes a callback function from a log channel registry list</summary>
    /// <param name="channel">The log channel to handle the callback</param>
    /// <param name="callback">The callback function to register</param>
    public static void RemoveCallback(string channel, MessageDelegate callback) { GetOrCreateDefault(channel).CallbackList.Remove(callback); }

    /// <summary>Removes all callback functions from a log channel registry list</summary>
    /// <param name="channel">The log channel to handle the callback</param>
    public static void RemoveCallbacks(string channel) { GetOrCreateDefault(channel).CallbackList.Clear(); }

    /// <summary>Sets the time format for a log channel</summary>
    /// <param name="channel">The log channel to handle the callback</param>
    /// <param name="format">The new time format to use</param>
    public static void SetTimeFormat(string channel, string format) { GetOrCreateDefault(channel).TimeFormat = format; }

    #endregion
  }
}
