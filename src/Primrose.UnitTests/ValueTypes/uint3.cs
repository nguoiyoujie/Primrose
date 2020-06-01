using NUnit.Framework;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.ValueTypes
{
  [TestFixture]
  public class UInt3
  {
    private const string _NumberSource = "NumberSource";
    private static object[] NumberSource =
    {
      new object[] { (uint)0, (uint)0, (uint)0 },
      new object[] { (uint)45, (uint)6, (uint)15 },
      new object[] { (uint)37, (uint)117, (uint)53 },
      new object[] { (uint)2, (uint)58, (uint)233 },
      new object[] { (uint)128, (uint)170, (uint)69 },
      new object[] { (uint)121, (uint)11, (uint)1 },
      new object[] { uint.MinValue, uint.MinValue, uint.MinValue  },
      new object[] { uint.MaxValue, uint.MaxValue, uint.MaxValue }
    };

    private const string _InvalidStringSource = "InvalidStringSource";
    private static object[] InvalidStringSource =
    {
      new object[] { "{-1, 0, 0}" },
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
    public void UInt3_GetFields(uint v0, uint v1, uint v2)
    {
      uint3 n = new uint3(v0, v1, v2);
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
      Assert.That(n.z, Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void UInt3_GetIndices(uint v0, uint v1, uint v2)
    {
      uint3 n = new uint3(v0, v1, v2);
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
      Assert.That(n[2], Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void UInt3_SetFields(uint v0, uint v1, uint v2)
    {
      uint3 n;
      n.x = v0;
      n.y = v1;
      n.z = v2;
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
      Assert.That(n.z, Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void UInt3_SetIndices(uint v0, uint v1, uint v2)
    {
      uint3 n = new uint3();
      n[0] = v0;
      n[1] = v1;
      n[2] = v2;
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
      Assert.That(n[2], Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void UInt3_ParseFromString(uint v0, uint v1, uint v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      uint3 n = new uint3(v0, v1, v2);
      uint3 ns = uint3.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void UInt3_ParseFromString_ArbitarySpace(uint v0, uint v1, uint v2)
    {
      foreach (string fmt in ArbitaryWhiteSpaceFormat)
      {
        string s = fmt.F(v0, v1, v2);
        uint3 n = new uint3(v0, v1, v2);
        uint3 ns = uint3.Parse(s);
        Assert.That(ns, Is.EqualTo(n));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void UInt3_ParseFromString_NoBrackets(uint v0, uint v1, uint v2)
    {
      string s = NoBracketFormat.F(v0, v1, v2);
      uint3 n = new uint3(v0, v1, v2);
      uint3 ns = uint3.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void UInt3_WriteToString(uint v0, uint v1, uint v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      uint3 n = new uint3(v0, v1, v2);
      string sn = n.ToString();
      Assert.That(sn, Is.EqualTo(s));
    }

    [TestCaseSource(_NumberSource)]
    public void UInt3_TryParse(uint v0, uint v1, uint v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      uint3 n = new uint3(v0, v1, v2);
      uint3 ns;
      Assert.IsTrue(uint3.TryParse(s, out ns));
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_InvalidStringSource)]
    public void UInt3_TryParse_InvalidReturnsFalse(string invalid)
    {
      string s = invalid;
      uint3 ns;
      Assert.IsFalse(uint3.TryParse(s, out ns));
    }
  }
}
