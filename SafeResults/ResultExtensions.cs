namespace SafeResults;

public static class ResultExtensions
{
    private readonly static Lazy<Ok> okLazy = new();

    public static IOk ok() => okLazy.Value;
    public static IOk<T> ok<T>(T value) => new Ok<T>(value);
    public static IOkBut okBut(string msg) => new OkBut(msg);
    public static IResult<T> okBut<T>(T Value, string msg) => new OkBut<T>(Value, msg);
    public static IFail error(string msg) => new Error(msg);
    public static IFail<T> error<T>(string msg) => new Error<T>(msg);
    public static OptionResult<T> option<T>(T defaultValue) => new OptionResult<T>(defaultValue)
        
        
        ;
    private record Ok : IOk;
    private record OkBut(string Message) : IOkBut;
    private record Error(string Message) : IFail;

    private record Ok<T>(T Value) : IOk<T>;

    private record OkBut<T>(T Value, string Message) : Ok<T>(Value), IOk;
    private record Error<T>(string Message) : IFail<T>;

    public static T Safe<T>(this IResult<T> res) where T : IUndefined
    {
        if (res is IOk<T> k)
            return k.Value;

        return (T)typeof(T).GetInterfaces()
            .Single(t =>
                t.Name.StartsWith(nameof(IUndefined)) &&
                t.IsGenericType &&
                typeof(T).IsAssignableFrom(t.GenericTypeArguments[0]))
                .GetMethods()
                .Single(m => m.Name == "get_" + nameof(IUndefined<T>.Instance))
                .Invoke(null, null)!;
    }
    public static T Safe<T>(this IResult<T> res, T @default)
    {
        if (res is IOk<T> k)
            return k.Value;

        return @default;
    }
}
