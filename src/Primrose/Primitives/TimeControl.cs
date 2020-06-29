using Primrose.Primitives.Extensions;
using System.Diagnostics;
using System.Threading;

namespace Primrose.Primitives
{
  /// <summary>
  /// Represents a time control 
  /// </summary>
  public class TimeControl
  {
    private uint _targetFPS;
    private int _FPScounter;
    private float _addTime;
    private float _FPScountTime;
    private readonly object waitlock = new object();
    private readonly Stopwatch stopwatch = Stopwatch.StartNew();

    /// <summary>
    /// Initializes a time control
    /// </summary>
    public TimeControl() { TargetFPS = 60; }

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

        _FPScounter = 0;
        _FPScountTime = 0;
      }

      _FPScounter++;
      _FPScountTime += interval;

      UpdateInterval = interval.Clamp(1f / MaximumFPS, 1f / MinimumFPS);
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
      long target = 1000 / TargetFPS;
      long passed = stopwatch.ElapsedMilliseconds;
      int wait = (int)(target - passed);
      if (wait > 1)
        Thread.Sleep(wait);
    }
  }
}
