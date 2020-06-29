using System;
using System.Collections.Generic;

namespace Primrose.Primitives.Triggers
{
  /// <summary>Represents a trigger for performing actions when a condition is fulfilled</summary>
  public class Trigger
  {
    /// <summary>The collection of actions to be performed by the trigger</summary>
    public ActionCollection Actions;
    private ICondition _cond = null;
    private readonly List<Action> _actions = new List<Action>();

    /// <summary>Creates a trigger for performing actions when a condition is fulfilled</summary>
    public Trigger(ICondition condition, params Action[] actions)
    {
      _cond = condition;
      condition.Update += EventCheck;
      _actions.AddRange(actions);
    }

    /// <summary>The condition checked by the trigger</summary>
    public ICondition Condition
    {
      get { return _cond; }
      set
      {
        if (_cond != null)
          _cond.Update -= EventCheck;

        _cond = value;
        value.Update += EventCheck;
      }
    }

    /// <summary>Represents a collection of actions to be performed by the trigger</summary>
    public struct ActionCollection
    {
      private Trigger _p;
      internal void Init(Trigger t) { _p = t; }

      /// <summary>Adds an action to the trigger</summary>
      public void Add(Action a) { _p._actions.Add(a); }

      /// <summary>Adds an action to the trigger</summary>
      public void AddRange(Action[] array) { _p._actions.AddRange(array); }

      /// <summary>Removes an action from the trigger</summary>
      public void Remove(Action a) { _p._actions.Remove(a); }

      /// <summary>Clears all actions from the trigger</summary>
      public void Clear() { _p._actions.Clear(); }
    }

    private void EventCheck(ConditionUpdateEventArgs args)
    {
      if (args.Fulfilled)
        Execute();
    }

    private void Execute()
    {
      foreach (Action a in _actions)
        a.Invoke();
    }
  }
}
