namespace SafeResults;

public interface IResult;

public interface IFail<T> : IResult<T>, IFail;
public interface IFail : IResult { string Message { get; } }

public interface IOkBut<T> : IOkBut, IOk<T>;
public interface IOkBut : IOk { string Message { get; } }
public interface IOk<T> : IOk, IResult<T>
{
    T Value { get; }
}

public interface IOk : IResult;
public interface IResult<T> : IResult;

public interface IOptionResult<T> : IResult<T>
{
    bool Some { get; }
    bool None { get; }
}
