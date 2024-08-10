using static SafeResults.ResultExtensions;
namespace SafeResults.Primitive;

public struct DateAndOrTimeValue : IPrimitive, IEquatable<DateAndOrTimeValue>
{
    private bool date = false;
    private bool time = false;

    private DateTime value;

    public DateAndOrTimeValue(DateTime value)
    {
        this.value = value;
    }

    public DateAndOrTimeValue(DateOnly value) : this(value.ToDateTime(new TimeOnly()))
    {
        date = true;
    }
    public DateAndOrTimeValue(TimeOnly value) : this(DateTime.Today.AddTicks(value.Ticks))
    {
        time = true;
    }
    public string ValueString => value.ToString("yyyy-MM-ddTHH:mm:ss");
    public bool IsNumeric => false;
    public bool IsInteger => false;
    public bool IsBool => false;

    public bool IsString => false;

    public bool IsDate => !date && !time || date;

    public bool IsTime => !date && !time || time;
    public DateTime Value => value;
    public bool Equals(DateAndOrTimeValue other)
    {
        return other.date == date && other.time == time && other.value.Equals(value);
    }

    public bool Equals(IPrimitive? other)
    {
        if (other is IUndefined)
            return false;

        if (other is DateAndOrTimeValue d)
            return Equals(d);

        return false;
    }

    public IResult<T> GetValue<T>()
    {
        if (typeof(T) == typeof(DateTime))
        {
            return (IResult<T>)ok(value);
        }

        if (typeof(T) == typeof(DateOnly))
        {
            return (IResult<T>)ok(new DateOnly(value.Year, value.Month, value.Day));
        }
        if (typeof(T) == typeof(TimeOnly))
        {
            return (IResult<T>)ok(new TimeOnly(value.Ticks));
        }

        return error<T>($"{typeof(T).FullName} is not compatible with {typeof(DateTime).FullName}");
    }

    public override int GetHashCode() =>
        GetValueHashCode();

    public int GetValueHashCode() =>
        Value.GetHashCode();

}
