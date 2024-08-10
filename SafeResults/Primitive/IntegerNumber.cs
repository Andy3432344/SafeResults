using static SafeResults.ResultExtensions;
namespace SafeResults.Primitive;

public struct IntegerNumber : IPrimitive, IEquatable<IntegerNumber>
{
    private ulong uvalue;
    private long svalue;

    private bool signed = false;



    public IntegerNumber(int value) { this.svalue = (long)value; }
    public IntegerNumber(byte value) { this.svalue = value; }
    public IntegerNumber(sbyte value) { this.svalue = (long)value; }
    public IntegerNumber(short value) { this.svalue = (long)value; }
    public IntegerNumber(long value) { this.svalue = (long)value; }
    public IntegerNumber(ushort value) { this.uvalue = value;signed = true; }
    public IntegerNumber(uint value) { this.uvalue = value; signed = true; }
    public IntegerNumber(ulong value) { this.uvalue = value; signed = true; }

    public string ValueString => signed ? svalue.ToString() : uvalue.ToString();
    public bool IsNumeric => true;

    public bool IsInteger => true;

    public bool IsBool => false;

    public bool IsString => false;

    public bool IsDate => false;

    public bool IsTime => false;
    public ulong UnsignedValue => signed ? (ulong)svalue : uvalue;
    public long SignedValue => signed ? (long)uvalue : svalue;


    public bool Equals(IntegerNumber other)
    {
        return other.signed ?
            other.svalue.Equals(svalue):
            other.uvalue.Equals(uvalue);
    }

    public bool Equals(IPrimitive? other)
    {
        if (other is IUndefined)
            return false;

        if (other is IntegerNumber i)
            return signed ? i.svalue == svalue : i.uvalue==uvalue;

        if (other is DecimalNumber dec && dec.Value % 2 == 0)
            return (ulong)dec.Value == uvalue;

        return false;
    }
    public static implicit operator IntegerNumber(int val) => new(val);
    public static implicit operator IntegerNumber(uint val) => new(val);
    public static implicit operator IntegerNumber(long val) => new(val);
    public static implicit operator IntegerNumber(ulong val) => new(val);
    public static implicit operator IntegerNumber(short val) => new(val);
    public static implicit operator IntegerNumber(Byte val) => new(val);

    public IResult<T> GetValue<T>() =>
        typeof(T) switch
        {
            Type t when t == typeof(int) && svalue <= int.MaxValue =>
             (IResult<T>)ok<int>((int)svalue),
            Type t when t == typeof(uint) && uvalue <= uint.MaxValue && uvalue >= uint.MinValue =>
             (IResult<T>)ok<uint>((uint)uvalue),
            Type t when t == typeof(byte) && svalue <= byte.MaxValue && svalue >= byte.MinValue =>
             (IResult<T>)ok<byte>((byte)svalue),
            Type t when t == typeof(sbyte) && svalue <= (int)sbyte.MaxValue =>
             (IResult<T>)ok<sbyte>((sbyte)svalue),
            Type t when t == typeof(short) && svalue <= (int)short.MaxValue =>
             (IResult<T>)ok<short>((short)svalue),
            Type t when t == typeof(ushort) && uvalue <= ushort.MaxValue =>
             (IResult<T>)ok<ushort>((ushort)uvalue),
            Type t when t == typeof(long) && svalue <= long.MaxValue =>
             (IResult<T>)ok<long>((long)svalue),
            Type t when t == typeof(ulong) => (IResult<T>)ok<ulong>((ulong)uvalue),
            _ => error<T>($"{typeof(T).FullName} is not compatible with {typeof(ulong).FullName}")

        };

    public override int GetHashCode() =>
        GetValueHashCode();

    public int GetValueHashCode() =>
        UnsignedValue.GetHashCode();
}
