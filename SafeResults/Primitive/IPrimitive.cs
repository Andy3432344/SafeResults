namespace SafeResults.Primitive;

public interface IPrimitive : IEquatable<IPrimitive>
{

    bool IsNumeric { get; }
    bool IsInteger { get; }
    bool IsBool { get; }
    bool IsString { get; }
    bool IsDate { get; }
    bool IsTime { get; }

    IResult<T> GetValue<T>();
    string ValueString { get; }
    int GetValueHashCode();
}
