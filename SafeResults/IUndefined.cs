namespace SafeResults;

public interface IUndefined;

public interface IUndefined<T> : IUndefined
{
    abstract static T Instance { get; }
}