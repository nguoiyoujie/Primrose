using NUnit.Framework;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.ValueTypes
{
  [TestFixture]
  public class Float4
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
    };

    private const string _InvalidStringSource = "InvalidStringSource";
    private static object[] InvalidStringSource =
    {
      new object[] { "NotANumber" }
    };

    private static float Tolerance = 0.00001f;

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
    public void Float4_GetFields(float v0, float v1, float v2, float v3)
    {
      float4 n = new float4(v0, v1, v2, v3);
      Assert.That(n.x, Is.EqualTo(v0).Within(Tolerance));
      Assert.That(n.y, Is.EqualTo(v1).Within(Tolerance));
      Assert.That(n.z, Is.EqualTo(v2).Within(Tolerance));
      Assert.That(n.w, Is.EqualTo(v3).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float4_GetIndices(float v0, float v1, float v2, float v3)
    {
      float4 n = new float4(v0, v1, v2, v3);
      Assert.That(n[0], Is.EqualTo(v0).Within(Tolerance));
      Assert.That(n[1], Is.EqualTo(v1).Within(Tolerance));
      Assert.That(n[2], Is.EqualTo(v2).Within(Tolerance));
      Assert.That(n[3], Is.EqualTo(v3).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float4_SetFields(float v0, float v1, float v2, float v3)
    {
      float4 n;
      n.x = v0;
      n.y = v1;
      n.z = v2;
      n.w = v3;
      Assert.That(n.x, Is.EqualTo(v0).Within(Tolerance));
      Assert.That(n.y, Is.EqualTo(v1).Within(Tolerance));
      Assert.That(n.z, Is.EqualTo(v2).Within(Tolerance));
      Assert.That(n.w, Is.EqualTo(v3).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float4_SetIndices(float v0, float v1, float v2, float v3)
    {
      float4 n = new float4();
      n[0] = v0;
      n[1] = v1;
      n[2] = v2;
      n[3] = v3;
      Assert.That(n[0], Is.EqualTo(v0).Within(Tolerance));
      Assert.That(n[1], Is.EqualTo(v1).Within(Tolerance));
      Assert.That(n[2], Is.EqualTo(v2).Within(Tolerance));
      Assert.That(n[3], Is.EqualTo(v3).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float4_Add(float v0, float v1, float v2, float v3)
    {
      float4 n = new float4(v0, v1, v2, v3);
      float4 x = new float4(v1, v2, v3, v0);
      float4 nx = n + x;
      float4 n_expect = new float4(n[0] + x[0], n[1] + x[1], n[2] + x[2], n[3] + x[3]);

      Assert.That(nx[0], Is.EqualTo(n_expect[0]).Within(Tolerance));
      Assert.That(nx[1], Is.EqualTo(n_expect[1]).Within(Tolerance));
      Assert.That(nx[2], Is.EqualTo(n_expect[2]).Within(Tolerance));
      Assert.That(nx[3], Is.EqualTo(n_expect[3]).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float4_Subtract(float v0, float v1, float v2, float v3)
    {
      float4 n = new float4(v0, v1, v2, v3);
      float4 x = new float4(v1, v2, v3, v0);
      float4 nx = n - x;
      float4 n_expect = new float4(n[0] - x[0], n[1] - x[1], n[2] - x[2], n[3] - x[3]);

      Assert.That(nx[0], Is.EqualTo(n_expect[0]).Within(Tolerance));
      Assert.That(nx[1], Is.EqualTo(n_expect[1]).Within(Tolerance));
      Assert.That(nx[2], Is.EqualTo(n_expect[2]).Within(Tolerance));
      Assert.That(nx[3], Is.EqualTo(n_expect[3]).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float4_TermwiseMultiply(float v0, float v1, float v2, float v3)
    {
      float4 n = new float4(v0, v1, v2, v3);
      float4 x = new float4(v1, v2, v3, v0);
      float4 nx = n * x;
      float4 n_expect = new float4(n[0] * x[0], n[1] * x[1], n[2] * x[2], n[3] * x[3]);

      Assert.That(nx[0], Is.EqualTo(n_expect[0]).Within(Tolerance));
      Assert.That(nx[1], Is.EqualTo(n_expect[1]).Within(Tolerance));
      Assert.That(nx[2], Is.EqualTo(n_expect[2]).Within(Tolerance));
      Assert.That(nx[3], Is.EqualTo(n_expect[3]).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float4_ScalarMultiply(float v0, float v1, float v2, float v3)
    {
      float4 n = new float4(v0, v1, v2, v3);
      float x = v0 + v1 + v2 + v3;
      float4 nx = n * x;
      float4 n_expect = new float4(n[0] * x, n[1] * x, n[2] * x, n[3] * x);

      Assert.That(nx[0], Is.EqualTo(n_expect[0]).Within(Tolerance));
      Assert.That(nx[1], Is.EqualTo(n_expect[1]).Within(Tolerance));
      Assert.That(nx[2], Is.EqualTo(n_expect[2]).Within(Tolerance));
      Assert.That(nx[3], Is.EqualTo(n_expect[3]).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float4_ParseFromString(float v0, float v1, float v2, float v3)
    {
      string s = NormalFormat.F(v0, v1, v2, v3);
      float4 n = new float4(v0, v1, v2, v3);
      float4 ns = float4.Parse(s);
      Assert.That(ns, Is.EqualTo(n).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float4_ParseFromString_ArbitarySpace(float v0, float v1, float v2, float v3)
    {
      foreach (string fmt in ArbitaryWhiteSpaceFormat)
      {
        string s = fmt.F(v0, v1, v2, v3);
        float4 n = new float4(v0, v1, v2, v3);
        float4 ns = float4.Parse(s);
        Assert.That(ns, Is.EqualTo(n).Within(Tolerance));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Float4_ParseFromString_NoBrackets(float v0, float v1, float v2, float v3)
    {
      string s = NoBracketFormat.F(v0, v1, v2, v3);
      float4 n = new float4(v0, v1, v2, v3);
      float4 ns = float4.Parse(s);
      Assert.That(ns, Is.EqualTo(n).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float4_WriteToString(float v0, float v1, float v2, float v3)
    {
      string s = NormalFormat.F(v0, v1, v2, v3);
      float4 n = new float4(v0, v1, v2, v3);
      string sn = n.ToString();
      Assert.That(sn, Is.EqualTo(s));
    }

    [TestCaseSource(_NumberSource)]
    public void Float4_TryParse(float v0, float v1, float v2, float v3)
    {
      string s = NormalFormat.F(v0, v1, v2, v3);
      float4 n = new float4(v0, v1, v2, v3);
      float4 ns;
      Assert.IsTrue(float4.TryParse(s, out ns));
      Assert.That(ns, Is.EqualTo(n).Within(Tolerance));
    }

    [TestCaseSource(_InvalidStringSource)]
    public void Float4_TryParse_InvalidReturnsFalse(string invalid)
    {
      string s = invalid;
      float4 ns;
      Assert.IsFalse(float4.TryParse(s, out ns));
    }
  }
}
