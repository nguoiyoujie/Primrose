﻿using NUnit.Framework;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.ValueTypes
{
  [TestFixture]
  public class Short4
  {
    private const string _NumberSource = "NumberSource";
    private static object[] NumberSource =
    {
      new object[] { (short)0, (short)0, (short)0, (short)0 },
      new object[] { (short)-45, (short)6, (short)15, (short)25},
      new object[] { (short)37, (short)-117, (short)53, (short)75},
      new object[] { (short)2, (short)58, (short)-233, (short)-100},
      new object[] { (short)-128, (short)-170, (short)-69, (short)-86},
      new object[] { (short)121, (short)11, (short)1, (short)111},
      new object[] { short.MinValue, short.MinValue, short.MinValue, short.MinValue }, 
      new object[] { short.MaxValue, short.MaxValue, short.MaxValue, short.MaxValue }
    };

    private const string _InvalidStringSource = "InvalidStringSource";
    private static object[] InvalidStringSource =
    {
      new object[] { "{66666, 0, 0, 0}" },
      new object[] { "{0, -66666, 0, 0}" },
      new object[] { "{0, 0, -66666, 0}" },
      new object[] { "{0, 0, 0, 66666}" },
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
    public void Short4_GetFields(short v0, short v1, short v2, short v3)
    {
      short4 n = new short4(v0, v1, v2, v3);
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
      Assert.That(n.z, Is.EqualTo(v2));
      Assert.That(n.w, Is.EqualTo(v3));
    }

    [TestCaseSource(_NumberSource)]
    public void Short4_GetIndices(short v0, short v1, short v2, short v3)
    {
      short4 n = new short4(v0, v1, v2, v3);
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
      Assert.That(n[2], Is.EqualTo(v2));
      Assert.That(n[3], Is.EqualTo(v3));
    }

    [TestCaseSource(_NumberSource)]
    public void Short4_SetFields(short v0, short v1, short v2, short v3)
    {
      short4 n;
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
    public void Short4_SetIndices(short v0, short v1, short v2, short v3)
    {
      short4 n = new short4();
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
    public void Short4_ParseFromString(short v0, short v1, short v2, short v3)
    {
      string s = NormalFormat.F(v0, v1, v2, v3);
      short4 n = new short4(v0, v1, v2, v3);
      short4 ns = short4.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Short4_ParseFromString_ArbitarySpace(short v0, short v1, short v2, short v3)
    {
      foreach (string fmt in ArbitaryWhiteSpaceFormat)
      {
        string s = fmt.F(v0, v1, v2, v3);
        short4 n = new short4(v0, v1, v2, v3);
        short4 ns = short4.Parse(s);
        Assert.That(ns, Is.EqualTo(n));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Short4_ParseFromString_NoBrackets(short v0, short v1, short v2, short v3)
    {
      string s = NoBracketFormat.F(v0, v1, v2, v3);
      short4 n = new short4(v0, v1, v2, v3);
      short4 ns = short4.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Short4_WriteToString(short v0, short v1, short v2, short v3)
    {
      string s = NormalFormat.F(v0, v1, v2, v3);
      short4 n = new short4(v0, v1, v2, v3);
      string sn = n.ToString();
      Assert.That(sn, Is.EqualTo(s));
    }

    [TestCaseSource(_NumberSource)]
    public void Short4_TryParse(short v0, short v1, short v2, short v3)
    {
      string s = NormalFormat.F(v0, v1, v2, v3);
      short4 n = new short4(v0, v1, v2, v3);
      short4 ns;
      Assert.IsTrue(short4.TryParse(s, out ns));
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_InvalidStringSource)]
    public void Short4_TryParse_InvalidReturnsFalse(string invalid)
    {
      string s = invalid;
      short4 ns;
      Assert.IsFalse(short4.TryParse(s, out ns));
    }
  }
}
