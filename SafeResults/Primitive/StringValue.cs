using static SafeResults.ResultExtensions;
namespace SafeResults.Primitive;

public struct StringValue : IPrimitive, IEquatable<StringValue>
{
    private string value;
    public StringValue(string value)
    {
        this.value = value;
    }
    public StringValue(char value)
    {
        this.value = value.ToString();
    }
    public string ValueString => value;
    public bool IsNumeric => false;

    public bool IsInteger => false;
    public bool IsBool => false;

    public bool IsString => true;

    public bool IsDate => false;

    public bool IsTime => false;

    public bool Equals(StringValue other)
    {
        return other.value.Equals(value);
    }

    public bool Equals(IPrimitive? other)
    {
        if (other is IUndefined)
            return false;

        if (other is not null)
            return other.ValueString == ValueString;

        return false;
    }

    public IResult<T> GetValue<T>()
    {
        if (typeof(T) == typeof(string))
            return (IResult<T>)ok<string>(value);


        if (typeof(T) == typeof(char) && value.Length == 1)
            return (IResult<T>)ok<char>(value[0]);

        return error<T>($"{typeof(T).FullName} is not compatible with {typeof(bool).FullName}");

    }

    public override int GetHashCode() =>
        GetValueHashCode();

    public int GetValueHashCode() =>
        ValueString.GetHashCode();

}
