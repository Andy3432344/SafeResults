using static SafeResults.ResultExtensions;
namespace SafeResults.Primitive;



/// <summary>
/// Yeah idk, probably don't use this....
/// </summary>
internal sealed class Struct : IStruct
{
    private readonly Lazy<UndefinedValue> undefined = new();
    private class FieldName : IFieldName
    {
        public required string Name { get; init; }
    }

    private Dictionary<IFieldName, IPrimitive> values = new();

    public IPrimitive this[IFieldName field] { get => GetFieldValue(field); set => SetFieldValue(field, value); }

    public IFieldName[] FieldNames => values.Keys.ToArray();


    private IPrimitive GetFieldValue(IFieldName field)
    {
        if (!values.TryGetValue(field, out var value))
            value = undefined.Value;

        return value;
    }
    /// <summary>
    /// Set the value of some existing field
    /// </summary>
    /// <param name="field">A field contained in this Struct</param>
    /// <param name="value">The Value </param>
    /// <exception cref="ArgumentNullException">Null field</exception>
    /// <exception cref="MissingMemberException">Unrecognized field</exception>
    private void SetFieldValue(IFieldName field, IPrimitive? value)
    {
        if (field == null)
            throw new ArgumentNullException(nameof(field));

        if (!values.ContainsKey(field))
            throw new MissingMemberException(nameof(field));


        if (value == null)
            value = undefined.Value;

        values[field] = value;
    }


    public bool IsNumeric => false;
    public bool IsInteger => false;

    public bool IsBool => false;
    public bool IsString => false;
    public bool IsDate => false;
    public bool IsTime => false;
    public string ValueString => string.Join("\n", values.Select(v => new { Name = v.Key, Value = v.Value.ValueString }.Value));

    public override int GetHashCode() =>
        GetValueHashCode();

    public int GetValueHashCode() =>
        values.Values.Select(v=> v.GetHashCode()).Aggregate(1, (x, y) => x * y); //idk

    public IResult<IDictionary<string, IPrimitive>> GetValue()
    {
        return ok<IDictionary<string, IPrimitive>>(values.ToDictionary(v => v.Key.Name, v => v.Value));
    }

    public IResult<T> GetValue<T>()
    {
        if (typeof(T) == typeof(IDictionary<string, IPrimitive>))
        {
            return (IResult<T>)GetValue();
        }
        return error<T>($"UnsignedValue of {typeof(IStruct).FullName} must be retrieved using {nameof(IDictionary<string, IPrimitive>)}");
    }

    public bool Equals(IPrimitive? other)
    {
        throw new NotImplementedException();
    }
}