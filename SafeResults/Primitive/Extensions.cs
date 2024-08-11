using System.Diagnostics.CodeAnalysis;

namespace SafeResults.Primitive;

public static class Extensions
{
    public static bool GetAnonymousPrimitive(this object? value,[NotNullWhen(true)] out IPrimitive? primitive)
    {
        primitive = null;
        if (value == null)
            return false; 

        switch (value)
        {
            case long l:
                primitive = new IntegerNumber(l);
                return true;
            case int i:
                primitive = new IntegerNumber(i);
                return true;
            case float f:
                primitive = new DecimalNumber(f);
                return true;
            case double n:
                primitive = new DecimalNumber(n);
                return true;
            case decimal m:
                primitive = new DecimalNumber(m);
                return true;
            case DateOnly date:
                primitive = new DateAndOrTimeValue(date);
                return true;
            case TimeOnly time:
                primitive = new DateAndOrTimeValue(time);
                return true;
            case DateTime datetime:
                primitive = new DateAndOrTimeValue(datetime);
                return true;
            case bool b:
                primitive = new BooleanValue(b);
                return true;
            case string s:
                primitive = new StringValue(s);
                return true;
            case char c:
                primitive = new StringValue(c.ToString());
                return true;
            case Guid g:
                primitive = new UniqueIdentifierValue(g);
                return true;
            default:
                primitive = UndefinedValue.Instance;
                return false;
        }
    }



    internal static IPrimitive FromStringPrimitive<T>(this string value)
    {
        var newValue = (T)Convert.ChangeType(value, typeof(T));
        return newValue.GetPrimitive();
    }

    public static IPrimitive GetPrimitive<T>(this T? value)
    {
        return value.GetObjectPrimitive<T>();
    }

    private static IPrimitive GetObjectPrimitive<T>(this object? value)
    {
        if (value != null)
        {
            if (value is string s && typeof(T) != typeof(string))
            {
                return (s ?? "")
                    .FromStringPrimitive<T>();
            }

            switch (typeof(T))
            {
                case Type t when t == typeof(string) || t == typeof(char) && value.ToString()?.Length == 1:
                    return new StringValue((string)value);
                case Type t when t == typeof(DateTime):
                    return new DateAndOrTimeValue((DateTime)value);
                case Type t when t == typeof(DateOnly):
                    return new DateAndOrTimeValue((DateOnly)value);
                case Type t when t == typeof(TimeOnly):
                    return new DateAndOrTimeValue((TimeOnly)value);
                case Type t when t == typeof(bool):
                    return new BooleanValue((bool)value);
                case Type t when t == typeof(byte):
                    return new IntegerNumber((byte)value);
                case Type t when t == typeof(short):
                    return new IntegerNumber((short)value);
                case Type t when t == typeof(int):
                    return new IntegerNumber((int)value);
                case Type t when t == typeof(long):
                    return new IntegerNumber((long)value);
                case Type t when t == typeof(sbyte):
                    return new IntegerNumber((sbyte)value);
                case Type t when t == typeof(ushort):
                    return new IntegerNumber((ushort)value);
                case Type t when t == typeof(uint):
                    return new IntegerNumber((uint)value);
                case Type t when t == typeof(ulong):
                    return new IntegerNumber((ulong)value);
                case Type t when t == typeof(double):
                    return new DecimalNumber((double)value);
                case Type t when t == typeof(decimal):
                    return new DecimalNumber((decimal)value);
                case Type t when t == typeof(float):
                    return new DecimalNumber((float)value);
                case Type t when t == typeof(Guid):
                    return new UniqueIdentifierValue((Guid)value);

            }
        }

        return new UndefinedValue();

    }

    //public static IPrimitive GetPrimitiveProperty<T>(this T obj, string propertyName)
    //{
    //    var prop = obj.GetType().GetProperty(propertyName);

    //    if (prop is PropertyInfo p)
    //    {
    //        var val = p.GetValue(obj);
    //        return val.GetPrimitive<T>();
    //    }

    //    return new Undefined();
    //}
}