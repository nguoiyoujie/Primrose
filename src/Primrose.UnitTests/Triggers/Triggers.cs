using NUnit.Framework;
using Primrose.Primitives.Observables;
using Primrose.Primitives.Triggers;
using System;

namespace Primrose.UnitTests.Collections
{
  [TestFixture]
  public class Triggers
  {
    private const string _ValueConditionSource = "ValueConditionSource";
    private static object[] ValueConditionSource =
    {
      new object[] { MatchType.NUMERIC_EQUALS, 1, 100, 1 },
      new object[] { MatchType.EQUALS, 2, 100, 1 },
      new object[] { MatchType.STARTS_WITH, 1, 100, 11 },
      new object[] { MatchType.MORE_THAN, 50, 100, 49 },
    };

    [TestCaseSource(_ValueConditionSource)]
    public void Triggers_ValueCondition_TriggerCount(MatchType matchType, int cmp, int count_until_exclusive, int expected_result)
    {
      ObservableValue<int> x = new ObservableValue<int>(-1);
      ICondition cond = new ValueCondition<int>(ref x, matchType, cmp);

      int triggercount = 0;
      Action action = () => { triggercount++; Console.Write(" " + x.Value); };
      Trigger trigger = new Trigger(cond, action);

      Console.Write("Trigger at: ");
      for (int i = 0; i < count_until_exclusive; i++)
      {
        x.Value = i;
      }

      Assert.That(triggercount, Is.EqualTo(expected_result));
    }

    private const string _FuncConditionSource = "FuncConditionSource";
    private static object[] FuncConditionSource =
    {
      new object[] { new Func<int, bool>(i => i % 4 == 0), 100, 25 },
    };

    [TestCaseSource(_FuncConditionSource)]
    public void Triggers_FuncCondition_TriggerCount(Func<int, bool> func, int count_until_exclusive, int expected_result)
    {
      ObservableValue<int> x = new ObservableValue<int>(-1);
      ICondition cond = new FuncCondition<int>(ref x, func);

      int triggercount = 0;
      Action action = () => { triggercount++; Console.Write(" " + x.Value); };
      Trigger trigger = new Trigger(cond, action);

      Console.Write("Trigger at: ");
      for (int i = 0; i < count_until_exclusive; i++)
      {
        x.Value = i;
      }

      Assert.That(triggercount, Is.EqualTo(expected_result));
    }

    private const string _ValueConditionOrSource = "ValueConditionOrSource";
    private static object[] ValueConditionOrSource =
    {
      new object[] { MatchType.CONTAINS, 5, MatchType.CONTAINS, 4, 100, 36 }
    };

    [TestCaseSource(_ValueConditionOrSource)]
    public void Triggers_ValueCondition_TriggerCount_Or(MatchType matchType1, int cmp1, MatchType matchType2, int cmp2, int count_until_exclusive, int expected_result)
    {
      ObservableValue<int> x = new ObservableValue<int>(-1);
      ICondition cond1 = new ValueCondition<int>(ref x, matchType1, cmp1);
      ICondition cond2 = new ValueCondition<int>(ref x, matchType2, cmp2);

      int triggercount = 0;
      Action action = () => { triggercount++; Console.Write(" " + x.Value); };
      Trigger trigger = new Trigger(cond1.Or(cond2), action);

      Console.Write("Trigger at: ");
      for (int i = 0; i < count_until_exclusive; i++)
      {
        x.Value = i;
      }

      Assert.That(triggercount, Is.EqualTo(expected_result * 2)); // because there are two observable links, 2 events is expected per change.
    }

    private const string _FuncConditionOrSource = "FuncConditionOrSource";
    private static object[] FuncConditionOrSource =
    {
      new object[] { new Func<int, bool>(i => i % 4 == 0), new Func<int, bool>(i => i % 6 == 0), 100, 33 },
    };

    [TestCaseSource(_FuncConditionOrSource)]
    public void Triggers_FuncCondition_TriggerCount_Or(Func<int, bool> func1, Func<int, bool> func2, int count_until_exclusive, int expected_result)
    {
      ObservableValue<int> x = new ObservableValue<int>(-1);
      ICondition cond1 = new FuncCondition<int>(ref x, func1);
      ICondition cond2 = new FuncCondition<int>(ref x, func2);

      int triggercount = 0;
      Action action = () => { triggercount++; Console.Write(" " + x.Value); };
      Trigger trigger = new Trigger(cond1.Or(cond2), action);

      Console.Write("Trigger at: ");
      for (int i = 0; i < count_until_exclusive; i++)
      {
        x.Value = i;
      }

      Assert.That(triggercount, Is.EqualTo(expected_result * 2)); // because there are two observable links, 2 events is expected per change.
    }

    private const string _ValueConditionAndSource = "ValueConditionAndSource";
    private static object[] ValueConditionAndSource =
    {
      new object[] { MatchType.CONTAINS, 5, MatchType.CONTAINS, 4, 100, 2 }
    };

    [TestCaseSource(_ValueConditionAndSource)]
    public void Triggers_ValueCondition_TriggerCount_And(MatchType matchType1, int cmp1, MatchType matchType2, int cmp2, int count_until_exclusive, int expected_result)
    {
      ObservableValue<int> x = new ObservableValue<int>(-1);
      ICondition cond1 = new ValueCondition<int>(ref x, matchType1, cmp1);
      ICondition cond2 = new ValueCondition<int>(ref x, matchType2, cmp2);
      int triggercount = 0;

      Action action = () => { triggercount++; Console.Write(" " + x.Value); };
      Trigger trigger = new Trigger(cond1.And(cond2), action);

      Console.Write("Trigger at: ");
      for (int i = 0; i < count_until_exclusive; i++)
      {
        x.Value = i;
      }

      Assert.That(triggercount, Is.EqualTo(expected_result * 2)); // because there are two observable links, 2 events is expected per change.
    }

    private const string _FuncConditionAndSource = "FuncConditionAndSource";
    private static object[] FuncConditionAndSource =
    {
      new object[] { new Func<int, bool>(i => i % 4 == 0), new Func<int, bool>(i => i % 6 == 0), 100, 9 },
    };

    [TestCaseSource(_FuncConditionAndSource)]
    public void Triggers_FuncCondition_TriggerCount_And(Func<int, bool> func1, Func<int, bool> func2, int count_until_exclusive, int expected_result)
    {
      ObservableValue<int> x = new ObservableValue<int>(-1);
      ICondition cond1 = new FuncCondition<int>(ref x, func1);
      ICondition cond2 = new FuncCondition<int>(ref x, func2);
      int triggercount = 0;

      Action action = () => { triggercount++; Console.Write(" " + x.Value); };
      Trigger trigger = new Trigger(cond1.And(cond2), action);

      Console.Write("Trigger at: ");
      for (int i = 0; i < count_until_exclusive; i++)
      {
        x.Value = i;
      }

      Assert.That(triggercount, Is.EqualTo(expected_result * 2)); // because there are two observable links, 2 events is expected per change.
    }
  }
}
