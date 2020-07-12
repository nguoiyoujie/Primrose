using NUnit.Framework;
using Primrose.FileFormat.INI;
using Primrose.Primitives.Factories;
using Primrose.Primitives.ValueTypes;
using System;
using System.IO;
using System.Runtime;
using System.Text;

namespace Primrose.UnitTests.FileSystem.INI
{
  [TestFixture]
  public class INIFiles
  {
    static INIFiles()
    {
      Registry<int, MockINIInnerStruct> regstr = new Registry<int, MockINIInnerStruct>();
      regstr.Add(1, new MockINIInnerStruct(1, 2, 0, 0));
      regstr.Add(2, new MockINIInnerStruct(0, 0, 3, 4));
      regstr.Add(3, new MockINIInnerStruct(1, 0, 3, 0));
      regstr.Add(4, new MockINIInnerStruct(1, 0, 0, 4));
      ReadSubSectionRegistrySource = new object[]
      {
        new object[] { "TestSubSectionRegistryStruct", new MockINISubSectionRegistryStruct(){ INNER = regstr } }
      };
    }

    public const string _ReadAndMatchSource = "ReadAndMatchSource";
    public static object[] ReadAndMatchSource = new object[]
    {
      new object[] { null, new MockINIStruct() { BOOLEAN = true, INT = 1, INT4 = new int4(4, 5, 6, 7), FLOAT = 1.2f, FLOAT4 = new float4(23, 3.4f, 0.5f, .56f) } },
      new object[] { "Test1", new MockINIStruct() { SHORT = 3, SHORT2 = new short2(5, 7), SHORT3 = new short3(9, 11, 13), SHORT4 = new short4(15, 17, 19, 21) } },
      new object[] { "Test2", new MockINIStruct() { STRING = "This is one" } },
      new object[] { "Test3", new MockINIStruct() { ENUM = MockEnum.ONE | MockEnum.TWO } },
    };

    [TestCaseSource(_ReadAndMatchSource)]
    public void INIFile_ReadAndMatch(string section, MockINIStruct cmp)
    {
      INIFile f = new INIFile();
      using (Stream m = new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.INI_ReadAndMatch)))
      {
        f.ReadFromStream(m);
      }
      MockINIStruct mock = new MockINIStruct();
      f.LoadByAttribute(ref mock, section);

      Assert.That(mock, Is.EqualTo(cmp));

      INIFile fw = new INIFile();
      fw.UpdateByAttribute(ref mock, section);
      PrintToConsole(fw);
    }

    public const string _ReadSubSectionKeyListSource = "ReadSubSectionKeyListSource";
    public static object[] ReadSubSectionKeyListSource = new object[]
    {
      new object[] { "TestSubsectionKeyList", new MockINISubSectionKeyListStruct() { INNER = new MockINIInnerStruct[4]
      {
        new MockINIInnerStruct(1,2,0,0),
        new MockINIInnerStruct(0,0,3,4),
        new MockINIInnerStruct(1,0,3,0),
        new MockINIInnerStruct(1,0,0,4)
      } } },
      new object[] { null, new MockINISubSectionKeyListStruct() { INNER = new MockINIInnerStruct[4]
      {
        new MockINIInnerStruct(1,2,0,0),
        new MockINIInnerStruct(0,0,3,4),
        new MockINIInnerStruct(1,0,3,0),
        new MockINIInnerStruct(1,0,0,4)
      } } }
    };

    [TestCaseSource(_ReadSubSectionKeyListSource)]
    public void INIFile_ReadSubSectionKeyList(string section, MockINISubSectionKeyListStruct cmp)
    {
      INIFile f = new INIFile();
      using (Stream m = new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.INI_ReadSubSections)))
      {
        f.ReadFromStream(m);
      }
      MockINISubSectionKeyListStruct mock = new MockINISubSectionKeyListStruct();
      f.LoadByAttribute(ref mock, section);

      Assert.Multiple(() =>
      {
        Assert.That(mock.INNER, Is.Not.Null);
        Assert.That(mock.INNER.Length, Is.Not.EqualTo(0));
        Assert.That(mock.INNER.Length, Is.EqualTo(cmp.INNER.Length));
        for (int i = 0; i < cmp.INNER.Length; i++)
        {
          Assert.That(mock.INNER[i], Is.EqualTo(cmp.INNER[i]));
        }
      });

      INIFile fw = new INIFile();
      fw.UpdateByAttribute(ref mock, section);
      PrintToConsole(fw);
    }

    public const string _ReadSubSectionListSource = "ReadSubSectionListSource";
    public static object[] ReadSubSectionListSource = new object[]
    {
      new object[] { "TestSubsectionList", new MockINISubSectionKeyListStruct() { INNER = new MockINIInnerStruct[4]
      {
        new MockINIInnerStruct(1,2,0,0),
        new MockINIInnerStruct(0,0,3,4),
        new MockINIInnerStruct(1,0,3,0),
        new MockINIInnerStruct(1,0,0,4)
      } } },
      new object[] { null, new MockINISubSectionKeyListStruct() { INNER = new MockINIInnerStruct[4]
      {
        new MockINIInnerStruct(1,2,0,0),
        new MockINIInnerStruct(0,0,3,4),
        new MockINIInnerStruct(1,0,3,0),
        new MockINIInnerStruct(1,0,0,4)
      } } }
    };

    [TestCaseSource(_ReadSubSectionListSource)]
    public void INIFile_ReadSubSectionList(string section, MockINISubSectionKeyListStruct cmp)
    {
      INIFile f = new INIFile();
      using (Stream m = new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.INI_ReadSubSections)))
      {
        f.ReadFromStream(m);
      }
      MockINISubSectionListStruct mock = new MockINISubSectionListStruct();
      f.LoadByAttribute(ref mock, section);

      Assert.Multiple(() =>
      {
        Assert.That(mock.INNER, Is.Not.Null);
        Assert.That(mock.INNER.Length, Is.Not.EqualTo(0));
        Assert.That(mock.INNER.Length, Is.EqualTo(cmp.INNER.Length));
        for (int i = 0; i < cmp.INNER.Length; i++)
        {
          Assert.That(mock.INNER[i], Is.EqualTo(cmp.INNER[i]));
        }
      });

      INIFile fw = new INIFile();
      fw.UpdateByAttribute(ref mock, section);
      PrintToConsole(fw);
    }


    public const string _ReadSubSectionRegistrySource = "ReadSubSectionRegistrySource";
    public static object[] ReadSubSectionRegistrySource;

    [TestCaseSource(_ReadSubSectionRegistrySource)]
    public void INIFile_ReadSubSectionRegistry(string section, MockINISubSectionRegistryStruct cmp)
    {
      INIFile f = new INIFile();
      using (Stream m = new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.INI_ReadSubSections)))
      {
        f.ReadFromStream(m);
      }
      MockINISubSectionRegistryStruct mock = new MockINISubSectionRegistryStruct();
      f.LoadByAttribute(ref mock, section);

      Assert.Multiple(() =>
      {
        Assert.That(mock.INNER, Is.Not.Null);
        Assert.That(mock.INNER.Count, Is.Not.EqualTo(0));
        Assert.That(mock.INNER.Count, Is.EqualTo(cmp.INNER.Count));
        for (int i = 0; i < cmp.INNER.Count; i++)
        {
          Assert.That(mock.INNER[i], Is.EqualTo(cmp.INNER[i]));
        }
      });

      INIFile fw = new INIFile();
      fw.UpdateByAttribute(ref mock, section);
      PrintToConsole(fw);
    }

    private void PrintToConsole(INIFile f)
    {
      Console.WriteLine("Mock structure");
      Console.WriteLine("--------------------------------------------");

      using (MemoryStream m = new MemoryStream(1000))
      {
        using (StreamWriter sw = new StreamWriter(m))
        {
          f.WriteToStream(sw);
          sw.Flush();
          Console.WriteLine(Encoding.UTF8.GetString(m.GetBuffer(), 0, (int)m.Length));
        }
      }
    }
  }
}
