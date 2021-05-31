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
    private float _FPScountTime;
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

      stopwatch.Restart();
      if (_FPScountTime > FPSRefreshInterval)
      {
        FPS = _FPScounter / _FPScountTime;
        SpinsPerFrame = _spincounter / _FPScounter;

        _FPScounter = 0;
        _FPScountTime = 0;
        _spincounter = 0;
      }

      _FPScounter++;
      _FPScountTime += interval;

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
      SpinsInLastFrame = _waitFunc.Invoke(stopwatch, TargetFPS);
      _spincounter += SpinsInLastFrame;
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
