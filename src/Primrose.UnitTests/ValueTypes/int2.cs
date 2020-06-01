using NUnit.Framework;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.ValueTypes
{
  [TestFixture]
  public class Int2
  {
    private const string _NumberSource = "NumberSource";
    private static object[] NumberSource =
    {
      new object[] { 0, 0 },
      new object[] { -45, 6},
      new object[] { 37, -117},
      new object[] { 2, 58},
      new object[] { -128, -170},
      new object[] { 121, 11},
      new object[] { int.MinValue, int.MinValue },
      new object[] { int.MaxValue, int.MaxValue }
    };

    private const string _InvalidStringSource = "InvalidStringSource";
    private static object[] InvalidStringSource =
    {
      new object[] { "{9999999999, 0}" },
      new object[] { "{0, -9999999999}" },
      new object[] { "NotANumber" }
    };

    private static string NormalFormat = "{{{0},{1}}}";
    private static string NoBracketFormat = "{0},{1}";
    private static string[] ArbitaryWhiteSpaceFormat =
    {
      "{{{0}, {1}}}",
      "{{{0}   , {1}}}",
      "{{{0} \t,\t{1}}}",
      "{{   {0} , {1}   \t}}",
      "    {0}, {1}     ",
      "\t\t\t\t {0}, {1}"
    };

    [TestCaseSource(_NumberSource)]
    public void Int2_GetFields(int v0, int v1)
    {
      int2 n = new int2(v0, v1);
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void Int2_GetIndices(int v0, int v1)
    {
      int2 n = new int2(v0, v1);
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void Int2_SetFields(int v0, int v1)
    {
      int2 n;
      n.x = v0;
      n.y = v1;
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void Int2_SetIndices(int v0, int v1)
    {
      int2 n = new int2();
      n[0] = v0;
      n[1] = v1;
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void Int2_Add(int v0, int v1)
    {
      unchecked
      {
        int2 n = new int2(v0, v1);
        int2 x = new int2(v1, v0);
        int2 nx = n + x;
        int2 n_expect = new int2(n[0] + x[0], n[1] + x[1]);

        Assert.That(nx[0], Is.EqualTo(n_expect[0]));
        Assert.That(nx[1], Is.EqualTo(n_expect[1]));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int2_Subtract(int v0, int v1)
    {
      unchecked
      {
        int2 n = new int2(v0, v1);
        int2 x = new int2(v1, v0);
        int2 nx = n - x;
        int2 n_expect = new int2(n[0] - x[0], n[1] - x[1]);

        Assert.That(nx[0], Is.EqualTo(n_expect[0]));
        Assert.That(nx[1], Is.EqualTo(n_expect[1]));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int2_TermwiseMultiply(int v0, int v1)
    {
      unchecked
      {
        int2 n = new int2(v0, v1);
        int2 x = new int2(v1, v0);
        int2 nx = n * x;
        int2 n_expect = new int2(n[0] * x[0], n[1] * x[1]);

        Assert.That(nx[0], Is.EqualTo(n_expect[0]));
        Assert.That(nx[1], Is.EqualTo(n_expect[1]));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int2_ScalarMultiply(int v0, int v1)
    {
      unchecked
      {
        int2 n = new int2(v0, v1);
        int x = v0 + v1;
        int2 nx = n * x;
        int2 n_expect = new int2(n[0] * x, n[1] * x);

        Assert.That(nx[0], Is.EqualTo(n_expect[0]));
        Assert.That(nx[1], Is.EqualTo(n_expect[1]));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int2_ParseFromString(int v0, int v1)
    {
      string s = NormalFormat.F(v0, v1);
      int2 n = new int2(v0, v1);
      int2 ns = int2.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Int2_ParseFromString_ArbitarySpace(int v0, int v1)
    {
      foreach (string fmt in ArbitaryWhiteSpaceFormat)
      {
        string s = fmt.F(v0, v1);
        int2 n = new int2(v0, v1);
        int2 ns = int2.Parse(s);
        Assert.That(ns, Is.EqualTo(n));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int2_ParseFromString_NoBrackets(int v0, int v1)
    {
      string s = NoBracketFormat.F(v0, v1);
      int2 n = new int2(v0, v1);
      int2 ns = int2.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Int2_WriteToString(int v0, int v1)
    {
      string s = NormalFormat.F(v0, v1);
      int2 n = new int2(v0, v1);
      string sn = n.ToString();
      Assert.That(sn, Is.EqualTo(s));
    }

    [TestCaseSource(_NumberSource)]
    public void Int2_TryParse(int v0, int v1)
    {
      string s = NormalFormat.F(v0, v1);
      int2 n = new int2(v0, v1);
      int2 ns;
      Assert.IsTrue(int2.TryParse(s, out ns));
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_InvalidStringSource)]
    public void Int2_TryParse_InvalidReturnsFalse(string invalid)
    {
      string s = invalid;
      int2 ns;
      Assert.IsFalse(int2.TryParse(s, out ns));
    }
  }
}
