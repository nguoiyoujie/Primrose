// Adapted from: https://stackoverflow.com/questions/1189144/c-sharp-non-boxing-conversion-of-generic-enum-to-int

using Primrose.Primitives.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Primrose.Expressions
{
  internal static class ImplicitConversionTable
  {
    private static readonly List<Pair<Type, Type>> _table = new List<Pair<Type, Type>>();
    private static readonly List<Pair<Type, Type>> _ntable = new List<Pair<Type, Type>>();

    static ImplicitConversionTable()
    {
      // Reference for primitives: https://msdn.microsoft.com/en-us/library/y5b434w4.aspx
      // generate initial cache for core primitives as reflection methods will not include them
      foreach (Type t in new Type[] { typeof(short), typeof(int), typeof(long), typeof(float), typeof(double), typeof(decimal) })
        _table.Add(new Pair<Type, Type>(typeof(sbyte), t));

      foreach (Type t in new Type[] { typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) })
        _table.Add(new Pair<Type, Type>(typeof(byte), t));

      foreach (Type t in new Type[] { typeof(int), typeof(long), typeof(float), typeof(double), typeof(decimal) })
        _table.Add(new Pair<Type, Type>(typeof(short), t));

      foreach (Type t in new Type[] { typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) })
        _table.Add(new Pair<Type, Type>(typeof(ushort), t));

      foreach (Type t in new Type[] { typeof(long), typeof(float), typeof(double), typeof(decimal) })
        _table.Add(new Pair<Type, Type>(typeof(int), t));

      foreach (Type t in new Type[] { typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) })
        _table.Add(new Pair<Type, Type>(typeof(uint), t));

      foreach (Type t in new Type[]  { typeof(float), typeof(double), typeof(decimal) })
        _table.Add(new Pair<Type, Type>(typeof(long), t));

      foreach (Type t in new Type[] { typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) })
        _table.Add(new Pair<Type, Type>(typeof(char), t));

      foreach (Type t in new Type[] { typeof(double) })
        _table.Add(new Pair<Type, Type>(typeof(float), t));
      
      foreach (Type t in new Type[] { typeof(float), typeof(double), typeof(decimal) })
        _table.Add(new Pair<Type, Type>(typeof(ulong), t));
    }

    public static bool HasImplicitConversion(Type baseType, Type targetType, out Type intermediateType)
    {
      intermediateType = targetType;

      // special, force ToString() if target is string
      if (targetType == typeof(string)) 
        return true;

      if (targetType == typeof(Array))
      { }

      Pair<Type, Type> p = new Pair<Type, Type>(baseType, targetType);
      if (_table.Contains(p))
        return true;

      if (_ntable.Contains(p))
        return false;

      GetImplicitConversions(baseType, baseType);
      GetImplicitConversions(targetType, baseType);

      //if (_table.Contains(p))
      foreach (Pair<Type, Type> pair in _table) // _table.Where(u => { return u.t.IsAssignableFrom(p.t) && p.u.IsAssignableFrom(u.u); }) Avoid 'Where', heavy allocation
      {
        if (pair.t.IsAssignableFrom(p.t) && p.u.IsAssignableFrom(pair.u))
        {
          intermediateType = pair.u;
          return true;
        }
      }

      _ntable.Add(p);
      return false;

      //bool result = HasImplicitConversion(baseType, baseType, targetType) || HasImplicitConversion(targetType, baseType, targetType);
      //if (result) // cache result
      //  _table.Add(p);
      //else
      //  _ntable.Add(p);

      //return result;
    }

    /*
    private static bool HasImplicitConversion(Type definedOn, Type baseType, Type targetType)
    {
      return definedOn.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .Where(mi => mi.Name == "op_Implicit" && mi.ReturnType == targetType)
                    .Any(mi =>
                    {
                      ParameterInfo pi = mi.GetParameters().FirstOrDefault();
                      return pi != null && pi.ParameterType == baseType;
                    });
    }
    */

    private static void GetImplicitConversions(Type definedOn, Type baseType)
    {
      foreach (MethodInfo m in definedOn.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .Where(mi => mi.Name == "op_Implicit"))
      {
        ParameterInfo pi = m.GetParameters().FirstOrDefault();
        if (pi != null)
        {
          Pair<Type, Type> p = new Pair<Type, Type>(pi.ParameterType, m.ReturnType);
          if (!_table.Contains(p))
            _table.Add(p);
        }
      }
    }
  }
}
