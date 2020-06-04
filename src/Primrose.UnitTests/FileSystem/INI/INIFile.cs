using NUnit.Framework;
using Primitives.FileFormat.INI;
using Primrose.Primitives.ValueTypes;
using System.IO;
using System.Text;

namespace Primrose.UnitTests.FileSystem.INI
{
  [TestFixture]
  public class INIFiles
  {
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
    }
  }
}
