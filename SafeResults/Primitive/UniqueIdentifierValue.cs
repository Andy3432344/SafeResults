using System.Security.Cryptography;
using System.Text;
using static SafeResults.ResultExtensions;
namespace SafeResults.Primitive;

public struct UniqueIdentifierValue : IPrimitive, IEquatable<UniqueIdentifierValue>
{
    private Guid value;

    public UniqueIdentifierValue(Guid value)
    {
        this.value = value;
    }
    public UniqueIdentifierValue(string value)
    {
        var byt = Encoding.UTF8.GetBytes(value);
        var hash = MD5.Create().ComputeHash(byt);
        this.value = new Guid(hash);
    }

    public string ValueString => value.ToString();
    public bool IsNumeric => false;

    public bool IsInteger => false;
    public bool IsBool => false;

    public bool IsString => true;

    public bool IsDate => false;

    public bool IsTime => false;
    public Guid Value => value;
    public bool Equals(UniqueIdentifierValue other)
    {
        return other.value.Equals(value);
    }

    public override int GetHashCode() =>
        GetValueHashCode();

    public int GetValueHashCode() =>
        Value.GetHashCode();
    public bool Equals(IPrimitive? other)
    {
        if (other == null || other is IUndefined)
            return false;

        if (other is UniqueIdentifierValue uid)
            return uid.value.Equals(value);

        return new UniqueIdentifierValue(other.ValueString).Equals(this);
    }

    public IResult<T> GetValue<T>()
    {
        if (typeof(T) == typeof(Guid))
            return (IResult<T>)ok<Guid>(value);


        if (typeof(T) == typeof(string))
            return (IResult<T>)ok<string>(ValueString);

        return error<T>($"{typeof(T).FullName} is not compatible with {typeof(bool).FullName}");

    }
    public static implicit operator UniqueIdentifierValue(Guid value)
    {
        return new UniqueIdentifierValue(value);
    }
}
