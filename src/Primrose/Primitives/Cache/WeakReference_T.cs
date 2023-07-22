using System;

namespace Primrose.Primitives.Cache
{
  // Created to mimic the class of the same name in .NET Framework 4.5

  /// <summary>
  /// Holds a typed object in a weak reference, allowing garbage collection
  /// </summary>
  /// <typeparam name="T">The type of the referenced object</typeparam>
  [Serializable]
  public class WeakReference<T> : WeakReference
  {
    /// <summary>Holds a typed object in a weak reference, allowing garbage collection</summary>
    /// <param name="target">The object to hold</param>
    public WeakReference(T target)
        : base(target)
    {
    }

    /// <summary>Holds a typed object in a weak reference, allowing garbage collection</summary>
    /// <param name="target">The object to hold</param>
    /// <param name="trackResurrection"></param>
    public WeakReference(T target, bool trackResurrection)
        : base(target, trackResurrection)
    {
    }

    /*
    public WeakReference(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
    */

    /// <summary>Gets the target object referenced by the current WeakReference&lt;T&gt; object.</summary>
    public new T Target
    {
      get
      {
        return (T)base.Target;
      }
    }

    /// <summary>Tries to retrieve the target object that is referenced by the current WeakReference&lt;T&gt; object.</summary>
    public bool TryGetTarget(out T target)
    {
      target = default;

      try
      {
        if (IsAlive)
        {
          target = Target;
          return true;
        }
        else
          return false;
      }
      catch { return false; }
    }
  }
}
