// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace UniquomeApp.Utilities;

public static class TypeUtilities
{
    public  const double DoubleTolerance = 0.00000000001;

    public static bool CheckObjectsForDifference<T>(T object1, T object2, IList<string> propertiesToIgnore, double doubleTolerance = DoubleTolerance)
    {
        //TODO: Verify that in case of nulls true is the correct answer
        if (object1 == null && object2 == null) return true;
        if (object1 != null && object2 == null) return false;
        if (object1 == null && object2 != null) return false;


        var propertyInfos = typeof(T).GetProperties();
        foreach (var propertyInfo in propertyInfos)
        {
            if (propertyInfo.PropertyType.IsClass)
            {
                /*TODO: Revisit this idea. I want to pass a list of interfaces and if the type implements any of them then at runtime check the properties of that  type bwtween object 1 / object 2
                 * //,IList<Type> interfacesToCheck)
                foreach (var k in interfacesToCheck)
                {
                    if (propertyInfo.PropertyType.GetInterface(k.Name) != null)
                    {
                        var hasDiffs = (CheckObjectsForDifference(object1 as propertyInfo.PropertyType,
                            object2 as propertyInfo.PropertyType, null, null));
                    }
                }
                */
                continue;
            }

            if (propertiesToIgnore.Contains(propertyInfo.Name)) continue;

            if (propertyInfo.PropertyType == typeof(int))
            {
                if ((int)propertyInfo.GetValue(object1) != (int)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(int?))
            {
                if ((int?)propertyInfo.GetValue(object1) != (int?)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(short))
            {
                if ((short)propertyInfo.GetValue(object1) != (short)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(short?))
            {
                if ((short?)propertyInfo.GetValue(object1) != (short?)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(double))
            {
                if (Math.Abs((double)propertyInfo.GetValue(object1) - (double)propertyInfo.GetValue(object2)) > doubleTolerance)
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(double?))
            {
                if (!Equals((double?)propertyInfo.GetValue(object1), (double?)propertyInfo.GetValue(object2)))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(decimal))
            {
                if ((decimal)propertyInfo.GetValue(object1) != (decimal)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(decimal?))
            {
                if ((decimal?)propertyInfo.GetValue(object1) != (decimal?)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(bool))
            {
                if ((bool)propertyInfo.GetValue(object1) != (bool)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(bool?))
            {
                if ((bool?)propertyInfo.GetValue(object1) != (bool?)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(string))
            {
                if ((string)propertyInfo.GetValue(object1) != (string)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(long))
            {
                if ((long)propertyInfo.GetValue(object1) != (long)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(long?))
            {
                if ((long?)propertyInfo.GetValue(object1) != (long?)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(byte))
            {
                if ((byte)propertyInfo.GetValue(object1) != (byte)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(byte?))
            {
                if ((byte?)propertyInfo.GetValue(object1) != (byte?)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(char))
            {
                if ((char)propertyInfo.GetValue(object1) != (char)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(char?))
            {
                if ((char?)propertyInfo.GetValue(object1) != (char?)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(float))
            {
                if (Math.Abs((float)propertyInfo.GetValue(object1) - (float)propertyInfo.GetValue(object2)) > doubleTolerance)
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(float?))
            {
                if (!Equals((float?)propertyInfo.GetValue(object1), (float?)propertyInfo.GetValue(object2)))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(sbyte))
            {
                if ((sbyte)propertyInfo.GetValue(object1) != (sbyte)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(sbyte?))
            {
                if ((sbyte?)propertyInfo.GetValue(object1) != (sbyte?)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(uint))
            {
                if ((uint)propertyInfo.GetValue(object1) != (uint)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(uint?))
            {
                if ((uint?)propertyInfo.GetValue(object1) != (uint?)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(ushort))
            {
                if ((ushort)propertyInfo.GetValue(object1) != (ushort)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(ushort?))
            {
                if ((ushort?)propertyInfo.GetValue(object1) != (ushort?)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(ulong))
            {
                if ((ulong)propertyInfo.GetValue(object1) != (ulong)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(ulong?))
            {
                if ((ulong?)propertyInfo.GetValue(object1) != (ulong?)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(DateTime))
            {
                if ((DateTime)propertyInfo.GetValue(object1) != (DateTime)propertyInfo.GetValue(object2))
                    return true;
            }
            if (propertyInfo.PropertyType == typeof(DateTime?))
            {
                if ((DateTime?)propertyInfo.GetValue(object1) != (DateTime?)propertyInfo.GetValue(object2))
                    return true;
            }
        }
        return false;
    }
}