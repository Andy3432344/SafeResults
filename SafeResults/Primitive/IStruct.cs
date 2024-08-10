namespace SafeResults.Primitive;

public interface IStruct : IPrimitive
{
    IResult<IDictionary<string, IPrimitive>> GetValue();
    IFieldName[] FieldNames { get; }

    IPrimitive this[IFieldName field] { get; set; }
}

public interface IFieldName { string Name { get; } }

public interface IStructBuilder
{
    IResult AddField<T>(string name, T value);
    IResult AddBoolField(string name, bool value);
    IResult AddStringField(string name, string value);
    IResult AddNumberField(string name, int value);
    IResult AddNumberField(string name, float value);
    IResult AddNumberField(string name, double value);
    IResult AddNumberField(string name, decimal value);
    IResult AddDateField(string name, DateTime value);
    IResult AddDateField(string name, DateOnly value);
    IResult AddDTimeField(string name, TimeOnly value);
    IResult AddStructField(string name, IStruct value);

}

