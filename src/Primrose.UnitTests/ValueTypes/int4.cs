using NUnit.Framework;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.ValueTypes
{
  [TestFixture]
  public class Int4
  {
    private const string _NumberSource = "NumberSource";
    private static object[] NumberSource =
    {
      new object[] { 0, 0, 0, 0 },
      new object[] { -45, 6, 15, 25},
      new object[] { 37, -117, 53, 75},
      new object[] { 2, 58, -233, 100},
      new object[] { -128, -170, -69, -86},
      new object[] { 121, 11, 1, 111},
      new object[] { int.MinValue, int.MinValue, int.MinValue, int.MinValue },
      new object[] { int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue }
    };

    private const string _InvalidStringSource = "InvalidStringSource";
    private static object[] InvalidStringSource =
    {
      new object[] { "{9999999999, 0, 0, 0}" },
      new object[] { "{0, -9999999999, 0, 0}" },
      new object[] { "{0, 0, -9999999999, 0}" },
      new object[] { "{0, 0, 0, 9999999999}" },
      new object[] { "NotANumber" }
    };

    private static string NormalFormat = "{{{0},{1},{2},{3}}}";
    private static string NoBracketFormat = "{0},{1},{2},{3}";
    private static string[] ArbitaryWhiteSpaceFormat =
    {
      "{{{0}, {1}, {2}, {3}}}",
      "{{{0}   , {1}    , {2}      , {3}}}",
      "{{{0} \t,\t{1},\t{2}\t,\t{3}}}",
      "{{   {0} , {1}   \t,{2}  \t,   {3}   \t}}",
      "    {0}, {1} ,{2} ,  {3}     ",
      "\t\t\t\t {0}, {1}, {2}, {3}"
    };

    [TestCaseSource(_NumberSource)]
    public void Int4_GetFields(int v0, int v1, int v2, int v3)
    {
      int4 n = new int4(v0, v1, v2, v3);
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
      Assert.That(n.z, Is.EqualTo(v2));
      Assert.That(n.w, Is.EqualTo(v3));
    }

    [TestCaseSource(_NumberSource)]
    public void Int4_GetIndices(int v0, int v1, int v2, int v3)
    {
      int4 n = new int4(v0, v1, v2, v3);
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
      Assert.That(n[2], Is.EqualTo(v2));
      Assert.That(n[3], Is.EqualTo(v3));
    }

    [TestCaseSource(_NumberSource)]
    public void Int4_SetFields(int v0, int v1, int v2, int v3)
    {
      int4 n;
      n.x = v0;
      n.y = v1;
      n.z = v2;
      n.w = v3;
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
      Assert.That(n.z, Is.EqualTo(v2));
      Assert.That(n.w, Is.EqualTo(v3));
    }

    [TestCaseSource(_NumberSource)]
    public void Int4_SetIndices(int v0, int v1, int v2, int v3)
    {
      int4 n = new int4();
      n[0] = v0;
      n[1] = v1;
      n[2] = v2;
      n[3] = v3;
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
      Assert.That(n[2], Is.EqualTo(v2));
      Assert.That(n[3], Is.EqualTo(v3));
    }

    [TestCaseSource(_NumberSource)]
    public void Int4_Add(int v0, int v1, int v2, int v3)
    {
      unchecked
      {
        int4 n = new int4(v0, v1, v2, v3);
        int4 x = new int4(v1, v2, v3, v0);
        int4 nx = n + x;
        int4 n_expect = new int4(n[0] + x[0], n[1] + x[1], n[2] + x[2], n[3] + x[3]);

        Assert.That(nx[0], Is.EqualTo(n_expect[0]));
        Assert.That(nx[1], Is.EqualTo(n_expect[1]));
        Assert.That(nx[2], Is.EqualTo(n_expect[2]));
        Assert.That(nx[3], Is.EqualTo(n_expect[3]));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int4_Subtract(int v0, int v1, int v2, int v3)
    {
      unchecked
      {
        int4 n = new int4(v0, v1, v2, v3);
        int4 x = new int4(v1, v2, v3, v0);
        int4 nx = n - x;
        int4 n_expect = new int4(n[0] - x[0], n[1] - x[1], n[2] - x[2], n[3] - x[3]);

        Assert.That(nx[0], Is.EqualTo(n_expect[0]));
        Assert.That(nx[1], Is.EqualTo(n_expect[1]));
        Assert.That(nx[2], Is.EqualTo(n_expect[2]));
        Assert.That(nx[3], Is.EqualTo(n_expect[3]));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int4_TermwiseMultiply(int v0, int v1, int v2, int v3)
    {
      unchecked
      { int4 n = new int4(v0, v1, v2, v3);
        int4 x = new int4(v1, v2, v3, v0);
        int4 nx = n * x;
        int4 n_expect = new int4(n[0] * x[0], n[1] * x[1], n[2] * x[2], n[3] * x[3]);

        Assert.That(nx[0], Is.EqualTo(n_expect[0]));
        Assert.That(nx[1], Is.EqualTo(n_expect[1]));
        Assert.That(nx[2], Is.EqualTo(n_expect[2]));
        Assert.That(nx[3], Is.EqualTo(n_expect[3]));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int4_ScalarMultiply(int v0, int v1, int v2, int v3)
    {
      unchecked
      {
        int4 n = new int4(v0, v1, v2, v3);
        int x = v0 + v1 + v2 + v3;
        int4 nx = n * x;
        int4 n_expect = new int4(n[0] * x, n[1] * x, n[2] * x, n[3] * x);

        Assert.That(nx[0], Is.EqualTo(n_expect[0]));
        Assert.That(nx[1], Is.EqualTo(n_expect[1]));
        Assert.That(nx[2], Is.EqualTo(n_expect[2]));
        Assert.That(nx[3], Is.EqualTo(n_expect[3]));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int4_ParseFromString(int v0, int v1, int v2, int v3)
    {
      string s = NormalFormat.F(v0, v1, v2, v3);
      int4 n = new int4(v0, v1, v2, v3);
      int4 ns = int4.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Int4_ParseFromString_ArbitarySpace(int v0, int v1, int v2, int v3)
    {
      foreach (string fmt in ArbitaryWhiteSpaceFormat)
      {
        string s = fmt.F(v0, v1, v2, v3);
        int4 n = new int4(v0, v1, v2, v3);
        int4 ns = int4.Parse(s);
        Assert.That(ns, Is.EqualTo(n));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Int4_ParseFromString_NoBrackets(int v0, int v1, int v2, int v3)
    {
      string s = NoBracketFormat.F(v0, v1, v2, v3);
      int4 n = new int4(v0, v1, v2, v3);
      int4 ns = int4.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Int4_WriteToString(int v0, int v1, int v2, int v3)
    {
      string s = NormalFormat.F(v0, v1, v2, v3);
      int4 n = new int4(v0, v1, v2, v3);
      string sn = n.ToString();
      Assert.That(sn, Is.EqualTo(s));
    }

    [TestCaseSource(_NumberSource)]
    public void Int4_TryParse(int v0, int v1, int v2, int v3)
    {
      string s = NormalFormat.F(v0, v1, v2, v3);
      int4 n = new int4(v0, v1, v2, v3);
      int4 ns;
      Assert.IsTrue(int4.TryParse(s, out ns));
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_InvalidStringSource)]
    public void Int4_TryParse_InvalidReturnsFalse(string invalid)
    {
      string s = invalid;
      int4 ns;
      Assert.IsFalse(int4.TryParse(s, out ns));
    }
  }
}
