using SafeResults.Primitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SafeResults.Primitive.Extensions;
namespace SafeResults;

public readonly struct Key : IEquatable<Key>
{
    private readonly IPrimitive key;
    public IPrimitive Value => key;

    public Key()
    {
        key = UndefinedValue.Instance;
    }

    private Key(object key)
    {
        if (key.GetAnonymousPrimitive(out var k))
            this.key = k;
        else
            this.key = UndefinedValue.Instance;
    }

    public bool Equals(Key other)
    {
        if (!other.IsValid() || !IsValid())
            return false;

        return other.key.Equals(key);
    }

    public bool IsValid() => key is not UndefinedValue;

    public static Key DefaultInvalid => new();

    public static explicit operator Key(string key)
    {
        return new Key(key);
    }

    public static explicit operator Key(int key)
    {
        return new Key(key);
    }
    public static explicit operator Key(Guid key)
    {
        return new Key(key);
    }

    public override bool Equals(object? obj)
    {
        return obj is Key && Equals((Key)obj);
    }

    public static bool operator ==(Key left, Key right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Key left, Key right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        if (key == null)
            return 0;

        return key.GetHashCode();
    }
}
