using NUnit.Framework;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.ValueTypes
{
  [TestFixture]
  public class Float2
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
    };

    private const string _InvalidStringSource = "InvalidStringSource";
    private static object[] InvalidStringSource =
    {
      new object[] { "NotANumber" }
    };

    private static float Tolerance = 0.00001f;

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
    public void Float2_GetFields(float v0, float v1)
    {
      float2 n = new float2(v0, v1);
      Assert.That(n.x, Is.EqualTo(v0).Within(Tolerance));
      Assert.That(n.y, Is.EqualTo(v1).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float2_GetIndices(float v0, float v1)
    {
      float2 n = new float2(v0, v1);
      Assert.That(n[0], Is.EqualTo(v0).Within(Tolerance));
      Assert.That(n[1], Is.EqualTo(v1).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float2_SetFields(float v0, float v1)
    {
      float2 n;
      n.x = v0;
      n.y = v1;
      Assert.That(n.x, Is.EqualTo(v0).Within(Tolerance));
      Assert.That(n.y, Is.EqualTo(v1).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float2_SetIndices(float v0, float v1)
    {
      float2 n = new float2();
      n[0] = v0;
      n[1] = v1;
      Assert.That(n[0], Is.EqualTo(v0).Within(Tolerance));
      Assert.That(n[1], Is.EqualTo(v1).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float2_Add(float v0, float v1)
    {
      float2 n = new float2(v0, v1);
      float2 x = new float2(v1, v0);
      float2 nx = n + x;
      float2 n_expect = new float2(n[0] + x[0], n[1] + x[1]);

      Assert.That(nx[0], Is.EqualTo(n_expect[0]).Within(Tolerance));
      Assert.That(nx[1], Is.EqualTo(n_expect[1]).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float2_Subtract(float v0, float v1)
    {
      float2 n = new float2(v0, v1);
      float2 x = new float2(v1, v0);
      float2 nx = n - x;
      float2 n_expect = new float2(n[0] - x[0], n[1] - x[1]);

      Assert.That(nx[0], Is.EqualTo(n_expect[0]).Within(Tolerance));
      Assert.That(nx[1], Is.EqualTo(n_expect[1]).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float2_TermwiseMultiply(float v0, float v1)
    {
      float2 n = new float2(v0, v1);
      float2 x = new float2(v1, v0);
      float2 nx = n * x;
      float2 n_expect = new float2(n[0] * x[0], n[1] * x[1]);

      Assert.That(nx[0], Is.EqualTo(n_expect[0]).Within(Tolerance));
      Assert.That(nx[1], Is.EqualTo(n_expect[1]).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float2_ScalarMultiply(float v0, float v1)
    {
      float2 n = new float2(v0, v1);
      float x = v0 + v1;
      float2 nx = n * x;
      float2 n_expect = new float2(n[0] * x, n[1] * x);

      Assert.That(nx[0], Is.EqualTo(n_expect[0]).Within(Tolerance));
      Assert.That(nx[1], Is.EqualTo(n_expect[1]).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float2_ParseFromString(float v0, float v1)
    {
      string s = NormalFormat.F(v0, v1);
      float2 n = new float2(v0, v1);
      float2 ns = float2.Parse(s);
      Assert.That(ns, Is.EqualTo(n).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float2_ParseFromString_ArbitarySpace(float v0, float v1)
    {
      foreach (string fmt in ArbitaryWhiteSpaceFormat)
      {
        string s = fmt.F(v0, v1);
        float2 n = new float2(v0, v1);
        float2 ns = float2.Parse(s);
        Assert.That(ns, Is.EqualTo(n).Within(Tolerance));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Float2_ParseFromString_NoBrackets(float v0, float v1)
    {
      string s = NoBracketFormat.F(v0, v1);
      float2 n = new float2(v0, v1);
      float2 ns = float2.Parse(s);
      Assert.That(ns, Is.EqualTo(n).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float2_WriteToString(float v0, float v1)
    {
      string s = NormalFormat.F(v0, v1);
      float2 n = new float2(v0, v1);
      string sn = n.ToString();
      Assert.That(sn, Is.EqualTo(s));
    }

    [TestCaseSource(_NumberSource)]
    public void Float2_TryParse(float v0, float v1)
    {
      string s = NormalFormat.F(v0, v1);
      float2 n = new float2(v0, v1);
      float2 ns;
      Assert.IsTrue(float2.TryParse(s, out ns));
      Assert.That(ns, Is.EqualTo(n).Within(Tolerance));
    }

    [TestCaseSource(_InvalidStringSource)]
    public void Float2_TryParse_InvalidReturnsFalse(string invalid)
    {
      string s = invalid;
      float2 ns;
      Assert.IsFalse(float2.TryParse(s, out ns));
    }
  }
}
