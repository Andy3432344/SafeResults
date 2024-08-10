namespace SafeResults;

public class OptionResult<T> : IOptionResult<T>
{
    private readonly T @default;
    public bool Some { get; private set; } = true;
    public bool None => !Some;
    public string Message { get; private set; } = "";
    public OptionResult(T @default)
    {
        this.@default = @default;
    }

    public static T operator |(IResult<T> left, OptionResult<T> right)
    {
        if (left is IOk<T> k)
            return k.Value;

        if (left is IFail err)
            right.Message = err.Message;

        right.Some = false;
        return right.@default;
    }

    public static T operator |(OptionResult<T> left, IResult<T> right)
    {
        return right | left;
    }


    public static implicit operator OptionResult<T>(T value)
    {
        return new(value);
    }
}