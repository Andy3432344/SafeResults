using SafeResults.Primitive;
using static SafeResults.ResultExtensions;



namespace SafeResults.Test;

public class PrimitiveUnitTests
{

    [Fact]
    public void TestPrimitiveNumbers()
    {
        IntegerNumber integer = 42;
        DecimalNumber dec = 1.41421356237M;
        var ansr = 42 + 1.41421356237M;
        var test = integer.SignedValue + dec.Value;
        Assert.Equal(ansr, test);

        long num1 = -111345;
        ulong num2 = ulong.MaxValue;
        short num3 = short.MinValue;
        Byte num4 = (Byte)3;

        IntegerNumber neg = num1;
        IntegerNumber big = num2;
        IntegerNumber small = num3;
        IntegerNumber tiny = num4;

        var res1 = neg.GetValue<long>() as IOk<long>;
        var res2 = big.GetValue<ulong>() as IOk<ulong>;
        var res3 = small.GetValue<short>() as IOk<short>;
        var res4 = tiny.GetValue<Byte>() as IOk<Byte>;

        Assert.Equal(num1, res1?.Value);
        Assert.Equal(num2, res2?.Value);
        Assert.Equal(num3, res3?.Value);
        Assert.Equal(num4, res4?.Value);
    }

    [Fact]
    public void TestPrimitiveKey()
    {
        Key intKey = (Key)42;
        Key stringKey = (Key)"A117b";
        Key guidKey = (Key)Guid.NewGuid();

        Dictionary<Key, int> sut = new();

        sut[intKey] = 1;
        sut[stringKey] = 2;
        sut[guidKey] = 3;

        Assert.Equal(1, sut[intKey]);
        Assert.Equal(2, sut[stringKey]);
        Assert.Equal(3, sut[guidKey]);

    }
}
