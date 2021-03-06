﻿using System;
using Primrose.Primitives.Extensions;
using Primrose.Expressions.UnitTests.Scripting;
using System.IO;
using NUnit.Framework;

namespace Primrose.Expressions.UnitTests
{
  [TestFixture]
  public class Script_Test
  {
    [Test]
    public void Script_Simulation()
    {
      Context c = new Context();
      string test_dir = Path.Combine(TestContext.CurrentContext.TestDirectory, "./Scripts");

      foreach (string sfile in Directory.GetFiles(test_dir))
      {
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("loading script file: ".C(Path.GetFileName(sfile)));

        c.Reset();
        ScriptFile f = new ScriptFile(c)
        {
          ScriptReadBegin = ReadScript
        };
        f.ReadFromFile(sfile);
        Console.WriteLine();

        c.Scripts.Global.Run(c);
        Console.WriteLine("global content");
        Console.WriteLine();
        Console.WriteLine(c.Scripts.Global.Write());
        Console.WriteLine();

        foreach (Script s in c.Scripts.GetAll())
        {
          Console.WriteLine("running script: ".C(s.Name));
          s.Run(c);
          Console.WriteLine("script content");
          Console.WriteLine();
          Console.WriteLine(s.Write());
          Console.WriteLine();
          Console.WriteLine("script ".C(s.Name, "... OK!"));
          Console.WriteLine();
        }
        Console.WriteLine("script file complete!");
        Console.WriteLine();
      }
    }

    public void ReadScript(string name)
    {
      Console.WriteLine("loading script: ".C(name));
    }
  }
}
