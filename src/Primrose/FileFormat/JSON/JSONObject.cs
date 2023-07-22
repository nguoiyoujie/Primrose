using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Primrose.Primitives.ValueTypes;

namespace Primrose.FileFormat.JSON
{
  /// <summary>Denotes the type of a JSON object</summary>
  public enum JSONValueType
  {
    /// <summary>Denotes a null value</summary>
    NULL,

    /// <summary>Denotes a boolean value</summary>
    BOOLEAN,

    /// <summary>Denotes a numeric value. Typically this is compatible with double</summary>
    NUMERIC,

    /// <summary>Denotes a string object</summary>
    STRING,

    /// <summary>Denotes an array of JSON-compatible values</summary>
    ARRAY,

    /// <summary>Denotes a JSON object, with unordered pairs of string keys and JSON-compatible values</summary>
    OBJECT
  }

  /// <summary>Holds a JSON-compatible value or object</summary>
  [StructLayout(LayoutKind.Explicit, Size = 32)]
  public struct JSONValue
  {
    // valuetypes (up to 64-bit)
    [FieldOffset(0)]
    private bool v_boolean;
    [FieldOffset(0)]
    private float v_single;
    [FieldOffset(0)]
    private double v_double;
    [FieldOffset(0)]
    private int v_int;
    [FieldOffset(0)]
    private uint v_uint;
    [FieldOffset(0)]
    private long v_long;
    [FieldOffset(0)]
    private ulong v_ulong;

    // objects (up to 64-bit)
    [FieldOffset(8)]
    private string o_string;
    [FieldOffset(8)]
    private List<JSONValue> o_array;
    [FieldOffset(8)]
    private List<Pair<string, JSONValue>> o_object;

    [FieldOffset(16)]
    private JSONValueType j_type;

    [FieldOffset(24)]
    private Type t_type;


    //public JSONValue() { _type = JSONValueType.NULL; }
    /// <summary>Creates a JSON-compatible boolean value</summary>
    public JSONValue(bool value) { this = new JSONValue(); j_type = JSONValueType.BOOLEAN; t_type = typeof(bool);  v_boolean = value; }

    /// <summary>Creates a JSON-compatible float value</summary>
    public JSONValue(float value) { this = new JSONValue(); j_type = JSONValueType.NUMERIC; t_type = typeof(float); v_single = value; }

    /// <summary>Creates a JSON-compatible double value</summary>
    public JSONValue(double value) { this = new JSONValue(); j_type = JSONValueType.NUMERIC; t_type = typeof(double); v_double = value; }

    /// <summary>Creates a JSON-compatible int value</summary>
    public JSONValue(int value) { this = new JSONValue(); j_type = JSONValueType.NUMERIC; t_type = typeof(int); v_int = value; }

    /// <summary>Creates a JSON-compatible uint value</summary>
    public JSONValue(uint value) { this = new JSONValue(); j_type = JSONValueType.NUMERIC; t_type = typeof(uint); v_uint = value; }

    /// <summary>Creates a JSON-compatible long value</summary>
    public JSONValue(long value) { this = new JSONValue(); j_type = JSONValueType.NUMERIC; t_type = typeof(long); v_long = value; }

    /// <summary>Creates a JSON-compatible ulong value</summary>
    public JSONValue(ulong value) { this = new JSONValue(); j_type = JSONValueType.NUMERIC; t_type = typeof(ulong); v_ulong = value; }

    /// <summary>Creates a JSON-compatible string object</summary>
    public JSONValue(string value) { this = new JSONValue(); j_type = JSONValueType.STRING; t_type = typeof(string); o_string = value; }

    /// <summary>Creates a JSON-compatible array object, copying the array elements</summary>
    public JSONValue(IEnumerable<JSONValue> array) { this = new JSONValue(); j_type = JSONValueType.ARRAY; o_array = new List<JSONValue>(array); }

    /// <summary>Creates a JSON-compatible string object, copying the object elements</summary>
    public JSONValue(IEnumerable<Pair<string, JSONValue>> obj) { this = new JSONValue(); j_type = JSONValueType.OBJECT; o_object = new List<Pair<string, JSONValue>>(obj); }

    /// <summary>Retrieves the value as an object</summary>
    public object Get()
    {
      if (j_type == JSONValueType.NULL) { return null; }
      else if (j_type == JSONValueType.NUMERIC)
      {
#pragma warning disable HAA0601 // Explicit boxing for object return
        if (t_type == typeof(bool)) { return v_boolean; }
        else if (t_type == typeof(float)) { return v_single; }
        else if (t_type == typeof(double)) { return v_double; }
        else if (t_type == typeof(int)) { return v_int; }
        else if (t_type == typeof(uint)) { return v_uint; }
        else if (t_type == typeof(long)) { return v_long; }
        else if (t_type == typeof(ulong)) { return v_ulong; }
        else return 0;
#pragma warning restore HAA0601
      }
      else if (j_type == JSONValueType.STRING) { return o_string; }
      else if (j_type == JSONValueType.ARRAY) { return o_array; }
      else if (j_type == JSONValueType.OBJECT) { return o_object; }
      else return null;
    }
  }
}
