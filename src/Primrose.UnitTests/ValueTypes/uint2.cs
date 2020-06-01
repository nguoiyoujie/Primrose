using NUnit.Framework;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.ValueTypes
{
  [TestFixture]
  public class UInt2
  {
    private const string _NumberSource = "NumberSource";
    private static object[] NumberSource =
    {
      new object[] { (uint)0, (uint)0 },
      new object[] { (uint)45, (uint)6 },
      new object[] { (uint)37, (uint)117 },
      new object[] { (uint)2, (uint)58 },
      new object[] { (uint)128, (uint)170 },
      new object[] { (uint)121, (uint)11 },
      new object[] { uint.MinValue, uint.MinValue },
      new object[] { uint.MaxValue, uint.MaxValue }
    };

    private const string _InvalidStringSource = "InvalidStringSource";
    private static object[] InvalidStringSource =
    {
      new object[] { "{-1, 0}" },
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
    public void UInt2_GetFields(uint v0, uint v1)
    {
      uint2 n = new uint2(v0, v1);
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void UInt2_GetIndices(uint v0, uint v1)
    {
      uint2 n = new uint2(v0, v1);
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void UInt2_SetFields(uint v0, uint v1)
    {
      uint2 n;
      n.x = v0;
      n.y = v1;
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void UInt2_SetIndices(uint v0, uint v1)
    {
      uint2 n = new uint2();
      n[0] = v0;
      n[1] = v1;
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void UInt2_ParseFromString(uint v0, uint v1)
    {
      string s = NormalFormat.F(v0, v1);
      uint2 n = new uint2(v0, v1);
      uint2 ns = uint2.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void UInt2_ParseFromString_ArbitarySpace(uint v0, uint v1)
    {
      foreach (string fmt in ArbitaryWhiteSpaceFormat)
      {
        string s = fmt.F(v0, v1);
        uint2 n = new uint2(v0, v1);
        uint2 ns = uint2.Parse(s);
        Assert.That(ns, Is.EqualTo(n));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void UInt2_ParseFromString_NoBrackets(uint v0, uint v1)
    {
      string s = NoBracketFormat.F(v0, v1);
      uint2 n = new uint2(v0, v1);
      uint2 ns = uint2.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void UInt2_WriteToString(uint v0, uint v1)
    {
      string s = NormalFormat.F(v0, v1);
      uint2 n = new uint2(v0, v1);
      string sn = n.ToString();
      Assert.That(sn, Is.EqualTo(s));
    }

    [TestCaseSource(_NumberSource)]
    public void UInt2_TryParse(uint v0, uint v1)
    {
      string s = NormalFormat.F(v0, v1);
      uint2 n = new uint2(v0, v1);
      uint2 ns;
      Assert.IsTrue(uint2.TryParse(s, out ns));
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_InvalidStringSource)]
    public void UInt2_TryParse_InvalidReturnsFalse(string invalid)
    {
      string s = invalid;
      uint2 ns;
      Assert.IsFalse(uint2.TryParse(s, out ns));
    }
  }
}
