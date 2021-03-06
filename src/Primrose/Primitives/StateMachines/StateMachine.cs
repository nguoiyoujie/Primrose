﻿using System;
using System.Collections.Generic;

namespace Primrose.Primitives.StateMachines
{
  /// <summary>
  /// A programmable finite state machine, using commands to perform state changes and execute transition actions. This state machine itself does not store the state.
  /// </summary>
  /// <typeparam name="O">A parameterized object type that is passed into transition actions</typeparam>
  /// <typeparam name="T">The state type</typeparam>
  /// <typeparam name="U">The command type</typeparam>
  public class StateMachine<O, T, U> 
    where T : struct 
    where U : struct
  {
    private readonly Dictionary<T, InStateMachine> _transitions = new Dictionary<T, InStateMachine>();

    /// <summary>Initializes the state machine with an initial state</summary>
    /// <param name="owner">The owner object</param>
    /// <param name="state">The initial state</param>
    protected void Initialize(O owner, T state)
    {
      if (_transitions.TryGetValue(state, out InStateMachine _in))
        _in.Initialize(owner, state);
    }

    /// <summary></summary>
    /// <param name="state">The state</param>
    /// <returns>The finite state machine, now programmed with a state, awaiting further instructions</returns>
    protected InStateMachine In(T state)
    {
      if (!_transitions.TryGetValue(state, out InStateMachine ret))
      {
        ret = new InStateMachine(this);
        _transitions.Add(state, ret);
      }
      return ret;
    }

    /// <summary>
    /// Defines a programmable finite state machine, now programmed with a state.
    /// </summary>
    protected class InStateMachine
    {
      private readonly StateMachine<O, T, U> SM;
      private Action<O, T> Entry;
      private Action<O, T> Exit;
      private readonly Dictionary<U, OutStateMachine> _transitions = new Dictionary<U, OutStateMachine>();

      internal InStateMachine(StateMachine<O, T, U> statemachine) { SM = statemachine; }

      /// <summary>Adds the state change command in the programming condition</summary>
      /// <param name="command">The new command acting on this state</param>
      /// <returns>The finite state machine, now programmed with the new state change command, awaiting further instructions</returns>
      public OutStateMachine On(U command)
      {
        if (!_transitions.TryGetValue(command, out OutStateMachine ret))
        {
          ret = new OutStateMachine(this);
          _transitions.Add(command, ret);
        }
        return ret;
      }

      /// <summary>Instructs the state machine to execute an action on entering this state, regardless of command</summary>
      /// <param name="action">The action to be executed</param>
      /// <returns>The same state machine, awaiting further instructions</returns>
      public InStateMachine ExecuteOnEntry(Action<O, T> action) { Entry = action; return this; }

      /// <summary>Instructs the state machine to execute an action on exiting this state, regardless of command</summary>
      /// <param name="action">The action to be executed</param>
      /// <returns>The same state machine, awaiting further instructions</returns>
      public InStateMachine ExecuteOnExit(Action<O, T> action) { Exit = action; return this; }

      internal void Initialize(O owner, T state)
      {
        Entry?.Invoke(owner, state);
      }

      internal void Fire(O owner, U command, ref T state)
      {
        if (_transitions.TryGetValue(command, out OutStateMachine _on))
        {
          Exit?.Invoke(owner, state);
          _on.Fire(owner, ref state); // state is changed here
          SM.Initialize(owner, state);
        }
        else
          throw new InvalidStateCommandException<T, U>(command, state);
      }

      internal bool IsValid(U command)
      {
        return _transitions.TryGetValue(command, out _);
      }
    }

    /// <summary>
    /// Defines a programmable finite state machine, now programmed with an initial state and a state change command.
    /// </summary>
    protected class OutStateMachine
    {
      private readonly InStateMachine IN;
      private T NewState;
      private Action<O, T> Transit;

      internal OutStateMachine(InStateMachine instate) { IN = instate; }

      /// <summary>Switches the state change command</summary>
      /// <param name="command">The new command acting on the initial state</param>
      /// <returns>The finite state machine, now programmed with the new state change command</returns>
      public OutStateMachine On(U command)
      {
        return IN.On(command);
      }

      /// <summary>Instructs the state machine to transit to a new state from this initial state and state change command</summary>
      /// <param name="targetstate">The new state to transition into</param>
      /// <returns>The same state machine, awaiting further instructions</returns>
      public OutStateMachine Goto(T targetstate) { NewState = targetstate; return this; }

      /// <summary>Instructs the state machine to execute a transition action from this initial state and state change command</summary>
      /// <param name="action">The action to be executed</param>
      /// <returns>The same state machine, awaiting further instructions</returns>
      public OutStateMachine Execute(Action<O, T> action) { Transit = action; return this; }

      internal void Fire(O owner, ref T state)
      {
        Transit?.Invoke(owner, state);
        state = NewState;
      }
    }

    /// <summary>
    /// Fires a command and performs any state changes or transition actions.
    /// </summary>
    /// <param name="owner">The parameterized owner object to be passed to any transition action that is executed by this command</param>
    /// <param name="command">The command</param>
    /// <param name="state">The state</param>
    public void Fire(O owner, U command, ref T state)
    {
      if (_transitions.TryGetValue(state, out InStateMachine _in))
        _in.Fire(owner, command, ref state);
      else
        throw new InvalidStateCommandException<T, U>(command, state);
    }

    /// <summary>
    /// Determines if a command is valid at this state
    /// </summary>
    /// <param name="command">The command</param>
    /// <param name="state">The state</param>
    public bool IsValid(U command, T state)
    {
        return _transitions.TryGetValue(state, out InStateMachine _in) && _in.IsValid(command);
    }
  }
}
