using static SafeResults.ResultExtensions;
namespace SafeResults.Primitive;

public struct DecimalNumber : IPrimitive, IEquatable<DecimalNumber>
{
    private decimal value;

    public DecimalNumber(decimal value) { this.value = value; }

    public DecimalNumber(float value) : this((decimal)value) { }

    public DecimalNumber(double value) : this((decimal)value) { }
    public string ValueString => value.ToString();

    public bool IsNumeric => true;

    public bool IsInteger => false;

    public bool IsBool => false;

    public bool IsString => false;

    public bool IsDate => false;

    public bool IsTime => false;
    public decimal Value => value;


    public static implicit operator DecimalNumber(decimal value) { return new(value); }  

    public bool Equals(DecimalNumber other)
    {
        return other.value.Equals(value);
    }

    public bool Equals(IPrimitive? other)
    {
        if (other is IUndefined)
            return false;

        if (other is DecimalNumber dec)
            return dec.value == value;

        if (other is IntegerNumber integer && value % 2 == 0)
            return (ulong)value == integer.UnsignedValue;

        return false;
    }

    public IResult<T> GetValue<T>()
    {
        if (typeof(T) == typeof(decimal))
        {
            return (IResult<T>)ok<decimal>(value);
        }
        if (typeof(T) == typeof(float))
        {
            return (IResult<T>)ok<float>((float)value);
        }
        if (typeof(T) == typeof(double))
        {
            return (IResult<T>)ok<double>((double)value);
        }

        return error<T>($"{typeof(T).FullName} is not compatible with {typeof(decimal).FullName}");
    }

    public override int GetHashCode() =>
        GetValueHashCode();

    public int GetValueHashCode() =>
        Value.GetHashCode();

}
