using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.ValueTypes;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Primrose.Expressions
{
  /// <summary>
  /// A value holder for reference types.
  /// </summary>
  /// <comment>
  /// Since the Val object that contains this struct is internal for use as the expression value only, 
  /// it should be constrained to supported types. If the expression tree is to support generics, then
  /// swtiching to a single 'object' field
  /// </comment>
  [StructLayout(LayoutKind.Explicit, Size = 8)]
  internal struct ValObj
  {
    [FieldOffset(0)]
    internal readonly string vS;
    [FieldOffset(0)]
    internal readonly bool[] aB;
    [FieldOffset(0)]
    internal readonly int[] aI;
    [FieldOffset(0)]
    internal readonly float[] aF;
    [FieldOffset(0)]
    internal readonly string[] aS;

    public ValObj(string val)
    {
      aB = null;
      aI = null;
      aF = null;
      aS = null;
      vS = val ?? string.Empty;
    }

    public ValObj(float2 val)
    {
      vS = null;
      aB = null;
      aI = null;
      aS = null;
      aF = val.ToArray();
    }

    public ValObj(float3 val)
    {
      vS = null;
      aB = null;
      aI = null;
      aS = null;
      aF = val.ToArray();
    }

    public ValObj(float4 val)
    {
      vS = null;
      aB = null;
      aI = null;
      aS = null;
      aF = val.ToArray();
    }

    public ValObj(bool[] val)
    {
      vS = null;
      aI = null;
      aF = null;
      aS = null;
      aB = val;
    }

    public ValObj(int[] val)
    {
      vS = null;
      aB = null;
      aF = null;
      aS = null;
      aI = val;
    }

    public ValObj(float[] val)
    {
      vS = null;
      aB = null;
      aI = null;
      aS = null;
      aF = val;
    }

    public ValObj(string[] val)
    {
      vS = null;
      aB = null;
      aI = null;
      aF = null;
      aS = val;
    }
  }

  /// <summary>
  /// A value holder for 32-bit primitive types.
  /// </summary>
  [StructLayout(LayoutKind.Explicit)]
  internal struct ValPrim
  {
    // 1-byte
    [FieldOffset(0)]
    internal readonly bool v_bool;
    [FieldOffset(0)]
    internal readonly byte v_byte;
    [FieldOffset(0)]
    internal readonly char v_char;
    [FieldOffset(0)]
    internal readonly sbyte v_sbyte;

    // 2-bytes
    [FieldOffset(0)]
    internal readonly short v_short;
    [FieldOffset(0)]
    internal readonly ushort v_ushort;

    // 4-bytes
    [FieldOffset(0)]
    internal readonly int v_int;
    [FieldOffset(0)]
    internal readonly uint v_uint;
    [FieldOffset(0)]
    internal readonly float v_float;

    [FieldOffset(4)]
    internal readonly Type Type;

    public ValPrim(Type type) 
    { 
      this = new ValPrim();
      if (type == typeof(bool) 
       || type == typeof(byte)
       || type == typeof(char)
       || type == typeof(sbyte)
       || type == typeof(short)
       || type == typeof(ushort)
       || type == typeof(int)
       || type == typeof(uint) 
       || type == typeof(float))
      {
        Type = type;
      }
    }

    public ValPrim(bool val) { this = new ValPrim(); v_bool = val; Type = typeof(bool); }
    public ValPrim(byte val) { this = new ValPrim(); v_byte = val; Type = typeof(byte); }
    public ValPrim(char val) { this = new ValPrim(); v_char = val; Type = typeof(char); }
    public ValPrim(sbyte val) { this = new ValPrim(); v_sbyte = val; Type = typeof(sbyte); }
    public ValPrim(short val) { this = new ValPrim(); v_short = val; Type = typeof(short); }
    public ValPrim(ushort val) { this = new ValPrim(); v_ushort = val; Type = typeof(ushort); }
    public ValPrim(int val) { this = new ValPrim(); v_int = val; Type = typeof(int); }
    public ValPrim(uint val) { this = new ValPrim(); v_uint = val; Type = typeof(uint); }
    public ValPrim(float val) { this = new ValPrim(); v_float = val; Type = typeof(float); }

    public T Get<T>()
    {
      if (Type == typeof(bool)) { return UnaryOp<bool>.Cast<T>(v_bool); }
      else if (Type == typeof(byte)) { return UnaryOp<byte>.Cast<T>(v_byte); }
      else if (Type == typeof(char)) { return UnaryOp<byte>.Cast<T>(v_byte); }
      else if (Type == typeof(sbyte)) { return UnaryOp<sbyte>.Cast<T>(v_sbyte); }
      else if (Type == typeof(short)) { return UnaryOp<short>.Cast<T>(v_short); }
      else if (Type == typeof(ushort)) { return UnaryOp<ushort>.Cast<T>(v_ushort); }
      else if (Type == typeof(int)) { return UnaryOp<int>.Cast<T>(v_int); }
      else if (Type == typeof(uint)) { return UnaryOp<uint>.Cast<T>(v_uint); }
      else if (Type == typeof(float)) { return UnaryOp<float>.Cast<T>(v_float); }
      else return default;
    }
  }

  /// <summary>
  /// Represents a script value
  /// </summary>
  [StructLayout(LayoutKind.Explicit)]
  public struct Val
  {
    [FieldOffset(0)]
    private readonly Type _type;
    [FieldOffset(4)]
    private readonly object _obj; // for reference types
    [FieldOffset(8)]
    private readonly ValPrim _val; // size 8

    /// <summary>Retrieves the type of the value</summary>
    public Type Type { get => _val.Type ?? _type; }

    private static readonly Registry<Pair<Type, Type>, object> val_cast_func = new Registry<Pair<Type, Type>, object>();
    private static readonly Registry<Pair<Type, Type>, object> obj_cast_func = new Registry<Pair<Type, Type>, object>();

    static Val()
    {
      // Output type, Input type 
      // bool
      val_cast_func.Add(new Pair<Type, Type>(typeof(bool), typeof(bool)), new Func<Val, bool>((v) => v._val.v_bool));

      // byte
      val_cast_func.Add(new Pair<Type, Type>(typeof(byte), typeof(byte)), new Func<Val, byte>((v) => v._val.v_byte));
      val_cast_func.Add(new Pair<Type, Type>(typeof(sbyte), typeof(byte)), new Func<Val, sbyte>((v) => (sbyte)v._val.v_byte));
      val_cast_func.Add(new Pair<Type, Type>(typeof(short), typeof(byte)), new Func<Val, short>((v) => v._val.v_byte));
      val_cast_func.Add(new Pair<Type, Type>(typeof(ushort), typeof(byte)), new Func<Val, ushort>((v) => v._val.v_byte));
      val_cast_func.Add(new Pair<Type, Type>(typeof(int), typeof(byte)), new Func<Val, int>((v) => v._val.v_byte));
      val_cast_func.Add(new Pair<Type, Type>(typeof(uint), typeof(byte)), new Func<Val, uint>((v) => v._val.v_byte));
      val_cast_func.Add(new Pair<Type, Type>(typeof(float), typeof(byte)), new Func<Val, float>((v) => v._val.v_byte));

      // sbyte
      val_cast_func.Add(new Pair<Type, Type>(typeof(byte), typeof(sbyte)), new Func<Val, byte>((v) => (byte)v._val.v_sbyte));
      val_cast_func.Add(new Pair<Type, Type>(typeof(sbyte), typeof(sbyte)), new Func<Val, sbyte>((v) => v._val.v_sbyte));
      val_cast_func.Add(new Pair<Type, Type>(typeof(short), typeof(sbyte)), new Func<Val, short>((v) => v._val.v_sbyte));
      val_cast_func.Add(new Pair<Type, Type>(typeof(ushort), typeof(sbyte)), new Func<Val, ushort>((v) => (ushort)v._val.v_sbyte));
      val_cast_func.Add(new Pair<Type, Type>(typeof(int), typeof(sbyte)), new Func<Val, int>((v) => v._val.v_sbyte));
      val_cast_func.Add(new Pair<Type, Type>(typeof(uint), typeof(sbyte)), new Func<Val, uint>((v) => (uint)v._val.v_sbyte));
      val_cast_func.Add(new Pair<Type, Type>(typeof(float), typeof(sbyte)), new Func<Val, float>((v) => v._val.v_sbyte));

      // short
      val_cast_func.Add(new Pair<Type, Type>(typeof(byte), typeof(short)), new Func<Val, byte>((v) => (byte)v._val.v_short));
      val_cast_func.Add(new Pair<Type, Type>(typeof(sbyte), typeof(short)), new Func<Val, sbyte>((v) => (sbyte)v._val.v_short));
      val_cast_func.Add(new Pair<Type, Type>(typeof(short), typeof(short)), new Func<Val, short>((v) => v._val.v_short));
      val_cast_func.Add(new Pair<Type, Type>(typeof(ushort), typeof(short)), new Func<Val, ushort>((v) => (ushort)v._val.v_short));
      val_cast_func.Add(new Pair<Type, Type>(typeof(int), typeof(short)), new Func<Val, int>((v) => v._val.v_short));
      val_cast_func.Add(new Pair<Type, Type>(typeof(uint), typeof(short)), new Func<Val, uint>((v) => (uint)v._val.v_short));
      val_cast_func.Add(new Pair<Type, Type>(typeof(float), typeof(short)), new Func<Val, float>((v) => v._val.v_short));

      // ushort
      val_cast_func.Add(new Pair<Type, Type>(typeof(byte), typeof(ushort)), new Func<Val, byte>((v) => (byte)v._val.v_ushort));
      val_cast_func.Add(new Pair<Type, Type>(typeof(sbyte), typeof(ushort)), new Func<Val, sbyte>((v) => (sbyte)v._val.v_ushort));
      val_cast_func.Add(new Pair<Type, Type>(typeof(short), typeof(ushort)), new Func<Val, short>((v) => (short)v._val.v_ushort));
      val_cast_func.Add(new Pair<Type, Type>(typeof(ushort), typeof(ushort)), new Func<Val, ushort>((v) => v._val.v_ushort));
      val_cast_func.Add(new Pair<Type, Type>(typeof(int), typeof(ushort)), new Func<Val, int>((v) => v._val.v_ushort));
      val_cast_func.Add(new Pair<Type, Type>(typeof(uint), typeof(ushort)), new Func<Val, uint>((v) => v._val.v_ushort));
      val_cast_func.Add(new Pair<Type, Type>(typeof(float), typeof(ushort)), new Func<Val, float>((v) => v._val.v_ushort));

      // int
      val_cast_func.Add(new Pair<Type, Type>(typeof(byte), typeof(int)), new Func<Val, byte>((v) => (byte)v._val.v_int));
      val_cast_func.Add(new Pair<Type, Type>(typeof(sbyte), typeof(int)), new Func<Val, sbyte>((v) => (sbyte)v._val.v_int));
      val_cast_func.Add(new Pair<Type, Type>(typeof(short), typeof(int)), new Func<Val, short>((v) => (short)v._val.v_int));
      val_cast_func.Add(new Pair<Type, Type>(typeof(ushort), typeof(int)), new Func<Val, ushort>((v) => (ushort)v._val.v_int));
      val_cast_func.Add(new Pair<Type, Type>(typeof(int), typeof(int)), new Func<Val, int>((v) => v._val.v_int));
      val_cast_func.Add(new Pair<Type, Type>(typeof(uint), typeof(int)), new Func<Val, uint>((v) => (uint)v._val.v_int));
      val_cast_func.Add(new Pair<Type, Type>(typeof(float), typeof(int)), new Func<Val, float>((v) => v._val.v_int));

      // uint
      val_cast_func.Add(new Pair<Type, Type>(typeof(byte), typeof(uint)), new Func<Val, byte>((v) => (byte)v._val.v_uint));
      val_cast_func.Add(new Pair<Type, Type>(typeof(sbyte), typeof(uint)), new Func<Val, sbyte>((v) => (sbyte)v._val.v_uint));
      val_cast_func.Add(new Pair<Type, Type>(typeof(short), typeof(uint)), new Func<Val, short>((v) => (short)v._val.v_uint));
      val_cast_func.Add(new Pair<Type, Type>(typeof(ushort), typeof(uint)), new Func<Val, ushort>((v) => (ushort)v._val.v_uint));
      val_cast_func.Add(new Pair<Type, Type>(typeof(int), typeof(uint)), new Func<Val, int>((v) => (int)v._val.v_uint));
      val_cast_func.Add(new Pair<Type, Type>(typeof(uint), typeof(uint)), new Func<Val, uint>((v) => v._val.v_uint));
      val_cast_func.Add(new Pair<Type, Type>(typeof(float), typeof(uint)), new Func<Val, float>((v) => v._val.v_uint));

      // float
      val_cast_func.Add(new Pair<Type, Type>(typeof(byte), typeof(float)), new Func<Val, byte>((v) => (byte)v._val.v_float));
      val_cast_func.Add(new Pair<Type, Type>(typeof(sbyte), typeof(float)), new Func<Val, sbyte>((v) => (sbyte)v._val.v_float));
      val_cast_func.Add(new Pair<Type, Type>(typeof(short), typeof(float)), new Func<Val, short>((v) => (short)v._val.v_float));
      val_cast_func.Add(new Pair<Type, Type>(typeof(ushort), typeof(float)), new Func<Val, ushort>((v) => (ushort)v._val.v_float));
      val_cast_func.Add(new Pair<Type, Type>(typeof(int), typeof(float)), new Func<Val, int>((v) => (int)v._val.v_float));
      val_cast_func.Add(new Pair<Type, Type>(typeof(uint), typeof(float)), new Func<Val, uint>((v) => (uint)v._val.v_float));
      val_cast_func.Add(new Pair<Type, Type>(typeof(float), typeof(float)), new Func<Val, float>((v) => v._val.v_float));

      // byte[] <-> byteX
      obj_cast_func.Add(new Pair<Type, Type>(typeof(byte2), typeof(byte[])), new Func<object, byte2>((o) => byte2.FromArray((byte[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(byte3), typeof(byte[])), new Func<object, byte3>((o) => byte3.FromArray((byte[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(byte4), typeof(byte[])), new Func<object, byte4>((o) => byte4.FromArray((byte[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(byte[]), typeof(byte2)), new Func<object, byte[]>((o) => ((byte2)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(byte[]), typeof(byte3)), new Func<object, byte[]>((o) => ((byte3)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(byte[]), typeof(byte4)), new Func<object, byte[]>((o) => ((byte4)o).ToArray()));

      // sbyte[] <-> sbyteX
      obj_cast_func.Add(new Pair<Type, Type>(typeof(sbyte2), typeof(sbyte[])), new Func<object, sbyte2>((o) => sbyte2.FromArray((sbyte[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(sbyte3), typeof(sbyte[])), new Func<object, sbyte3>((o) => sbyte3.FromArray((sbyte[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(sbyte4), typeof(sbyte[])), new Func<object, sbyte4>((o) => sbyte4.FromArray((sbyte[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(sbyte[]), typeof(sbyte2)), new Func<object, sbyte[]>((o) => ((sbyte2)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(sbyte[]), typeof(sbyte3)), new Func<object, sbyte[]>((o) => ((sbyte3)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(sbyte[]), typeof(sbyte4)), new Func<object, sbyte[]>((o) => ((sbyte4)o).ToArray()));

      // short[] <-> shortX
      obj_cast_func.Add(new Pair<Type, Type>(typeof(short2), typeof(short[])), new Func<object, short2>((o) => short2.FromArray((short[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(short3), typeof(short[])), new Func<object, short3>((o) => short3.FromArray((short[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(short4), typeof(short[])), new Func<object, short4>((o) => short4.FromArray((short[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(short[]), typeof(short2)), new Func<object, short[]>((o) => ((short2)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(short[]), typeof(short3)), new Func<object, short[]>((o) => ((short3)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(short[]), typeof(short4)), new Func<object, short[]>((o) => ((short4)o).ToArray()));

      // ushort[] <-> ushortX
      obj_cast_func.Add(new Pair<Type, Type>(typeof(ushort2), typeof(ushort[])), new Func<object, ushort2>((o) => ushort2.FromArray((ushort[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(ushort3), typeof(ushort[])), new Func<object, ushort3>((o) => ushort3.FromArray((ushort[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(ushort4), typeof(ushort[])), new Func<object, ushort4>((o) => ushort4.FromArray((ushort[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(ushort[]), typeof(ushort2)), new Func<object, ushort[]>((o) => ((ushort2)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(ushort[]), typeof(ushort3)), new Func<object, ushort[]>((o) => ((ushort3)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(ushort[]), typeof(ushort4)), new Func<object, ushort[]>((o) => ((ushort4)o).ToArray()));

      // int[] <-> intX
      obj_cast_func.Add(new Pair<Type, Type>(typeof(int2), typeof(int[])), new Func<object, int2>((o) => int2.FromArray((int[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(int3), typeof(int[])), new Func<object, int3>((o) => int3.FromArray((int[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(int4), typeof(int[])), new Func<object, int4>((o) => int4.FromArray((int[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(int[]), typeof(int2)), new Func<object, int[]>((o) => ((int2)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(int[]), typeof(int3)), new Func<object, int[]>((o) => ((int3)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(int[]), typeof(int4)), new Func<object, int[]>((o) => ((int4)o).ToArray()));

      // uint[] <-> uintX
      obj_cast_func.Add(new Pair<Type, Type>(typeof(uint2), typeof(uint[])), new Func<object, uint2>((o) => uint2.FromArray((uint[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(uint3), typeof(uint[])), new Func<object, uint3>((o) => uint3.FromArray((uint[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(uint4), typeof(uint[])), new Func<object, uint4>((o) => uint4.FromArray((uint[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(uint[]), typeof(uint2)), new Func<object, uint[]>((o) => ((uint2)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(uint[]), typeof(uint3)), new Func<object, uint[]>((o) => ((uint3)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(uint[]), typeof(uint4)), new Func<object, uint[]>((o) => ((uint4)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(Array), typeof(uint2)), new Func<object, Array>((o) => ((uint2)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(Array), typeof(uint3)), new Func<object, Array>((o) => ((uint3)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(Array), typeof(uint4)), new Func<object, Array>((o) => ((uint4)o).ToArray()));

      // float[] <-> floatX
      obj_cast_func.Add(new Pair<Type, Type>(typeof(float2), typeof(float[])), new Func<object, float2>((o) => float2.FromArray((float[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(float3), typeof(float[])), new Func<object, float3>((o) => float3.FromArray((float[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(float4), typeof(float[])), new Func<object, float4>((o) => float4.FromArray((float[])o)));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(float[]), typeof(float2)), new Func<object, float[]>((o) => ((float2)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(float[]), typeof(float3)), new Func<object, float[]>((o) => ((float3)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(float[]), typeof(float4)), new Func<object, float[]>((o) => ((float4)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(Array), typeof(float2)), new Func<object, Array>((o) => ((float2)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(Array), typeof(float3)), new Func<object, Array>((o) => ((float3)o).ToArray()));
      obj_cast_func.Add(new Pair<Type, Type>(typeof(Array), typeof(float4)), new Func<object, Array>((o) => ((float4)o).ToArray()));
    }

    /// <summary>Retrieves the value and casts it as a <typeparamref name="T"/></summary>
    public T Cast<T>()
    {
      Type t_out = typeof(T);
      Type t_in = Type;
      if (t_out == typeof(object)) 
      {
        t_out = t_in;
      }

      Pair<Type, Type> p = new Pair<Type, Type>(t_out, t_in);

      if (_val.Type != null)
      {
        return _val.Get<T>();
      }
      else
      {
        if (_obj == null)
        {
          return default;
        }
        else if (_obj is T tobj)
        {
          return tobj;
        }
        else if (ImplicitConversionTable.HasImplicitConversion(t_in, t_out, out Type t))
        {
          MethodInfo mRead = typeof(UnaryOp<>).MakeGenericType(new Type[] { t_in })
            .GetMethod(nameof(UnaryOp<T>.CastIntermediate), BindingFlags.Public | BindingFlags.Static);

          MethodInfo gmRead = mRead.MakeGenericMethod(new Type[] { t });
          return (T)gmRead.Invoke(null, new object[] { _obj, t });
        }
      }
      throw new InvalidValCastException(t_out, p.u);
    }

    /// <summary>Retrieves the value as a bool</summary>
    public static explicit operator bool(Val a) { return a.Cast<bool>(); }

    /// <summary>Retrieves the value as a 32-bit integer</summary>
    public static explicit operator int(Val a) { return a.Cast<int>(); }

    /// <summary>Retrieves the value as a floating point</summary>
    public static explicit operator float(Val a) { return a.Cast<float>(); }

    /// <summary>Retrieves the value as a string</summary>
    public static explicit operator string(Val a) { return a.Cast<string>(); }

    /// <summary>Retrieves the value as float2</summary>
    public static explicit operator float2 (Val a) { return a.Cast<float2>(); }

    /// <summary>Retrieves the value as float3</summary>
    public static explicit operator float3(Val a) { return a.Cast<float3>(); }

    /// <summary>Retrieves the value as float2</summary>
    public static explicit operator float4(Val a) { return a.Cast<float4>(); }

    /// <summary>Retrieves a boolean array</summary>
    public static explicit operator bool[](Val a) { return a.Cast<bool[]>(); }

    /// <summary>Retrieves an integer array</summary>
    public static explicit operator int[] (Val a) { return a.Cast<int[]>(); }

    /// <summary>Retrieves a floating point array</summary>
    public static explicit operator float[] (Val a) { return a.Cast<float[]>(); }

    /// <summary>Retrieves a string array</summary>
    public static explicit operator string[] (Val a) { return a.Cast<string[]>(); }

    /// <summary>Retrieves the value as an object</summary>
    public object Value { get => Cast<object>(); }

    /// <summary>Defines a value</summary>
    public Val(Type type) { this = new Val(); _type = type; _val = new ValPrim(type); }

    /// <summary>Defines a value</summary>
    public Val(bool val) { this = new Val(); _type = typeof(bool); _val = new ValPrim(val); }

    /// <summary>Defines a value</summary>
    public Val(int val) { this = new Val(); _type = typeof(int); _val = new ValPrim(val); }

    /// <summary>Defines a value</summary>
    public Val(float val) { this = new Val(); _type = typeof(float); _val = new ValPrim(val); }

    /// <summary>Defines a value</summary>
    public Val(string val) { this = new Val(); _type = typeof(string); _obj = val; }

    /// <summary>Defines a value</summary>
    public Val(float2 val) { this = new Val(); _type = typeof(float2); _obj = val; }

    /// <summary>Defines a value</summary>
    public Val(float3 val) { this = new Val(); _type = typeof(float3); _obj = val; }

    /// <summary>Defines a value</summary>
    public Val(float4 val) { this = new Val(); _type = typeof(float4); _obj = val; }

    /// <summary>Defines a value</summary>
    public Val(bool[] val) { this = new Val(); _type = typeof(bool[]);  _obj = val; }

    /// <summary>Defines a value</summary>
    public Val(int[] val) { this = new Val(); _type = typeof(int[]); _obj = val; }

    /// <summary>Defines a value</summary>
    public Val(float[] val) { this = new Val(); _type = typeof(float[]); _obj = val; }

    /// <summary>Defines a value</summary>
    public Val(string[] val) { this = new Val(); _type = typeof(string[]); _obj = val; }

    /// <summary>Defines a value</summary>
    public Val(object val) 
    { 
      this = new Val();
      if (val != null)
      {
        Type type = val.GetType();
        _type = type;
        if (type == typeof(bool)) { _val = new ValPrim((bool)val); }
        else if (type == typeof(byte)) { _val = new ValPrim((byte)val); }
        else if (type == typeof(sbyte)) { _val = new ValPrim((sbyte)val); }
        else if (type == typeof(short)) { _val = new ValPrim((short)val); }
        else if (type == typeof(ushort)) { _val = new ValPrim((ushort)val); }
        else if (type == typeof(int)) { _val = new ValPrim((int)val); }
        else if (type == typeof(uint)) { _val = new ValPrim((uint)val); }
        else if (type == typeof(float)) { _val = new ValPrim((float)val); }
        else _obj = val;
      }
    }


    /// <summary>Represents a null value</summary>
    public static readonly Val NULL = new Val();

    /// <summary>Represents a boolean true value</summary>
    public static readonly Val TRUE = new Val(true);

    /// <summary>Represents a boolean false value</summary>
    public static readonly Val FALSE = new Val(false);

    /// <summary>Determines if a value is null</summary>
    public bool IsNull { get { return Value == null; } }

    /// <summary>Determines if a value is true</summary>
    public bool IsTrue { get { return Cast<bool>(); } }

    /// <summary>Determines if a value is false</summary>
    public bool IsFalse { get { return !Cast<bool>(); } }

    /// <summary>Determines if a value is an array</summary>
    public bool IsArray { get { return Value is Array; } }

    /// <summary>Gets the string representation of the value</summary>
    public override string ToString()
    {
      return "{{Type: \"{0}\"  Value: \"{1}\"}}".F(Type.FullName ?? "<null>", Value?.ToString() ?? "<null>"); ;
    }
  }
}
