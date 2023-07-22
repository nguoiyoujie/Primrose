using Primrose.Primitives.Extensions;
using System;
using System.Diagnostics;
using System.Threading;

namespace Primrose.Primitives
{
  /// <summary>
  /// Represents a time control 
  /// </summary>
  public class TimeControl
  {
    /// <summary>Sets a time precision mode</summary>
    public enum TimePrecision 
    {
      /// <summary>Disables any time control. Wait() will do nothing. Intended for when you have other methods of time control and only want to measure the time</summary>
      NONE = 0,

      /// <summary>Sets a time control with low precision</summary>
      SIMPLE = 1,

      /// <summary>Sets a time control to high precision by spinning the CPU instead of yielding it</summary>
      HIGH_PRECISION = 2,

      /// <summary>Sets a time control to adjust itself between simple and high precision modes</summary>
      DYNAMIC = 3,
    }

    private uint _targetFPS;
    private int _FPScounter;
    private int _spincounter;
    private float _addTime;
    private float _countTimeTotal;
    private float _countTimeProcess;
    private float _countTimeMark;
    private float _countTimeWait;
    private float _lastTimeProcess;
    private float _lastTimeMark = -1;
    private float _lastTimeWait;
    private TimePrecision _precision = TimePrecision.NONE;
    private Func<Stopwatch, uint, int> _waitFunc = _wait_none;
    private readonly Stopwatch stopwatch = Stopwatch.StartNew();

    /// <summary>
    /// Initializes a time control
    /// </summary>
    public TimeControl() : this(60u) { }

    /// <summary>
    /// Initializes a time control
    /// </summary>
    public TimeControl(uint targetFPS) : this(targetFPS, TimePrecision.SIMPLE) { }

    /// <summary>
    /// Initializes a time control
    /// </summary>
    public TimeControl(uint targetFPS, TimePrecision precision) { TargetFPS = targetFPS; Precision = precision; }

    /// <summary>Defines the minimum desirable FPS for throttling purposes</summary>
    public uint MinimumFPS { get; set; } = 15;

    /// <summary>Defines the maximum desirable FPS for throttling purposes</summary>
    public uint MaximumFPS { get; set; } = 90;

    /// <summary>Defines the FPS where performance savings should be triggered</summary>
    public uint PerformanceSavingFPS { get; set; } = 25;

    /// <summary>The time period in which FPS is updated</summary>
    public float FPSRefreshInterval = 0.2f;

    /// <summary>The current FPS</summary>
    public float FPS { get; private set; }

    /// <summary>Shows the number of spinning cycles made while during the last Wait() call. Can be used to check performance</summary>
    public int SpinsInLastFrame { get; private set; }

    /// <summary>The current number of spin cycles per second</summary>
    public float SpinsPerFrame { get; private set; }

    /// <summary>The fraction of time spent in Wait()</summary>
    public float PercentWait { get; private set; }

    /// <summary>The fraction of time spent between Mark() and Unmark()</summary>
    public float PercentMarked { get; private set; }

    /// <summary>The fraction of time spent outside of Wait()</summary>
    public float PercentProcess { get; private set; }

    /// <summary>The precision mode of the timer</summary>
    public TimePrecision Precision
    {
      get
      {
        return _precision;
      }
      set
      {
        if (_precision != value)
        {
          _precision = value;
          switch (_precision)
          {
            case TimePrecision.NONE:
            default:
              _waitFunc = _wait_none;
              break;

            case TimePrecision.SIMPLE:
              _waitFunc = _wait_simple;
              break;

            case TimePrecision.HIGH_PRECISION:
              _waitFunc = _wait_hiprecision;
              break;

            case TimePrecision.DYNAMIC:
              _waitFunc = _wait_dynamic;
              break;
          }
        }
      }
    }
    
    /// <summary>The target FPS</summary>
    public uint TargetFPS
    {
      get
      {
        return _targetFPS;
      }
      set
      {
        _targetFPS = value.Max(MinimumFPS);
      }
    }

    /// <summary>A multiplier to world time</summary>
    public float SpeedModifier = 1;

    /// <summary>The world time</summary>
    public float WorldTime { get; private set; } = 0;

    /// <summary>A interval between two successive updates</summary>
    public float UpdateInterval { get; private set; }
    
    /// <summary>The interval in world time</summary>
    public float WorldInterval { get { return UpdateInterval * SpeedModifier + _addTime; } }

    /// <summary>Updates the time</summary>
    public void Update()
    {
      float interval = (float)stopwatch.Elapsed.TotalSeconds;
      if (_lastTimeProcess > 0)
      {
        _lastTimeWait = interval - _lastTimeProcess;
      }
      else
      {
        _lastTimeProcess = interval;
      }

      stopwatch.Restart();

      _FPScounter++;
      _countTimeTotal += interval;
      _countTimeProcess += _lastTimeProcess;
      _countTimeWait += _lastTimeWait;
      _lastTimeProcess = 0;
      _lastTimeWait = 0;

      if (_countTimeTotal > FPSRefreshInterval)
      {
        FPS = _FPScounter / _countTimeTotal;
        SpinsPerFrame = _spincounter / _FPScounter;
        PercentProcess = _countTimeProcess / _countTimeTotal * 100;
        PercentWait = _countTimeWait / _countTimeTotal * 100;
        PercentMarked = _countTimeMark / _countTimeTotal * 100;

        _FPScounter = 0;
        _countTimeTotal = 0;
        _countTimeProcess = 0;
        _countTimeWait = 0;
        _countTimeMark = 0;
        _spincounter = 0;
      }
      UpdateInterval = interval.Clamp(1f / FPS.Max(TargetFPS), 1f / MinimumFPS);
      WorldTime += WorldInterval;
      _addTime = 0;
    }

    /// <summary>Performs a time skip to increment the time</summary>
    /// <param name="worldtime"></param>
    public void AddTime(float worldtime)
    {
      _addTime += worldtime;
    }

    /// <summary>Performs a wait to suspend process until the target FPS is reached</summary>
    public void Wait()
    {
      _lastTimeProcess = (float)stopwatch.Elapsed.TotalSeconds;
      _lastTimeWait = 0;
      SpinsInLastFrame = _waitFunc.Invoke(stopwatch, TargetFPS);
      _spincounter += SpinsInLastFrame;
    }

    /// <summary>Begins measurement of two points of time</summary>
    public void Mark()
    {
      _lastTimeMark = (float)stopwatch.Elapsed.TotalSeconds;
    }

    /// <summary>Ends measurement of two points of time</summary>
    public float Unmark()
    {
      if (_lastTimeMark != -1)
      {
        float m = (float)stopwatch.Elapsed.TotalSeconds - _lastTimeMark;
        _countTimeMark += m;
        _lastTimeMark = -1;
        return m;
      }
      else
      {
        // Mark() was not called
        return 0;
      }
    }

    private static Func<Stopwatch, uint, int> _wait_none = new Func<Stopwatch, uint, int>((s, targetFPS) =>
    {
      return 0;
    });

    private static Func<Stopwatch, uint, int> _wait_simple = new Func<Stopwatch, uint, int>((s, targetFPS) =>
    {
      long target_ticks = 1000 / targetFPS;
      long passed_ticks = s.ElapsedMilliseconds;
      long diff = target_ticks - passed_ticks;

      if (diff >= 0)
      {
        Thread.Sleep((int)diff);
      }
      return 1;
    });

    private static Func<Stopwatch, uint, int> _wait_hiprecision = new Func<Stopwatch, uint, int>((s, targetFPS) =>
    {
      long target_ticks = 1000 * TimeSpan.TicksPerMillisecond / targetFPS;
      long passed_ticks = s.ElapsedTicks;
      long diff = target_ticks - passed_ticks;

      int calls = 0;
      while (diff > 0)
      {
        Thread.SpinWait((int)diff);
        diff = target_ticks - s.ElapsedTicks;
        calls++;
      }
      return calls;
    });

    private static Func<Stopwatch, uint, int> _wait_dynamic = new Func<Stopwatch, uint, int>((s, targetFPS) =>
    {
      long target_ticks = 1000 * TimeSpan.TicksPerMillisecond / targetFPS;
      long passed_ticks = s.ElapsedTicks;
      long diff = target_ticks - passed_ticks;

      int calls = 0;
      int diff_s = (int)(diff / TimeSpan.TicksPerMillisecond).Clamp(0,1);
      while (diff > 0)
      {
        if (diff_s > 0)
        {
          Thread.Sleep(diff_s);
        }
        else
        {
          Thread.SpinWait((int)diff);
        }
        diff = target_ticks - s.ElapsedTicks;
        calls++;
      }
      return calls;
    });
  }
}
