using Primrose.Primitives.Collections;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Primrose.Diagnostics
{
  /// <summary>Sets up runtime monitoring of functions</summary>
  public class PerformanceMonitor
  {
    /// <summary>Sets up runtime monitoring of functions</summary>
    public PerformanceMonitor()
    {
      pool = new ObjectPool<PerfElement>(() => new PerfElement(this, "<undefined>"), (p) => p.Reset());
      sbpool = new ObjectPool<StringBuilder>(() => new StringBuilder(128), (p) => p.Clear());
    }

    /// <summary>Determines the file path where monitoring report will be written to</summary>
    public string LogPath = @"perf.log";

    /// <summary>Retrieves the most recent report</summary>
    public string Report = string.Empty;

    /// <summary>Allows user applications to register their own values in properties</summary>
    public readonly Registry<string, string> Properties = new Registry<string, string>();

    /// <summary>Determines which user properties would be included in the report</summary>
    public readonly List<string> ShowProperties = new List<string>();

    private DateTime m_last_refresh_time = DateTime.Now;
    private CircularQueue<PerfToken> Queue;
    private Dictionary<string, PerfToken> Elements;
    private Dictionary<int, double> ThreadTimes;
    private readonly int _limit = 100000;

    private Dictionary<int, List<PerfElement>> pt_current = new Dictionary<int, List<PerfElement>>();
    private readonly ObjectPool<PerfElement> pool;
    private readonly ObjectPool<StringBuilder> sbpool;

    private bool _enabled;

    /// <summary>Determines whether monitoring functions are enabled</summary>
    public bool Enabled
    {
      get { return _enabled; }
      set
      {
        if (_enabled != value)
        {
          if (value) { Init(); }
          _enabled = value;
          if (!value) { Deinit(); }
        }
      }
    }

    private void Init()
    {
      m_last_refresh_time = DateTime.Now;
      Queue = new CircularQueue<PerfToken>(100000, false);
      Elements = new Dictionary<string, PerfToken>();
      ThreadTimes = new Dictionary<int, double>();
      pt_current = new Dictionary<int, List<PerfElement>>();
    }

    private void Deinit()
    {
      Elements.Clear();
      ThreadTimes.Clear();
      pt_current.Clear();
    }

    /// <summary>Creates a Performance countering element. The monitor will be notified on disposal</summary>
    public IDisposable Create(string name)
    {
      if (Enabled)
      {
        PerfElement e = pool.GetNew();
        int id = Thread.CurrentThread.ManagedThreadId;
        if (pt_current.TryGetValue(id, out List<PerfElement> list) && list.Count > 0)
          e.Name = list[list.Count - 1].Name + PerfElement.Delimiter + name;
        else
          e.Name = name;

        if (pt_current.ContainsKey(id))
          pt_current[id].Add(e);
        else
          pt_current.Add(id, new List<PerfElement> { e });
        return e;
      }
      else
        return null;
    }

    internal void UpdatePerfElement(PerfElement element, double value)
    {
      if (Queue.Count < _limit)
        Queue.Enqueue(new PerfToken { Name = element.Name, Seconds = value });

      int id = Thread.CurrentThread.ManagedThreadId;
      if (pt_current.ContainsKey(id))
        pt_current[id].Remove(element);

      pool.Return(element);
    }

    private void ProcessQueue()
    {
      PerfToken p;
      int limit = _limit;
      while (Queue.Count > 0 && limit >= 0) //Queue.TryDequeue(out p) || limit < 0)
      {
        p = Queue.Dequeue();
        if (p.Name == null)
          break;

        limit--;
        PerfToken pt = Elements.GetOrDefault(p.Name);
        if (pt.Name == null)
        {
          Elements.Put(p.Name, new PerfToken { Name = p.Name, Count = 1, Seconds = p.Seconds, Peak = p.Seconds });
        }
        else
        {
          double peak = (pt.Peak < p.Seconds) ? p.Seconds : pt.Peak;
          double seconds = pt.Seconds + p.Seconds;
          int count = ++pt.Count;

          Elements.Put(p.Name, new PerfToken { Name = p.Name, Count = count, Seconds = seconds, Peak = peak });
        }
      }
    }

    private void Refresh()
    {
      Elements.Clear();
      ProcessQueue();
    }

    /// <summary>Clears existing data and logged reports</summary>
    public void Clear()
    {
      if (File.Exists(LogPath))
        File.Delete(LogPath);

      if (Enabled)
        Refresh();
    }

    /// <summary>Writes the Report to the file indicated by LogPath</summary>
    public void Print()
    {
      File.AppendAllText(LogPath, Report);
    }

    /// <summary>Updates the Report</summary>
    public void UpdateReport()
    {
      if (!Enabled) { return; }

      try
      {
        double refresh_ms = (DateTime.Now - m_last_refresh_time).TotalMilliseconds;
        m_last_refresh_time = DateTime.Now;
        StringBuilder sb = sbpool.GetNew();
        sb.AppendLine();
        sb.AppendLine("{0,30} : [{1,4:0}ms] {2:s}".F("Sampling Time", refresh_ms, m_last_refresh_time));
        foreach (string key in ShowProperties)
        {
          sb.AppendLine("{0,30} : {1}".F(key, Properties[key]));
        }

        List<PerfToken> newElements = new List<PerfToken>(Elements.Values);

        // ---------- PROCESS THREAD
        foreach (ProcessThread pt in Process.GetCurrentProcess().Threads)
        {
          if (pt.ThreadState != System.Diagnostics.ThreadState.Terminated && pt.ThreadState != System.Diagnostics.ThreadState.Wait)
          {
            double d = pt.TotalProcessorTime.TotalMilliseconds - ThreadTimes.GetOrDefault(pt.Id);
            sb.AppendLine("Thread {0:00000} {1,17} : {2:0.00}%".F(pt.Id, pt.ThreadState, d / refresh_ms * 100));
            ThreadTimes.Put(pt.Id, pt.TotalProcessorTime.TotalMilliseconds);
          }
        }

        sb.AppendLine("                                 [ Count] Total|s   Avg|ms  Peak|ms");
        newElements.Sort(new PerfComparer());
        foreach (PerfToken e in newElements)
        {
          string[] div = e.Name.Split(PerfElement.Delimiter);
          string name = (div.Length > 0) ? new string('-', div.Length - 1) + div[div.Length - 1] : div[div.Length - 1];
          name = (name.Length > 30) ? name.Remove(27) + "..." : name;
          sb.AppendLine("{0,-30} : [{1,6}] {2,7:0.000}  {3,7:0.00}  {4,7:0.00}".F(
                          name
                        , e.Count
                        , e.Seconds
                        , e.Seconds / e.Count * 1000
                        , e.Peak * 1000));
        }

        Report = sb.ToString();
        sbpool.Return(sb);
        Refresh();
      }
      catch
      {
      }
    }
  }

  internal class PerfElement : IDisposable
  {
    internal static char[] Delimiter = new char[] { '.' };

    public string Name;
    private readonly PerformanceMonitor _handler;
    private long _timestamp;

    public PerfElement(PerformanceMonitor handler, string name)
    {
      Name = name;
      _handler = handler;
      _timestamp = Stopwatch.GetTimestamp();
    }

    public void Reset()
    {
      Name = "<undefined>";
      _timestamp = Stopwatch.GetTimestamp();
    }

    public void Dispose()
    {
      if (_handler.Enabled)
        _handler.UpdatePerfElement(this, 0.0d.Max(Stopwatch.GetTimestamp() - _timestamp) / Stopwatch.Frequency);
    }
  }

  internal class PerfComparer : IComparer<PerfToken>
  {
    int IComparer<PerfToken>.Compare(PerfToken x, PerfToken y)
    {
      return x.Name.CompareTo(y.Name);
    }
  }

  internal struct PerfToken
  {
    public string Name;
    public int Count;
    public double Seconds;
    public double Peak;
  }
}
