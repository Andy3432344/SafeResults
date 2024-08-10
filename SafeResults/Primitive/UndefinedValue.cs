using SafeResults.Primitive;
using static SafeResults.ResultExtensions;
namespace SafeResults.Primitive;

public struct UndefinedValue : IPrimitive, IUndefined<UndefinedValue>, IEquatable<IPrimitive>
{
    public bool IsNumeric => false;
    public bool IsInteger => false;
    public bool IsBool => false;

    public bool IsString => false;

    public bool IsDate => false;

    public bool IsTime => false;


    public string ValueString => nameof(UndefinedValue);

    public IResult<T> GetValue<T>()
    {
        return error<T>("Error: Undefined");
    }

    public bool Equals(IPrimitive? other)
    {
        return false;
    }

    public int GetValueHashCode()
    {
        return 0;
    }

    private static Lazy<UndefinedValue> lazy = new();
    public static UndefinedValue Instance => lazy.Value;

}
