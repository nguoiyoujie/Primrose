using NUnit.Framework;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.ValueTypes
{
  [TestFixture]
  public class Float3
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
    };

    private const string _InvalidStringSource = "InvalidStringSource";
    private static object[] InvalidStringSource =
    {
      new object[] { "NotANumber" }
    };

    private static float Tolerance = 0.00001f;

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
    public void Float3_GetFields(float v0, float v1, float v2)
    {
      float3 n = new float3(v0, v1, v2);
      Assert.That(n.x, Is.EqualTo(v0).Within(Tolerance));
      Assert.That(n.y, Is.EqualTo(v1).Within(Tolerance));
      Assert.That(n.z, Is.EqualTo(v2).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float3_GetIndices(float v0, float v1, float v2)
    {
      float3 n = new float3(v0, v1, v2);
      Assert.That(n[0], Is.EqualTo(v0).Within(Tolerance));
      Assert.That(n[1], Is.EqualTo(v1).Within(Tolerance));
      Assert.That(n[2], Is.EqualTo(v2).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float3_SetFields(float v0, float v1, float v2)
    {
      float3 n;
      n.x = v0;
      n.y = v1;
      n.z = v2;
      Assert.That(n.x, Is.EqualTo(v0).Within(Tolerance));
      Assert.That(n.y, Is.EqualTo(v1).Within(Tolerance));
      Assert.That(n.z, Is.EqualTo(v2).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float3_SetIndices(float v0, float v1, float v2)
    {
      float3 n = new float3();
      n[0] = v0;
      n[1] = v1;
      n[2] = v2;
      Assert.That(n[0], Is.EqualTo(v0).Within(Tolerance));
      Assert.That(n[1], Is.EqualTo(v1).Within(Tolerance));
      Assert.That(n[2], Is.EqualTo(v2).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float3_Add(float v0, float v1, float v2)
    {
      float3 n = new float3(v0, v1, v2);
      float3 x = new float3(v1, v2, v0);
      float3 nx = n + x;
      float3 n_expect = new float3(n[0] + x[0], n[1] + x[1], n[2] + x[2]);

      Assert.That(nx[0], Is.EqualTo(n_expect[0]).Within(Tolerance));
      Assert.That(nx[1], Is.EqualTo(n_expect[1]).Within(Tolerance));
      Assert.That(nx[2], Is.EqualTo(n_expect[2]).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float3_Subtract(float v0, float v1, float v2)
    {
      float3 n = new float3(v0, v1, v2);
      float3 x = new float3(v1, v2, v0);
      float3 nx = n - x;
      float3 n_expect = new float3(n[0] - x[0], n[1] - x[1], n[2] - x[2]);

      Assert.That(nx[0], Is.EqualTo(n_expect[0]).Within(Tolerance));
      Assert.That(nx[1], Is.EqualTo(n_expect[1]).Within(Tolerance));
      Assert.That(nx[2], Is.EqualTo(n_expect[2]).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float3_TermwiseMultiply(float v0, float v1, float v2)
    {
      float3 n = new float3(v0, v1, v2);
      float3 x = new float3(v1, v2, v0);
      float3 nx = n * x;
      float3 n_expect = new float3(n[0] * x[0], n[1] * x[1], n[2] * x[2]);

      Assert.That(nx[0], Is.EqualTo(n_expect[0]).Within(Tolerance));
      Assert.That(nx[1], Is.EqualTo(n_expect[1]).Within(Tolerance));
      Assert.That(nx[2], Is.EqualTo(n_expect[2]).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float3_ScalarMultiply(float v0, float v1, float v2)
    {
      float3 n = new float3(v0, v1, v2);
      float x = v0 + v1 + v2;
      float3 nx = n * x;
      float3 n_expect = new float3(n[0] * x, n[1] * x, n[2] * x);

      Assert.That(nx[0], Is.EqualTo(n_expect[0]).Within(Tolerance));
      Assert.That(nx[1], Is.EqualTo(n_expect[1]).Within(Tolerance));
      Assert.That(nx[2], Is.EqualTo(n_expect[2]).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float3_ParseFromString(float v0, float v1, float v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      float3 n = new float3(v0, v1, v2);
      float3 ns = float3.Parse(s);
      Assert.That(ns, Is.EqualTo(n).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float3_ParseFromString_ArbitarySpace(float v0, float v1, float v2)
    {
      foreach (string fmt in ArbitaryWhiteSpaceFormat)
      {
        string s = fmt.F(v0, v1, v2);
        float3 n = new float3(v0, v1, v2);
        float3 ns = float3.Parse(s);
        Assert.That(ns, Is.EqualTo(n).Within(Tolerance));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Float3_ParseFromString_NoBrackets(float v0, float v1, float v2)
    {
      string s = NoBracketFormat.F(v0, v1, v2);
      float3 n = new float3(v0, v1, v2);
      float3 ns = float3.Parse(s);
      Assert.That(ns, Is.EqualTo(n).Within(Tolerance));
    }

    [TestCaseSource(_NumberSource)]
    public void Float3_WriteToString(float v0, float v1, float v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      float3 n = new float3(v0, v1, v2);
      string sn = n.ToString();
      Assert.That(sn, Is.EqualTo(s));
    }

    [TestCaseSource(_NumberSource)]
    public void Float3_TryParse(float v0, float v1, float v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      float3 n = new float3(v0, v1, v2);
      float3 ns;
      Assert.IsTrue(float3.TryParse(s, out ns));
      Assert.That(ns, Is.EqualTo(n).Within(Tolerance));
    }

    [TestCaseSource(_InvalidStringSource)]
    public void Float3_TryParse_InvalidReturnsFalse(string invalid)
    {
      string s = invalid;
      float3 ns;
      Assert.IsFalse(float3.TryParse(s, out ns));
    }
  }
}
