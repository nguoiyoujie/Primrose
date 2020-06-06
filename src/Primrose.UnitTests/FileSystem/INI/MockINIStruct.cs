using Primitives.FileFormat.INI;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.FileSystem.INI
{
  public enum MockEnum
  {
    NONE = 0,
    ONE = 1,
    TWO = 2,
    THREE = 4
  }

  public struct MockINIInnerStruct
  {
    [INIValue]
    public int VAL1;

    [INIValue]
    public int VAL2;

    [INIValue]
    public int VAL3;

    [INIValue]
    public int VAL4;

    public MockINIInnerStruct(int v1, int v2, int v3, int v4)
    {
      VAL1 = v1;
      VAL2 = v2;
      VAL3 = v3;
      VAL4 = v4;
    }
  }

  public struct MockINISubSectionListStruct
  {
    [INISubSectionList("I")]
    public MockINIInnerStruct[] INNER;
  }

  public struct MockINISubSectionKeyListStruct
  {
    [INISubSectionKeyList("I")]
    public MockINIInnerStruct[] INNER;
  }

  public struct MockINIStruct
  {
    [INIValue]
    public MockEnum ENUM;

    [INIValue]
    public bool BOOLEAN;

    [INIValue]
    public byte BYTE;

    [INIValue]
    public byte2 BYTE2;

    [INIValue]
    public byte3 BYTE3;

    [INIValue]
    public byte4 BYTE4;

    [INIValue]
    public short SHORT;

    [INIValue]
    public short2 SHORT2;

    [INIValue]
    public short3 SHORT3;

    [INIValue]
    public short4 SHORT4;

    [INIValue]
    public int INT;

    [INIValue]
    public int2 INT2;

    [INIValue]
    public int3 INT3;

    [INIValue]
    public int4 INT4;

    [INIValue]
    public float FLOAT;

    [INIValue]
    public float2 FLOAT2;

    [INIValue]
    public float3 FLOAT3;

    [INIValue]
    public float4 FLOAT4;

    [INIValue]
    public string STRING;
  }
}
