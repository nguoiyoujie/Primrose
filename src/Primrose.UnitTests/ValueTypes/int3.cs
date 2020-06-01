using NUnit.Framework;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.ValueTypes
{
  [TestFixture]
  public class Int3
  {
    private const string _NumberSource = "NumberSource";
    private static object[] NumberSource =
    {
      new object[] { 0, 0, 0 },
      new object[] { -45, 6, 15},
      new object[] { 37, -117, 53},
      new object[] { 2, 58, -233},
      new object[] { -128, -170, -69},
      new object[] { 121, 11, 1},
      new object[] { int.MinValue, int.MinValue, int.MinValue  },
      new object[] { int.MaxValue, int.MaxValue, int.MaxValue }
    };

    private const string _InvalidStringSource = "InvalidStringSource";
    private static object[] InvalidStringSource =
    {
      new object[] { "{9999999999, 0, 0}" },
      new object[] { "{0, -9999999999, 0}" },
      new object[] { "{0, 0, 9999999999}" },
      new object[] { "NotANumber" }
    };

    private static string NormalFormat = "{{{0},{1},{2}}}";
    private static string NoBracketFormat = "{0},{1},{2}";
    private static string[] ArbitaryWhiteSpaceFormat =
    {
      "{{{0}, {1}, {2}}}",
      "{{{0}   , {1}    , {2}}}",
      "{{{0} \t,\t{1},\t{2}}}",
      "{{   {0} , {1}   \t,{2}  \t}}",
      "    {0}, {1} ,{2}      ",
      "\t\t\t\t {0}, {1}, {2}"
    };

    [TestCaseSource(_NumberSource)]
    public void Int3_GetFields(int v0, int v1, int v2)
    {
      int3 n = new int3(v0, v1, v2);
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
      Assert.That(n.z, Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void Int3_GetIndices(int v0, int v1, int v2)
    {
      int3 n = new int3(v0, v1, v2);
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
      Assert.That(n[2], Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void Int3_SetFields(int v0, int v1, int v2)
    {
      int3 n;
      n.x = v0;
      n.y = v1;
      n.z = v2;
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
      Assert.That(n.z, Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void Int3_SetIndices(int v0, int v1, int v2)
    {
      int3 n = new int3();
      n[0] = v0;
      n[1] = v1;
      n[2] = v2;
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
      Assert.That(n[2], Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void Int3_Add(int v0, int v1, int v2)
    {
      unchecked
      {
        int3 n = new int3(v0, v1, v2);
        int3 x = new int3(v1, v2, v0);
        int3 nx = n + x;
        int3 n_expect = new int3(n[0] + x[0], n[1] + x[1], n[2] + x[2]);

        Assert.That(nx[0], Is.EqualTo(n_expect[0]));
        Assert.That(nx[1], Is.EqualTo(n_expect[1]));
        Assert.That(nx[2], Is.EqualTo(n_expect[2]));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int3_Subtract(int v0, int v1, int v2)
    {
      unchecked
      {
        int3 n = new int3(v0, v1, v2);
        int3 x = new int3(v1, v2, v0);
        int3 nx = n - x;
        int3 n_expect = new int3(n[0] - x[0], n[1] - x[1], n[2] - x[2]);

        Assert.That(nx[0], Is.EqualTo(n_expect[0]));
        Assert.That(nx[1], Is.EqualTo(n_expect[1]));
        Assert.That(nx[2], Is.EqualTo(n_expect[2]));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int3_TermwiseMultiply(int v0, int v1, int v2)
    {
      unchecked
      {
        int3 n = new int3(v0, v1, v2);
        int3 x = new int3(v1, v2, v0);
        int3 nx = n * x;
        int3 n_expect = new int3(n[0] * x[0], n[1] * x[1], n[2] * x[2]);

        Assert.That(nx[0], Is.EqualTo(n_expect[0]));
        Assert.That(nx[1], Is.EqualTo(n_expect[1]));
        Assert.That(nx[2], Is.EqualTo(n_expect[2]));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int3_ScalarMultiply(int v0, int v1, int v2)
    {
      unchecked
      {
        int3 n = new int3(v0, v1, v2);
        int x = v0 + v1 + v2;
        int3 nx = n * x;
        int3 n_expect = new int3(n[0] * x, n[1] * x, n[2] * x);

        Assert.That(nx[0], Is.EqualTo(n_expect[0]));
        Assert.That(nx[1], Is.EqualTo(n_expect[1]));
        Assert.That(nx[2], Is.EqualTo(n_expect[2]));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int3_ParseFromString(int v0, int v1, int v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      int3 n = new int3(v0, v1, v2);
      int3 ns = int3.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Int3_ParseFromString_ArbitarySpace(int v0, int v1, int v2)
    {
      foreach (string fmt in ArbitaryWhiteSpaceFormat)
      {
        string s = fmt.F(v0, v1, v2);
        int3 n = new int3(v0, v1, v2);
        int3 ns = int3.Parse(s);
        Assert.That(ns, Is.EqualTo(n));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int3_ParseFromString_NoBrackets(int v0, int v1, int v2)
    {
      string s = NoBracketFormat.F(v0, v1, v2);
      int3 n = new int3(v0, v1, v2);
      int3 ns = int3.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Int3_WriteToString(int v0, int v1, int v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      int3 n = new int3(v0, v1, v2);
      string sn = n.ToString();
      Assert.That(sn, Is.EqualTo(s));
    }

    [TestCaseSource(_NumberSource)]
    public void Int3_TryParse(int v0, int v1, int v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      int3 n = new int3(v0, v1, v2);
      int3 ns;
      Assert.IsTrue(int3.TryParse(s, out ns));
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_InvalidStringSource)]
    public void Int3_TryParse_InvalidReturnsFalse(string invalid)
    {
      string s = invalid;
      int3 ns;
      Assert.IsFalse(int3.TryParse(s, out ns));
    }
  }
}
