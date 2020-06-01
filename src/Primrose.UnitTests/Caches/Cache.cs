using NUnit.Framework;
using Primrose.Primitives;
using System;
using System.Collections.Generic;

namespace Primrose.UnitTests
{
  [TestFixture]
  public class Caches
  {
    private string key = "key";
    private EqualityComparer<int> cmp = EqualityComparer<int>.Default;
    private Func<int, int> f_ret = (s) => { return s; };

    private const string _NumberSource = "NumberSource";
    private static object[] NumberSource =
    {
      new object[] { 0, 0, new int[] { 1, 2, 3, 5, 8, 13 } },
      new object[] { 0, 0, new int[] { 1, 2, 3} }
    };

    [TestCaseSource(_NumberSource)]
    public void Cache_SameToken_NoUpdate(int token, int first_val, params int[] vals)
    {
      Cache<string, int, int, int> test_cache = new Cache<string, int, int, int>();

      test_cache.Define(key, 0);
      int first_result = test_cache.GetOrDefine(key, token, f_ret, first_val, cmp);
      Assert.That(first_result, Is.EqualTo(f_ret(first_val)));

      foreach (int v in vals)
      {
        int result = test_cache.GetOrDefine(key, token, f_ret, v, cmp);
        Assert.That(result, Is.EqualTo(first_result));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void DifferentToken_Update(int token, int first_val, params int[] vals)
    {
      Cache<string, int, int, int> test_cache = new Cache<string, int, int, int>();

      test_cache.Define(key, 0);
      int first_result = test_cache.GetOrDefine(key, token, f_ret, first_val, cmp);
      Assert.That(first_result, Is.EqualTo(f_ret(first_val)));

      foreach (int v in vals)
      {
        int result = test_cache.GetOrDefine(key, ++token, f_ret, v, cmp);
        Assert.That(result, Is.EqualTo(f_ret(v)));
      }
    }
  }
}
