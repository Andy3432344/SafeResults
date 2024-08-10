using static SafeResults.ResultExtensions;
namespace SafeResults.Primitive;

public struct BooleanValue : IPrimitive, IEquatable<BooleanValue>
{
    private bool value;

    public BooleanValue(bool value)
    {
        this.value = value;
    }

    public bool IsNumeric => false;

    public bool IsInteger => false;

    public bool IsBool => true;

    public string ValueString => value.ToString();
    public bool Value => value;

    public bool IsString => false;

    public bool IsDate => false;

    public bool IsTime => false;

    public bool Equals(BooleanValue other)
    {
        return other.value == value;
    }

    public bool Equals(IPrimitive? other)
    {
        if (other is IUndefined)
            return false;

        if (other is BooleanValue b)
            return b.value == value;

        return false;
    }

    public IResult<T> GetValue<T>()
    {
        if (typeof(T) == typeof(bool))
        {
            return (IResult<T>)GetValue();
        }
        return error<T>($"{typeof(T).FullName} is not compatible with {typeof(bool).FullName}");
    }

    public IResult<bool> GetValue()
    {
        return ok(value);
    }

    public override int GetHashCode() =>
        GetValueHashCode();

    public int GetValueHashCode() =>
        Value.GetHashCode();
}

